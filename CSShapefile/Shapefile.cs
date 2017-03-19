using System;
using System.Collections.Generic;
using System.Text;

namespace CSShapefile
{
	public class Shapefile
	{
		public Shapefile(ShapefileHeader header)
		{
			Header = header;
			Records = new List<IRecord>();
		}

		public ShapefileHeader Header { get; }

		public IList<IRecord> Records { get; }

		public override string ToString()
		{
			StringBuilder recordsStr = new StringBuilder();
			foreach (IRecord record in Records)
			{
				recordsStr.AppendLine(record.ToString());
			}

			return string.Format("[Shapefile: Header={0}, Records={1}]", Header, recordsStr);
		}
	}

	public struct XYBoundingBox
	{
		public readonly double XMin, YMin, XMax, YMax;

		public XYBoundingBox(double xMin, double yMin, double xMax, double yMax)
		{
			XMin = xMin;
			XMax = xMax;
			YMin = yMin;
			YMax = yMax;
		}
	}

	public struct XYZMBoundingBox
	{
		public readonly double XMin, YMin, XMax, YMax, ZMin, ZMax, MMin, MMax;

		public XYZMBoundingBox(double xMin, double yMin, double xMax, double yMax, double zMin, double zMax, double mMin, double mMax)
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
