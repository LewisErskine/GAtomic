using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace GLDoors2.GAtomic
{
	/// <summary>
	/// Summary description for GAtomicNewGameForm.
	/// </summary>
	public class GAtomicNewGameForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region VARIABLES
		private System.Windows.Forms.Panel panelSizes;
		private System.Windows.Forms.Panel panelButtons;
		private System.Windows.Forms.ListBox listBoxFormulas;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label labelGameAreaRows;
		private System.Windows.Forms.TextBox txtGameAreaRows;
		private System.Windows.Forms.TextBox txtGameAreaCols;
		private System.Windows.Forms.Label labelGameAreaCols;

		public String selected_formula;
		public int game_area_rows;
		public int game_area_cols;
		#endregion
	
		#region CONSTRUCTOR
		public GAtomicNewGameForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			String[] files = Directory.GetFiles (Directory.GetCurrentDirectory () + "\\formulas", "*.gaf");
			String[] formula_names = new String [files.GetLength (0)];
			int index = 0;
			foreach (String file in files)
			{
				formula_names[index] = file.Substring (file.LastIndexOf ("\\") + 1);
				formula_names[index] = formula_names[index].Substring (0, formula_names[index].LastIndexOf ("."));
				index++;
			}

			foreach (String formula_name in formula_names)
				listBoxFormulas.Items.Add (formula_name);

			selected_formula = "";

			listBoxFormulas.Focus ();

			game_area_rows = 0;
			game_area_cols = 0;
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
			this.panelSizes = new System.Windows.Forms.Panel();
			this.txtGameAreaCols = new System.Windows.Forms.TextBox();
			this.labelGameAreaCols = new System.Windows.Forms.Label();
			this.txtGameAreaRows = new System.Windows.Forms.TextBox();
			this.labelGameAreaRows = new System.Windows.Forms.Label();
			this.panelButtons = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.listBoxFormulas = new System.Windows.Forms.ListBox();
			this.panelSizes.SuspendLayout();
			this.panelButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelSizes
			// 
			this.panelSizes.Controls.Add(this.txtGameAreaCols);
			this.panelSizes.Controls.Add(this.labelGameAreaCols);
			this.panelSizes.Controls.Add(this.txtGameAreaRows);
			this.panelSizes.Controls.Add(this.labelGameAreaRows);
			this.panelSizes.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelSizes.Location = new System.Drawing.Point(0, 357);
			this.panelSizes.Name = "panelSizes";
			this.panelSizes.Size = new System.Drawing.Size(432, 40);
			this.panelSizes.TabIndex = 0;
			// 
			// txtGameAreaCols
			// 
			this.txtGameAreaCols.Location = new System.Drawing.Point(328, 8);
			this.txtGameAreaCols.Name = "txtGameAreaCols";
			this.txtGameAreaCols.TabIndex = 3;
			this.txtGameAreaCols.Text = "20";
			// 
			// labelGameAreaCols
			// 
			this.labelGameAreaCols.Location = new System.Drawing.Point(224, 8);
			this.labelGameAreaCols.Name = "labelGameAreaCols";
			this.labelGameAreaCols.Size = new System.Drawing.Size(96, 24);
			this.labelGameAreaCols.TabIndex = 2;
			this.labelGameAreaCols.Text = "Game Area Cols:";
			this.labelGameAreaCols.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtGameAreaRows
			// 
			this.txtGameAreaRows.Location = new System.Drawing.Point(112, 8);
			this.txtGameAreaRows.Name = "txtGameAreaRows";
			this.txtGameAreaRows.TabIndex = 1;
			this.txtGameAreaRows.Text = "20";
			// 
			// labelGameAreaRows
			// 
			this.labelGameAreaRows.Location = new System.Drawing.Point(8, 8);
			this.labelGameAreaRows.Name = "labelGameAreaRows";
			this.labelGameAreaRows.Size = new System.Drawing.Size(96, 24);
			this.labelGameAreaRows.TabIndex = 0;
			this.labelGameAreaRows.Text = "Game Area Rows:";
			this.labelGameAreaRows.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelButtons
			// 
			this.panelButtons.Controls.Add(this.btnCancel);
			this.panelButtons.Controls.Add(this.btnOK);
			this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelButtons.Location = new System.Drawing.Point(0, 397);
			this.panelButtons.Name = "panelButtons";
			this.panelButtons.Size = new System.Drawing.Size(432, 40);
			this.panelButtons.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(352, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 32);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(160, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(96, 32);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// listBoxFormulas
			// 
			this.listBoxFormulas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxFormulas.Location = new System.Drawing.Point(0, 0);
			this.listBoxFormulas.Name = "listBoxFormulas";
			this.listBoxFormulas.Size = new System.Drawing.Size(432, 355);
			this.listBoxFormulas.TabIndex = 2;
			// 
			// GAtomicNewGameForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(432, 437);
			this.Controls.Add(this.listBoxFormulas);
			this.Controls.Add(this.panelSizes);
			this.Controls.Add(this.panelButtons);
			this.Name = "GAtomicNewGameForm";
			this.Text = "GAtomicNewGame";
			this.panelSizes.ResumeLayout(false);
			this.panelButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region EVENTS
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if ((txtGameAreaRows.Text == "") || (txtGameAreaCols.Text == "")
				|| (listBoxFormulas.SelectedIndex < 0))
				return;

			try
			{
				game_area_rows = Convert.ToInt32 (txtGameAreaRows.Text);
				game_area_cols = Convert.ToInt32 (txtGameAreaCols.Text);
			}
			catch
			{
				return;
			}

			selected_formula =  (String)listBoxFormulas.SelectedItem;
			DialogResult = DialogResult.OK;
			this.Close ();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close ();
		}
		#endregion
	}
}
