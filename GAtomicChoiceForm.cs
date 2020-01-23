using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace GLDoors2.GAtomic
{
	/// <summary>
	/// Summary description for GAtomicChoiceForm.
	/// </summary>
	public class GAtomicChoiceForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region VARIABLES
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ListBox listBoxGames;

		public String selected_game;
		#endregion
		
		#region CONSTRUCTOR
		public GAtomicChoiceForm(String current_game)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			selected_game = "";
			
			String[] files = Directory.GetFiles (Directory.GetCurrentDirectory () + "\\games", "*.gagh");
			String[] game_names = new String [files.GetLength (0)];
			int index = 0;
			foreach (String file in files)
			{
				game_names[index] = file.Substring (file.LastIndexOf ("\\") + 1);
				game_names[index] = game_names[index].Substring (0, game_names[index].LastIndexOf ("."));
				index++;
			}

			foreach (String game_name in game_names)
				listBoxGames.Items.Add (game_name);

			listBoxGames.Focus ();

			for (int k = 0; k < listBoxGames.Items.Count; k++)
			{
				if ((String)listBoxGames.Items[k] == current_game)
				{
					listBoxGames.SelectedIndex = k;
					break;
				}
			}
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
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.listBoxGames = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(96, 232);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(88, 32);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(200, 232);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 32);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// listBoxGames
			// 
			this.listBoxGames.Location = new System.Drawing.Point(8, 8);
			this.listBoxGames.Name = "listBoxGames";
			this.listBoxGames.Size = new System.Drawing.Size(280, 199);
			this.listBoxGames.TabIndex = 2;
			this.listBoxGames.DoubleClick += new System.EventHandler(this.listBoxGames_DoubleClick);
			// 
			// GAtomicChoiceForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.listBoxGames);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.MaximizeBox = false;
			this.Name = "GAtomicChoiceForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "GAtomic - Choose new game";
			this.ResumeLayout(false);

		}
		#endregion

		#region EVENTS
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close ();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			selected_game =  (String)listBoxGames.SelectedItem;
			DialogResult = DialogResult.OK;
			this.Close ();
		}

		private void listBoxGames_DoubleClick(object sender, System.EventArgs e)
		{
			btnOK_Click (sender, e);
		}
		#endregion

		

	}
}
