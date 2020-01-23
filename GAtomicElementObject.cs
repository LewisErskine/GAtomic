using System;
using System.Drawing;

namespace GLDoors2.GAtomic.objects
{
	/// <summary>
	/// Summary description for GAtomicElementObject.
	/// </summary>
	public class GAtomicElementObject : GAtomicObject
	{
		#region VARIABLES
		/// start with top and clockwise
		
		// Connections
		private GAtomicConnectionElement[] connection_elements;
		public GAtomicConnectionElement[] ConnectionElements
		{
			get
			{	return	connection_elements;	}
		}
		public object _getConnectionElement (GAtomicConnectionDirection direction)
		{
			if (connection_elements == null)
				return null;

			if ((direction < GAtomicConnectionDirection.TOP) || (direction > GAtomicConnectionDirection.TRIPLE_LEFT))
				return null;

			for (int k = 0; k < connection_elements.Length; k++)
			{
				GAtomicConnectionElement c = (GAtomicConnectionElement)connection_elements[k];
				if (c.DirectionOfConnectingElement == direction)
					return c.AtomicElement;
			}
			
			return null;
		}

		public bool _setConnectionElement (GAtomicConnectionDirection direction, GAtomicElementObject element)
		{
			if ((direction < GAtomicConnectionDirection.TOP) || (direction > GAtomicConnectionDirection.TRIPLE_LEFT))
				return false;

			for (int k = 0; k < connection_elements.Length; k++)
			{
				GAtomicConnectionElement c = (GAtomicConnectionElement)connection_elements[k];
				if (c.DirectionOfConnectingElement == direction)
					c.AtomicElement = element;
			}
			
			return true;
		}

		public bool _hasConnection (GAtomicConnectionDirection direction)
		{
			if (connection_elements == null)
				return false;

			if ((direction < GAtomicConnectionDirection.TOP) || (direction > GAtomicConnectionDirection.TRIPLE_LEFT))
				return false;

			for (int k = 0; k < connection_elements.Length; k++)
			{
				GAtomicConnectionElement c = (GAtomicConnectionElement)connection_elements[k];
				if (c.DirectionOfConnectingElement == direction)
					return true;
			}
			
			return false;
		}

		// Element (hydrogen, oxygen, etc...
		private GAtomicElement element;
		public GAtomicElement _getAtomicElement ()
		{
			return element;
		}

		private int index_in_list;
		public int IndexInList
		{
			get
			{	return index_in_list;	}
		}
		#endregion

		#region CONSTRUCTOR
		public GAtomicElementObject(Point t_l, int row, int col, int s, GAtomicElement elem, GAtomicConnectionElement[] conn_elems, int ind)
			: base (t_l, row, col, s)
		{

			element = elem;
			
			index_in_list = ind;

			if (conn_elems == null)
				connection_elements = null;
			else
			{
				connection_elements = new GAtomicConnectionElement[conn_elems.Length];
				int k = 0;
				for (; k < conn_elems.Length; k++)
				{
					connection_elements[k] = conn_elems[k];
				}

				bool valid = true;
				for (k = 0; k < conn_elems.Length; k++)
				{
					for (int y = 0; y < conn_elems.Length; y++)
					{
						if (y != k)
						{
							if (((GAtomicConnectionElement)connection_elements[y]).DirectionOfConnectingElement ==
								((GAtomicConnectionElement)connection_elements[k]).DirectionOfConnectingElement)
							{
								// same direction twice, conceptual error
								valid = false;
								break;
							}
						}
					}
				}

				if (!valid)
					throw new GAtomicElementObjectInvalidException ();
			}
			
		}

