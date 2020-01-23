using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

using GLDoors2.GAtomic.objects;

namespace GLDoors2.GAtomic
{
	/// <summary>
	/// Summary description for Form1.
	/// to do
	/// game editor, under construction
	/// 
	/// maybe : double bonds, top left, top right, bottom right, bottom left
	/// 
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region VARIABLES
		private Array elements;

		// Checking
		private Array checking_elements;
		
		private int selected_element_row;
		private int selected_element_col;

		private int number_of_elements;
		private int last_selected_element_index_in_list;

		private bool game_over;
		private Rectangle game_area_rect;
		private String current_game;

		private int movements;
		private Label label_movements;

		// draw miniature figure
		private Rectangle miniature_area_rect;
		private Array miniature_elements;
		#endregion

		#region CONSTRUCTOR
		public Form1()
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

			GAtomicConfiguration._setPlayingAreaTopLeft (new Point (20, 20));
			GAtomicConfiguration._setPlayingAreaAtomicObjectSize (30);

			GAtomicConfiguration._setMiniatureAreaAtomicObjectSize (20);
			
			selected_element_row = -1;
			selected_element_col = -1;
			
			label_movements = new Label ();
			label_movements.Name = "label_movements";
			label_movements.Size = new Size (150, 20);
			//label_movements.BorderStyle = BorderStyle.FixedSingle;
			label_movements.TextAlign = ContentAlignment.MiddleLeft;
			label_movements.Font = new Font ("Times New Roman", 12);
			this.Controls.Add (label_movements);

