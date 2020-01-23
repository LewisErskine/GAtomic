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
	/// Summary description for GAtomicFormulaEditorForm.
	/// </summary>
	public class GAtomicFormulaEditorForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region VARIABLES
		private Rectangle miniature_area_rect;
		private Array miniature_elements;

		private Rectangle object_creation_rect;

		private GAtomicElement gatomicelement;

		private ArrayList gatomicelement_connections;
		private GAtomicElementObject gatomicelementobject;

		private System.Windows.Forms.GroupBox groupBoxAtomicElement;
		private System.Windows.Forms.RadioButton groupBoxAtomicElementRadioHydrogen;
		private System.Windows.Forms.RadioButton groupBoxAtomicElementRadioCarbon;
		private System.Windows.Forms.RadioButton groupBoxAtomicElementRadioNitrogen;
		private System.Windows.Forms.RadioButton groupBoxAtomicElementRadioOxygen;
		private System.Windows.Forms.RadioButton groupBoxAtomicElementRadioSulfur;
		private System.Windows.Forms.GroupBox groupBoxConnections;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionTop;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionRight;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionLeft;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionBottom;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionTopLeft;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionBottomLeft;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionBottomRight;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionTopRight;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionDoubleLeft;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionDoubleBottom;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionDoubleRight;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionDoubleTop;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionTripleRight;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionTripleBottom;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionTripleLeft;
		private System.Windows.Forms.CheckBox groupBoxConnectionsDirectionTripleTop;
		private System.Windows.Forms.TextBox txtFormulaName;
		private System.Windows.Forms.Button btnSaveFormula;
		private System.Windows.Forms.Button btnLoadFormula;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.RadioButton groupBoxAtomicElementRadioPhosphorous;
		private System.Windows.Forms.RadioButton groupBoxAtomicElementRadioFluoride;
		private System.Windows.Forms.RadioButton groupBoxAtomicElementRadioHLine;
		private System.Windows.Forms.RadioButton groupBoxAtomicElementRadioVLine;
		private System.Windows.Forms.Button btnGetChemicalFormula;
		private System.Windows.Forms.RadioButton groupBoxAtomicElementRadioNeutral;
		private System.Windows.Forms.Button btnNewFormula;
		private	Rectangle gatomicelement_rect;
		#endregion

		#region CONSTRUCTOR
		public GAtomicFormulaEditorForm()
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

			_newFormula ();
	
			gatomicelementobject = null;
			gatomicelement_connections = new ArrayList ();
			groupBoxAtomicElementRadioHydrogen.Checked = true;
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
			this.groupBoxAtomicElement = new System.Windows.Forms.GroupBox();
			this.groupBoxAtomicElementRadioNeutral = new System.Windows.Forms.RadioButton();
			this.groupBoxAtomicElementRadioVLine = new System.Windows.Forms.RadioButton();
			this.groupBoxAtomicElementRadioHLine = new System.Windows.Forms.RadioButton();
			this.groupBoxAtomicElementRadioFluoride = new System.Windows.Forms.RadioButton();
			this.groupBoxAtomicElementRadioPhosphorous = new System.Windows.Forms.RadioButton();
			this.groupBoxAtomicElementRadioSulfur = new System.Windows.Forms.RadioButton();
			this.groupBoxAtomicElementRadioNitrogen = new System.Windows.Forms.RadioButton();
			this.groupBoxAtomicElementRadioOxygen = new System.Windows.Forms.RadioButton();
			this.groupBoxAtomicElementRadioCarbon = new System.Windows.Forms.RadioButton();
			this.groupBoxAtomicElementRadioHydrogen = new System.Windows.Forms.RadioButton();
			this.groupBoxConnections = new System.Windows.Forms.GroupBox();
			this.groupBoxConnectionsDirectionTripleRight = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionTripleBottom = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionTripleLeft = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionTripleTop = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionDoubleLeft = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionDoubleBottom = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionDoubleRight = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionDoubleTop = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionTopLeft = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionBottomLeft = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionBottomRight = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionTopRight = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionLeft = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionBottom = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionRight = new System.Windows.Forms.CheckBox();
			this.groupBoxConnectionsDirectionTop = new System.Windows.Forms.CheckBox();
			this.txtFormulaName = new System.Windows.Forms.TextBox();
			this.btnSaveFormula = new System.Windows.Forms.Button();
			this.btnLoadFormula = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnGetChemicalFormula = new System.Windows.Forms.Button();
			this.btnNewFormula = new System.Windows.Forms.Button();
			this.groupBoxAtomicElement.SuspendLayout();
			this.groupBoxConnections.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxAtomicElement
			// 
			this.groupBoxAtomicElement.Controls.Add(this.groupBoxAtomicElementRadioNeutral);
			this.groupBoxAtomicElement.Controls.Add(this.groupBoxAtomicElementRadioVLine);
			this.groupBoxAtomicElement.Controls.Add(this.groupBoxAtomicElementRadioHLine);
			this.groupBoxAtomicElement.Controls.Add(this.groupBoxAtomicElementRadioFluoride);
			this.groupBoxAtomicElement.Controls.Add(this.groupBoxAtomicElementRadioPhosphorous);
			this.groupBoxAtomicElement.Controls.Add(this.groupBoxAtomicElementRadioSulfur);
			this.groupBoxAtomicElement.Controls.Add(this.groupBoxAtomicElementRadioNitrogen);
			this.groupBoxAtomicElement.Controls.Add(this.groupBoxAtomicElementRadioOxygen);
			this.groupBoxAtomicElement.Controls.Add(this.groupBoxAtomicElementRadioCarbon);
			this.groupBoxAtomicElement.Controls.Add(this.groupBoxAtomicElementRadioHydrogen);
			this.groupBoxAtomicElement.Location = new System.Drawing.Point(40, 16);
			this.groupBoxAtomicElement.Name = "groupBoxAtomicElement";
			this.groupBoxAtomicElement.Size = new System.Drawing.Size(152, 336);
			this.groupBoxAtomicElement.TabIndex = 0;
			this.groupBoxAtomicElement.TabStop = false;
			this.groupBoxAtomicElement.Text = "Atomic Element";
			// 
			// groupBoxAtomicElementRadioNeutral
			// 
			this.groupBoxAtomicElementRadioNeutral.Location = new System.Drawing.Point(16, 248);
			this.groupBoxAtomicElementRadioNeutral.Name = "groupBoxAtomicElementRadioNeutral";
			this.groupBoxAtomicElementRadioNeutral.Size = new System.Drawing.Size(120, 16);
			this.groupBoxAtomicElementRadioNeutral.TabIndex = 9;
			this.groupBoxAtomicElementRadioNeutral.Text = "Neutral";
			this.groupBoxAtomicElementRadioNeutral.CheckedChanged += new System.EventHandler(this.groupBoxAtomicElement_CheckedChanged);
			// 
			// groupBoxAtomicElementRadioVLine
			// 
			this.groupBoxAtomicElementRadioVLine.Location = new System.Drawing.Point(16, 312);
			this.groupBoxAtomicElementRadioVLine.Name = "groupBoxAtomicElementRadioVLine";
			this.groupBoxAtomicElementRadioVLine.Size = new System.Drawing.Size(120, 16);
			this.groupBoxAtomicElementRadioVLine.TabIndex = 8;
			this.groupBoxAtomicElementRadioVLine.Text = "Vertical Line";
			this.groupBoxAtomicElementRadioVLine.CheckedChanged += new System.EventHandler(this.groupBoxAtomicElement_CheckedChanged);
			// 
			// groupBoxAtomicElementRadioHLine
			// 
			this.groupBoxAtomicElementRadioHLine.Location = new System.Drawing.Point(16, 280);
			this.groupBoxAtomicElementRadioHLine.Name = "groupBoxAtomicElementRadioHLine";
			this.groupBoxAtomicElementRadioHLine.Size = new System.Drawing.Size(120, 16);
			this.groupBoxAtomicElementRadioHLine.TabIndex = 7;
			this.groupBoxAtomicElementRadioHLine.Text = "Horizontal Line";
			this.groupBoxAtomicElementRadioHLine.CheckedChanged += new System.EventHandler(this.groupBoxAtomicElement_CheckedChanged);
			// 
			// groupBoxAtomicElementRadioFluoride
			// 
			this.groupBoxAtomicElementRadioFluoride.Location = new System.Drawing.Point(16, 216);
			this.groupBoxAtomicElementRadioFluoride.Name = "groupBoxAtomicElementRadioFluoride";
			this.groupBoxAtomicElementRadioFluoride.Size = new System.Drawing.Size(120, 16);
			this.groupBoxAtomicElementRadioFluoride.TabIndex = 6;
			this.groupBoxAtomicElementRadioFluoride.Text = "Fluoride";
			this.groupBoxAtomicElementRadioFluoride.CheckedChanged += new System.EventHandler(this.groupBoxAtomicElement_CheckedChanged);
			// 
			// groupBoxAtomicElementRadioPhosphorous
			// 
			this.groupBoxAtomicElementRadioPhosphorous.Location = new System.Drawing.Point(16, 184);
			this.groupBoxAtomicElementRadioPhosphorous.Name = "groupBoxAtomicElementRadioPhosphorous";
			this.groupBoxAtomicElementRadioPhosphorous.Size = new System.Drawing.Size(120, 16);
			this.groupBoxAtomicElementRadioPhosphorous.TabIndex = 5;
			this.groupBoxAtomicElementRadioPhosphorous.Text = "Phosphorous";
			this.groupBoxAtomicElementRadioPhosphorous.CheckedChanged += new System.EventHandler(this.groupBoxAtomicElement_CheckedChanged);
			// 
			// groupBoxAtomicElementRadioSulfur
			// 
			this.groupBoxAtomicElementRadioSulfur.Location = new System.Drawing.Point(16, 152);
			this.groupBoxAtomicElementRadioSulfur.Name = "groupBoxAtomicElementRadioSulfur";
			this.groupBoxAtomicElementRadioSulfur.Size = new System.Drawing.Size(120, 16);
			this.groupBoxAtomicElementRadioSulfur.TabIndex = 4;
			this.groupBoxAtomicElementRadioSulfur.Text = "Sulfur";
			this.groupBoxAtomicElementRadioSulfur.CheckedChanged += new System.EventHandler(this.groupBoxAtomicElement_CheckedChanged);
			// 
			// groupBoxAtomicElementRadioNitrogen
			// 
			this.groupBoxAtomicElementRadioNitrogen.Location = new System.Drawing.Point(16, 120);
			this.groupBoxAtomicElementRadioNitrogen.Name = "groupBoxAtomicElementRadioNitrogen";
			this.groupBoxAtomicElementRadioNitrogen.Size = new System.Drawing.Size(120, 16);
			this.groupBoxAtomicElementRadioNitrogen.TabIndex = 3;
			this.groupBoxAtomicElementRadioNitrogen.Text = "Nitrogen";
			this.groupBoxAtomicElementRadioNitrogen.CheckedChanged += new System.EventHandler(this.groupBoxAtomicElement_CheckedChanged);
			// 
			// groupBoxAtomicElementRadioOxygen
			// 
			this.groupBoxAtomicElementRadioOxygen.Location = new System.Drawing.Point(16, 88);
			this.groupBoxAtomicElementRadioOxygen.Name = "groupBoxAtomicElementRadioOxygen";
			this.groupBoxAtomicElementRadioOxygen.Size = new System.Drawing.Size(120, 16);
			this.groupBoxAtomicElementRadioOxygen.TabIndex = 2;
			this.groupBoxAtomicElementRadioOxygen.Text = "Oxygen";
			this.groupBoxAtomicElementRadioOxygen.CheckedChanged += new System.EventHandler(this.groupBoxAtomicElement_CheckedChanged);
			// 
			// groupBoxAtomicElementRadioCarbon
			// 
			this.groupBoxAtomicElementRadioCarbon.Location = new System.Drawing.Point(16, 56);
			this.groupBoxAtomicElementRadioCarbon.Name = "groupBoxAtomicElementRadioCarbon";
			this.groupBoxAtomicElementRadioCarbon.Size = new System.Drawing.Size(120, 16);
			this.groupBoxAtomicElementRadioCarbon.TabIndex = 1;
			this.groupBoxAtomicElementRadioCarbon.Text = "Carbon";
			this.groupBoxAtomicElementRadioCarbon.CheckedChanged += new System.EventHandler(this.groupBoxAtomicElement_CheckedChanged);
			// 
			// groupBoxAtomicElementRadioHydrogen
			// 
			this.groupBoxAtomicElementRadioHydrogen.Location = new System.Drawing.Point(16, 24);
			this.groupBoxAtomicElementRadioHydrogen.Name = "groupBoxAtomicElementRadioHydrogen";
			this.groupBoxAtomicElementRadioHydrogen.Size = new System.Drawing.Size(120, 16);
			this.groupBoxAtomicElementRadioHydrogen.TabIndex = 0;
			this.groupBoxAtomicElementRadioHydrogen.Text = "Hydrogen";
			this.groupBoxAtomicElementRadioHydrogen.CheckedChanged += new System.EventHandler(this.groupBoxAtomicElement_CheckedChanged);
			// 
			// groupBoxConnections
			// 
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionTripleRight);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionTripleBottom);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionTripleLeft);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionTripleTop);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionDoubleLeft);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionDoubleBottom);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionDoubleRight);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionDoubleTop);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionTopLeft);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionBottomLeft);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionBottomRight);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionTopRight);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionLeft);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionBottom);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionRight);
			this.groupBoxConnections.Controls.Add(this.groupBoxConnectionsDirectionTop);
			this.groupBoxConnections.Location = new System.Drawing.Point(240, 8);
			this.groupBoxConnections.Name = "groupBoxConnections";
			this.groupBoxConnections.Size = new System.Drawing.Size(240, 288);
			this.groupBoxConnections.TabIndex = 1;
			this.groupBoxConnections.TabStop = false;
			this.groupBoxConnections.Text = "Connections";
			// 
			// groupBoxConnectionsDirectionTripleRight
			// 
			this.groupBoxConnectionsDirectionTripleRight.Location = new System.Drawing.Point(128, 248);
			this.groupBoxConnectionsDirectionTripleRight.Name = "groupBoxConnectionsDirectionTripleRight";
			this.groupBoxConnectionsDirectionTripleRight.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionTripleRight.TabIndex = 15;
			this.groupBoxConnectionsDirectionTripleRight.Text = "Triple Right";
			this.groupBoxConnectionsDirectionTripleRight.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionTripleBottom
			// 
			this.groupBoxConnectionsDirectionTripleBottom.Location = new System.Drawing.Point(128, 216);
			this.groupBoxConnectionsDirectionTripleBottom.Name = "groupBoxConnectionsDirectionTripleBottom";
			this.groupBoxConnectionsDirectionTripleBottom.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionTripleBottom.TabIndex = 14;
			this.groupBoxConnectionsDirectionTripleBottom.Text = "Triple Bottom";
			this.groupBoxConnectionsDirectionTripleBottom.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionTripleLeft
			// 
			this.groupBoxConnectionsDirectionTripleLeft.Location = new System.Drawing.Point(128, 184);
			this.groupBoxConnectionsDirectionTripleLeft.Name = "groupBoxConnectionsDirectionTripleLeft";
			this.groupBoxConnectionsDirectionTripleLeft.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionTripleLeft.TabIndex = 13;
			this.groupBoxConnectionsDirectionTripleLeft.Text = "Tripple Left";
			this.groupBoxConnectionsDirectionTripleLeft.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionTripleTop
			// 
			this.groupBoxConnectionsDirectionTripleTop.Location = new System.Drawing.Point(128, 152);
			this.groupBoxConnectionsDirectionTripleTop.Name = "groupBoxConnectionsDirectionTripleTop";
			this.groupBoxConnectionsDirectionTripleTop.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionTripleTop.TabIndex = 12;
			this.groupBoxConnectionsDirectionTripleTop.Text = "Triple Top";
			this.groupBoxConnectionsDirectionTripleTop.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionDoubleLeft
			// 
			this.groupBoxConnectionsDirectionDoubleLeft.Location = new System.Drawing.Point(128, 120);
			this.groupBoxConnectionsDirectionDoubleLeft.Name = "groupBoxConnectionsDirectionDoubleLeft";
			this.groupBoxConnectionsDirectionDoubleLeft.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionDoubleLeft.TabIndex = 11;
			this.groupBoxConnectionsDirectionDoubleLeft.Text = "Double Left";
			this.groupBoxConnectionsDirectionDoubleLeft.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionDoubleBottom
			// 
			this.groupBoxConnectionsDirectionDoubleBottom.Location = new System.Drawing.Point(128, 88);
			this.groupBoxConnectionsDirectionDoubleBottom.Name = "groupBoxConnectionsDirectionDoubleBottom";
			this.groupBoxConnectionsDirectionDoubleBottom.Size = new System.Drawing.Size(104, 16);
			this.groupBoxConnectionsDirectionDoubleBottom.TabIndex = 10;
			this.groupBoxConnectionsDirectionDoubleBottom.Text = "Double Bottom";
			this.groupBoxConnectionsDirectionDoubleBottom.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionDoubleRight
			// 
			this.groupBoxConnectionsDirectionDoubleRight.Location = new System.Drawing.Point(128, 56);
			this.groupBoxConnectionsDirectionDoubleRight.Name = "groupBoxConnectionsDirectionDoubleRight";
			this.groupBoxConnectionsDirectionDoubleRight.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionDoubleRight.TabIndex = 9;
			this.groupBoxConnectionsDirectionDoubleRight.Text = "Double Right";
			this.groupBoxConnectionsDirectionDoubleRight.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionDoubleTop
			// 
			this.groupBoxConnectionsDirectionDoubleTop.Location = new System.Drawing.Point(128, 24);
			this.groupBoxConnectionsDirectionDoubleTop.Name = "groupBoxConnectionsDirectionDoubleTop";
			this.groupBoxConnectionsDirectionDoubleTop.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionDoubleTop.TabIndex = 8;
			this.groupBoxConnectionsDirectionDoubleTop.Text = "Double Top";
			this.groupBoxConnectionsDirectionDoubleTop.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionTopLeft
			// 
			this.groupBoxConnectionsDirectionTopLeft.Location = new System.Drawing.Point(16, 248);
			this.groupBoxConnectionsDirectionTopLeft.Name = "groupBoxConnectionsDirectionTopLeft";
			this.groupBoxConnectionsDirectionTopLeft.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionTopLeft.TabIndex = 7;
			this.groupBoxConnectionsDirectionTopLeft.Text = "Top Left";
			this.groupBoxConnectionsDirectionTopLeft.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionBottomLeft
			// 
			this.groupBoxConnectionsDirectionBottomLeft.Location = new System.Drawing.Point(16, 216);
			this.groupBoxConnectionsDirectionBottomLeft.Name = "groupBoxConnectionsDirectionBottomLeft";
			this.groupBoxConnectionsDirectionBottomLeft.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionBottomLeft.TabIndex = 6;
			this.groupBoxConnectionsDirectionBottomLeft.Text = "Bottom Left";
			this.groupBoxConnectionsDirectionBottomLeft.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionBottomRight
			// 
			this.groupBoxConnectionsDirectionBottomRight.Location = new System.Drawing.Point(16, 184);
			this.groupBoxConnectionsDirectionBottomRight.Name = "groupBoxConnectionsDirectionBottomRight";
			this.groupBoxConnectionsDirectionBottomRight.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionBottomRight.TabIndex = 5;
			this.groupBoxConnectionsDirectionBottomRight.Text = "Bottom Right";
			this.groupBoxConnectionsDirectionBottomRight.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionTopRight
			// 
			this.groupBoxConnectionsDirectionTopRight.Location = new System.Drawing.Point(16, 152);
			this.groupBoxConnectionsDirectionTopRight.Name = "groupBoxConnectionsDirectionTopRight";
			this.groupBoxConnectionsDirectionTopRight.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionTopRight.TabIndex = 4;
			this.groupBoxConnectionsDirectionTopRight.Text = "Top Right";
			this.groupBoxConnectionsDirectionTopRight.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionLeft
			// 
			this.groupBoxConnectionsDirectionLeft.Location = new System.Drawing.Point(16, 120);
			this.groupBoxConnectionsDirectionLeft.Name = "groupBoxConnectionsDirectionLeft";
			this.groupBoxConnectionsDirectionLeft.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionLeft.TabIndex = 3;
			this.groupBoxConnectionsDirectionLeft.Text = "Left";
			this.groupBoxConnectionsDirectionLeft.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionBottom
			// 
			this.groupBoxConnectionsDirectionBottom.Location = new System.Drawing.Point(16, 88);
			this.groupBoxConnectionsDirectionBottom.Name = "groupBoxConnectionsDirectionBottom";
			this.groupBoxConnectionsDirectionBottom.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionBottom.TabIndex = 2;
			this.groupBoxConnectionsDirectionBottom.Text = "Bottom";
			this.groupBoxConnectionsDirectionBottom.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionRight
			// 
			this.groupBoxConnectionsDirectionRight.Location = new System.Drawing.Point(16, 56);
			this.groupBoxConnectionsDirectionRight.Name = "groupBoxConnectionsDirectionRight";
			this.groupBoxConnectionsDirectionRight.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionRight.TabIndex = 1;
			this.groupBoxConnectionsDirectionRight.Text = "Right";
			this.groupBoxConnectionsDirectionRight.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// groupBoxConnectionsDirectionTop
			// 
			this.groupBoxConnectionsDirectionTop.Location = new System.Drawing.Point(16, 24);
			this.groupBoxConnectionsDirectionTop.Name = "groupBoxConnectionsDirectionTop";
			this.groupBoxConnectionsDirectionTop.Size = new System.Drawing.Size(96, 16);
			this.groupBoxConnectionsDirectionTop.TabIndex = 0;
			this.groupBoxConnectionsDirectionTop.Text = "Top";
			this.groupBoxConnectionsDirectionTop.CheckedChanged += new System.EventHandler(this.groupBoxConnectionsDirection_CheckedChanged);
			// 
			// txtFormulaName
			// 
			this.txtFormulaName.Location = new System.Drawing.Point(40, 416);
			this.txtFormulaName.Name = "txtFormulaName";
			this.txtFormulaName.Size = new System.Drawing.Size(160, 20);
			this.txtFormulaName.TabIndex = 2;
			this.txtFormulaName.Text = "FormulaName";
			// 
			// btnSaveFormula
			// 
			this.btnSaveFormula.Location = new System.Drawing.Point(216, 416);
			this.btnSaveFormula.Name = "btnSaveFormula";
			this.btnSaveFormula.Size = new System.Drawing.Size(96, 24);
			this.btnSaveFormula.TabIndex = 3;
			this.btnSaveFormula.Text = "Save";
			this.btnSaveFormula.Click += new System.EventHandler(this.btnSaveFormula_Click);
			// 
			// btnLoadFormula
			// 
			this.btnLoadFormula.Location = new System.Drawing.Point(360, 416);
			this.btnLoadFormula.Name = "btnLoadFormula";
			this.btnLoadFormula.Size = new System.Drawing.Size(88, 24);
			this.btnLoadFormula.TabIndex = 4;
			this.btnLoadFormula.Text = "Load";
			this.btnLoadFormula.Click += new System.EventHandler(this.btnLoadFormula_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(480, 416);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(64, 24);
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "Quit";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnGetChemicalFormula
			// 
			this.btnGetChemicalFormula.Location = new System.Drawing.Point(320, 416);
			this.btnGetChemicalFormula.Name = "btnGetChemicalFormula";
			this.btnGetChemicalFormula.Size = new System.Drawing.Size(32, 24);
			this.btnGetChemicalFormula.TabIndex = 6;
			this.btnGetChemicalFormula.Text = "CF";
			this.btnGetChemicalFormula.Click += new System.EventHandler(this.btnGetChemicalFormula_Click);
			// 
			// btnNewFormula
			// 
			this.btnNewFormula.Location = new System.Drawing.Point(224, 368);
			this.btnNewFormula.Name = "btnNewFormula";
			this.btnNewFormula.Size = new System.Drawing.Size(96, 24);
			this.btnNewFormula.TabIndex = 7;
			this.btnNewFormula.Text = "Clear";
			this.btnNewFormula.Click += new System.EventHandler(this.btnNewFormula_Click);
			// 
			// GAtomicFormulaEditorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 453);
			this.Controls.Add(this.btnNewFormula);
			this.Controls.Add(this.btnGetChemicalFormula);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnLoadFormula);
			this.Controls.Add(this.btnSaveFormula);
			this.Controls.Add(this.txtFormulaName);
			this.Controls.Add(this.groupBoxConnections);
			this.Controls.Add(this.groupBoxAtomicElement);
			this.MaximizeBox = false;
			this.Name = "GAtomicFormulaEditorForm";
			this.Text = "GAtomicFormulaEditor";
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GAtomicEditorForm_MouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.GAtomicEditorForm_Paint);
			this.groupBoxAtomicElement.ResumeLayout(false);
			this.groupBoxConnections.ResumeLayout(false);
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
			Application.Run(new GAtomicFormulaEditorForm());
		}
		#endregion

		#region GAtomicEditorForm_Paint
		private void GAtomicEditorForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (miniature_elements == null)
				return;

			object o = null;
			for (int row = 0; row < miniature_elements.GetLength (0); row++)
			{
				for (int col = 0; col < miniature_elements.GetLength (1); col++)
				{
					o = miniature_elements.GetValue (row, col);
					if ((o != null) && (o is GAtomicObject))
						((GAtomicObject)o)._drawWithGrid (e.Graphics);
				}
			}

			if (gatomicelementobject != null)
				gatomicelementobject._drawWithGrid (e.Graphics);

			//e.Graphics.FillRectangle (new SolidBrush (Color.Red), object_creation_rect.Left, object_creation_rect.Top, object_creation_rect.Width, object_creation_rect.Height);
		}
		#endregion

		#region _newFormula
		private void _newFormula ()
		{
			// miniature area 20 by 20
			miniature_elements = Array.CreateInstance (typeof(GAtomicObject), 20, 20);

			GAtomicConfiguration._setPlayingAreaAtomicObjectSize (30);

			miniature_area_rect = new Rectangle (50, 50, 100, 100);

			Point miniature_top_left = new Point (miniature_area_rect.Left, miniature_area_rect.Top);

			miniature_area_rect.Width = miniature_elements.GetLength (0) * GAtomicConfiguration._getPlayingAreaAtomicObjectSize ();
			miniature_area_rect.Height = miniature_elements.GetLength (1) * GAtomicConfiguration._getPlayingAreaAtomicObjectSize ();

			object_creation_rect = new Rectangle (miniature_area_rect.Left + miniature_area_rect.Width + 50, 0, 250, miniature_area_rect.Height + 2 * miniature_area_rect.Top);
			
			groupBoxAtomicElement.Location = new Point (object_creation_rect.Left, object_creation_rect.Top + 10);

			groupBoxConnections.Location = new Point (object_creation_rect.Left, groupBoxAtomicElement.Bottom + 10);

			txtFormulaName.Location = new Point (miniature_area_rect.Left + (miniature_area_rect.Width - txtFormulaName.Width) / 2, 10);

			btnNewFormula.Location = new Point (txtFormulaName.Left - btnNewFormula.Width - 10, txtFormulaName.Top);

			btnSaveFormula.Location = new Point (txtFormulaName.Right + 10, txtFormulaName.Location.Y);

			btnGetChemicalFormula.Location = new Point (btnSaveFormula.Right + 20, btnSaveFormula.Location.Y);

			btnClose.Location = new Point (object_creation_rect.Right - btnClose.Width - 10, object_creation_rect.Bottom - 50);

			this.ClientSize = new Size (object_creation_rect.Left + object_creation_rect.Width, object_creation_rect.Height);

			//gatomicelement_rect = new Rectangle (object_creation_rect.Left + (object_creation_rect.Width - GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()) / 2
			//									,object_creation_rect.Top + (object_creation_rect.Height - GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()) / 2
			//									,GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()
			//									,GAtomicConfiguration._getPlayingAreaAtomicObjectSize ());

			//gatomicelement_rect = new Rectangle (object_creation_rect.Left + 10
			//									,groupBoxConnections.Bottom + 10
			//									,GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()
			//									,GAtomicConfiguration._getPlayingAreaAtomicObjectSize ());
			gatomicelement_rect = new Rectangle (groupBoxAtomicElement.Right + 10
				,groupBoxAtomicElement.Bottom - 50
				,GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()
				,GAtomicConfiguration._getPlayingAreaAtomicObjectSize ());

			btnLoadFormula.Location = new Point (groupBoxAtomicElement.Right + 5, groupBoxAtomicElement.Top);
			
			int row = 0;
			int col = 0;
			// top
			for (row = 0; row < miniature_elements.GetLength (0); row++)
			{
				for (col = 0; col < miniature_elements.GetLength (1); col++)
				{
					miniature_elements.SetValue (new GAtomicObject (miniature_top_left, row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col);
				}
			}

			txtFormulaName.Text = "FormulaName";

			this.Invalidate ();
		}
		#endregion

		#region groupBoxAtomicElement_CheckedChanged
		private void groupBoxAtomicElement_CheckedChanged(object sender, System.EventArgs e)
		{
			_newAtomicElementChecked ();		
		}
		#endregion

		#region _changeAtomicElement
		private void _changeAtomicElement ()
		{
			if (gatomicelement == GAtomicElement.VERTICAL_LINE_LINK)
			{
				gatomicelement_connections.Clear ();
				gatomicelement_connections.Add (GAtomicConnectionDirection.TOP);
				gatomicelement_connections.Add (GAtomicConnectionDirection.BOTTOM);
			}
			else if (gatomicelement == GAtomicElement.HORIZONTAL_LINE_LINK)
			{
				gatomicelement_connections.Clear ();
				gatomicelement_connections.Add (GAtomicConnectionDirection.RIGHT);
				gatomicelement_connections.Add (GAtomicConnectionDirection.LEFT);
			}

			if (gatomicelement_connections.Count == 0)
				gatomicelementobject = new GAtomicElementObject (new Point (gatomicelement_rect.Left, gatomicelement_rect.Top), 0, 0, GAtomicConfiguration._getPlayingAreaAtomicObjectSize (), gatomicelement, null, 0);
			else
			{
				GAtomicConnectionElement[] connection_elements = new GAtomicConnectionElement[gatomicelement_connections.Count];
				for (int k = 0; k < gatomicelement_connections.Count; k++)
				{
					connection_elements[k] = new GAtomicConnectionElement (null, (GAtomicConnectionDirection)gatomicelement_connections[k]);
				}
				gatomicelementobject = new GAtomicElementObject (new Point (gatomicelement_rect.Left, gatomicelement_rect.Top), 0, 0, GAtomicConfiguration._getPlayingAreaAtomicObjectSize (), gatomicelement, connection_elements, 0);
			}

			this.Invalidate (gatomicelement_rect);

			if ((gatomicelement == GAtomicElement.VERTICAL_LINE_LINK) || (gatomicelement == GAtomicElement.HORIZONTAL_LINE_LINK))
			{
				gatomicelement_connections.Clear ();
			}
		}
		#endregion

		#region _newAtomicElementChecked
		private void _newAtomicElementChecked ()
		{
			for (int k = 15; k > -1; k--)
			{
				groupBoxConnections.Controls[k].Enabled = true;
			}

			if (groupBoxAtomicElementRadioHydrogen.Checked)
			{
				gatomicelement = GAtomicElement.HYDROGEN;
				/*for (int k = 7; k > -1; k--)
				{
					groupBoxConnections.Controls[k].Enabled = false;
				}*/
			}
			else if (groupBoxAtomicElementRadioCarbon.Checked)
				gatomicelement = GAtomicElement.CARBON;
			else if (groupBoxAtomicElementRadioOxygen.Checked)
			{
				gatomicelement = GAtomicElement.OXYGEN;
				/*for (int k = 3; k > -1; k--)
				{
					groupBoxConnections.Controls[k].Enabled = false;
				}*/
			}
			else if (groupBoxAtomicElementRadioNitrogen.Checked)
				gatomicelement = GAtomicElement.NITROGEN;
			else if (groupBoxAtomicElementRadioSulfur.Checked)
				gatomicelement = GAtomicElement.SULFUR;
			else if (groupBoxAtomicElementRadioPhosphorous.Checked)
				gatomicelement = GAtomicElement.PHOSPHORUS;
			else if (groupBoxAtomicElementRadioFluoride.Checked)
				gatomicelement = GAtomicElement.FLUORIDE;
			else if (groupBoxAtomicElementRadioNeutral.Checked)
				gatomicelement = GAtomicElement.NEUTRAL;
			else if (groupBoxAtomicElementRadioVLine.Checked)
			{
				gatomicelement = GAtomicElement.VERTICAL_LINE_LINK;
				for (int k = 15; k > -1; k--)
				{
					groupBoxConnections.Controls[k].Enabled = false;
				}
			}
			else if (groupBoxAtomicElementRadioHLine.Checked)
			{
				gatomicelement = GAtomicElement.HORIZONTAL_LINE_LINK;
				for (int k = 15; k > -1; k--)
				{
					groupBoxConnections.Controls[k].Enabled = false;
				}
			}

			_changeAtomicElement ();
		}
		#endregion

		#region groupBoxConnectionsDirection_CheckedChanged
		private void groupBoxConnectionsDirection_CheckedChanged(object sender, System.EventArgs e)
		{
			gatomicelement_connections.Clear ();
			
			if (groupBoxConnectionsDirectionTop.Enabled && groupBoxConnectionsDirectionTop.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.TOP);
			}
			if (groupBoxConnectionsDirectionRight.Enabled && groupBoxConnectionsDirectionRight.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.RIGHT);
			}
			if (groupBoxConnectionsDirectionBottom.Enabled && groupBoxConnectionsDirectionBottom.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.BOTTOM);
			}
			if (groupBoxConnectionsDirectionLeft.Enabled && groupBoxConnectionsDirectionLeft.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.LEFT);
			}
			if (groupBoxConnectionsDirectionTopRight.Enabled && groupBoxConnectionsDirectionTopRight.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.TOP_RIGHT);
			}
			if (groupBoxConnectionsDirectionBottomRight.Enabled && groupBoxConnectionsDirectionBottomRight.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.BOTTOM_RIGHT);
			}
			if (groupBoxConnectionsDirectionBottomLeft.Enabled && groupBoxConnectionsDirectionBottomLeft.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.BOTTOM_LEFT);
			}
			if (groupBoxConnectionsDirectionTopLeft.Enabled && groupBoxConnectionsDirectionTopLeft.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.TOP_LEFT);
			}
			if (groupBoxConnectionsDirectionDoubleTop.Enabled && groupBoxConnectionsDirectionDoubleTop.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.DOUBLE_TOP);
			}
			if (groupBoxConnectionsDirectionDoubleRight.Enabled && groupBoxConnectionsDirectionDoubleRight.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.DOUBLE_RIGHT);
			}
			if (groupBoxConnectionsDirectionDoubleBottom.Enabled && groupBoxConnectionsDirectionDoubleBottom.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.DOUBLE_BOTTOM);
			}
			if (groupBoxConnectionsDirectionDoubleLeft.Enabled && groupBoxConnectionsDirectionDoubleLeft.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.DOUBLE_LEFT);
			}
			if (groupBoxConnectionsDirectionTripleTop.Enabled && groupBoxConnectionsDirectionTripleTop.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.TRIPLE_TOP);
			}
			if (groupBoxConnectionsDirectionTripleRight.Enabled && groupBoxConnectionsDirectionTripleRight.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.TRIPLE_RIGHT);
			}
			if (groupBoxConnectionsDirectionTripleBottom.Enabled && groupBoxConnectionsDirectionTripleBottom.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.TRIPLE_BOTTOM);
			}
			if (groupBoxConnectionsDirectionTripleLeft.Enabled && groupBoxConnectionsDirectionTripleLeft.Checked)
			{
				gatomicelement_connections.Add (GAtomicConnectionDirection.TRIPLE_LEFT);
			}
			
			_changeAtomicElement ();
		}
		#endregion

		#region btnSaveFormula_Click
		private void btnSaveFormula_Click(object sender, System.EventArgs e)
		{
			if (txtFormulaName.Text == "")
				return;

			// Search for last empty row before formula, first empty row after formula
			// same for col
			int last_empty_row_before_formula = -1;
			int first_empty_row_after_formula = -1;
			int row = 0;
            for (; row < miniature_elements.GetLength (0); row++)
			{
				if (!_isEmptyRow (row))
				{
					break;
				}
			}
			
			last_empty_row_before_formula = row - 1;

			for (; row < miniature_elements.GetLength (0); row++)
			{
				if (_isEmptyRow (row))
				{
					break;
				}
			}

			first_empty_row_after_formula = row;

			// formula from last_empty_row_before_formula + 1 to first_empty_row_after_formula - 1
			int formula_number_of_rows = first_empty_row_after_formula - last_empty_row_before_formula - 1;

			int last_empty_col_before_formula = -1;
			int first_empty_col_after_formula = -1;
			int col = 0;
			for (; col < miniature_elements.GetLength (0); col++)
			{
				if (!_isEmptyCol (col))
				{
					break;
				}
			}
			
			last_empty_col_before_formula = col - 1;

			for (; col < miniature_elements.GetLength (0); col++)
			{
				if (_isEmptyCol (col))
				{
					break;
				}
			}

			first_empty_col_after_formula = col;

			// formula from last_empty_col_before_formula + 1 to first_empty_col_after_formula - 1
			int formula_number_of_cols = first_empty_col_after_formula - last_empty_col_before_formula - 1;

			bool file_exists = File.Exists (Directory.GetCurrentDirectory () + "/formulas/" + txtFormulaName.Text + ".gaf");
			if (file_exists)
			{
				// Overwrite file
				if (MessageBox.Show ("Formula with same name already exist. Overwrite?", "Formula exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
				{
					return;
				}
				File.Delete (Directory.GetCurrentDirectory () + "/formulas/" + txtFormulaName.Text + ".gaf");
			}
			// Save to file
			String string_to_write_into_file = "";
			// Save file name with formula name
			GLDoorsFileOperations file_operations = new GLDoorsFileOperations (Directory.GetCurrentDirectory () + "/formulas/" + txtFormulaName.Text + ".gaf");
			file_operations.CreateFile ();
			// Save line, formula name
			string_to_write_into_file = txtFormulaName.Text + "\r\n";
			file_operations.WriteStringToFile (string_to_write_into_file);
			// Save line, formula_number_of_rows x formula_number_of_cols
			string_to_write_into_file = String.Format ("{0}x{1}\r\n", formula_number_of_rows, formula_number_of_cols);
			file_operations.WriteStringToFile (string_to_write_into_file);
			// Save elements by rows, EMPTY for no element
			object o = null;
            for (row = last_empty_row_before_formula + 1; row < first_empty_row_after_formula; row++)
			{
				for (col = last_empty_col_before_formula + 1; col < first_empty_col_after_formula; col++)
				{
					 o = miniature_elements.GetValue (row, col);
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

			file_operations.CloseFile ();

			MessageBox.Show ("Formula '" + txtFormulaName.Text + "' saved.", "Formula saved");

		}

		private bool _isEmptyRow (int row)
		{
			object o = null;
			for (int col = 0; col < miniature_elements.GetLength (1); col++)
			{
				o = miniature_elements.GetValue (row, col);
				if (o is GAtomicElementObject)
					return false;
			}

			return true;
		}

		private bool _isEmptyCol (int col)
		{
			object o = null;
			for (int row = 0; row < miniature_elements.GetLength (0); row++)
			{
				o = miniature_elements.GetValue (row, col);
				if (o is GAtomicElementObject)
					return false;
			}

			return true;
		}
		#endregion

		#region GAtomicEditorForm_MouseUp
		private void GAtomicEditorForm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point p = new Point (e.X - miniature_area_rect.Left, e.Y - miniature_area_rect.Top);

			if ((p.X < 0) || (p.Y < 0))
				return;

			if ((p.X > miniature_area_rect.Width) || (p.Y > miniature_area_rect.Height))
				return;

			int row = p.Y / GAtomic.objects.GAtomicConfiguration._getPlayingAreaAtomicObjectSize ();
			int col = p.X / GAtomic.objects.GAtomicConfiguration._getPlayingAreaAtomicObjectSize ();

			object selected_object = miniature_elements.GetValue (row, col);

			if (selected_object == null)
				return;

			if (selected_object is GAtomicElementObject)
			{
				// modify, replace, delete?
				if (e.Button == MouseButtons.Right)
				{
					// Delete
					miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col);
					this.Invalidate (miniature_area_rect);
				}
			}
			else if (selected_object is GAtomicObject)
			{
				// Add the current
				GAtomicConnectionElement[] connection_elements = new GAtomicConnectionElement[gatomicelement_connections.Count];
				for (int k = 0; k < gatomicelement_connections.Count; k++)
				{
					connection_elements[k] = new GAtomicConnectionElement (null, (GAtomicConnectionDirection)gatomicelement_connections[k]);
				}
				GAtomicElementObject a = new GAtomicElementObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col
									, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()
									, gatomicelementobject._getAtomicElement (), connection_elements, 0);
				miniature_elements.SetValue (a, row, col);
				this.Invalidate (miniature_area_rect);
			}


		}
		#endregion

		#region btnLoadFormula_Click
		private void btnLoadFormula_Click(object sender, System.EventArgs e)
		{
			GAtomicLoadFormulaForm load_formula_form = new GAtomicLoadFormulaForm ();
			if (load_formula_form.ShowDialog () == DialogResult.Cancel)
				return;
			
			if (load_formula_form.selected_formula == "")
				return;

			String open_file_name = Directory.GetCurrentDirectory () + "/formulas/" + load_formula_form.selected_formula + ".gaf";
			
			_newFormula ();

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
				// Read size
				file_operations.ReadStringLineFromFile (out s, out b);
				String sizes_string = s;
				int rows = Convert.ToInt32 (s.Substring (0, s.IndexOf ("x")));
				int cols = Convert.ToInt32 (s.Substring (s.IndexOf ("x") + 1));

				// now, we know number of rows and cols, where to draw it?
				// in middle
				int rows_remaining = miniature_elements.GetLength (0) - rows;
				int rows_remaining_over_2 = rows_remaining / 2;
				int start_row = rows_remaining_over_2;

				int cols_remaining = miniature_elements.GetLength (0) - cols;
				int cols_remaining_over_2 = cols_remaining / 2;
				int start_col = cols_remaining_over_2;
			
				for (int row = start_row; row < rows + start_row; row++)
				{
					for (int col = start_col; col < cols + start_col; col++)
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
							miniature_elements.SetValue (new GAtomicObject (new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize ()), row, col); 
						}
						else
						{
							// Read and add
							object o = GAtomicElementObject._getAtomicElementFromString (s, new Point (miniature_area_rect.Left, miniature_area_rect.Top), row, col, GAtomicConfiguration._getPlayingAreaAtomicObjectSize (), 0);
							if (o != null)
							{
								miniature_elements.SetValue ((GAtomicElementObject)o, row, col); 
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

			this.Invalidate (miniature_area_rect);

			if (!error)
			{
				txtFormulaName.Text = load_formula_form.selected_formula;
				MessageBox.Show ("Formula '" + load_formula_form.selected_formula + "' (" + _calculateChemicalFormula () + ") loaded.", "Formula loaded");
			}
			else
				MessageBox.Show ("Formula '" + load_formula_form.selected_formula + "' not loaded.", "Formula loaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		#endregion

		#region btnClose_Click
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show ("Quit? All none saved data will be lost.", "Quit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
			{
				return;
			}
			
			this.Close ();
		}
		#endregion

		#region _calculateChemicalFormula
		private String _calculateChemicalFormula ()
		{
            int[] one_element_number = new int[(int)GAtomicElement.VERTICAL_LINE_LINK];
			int k = 0;
			for (k = 0; k < (int)GAtomicElement.VERTICAL_LINE_LINK; k++)
			{
				one_element_number[k] = 0;
			}

			String formula = "";
			object o = null;
			for (int row = 0; row < miniature_elements.GetLength (0); row++)
			{
				for (int col = 0; col < miniature_elements.GetLength (1); col++)
				{
					o = miniature_elements.GetValue (row, col);
					if ((o != null) && (o is GAtomicElementObject) && (((GAtomicElementObject)o)._getAtomicElement () < GAtomicElement.VERTICAL_LINE_LINK))
					{
						one_element_number[(int)((GAtomicElementObject)o)._getAtomicElement ()]++;
					}
				}
			}

			GAtomicElementObject elem = null;
			for (k = 0; k < (int)GAtomicElement.VERTICAL_LINE_LINK; k++)
			{
				if (one_element_number[k] > 0)
				{
					elem = new GAtomicElementObject ((GAtomicElement)k, null);
                    formula += String.Format ("{0}", elem._getElementLetter ());
					if (one_element_number[k] > 1)
						formula += String.Format ("{0}", one_element_number[k]);
				}
			}

			return formula;
		}

		private void btnGetChemicalFormula_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show ("Chemical Formula = " + _calculateChemicalFormula () + "", "Chemical Formula.");
		}
		#endregion

		#region btnNewFormula_Click
		private void btnNewFormula_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show ("Clear all? All none saved data will be lost.", "Clear", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
			{
				return;
			}

			_newFormula ();
		}
		#endregion

		
	}
}