		/// 
		/// element constructor without the dimensions and index (default values)
		/// 
		public GAtomicElementObject(GAtomicElement elem, GAtomicConnectionElement[] conn_elems)
			: base (new Point (0, 0), 0, 0, 0)
		{

			element = elem;
			
			index_in_list = 0;

			if (conn_elems == null)
				connection_elements = null;
			else
			{
				connection_elements = new GAtomicConnectionElement[conn_elems.Length];
				int k = 0;
				for (; k < conn_elems.Length; k++)
				{
					connection_elements[k] = conn_elems[k];
				}

				bool valid = true;
				for (k = 0; k < conn_elems.Length; k++)
				{
					for (int y = 0; y < conn_elems.Length; y++)
					{
						if (y != k)
						{
							if (((GAtomicConnectionElement)connection_elements[y]).DirectionOfConnectingElement ==
								((GAtomicConnectionElement)connection_elements[k]).DirectionOfConnectingElement)
							{
								// same direction twice, conceptual error
								valid = false;
								break;
							}
						}
					}
				}

				if (!valid)
					throw new GAtomicElementObjectInvalidException ();
			}
			
		}
		#endregion

		#region _draw
		public override void _draw (Graphics g)
		{
			base._draw (g);

			_drawText (g);		

			// Draw connections
			_drawConnections (g);

			
		}

		public override void _drawWithGrid (Graphics g)
		{
			base._drawWithGrid (g);

			_drawText (g);

			// Draw connections
			if ((element != GAtomicElement.VERTICAL_LINE_LINK) && (element != GAtomicElement.HORIZONTAL_LINE_LINK))
				_drawConnections (g);
		}

		public override void _drawMiniature (Graphics g)
		{
			base._draw (g);

			_drawMiniatureText (g);		

			// Draw connections
			if ((element != GAtomicElement.VERTICAL_LINE_LINK) && (element != GAtomicElement.HORIZONTAL_LINE_LINK))
				_drawConnections (g);

		}
		#endregion

		#region _drawText
		public void _drawText (Graphics g)
		{
			Point center_point = new Point (top_left.X + size / 2, top_left.Y + size / 2);
			int circle_radius = (size - (4 * size / 15)) / 2;

			if (element == GAtomicElement.VERTICAL_LINE_LINK)
			{
				g.DrawLine (new Pen (Color.White, 3), center_point.X, center_point.Y + size / 2, center_point.X, center_point.Y - size / 2);
				return;
			}
			else if (element == GAtomicElement.HORIZONTAL_LINE_LINK)
			{
				g.DrawLine (new Pen (Color.White, 3), center_point.X - size / 2, center_point.Y, center_point.X + size / 2, center_point.Y);
				return;
			}

			g.FillEllipse (new SolidBrush (_getBackgroundColor ()), center_point.X - circle_radius, center_point.Y - circle_radius, 2 * circle_radius, 2 * circle_radius);

			int x_offset = 2;
			if (element == GAtomicElement.CARBON)
				x_offset = 1;
			else if (element == GAtomicElement.OXYGEN)
				x_offset = 1;
						
			g.DrawString (_getElementLetter (), new Font ("Arial", 14), new SolidBrush (Color.White), new RectangleF (center_point.X - circle_radius + x_offset, center_point.Y - circle_radius, 2 * circle_radius - 2 * x_offset, 2 * circle_radius));
		}

		public void _drawMiniatureText (Graphics g)
		{
			Point center_point = new Point (top_left.X + size / 2, top_left.Y + size / 2);
			int circle_radius = (size - (4 * size / 15)) / 2;

			if (element == GAtomicElement.VERTICAL_LINE_LINK)
			{
				g.DrawLine (new Pen (Color.White, 3), center_point.X, center_point.Y + size / 2, center_point.X, center_point.Y - size / 2);
				return;
			}
			else if (element == GAtomicElement.HORIZONTAL_LINE_LINK)
			{
				g.DrawLine (new Pen (Color.White, 3), center_point.X - size / 2, center_point.Y, center_point.X + size / 2, center_point.Y);
				return;
			}

			g.FillEllipse (new SolidBrush (_getBackgroundColor ()), center_point.X - circle_radius, center_point.Y - circle_radius, 2 * circle_radius, 2 * circle_radius);

			float x_offset = 2f;
			if (element == GAtomicElement.CARBON)
				x_offset = 2;
			else if (element == GAtomicElement.OXYGEN)
				x_offset = 2;
			
			g.DrawString (_getElementLetter (), new Font ("Arial", 8), new SolidBrush (Color.White), new RectangleF (center_point.X - circle_radius + x_offset, center_point.Y - circle_radius, 2 * circle_radius - 2 * x_offset, 2 * circle_radius));
		}
		#endregion

