using System;
using System.Collections.Generic;

namespace CSShapefile
{
	public class Shapefile
	{
		public Shapefile(ShapefileHeader header)
		{
			Header = header;
			Records = new List<ShapeRecord>();
		}

		public ShapefileHeader Header { get; }

		public IList<ShapeRecord> Records { get; }
	}

	public struct ShapefileHeader
	{
		/// <summary>
		/// 9994
		/// </summary>
		public int FileCode { get; set; }

		/// <summary>
		/// Gets or sets the file length in 16 bit words
		/// </summary>
		public int FileLengthWords { get; set; }

		public int FileLengthBytes
		{
			get { return FileLengthWords * 2; }
		}

		/// <summary>
		/// 1000
		/// </summary>
		public int Version { get; set; }

		public ShapeType ShapeType { get; set; }

		public BoundingBox BoundingBox { get; set; }
	}

	public struct RecordHeader
	{
		public readonly int RecordNumber;
		public readonly int ContentLengthWords;

		public RecordHeader(int recordNumber, int contentLengthWords)
		{
			RecordNumber = recordNumber;
			ContentLengthWords = contentLengthWords;
		}

		public int ContentLengthBytes { get { return ContentLengthWords * 2; } }
	}

	public struct BoundingBox
	{
		public readonly double XMin, YMin, XMax, YMax, ZMin, ZMax, MMin, MMax;

		public BoundingBox(double xMin, double yMin, double xMax, double yMax, double zMin, double zMax, double mMin, double mMax)
		{
			XMin = xMin;
			XMax = xMax;
			YMin = yMin;
			YMax = yMax;
			ZMin = zMin;
			ZMax = zMax;
			MMin = mMin;
			MMax = mMax;
		}
	}
}
