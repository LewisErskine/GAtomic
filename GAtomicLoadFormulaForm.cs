using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace GLDoors2.GAtomic
{
	/// <summary>
	/// Summary description for GAtomicLoadFormulaForm.
	/// </summary>
	public class GAtomicLoadFormulaForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		
		#region VARIABLES
		private System.ComponentModel.Container components = null;
		
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.ListBox listBoxFormulas;
		private System.Windows.Forms.Button btnSelectFormula;
		private System.Windows.Forms.Button btnCancel;

		public String selected_formula;
		#endregion
		
		#region CONSTRUCTOR
		public GAtomicLoadFormulaForm()
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
			this.panelBottom = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSelectFormula = new System.Windows.Forms.Button();
			this.listBoxFormulas = new System.Windows.Forms.ListBox();
			this.panelBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelBottom
			// 
			this.panelBottom.Controls.Add(this.btnCancel);
			this.panelBottom.Controls.Add(this.btnSelectFormula);
			this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBottom.Location = new System.Drawing.Point(0, 299);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(292, 50);
			this.panelBottom.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(216, 8);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 32);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSelectFormula
			// 
			this.btnSelectFormula.Location = new System.Drawing.Point(88, 8);
			this.btnSelectFormula.Name = "btnSelectFormula";
			this.btnSelectFormula.Size = new System.Drawing.Size(96, 32);
			this.btnSelectFormula.TabIndex = 0;
			this.btnSelectFormula.Text = "Select";
			this.btnSelectFormula.Click += new System.EventHandler(this.btnSelectFormula_Click);
			// 
			// listBoxFormulas
			// 
			this.listBoxFormulas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxFormulas.Location = new System.Drawing.Point(0, 0);
			this.listBoxFormulas.Name = "listBoxFormulas";
			this.listBoxFormulas.Size = new System.Drawing.Size(292, 290);
			this.listBoxFormulas.TabIndex = 1;
			this.listBoxFormulas.DoubleClick += new System.EventHandler(this.btnSelectFormula_Click);
			// 
			// GAtomicLoadFormulaForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 349);
			this.Controls.Add(this.listBoxFormulas);
			this.Controls.Add(this.panelBottom);
			this.MaximizeBox = false;
			this.Name = "GAtomicLoadFormulaForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "GAtomicLoadFormula";
			this.panelBottom.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Events
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close ();
		}

		private void btnSelectFormula_Click(object sender, System.EventArgs e)
		{
			selected_formula =  (String)listBoxFormulas.SelectedItem;
			DialogResult = DialogResult.OK;
			this.Close ();
		}
		#endregion
	}
}