		#region _drawConnections
		private void _drawConnections (Graphics g)
		{
			Point center_point = new Point (top_left.X + size / 2, top_left.Y + size / 2);
			int circle_radius = (size - (4 * size / 15)) / 2;

			// Draw connections
			if (_hasConnection (GAtomicConnectionDirection.TOP))
			{
				// top
				g.DrawLine (new Pen (Color.White, 3), center_point.X, center_point.Y - circle_radius + 0.5f, center_point.X, center_point.Y - size / 2);
			}

			if (_hasConnection (GAtomicConnectionDirection.TOP_RIGHT))
			{
				// top right
				// angle is 45°
				// x1 = center.X + radius * cos @
				// y1 = center.Y - radius * sin @
				// x2 = center.X + size / 2
				// y2 = center.Y - size / 2
				g.DrawLine (new Pen (Color.White, 2), center_point.X + circle_radius * 0.71f, center_point.Y - circle_radius * 0.71f, center_point.X + size / 2, center_point.Y - size / 2);
			}

			if (_hasConnection (GAtomicConnectionDirection.RIGHT))
			{
				// right
				g.DrawLine (new Pen (Color.White, 3), center_point.X + circle_radius, center_point.Y, center_point.X + size / 2, center_point.Y);
			}

			if (_hasConnection (GAtomicConnectionDirection.BOTTOM_RIGHT))
			{
				// bottom right
				// angle is 45°
				// x1 = center.X + radius * cos @
				// y1 = center.Y + radius * sin @
				// x2 = center.X + size / 2
				// y2 = center.Y + size / 2
				g.DrawLine (new Pen (Color.White, 2), center_point.X + circle_radius * 0.71f - 0.5f, center_point.Y + circle_radius * 0.71f, top_left.X + size, top_left.Y + size);
			}

			if (_hasConnection (GAtomicConnectionDirection.BOTTOM))
			{
				// bottom
				g.DrawLine (new Pen (Color.White, 3), center_point.X, center_point.Y + circle_radius, center_point.X, center_point.Y + size / 2);
			}

			if (_hasConnection (GAtomicConnectionDirection.BOTTOM_LEFT))
			{
				// bottom left
				// angle is 45°
				// x1 = center.X - radius * cos @
				// y1 = center.Y + radius * sin @
				// x2 = center.X - size / 2
				// y2 = center.Y + size / 2
				g.DrawLine (new Pen (Color.White, 2), center_point.X - circle_radius * 0.71f, center_point.Y + circle_radius * 0.71f, center_point.X - size / 2, center_point.Y + size / 2);
			}

			if (_hasConnection (GAtomicConnectionDirection.LEFT))
			{
				// left
				g.DrawLine (new Pen (Color.White, 3), center_point.X - circle_radius, center_point.Y, center_point.X - size / 2, center_point.Y);
			}

			if (_hasConnection (GAtomicConnectionDirection.TOP_LEFT))
			{
				// top left
				// angle is 45°
				// x1 = center.X - radius * cos @
				// y1 = center.Y - radius * sin @
				// x2 = center.X - size / 2
				// y2 = center.Y - size / 2
				g.DrawLine (new Pen (Color.White, 2), center_point.X - circle_radius * 0.71f, center_point.Y - circle_radius * 0.71f, center_point.X - size / 2, center_point.Y - size / 2);
			}

			if (_hasConnection (GAtomicConnectionDirection.DOUBLE_TOP))
			{
				// double top
				// angle is 24°

				// left line
				// x1 = center.X - radius * sin @
				// y1 = center.Y - radius * cos @
				// x2 = x1
				// y2 = center.Y - size / 2
				g.DrawLine (new Pen (Color.White, 2), center_point.X - circle_radius * 0.26f, center_point.Y - circle_radius * 0.97f, center_point.X - circle_radius * 0.26f, center_point.Y - size / 2);

				// right line
				// x changes from - to +
				g.DrawLine (new Pen (Color.White, 2), center_point.X + circle_radius * 0.26f, center_point.Y - circle_radius * 0.971f, center_point.X + circle_radius * 0.26f, center_point.Y - size / 2);
			}

			if (_hasConnection (GAtomicConnectionDirection.DOUBLE_RIGHT))
			{
				// double right
				// angle is 24°

				// top line
				// x1 = center.X + radius * cos @
				// y1 = center.Y - radius * sin @
				// x2 = center.X + size / 2
				// y2 = y1
				g.DrawLine (new Pen (Color.White, 2), center_point.X + circle_radius * 0.97f, center_point.Y - circle_radius * 0.26f, center_point.X + size / 2, center_point.Y - circle_radius * 0.26f);

				// bottom line
				// y changes from - to +
				g.DrawLine (new Pen (Color.White, 2), center_point.X + circle_radius * 0.97f, center_point.Y + circle_radius * 0.26f, center_point.X + size / 2, center_point.Y + circle_radius * 0.26f);
			}

			if (_hasConnection (GAtomicConnectionDirection.DOUBLE_BOTTOM))
			{
				// double top
				// angle is 24°

				// right line
				// x1 = center.X + radius * sin @
				// y1 = center.Y + radius * cos @
				// x2 = x1
				// y2 = center.Y + size / 2
				g.DrawLine (new Pen (Color.White, 2), center_point.X + circle_radius * 0.26f, center_point.Y + circle_radius * 0.97f, center_point.X + circle_radius * 0.26f, center_point.Y + size / 2);

				// left line
				// x changes from + to -
				g.DrawLine (new Pen (Color.White, 2), center_point.X - circle_radius * 0.26f, center_point.Y + circle_radius * 0.971f, center_point.X - circle_radius * 0.26f, center_point.Y + size / 2);
			}

			if (_hasConnection (GAtomicConnectionDirection.DOUBLE_LEFT))
			{
				// double top
				// angle is 24°

				// bottom line
				// x1 = center.X - radius * sin @
				// y1 = center.Y + radius * cos @
				// x2 = center.X - size / 2
				// y2 = y1
				g.DrawLine (new Pen (Color.White, 2), center_point.X - circle_radius * 0.97f, center_point.Y + circle_radius * 0.26f, center_point.X - size / 2, center_point.Y + circle_radius * 0.26f);

				// top line
				// y changes from + to -
				g.DrawLine (new Pen (Color.White, 2), center_point.X - circle_radius * 0.97f, center_point.Y - circle_radius * 0.26f, center_point.X - size / 2, center_point.Y - circle_radius * 0.26f);
			}

			if (_hasConnection (GAtomicConnectionDirection.TRIPLE_TOP))
			{
				// double top + top
				// double top
				// angle is 30°

				// left line
				// x1 = center.X - radius * sin @
				// y1 = center.Y - radius * cos @
				// x2 = x1
				// y2 = center.Y - size / 2
				g.DrawLine (new Pen (Color.White, 2), center_point.X - circle_radius * 0.5f, center_point.Y - circle_radius * 0.87f, center_point.X - circle_radius * 0.5f, center_point.Y - size / 2);

				// top
				g.DrawLine (new Pen (Color.White, 3), center_point.X, center_point.Y - circle_radius + 0.5f, center_point.X, center_point.Y - size / 2);

				// right line
				// x changes from - to +
				g.DrawLine (new Pen (Color.White, 2), center_point.X + circle_radius * 0.5f, center_point.Y - circle_radius * 0.87f, center_point.X + circle_radius * 0.5f, center_point.Y - size / 2);
			}

			if (_hasConnection (GAtomicConnectionDirection.TRIPLE_RIGHT))
			{
				// double right + right
				// double right
				// angle is 30°

				// top line
				// x1 = center.X + radius * cos @
				// y1 = center.Y - radius * sin @
				// x2 = center.X + size / 2
				// y2 = y1
				g.DrawLine (new Pen (Color.White, 2), center_point.X + circle_radius * 0.87f, center_point.Y - circle_radius * 0.5f, center_point.X + size / 2, center_point.Y - circle_radius * 0.5f);

				// right
				g.DrawLine (new Pen (Color.White, 3), center_point.X + circle_radius, center_point.Y, center_point.X + size / 2, center_point.Y);

				// bottom line
				// y changes from - to +
				g.DrawLine (new Pen (Color.White, 2), center_point.X + circle_radius * 0.87f, center_point.Y + circle_radius * 0.5f, center_point.X + size / 2, center_point.Y + circle_radius * 0.5f);
			}

			if (_hasConnection (GAtomicConnectionDirection.TRIPLE_BOTTOM))
			{
				// double bottom
				// angle is 24°

				// right line
				// x1 = center.X + radius * sin @
				// y1 = center.Y + radius * cos @
				// x2 = x1
				// y2 = center.Y + size / 2
				g.DrawLine (new Pen (Color.White, 2), center_point.X + circle_radius * 0.5f, center_point.Y + circle_radius * 0.87f, center_point.X + circle_radius * 0.5f, center_point.Y + size / 2);

				// bottom
				g.DrawLine (new Pen (Color.White, 3), center_point.X, center_point.Y + circle_radius, center_point.X, center_point.Y + size / 2);

				// left line
				// x changes from + to -
				g.DrawLine (new Pen (Color.White, 2), center_point.X - circle_radius * 0.5f, center_point.Y + circle_radius * 0.87f, center_point.X - circle_radius * 0.5f, center_point.Y + size / 2);
			}

			if (_hasConnection (GAtomicConnectionDirection.TRIPLE_LEFT))
			{
				// double left
				// angle is 24°

				// bottom line
				// x1 = center.X - radius * sin @
				// y1 = center.Y + radius * cos @
				// x2 = center.X - size / 2
				// y2 = y1
				g.DrawLine (new Pen (Color.White, 2), center_point.X - circle_radius * 0.87f, center_point.Y + circle_radius * 0.5f, center_point.X - size / 2, center_point.Y + circle_radius * 0.5f);

				// left
				g.DrawLine (new Pen (Color.White, 3), center_point.X - circle_radius, center_point.Y, center_point.X - size / 2, center_point.Y);

				// top line
				// y changes from + to -
				g.DrawLine (new Pen (Color.White, 2), center_point.X - circle_radius * 0.87f, center_point.Y - circle_radius * 0.5f, center_point.X - size / 2, center_point.Y - circle_radius * 0.5f);
			}
		}

