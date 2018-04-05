namespace Sigesoft.Node.WinClient.UI
{
    partial class frmAuthorizedPerson
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProtocolName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_FirstName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_FirstLastName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_SecondLastName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_SexTypeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DocTypeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DocNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_Birthdate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_DocTypeId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_SexTypeId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CurrentOccupation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_OrganitationName");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtProtocolName = new System.Windows.Forms.TextBox();
            this.grdDataPeople = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.lblRecordCountPacients = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.btnCancelPersonMedical = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSearchProtocol = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataPeople)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.btnSearchProtocol);
            this.groupBox4.Controls.Add(this.label36);
            this.groupBox4.Controls.Add(this.txtProtocolName);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox4.Location = new System.Drawing.Point(2, 402);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(762, 48);
            this.groupBox4.TabIndex = 41;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "< Asignación de Protocolo >";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.Black;
            this.label36.Location = new System.Drawing.Point(4, 21);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(52, 13);
            this.label36.TabIndex = 33;
            this.label36.Text = "Protocolo";
            // 
            // txtProtocolName
            // 
            this.txtProtocolName.BackColor = System.Drawing.Color.White;
            this.txtProtocolName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProtocolName.ForeColor = System.Drawing.Color.Black;
            this.txtProtocolName.Location = new System.Drawing.Point(60, 18);
            this.txtProtocolName.Margin = new System.Windows.Forms.Padding(2);
            this.txtProtocolName.Name = "txtProtocolName";
            this.txtProtocolName.ReadOnly = true;
            this.txtProtocolName.Size = new System.Drawing.Size(552, 20);
            this.txtProtocolName.TabIndex = 1;
            this.txtProtocolName.TextChanged += new System.EventHandler(this.txtProtocolName_TextChanged);
            // 
            // grdDataPeople
            // 
            this.grdDataPeople.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataPeople.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDataPeople.DisplayLayout.Appearance = appearance1;
            ultraGridColumn12.Header.Caption = "Protocolo";
            ultraGridColumn12.Header.VisiblePosition = 0;
            ultraGridColumn12.Width = 213;
            ultraGridColumn3.Header.Caption = "Nombre";
            ultraGridColumn3.Header.VisiblePosition = 1;
            ultraGridColumn3.Width = 172;
            ultraGridColumn6.Header.Caption = "Apellido Paterno";
            ultraGridColumn6.Header.VisiblePosition = 2;
            ultraGridColumn6.Width = 131;
            ultraGridColumn7.Header.Caption = "Apellido Materno";
            ultraGridColumn7.Header.VisiblePosition = 3;
            ultraGridColumn7.Width = 122;
            ultraGridColumn2.Header.Caption = "Género";
            ultraGridColumn2.Header.VisiblePosition = 10;
            ultraGridColumn4.Header.Caption = "Tipo Documento";
            ultraGridColumn4.Header.VisiblePosition = 7;
            ultraGridColumn5.Header.Caption = "Número Documento";
            ultraGridColumn5.Header.VisiblePosition = 8;
            ultraGridColumn9.Header.Caption = "Fecha Nacimiento";
            ultraGridColumn9.Header.VisiblePosition = 11;
            ultraGridColumn1.Header.Caption = "ID Tipo Documento";
            ultraGridColumn1.Header.VisiblePosition = 6;
            ultraGridColumn8.Header.Caption = "ID Género";
            ultraGridColumn8.Header.VisiblePosition = 9;
            ultraGridColumn13.Header.Caption = "Puesto";
            ultraGridColumn13.Header.VisiblePosition = 5;
            ultraGridColumn13.Width = 104;
            ultraGridColumn11.Header.Caption = "Empresa";
            ultraGridColumn11.Header.VisiblePosition = 4;
            ultraGridColumn11.Width = 163;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn12,
            ultraGridColumn3,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn2,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn9,
            ultraGridColumn1,
            ultraGridColumn8,
            ultraGridColumn13,
            ultraGridColumn11});
            this.grdDataPeople.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDataPeople.DisplayLayout.InterBandSpacing = 10;
            this.grdDataPeople.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataPeople.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataPeople.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataPeople.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataPeople.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataPeople.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataPeople.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataPeople.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataPeople.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdDataPeople.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDataPeople.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdDataPeople.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDataPeople.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdDataPeople.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDataPeople.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDataPeople.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdDataPeople.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdDataPeople.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdDataPeople.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataPeople.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDataPeople.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDataPeople.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataPeople.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataPeople.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDataPeople.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDataPeople.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDataPeople.Location = new System.Drawing.Point(2, 31);
            this.grdDataPeople.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataPeople.Name = "grdDataPeople";
            this.grdDataPeople.Size = new System.Drawing.Size(926, 366);
            this.grdDataPeople.TabIndex = 61;
            // 
            // lblRecordCountPacients
            // 
            this.lblRecordCountPacients.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountPacients.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCountPacients.Location = new System.Drawing.Point(697, 10);
            this.lblRecordCountPacients.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountPacients.Name = "lblRecordCountPacients";
            this.lblRecordCountPacients.Size = new System.Drawing.Size(231, 19);
            this.lblRecordCountPacients.TabIndex = 65;
            this.lblRecordCountPacients.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCountPacients.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.BackColor = System.Drawing.SystemColors.Control;
            this.btnImportExcel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnImportExcel.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnImportExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnImportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportExcel.ForeColor = System.Drawing.Color.Black;
            this.btnImportExcel.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_excel;
            this.btnImportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportExcel.Location = new System.Drawing.Point(2, 3);
            this.btnImportExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(177, 24);
            this.btnImportExcel.TabIndex = 104;
            this.btnImportExcel.Text = "    Importar Lista de Autorizados";
            this.btnImportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImportExcel.UseVisualStyleBackColor = false;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // btnCancelPersonMedical
            // 
            this.btnCancelPersonMedical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelPersonMedical.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancelPersonMedical.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelPersonMedical.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnCancelPersonMedical.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCancelPersonMedical.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnCancelPersonMedical.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelPersonMedical.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancelPersonMedical.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelPersonMedical.Location = new System.Drawing.Point(853, 416);
            this.btnCancelPersonMedical.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelPersonMedical.Name = "btnCancelPersonMedical";
            this.btnCancelPersonMedical.Size = new System.Drawing.Size(75, 24);
            this.btnCancelPersonMedical.TabIndex = 103;
            this.btnCancelPersonMedical.Text = "   Salir";
            this.btnCancelPersonMedical.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Enabled = false;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(776, 416);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 24);
            this.btnSave.TabIndex = 102;
            this.btnSave.Text = "      Guardar";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSearchProtocol
            // 
            this.btnSearchProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearchProtocol.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearchProtocol.Enabled = false;
            this.btnSearchProtocol.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSearchProtocol.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSearchProtocol.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSearchProtocol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchProtocol.ForeColor = System.Drawing.Color.Black;
            this.btnSearchProtocol.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_search;
            this.btnSearchProtocol.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchProtocol.Location = new System.Drawing.Point(615, 15);
            this.btnSearchProtocol.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearchProtocol.Name = "btnSearchProtocol";
            this.btnSearchProtocol.Size = new System.Drawing.Size(139, 24);
            this.btnSearchProtocol.TabIndex = 103;
            this.btnSearchProtocol.Text = "Seleccione Protocolo";
            this.btnSearchProtocol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchProtocol.UseVisualStyleBackColor = false;
            this.btnSearchProtocol.Click += new System.EventHandler(this.btnSearchProtocol_Click);
            // 
            // frmAuthorizedPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(939, 457);
            this.Controls.Add(this.btnImportExcel);
            this.Controls.Add(this.btnCancelPersonMedical);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblRecordCountPacients);
            this.Controls.Add(this.grdDataPeople);
            this.Controls.Add(this.groupBox4);
            this.MinimizeBox = false;
            this.Name = "frmAuthorizedPerson";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Volcado de Trabajadores Autorizados";
            this.Load += new System.EventHandler(this.frmAuthorizedPerson_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataPeople)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtProtocolName;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataPeople;
        private System.Windows.Forms.Label lblRecordCountPacients;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSearchProtocol;
        private System.Windows.Forms.Button btnCancelPersonMedical;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnImportExcel;
    }
}