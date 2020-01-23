using System;
using System.Drawing;

namespace GLDoors2.GAtomic.objects
{
	/// <summary>
	/// Summary description for GAtomicConfiguration.
	/// </summary>
	public class GAtomicConfiguration
	{
		#region CONSTRUCTOR
		public GAtomicConfiguration()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region VARIABLES
		private static Point playing_area_top_left;
		public static Point _getPlayingAreaTopLeft ()
		{
			return playing_area_top_left;
		}
		public static void _setPlayingAreaTopLeft (Point p)
		{
			playing_area_top_left = p;
		}

		private static int playing_area_atomic_object_size;
		public static int _getPlayingAreaAtomicObjectSize ()
		{
			return playing_area_atomic_object_size;
		}
		public static void _setPlayingAreaAtomicObjectSize (int i)
		{
			playing_area_atomic_object_size = i;
		}

		private static int miniature_area_atomic_object_size;
		public static int _getMiniatureAreaAtomicObjectSize ()
		{
			return miniature_area_atomic_object_size;
		}
		public static void _setMiniatureAreaAtomicObjectSize (int i)
		{
			miniature_area_atomic_object_size = i;
		}
		#endregion
	}
}
