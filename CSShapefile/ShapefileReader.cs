using System;
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

		public ShapefileReader(Stream stream)
		{
			_stream = stream;
		}

		public Shapefile Read()
		{
			byte[] headerBytes = new byte[100];
			_stream.Read(headerBytes, 0, 100);

			ShapefileHeader header = new ShapefileHeader
			{
				FileCode = Endian.Swap(BitConverter.ToInt32(headerBytes, 0)),
				FileLengthWords = Endian.Swap(BitConverter.ToInt32(headerBytes, 24)),
				Version = BitConverter.ToInt32(headerBytes, 28),
				ShapeType = (ShapeType)BitConverter.ToInt32(headerBytes, 32),
				BoundingBox = CreateBoundingBox(headerBytes, 36)
			};

			Shapefile shapefile = new Shapefile
			{
				Header = header
			};

			return shapefile;
		}

		private static BoundingBox CreateBoundingBox(byte[] bytes, int start)
		{
			return new BoundingBox(
				BitConverter.ToDouble(bytes, start),
				BitConverter.ToDouble(bytes, start + 8),
				BitConverter.ToDouble(bytes, start + 16),
				BitConverter.ToDouble(bytes, start + 24),
				BitConverter.ToDouble(bytes, start + 32),
				BitConverter.ToDouble(bytes, start + 40),
				BitConverter.ToDouble(bytes, start + 48),
				BitConverter.ToDouble(bytes, start + 56));
		}
	}
}
