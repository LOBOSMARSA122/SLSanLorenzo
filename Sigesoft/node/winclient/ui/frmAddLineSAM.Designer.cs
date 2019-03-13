namespace Sigesoft.Node.WinClient.UI
{
    partial class frmAddLineSAM
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
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 0");
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CodLinea", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Nombre");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_UsuarioCreacion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("t_InsertaFecha");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_UsuarioModificacion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("t_ActualizaFecha");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_IdLinea");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.btnLineaAgregar = new Infragistics.Win.Misc.UltraButton();
            this.btnLineaBuscar = new Infragistics.Win.Misc.UltraButton();
            this.txtLineaNombre = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtLineaCodigo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.label37 = new Infragistics.Win.Misc.UltraLabel();
            this.label38 = new Infragistics.Win.Misc.UltraLabel();
            this.groupBox5 = new Infragistics.Win.Misc.UltraGroupBox();
            this.grdDataLinea = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnLineaEliminar = new Infragistics.Win.Misc.UltraButton();
            this.btnLineaEditar = new Infragistics.Win.Misc.UltraButton();
            this.lblContadorFilasLinea = new Infragistics.Win.Misc.UltraLabel();
            this.btnEditar_2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLineaNombre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLineaCodigo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox5)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataLinea)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraDataSource1
            // 
            this.ultraDataSource1.Band.Columns.AddRange(new object[] {
            ultraDataColumn3});
            // 
            // btnLineaAgregar
            // 
            this.btnLineaAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance18.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.add;
            appearance18.ImageHAlign = Infragistics.Win.HAlign.Right;
            appearance18.ImageVAlign = Infragistics.Win.VAlign.Middle;
            appearance18.TextHAlignAsString = "Left";
            appearance18.TextVAlignAsString = "Middle";
            this.btnLineaAgregar.Appearance = appearance18;
            this.btnLineaAgregar.BackColorInternal = System.Drawing.Color.Transparent;
            this.btnLineaAgregar.Location = new System.Drawing.Point(443, 41);
            this.btnLineaAgregar.Name = "btnLineaAgregar";
            this.btnLineaAgregar.Size = new System.Drawing.Size(95, 29);
            this.btnLineaAgregar.TabIndex = 17;
            this.btnLineaAgregar.Text = "Agregar";
            this.btnLineaAgregar.Click += new System.EventHandler(this.btnLineaAgregar_Click);
            // 
            // btnLineaBuscar
            // 
            this.btnLineaBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance19.Image = global::Sigesoft.Node.WinClient.UI.Resources.find;
            appearance19.ImageHAlign = Infragistics.Win.HAlign.Right;
            appearance19.ImageVAlign = Infragistics.Win.VAlign.Middle;
            appearance19.TextHAlignAsString = "Left";
            appearance19.TextVAlignAsString = "Middle";
            this.btnLineaBuscar.Appearance = appearance19;
            this.btnLineaBuscar.BackColorInternal = System.Drawing.Color.Transparent;
            this.btnLineaBuscar.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
            this.btnLineaBuscar.Location = new System.Drawing.Point(443, 6);
            this.btnLineaBuscar.Name = "btnLineaBuscar";
            this.btnLineaBuscar.Size = new System.Drawing.Size(95, 29);
            this.btnLineaBuscar.TabIndex = 16;
            this.btnLineaBuscar.Text = "Buscar";
            this.btnLineaBuscar.Click += new System.EventHandler(this.btnLineaBuscar_Click);
            // 
            // txtLineaNombre
            // 
            this.txtLineaNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLineaNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLineaNombre.Location = new System.Drawing.Point(70, 39);
            this.txtLineaNombre.MaxLength = 500;
            this.txtLineaNombre.Name = "txtLineaNombre";
            this.txtLineaNombre.Size = new System.Drawing.Size(328, 21);
            this.txtLineaNombre.TabIndex = 15;
            // 
            // txtLineaCodigo
            // 
            this.txtLineaCodigo.Location = new System.Drawing.Point(70, 10);
            this.txtLineaCodigo.MaxLength = 10;
            this.txtLineaCodigo.Name = "txtLineaCodigo";
            this.txtLineaCodigo.Size = new System.Drawing.Size(328, 21);
            this.txtLineaCodigo.TabIndex = 14;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(24, 42);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(44, 14);
            this.label37.TabIndex = 19;
            this.label37.Text = "Nombre";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(24, 13);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(40, 14);
            this.label38.TabIndex = 18;
            this.label38.Text = "Código";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.grdDataLinea);
            this.groupBox5.Controls.Add(this.btnLineaEliminar);
            this.groupBox5.Controls.Add(this.btnLineaEditar);
            this.groupBox5.Controls.Add(this.lblContadorFilasLinea);
            this.groupBox5.Location = new System.Drawing.Point(24, 83);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(523, 400);
            this.groupBox5.TabIndex = 20;
            this.groupBox5.Text = "Resultado de Búsqueda";
            // 
            // grdDataLinea
            // 
            this.grdDataLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataLinea.CausesValidation = false;
            appearance3.BackColor = System.Drawing.SystemColors.Window;
            appearance3.BackColor2 = System.Drawing.Color.LightSteelBlue;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDataLinea.DisplayLayout.Appearance = appearance3;
            ultraGridColumn29.Header.Caption = "Código";
            ultraGridColumn29.Header.VisiblePosition = 0;
            ultraGridColumn30.Header.Caption = "Nombre";
            ultraGridColumn30.Header.VisiblePosition = 1;
            ultraGridColumn31.Header.Caption = "Usuario Crea.";
            ultraGridColumn31.Header.VisiblePosition = 2;
            ultraGridColumn32.Format = "dd/MM/yyyy hh:mm tt";
            ultraGridColumn32.Header.Caption = "Fecha Crea.";
            ultraGridColumn32.Header.VisiblePosition = 3;
            ultraGridColumn33.Header.Caption = "Usuario Act.";
            ultraGridColumn33.Header.VisiblePosition = 4;
            ultraGridColumn34.Format = "dd/MM/yyyy hh:mm tt";
            ultraGridColumn34.Header.Caption = "Fecha Act.";
            ultraGridColumn34.Header.VisiblePosition = 5;
            ultraGridColumn1.Header.VisiblePosition = 6;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn29,
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34,
            ultraGridColumn1});
            this.grdDataLinea.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDataLinea.DisplayLayout.InterBandSpacing = 10;
            this.grdDataLinea.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataLinea.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataLinea.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataLinea.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataLinea.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataLinea.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataLinea.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataLinea.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance4.BackColor = System.Drawing.Color.Transparent;
            this.grdDataLinea.DisplayLayout.Override.CardAreaAppearance = appearance4;
            appearance5.BackColor = System.Drawing.SystemColors.Control;
            appearance5.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDataLinea.DisplayLayout.Override.CellAppearance = appearance5;
            this.grdDataLinea.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance6.BackColor = System.Drawing.SystemColors.Control;
            appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance6.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDataLinea.DisplayLayout.Override.HeaderAppearance = appearance6;
            this.grdDataLinea.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance7.AlphaLevel = ((short)(187));
            appearance7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            appearance7.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdDataLinea.DisplayLayout.Override.RowAlternateAppearance = appearance7;
            appearance8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDataLinea.DisplayLayout.Override.RowSelectorAppearance = appearance8;
            this.grdDataLinea.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance9.BackColor = System.Drawing.SystemColors.Highlight;
            appearance9.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance9.FontData.BoldAsString = "True";
            this.grdDataLinea.DisplayLayout.Override.SelectedRowAppearance = appearance9;
            this.grdDataLinea.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataLinea.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDataLinea.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDataLinea.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataLinea.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataLinea.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDataLinea.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDataLinea.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDataLinea.Location = new System.Drawing.Point(7, 32);
            this.grdDataLinea.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataLinea.Name = "grdDataLinea";
            this.grdDataLinea.Size = new System.Drawing.Size(507, 332);
            this.grdDataLinea.TabIndex = 49;
            this.grdDataLinea.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.grdDataLinea_ClickCell);
            // 
            // btnLineaEliminar
            // 
            this.btnLineaEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            appearance20.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.delete;
            appearance20.ImageHAlign = Infragistics.Win.HAlign.Left;
            appearance20.ImageVAlign = Infragistics.Win.VAlign.Middle;
            appearance20.TextHAlignAsString = "Right";
            appearance20.TextVAlignAsString = "Middle";
            this.btnLineaEliminar.Appearance = appearance20;
            this.btnLineaEliminar.Enabled = false;
            this.btnLineaEliminar.Location = new System.Drawing.Point(307, 367);
            this.btnLineaEliminar.Name = "btnLineaEliminar";
            this.btnLineaEliminar.Size = new System.Drawing.Size(105, 31);
            this.btnLineaEliminar.TabIndex = 48;
            this.btnLineaEliminar.Text = "E&liminar";
            this.btnLineaEliminar.Click += new System.EventHandler(this.btnLineaEliminar_Click);
            // 
            // btnLineaEditar
            // 
            this.btnLineaEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            appearance21.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.pencil;
            appearance21.ImageHAlign = Infragistics.Win.HAlign.Left;
            appearance21.ImageVAlign = Infragistics.Win.VAlign.Middle;
            appearance21.TextHAlignAsString = "Right";
            appearance21.TextVAlignAsString = "Middle";
            this.btnLineaEditar.Appearance = appearance21;
            this.btnLineaEditar.Enabled = false;
            this.btnLineaEditar.Location = new System.Drawing.Point(419, 367);
            this.btnLineaEditar.Name = "btnLineaEditar";
            this.btnLineaEditar.Size = new System.Drawing.Size(95, 31);
            this.btnLineaEditar.TabIndex = 47;
            this.btnLineaEditar.Text = "E&ditar";
            this.btnLineaEditar.Click += new System.EventHandler(this.btnLineaEditar_Click);
            // 
            // lblContadorFilasLinea
            // 
            this.lblContadorFilasLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance22.FontData.SizeInPoints = 7F;
            appearance22.TextHAlignAsString = "Right";
            appearance22.TextVAlignAsString = "Middle";
            this.lblContadorFilasLinea.Appearance = appearance22;
            this.lblContadorFilasLinea.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContadorFilasLinea.Location = new System.Drawing.Point(289, 16);
            this.lblContadorFilasLinea.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblContadorFilasLinea.Name = "lblContadorFilasLinea";
            this.lblContadorFilasLinea.Size = new System.Drawing.Size(231, 12);
            this.lblContadorFilasLinea.TabIndex = 45;
            this.lblContadorFilasLinea.Text = "No se ha realizado la búsqueda aún.";
            // 
            // btnEditar_2
            // 
            this.btnEditar_2.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.pencil;
            this.btnEditar_2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditar_2.Location = new System.Drawing.Point(443, 42);
            this.btnEditar_2.Name = "btnEditar_2";
            this.btnEditar_2.Size = new System.Drawing.Size(95, 29);
            this.btnEditar_2.TabIndex = 21;
            this.btnEditar_2.Text = "Editar";
            this.btnEditar_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar_2.UseVisualStyleBackColor = true;
            this.btnEditar_2.Visible = false;
            this.btnEditar_2.Click += new System.EventHandler(this.btnEditar_2_Click);
            // 
            // frmAddLineSAM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 495);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.btnLineaAgregar);
            this.Controls.Add(this.btnLineaBuscar);
            this.Controls.Add(this.txtLineaNombre);
            this.Controls.Add(this.txtLineaCodigo);
            this.Controls.Add(this.btnEditar_2);
            this.Name = "frmAddLineSAM";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unidades productivas";
            this.Load += new System.EventHandler(this.frmAddLineSAM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLineaNombre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLineaCodigo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox5)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataLinea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
        private Infragistics.Win.Misc.UltraButton btnLineaAgregar;
        private Infragistics.Win.Misc.UltraButton btnLineaBuscar;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtLineaNombre;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtLineaCodigo;
        private Infragistics.Win.Misc.UltraLabel label37;
        private Infragistics.Win.Misc.UltraLabel label38;
        private Infragistics.Win.Misc.UltraGroupBox groupBox5;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataLinea;
        private Infragistics.Win.Misc.UltraButton btnLineaEliminar;
        private Infragistics.Win.Misc.UltraButton btnLineaEditar;
        private Infragistics.Win.Misc.UltraLabel lblContadorFilasLinea;
        private System.Windows.Forms.Button btnEditar_2;
    }
}