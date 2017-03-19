using System;
using CSShapefile;

namespace Test
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			//Shapefile shapefile = ShapefileReader.ShapefileFromPath("/Users/craig/Downloads/libspatialite-4.4.0-RC1/test/shp/foggia/local_councils.shp");
			//Shapefile shapefile = ShapefileReader.ShapefileFromPath("/Users/craig/Downloads/libspatialite-4.4.0-RC1/test/shp/new-caledonia/buildings.shp");
			Shapefile shapefile = ShapefileReader.ShapefileFromPath("/Users/craig/Downloads/libspatialite-4.4.0-RC1/test/shp/taiwan/route.shp");


			Console.WriteLine("Test");
		}
	}
}
