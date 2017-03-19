using System;
using System.Collections.Generic;
using System.Text;

namespace CSShapefile
{
	/// <summary>
	/// Shapefile file header content
	/// </summary>
	public struct ShapefileHeader
	{
		/// <summary>
		/// 9994
		/// </summary>
		public int FileCode { get; set; }

		/// <summary>
		/// Gets or sets the file length in 16 bit words
		/// </summary>
		public int FileLengthWords { get; set; }

		public int FileLengthBytes
		{
			get { return FileLengthWords * 2; }
		}

		/// <summary>
		/// 1000
		/// </summary>
		public int Version { get; set; }

		public ShapeType ShapeType { get; set; }

		public XYZMBoundingBox BoundingBox { get; set; }

		public override string ToString()
		{
			return string.Format("[ShapefileHeader: FileCode={0}, FileLengthWords={1}, FileLengthBytes={2}, Version={3}, ShapeType={4}, BoundingBox={5}]", FileCode, FileLengthWords, FileLengthBytes, Version, ShapeType, BoundingBox);
		}
	}
	
}
