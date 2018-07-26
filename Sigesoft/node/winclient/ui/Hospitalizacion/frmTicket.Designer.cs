namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    partial class frmTicket
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_IdProductoDetalle");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_NombreProducto");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_Cantidad");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_EsDespachado");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNTicket = new System.Windows.Forms.TextBox();
            this.txtNServicio = new System.Windows.Forms.TextBox();
            this.btnNuevoProducto = new System.Windows.Forms.Button();
            this.btnCancelarTicket = new System.Windows.Forms.Button();
            this.btnGuardarTicket = new System.Windows.Forms.Button();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.grdTicketDetalle = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.grdTicketDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N° Ticket :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(325, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(583, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "N° Servicio :";
            // 
            // txtNTicket
            // 
            this.txtNTicket.Location = new System.Drawing.Point(70, 16);
            this.txtNTicket.Name = "txtNTicket";
            this.txtNTicket.Size = new System.Drawing.Size(186, 20);
            this.txtNTicket.TabIndex = 3;
            // 
            // txtNServicio
            // 
            this.txtNServicio.Enabled = false;
            this.txtNServicio.Location = new System.Drawing.Point(655, 16);
            this.txtNServicio.Name = "txtNServicio";
            this.txtNServicio.Size = new System.Drawing.Size(165, 20);
            this.txtNServicio.TabIndex = 5;
            // 
            // btnNuevoProducto
            // 
            this.btnNuevoProducto.Location = new System.Drawing.Point(740, 69);
            this.btnNuevoProducto.Name = "btnNuevoProducto";
            this.btnNuevoProducto.Size = new System.Drawing.Size(88, 47);
            this.btnNuevoProducto.TabIndex = 6;
            this.btnNuevoProducto.Text = "Nuevo Producto";
            this.btnNuevoProducto.UseVisualStyleBackColor = true;
            this.btnNuevoProducto.Click += new System.EventHandler(this.btnNuevoProducto_Click);
            // 
            // btnCancelarTicket
            // 
            this.btnCancelarTicket.Location = new System.Drawing.Point(560, 417);
            this.btnCancelarTicket.Name = "btnCancelarTicket";
            this.btnCancelarTicket.Size = new System.Drawing.Size(132, 23);
            this.btnCancelarTicket.TabIndex = 7;
            this.btnCancelarTicket.Text = "Cancelar";
            this.btnCancelarTicket.UseVisualStyleBackColor = true;
            this.btnCancelarTicket.Click += new System.EventHandler(this.btnCancelarTicket_Click);
            // 
            // btnGuardarTicket
            // 
            this.btnGuardarTicket.Location = new System.Drawing.Point(703, 417);
            this.btnGuardarTicket.Name = "btnGuardarTicket";
            this.btnGuardarTicket.Size = new System.Drawing.Size(122, 23);
            this.btnGuardarTicket.TabIndex = 8;
            this.btnGuardarTicket.Text = "Guardar";
            this.btnGuardarTicket.UseVisualStyleBackColor = true;
            this.btnGuardarTicket.Click += new System.EventHandler(this.btnGuardarTicket_Click);
            // 
            // txtFecha
            // 
            this.txtFecha.Enabled = false;
            this.txtFecha.Location = new System.Drawing.Point(378, 16);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(137, 20);
            this.txtFecha.TabIndex = 4;
            // 
            // grdTicketDetalle
            // 
            this.grdTicketDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTicketDetalle.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdTicketDetalle.DisplayLayout.Appearance = appearance1;
            ultraGridColumn11.Header.VisiblePosition = 0;
            ultraGridColumn15.Header.VisiblePosition = 1;
            ultraGridColumn12.Header.VisiblePosition = 2;
            ultraGridColumn13.Header.VisiblePosition = 3;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn11,
            ultraGridColumn15,
            ultraGridColumn12,
            ultraGridColumn13});
            this.grdTicketDetalle.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdTicketDetalle.DisplayLayout.InterBandSpacing = 10;
            this.grdTicketDetalle.DisplayLayout.MaxColScrollRegions = 1;
            this.grdTicketDetalle.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdTicketDetalle.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdTicketDetalle.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdTicketDetalle.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTicketDetalle.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdTicketDetalle.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdTicketDetalle.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdTicketDetalle.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdTicketDetalle.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdTicketDetalle.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdTicketDetalle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdTicketDetalle.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdTicketDetalle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdTicketDetalle.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdTicketDetalle.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdTicketDetalle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdTicketDetalle.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdTicketDetalle.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdTicketDetalle.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdTicketDetalle.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdTicketDetalle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTicketDetalle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTicketDetalle.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdTicketDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdTicketDetalle.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdTicketDetalle.Location = new System.Drawing.Point(8, 59);
            this.grdTicketDetalle.Margin = new System.Windows.Forms.Padding(2);
            this.grdTicketDetalle.Name = "grdTicketDetalle";
            this.grdTicketDetalle.Size = new System.Drawing.Size(727, 353);
            this.grdTicketDetalle.TabIndex = 47;
            // 
            // frmTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 452);
            this.Controls.Add(this.grdTicketDetalle);
            this.Controls.Add(this.btnGuardarTicket);
            this.Controls.Add(this.btnCancelarTicket);
            this.Controls.Add(this.btnNuevoProducto);
            this.Controls.Add(this.txtNServicio);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.txtNTicket);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmTicket";
            this.Text = "Ticket";
            this.Load += new System.EventHandler(this.frmTicket_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTicketDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNTicket;
        private System.Windows.Forms.TextBox txtNServicio;
        private System.Windows.Forms.Button btnNuevoProducto;
        private System.Windows.Forms.Button btnCancelarTicket;
        private System.Windows.Forms.Button btnGuardarTicket;
        private System.Windows.Forms.TextBox txtFecha;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdTicketDetalle;
    }
}