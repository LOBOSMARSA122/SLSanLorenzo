namespace Sigesoft.Node.WinClient.UI
{
    partial class FrmConfiguracionPagoEspecialista
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_SystemUserId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_CategoryId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_EmployerOrganizationId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CustomerOrganizationId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_WorkingOrganizationId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Price");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Select", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Edit", 1);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfiguracionPagoEspecialista));
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Delete", 2);
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("i_SystemUserId");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("i_CategoryId");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("v_EmployerOrganizationId");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn4 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("v_CustomerOrganizationId");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn5 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("v_WorkingOrganizationId");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn6 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price");
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdConfiguration = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdConfiguration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdConfiguration);
            this.groupBox2.Controls.Add(this.btnAddRow);
            this.groupBox2.Location = new System.Drawing.Point(4, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1060, 428);
            this.groupBox2.TabIndex = 114;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista de Configuración";
            // 
            // grdConfiguration
            // 
            this.grdConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdConfiguration.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdConfiguration.DisplayLayout.Appearance = appearance1;
            ultraGridColumn4.Header.Caption = "Usuario Médico";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn9.Header.Caption = "Categoría";
            ultraGridColumn9.Header.VisiblePosition = 4;
            ultraGridColumn9.Width = 141;
            ultraGridColumn10.Header.Caption = "Empleadora";
            ultraGridColumn10.Header.VisiblePosition = 5;
            ultraGridColumn10.Width = 259;
            ultraGridColumn11.Header.Caption = "Cliente";
            ultraGridColumn11.Header.VisiblePosition = 6;
            ultraGridColumn11.Width = 171;
            ultraGridColumn12.Header.Caption = "Trabajadora";
            ultraGridColumn12.Header.VisiblePosition = 7;
            ultraGridColumn12.Width = 156;
            ultraGridColumn8.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            ultraGridColumn8.Header.Caption = "Precio";
            ultraGridColumn8.Header.VisiblePosition = 8;
            ultraGridColumn8.Width = 57;
            ultraGridColumn19.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
            ultraGridColumn19.Header.Fixed = true;
            ultraGridColumn19.Header.VisiblePosition = 0;
            ultraGridColumn19.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn19.Width = 39;
            ultraGridColumn20.ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            appearance2.Image = ((object)(resources.GetObject("appearance2.Image")));
            appearance2.TextHAlignAsString = "Center";
            appearance2.TextVAlignAsString = "Middle";
            ultraGridColumn20.CellAppearance = appearance2;
            appearance3.Image = ((object)(resources.GetObject("appearance3.Image")));
            appearance3.ImageHAlign = Infragistics.Win.HAlign.Center;
            appearance3.ImageVAlign = Infragistics.Win.VAlign.Middle;
            ultraGridColumn20.CellButtonAppearance = appearance3;
            appearance4.Image = ((object)(resources.GetObject("appearance4.Image")));
            ultraGridColumn20.Header.Appearance = appearance4;
            ultraGridColumn20.Header.Fixed = true;
            ultraGridColumn20.Header.ToolTipText = "Clonar Registro";
            ultraGridColumn20.Header.VisiblePosition = 1;
            ultraGridColumn20.Hidden = true;
            ultraGridColumn20.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            ultraGridColumn20.Width = 43;
            ultraGridColumn1.ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            appearance5.Image = ((object)(resources.GetObject("appearance5.Image")));
            appearance5.ImageHAlign = Infragistics.Win.HAlign.Center;
            appearance5.ImageVAlign = Infragistics.Win.VAlign.Middle;
            ultraGridColumn1.CellAppearance = appearance5;
            appearance6.Image = ((object)(resources.GetObject("appearance6.Image")));
            appearance6.TextHAlignAsString = "Center";
            appearance6.TextVAlignAsString = "Middle";
            ultraGridColumn1.CellButtonAppearance = appearance6;
            ultraGridColumn1.Header.VisiblePosition = 2;
            ultraGridColumn1.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            ultraGridColumn1.Width = 45;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn4,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn8,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn1});
            ultraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.Yes;
            this.grdConfiguration.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdConfiguration.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.grdConfiguration.DisplayLayout.GroupByBox.Prompt = " ";
            this.grdConfiguration.DisplayLayout.InterBandSpacing = 10;
            this.grdConfiguration.DisplayLayout.MaxColScrollRegions = 1;
            this.grdConfiguration.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdConfiguration.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdConfiguration.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdConfiguration.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdConfiguration.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance7.BackColor = System.Drawing.Color.Transparent;
            this.grdConfiguration.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BackColor = System.Drawing.Color.White;
            appearance8.BackColor2 = System.Drawing.Color.White;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdConfiguration.DisplayLayout.Override.CellAppearance = appearance8;
            this.grdConfiguration.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdConfiguration.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            this.grdConfiguration.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            appearance9.BackColor = System.Drawing.Color.White;
            appearance9.BackColor2 = System.Drawing.Color.LightGray;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance9.BorderColor = System.Drawing.Color.DarkGray;
            appearance9.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdConfiguration.DisplayLayout.Override.HeaderAppearance = appearance9;
            this.grdConfiguration.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance10.AlphaLevel = ((short)(187));
            appearance10.BackColor = System.Drawing.Color.Gainsboro;
            appearance10.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance10.ForeColor = System.Drawing.Color.Black;
            appearance10.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdConfiguration.DisplayLayout.Override.RowAlternateAppearance = appearance10;
            appearance11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdConfiguration.DisplayLayout.Override.RowSelectorAppearance = appearance11;
            this.grdConfiguration.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.grdConfiguration.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.RowIndex;
            this.grdConfiguration.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.grdConfiguration.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            appearance12.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance12.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance12.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance12.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance12.FontData.BoldAsString = "False";
            appearance12.ForeColor = System.Drawing.Color.Black;
            this.grdConfiguration.DisplayLayout.Override.SelectedRowAppearance = appearance12;
            this.grdConfiguration.DisplayLayout.Override.SupportDataErrorInfo = Infragistics.Win.UltraWinGrid.SupportDataErrorInfo.RowsAndCells;
            this.grdConfiguration.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdConfiguration.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdConfiguration.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdConfiguration.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdConfiguration.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdConfiguration.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grdConfiguration.Location = new System.Drawing.Point(8, 18);
            this.grdConfiguration.Margin = new System.Windows.Forms.Padding(2);
            this.grdConfiguration.Name = "grdConfiguration";
            this.grdConfiguration.Size = new System.Drawing.Size(1047, 377);
            this.grdConfiguration.TabIndex = 155;
            this.grdConfiguration.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grdConfiguration_InitializeLayout);
            this.grdConfiguration.ClickCellButton += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.grdConfiguration_ClickCellButton);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddRow.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddRow.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAddRow.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAddRow.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAddRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRow.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRow.ForeColor = System.Drawing.Color.Black;
            this.btnAddRow.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.add;
            this.btnAddRow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddRow.Location = new System.Drawing.Point(8, 399);
            this.btnAddRow.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(147, 24);
            this.btnAddRow.TabIndex = 113;
            this.btnAddRow.Text = "Agregar Nuevo Registro";
            this.btnAddRow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddRow.UseVisualStyleBackColor = false;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(944, 445);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(115, 24);
            this.btnSave.TabIndex = 114;
            this.btnSave.Text = "Grabar Cambios";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ultraDataSource1
            // 
            this.ultraDataSource1.Band.Columns.AddRange(new object[] {
            ultraDataColumn1,
            ultraDataColumn2,
            ultraDataColumn3,
            ultraDataColumn4,
            ultraDataColumn5,
            ultraDataColumn6});
            // 
            // FrmConfiguracionPagoEspecialista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 480);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmConfiguracionPagoEspecialista";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración de Pago por Especialista";
            this.Load += new System.EventHandler(this.FrmConfiguracionPagoEspecialista_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdConfiguration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnSave;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdConfiguration;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
    }
}