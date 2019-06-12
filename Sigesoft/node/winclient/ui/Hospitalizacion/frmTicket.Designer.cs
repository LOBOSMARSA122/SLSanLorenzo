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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTicket));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_IdProductoDetalle");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_NombreProducto");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_Cantidad");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_EsDespachado");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CodInterno");
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
            this.btnCancelarTicket = new System.Windows.Forms.Button();
            this.btnGuardarTicket = new System.Windows.Forms.Button();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.grdTicketDetalle = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboTipoCuenta = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRecordCount2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gbConCargo = new System.Windows.Forms.GroupBox();
            this.rbPaciente = new System.Windows.Forms.RadioButton();
            this.rbMedicoTratante = new System.Windows.Forms.RadioButton();
            this.chkTicketInterno = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdTicketDetalle)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbConCargo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N° Ticket :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(307, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "N° Servicio :";
            // 
            // txtNTicket
            // 
            this.txtNTicket.Enabled = false;
            this.txtNTicket.Location = new System.Drawing.Point(61, 19);
            this.txtNTicket.Name = "txtNTicket";
            this.txtNTicket.Size = new System.Drawing.Size(89, 20);
            this.txtNTicket.TabIndex = 3;
            // 
            // txtNServicio
            // 
            this.txtNServicio.Enabled = false;
            this.txtNServicio.Location = new System.Drawing.Point(379, 19);
            this.txtNServicio.Name = "txtNServicio";
            this.txtNServicio.Size = new System.Drawing.Size(98, 20);
            this.txtNServicio.TabIndex = 5;
            // 
            // btnCancelarTicket
            // 
            this.btnCancelarTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarTicket.Image = global::Sigesoft.Node.WinClient.UI.Resources.bullet_cross;
            this.btnCancelarTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelarTicket.Location = new System.Drawing.Point(14, 429);
            this.btnCancelarTicket.Name = "btnCancelarTicket";
            this.btnCancelarTicket.Size = new System.Drawing.Size(77, 23);
            this.btnCancelarTicket.TabIndex = 7;
            this.btnCancelarTicket.Text = "Cancelar";
            this.btnCancelarTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelarTicket.UseVisualStyleBackColor = true;
            this.btnCancelarTicket.Click += new System.EventHandler(this.btnCancelarTicket_Click);
            // 
            // btnGuardarTicket
            // 
            this.btnGuardarTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarTicket.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarTicket.Image")));
            this.btnGuardarTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarTicket.Location = new System.Drawing.Point(678, 429);
            this.btnGuardarTicket.Name = "btnGuardarTicket";
            this.btnGuardarTicket.Size = new System.Drawing.Size(77, 23);
            this.btnGuardarTicket.TabIndex = 8;
            this.btnGuardarTicket.Text = "Guardar";
            this.btnGuardarTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardarTicket.UseVisualStyleBackColor = true;
            this.btnGuardarTicket.Click += new System.EventHandler(this.btnGuardarTicket_Click);
            // 
            // txtFecha
            // 
            this.txtFecha.Enabled = false;
            this.txtFecha.Location = new System.Drawing.Point(211, 19);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(90, 20);
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
            ultraGridColumn11.Hidden = true;
            ultraGridColumn15.Header.Caption = "Nombre Producto";
            ultraGridColumn15.Header.VisiblePosition = 2;
            ultraGridColumn15.Width = 196;
            ultraGridColumn12.Header.Caption = "Cantidad";
            ultraGridColumn12.Header.VisiblePosition = 3;
            ultraGridColumn13.Header.VisiblePosition = 4;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn11,
            ultraGridColumn15,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn1});
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
            this.grdTicketDetalle.Location = new System.Drawing.Point(14, 165);
            this.grdTicketDetalle.Margin = new System.Windows.Forms.Padding(2);
            this.grdTicketDetalle.Name = "grdTicketDetalle";
            this.grdTicketDetalle.Size = new System.Drawing.Size(725, 249);
            this.grdTicketDetalle.TabIndex = 47;
            this.grdTicketDetalle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdproduc_AfterSelectChange);
            // 
            // btnRemover
            // 
            this.btnRemover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemover.BackColor = System.Drawing.SystemColors.Control;
            this.btnRemover.Enabled = false;
            this.btnRemover.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnRemover.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRemover.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnRemover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemover.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemover.ForeColor = System.Drawing.Color.Black;
            this.btnRemover.Image = ((System.Drawing.Image)(resources.GetObject("btnRemover.Image")));
            this.btnRemover.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemover.Location = new System.Drawing.Point(743, 114);
            this.btnRemover.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(69, 24);
            this.btnRemover.TabIndex = 96;
            this.btnRemover.Text = "     Eliminar";
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.BackColor = System.Drawing.SystemColors.Control;
            this.btnEditar.Enabled = false;
            this.btnEditar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnEditar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.Black;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(743, 76);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(69, 24);
            this.btnEditar.TabIndex = 95;
            this.btnEditar.Text = "      Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevo.BackColor = System.Drawing.SystemColors.Control;
            this.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnNuevo.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.Black;
            this.btnNuevo.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.add;
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(743, 39);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(69, 24);
            this.btnNuevo.TabIndex = 97;
            this.btnNuevo.Text = "     Agregar ";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboTipoCuenta);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtNServicio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNTicket);
            this.groupBox1.Controls.Add(this.txtFecha);
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(817, 53);
            this.groupBox1.TabIndex = 99;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Ticket";
            // 
            // cboTipoCuenta
            // 
            this.cboTipoCuenta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboTipoCuenta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTipoCuenta.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoCuenta.FormattingEnabled = true;
            this.cboTipoCuenta.Location = new System.Drawing.Point(568, 18);
            this.cboTipoCuenta.Margin = new System.Windows.Forms.Padding(2);
            this.cboTipoCuenta.Name = "cboTipoCuenta";
            this.cboTipoCuenta.Size = new System.Drawing.Size(231, 21);
            this.cboTipoCuenta.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(483, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tipo de Cuenta";
            // 
            // lblRecordCount2
            // 
            this.lblRecordCount2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount2.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCount2.Location = new System.Drawing.Point(485, 16);
            this.lblRecordCount2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCount2.Name = "lblRecordCount2";
            this.lblRecordCount2.Size = new System.Drawing.Size(259, 18);
            this.lblRecordCount2.TabIndex = 98;
            this.lblRecordCount2.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCount2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblRecordCount2);
            this.groupBox2.Controls.Add(this.btnNuevo);
            this.groupBox2.Controls.Add(this.btnRemover);
            this.groupBox2.Controls.Add(this.btnEditar);
            this.groupBox2.Location = new System.Drawing.Point(8, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(817, 293);
            this.groupBox2.TabIndex = 100;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalle de Productos";
            // 
            // gbConCargo
            // 
            this.gbConCargo.Controls.Add(this.rbPaciente);
            this.gbConCargo.Controls.Add(this.rbMedicoTratante);
            this.gbConCargo.Location = new System.Drawing.Point(8, 71);
            this.gbConCargo.Name = "gbConCargo";
            this.gbConCargo.Size = new System.Drawing.Size(276, 49);
            this.gbConCargo.TabIndex = 101;
            this.gbConCargo.TabStop = false;
            this.gbConCargo.Text = "Con Cargo a:";
            // 
            // rbPaciente
            // 
            this.rbPaciente.AutoSize = true;
            this.rbPaciente.Location = new System.Drawing.Point(159, 22);
            this.rbPaciente.Name = "rbPaciente";
            this.rbPaciente.Size = new System.Drawing.Size(67, 17);
            this.rbPaciente.TabIndex = 10;
            this.rbPaciente.TabStop = true;
            this.rbPaciente.Text = "Paciente";
            this.rbPaciente.UseVisualStyleBackColor = true;
            // 
            // rbMedicoTratante
            // 
            this.rbMedicoTratante.AutoSize = true;
            this.rbMedicoTratante.Location = new System.Drawing.Point(6, 22);
            this.rbMedicoTratante.Name = "rbMedicoTratante";
            this.rbMedicoTratante.Size = new System.Drawing.Size(103, 17);
            this.rbMedicoTratante.TabIndex = 9;
            this.rbMedicoTratante.TabStop = true;
            this.rbMedicoTratante.Text = "Medico Tratante";
            this.rbMedicoTratante.UseVisualStyleBackColor = true;
            // 
            // chkTicketInterno
            // 
            this.chkTicketInterno.AutoSize = true;
            this.chkTicketInterno.Location = new System.Drawing.Point(690, 93);
            this.chkTicketInterno.Name = "chkTicketInterno";
            this.chkTicketInterno.Size = new System.Drawing.Size(117, 17);
            this.chkTicketInterno.TabIndex = 102;
            this.chkTicketInterno.Text = "Es un ticket interno";
            this.chkTicketInterno.UseVisualStyleBackColor = true;
            // 
            // frmTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 460);
            this.Controls.Add(this.chkTicketInterno);
            this.Controls.Add(this.gbConCargo);
            this.Controls.Add(this.grdTicketDetalle);
            this.Controls.Add(this.btnGuardarTicket);
            this.Controls.Add(this.btnCancelarTicket);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTicket";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ticket";
            this.Load += new System.EventHandler(this.frmTicket_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTicketDetalle)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.gbConCargo.ResumeLayout(false);
            this.gbConCargo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNTicket;
        private System.Windows.Forms.TextBox txtNServicio;
        private System.Windows.Forms.Button btnCancelarTicket;
        private System.Windows.Forms.Button btnGuardarTicket;
        private System.Windows.Forms.TextBox txtFecha;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdTicketDetalle;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblRecordCount2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox gbConCargo;
        private System.Windows.Forms.RadioButton rbPaciente;
        private System.Windows.Forms.RadioButton rbMedicoTratante;
        private System.Windows.Forms.ComboBox cboTipoCuenta;
        private System.Windows.Forms.CheckBox chkTicketInterno;
    }
}