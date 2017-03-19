using System;

namespace CSShapefile
{
	/// <summary>
	/// Shape point
	/// </summary>
	public struct ShapePoint
	{
		public ShapePoint(double x, double y)
		{
			X = x;
			Y = y;
		}

		public double X { get; }

		public double Y { get; }

		public override string ToString()
		{
			return string.Format("[ShapePoint: X={0}, Y={1}]", X, Y);
		}
	}
}
