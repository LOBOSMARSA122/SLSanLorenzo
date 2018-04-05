namespace Sigesoft.Node.WinClient.UI
{
    partial class frmEspecialistaDiagnostico
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DiagnosticRepositoryId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DiseasesId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DiseasesName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_AutoManualName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_RestrictionsName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_RecomendationsName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_PreQualificationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_AutoManualId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentFieldsId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_RecordStatus");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_RecordType");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.gbDiagnosticoExamen = new System.Windows.Forms.GroupBox();
            this.btnRemoverDxExamen = new System.Windows.Forms.Button();
            this.btnEditarDxExamen = new System.Windows.Forms.Button();
            this.btnAgregarDxExamen = new System.Windows.Forms.Button();
            this.grdDiagnosticoPorExamenComponente = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.lblRecordCountDiagnosticoPorExamenCom = new System.Windows.Forms.Label();
            this.btnGuardarExamen = new System.Windows.Forms.Button();
            this.gbDiagnosticoExamen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagnosticoPorExamenComponente)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDiagnosticoExamen
            // 
            this.gbDiagnosticoExamen.Controls.Add(this.btnGuardarExamen);
            this.gbDiagnosticoExamen.Controls.Add(this.btnRemoverDxExamen);
            this.gbDiagnosticoExamen.Controls.Add(this.btnEditarDxExamen);
            this.gbDiagnosticoExamen.Controls.Add(this.btnAgregarDxExamen);
            this.gbDiagnosticoExamen.Controls.Add(this.grdDiagnosticoPorExamenComponente);
            this.gbDiagnosticoExamen.Controls.Add(this.lblRecordCountDiagnosticoPorExamenCom);
            this.gbDiagnosticoExamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDiagnosticoExamen.ForeColor = System.Drawing.Color.MediumBlue;
            this.gbDiagnosticoExamen.Location = new System.Drawing.Point(12, 12);
            this.gbDiagnosticoExamen.Name = "gbDiagnosticoExamen";
            this.gbDiagnosticoExamen.Size = new System.Drawing.Size(858, 165);
            this.gbDiagnosticoExamen.TabIndex = 50;
            this.gbDiagnosticoExamen.TabStop = false;
            this.gbDiagnosticoExamen.Text = "Diagnosticos del Examen";
            // 
            // btnRemoverDxExamen
            // 
            this.btnRemoverDxExamen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoverDxExamen.BackColor = System.Drawing.SystemColors.Control;
            this.btnRemoverDxExamen.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnRemoverDxExamen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRemoverDxExamen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnRemoverDxExamen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoverDxExamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoverDxExamen.ForeColor = System.Drawing.Color.Black;
            this.btnRemoverDxExamen.Image = global::Sigesoft.Node.WinClient.UI.Resources.delete;
            this.btnRemoverDxExamen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoverDxExamen.Location = new System.Drawing.Point(188, 132);
            this.btnRemoverDxExamen.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoverDxExamen.Name = "btnRemoverDxExamen";
            this.btnRemoverDxExamen.Size = new System.Drawing.Size(80, 24);
            this.btnRemoverDxExamen.TabIndex = 91;
            this.btnRemoverDxExamen.Text = "     Eliminar";
            this.btnRemoverDxExamen.UseVisualStyleBackColor = false;
            this.btnRemoverDxExamen.Click += new System.EventHandler(this.btnRemoverDxExamen_Click);
            // 
            // btnEditarDxExamen
            // 
            this.btnEditarDxExamen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditarDxExamen.BackColor = System.Drawing.SystemColors.Control;
            this.btnEditarDxExamen.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnEditarDxExamen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEditarDxExamen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnEditarDxExamen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarDxExamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarDxExamen.ForeColor = System.Drawing.Color.Black;
            this.btnEditarDxExamen.Image = global::Sigesoft.Node.WinClient.UI.Resources.pencil;
            this.btnEditarDxExamen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditarDxExamen.Location = new System.Drawing.Point(106, 132);
            this.btnEditarDxExamen.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditarDxExamen.Name = "btnEditarDxExamen";
            this.btnEditarDxExamen.Size = new System.Drawing.Size(80, 24);
            this.btnEditarDxExamen.TabIndex = 90;
            this.btnEditarDxExamen.Text = "Modificar";
            this.btnEditarDxExamen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditarDxExamen.UseVisualStyleBackColor = false;
            this.btnEditarDxExamen.Click += new System.EventHandler(this.btnEditarDxExamen_Click);
            // 
            // btnAgregarDxExamen
            // 
            this.btnAgregarDxExamen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregarDxExamen.BackColor = System.Drawing.SystemColors.Control;
            this.btnAgregarDxExamen.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAgregarDxExamen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAgregarDxExamen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAgregarDxExamen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarDxExamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarDxExamen.ForeColor = System.Drawing.Color.Black;
            this.btnAgregarDxExamen.Image = global::Sigesoft.Node.WinClient.UI.Resources.add;
            this.btnAgregarDxExamen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarDxExamen.Location = new System.Drawing.Point(24, 132);
            this.btnAgregarDxExamen.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarDxExamen.Name = "btnAgregarDxExamen";
            this.btnAgregarDxExamen.Size = new System.Drawing.Size(80, 24);
            this.btnAgregarDxExamen.TabIndex = 89;
            this.btnAgregarDxExamen.Text = "      Agregar";
            this.btnAgregarDxExamen.UseVisualStyleBackColor = false;
            this.btnAgregarDxExamen.Click += new System.EventHandler(this.btnAgregarDxExamen_Click);
            // 
            // grdDiagnosticoPorExamenComponente
            // 
            this.grdDiagnosticoPorExamenComponente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grdDiagnosticoPorExamenComponente.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.Caption = "Diagnóstico";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 403;
            ultraGridColumn4.Header.Caption = "Automatico?";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.Header.Caption = "Restricciones";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 311;
            ultraGridColumn6.Header.Caption = "Recomendaciones";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 182;
            ultraGridColumn7.Header.Caption = "Pre-Calificación";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn11.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11});
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.InterBandSpacing = 10;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.Color.DarkGray;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Circular;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDiagnosticoPorExamenComponente.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDiagnosticoPorExamenComponente.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDiagnosticoPorExamenComponente.Location = new System.Drawing.Point(24, 29);
            this.grdDiagnosticoPorExamenComponente.Margin = new System.Windows.Forms.Padding(2);
            this.grdDiagnosticoPorExamenComponente.Name = "grdDiagnosticoPorExamenComponente";
            this.grdDiagnosticoPorExamenComponente.Size = new System.Drawing.Size(829, 101);
            this.grdDiagnosticoPorExamenComponente.TabIndex = 48;
            // 
            // lblRecordCountDiagnosticoPorExamenCom
            // 
            this.lblRecordCountDiagnosticoPorExamenCom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRecordCountDiagnosticoPorExamenCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountDiagnosticoPorExamenCom.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCountDiagnosticoPorExamenCom.Location = new System.Drawing.Point(662, 8);
            this.lblRecordCountDiagnosticoPorExamenCom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountDiagnosticoPorExamenCom.Name = "lblRecordCountDiagnosticoPorExamenCom";
            this.lblRecordCountDiagnosticoPorExamenCom.Size = new System.Drawing.Size(189, 19);
            this.lblRecordCountDiagnosticoPorExamenCom.TabIndex = 49;
            this.lblRecordCountDiagnosticoPorExamenCom.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCountDiagnosticoPorExamenCom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGuardarExamen
            // 
            this.btnGuardarExamen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardarExamen.BackColor = System.Drawing.SystemColors.Control;
            this.btnGuardarExamen.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGuardarExamen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGuardarExamen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnGuardarExamen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarExamen.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnGuardarExamen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarExamen.Location = new System.Drawing.Point(762, 132);
            this.btnGuardarExamen.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardarExamen.Name = "btnGuardarExamen";
            this.btnGuardarExamen.Size = new System.Drawing.Size(89, 24);
            this.btnGuardarExamen.TabIndex = 92;
            this.btnGuardarExamen.Text = "      Guardar";
            this.btnGuardarExamen.UseVisualStyleBackColor = false;
            this.btnGuardarExamen.Click += new System.EventHandler(this.btnGuardarExamen_Click);
            // 
            // frmEspecialistaDiagnostico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(878, 192);
            this.Controls.Add(this.gbDiagnosticoExamen);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEspecialistaDiagnostico";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Diagnósticos";
            this.Load += new System.EventHandler(this.frmEspecialistaDiagnostico_Load);
            this.gbDiagnosticoExamen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagnosticoPorExamenComponente)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDiagnosticoExamen;
        private System.Windows.Forms.Button btnRemoverDxExamen;
        private System.Windows.Forms.Button btnEditarDxExamen;
        private System.Windows.Forms.Button btnAgregarDxExamen;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDiagnosticoPorExamenComponente;
        private System.Windows.Forms.Label lblRecordCountDiagnosticoPorExamenCom;
        private System.Windows.Forms.Button btnGuardarExamen;
    }
}