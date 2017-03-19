using System;
using System.Collections.Generic;
using System.IO;

namespace CSShapefile
{
	/// <summary>
	/// Shapefile reading support
	/// http://www.esri.com/library/whitepapers/pdfs/shapefile.pdf
	/// </summary>
	public class ShapefileReader
	{
		private readonly Stream _stream;
		/// <summary>
		/// Dictionary of shape reader functions indexed by shape type
		/// </summary>
		private readonly IDictionary<ShapeType, ReadRecordDelegate> _recordReaders;

		/// <summary>
		/// Reads a shapefile into memory from <paramref name="path"/>
		/// </summary>
		/// <returns></returns>
		/// <param name="path"></param>
		public static Shapefile ShapefileFromPath(string path)
		{
			using (FileStream fs = File.OpenRead(path))
			{
				ShapefileReader reader = new ShapefileReader(fs);
				return reader.Read();
			}
		}

		/// <summary>
		/// Initialises with a shapefile stream to read in
		/// </summary>
		/// <param name="stream"></param>
		public ShapefileReader(Stream stream)
		{
			_stream = stream;

			_recordReaders = new Dictionary<ShapeType, ReadRecordDelegate>
			{
				{ShapeType.NullShape, bytes => null},
				{ShapeType.Point, ReadPointRecord},
				{ShapeType.MultiPoint, ReadMultiPointRecord},
				{ShapeType.PolyLine, ReadPolyLineRecord},
				{ShapeType.Polygon, ReadPolygonRecord}
			};
		}

		/// <summary>
		/// Reads the contents of the shapefile stream into a useful in memory data structure
		/// </summary>
		/// <returns>In memory shapefile representation</returns>
		public Shapefile Read()
		{
			byte[] headerBytes = new byte[100];
			_stream.Read(headerBytes, 0, 100);

			// Read in the file header
			ShapefileHeader header = new ShapefileHeader
			{
				FileCode = Endian.Swap(BitConverter.ToInt32(headerBytes, 0)),
				FileLengthWords = Endian.Swap(BitConverter.ToInt32(headerBytes, 24)),
				Version = BitConverter.ToInt32(headerBytes, 28),
				ShapeType = (ShapeType)BitConverter.ToInt32(headerBytes, 32),
				BoundingBox = CreateXYZMBoundingBox(headerBytes, 36)
			};

			Shapefile shapefile = new Shapefile(header);

			// Read every shape record in the file into memory
			for (int wordsRead = 0; wordsRead < header.FileLengthWords;)
			{
				RecordHeader recordHeader = ReadRecordHeader();
				//Console.WriteLine($"{recordHeader.RecordNumber} - {recordHeader.ContentLengthBytes}");
				wordsRead += 4;

				if (recordHeader.ContentLengthWords == 0)
					break;

				ReadRecord(shapefile, recordHeader);
				wordsRead += recordHeader.ContentLengthWords;
			}

			Console.WriteLine(shapefile);

			return shapefile;
		}

		/// <summary>
		/// Reads in the 8 byte header of a shape record
		/// </summary>
		/// <returns>Record header</returns>
		private RecordHeader ReadRecordHeader()
		{
			byte[] headerBytes = new byte[8];

			_stream.Read(headerBytes, 0, 8);

			return new RecordHeader(
				Endian.Swap(BitConverter.ToInt32(headerBytes, 0)),
				Endian.Swap(BitConverter.ToInt32(headerBytes, 4)));
		}

		/// <summary>
		/// Reads in a reacord and adds it to the shapefile instance
		/// </summary>
		/// <param name="shapefile"></param>
		/// <param name="header">Header of record to be read in</param>
		private void ReadRecord(Shapefile shapefile, RecordHeader header)
		{
			byte[] buffer = new byte[header.ContentLengthBytes];
			_stream.Read(buffer, 0, header.ContentLengthBytes);

			ShapeType shapeType = (ShapeType)BitConverter.ToInt32(buffer, 0);
			if (shapeType != shapefile.Header.ShapeType)
				throw new FormatException("File is not a valid shapefile");

			if (!_recordReaders.ContainsKey(shapeType))
				throw new NotSupportedException($"Record type {shapeType} is not supported");

			// Use the specific function to read in the required record type
			IRecord record = _recordReaders[shapeType](buffer);
			if (record != null) // Record should only ever be null if the shape is type 0 (Null Shape)
				shapefile.Records.Add(record);
		}

