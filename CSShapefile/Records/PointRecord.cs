using System;
namespace CSShapefile
{
	public class PointRecord : IRecord
	{
		public PointRecord(ShapePoint point)
		{
			Point = point;
		}

		public ShapePoint Point { get; }
	}
}
