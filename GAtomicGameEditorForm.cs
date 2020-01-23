using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using GLDoors2.GAtomic.objects;

namespace GLDoors2.GAtomic
{
	/// <summary>
	/// Summary description for GAtomicGameEditorForm.
	/// </summary>
	public class GAtomicGameEditorForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region VARIABLES
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnLoadGame;
		private System.Windows.Forms.Button btnSaveGame;
		private System.Windows.Forms.TextBox txtGameName;
		private System.Windows.Forms.Button btnNewGame;

		// Game
		private Rectangle game_area_rect;
		private int game_area_rows;
		private int game_area_cols;
		private Array game_elements;

		private Rectangle miniature_area_rect;
		private int miniature_area_rows;
		private int miniature_area_cols;
		private Array miniature_elements;
		private Array original_miniature_elements;
		private int miniature_number_of_elements;
		private bool miniature_moving;
		
		private Rectangle current_atomic_object_area_rect;
		private System.Windows.Forms.CheckBox checkBoxBrick;
		private GAtomicObject current_atomic_object;
		private System.Windows.Forms.Button btnMoveFormulaToGameBoard;

		private bool game_in_construction;
		#endregion

		#region CONSTRUCTOR
		public GAtomicGameEditorForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			// Double Buffering
			SetStyle (ControlStyles.UserPaint, true);
			SetStyle (ControlStyles.AllPaintingInWmPaint, true);
			SetStyle (ControlStyles.DoubleBuffer, true);

			GAtomicConfiguration._setPlayingAreaTopLeft (new Point (20, 40));
			GAtomicConfiguration._setPlayingAreaAtomicObjectSize (30);

			GAtomicConfiguration._setMiniatureAreaAtomicObjectSize (20);

