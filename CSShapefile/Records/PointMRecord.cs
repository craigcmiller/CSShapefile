using System;

namespace CSShapefile
{
	public class PointMRecord : PointRecord
	{
		public PointMRecord(ShapePoint point, double measure) : base(point)
		{
			Measure = measure;
		}

		public double Measure { get; }
	}
}