		#endregion

		#region _getBackgroundColor
		public Color _getBackgroundColor ()
		{
			if (element == GAtomicElement.CARBON)
				return Color.Blue;
			else if (element == GAtomicElement.HYDROGEN)
				return Color.Green;
			else if (element == GAtomicElement.NEUTRAL)
				return Color.DarkBlue;
			else if (element == GAtomicElement.NITROGEN)
				return Color.Orange;
			else if (element == GAtomicElement.OXYGEN)
				return Color.Red;
			else if (element == GAtomicElement.SULFUR)
				return Color.Navy;
			else if (element == GAtomicElement.PHOSPHORUS)
				return Color.Magenta;
			else if (element == GAtomicElement.FLUORIDE)
				return Color.DarkBlue;
			else if (element == GAtomicElement.BORANE)
				return Color.Orange;
			else
				return Color.Blue;

		}
		#endregion

		#region _getElementLetter
		public String _getElementLetter ()
		{
			if (element == GAtomicElement.CARBON)
				return "C";
			else if (element == GAtomicElement.HYDROGEN)
				return "H";
			else if (element == GAtomicElement.NEUTRAL)
				return "";
			else if (element == GAtomicElement.NITROGEN)
				return "N";
			else if (element == GAtomicElement.OXYGEN)
				return "O";
			else if (element == GAtomicElement.SULFUR)
				return "S";
			else if (element == GAtomicElement.PHOSPHORUS)
				return "P";
			else if (element == GAtomicElement.FLUORIDE)
				return "F";
			else if (element == GAtomicElement.BORANE)
				return "B";
			else
				return "";

		}
		#endregion

