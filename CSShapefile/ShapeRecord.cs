using System;

namespace CSShapefile
{
	public class ShapeRecord
	{
		private readonly RecordHeader _header;

		public ShapeRecord(RecordHeader header)
		{
			_header = header;
		}

		public RecordHeader Header
		{
			get { return _header; }
		}
	}
}
