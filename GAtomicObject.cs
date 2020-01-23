using System;
using System.Drawing;

namespace GLDoors2.GAtomic.objects
{
	#region enums
	/// 
	/// top
	/// top right
	/// right
	/// bottom right
	/// bottom
	/// bottom left
	/// left
	/// top left
	/// 
	public enum GAtomicConnectionDirection
	{
		TOP = 0,
		TOP_RIGHT,
		RIGHT,
		BOTTOM_RIGHT,
		BOTTOM,
		BOTTOM_LEFT,
		LEFT,
		TOP_LEFT,
		DOUBLE_TOP,
		DOUBLE_RIGHT,
		DOUBLE_BOTTOM,
		DOUBLE_LEFT,
		TRIPLE_TOP,
		TRIPLE_RIGHT,
		TRIPLE_BOTTOM,
		TRIPLE_LEFT
	};

	public enum GAtomicElement
	{
		UKNOWN_ELEMENT = -1,
		NEUTRAL = 0,
		CARBON,
		HYDROGEN,
		SULFUR,
		NITROGEN,
		OXYGEN,
		PHOSPHORUS,
		FLUORIDE,
		BORANE,
		VERTICAL_LINE_LINK,
		HORIZONTAL_LINE_LINK
	};

	public enum GAtomicMovementDirection
	{
		UP = 0,
		RIGHT,
		DOWN,
		LEFT
	};
	#endregion

	/// <summary>
	/// Summary description for GAtomicObject.
	/// </summary>
	public class GAtomicObject
	{
		#region VARIABLES
		protected Point top_left;
		public Point TopLeft
		{
			set
			{	top_left = value;	}
		}
		protected int size;
		#endregion

		#region CONSTRUCTOR
		public GAtomicObject(Point t_l, int row, int col, int s)
		{
			size = s;
			top_left = new Point (t_l.X + col * size, t_l.Y + row * size);
		}
		#endregion

        #region _draw
		public virtual void _draw (Graphics g)
		{
			g.FillRectangle (new SolidBrush (Color.DarkGray), top_left.X, top_left.Y, size, size);
		}

		public virtual void _drawWithGrid (Graphics g)
		{
			_draw (g);
			g.DrawRectangle (new Pen (Color.Black, 1), top_left.X, top_left.Y, size, size);
		}

		public virtual void _drawMiniature (Graphics g)
		{
			_draw (g);
		}
		#endregion

		#region _moveAndResize
		public void _moveAndResize (Point t_l, int row, int col, int s)
		{
			size = s;
			top_left = new Point (t_l.X + col * size, t_l.Y + row * size);
		}
		#endregion

		#region _getGAtomicElementsString
		public static String _getGAtomicElementsString (GAtomicElement element)
		{
			String s = "";
			if (element == GAtomicElement.HYDROGEN)
			{
				s = "HYD";
			}
			else if (element == GAtomicElement.OXYGEN)
			{
				s = "OXY";
			}
			else if (element == GAtomicElement.NITROGEN)
			{
				s = "NIT";
			}
			else if (element == GAtomicElement.SULFUR)
			{
				s = "SUL";
			}
			else if (element == GAtomicElement.CARBON)
			{
				s = "CAR";
			}
			else if (element == GAtomicElement.NEUTRAL)
			{
				s = "NEU";
			}
			else if (element == GAtomicElement.PHOSPHORUS)
			{
				s = "PHS";
			}
			else if (element == GAtomicElement.FLUORIDE)
			{
				s = "FLU";
			}
			else if (element == GAtomicElement.BORANE)
			{
				s = "BOR";
			}
			else if (element == GAtomicElement.VERTICAL_LINE_LINK)
			{
				s = "VLK";
			}
			else if (element == GAtomicElement.HORIZONTAL_LINE_LINK)
			{
				s = "HLK";
			}
			else
				s = "UKN";

			return s;
		}
		#endregion

		#region _getGAtomicElementFromString
		public static GAtomicElement _getGAtomicElementFromString (String s)
		{
			GAtomicElement atomic_elements = GAtomicElement.NEUTRAL;
			if (s == "HYD")
			{
				atomic_elements = GAtomicElement.HYDROGEN;
			}
			else if (s == "OXY")
			{
				atomic_elements = GAtomicElement.OXYGEN;
			}
			else if (s == "NIT")
			{
				atomic_elements = GAtomicElement.NITROGEN;
			}
			else if (s == "SUL")
			{
				atomic_elements = GAtomicElement.SULFUR;
			}
			else if (s == "CAR")
			{
				atomic_elements = GAtomicElement.CARBON;
			}
			else if (s == "NEU")
			{
				atomic_elements = GAtomicElement.NEUTRAL;
			}
			else if (s == "PHS")
			{
				atomic_elements = GAtomicElement.PHOSPHORUS;
			}
			else if (s == "FLU")
			{
				atomic_elements = GAtomicElement.FLUORIDE;
			}
			else if (s == "BOR")
			{
				atomic_elements = GAtomicElement.BORANE;
			}
			else if (s == "VLK")
			{
				atomic_elements = GAtomicElement.VERTICAL_LINE_LINK;
			}
			else if (s == "HLK")
			{
				atomic_elements = GAtomicElement.HORIZONTAL_LINE_LINK;
			}
			else
				atomic_elements = GAtomicElement.UKNOWN_ELEMENT;
			
			return atomic_elements;
		}
		#endregion

	}
}
