using System;
using System.IO;

namespace CSShapefile
{
	/// <summary>
	/// Shapefile reading support
	/// http://www.esri.com/library/whitepapers/pdfs/shapefile.pdf
	/// </summary>
	public class ShapefileReader
	{
		private readonly Stream _stream;

		public ShapefileReader(Stream stream)
		{
			_stream = stream;
		}
	}
}
