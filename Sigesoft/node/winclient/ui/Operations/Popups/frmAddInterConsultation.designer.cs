namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    partial class frmAddInterConsultation
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn69 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DiagnosticRepositoryId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn70 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DiseasesId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn71 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DiseasesName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn72 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_RecordStatus");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn73 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_RecordType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn74 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_GenerateMedicalBreak");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.lblRecordCountDiagnosticos = new System.Windows.Forms.Label();
            this.grdDiagnosticos = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.lvInterconsultas = new System.Windows.Forms.ListView();
            this.chDiagnostico = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chConsultorio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemoverInterConsulta = new System.Windows.Forms.Button();
            this.btnAgregarInterConsulta = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chDiagnosticRepositoryId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblRecordCountInterconsultas = new System.Windows.Forms.Label();
            this.chOfficeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagnosticos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRecordCountDiagnosticos
            // 
            this.lblRecordCountDiagnosticos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCountDiagnosticos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountDiagnosticos.Location = new System.Drawing.Point(219, 171);
            this.lblRecordCountDiagnosticos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountDiagnosticos.Name = "lblRecordCountDiagnosticos";
            this.lblRecordCountDiagnosticos.Size = new System.Drawing.Size(207, 18);
            this.lblRecordCountDiagnosticos.TabIndex = 61;
            this.lblRecordCountDiagnosticos.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCountDiagnosticos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdDiagnosticos
            // 
            this.grdDiagnosticos.CausesValidation = false;
            appearance1.BackColor = System.Drawing.SystemColors.ControlLight;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDiagnosticos.DisplayLayout.Appearance = appearance1;
            ultraGridColumn69.Header.VisiblePosition = 0;
            ultraGridColumn69.Hidden = true;
            ultraGridColumn70.Header.VisiblePosition = 1;
            ultraGridColumn70.Hidden = true;
            ultraGridColumn71.Header.Caption = "Diagnóstico";
            ultraGridColumn71.Header.VisiblePosition = 2;
            ultraGridColumn71.Width = 399;
            ultraGridColumn72.Header.VisiblePosition = 3;
            ultraGridColumn72.Hidden = true;
            ultraGridColumn73.Header.VisiblePosition = 4;
            ultraGridColumn73.Hidden = true;
            ultraGridColumn74.Header.VisiblePosition = 5;
            ultraGridColumn74.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn69,
            ultraGridColumn70,
            ultraGridColumn71,
            ultraGridColumn72,
            ultraGridColumn73,
            ultraGridColumn74});
            this.grdDiagnosticos.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDiagnosticos.DisplayLayout.InterBandSpacing = 10;
            this.grdDiagnosticos.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDiagnosticos.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDiagnosticos.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDiagnosticos.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDiagnosticos.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDiagnosticos.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDiagnosticos.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDiagnosticos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdDiagnosticos.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDiagnosticos.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdDiagnosticos.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.SystemColors.Control;
            appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDiagnosticos.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdDiagnosticos.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            appearance5.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdDiagnosticos.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDiagnosticos.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdDiagnosticos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.FontData.BoldAsString = "True";
            this.grdDiagnosticos.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdDiagnosticos.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.grdDiagnosticos.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDiagnosticos.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDiagnosticos.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDiagnosticos.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDiagnosticos.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDiagnosticos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDiagnosticos.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDiagnosticos.Location = new System.Drawing.Point(5, 18);
            this.grdDiagnosticos.Margin = new System.Windows.Forms.Padding(2);
            this.grdDiagnosticos.Name = "grdDiagnosticos";
            this.grdDiagnosticos.Size = new System.Drawing.Size(423, 151);
            this.grdDiagnosticos.TabIndex = 63;
            this.grdDiagnosticos.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdDiagnosticos_AfterSelectChange);
            this.grdDiagnosticos.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(this.grdDiagnosticos_DoubleClickRow);
            // 
            // lvInterconsultas
            // 
            this.lvInterconsultas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDiagnostico,
            this.chConsultorio,
            this.chDiagnosticRepositoryId,
            this.chOfficeId});
            this.lvInterconsultas.FullRowSelect = true;
            this.lvInterconsultas.Location = new System.Drawing.Point(6, 16);
            this.lvInterconsultas.Name = "lvInterconsultas";
            this.lvInterconsultas.Size = new System.Drawing.Size(350, 151);
            this.lvInterconsultas.TabIndex = 0;
            this.lvInterconsultas.UseCompatibleStateImageBehavior = false;
            this.lvInterconsultas.View = System.Windows.Forms.View.Details;
            this.lvInterconsultas.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvInterconsultas_ItemSelectionChanged);
            // 
            // chDiagnostico
            // 
            this.chDiagnostico.Text = "Diagnóstico";
            this.chDiagnostico.Width = 191;
            // 
            // chConsultorio
            // 
            this.chConsultorio.Text = "Area / Consultorio";
            this.chConsultorio.Width = 154;
            // 
            // btnRemoverInterConsulta
            // 
            this.btnRemoverInterConsulta.Enabled = false;
            this.btnRemoverInterConsulta.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnRemoverInterConsulta.Location = new System.Drawing.Point(436, 97);
            this.btnRemoverInterConsulta.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoverInterConsulta.Name = "btnRemoverInterConsulta";
            this.btnRemoverInterConsulta.Size = new System.Drawing.Size(25, 22);
            this.btnRemoverInterConsulta.TabIndex = 93;
            this.btnRemoverInterConsulta.UseVisualStyleBackColor = true;
            this.btnRemoverInterConsulta.Click += new System.EventHandler(this.btnRemoverInterConsulta_Click);
            // 
            // btnAgregarInterConsulta
            // 
            this.btnAgregarInterConsulta.Enabled = false;
            this.btnAgregarInterConsulta.Image = global::Sigesoft.Node.WinClient.UI.Resources.play_green;
            this.btnAgregarInterConsulta.Location = new System.Drawing.Point(436, 56);
            this.btnAgregarInterConsulta.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarInterConsulta.Name = "btnAgregarInterConsulta";
            this.btnAgregarInterConsulta.Size = new System.Drawing.Size(25, 22);
            this.btnAgregarInterConsulta.TabIndex = 92;
            this.btnAgregarInterConsulta.UseVisualStyleBackColor = true;
            this.btnAgregarInterConsulta.Click += new System.EventHandler(this.btnAgregarInterConsulta_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(745, 197);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 60;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(661, 197);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.TabIndex = 59;
            this.btnOK.Text = "Aceptar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdDiagnosticos);
            this.groupBox1.Controls.Add(this.lblRecordCountDiagnosticos);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 192);
            this.groupBox1.TabIndex = 94;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Diagnósticos";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblRecordCountInterconsultas);
            this.groupBox2.Controls.Add(this.lvInterconsultas);
            this.groupBox2.Location = new System.Drawing.Point(464, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(361, 187);
            this.groupBox2.TabIndex = 95;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Interconsultas";
            // 
            // chDiagnosticRepositoryId
            // 
            this.chDiagnosticRepositoryId.Text = "DiagnosticRepositoryId";
            this.chDiagnosticRepositoryId.Width = 0;
            // 
            // lblRecordCountInterconsultas
            // 
            this.lblRecordCountInterconsultas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCountInterconsultas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountInterconsultas.Location = new System.Drawing.Point(149, 167);
            this.lblRecordCountInterconsultas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountInterconsultas.Name = "lblRecordCountInterconsultas";
            this.lblRecordCountInterconsultas.Size = new System.Drawing.Size(207, 18);
            this.lblRecordCountInterconsultas.TabIndex = 62;
            this.lblRecordCountInterconsultas.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCountInterconsultas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chOfficeId
            // 
            this.chOfficeId.Text = "chOfficeId";
            this.chOfficeId.Width = 0;
            // 
            // frmAddInterConsultation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 227);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRemoverInterConsulta);
            this.Controls.Add(this.btnAgregarInterConsulta);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddInterConsultation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar Interconsulta";
            this.Load += new System.EventHandler(this.frmAddInterConsultation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagnosticos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblRecordCountDiagnosticos;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDiagnosticos;
        private System.Windows.Forms.ListView lvInterconsultas;
        private System.Windows.Forms.ColumnHeader chDiagnostico;
        private System.Windows.Forms.ColumnHeader chConsultorio;
        private System.Windows.Forms.Button btnRemoverInterConsulta;
        private System.Windows.Forms.Button btnAgregarInterConsulta;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ColumnHeader chDiagnosticRepositoryId;
        private System.Windows.Forms.Label lblRecordCountInterconsultas;
        private System.Windows.Forms.ColumnHeader chOfficeId;
    }
}