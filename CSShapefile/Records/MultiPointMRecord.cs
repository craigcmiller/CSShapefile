using System;
using System.Collections.Generic;

namespace CSShapefile
{
	public class MultiPointMRecord : MultiPointRecord
	{
		public MultiPointMRecord(XYBoundingBox boundingBox, IList<ShapePoint> points, double mMin, double mMax, IList<double> measures) : base(boundingBox, points)
		{
			MeasureMin = mMin;
			MeasureMax = mMax;
			Measures = measures;
		}

		public double MeasureMin { get; }

		public double MeasureMax { get; }

		public IList<double> Measures { get; }
	}
}
