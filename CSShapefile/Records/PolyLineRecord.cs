﻿using System;
using System.Collections.Generic;

namespace CSShapefile
{
	public class PolyLineRecord : IRecord
	{
		public PolyLineRecord(XYBoundingBox boundingBox, IList<int> parts, IList<ShapePoint> points)
		{
			BoundingBox = boundingBox;
			Parts = parts;
			Points = points;
		}

		public XYBoundingBox BoundingBox { get; }

		public IList<int> Parts { get; }

		public IList<ShapePoint> Points { get; }

		public override string ToString()
		{
			return string.Format("[PolyLineRecord: BoundingBox={0}, Parts={1}, Points={2}]", BoundingBox, Parts, Points);
		}
	}
}