			_newGame ();
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnClose = new System.Windows.Forms.Button();
			this.btnLoadGame = new System.Windows.Forms.Button();
			this.btnSaveGame = new System.Windows.Forms.Button();
			this.txtGameName = new System.Windows.Forms.TextBox();
			this.btnNewGame = new System.Windows.Forms.Button();
			this.checkBoxBrick = new System.Windows.Forms.CheckBox();
			this.btnMoveFormulaToGameBoard = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(512, 80);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(64, 24);
			this.btnClose.TabIndex = 9;
			this.btnClose.Text = "Quit";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnLoadGame
			// 
			this.btnLoadGame.Location = new System.Drawing.Point(336, 8);
			this.btnLoadGame.Name = "btnLoadGame";
			this.btnLoadGame.Size = new System.Drawing.Size(56, 24);
			this.btnLoadGame.TabIndex = 8;
			this.btnLoadGame.Text = "Load";
			this.btnLoadGame.Click += new System.EventHandler(this.btnLoadGame_Click);
			// 
			// btnSaveGame
			// 
			this.btnSaveGame.Location = new System.Drawing.Point(256, 8);
			this.btnSaveGame.Name = "btnSaveGame";
			this.btnSaveGame.Size = new System.Drawing.Size(56, 24);
			this.btnSaveGame.TabIndex = 7;
			this.btnSaveGame.Text = "Save";
			this.btnSaveGame.Click += new System.EventHandler(this.btnSaveGame_Click);
			// 
			// txtGameName
			// 
			this.txtGameName.Location = new System.Drawing.Point(88, 8);
			this.txtGameName.Name = "txtGameName";
			this.txtGameName.Size = new System.Drawing.Size(160, 20);
			this.txtGameName.TabIndex = 6;
			this.txtGameName.Text = "Game Name";
			// 
			// btnNewGame
			// 
			this.btnNewGame.Location = new System.Drawing.Point(16, 8);
			this.btnNewGame.Name = "btnNewGame";
			this.btnNewGame.Size = new System.Drawing.Size(64, 24);
			this.btnNewGame.TabIndex = 10;
			this.btnNewGame.Text = "New";
			this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
			// 
			// checkBoxBrick
			// 
			this.checkBoxBrick.Location = new System.Drawing.Point(312, 48);
			this.checkBoxBrick.Name = "checkBoxBrick";
			this.checkBoxBrick.Size = new System.Drawing.Size(80, 16);
			this.checkBoxBrick.TabIndex = 11;
			this.checkBoxBrick.Text = "Brick";
			this.checkBoxBrick.CheckedChanged += new System.EventHandler(this.checkBoxBrick_CheckedChanged);
			// 
			// btnMoveFormulaToGameBoard
			// 
			this.btnMoveFormulaToGameBoard.Location = new System.Drawing.Point(408, 8);
			this.btnMoveFormulaToGameBoard.Name = "btnMoveFormulaToGameBoard";
			this.btnMoveFormulaToGameBoard.Size = new System.Drawing.Size(168, 24);
			this.btnMoveFormulaToGameBoard.TabIndex = 12;
			this.btnMoveFormulaToGameBoard.Text = "Move Formula to Game Board";
			this.btnMoveFormulaToGameBoard.Click += new System.EventHandler(this.btnMoveFormulaToGameBoard_Click);
			// 
			// GAtomicGameEditorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(584, 109);
			this.Controls.Add(this.btnMoveFormulaToGameBoard);
			this.Controls.Add(this.checkBoxBrick);
			this.Controls.Add(this.btnNewGame);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnLoadGame);
			this.Controls.Add(this.btnSaveGame);
			this.Controls.Add(this.txtGameName);
			this.MaximizeBox = false;
			this.Name = "GAtomicGameEditorForm";
			this.Text = "GAtomicGameEditor";
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GAtomicGameEditorForm_MouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_Paint);
			this.ResumeLayout(false);

		}
		#endregion

		#region Main
		///
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new GAtomicGameEditorForm());
		}
		#endregion

		#region Paint
		private void Form_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if ((game_elements == null) || (miniature_elements == null))
				return;

			GAtomicObject o = null;
			for (int x = 0; x < game_elements.GetLength (1); x++)
			{
				for (int y = 0; y < game_elements.GetLength (0); y++)
				{
					o = (GAtomicObject)game_elements.GetValue (y, x);
					if (o != null)
						o._drawWithGrid (e.Graphics);
				}
			}

			//e.Graphics.FillRectangle (new SolidBrush (Color.DarkGray), miniature_area_rect);

			o = null;
			for (int x = 0; x < miniature_elements.GetLength (1); x++)
			{
				for (int y = 0; y < miniature_elements.GetLength (0); y++)
				{
					o = (GAtomicObject)miniature_elements.GetValue (y, x);
					if (o != null)
						o._drawMiniature (e.Graphics);
				}
			}

			if (current_atomic_object != null)
			{
				current_atomic_object._drawWithGrid (e.Graphics);
				//else
				//	current_atomic_object._draw (e.Graphics);
			}
		}

		#endregion

		#region _newGame
		private bool _newGame ()
		{
			GAtomicNewGameForm new_game_form = new GAtomicNewGameForm ();
			if (new_game_form.ShowDialog () == DialogResult.Cancel)
				return false;

			game_in_construction = true;

			game_area_rows = new_game_form.game_area_rows;
			game_area_cols = new_game_form.game_area_cols;

			game_elements = Array.CreateInstance (typeof(GAtomicObject), game_area_rows, game_area_cols);

			miniature_moving = false;

			String formula_file_name = Directory.GetCurrentDirectory () + "/formulas/" + new_game_form.selected_formula + ".gaf";

			bool error = false;
			int row = 0;
			int col = 0;
			try
			{
				// Load formula
				GLDoorsFileOperations file_operations = new GLDoorsFileOperations (formula_file_name);
				file_operations.OpenFileForRead ();

				String s = "";
				bool b = false;
				// Name OK, read line anyway
				file_operations.ReadStringLineFromFile (out s, out b);
				// Read size
				file_operations.ReadStringLineFromFile (out s, out b);
				String sizes_string = s;
				int rows = Convert.ToInt32 (s.Substring (0, s.IndexOf ("x")));
				int cols = Convert.ToInt32 (s.Substring (s.IndexOf ("x") + 1));

				miniature_area_rows = rows + 2;
				miniature_area_cols = cols + 2;

				miniature_elements = Array.CreateInstance (typeof(GAtomicObject), miniature_area_rows, miniature_area_cols);
				original_miniature_elements = Array.CreateInstance (typeof(GAtomicObject), miniature_area_rows, miniature_area_cols);

				_setSizesAndPlacements ();

				// Draw borders
				row = 0;
				col = 0;
				for (col = 0; col < miniature_area_cols; col++)
				{
					// First row
					row = 0;
					miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col);
					original_miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col);
					
					// Last row
					row = miniature_area_rows - 1;
					miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col);
					original_miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col);

				}
				for (row = 0; row < miniature_area_rows; row++)
				{
					// First col
					col = 0;
					miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col);
					original_miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col);
					
					// Last col
					col = miniature_area_cols - 1;
					miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col);
					original_miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col);

				}
				miniature_number_of_elements = 0;
				// Formula
				for (row = 1; row <= rows; row++)
				{
					for (col = 1; col <= cols; col++)
					{
						file_operations.ReadStringLineFromFile (out s, out b);
						if (!b)
						{
							// erreur
							error = true;
							break;
						}
						if (s == "EMPTY")
						{
							miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col); 
							original_miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col);
						}
						else
						{
							// Read and add
							object o = GAtomicElementObject._getAtomicElementFromString (s, new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize (), row * miniature_area_cols + col);
							if (o != null)
							{
								miniature_elements.SetValue ((GAtomicElementObject)o, row, col);
								original_miniature_elements.SetValue ((GAtomicElementObject)o, row, col);

								miniature_number_of_elements++;
							}
						}
					}
					if (error)
						break;
				}
			
				file_operations.CloseFile ();
			}
			catch
			{
				error = true;
			}

			if (!error)
			{
				// Initialize game area with bricks on the borders
				for (col = 0; col < game_area_cols; col++)
				{
					row = 0;
					game_elements.SetValue (new GAtomicBrick (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col);

					row = game_area_rows - 1;
					game_elements.SetValue (new GAtomicBrick (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col);
				}

				for (row = 0; row < game_area_rows; row++)
				{
					col = 0;
					game_elements.SetValue (new GAtomicBrick (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col);

					col = game_area_cols - 1;
					game_elements.SetValue (new GAtomicBrick (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col);
				}

				// other fill simply
				for (row = 0; row < game_elements.GetLength (0); row++)
				{
					for (col = 0; col < game_elements.GetLength (1); col++)
					{
						if (game_elements.GetValue (row, col) == null)
						{
							game_elements.SetValue (new GAtomicObject (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col);
						}
					}
				}

				checkBoxBrick.Checked = true;

				this.txtGameName.Text = new_game_form.selected_formula;
			}

			this.Invalidate ();

			return true;
		}
		#endregion
		
		#region _setSizesAndPlacements
		private void _setSizesAndPlacements ()
		{
			game_area_rect = new Rectangle (0, 0, 2 * GAtomicConfiguration._getPlayingAreaTopLeft ().X + game_area_cols * GAtomicConfiguration._getPlayingAreaAtomicObjectSize (), 2 * GAtomicConfiguration._getPlayingAreaTopLeft ().Y + game_area_rows * GAtomicConfiguration._getPlayingAreaAtomicObjectSize ());

			int miniature_area_height = miniature_area_rows * GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ();
			
			miniature_area_rect = new Rectangle (game_area_rect.Width, GAtomicConfiguration._getPlayingAreaTopLeft ().Y, miniature_area_cols * GAtomicConfiguration._getMiniatureAreaAtomicObjectSize (), miniature_area_height);

			this.ClientSize = new Size (game_area_rect.Width + GAtomicConfiguration._getPlayingAreaTopLeft ().X + miniature_area_rect.Width, game_area_rect.Height);

			// Place buttons and current atomic object
			current_atomic_object_area_rect = new Rectangle (miniature_area_rect.Left + (miniature_area_rect.Width - GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()) / 2, miniature_area_rect.Bottom + 50, GAtomicConfiguration._getPlayingAreaAtomicObjectSize (), GAtomicConfiguration._getPlayingAreaAtomicObjectSize ());
			current_atomic_object = new GAtomicBrick (new Point (current_atomic_object_area_rect.Left, current_atomic_object_area_rect.Top), 0, 0, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ());

			checkBoxBrick.Location = new Point (miniature_area_rect.Left + 10, miniature_area_rect.Bottom + 20);

			btnClose.Location = new Point (this.ClientSize.Width - 10 - btnClose.Width, this.ClientSize.Height - 10 - btnClose.Height);

			btnNewGame.Location = new Point (20, 10);

			txtGameName.Location = new Point (btnNewGame.Right + 10, 12);
			
			btnSaveGame.Location = new Point (txtGameName.Right + 10, 10);

			btnLoadGame.Location = new Point (btnSaveGame.Right + 10, 10);

			if (game_area_cols > 10)
				btnMoveFormulaToGameBoard.Location = new Point (btnLoadGame.Right + 10, 10);
			else
				btnMoveFormulaToGameBoard.Location = new Point (miniature_area_rect.Left, current_atomic_object_area_rect.Bottom + 25);
		}
		#endregion

		#region _isMiniatureEmpty
		private bool _isMiniatureEmpty ()
		{
			for (int row = 0; row < miniature_area_rows; row++)
			{
				for (int col = 0; col < miniature_area_cols; col++)
				{
					if (miniature_elements.GetValue (row, col) is GAtomicElementObject)
						return false;
				}
			}

			return true;
		}
		#endregion

		#region EVENTS
		private void btnNewGame_Click(object sender, System.EventArgs e)
		{
			// add checking to end current game
			if (game_in_construction)
			{
				if (MessageBox.Show ("Game in construction. Cancel the current game construction and start a new one?", "Game construction in progress.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
					return;
			}
			
			_newGame ();
			
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if (game_in_construction)
			{
				if (MessageBox.Show ("Game in construction. Cancel the current game construction?", "Game construction in progress.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
					return;
			}
			
			this.Close ();
		}

		private void checkBoxBrick_CheckedChanged(object sender, System.EventArgs e)
		{
			// if atomic element return to miniature
			if (current_atomic_object is GAtomicElementObject)
			{
				// return to miniature
				int index = ((GAtomicElementObject)current_atomic_object).IndexInList;
				int row = index / miniature_area_cols;
				int col = index - row * miniature_area_cols;

				GAtomicElementObject element_to_return_to_miniature = new GAtomicElementObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top)
					, row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()
					, ((GAtomicElementObject)current_atomic_object)._getAtomicElement ()
					, ((GAtomicElementObject)current_atomic_object).ConnectionElements
					, ((GAtomicElementObject)current_atomic_object).IndexInList);

				miniature_elements.SetValue (element_to_return_to_miniature, row, col);

				this.Invalidate (miniature_area_rect);
			}
			if (checkBoxBrick.Checked)
			{
				current_atomic_object = new GAtomicBrick (new Point (current_atomic_object_area_rect.Left, current_atomic_object_area_rect.Top), 0, 0, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ());
			}
			else
			{
				current_atomic_object = new GAtomicObject (new Point (current_atomic_object_area_rect.Left, current_atomic_object_area_rect.Top), 0, 0, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ());
			}

			this.Invalidate (current_atomic_object_area_rect);
		}
		#endregion

		#region GAtomicGameEditorForm_MouseUp
		private void GAtomicGameEditorForm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point p = new Point (0, 0);

			if (e.Button == MouseButtons.Right && miniature_moving)
			{
				miniature_moving = false;
				btnMoveFormulaToGameBoard.Text = "Move Formula to Game Board";
				return;
			}
			else if (miniature_moving)
			{
				// check for game area
				p = new Point (e.X - GAtomicConfiguration._getPlayingAreaTopLeft ().X, e.Y - GAtomicConfiguration._getPlayingAreaTopLeft ().Y);

				if ((p.X < 0) || (p.Y < 0))
					return;

				if ((p.X <= game_elements.GetLength (0) * GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()) && (p.Y <= game_elements.GetLength (1) * GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()))
				{
					// game_area clicked
					int row = p.Y / GAtomicConfiguration._getPlayingAreaAtomicObjectSize ();
					int col = p.X / GAtomicConfiguration._getPlayingAreaAtomicObjectSize ();

					if ((row == 0) || (row == game_area_rows - 1) || (col == 0) || (col == game_area_cols - 1))
					{
						// borders, do nothing
						return;
					}

					// bottom right, check if fits, any bricks do not count, they are replaced
					if (row - (miniature_area_rows - 2) < 0)
						return;
					else if (col + (miniature_area_cols - 2) > game_area_cols - 1)
						return;

					int bottom_row = row;
					int left_col = col;

					for (row = miniature_area_rows - 2; row >= 1; row--)
					{
						for (col = 1; col <= miniature_area_cols - 2; col++)
						{
							object o = miniature_elements.GetValue (row, col);
							if ((o != null) && (o is GAtomicElementObject))
							{
								GAtomicElementObject elem = new GAtomicElementObject (GAtomicConfiguration._getPlayingAreaTopLeft ()
									, bottom_row + (row - (miniature_area_rows - 2)), left_col + col - 1, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()
									, ((GAtomicElementObject)o)._getAtomicElement ()
									, ((GAtomicElementObject)o).ConnectionElements
									, ((GAtomicElementObject)o).IndexInList);
								
								game_elements.SetValue (elem, bottom_row + (row - (miniature_area_rows - 2)), left_col + col - 1);

								miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col);
							}
						}
					}

					miniature_moving = false;
					btnMoveFormulaToGameBoard.Text = "Move Formula to Game Board";

					this.Invalidate (miniature_area_rect);
					this.Invalidate (game_area_rect);
				}
				return;
			}

			// Check for miniature area
			p = new Point (e.X - miniature_area_rect.Left, e.Y - miniature_area_rect.Top);

			if ((p.X > 0) && (p.Y > 0) && (p.X <= miniature_area_rect.Width) && (p.Y <= miniature_area_rect.Height))
			{
				// miniature_area clicked
				int row = p.Y / GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ();
				int col = p.X / GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ();

				if ((row == 0) || (row == miniature_area_rows - 1) || (col == 0) || (col == miniature_area_cols - 1))
				{
					// borders, do nothing
					return;
				}

				GAtomicObject elem = (GAtomicObject)miniature_elements.GetValue (row, col);
				// Move gatomicelement to current object and remove from miniature
				
				if (elem is GAtomicElementObject)
				{
					checkBoxBrick.Checked = false;
					if (current_atomic_object is GAtomicElementObject)
					{
						// return to miniature
						int index = ((GAtomicElementObject)current_atomic_object).IndexInList;
						int row_temp = index / miniature_area_cols;
						int col_temp = index - row_temp * miniature_area_cols;

						GAtomicElementObject element_to_return_to_miniature = new GAtomicElementObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top)
							, row_temp, col_temp, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()
							, ((GAtomicElementObject)current_atomic_object)._getAtomicElement ()
							, ((GAtomicElementObject)current_atomic_object).ConnectionElements
							, ((GAtomicElementObject)current_atomic_object).IndexInList);

						miniature_elements.SetValue (element_to_return_to_miniature, row_temp, col_temp);

						//this.Invalidate (miniature_area_rect);
					}

					current_atomic_object = new GAtomicElementObject (new Point (current_atomic_object_area_rect.Left, current_atomic_object_area_rect.Top)
						, 0, 0, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()
						, ((GAtomicElementObject)elem)._getAtomicElement ()
						, ((GAtomicElementObject)elem).ConnectionElements
						, ((GAtomicElementObject)elem).IndexInList);

					this.Invalidate (current_atomic_object_area_rect);

					// remove from miniature
					miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col);

					this.Invalidate (miniature_area_rect);

				} // end of if (elem is GAtomicElementObject)
				else
				{
					// do nothing
				}

			} // end of if ((p.X > 0) && (p.Y > 0) && (p.X <= miniature_area_rect.Width) && (p.Y <= miniature_area_rect.Height))
			else	// of if ((p.X > 0) && (p.Y > 0) && (p.X <= miniature_area_rect.Width) && (p.Y <= miniature_area_rect.Height))
			{

				p = new Point (e.X - GAtomicConfiguration._getPlayingAreaTopLeft ().X, e.Y - GAtomicConfiguration._getPlayingAreaTopLeft ().Y);

				if ((p.X < 0) || (p.Y < 0))
					return;

				if ((p.X <= game_elements.GetLength (1) * GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()) && (p.Y <= game_elements.GetLength (0) * GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()))
				{
					// game_area clicked
					int row = p.Y / GAtomicConfiguration._getPlayingAreaAtomicObjectSize ();
					int col = p.X / GAtomicConfiguration._getPlayingAreaAtomicObjectSize ();

					if ((row == 0) || (row == game_area_rows - 1) || (col == 0) || (col == game_area_cols - 1))
					{
						// borders, do nothing
						return;
					}

					if (e.Button == MouseButtons.Right)
					{
						// if brick or element remove, brick, ok, element, send back to miniature
						object game_area_object = game_elements.GetValue (row, col);
						if (game_area_object is GAtomicBrick)
						{
							game_elements.SetValue (new GAtomicObject (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col);
							this.Invalidate (game_area_rect);
						}
						else if (game_area_object is GAtomicElementObject)
						{
							int index = ((GAtomicElementObject)game_area_object).IndexInList;
							int row_temp = index / miniature_area_cols;
							int col_temp = index - row_temp * miniature_area_cols;

							GAtomicElementObject element_to_return_to_miniature = new GAtomicElementObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top)
								, row_temp, col_temp, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()
								, ((GAtomicElementObject)game_area_object)._getAtomicElement ()
								, ((GAtomicElementObject)game_area_object).ConnectionElements
								, ((GAtomicElementObject)game_area_object).IndexInList);

							miniature_elements.SetValue (element_to_return_to_miniature, row_temp, col_temp);

							this.Invalidate (miniature_area_rect);

							game_elements.SetValue (new GAtomicObject (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col);
							this.Invalidate (game_area_rect);
						}
					}
					else if (e.Button == MouseButtons.Left)
					{
						// add current object if brick or atomic element
						// verify if atomic element doesn't exist where ur putting the other and return it to the miniature
						if (current_atomic_object != null)
						{
							if ((current_atomic_object is GAtomicBrick) || (current_atomic_object is GAtomicElementObject))
							{
								GAtomicObject game_area_object = (GAtomicObject)game_elements.GetValue (row, col);
								if (game_area_object is GAtomicElementObject)
								{
									int index = ((GAtomicElementObject)game_area_object).IndexInList;
									int row_temp = index / miniature_area_cols;
									int col_temp = index - row_temp * miniature_area_cols;

									GAtomicElementObject element_to_return_to_miniature = new GAtomicElementObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top)
										, row_temp, col_temp, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()
										, ((GAtomicElementObject)game_area_object)._getAtomicElement ()
										, ((GAtomicElementObject)game_area_object).ConnectionElements
										, ((GAtomicElementObject)game_area_object).IndexInList);

									miniature_elements.SetValue (element_to_return_to_miniature, row_temp, col_temp);

									this.Invalidate (miniature_area_rect);
								}
							}

							if (current_atomic_object is GAtomicBrick)
							{
								game_elements.SetValue (new GAtomicBrick (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col);
								this.Invalidate (game_area_rect);
							}
							else if (current_atomic_object is GAtomicElementObject)
							{
								GAtomicElementObject elem = (GAtomicElementObject)current_atomic_object;
								elem._moveAndResize (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ());
								game_elements.SetValue (elem, row, col);

								this.Invalidate (game_area_rect);

								// set current atomic object to simple atomic object
								current_atomic_object = new GAtomicObject (new Point (current_atomic_object_area_rect.Left, current_atomic_object_area_rect.Top), 0, 0, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ());
								this.Invalidate (current_atomic_object_area_rect);
							}

						}// end of if (current_atomic_object != null)

					} // end of else if (e.Button == MouseButtons.Left)

				} // end of if ((p.X <= game_area_rect.Width) && (p.Y <= game_area_rect.Height))

			} // end of else of if ((p.X > 0) && (p.Y > 0) && (p.X <= miniature_area_rect.Width) && (p.Y <= miniature_area_rect.Height))
		}
		#endregion

		#region btnSaveGame_Click
		private void btnSaveGame_Click(object sender, System.EventArgs e)
		{
			if (!_isMiniatureEmpty ())
			{
				MessageBox.Show ("Miniature not empty. Game not finished.", "Game not finished, cannot be saved yet.", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (MessageBox.Show ("Save Game? It cannot be modified after you save it.", "Save Game", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
			{
				return;
			}

			if (txtGameName.Text == "")
				return;

			// Write to file
			// first line, game name
			// save formula as in formula file
			// save game, dimensions, then each line
			// which one to select at start?
			bool file_exists = File.Exists (Directory.GetCurrentDirectory () + "/games/" + txtGameName.Text + ".gagh");
			if (file_exists)
			{
				// Overwrite file
				if (MessageBox.Show ("Game with same name already exist. Overwrite?", "Game exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
				{
					return;
				}
				File.Delete (Directory.GetCurrentDirectory () + "/games/" + txtGameName.Text + ".gagh");
			}
			// Save to file
			String string_to_write_into_file = "";
			// Save file name with formula name
			GLDoorsFileOperations file_operations = new GLDoorsFileOperations (Directory.GetCurrentDirectory () + "/games/" + txtGameName.Text + ".gagh");
			file_operations.CreateFile ();
			// Save line, game name
			string_to_write_into_file = txtGameName.Text + "\r\n";
			file_operations.WriteStringToFile (string_to_write_into_file);

			// Save sizes
			string_to_write_into_file = String.Format ("Game:{0}x{1}\r\n", game_area_rows, game_area_cols);
			file_operations.WriteStringToFile (string_to_write_into_file);
			
			string_to_write_into_file = String.Format ("Miniature:{0}x{1}\r\n", miniature_area_rows, miniature_area_cols);
			file_operations.WriteStringToFile (string_to_write_into_file);

			// Save elements by rows, EMPTY for no element, BRICK for brick
			object o = null;
			int row = 0;
			int col = 0;
			
			// Save formula (miniature with borders)
			for (row = 0; row < miniature_area_rows; row++)
			{
				for (col = 0; col < miniature_area_cols; col++)
				{
					o = original_miniature_elements.GetValue (row, col);
					if (o != null)
					{
						if (o is GAtomicElementObject)
						{
							string_to_write_into_file = ((GAtomicElementObject)o).ToString ();
						}
						else if (o is GAtomicObject)
						{
							string_to_write_into_file = "EMPTY";
						}

						file_operations.WriteStringToFile (string_to_write_into_file + "\r\n");
					}
				}
			}

			// Save game
			for (row = 0; row < game_area_rows; row++)
			{
				for (col = 0; col < game_area_cols; col++)
				{
					o = game_elements.GetValue (row, col);
					if (o != null)
					{
						if (o is GAtomicElementObject)
						{
							string_to_write_into_file = ((GAtomicElementObject)o).ToString () + String.Format ("{0}", ((GAtomicElementObject)o).IndexInList);
						}
						else if (o is GAtomicBrick)
						{
							string_to_write_into_file = "BRICK";
						}
						else if (o is GAtomicObject)
						{
							string_to_write_into_file = "EMPTY";
						}

						file_operations.WriteStringToFile (string_to_write_into_file + "\r\n");
					}
				}
			}

			file_operations.CloseFile ();

			MessageBox.Show ("Game '" + txtGameName.Text + "' saved.", "Game saved");


			// at the end and succesfull
			game_in_construction = false;
		}
		#endregion

		#region btnMoveFormulaToGameBoard_Click
		private void btnMoveFormulaToGameBoard_Click(object sender, System.EventArgs e)
		{
			int row, col, temp_number_of_elements = 0;
			for (row = 1; row <= miniature_area_rows - 2; row++)
			{
				for (col = 1; col <= miniature_area_cols - 2; col++)
				{
					object o = miniature_elements.GetValue (row, col);
					if (o != null && o is GAtomicElementObject)
					{
						miniature_elements.SetValue ((GAtomicElementObject)o, row, col);
						original_miniature_elements.SetValue ((GAtomicElementObject)o, row, col);
						temp_number_of_elements++;
					}
				}
			}
	
			if (temp_number_of_elements != miniature_number_of_elements)
			{
				MessageBox.Show ("Miniature must be complete.", "Miniature not complete.");
				return;
			}

			MessageBox.Show ("Select bottom right of miniature.", "Select bottom right.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

			miniature_moving = true;

			btnMoveFormulaToGameBoard.Text = "Move Formula to Game Board*";

		}
		#endregion

		#region _loadGame
		private bool _loadGame ()
		{
			GAtomicChoiceForm choice = new GAtomicChoiceForm ("");
			if (choice.ShowDialog (this) == DialogResult.Cancel)
				return false;
			
			String game = choice.selected_game;

			bool file_exists = File.Exists (Directory.GetCurrentDirectory () + "/games/" + game + ".gagh");
			if (!file_exists)
			{
				return false;
			}

			this.txtGameName.Text = game;

			GLDoorsFileOperations file_operations = new GLDoorsFileOperations (Directory.GetCurrentDirectory () + "/games/" + txtGameName.Text + ".gagh");
			file_operations.OpenFileForRead ();

			bool error = false;
			try
			{
				// read from file
				String s = "";
				bool b = false;
				// Name OK, read line anyway
				file_operations.ReadStringLineFromFile (out s, out b);
				// Read game size
				file_operations.ReadStringLineFromFile (out s, out b);
				String sizes_string = s.Substring (5);
				game_area_rows = Convert.ToInt32 (sizes_string.Substring (0, sizes_string.IndexOf ("x")));
				game_area_cols = Convert.ToInt32 (sizes_string.Substring (sizes_string.IndexOf ("x") + 1));

				game_in_construction = true;

				game_elements = Array.CreateInstance (typeof(GAtomicObject), game_area_rows, game_area_cols);

				miniature_moving = false;

				file_operations.ReadStringLineFromFile (out s, out b);
				sizes_string = s.Substring (10);
				miniature_area_rows = Convert.ToInt32 (sizes_string.Substring (0, sizes_string.IndexOf ("x")));
				miniature_area_cols = Convert.ToInt32 (sizes_string.Substring (sizes_string.IndexOf ("x") + 1));

				miniature_elements = Array.CreateInstance (typeof(GAtomicObject), miniature_area_rows, miniature_area_cols);
				original_miniature_elements = Array.CreateInstance (typeof(GAtomicObject), miniature_area_rows, miniature_area_cols);

				_setSizesAndPlacements ();

				Point miniature_top_left = new Point (miniature_area_rect.Left, miniature_area_rect.Top);
				//---------------------------Placement-and-sizes--------------------------------

				//--------------------------------Miniatures-----------------------------------------------
				miniature_number_of_elements = 0;
				int row = 0;
				int col = 0;
				for (row = 0; row < miniature_elements.GetLength (0); row++)
				{
					for (col = 0; col < miniature_elements.GetLength (1); col++)
					{
						file_operations.ReadStringLineFromFile (out s, out b);
						if (!b)
						{
							// erreur
							error = true;
							break;
						}
						miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col); 
						if (s == "EMPTY")
						{
							//miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col); 
							original_miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col); 
						}
						else
						{
							// Read and add
							object o = GAtomicElementObject._getAtomicElementFromString (s, new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize (), row * miniature_area_cols + col);
							if (o != null)
							{
								//miniature_elements.SetValue ((GAtomicElementObject)o, row, col);
								original_miniature_elements.SetValue ((GAtomicElementObject)o, row, col);
								miniature_number_of_elements++;
							}
						}
					} // end of for (col = 0; col < miniature_elements.GetLength (1); cols++)

					if (error)
						break;
				}
				//--------------------------------Miniatures-----------------------------------------------

				//-----------------------------------------Game-Area--------------------------------------------
				for (row = 0; row < game_elements.GetLength (0); row++)
				{
					for (col = 0; col < game_elements.GetLength (1); col++)
					{
						file_operations.ReadStringLineFromFile (out s, out b);
						if (!b)
						{
							// erreur
							error = true;
							break;
						}
						if (s == "EMPTY")
						{
							game_elements.SetValue (new GAtomicObject (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col); 
						}
						else if (s == "BRICK")
						{
							game_elements.SetValue (new GAtomicBrick (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col); 
						}
						else
						{
							// Read and add
							object o = GAtomicElementObject._getAtomicElementFromString (s.Substring (0, 19), GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize (), Convert.ToInt32 (s.Substring (19)));
							if (o != null)
							{
								game_elements.SetValue ((GAtomicElementObject)o, row, col);
							}
							else
							{
								error = true;
								break;
							}
						}
					}

					if (error)
						break;
				}

				//-----------------------------------------Game-Area--------------------------------------------

				this.Invalidate ();

			}
			catch
			{
				file_operations.CloseFile ();
				return false;
			}

			file_operations.CloseFile ();

			checkBoxBrick.Checked = true;

			if (error)
			{
				return false;
			}

			return true;
		}
		private void btnLoadGame_Click(object sender, System.EventArgs e)
		{
			if (game_in_construction)
			{
				if (MessageBox.Show ("Game in construction. Cancel the current game construction and load a new one?", "Game construction in progress.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
					return;
			}

			_loadGame ();
		}
		#endregion

	}
}