		#region Equality operator
		// Equality operator.
		public static bool operator ==(GAtomicElementObject x, GAtomicElementObject y) 
		{
			bool equal = true;
			
			// same element?
			if (x.element != y.element)
				equal = false;
			else
			{
				// no directions
				if ((x.connection_elements == null) && (y.connection_elements == null))
					equal = true;
				// element has same directions, connecting elements irrelevant
				else if (x.connection_elements.Length != y.connection_elements.Length)
					equal = false;
				else
				{
					for (int k = 0; k < x.connection_elements.Length; k++)
					{
						if (y._hasConnection (((GAtomicConnectionElement)x.connection_elements[k]).DirectionOfConnectingElement))
						{
							continue;
						}
						else
						{
							equal = false;
							break;
						}
					}
				}
			}

			// all equal, return true
			return equal;
		}

		public static bool operator !=(GAtomicElementObject x, GAtomicElementObject y) 
		{

			return true;
		}

		// Override the Object.Equals(object o) method:
		public override bool Equals(object o) 
		{
			try 
			{
				if (!(o is GAtomicElementObject))
					return false;

				return (this == (GAtomicElementObject)o);
			}
			catch 
			{
				return false;
			}
		}

		// Override the Object.GetHashCode() method:
		public override int GetHashCode() 
		{
			return base.GetHashCode ();
		}
		#endregion