			_newGame ("Water");
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
				if (components != null) 
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
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(664, 517);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "GAtomic";
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);

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
			Application.Run(new Form1());
		}
		#endregion

		#region Paint
		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (elements == null || miniature_elements == null)
				return;

			GAtomicObject o = null;
			for (int x = 0; x < elements.GetLength (1); x++)
			{
				for (int y = 0; y < elements.GetLength (0); y++)
				{
					o = (GAtomicObject)elements.GetValue (y, x);
					if (o != null)
						o._draw (e.Graphics);
				}
			}

			e.Graphics.FillRectangle (new SolidBrush (Color.DarkGray), miniature_area_rect);

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
		}

		#endregion

		#region Form1_MouseUp
		private void Form1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (game_over)
				return;

			Point p = new Point (e.X - GAtomicConfiguration._getPlayingAreaTopLeft ().X, e.Y - GAtomicConfiguration._getPlayingAreaTopLeft ().Y);

			// up or left
			if ((p.Y < 0) || (p.X < 0))
				return;

			// bottom
			if ((p.Y > elements.GetLength (0) * GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()) || (p.X > elements.GetLength (1) * GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()))
				return;

			int row = p.Y / GAtomicConfiguration._getPlayingAreaAtomicObjectSize ();
			int col = p.X / GAtomicConfiguration._getPlayingAreaAtomicObjectSize ();

			_selectElement (row, col);
		}

		#endregion

		#region Form1_KeyUp And Down
		private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Left)
			{
				// check if selection and left is an arrow
				if ((selected_element_row != -1) && (selected_element_col != -1))
				{
					if (_validRowAndColumn (selected_element_row, selected_element_col - 1))
					{
						object o = elements.GetValue (selected_element_row, selected_element_col - 1);
						if ((o != null) && (o is GAtomicDirectionArrow))
						{
							// fill 'simply' the arrows
							_fillSimplyTheDirectionsArrows ();
							_moveElement (GAtomicMovementDirection.LEFT);
						}
					}
				}
			}
			else if (e.KeyCode == Keys.Right)
			{
				// check if selection and left is an arrow
				if ((selected_element_row != -1) && (selected_element_col != -1))
				{
					if (_validRowAndColumn (selected_element_row, selected_element_col + 1))
					{
						object o = elements.GetValue (selected_element_row, selected_element_col + 1);
						if ((o != null) && (o is GAtomicDirectionArrow))
						{
							// fill 'simply' the arrows
							_fillSimplyTheDirectionsArrows ();
							_moveElement (GAtomicMovementDirection.RIGHT);
						}
					}
				}
			}
			else if (e.KeyCode == Keys.Down)
			{
				// check if selection and left is an arrow
				if ((selected_element_row != -1) && (selected_element_col != -1))
				{
					if (_validRowAndColumn (selected_element_row + 1, selected_element_col))
					{
						object o = elements.GetValue (selected_element_row + 1, selected_element_col);
						if ((o != null) && (o is GAtomicDirectionArrow))
						{
							// fill 'simply' the arrows
							_fillSimplyTheDirectionsArrows ();
							_moveElement (GAtomicMovementDirection.DOWN);
						}
					}
				}
			}
			else if (e.KeyCode == Keys.Up)
			{
				// check if selection and left is an arrow
				if ((selected_element_row != -1) && (selected_element_col != -1))
				{
					if (_validRowAndColumn (selected_element_row - 1, selected_element_col))
					{
						object o = elements.GetValue (selected_element_row - 1, selected_element_col);
						if ((o != null) && (o is GAtomicDirectionArrow))
						{
							// fill 'simply' the arrows
							_fillSimplyTheDirectionsArrows ();
							_moveElement (GAtomicMovementDirection.UP);
						}
					}
				}
			}
			else if (e.KeyCode == Keys.F5)
			{
				if (!game_over)
				{
					if (MessageBox.Show ("Cancel current game?", "Cancel", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
					{
						return;	
					}
				}
				GAtomicChoiceForm choice = new GAtomicChoiceForm (current_game);
				if (choice.ShowDialog (this) == DialogResult.OK)
				{
					current_game = choice.selected_game;
					_newGame (current_game);	
				}
			}
		}

		private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.N)
			{
				if (last_selected_element_index_in_list + 1 > number_of_elements - 1)
				{
					last_selected_element_index_in_list = -1;
				}
				object o = null;
				//last_selected_element_index_in_list;
				for (int row = 0; row < elements.GetLength (0); row++)
				{
					for (int col = 0; col < elements.GetLength (1); col++)
					{
						o = elements.GetValue (row, col);
						if ((o != null) && (o is GAtomicElementObject))
						{
							if (((GAtomicElementObject)o).IndexInList == last_selected_element_index_in_list + 1)
							{
								_selectElement (row, col);
								return;
							}
						}
				
					}// end of for (int col = 0;....
				}// end of for (int row = 0;....
			}
		}
		#endregion

		#region _selectElement
		private void _selectElement (int row, int col)
		{
			object o = elements.GetValue (row, col);
			
			if (o is GAtomicElementObject)
			{
				if ((selected_element_row == row) && (selected_element_col == col))
				{
					// already selected
					return;
				}

				_fillSimplyTheDirectionsArrows ();

				// where can it can be moved and show arrows in the adjacent blocks
				bool can_move = _canMoveAndIfYesSetArrows (row, col);

				selected_element_row = row;
				selected_element_col = col;

				last_selected_element_index_in_list = ((GAtomicElementObject)elements.GetValue (row, col)).IndexInList;

				this.Invalidate (game_area_rect);
			}
			else if (o is GAtomicDirectionArrow)
			{
				// fill 'simply' the arrows
				_fillSimplyTheDirectionsArrows ();

				// move element in the direction of the arrow
				_moveElement (((GAtomicDirectionArrow)o)._getMovementDirection ());
			}
			else if (o is GAtomicObject)
			{
				_fillSimplyTheDirectionsArrows ();

				// clear selection
				selected_element_row = -1;
				selected_element_col = -1;

				this.Invalidate (game_area_rect);
			}
		}
		#endregion

		#region _fillSimplyTheDirectionsArrows
		private void _fillSimplyTheDirectionsArrows ()
		{
			// fill 'simply' the arrows
			for (int r = 0; r < elements.GetLength (0); r++)
			{
				for (int c = 0; c < elements.GetLength (1); c++)
				{
					if ((elements.GetValue (r, c) != null) && (elements.GetValue (r, c) is GAtomicDirectionArrow))
					{
						elements.SetValue (new GAtomicObject (GAtomicConfiguration._getPlayingAreaTopLeft (), r, c, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), r, c);
						//break;
					}
				}
			}
		}
		#endregion

		#region _canMoveAndIfYesSetArrows
		public bool _canMoveAndIfYesSetArrows (int row, int col)
		{
			object o2 = null;

			bool can_move_up = false, can_move_right = false, can_move_down = false, can_move_left = false;
			// check up
			if (row > 0)
			{
				o2 = elements.GetValue (row - 1, col);
				if ((o2 is GAtomicElementObject) || (o2 is GAtomicBrick))
				{}
				else
				{
					can_move_up = true;
					elements.SetValue (new GAtomicDirectionArrow (GAtomicMovementDirection.UP, GAtomicConfiguration._getPlayingAreaTopLeft (), row - 1, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row - 1, col);
				}
			}
			// check right
			if (col < elements.GetLength (1) - 1)
			{
				o2 = elements.GetValue (row, col + 1);
				if ((o2 is GAtomicElementObject) || (o2 is GAtomicBrick))
				{}
				else
				{
					can_move_right = true;
					elements.SetValue (new GAtomicDirectionArrow (GAtomicMovementDirection.RIGHT, GAtomicConfiguration._getPlayingAreaTopLeft (), row, col + 1, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col + 1);
				}
			}
			// check down
			if (row < elements.GetLength (0) - 1)
			{
				o2 = elements.GetValue (row + 1, col);
				if ((o2 is GAtomicElementObject) || (o2 is GAtomicBrick))
				{}
				else
				{
					can_move_down = true;
					elements.SetValue (new GAtomicDirectionArrow (GAtomicMovementDirection.DOWN, GAtomicConfiguration._getPlayingAreaTopLeft (), row + 1, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row + 1, col);
				}
			}
			// check left
			if (col > 0)
			{
				o2 = elements.GetValue (row, col - 1);
				if ((o2 is GAtomicElementObject) || (o2 is GAtomicBrick))
				{}
				else
				{
					can_move_left = true;
					elements.SetValue (new GAtomicDirectionArrow (GAtomicMovementDirection.LEFT, GAtomicConfiguration._getPlayingAreaTopLeft (), row, col - 1, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col - 1);
				}
			}

			if (can_move_up || can_move_right || can_move_down || can_move_left)
				return true;
			else
			{
				return false;
			}
		}
		#endregion

		#region _moveElement
		public bool _moveElement (GAtomicMovementDirection direction)
		{
			// selected_element_row, selected_element_col
			int row = selected_element_row;
			int col = selected_element_col;

			while (_moveElementOneStep (row, col, direction))
			{
				if (direction == GAtomicMovementDirection.LEFT)
					col--;
				else if (direction == GAtomicMovementDirection.RIGHT)
					col++;
				else if (direction == GAtomicMovementDirection.UP)
					row--;
				else if (direction == GAtomicMovementDirection.DOWN)
					row++;
				else
					break;

				//System.Threading.Thread.Sleep (500);
			}

			movements++;
			label_movements.Text = String.Format ("Movements : {0}", movements);

			// Check if success
			if (_gameSuccess ())
			{
				MessageBox.Show ("Success.", "Success.");
				game_over = true;
				return true;
			}
			// Check new directions and set arrows
			bool can_move = _canMoveAndIfYesSetArrows (row, col);

			selected_element_row = -1;
			selected_element_col = -1;

			if (can_move)
			{
				selected_element_row = row;
				selected_element_col = col;

				this.Invalidate (game_area_rect);

				return true;
			}

			return false;
		}

		public bool _moveElementOneStep (int row, int col, GAtomicMovementDirection direction)
		{
			int new_row = row;
			int new_col = col;

			if (direction == GAtomicMovementDirection.LEFT)
				new_col--;
			else if (direction == GAtomicMovementDirection.RIGHT)
				new_col++;
			else if (direction == GAtomicMovementDirection.UP)
				new_row--;
			else if (direction == GAtomicMovementDirection.DOWN)
				new_row++;
			else
				return false;

			if (!_validRowAndColumn (new_row, new_col))
				return false;

			object o2 = elements.GetValue (new_row, new_col);
			if ((o2 is GAtomicElementObject) || (o2 is GAtomicBrick))
				return false;

			object o = elements.GetValue (row, col);

			if (!(o is GAtomicElementObject))
				return false;

			// change top left
			((GAtomicElementObject)o).TopLeft = new Point (GAtomicConfiguration._getPlayingAreaTopLeft ().X + new_col * GAtomicConfiguration._getPlayingAreaAtomicObjectSize (), GAtomicConfiguration._getPlayingAreaTopLeft ().Y + new_row * GAtomicConfiguration._getPlayingAreaAtomicObjectSize ());
			// new position
			elements.SetValue ((GAtomicElementObject)o, new_row, new_col);

			// clear old position
			elements.SetValue (new GAtomicObject (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col);

			this.Invalidate (game_area_rect);

			return true;
		}
		#endregion

		#region _validRowAndColumn
		private bool _validRowAndColumn (int row, int col)
		{
			if ((col < 0) || (col > elements.GetLength (1)))
				return false;
			else if ((row < 0) || (row > elements.GetLength (0)))
				return false;
			else
				return true;
		}
		#endregion

		#region _gameSuccess
		private bool _gameSuccess ()
		{
			//checking_elements
			// check the array for all elements with the characteristics needed

			object element_object = null;
			GAtomicElementObject atomic_element = null;
			
            GAtomicElementObject element_to_check_against = null;
			GAtomicElementObject element_to_check_against_connection_element = null;
			
			int neighbour_row = 0, neighbour_col = 0;

			int checking_elements_index = 0;

			bool element_ok = true;

			for (int row = 0; row < elements.GetLength (0); row++)
			{
				if (!element_ok)
				{
					// an element is in error, it's all we need to know
					return false;
				}

				for (int col = 0; col < elements.GetLength (1); col++)
				{
					if (!element_ok)
					{
						// an element is in error, it's all we need to know
						return false;
					}

                    element_object = elements.GetValue (row, col);
					if ((element_object != null) && (element_object is GAtomicElementObject))
					{
						atomic_element = (GAtomicElementObject)element_object;

						checking_elements_index = 0;
						element_ok = false;
						// check for same(s) in checking elements_array, can have many
						for (checking_elements_index = 0; checking_elements_index < checking_elements.GetLength (0) && !element_ok; checking_elements_index++)
						{
							// only directions are checked
							if (atomic_element == (GAtomicElementObject)checking_elements.GetValue (checking_elements_index))
							{
								// now directions + elements
								element_to_check_against = (GAtomicElementObject)checking_elements.GetValue (checking_elements_index);
								// check if atomic_element has same connecting elements as element_to_check_against

								// check all the directions of element_to_check_against
								for (GAtomicConnectionDirection direction = GAtomicConnectionDirection.TOP; direction <= GAtomicConnectionDirection.TRIPLE_LEFT; direction++)
								{
									element_object = element_to_check_against._getConnectionElement (direction);
									
									if (element_object != null)
									{
										element_to_check_against_connection_element = (GAtomicElementObject)element_object;
										// get next row and col
										neighbour_row = row;
										neighbour_col = col;
										if ((direction == GAtomicConnectionDirection.TOP) || (direction == GAtomicConnectionDirection.DOUBLE_TOP) || (direction == GAtomicConnectionDirection.TRIPLE_TOP))
										{
											neighbour_row--;
										}
										else if (direction == GAtomicConnectionDirection.TOP_RIGHT)
										{
											neighbour_row--;
											neighbour_col++;
										}
										else if ((direction == GAtomicConnectionDirection.RIGHT) || (direction == GAtomicConnectionDirection.DOUBLE_RIGHT) || (direction == GAtomicConnectionDirection.TRIPLE_RIGHT))
										{
											neighbour_col++;
										}
										else if (direction == GAtomicConnectionDirection.BOTTOM_RIGHT)
										{
											neighbour_row++;
											neighbour_col++;
										}
										else if ((direction == GAtomicConnectionDirection.BOTTOM) || (direction == GAtomicConnectionDirection.DOUBLE_BOTTOM) || (direction == GAtomicConnectionDirection.TRIPLE_BOTTOM))
										{
											neighbour_row++;
										}
										else if (direction == GAtomicConnectionDirection.BOTTOM_LEFT)
										{
											neighbour_row++;
											neighbour_col--;
										}
										else if ((direction == GAtomicConnectionDirection.LEFT) || (direction == GAtomicConnectionDirection.DOUBLE_LEFT) || (direction == GAtomicConnectionDirection.TRIPLE_LEFT))
										{
											neighbour_col--;
										}
										else if  (direction == GAtomicConnectionDirection.TOP_LEFT)
										{
											neighbour_row--;
											neighbour_col--;
										}
								
										if (!_validRowAndColumn (neighbour_row, neighbour_col))
											return false;
								
										element_object = elements.GetValue (neighbour_row, neighbour_col);

										if (!(element_object is GAtomicElementObject))
											return false;
								
										// check for same original element with same connections
										if ((GAtomicElementObject)element_object == element_to_check_against_connection_element)
										{
											element_ok = true;
											// continue for other directions
											continue;
											// break : for (checking_elements_index = 0; checking_elements_index < checking_elements.GetLength (0) && !element_ok; checking_elements_index++)
										}
										else
										{
											element_ok = false;	
											break;
										}
									} //  end of if (element_object != null)
								
								} // end of for (GAtomicConnectionDirection direction = GAtomicConnectionDirection.TOP; direction <= GAtomicConnectionDirection.TRIPLE_LEFT; direction++)

							} // end of if (atomic_element == (GAtomicElementObject)checking_elements.GetValue (checking_elements_index))

						} // end of for (checking_elements_index = 0; checking_elements_index < checking_elements.GetLength (0) && !element_ok; checking_elements_index++)

					} //  end of if ((element_object != null) && (element_object is GAtomicElementObject))

				} // end of for (int col = 0; col < elements.GetLength (1); col++)
				
			} //  end of for (int row = 0; row < elements.GetLength (0); row++)

			return true;
		}
		#endregion

		#region _setSizesAndPlacement
		private void _setSizesAndPlacement (int game_board_rows, int game_board_cols, int miniature_board_rows, int miniature_board_cols)
		{
			//---------------------------Placement-and-sizes--------------------------------
			game_area_rect = new Rectangle (0, 0, 2 * GAtomicConfiguration._getPlayingAreaTopLeft ().X + game_board_cols * GAtomicConfiguration._getPlayingAreaAtomicObjectSize (), 2 * GAtomicConfiguration._getPlayingAreaTopLeft ().Y + game_board_rows * GAtomicConfiguration._getPlayingAreaAtomicObjectSize ());

			int miniature_area_height = miniature_board_rows * GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ();
			
			//if (miniature_area_height > size2 * game_area_rect.Height / 3 - GAtomicConfiguration._getPlayingAreaTopLeft ().Y - 20*/

			miniature_area_rect = new Rectangle (game_area_rect.Width, GAtomicConfiguration._getPlayingAreaTopLeft ().Y, miniature_board_cols * GAtomicConfiguration._getMiniatureAreaAtomicObjectSize (), miniature_area_height);

			label_movements.Location = new Point (miniature_area_rect.Left, 2 * game_area_rect.Height / 3);
			movements = 0;
			label_movements.Text = String.Format ("Movements : -");
			
			this.ClientSize = new Size (game_area_rect.Width + GAtomicConfiguration._getPlayingAreaTopLeft ().X + miniature_area_rect.Width, game_area_rect.Height);
		}
		#endregion

		#region _newGame
		private void _newGame (String game)
		{
			String open_file_name = Directory.GetCurrentDirectory () + "/games/" + game + ".gagh";
			if (!File.Exists (open_file_name))
				return;

			game_over = false;

			selected_element_row = -1;
			selected_element_col = -1;

			current_game = game;
			this.Text = "GAtomic - " + current_game;

			// Read file
			bool error = false;
			try
			{
				// Load formula
				GLDoorsFileOperations file_operations = new GLDoorsFileOperations (open_file_name);
				file_operations.OpenFileForRead ();

				String s = "";
				bool b = false;
				// Name OK, read line anyway
				file_operations.ReadStringLineFromFile (out s, out b);
				// Read sizes
				file_operations.ReadStringLineFromFile (out s, out b);
				String sizes_string = s;
				s = s.Substring (5);
				int rows = Convert.ToInt32 (s.Substring (0, s.IndexOf ("x")));
				int cols = Convert.ToInt32 (s.Substring (s.IndexOf ("x") + 1));
			
				//---------------------------Board-size--------------------------------
				elements = Array.CreateInstance (typeof(GAtomicObject), rows, cols);

				file_operations.ReadStringLineFromFile (out s, out b);
				sizes_string = s;
				s = s.Substring (10);
				rows = Convert.ToInt32 (s.Substring (0, s.IndexOf ("x")));
				cols = Convert.ToInt32 (s.Substring (s.IndexOf ("x") + 1));
				miniature_elements = Array.CreateInstance (typeof(GAtomicObject), rows, cols);
				//---------------------------Board-size--------------------------------

				//---------------------------Placement-and-sizes--------------------------------
				_setSizesAndPlacement (elements.GetLength (0), elements.GetLength (1), miniature_elements.GetLength (0), miniature_elements.GetLength (1));

				Point miniature_top_left = new Point (miniature_area_rect.Left, miniature_area_rect.Top);
				//---------------------------Placement-and-sizes--------------------------------

				//--------------------------------Miniatures-----------------------------------------------
				number_of_elements = 0;
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
						if (s == "EMPTY")
						{
							miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize ()), row, col); 
						}
						else
						{
							// Read and add
							object o = GAtomicElementObject._getAtomicElementFromString (s, new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getMiniatureAreaAtomicObjectSize (), 0);
							if (o != null)
							{
								miniature_elements.SetValue ((GAtomicElementObject)o, row, col);
								number_of_elements++;
							}
						}
                    } // end of for (col = 0; col < miniature_elements.GetLength (1); cols++)

					if (error)
						break;
				}
				//--------------------------------Miniatures-----------------------------------------------

				//--------------------------------Checking-elements--------------------------------------
				_selectCheckingConnectionsAndElements ();
				//--------------------------------Checking-elements--------------------------------------

				int index = 0;
				Random rnd = new Random ();
				int sel_index = rnd.Next ();
				sel_index %= number_of_elements;
				int start_element_row = 0;
				int start_element_col = 0;
				//-----------------------------------------Game-Area--------------------------------------------
				for (row = 0; row < elements.GetLength (0); row++)
				{
					for (col = 0; col < elements.GetLength (1); col++)
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
							elements.SetValue (new GAtomicObject (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col); 
						}
						else if (s == "BRICK")
						{
							elements.SetValue (new GAtomicBrick (GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col); 
						}
						else
						{
							// Read and add
							object o = GAtomicElementObject._getAtomicElementFromString (s.Substring (0, 19), GAtomicConfiguration._getPlayingAreaTopLeft (), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize (), index++);
							if (o != null)
							{
								elements.SetValue ((GAtomicElementObject)o, row, col);
								if (sel_index == index - 1)
								{
									start_element_row = row;
									start_element_col = col;
								}
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

				// Select start element
				_selectElement (start_element_row, start_element_col);

				this.Invalidate ();

				file_operations.CloseFile ();
			}
			catch (GAtomicElementObjectInvalidException)
			{
				MessageBox.Show ("Invalid atomic element object.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				error = true;
			}
			catch
			{
				error = true;
			}
			
		}
		#endregion

		#region _selectCheckingConnectionsAndElements
		private void _selectCheckingConnectionsAndElements ()
		{
			checking_elements = Array.CreateInstance (typeof(GAtomicObject), number_of_elements);
			// read miniature elements and read connections from it in an array (connection direction and element)
			// forget first and last row, first and last column, only borders
			
			int row = 0, col = 0;
			int neighbour_row = row;
			int neighbour_col = col;
			object array_object = null;
			ArrayList neighbours = new ArrayList ();
			ArrayList connections = new ArrayList ();
			GAtomicElementObject base_element = null;

			int index = 0;

			for (row = 1; row < miniature_elements.GetLength (0) - 1; row++)
			{
				for (col = 1; col < miniature_elements.GetLength (1) - 1; col++)
				{
					array_object = miniature_elements.GetValue (row, col);
					if (array_object is GAtomicElementObject)
					{
						base_element = (GAtomicElementObject)array_object;

						neighbours = new ArrayList ();
						connections = new ArrayList ();
						// check if it has connecting elements assigned to it
						for (GAtomicConnectionDirection direction = GAtomicConnectionDirection.TOP; direction <= GAtomicConnectionDirection.TRIPLE_LEFT; direction++)
						{
							if (((GAtomicElementObject)array_object)._hasConnection (direction))
							{
								// Check connections
								neighbour_row = row;
								neighbour_col = col;
								if ((direction == GAtomicConnectionDirection.TOP) || (direction == GAtomicConnectionDirection.DOUBLE_TOP) || (direction == GAtomicConnectionDirection.TRIPLE_TOP))
								{
									neighbour_row--;
								}
								else if (direction == GAtomicConnectionDirection.TOP_RIGHT)
								{
									neighbour_row--;
									neighbour_col++;
								}
								else if ((direction == GAtomicConnectionDirection.RIGHT) || (direction == GAtomicConnectionDirection.DOUBLE_RIGHT) || (direction == GAtomicConnectionDirection.TRIPLE_RIGHT))
								{
									neighbour_col++;
								}
								else if (direction == GAtomicConnectionDirection.BOTTOM_RIGHT)
								{
									neighbour_row++;
									neighbour_col++;
								}
								else if ((direction == GAtomicConnectionDirection.BOTTOM) || (direction == GAtomicConnectionDirection.DOUBLE_BOTTOM) || (direction == GAtomicConnectionDirection.TRIPLE_BOTTOM))
								{
									neighbour_row++;
								}
								else if (direction == GAtomicConnectionDirection.BOTTOM_LEFT)
								{
									neighbour_row++;
									neighbour_col--;
								}
								else if ((direction == GAtomicConnectionDirection.LEFT) || (direction == GAtomicConnectionDirection.DOUBLE_LEFT) || (direction == GAtomicConnectionDirection.TRIPLE_LEFT))
								{
									neighbour_col--;
								}
								else if  (direction == GAtomicConnectionDirection.TOP_LEFT)
								{
									neighbour_row--;
									neighbour_col--;
								}
								
								if (!_validRowAndColumn (neighbour_row, neighbour_col))
									continue;

								base_element._setConnectionElement (direction, (GAtomicElementObject)miniature_elements.GetValue (neighbour_row, neighbour_col));
							}
						}// end of for (GAtomicConnectionDirection direction = GAt

						// you need to add en element with its directions**********
						// add element, direction and element at that direction
						// we want take the array object and put the elements in the direction instead of the null
						// so we need to make a new copy of this element
						// and then put the elements instead of null with each connection

						checking_elements.SetValue (base_element, index);					
					
						index++;
					}// end of if (array_object is GAtomicElementObject)
				}// end of for col
			}// end of for row
		}
		#endregion

	}
}