		private PointRecord ReadPointRecord(byte[] bytes)
		{
			return new PointRecord(CreatePoint(bytes, 4));
		}

		private MultiPointRecord ReadMultiPointRecord(byte[] bytes)
		{
			int numPoints = BitConverter.ToInt32(bytes, 36);

			List<ShapePoint> points = new List<ShapePoint>(numPoints);
			for (int i = 0; i < numPoints; i++)
			{
				points.Add(CreatePoint(bytes, 40 + i * 16));
			}

			return new MultiPointRecord(CreateXYBoundingBox(bytes, 4), points);
		}

		private PolyLineRecord ReadPolyLineRecord(byte[] bytes)
		{
			int numParts = BitConverter.ToInt32(bytes, 36);
			int numPoints = BitConverter.ToInt32(bytes, 40);

			List<int> parts = new List<int>(numParts);
			List<ShapePoint> points = new List<ShapePoint>(numPoints);

			for (int i = 0; i < numParts; i++)
			{
				parts.Add(BitConverter.ToInt32(bytes, 44 + i * 4));
			}

			for (int i = 0; i < numPoints; i++)
			{
				points.Add(CreatePoint(bytes, i * 16));
			}

			return new PolyLineRecord(CreateXYBoundingBox(bytes, 4), parts, points);
		}

		private PolygonRecord ReadPolygonRecord(byte[] bytes)
		{
			int numParts = BitConverter.ToInt32(bytes, 36);
			int numPoints = BitConverter.ToInt32(bytes, 40);

			List<int> parts = new List<int>(numParts);
			List<ShapePoint> points = new List<ShapePoint>(numPoints);

			for (int i = 0; i < numParts; i++)
			{
				parts.Add(BitConverter.ToInt32(bytes, 44 + i * 4));
			}

			for (int i = 0; i < numPoints; i++)
			{
				points.Add(CreatePoint(bytes, i * 16));
			}

			return new PolygonRecord(CreateXYBoundingBox(bytes, 4), parts, points);
		}

		/// <summary>
		/// Reads in a 16 byte (2 double) point from source bytes
		/// </summary>
		/// <returns></returns>
		/// <param name="bytes"></param>
		/// <param name="start">Start offset</param>
		private static ShapePoint CreatePoint(byte[] bytes, int start)
		{
			return new ShapePoint(
				BitConverter.ToDouble(bytes, start),
				BitConverter.ToDouble(bytes, start + 8));
		}

		private static XYBoundingBox CreateXYBoundingBox(byte[] bytes, int start)
		{
			return new XYBoundingBox(
				BitConverter.ToDouble(bytes, start),
				BitConverter.ToDouble(bytes, start + 8),
				BitConverter.ToDouble(bytes, start + 16),
				BitConverter.ToDouble(bytes, start + 24));
		}

		private static XYZMBoundingBox CreateXYZMBoundingBox(byte[] bytes, int start)
		{
			return new XYZMBoundingBox(
				BitConverter.ToDouble(bytes, start),
				BitConverter.ToDouble(bytes, start + 8),
				BitConverter.ToDouble(bytes, start + 16),
				BitConverter.ToDouble(bytes, start + 24),
				BitConverter.ToDouble(bytes, start + 32),
				BitConverter.ToDouble(bytes, start + 40),
				BitConverter.ToDouble(bytes, start + 48),
				BitConverter.ToDouble(bytes, start + 56));
		}

		/// <summary>
		/// Record type reader method signature
		/// </summary>
		private delegate IRecord ReadRecordDelegate(byte[] bytes);
	}
}
