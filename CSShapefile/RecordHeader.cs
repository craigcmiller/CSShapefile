using System;
using System.Collections.Generic;
using System.Text;

namespace CSShapefile
{
	/// <summary>
	/// Shapefile record header information
	/// </summary>
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

		public override string ToString()
		{
			return string.Format("[RecordHeader: ContentLengthBytes={0}]", ContentLengthBytes);
		}
	}
	
}
