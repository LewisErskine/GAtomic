using System;
using System.Drawing;

namespace GLDoors2.GAtomic.objects
{
	/// <summary>
	/// Summary description for GAtomicDirectionArrow.
	/// </summary>
	public class GAtomicDirectionArrow : GAtomicObject
	{
		#region VARIABLES
		private GAtomicMovementDirection movement_direction;
		public GAtomicMovementDirection _getMovementDirection ()
		{
			return movement_direction;;
		}
		#endregion

		#region CONSTRUCTOR
		public GAtomicDirectionArrow(GAtomicMovementDirection mv_dir, Point t_l, int row, int col, int s)
			: base (t_l, row, col, s)
		{
			movement_direction = mv_dir;
		}
		#endregion

		#region _draw
		public override void _draw (Graphics g)
		{
			base._draw (g);

			Color c = Color.Brown;
			
			//g.DrawRectangle (new Pen (Color.Black), top_left.X, top_left.Y, size, size);

			if (movement_direction == GAtomicMovementDirection.RIGHT)
			{
				g.FillRectangle (new SolidBrush (c), top_left.X, top_left.Y + size / 2 - 4, size - 12, 8);

				// arrow
				Point arrow_left = new Point (top_left.X + size - 12, top_left.Y + size / 2);
				g.FillPolygon (new SolidBrush (c), new Point[4]{new Point (arrow_left.X, arrow_left.Y - 10)
																   ,new Point (arrow_left.X + 12, arrow_left.Y)
																   ,new Point (arrow_left.X, arrow_left.Y + 10)
																   ,new Point (arrow_left.X, arrow_left.Y - 10)});
			}
			else if (movement_direction == GAtomicMovementDirection.LEFT)
			{
				g.FillRectangle (new SolidBrush (c), top_left.X + 12, top_left.Y + size / 2 - 4, size - 12, 8);

				// arrow
				Point arrow_right = new Point (top_left.X + 12, top_left.Y + size / 2);
				g.FillPolygon (new SolidBrush (c), new Point[4]{new Point (arrow_right.X, arrow_right.Y - 10)
																   ,new Point (arrow_right.X - 12, arrow_right.Y)
																   ,new Point (arrow_right.X, arrow_right.Y + 10)
																   ,new Point (arrow_right.X, arrow_right.Y - 10)});
			}
			else if (movement_direction == GAtomicMovementDirection.UP)
			{
				g.FillRectangle (new SolidBrush (c), top_left.X + size / 2 - 4, top_left.Y + 12, 8, size - 12);

				// arrow
				Point arrow_bottom = new Point (top_left.X + size / 2, top_left.Y + 12);
				g.FillPolygon (new SolidBrush (c), new Point[4]{new Point (arrow_bottom.X - 10, arrow_bottom.Y)
																   ,new Point (arrow_bottom.X, arrow_bottom.Y - 12)
																   ,new Point (arrow_bottom.X + 10, arrow_bottom.Y)
																   ,new Point (arrow_bottom.X - 10, arrow_bottom.Y)});
			}
			else if (movement_direction == GAtomicMovementDirection.DOWN)
			{
				g.FillRectangle (new SolidBrush (c), top_left.X + size / 2 - 4, top_left.Y, 8, size - 12);

				// arrow
				Point arrow_top = new Point (top_left.X + size / 2, top_left.Y + size - 12);
				g.FillPolygon (new SolidBrush (c), new Point[4]{new Point (arrow_top.X - 10, arrow_top.Y)
																   ,new Point (arrow_top.X, arrow_top.Y + 12)
																   ,new Point (arrow_top.X + 10, arrow_top.Y)
																   ,new Point (arrow_top.X - 10, arrow_top.Y)});
			}

		}
		#endregion
	}
}