		#region ToString
		public override String ToString ()
		{
			String s = GAtomicObject._getGAtomicElementsString (element);

			for (GAtomicConnectionDirection direction = GAtomicConnectionDirection.TOP; direction <= GAtomicConnectionDirection.TRIPLE_LEFT; direction++)
			{
				if (_hasConnection (direction))
				{
					s += "1";
				}
				else
					s += "0";
			}

			return s;
		}
		#endregion

		#region _getAtomicElementFromString
		public static GAtomicElementObject _getAtomicElementFromString (String s, Point p, int row, int col, int object_size, int ind_list)
		{
			String atomic_element_string = s.Substring (0, 3);
			GAtomicElement atomic_element = GAtomicObject._getGAtomicElementFromString (atomic_element_string);

			int number_of_connections = 0;
			String connections_string = s.Substring (3);
			if (connections_string.Length != (int)GAtomicConnectionDirection.TRIPLE_LEFT + 1)
				return null;

			int index = connections_string.IndexOf ("1");
			while (index != -1)
			{
				number_of_connections++;
				index = connections_string.IndexOf ("1", index + 1);
			}

			GAtomicConnectionElement[] connections = new GAtomicConnectionElement[number_of_connections];
			index = 0;
			int array_index = 0;
			while (index < connections_string.Length)
			{
				if (connections_string.Substring (index++, 1) == "1")
					connections[array_index++] = new GAtomicConnectionElement (null, (GAtomicConnectionDirection)(index - 1));
			}
			

			GAtomicElementObject a = new GAtomicElementObject (p, row, col
				, object_size
				, atomic_element, connections, ind_list);

			return a;
		}
		#endregion
	}
}
