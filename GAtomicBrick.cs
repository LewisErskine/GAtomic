using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GLDoors2.GAtomic.objects
{
	/// <summary>
	/// Summary description for GAtomicBrick.
	/// </summary>
	public class GAtomicBrick : GAtomicObject
	{

		#region CONSTRUCTOR
		public GAtomicBrick(Point t_l, int row, int col, int s)
			: base (t_l, row, col, s)
		{}
		#endregion

		#region _draw
		public override void _draw (Graphics g)
		{
			//g.DrawRectangle (new Pen (Color.Black), top_left.X, top_left.Y, size, size);

			HatchBrush brush = new HatchBrush (HatchStyle.Percent90, Color.Brown, Color.Black);
			
			g.FillRectangle (brush, top_left.X, top_left.Y, size, size);
		}
		#endregion

	}
}
