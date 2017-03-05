using System;
namespace CSShapefile
{
	public class Shapefile
	{
		public Shapefile()
		{
			
		}

		public ShapefileHeader Header { get; set; }
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
		public int FileLength { get; set; }

		/// <summary>
		/// 1000
		/// </summary>
		public int Version { get; set; }

		public BoundingBox BoundingBox { get; set; }
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
