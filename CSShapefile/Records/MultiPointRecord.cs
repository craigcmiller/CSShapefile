using System;
using System.Collections.Generic;

namespace CSShapefile
{
	public class MultiPointRecord : IRecord
	{
		public MultiPointRecord(XYBoundingBox boundingBox, IList<ShapePoint> points)
		{
			BoundingBox = boundingBox;
			Points = points;
		}

		public XYBoundingBox BoundingBox { get; }

		public IList<ShapePoint> Points { get; }
	}
}
