namespace Sigesoft.Node.WinClient.UI.Configuration
{
    partial class frmProtocolPlanAseguradora
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NombreLinea");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EsDeducible");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EsCoaseguro");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_Importe", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_ImporteCo");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProtocolPlanAseguradora));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cboEmpresa = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.grd = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.btnEliminar = new Infragistics.Win.Misc.UltraButton();
            this.btnNuevo = new Infragistics.Win.Misc.UltraButton();
            this.btnSalir = new Infragistics.Win.Misc.UltraButton();
            this.btnGuardar = new Infragistics.Win.Misc.UltraButton();
            this.btnAgregarLinea = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ultraGroupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grd, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ultraPanel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(646, 446);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Controls.Add(this.btnAgregarLinea);
            this.ultraGroupBox1.Controls.Add(this.cboEmpresa);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel1);
            this.ultraGroupBox1.Controls.Add(this.shapeContainer1);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(640, 65);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.Text = "Empresa Aseguradora:";
            // 
            // cboEmpresa
            // 
            this.cboEmpresa.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            this.cboEmpresa.DropDownResizeHandleStyle = Infragistics.Win.DropDownResizeHandleStyle.DiagonalResize;
            this.cboEmpresa.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            this.cboEmpresa.Location = new System.Drawing.Point(66, 26);
            this.cboEmpresa.Name = "cboEmpresa";
            this.cboEmpresa.Size = new System.Drawing.Size(442, 21);
            this.cboEmpresa.TabIndex = 1;
            this.cboEmpresa.ValueChanged += new System.EventHandler(this.cboEmpresa_ValueChanged);
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.AutoSize = true;
            this.ultraLabel1.Location = new System.Drawing.Point(11, 30);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(49, 14);
            this.ultraLabel1.TabIndex = 0;
            this.ultraLabel1.Text = "Empresa";
            // 
            // grd
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grd.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            appearance2.Image = ((object)(resources.GetObject("appearance2.Image")));
            ultraGridColumn1.CellButtonAppearance = appearance2;
            ultraGridColumn1.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
            ultraGridColumn1.Header.Caption = "Unidad Productiva";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
            ultraGridColumn1.Width = 216;
            ultraGridColumn5.Header.Caption = "Deducible";
            ultraGridColumn5.Header.VisiblePosition = 1;
            ultraGridColumn5.Width = 85;
            ultraGridColumn6.Header.Caption = "Coaseguro";
            ultraGridColumn6.Header.VisiblePosition = 3;
            ultraGridColumn6.Width = 78;
            appearance3.TextHAlignAsString = "Right";
            ultraGridColumn4.CellAppearance = appearance3;
            ultraGridColumn4.Header.Caption = "Valor";
            ultraGridColumn4.Header.VisiblePosition = 2;
            ultraGridColumn4.MaskClipMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.IncludeLiterals;
            ultraGridColumn4.MaskDataMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.IncludeLiterals;
            ultraGridColumn4.MaskDisplayMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.IncludeLiterals;
            ultraGridColumn4.MaskInput = "{double:9.2}";
            ultraGridColumn4.Width = 121;
            ultraGridColumn2.Header.VisiblePosition = 4;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn4,
            ultraGridColumn2});
            this.grd.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grd.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grd.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.SystemColors.Window;
            this.grd.DisplayLayout.GroupByBox.Appearance = appearance4;
            appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grd.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
            this.grd.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance6.BackColor2 = System.Drawing.SystemColors.Control;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grd.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
            this.grd.DisplayLayout.MaxColScrollRegions = 1;
            this.grd.DisplayLayout.MaxRowScrollRegions = 1;
            this.grd.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grd.DisplayLayout.Override.ActiveCellAppearance = appearance7;
            appearance8.BackColor = System.Drawing.SystemColors.Highlight;
            appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grd.DisplayLayout.Override.ActiveRowAppearance = appearance8;
            this.grd.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grd.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            this.grd.DisplayLayout.Override.CardAreaAppearance = appearance9;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grd.DisplayLayout.Override.CellAppearance = appearance10;
            this.grd.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grd.DisplayLayout.Override.CellPadding = 0;
            appearance11.BackColor = System.Drawing.SystemColors.Control;
            appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance11.BorderColor = System.Drawing.SystemColors.Window;
            this.grd.DisplayLayout.Override.GroupByRowAppearance = appearance11;
            appearance12.TextHAlignAsString = "Left";
            this.grd.DisplayLayout.Override.HeaderAppearance = appearance12;
            this.grd.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grd.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.Color.Silver;
            this.grd.DisplayLayout.Override.RowAppearance = appearance13;
            this.grd.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.RowIndex;
            this.grd.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.grd.DisplayLayout.Override.RowSelectorStyle = Infragistics.Win.HeaderStyle.WindowsVista;
            appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grd.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
            this.grd.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grd.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grd.Location = new System.Drawing.Point(3, 74);
            this.grd.Name = "grd";
            this.grd.Size = new System.Drawing.Size(640, 324);
            this.grd.TabIndex = 1;
            this.grd.Text = "ultraGrid1";
            this.grd.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.grd_CellChange);
            this.grd.ClickCellButton += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.grd_ClickCellButton);
            // 
            // ultraPanel1
            // 
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.btnEliminar);
            this.ultraPanel1.ClientArea.Controls.Add(this.btnNuevo);
            this.ultraPanel1.ClientArea.Controls.Add(this.btnSalir);
            this.ultraPanel1.ClientArea.Controls.Add(this.btnGuardar);
            this.ultraPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraPanel1.Location = new System.Drawing.Point(3, 404);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(640, 39);
            this.ultraPanel1.TabIndex = 2;
            // 
            // btnEliminar
            // 
            appearance15.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.delete;
            this.btnEliminar.Appearance = appearance15;
            this.btnEliminar.Location = new System.Drawing.Point(397, 4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 28);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "&Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnNuevo
            // 
            appearance16.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.add;
            this.btnNuevo.Appearance = appearance16;
            this.btnNuevo.Location = new System.Drawing.Point(478, 4);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 28);
            this.btnNuevo.TabIndex = 2;
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnSalir
            // 
            appearance17.Image = global::Sigesoft.Node.WinClient.UI.Resources.door_out;
            this.btnSalir.Appearance = appearance17;
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(11, 4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 28);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "&Salir";
            // 
            // btnGuardar
            // 
            appearance18.Image = ((object)(resources.GetObject("appearance18.Image")));
            this.btnGuardar.Appearance = appearance18;
            this.btnGuardar.Location = new System.Drawing.Point(92, 4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 28);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnAgregarLinea
            // 
            this.btnAgregarLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregarLinea.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAgregarLinea.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAgregarLinea.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAgregarLinea.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAgregarLinea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarLinea.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarLinea.ForeColor = System.Drawing.Color.Black;
            this.btnAgregarLinea.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarLinea.Image")));
            this.btnAgregarLinea.Location = new System.Drawing.Point(531, 31);
            this.btnAgregarLinea.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarLinea.Name = "btnAgregarLinea";
            this.btnAgregarLinea.Size = new System.Drawing.Size(82, 21);
            this.btnAgregarLinea.TabIndex = 63;
            this.btnAgregarLinea.UseVisualStyleBackColor = false;
            this.btnAgregarLinea.Click += new System.EventHandler(this.btnAgregarLinea_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(528, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "REGISTRAR PLAN";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 16);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(634, 46);
            this.shapeContainer1.TabIndex = 65;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 521;
            this.lineShape1.X2 = 521;
            this.lineShape1.Y1 = -16;
            this.lineShape1.Y2 = 44;
            // 
            // frmProtocolPlanAseguradora
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(646, 446);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProtocolPlanAseguradora";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plan de Aseguradora";
            this.Load += new System.EventHandler(this.frmProtocolPlanAseguradora_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cboEmpresa;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        public Infragistics.Win.UltraWinGrid.UltraGrid grd;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.Misc.UltraButton btnSalir;
        private Infragistics.Win.Misc.UltraButton btnGuardar;
        private Infragistics.Win.Misc.UltraButton btnEliminar;
        private Infragistics.Win.Misc.UltraButton btnNuevo;
        private System.Windows.Forms.Button btnAgregarLinea;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
    }
}