namespace Sigesoft.Node.WinClient.UI
{
    partial class frmServiceOrderEdit
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProtocolName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_NumberOfWorkerProtocol");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_ProtocolPrice");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_Total");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServiceOrderEdit));
            this.label1 = new System.Windows.Forms.Label();
            this.txtNroDocument = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtProtocolName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkProtocoloEspecial = new System.Windows.Forms.CheckBox();
            this.cbLineaCredito = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbSi = new System.Windows.Forms.RadioButton();
            this.rbNo = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.txtComentary = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDelirevy = new System.Windows.Forms.DateTimePicker();
            this.ddlStatusOrderServiceId = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNroTrabajadores = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTotalTrabajadores = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.grdData1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txttypeProtocol = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAdress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOrganitation = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.txtDateTime = new System.Windows.Forms.TextBox();
            this.ultraValidator1 = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.btnReportePDF = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnSearchProtocol = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraValidator1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(584, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fecha de Emisión";
            // 
            // txtNroDocument
            // 
            this.txtNroDocument.BackColor = System.Drawing.Color.Gray;
            this.txtNroDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNroDocument.ForeColor = System.Drawing.Color.White;
            this.txtNroDocument.Location = new System.Drawing.Point(753, 22);
            this.txtNroDocument.Margin = new System.Windows.Forms.Padding(2);
            this.txtNroDocument.MaxLength = 20;
            this.txtNroDocument.Name = "txtNroDocument";
            this.txtNroDocument.ReadOnly = true;
            this.txtNroDocument.Size = new System.Drawing.Size(136, 21);
            this.txtNroDocument.TabIndex = 13;
            this.txtNroDocument.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.Black;
            this.label36.Location = new System.Drawing.Point(5, 22);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(52, 13);
            this.label36.TabIndex = 33;
            this.label36.Text = "Protocolo";
            // 
            // txtProtocolName
            // 
            this.txtProtocolName.BackColor = System.Drawing.Color.White;
            this.txtProtocolName.Enabled = false;
            this.txtProtocolName.ForeColor = System.Drawing.Color.Black;
            this.txtProtocolName.Location = new System.Drawing.Point(91, 19);
            this.txtProtocolName.Margin = new System.Windows.Forms.Padding(2);
            this.txtProtocolName.Name = "txtProtocolName";
            this.txtProtocolName.ReadOnly = true;
            this.txtProtocolName.Size = new System.Drawing.Size(702, 20);
            this.txtProtocolName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.chkProtocoloEspecial);
            this.groupBox1.Controls.Add(this.cbLineaCredito);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtComentary);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dtpDelirevy);
            this.groupBox1.Controls.Add(this.ddlStatusOrderServiceId);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox1.Location = new System.Drawing.Point(13, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 75);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de la Orden de Servicio";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Bisque;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(564, 20);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 13);
            this.label13.TabIndex = 49;
            this.label13.Text = "Mostrar Precio";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // chkProtocoloEspecial
            // 
            this.chkProtocoloEspecial.AutoSize = true;
            this.chkProtocoloEspecial.Location = new System.Drawing.Point(736, 17);
            this.chkProtocoloEspecial.Name = "chkProtocoloEspecial";
            this.chkProtocoloEspecial.Size = new System.Drawing.Size(132, 17);
            this.chkProtocoloEspecial.TabIndex = 108;
            this.chkProtocoloEspecial.Text = "Protocolo Especial";
            this.chkProtocoloEspecial.UseVisualStyleBackColor = true;
            // 
            // cbLineaCredito
            // 
            this.cbLineaCredito.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLineaCredito.DropDownWidth = 150;
            this.cbLineaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLineaCredito.FormattingEnabled = true;
            this.cbLineaCredito.Location = new System.Drawing.Point(458, 17);
            this.cbLineaCredito.Margin = new System.Windows.Forms.Padding(2);
            this.cbLineaCredito.Name = "cbLineaCredito";
            this.cbLineaCredito.Size = new System.Drawing.Size(94, 21);
            this.cbLineaCredito.TabIndex = 48;
            this.ultraValidator1.GetValidationSettings(this.cbLineaCredito).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.ultraValidator1.GetValidationSettings(this.cbLineaCredito).IsRequired = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Bisque;
            this.panel1.Controls.Add(this.rbSi);
            this.panel1.Controls.Add(this.rbNo);
            this.panel1.Location = new System.Drawing.Point(557, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 26);
            this.panel1.TabIndex = 107;
            // 
            // rbSi
            // 
            this.rbSi.AutoSize = true;
            this.rbSi.Checked = true;
            this.rbSi.Location = new System.Drawing.Point(84, 5);
            this.rbSi.Name = "rbSi";
            this.rbSi.Size = new System.Drawing.Size(37, 17);
            this.rbSi.TabIndex = 105;
            this.rbSi.TabStop = true;
            this.rbSi.Text = "SÍ";
            this.rbSi.UseVisualStyleBackColor = true;
            // 
            // rbNo
            // 
            this.rbNo.AutoSize = true;
            this.rbNo.Location = new System.Drawing.Point(125, 5);
            this.rbNo.Name = "rbNo";
            this.rbNo.Size = new System.Drawing.Size(43, 17);
            this.rbNo.TabIndex = 106;
            this.rbNo.TabStop = true;
            this.rbNo.Text = "NO";
            this.rbNo.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(368, 21);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 47;
            this.label10.Text = "Línea de Crédito";
            // 
            // txtComentary
            // 
            this.txtComentary.Location = new System.Drawing.Point(107, 41);
            this.txtComentary.Multiline = true;
            this.txtComentary.Name = "txtComentary";
            this.txtComentary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComentary.Size = new System.Drawing.Size(767, 28);
            this.txtComentary.TabIndex = 46;
            this.txtComentary.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(11, 41);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Comentario";
            this.label6.Visible = false;
            // 
            // dtpDelirevy
            // 
            this.dtpDelirevy.Checked = false;
            this.dtpDelirevy.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDelirevy.Location = new System.Drawing.Point(107, 18);
            this.dtpDelirevy.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDelirevy.Name = "dtpDelirevy";
            this.dtpDelirevy.ShowCheckBox = true;
            this.dtpDelirevy.Size = new System.Drawing.Size(114, 20);
            this.dtpDelirevy.TabIndex = 44;
            // 
            // ddlStatusOrderServiceId
            // 
            this.ddlStatusOrderServiceId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStatusOrderServiceId.DropDownWidth = 150;
            this.ddlStatusOrderServiceId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlStatusOrderServiceId.FormattingEnabled = true;
            this.ddlStatusOrderServiceId.Location = new System.Drawing.Point(269, 18);
            this.ddlStatusOrderServiceId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlStatusOrderServiceId.Name = "ddlStatusOrderServiceId";
            this.ddlStatusOrderServiceId.Size = new System.Drawing.Size(94, 21);
            this.ddlStatusOrderServiceId.TabIndex = 42;
            this.ultraValidator1.GetValidationSettings(this.ddlStatusOrderServiceId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.ultraValidator1.GetValidationSettings(this.ddlStatusOrderServiceId).IsRequired = true;
            this.ddlStatusOrderServiceId.SelectedIndexChanged += new System.EventHandler(this.ddlStatusOrderServiceId_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(225, 21);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 41;
            this.label11.Text = "Estado";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(11, 21);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "Fecha de Entrega";
            // 
            // txtNroTrabajadores
            // 
            this.txtNroTrabajadores.Location = new System.Drawing.Point(91, 89);
            this.txtNroTrabajadores.Name = "txtNroTrabajadores";
            this.txtNroTrabajadores.Size = new System.Drawing.Size(46, 20);
            this.txtNroTrabajadores.TabIndex = 43;
            this.txtNroTrabajadores.TextChanged += new System.EventHandler(this.txtNroTrabajadores_TextChanged);
            this.txtNroTrabajadores.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNroTrabajadores_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(4, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "N° Trabajadores";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSendEmail);
            this.groupBox2.Controls.Add(this.btnReportePDF);
            this.groupBox2.Controls.Add(this.txtTotalTrabajadores);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.grdData1);
            this.groupBox2.Controls.Add(this.btnAgregar);
            this.groupBox2.Controls.Add(this.btnSearchProtocol);
            this.groupBox2.Controls.Add(this.txtNroTrabajadores);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtTotal);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label36);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txttypeProtocol);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtAdress);
            this.groupBox2.Controls.Add(this.txtProtocolName);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtContact);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtOrganitation);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox2.Location = new System.Drawing.Point(13, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(880, 348);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalle de la Orden de Servicio";
            // 
            // txtTotalTrabajadores
            // 
            this.txtTotalTrabajadores.Enabled = false;
            this.txtTotalTrabajadores.ForeColor = System.Drawing.Color.Black;
            this.txtTotalTrabajadores.Location = new System.Drawing.Point(680, 317);
            this.txtTotalTrabajadores.Name = "txtTotalTrabajadores";
            this.txtTotalTrabajadores.ReadOnly = true;
            this.txtTotalTrabajadores.Size = new System.Drawing.Size(46, 20);
            this.txtTotalTrabajadores.TabIndex = 59;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(564, 320);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 13);
            this.label12.TabIndex = 58;
            this.label12.Text = "Total de Trabajadores";
            // 
            // grdData1
            // 
            this.grdData1.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.LightGray;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdData1.DisplayLayout.Appearance = appearance1;
            ultraGridColumn5.Header.Caption = "Protocolo";
            ultraGridColumn5.Header.VisiblePosition = 0;
            ultraGridColumn5.Width = 475;
            ultraGridColumn2.Header.Caption = "Cantidad de Trabajadores";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn1.Header.Caption = "Precio Unitario";
            ultraGridColumn1.Header.VisiblePosition = 2;
            ultraGridColumn7.Header.Caption = "Total";
            ultraGridColumn7.Header.VisiblePosition = 3;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn5,
            ultraGridColumn2,
            ultraGridColumn1,
            ultraGridColumn7});
            this.grdData1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdData1.DisplayLayout.InterBandSpacing = 10;
            this.grdData1.DisplayLayout.MaxColScrollRegions = 1;
            this.grdData1.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdData1.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdData1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdData1.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdData1.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdData1.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdData1.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdData1.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdData1.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdData1.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdData1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdData1.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdData1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdData1.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdData1.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdData1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdData1.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdData1.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdData1.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdData1.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdData1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdData1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdData1.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdData1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdData1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdData1.Location = new System.Drawing.Point(5, 115);
            this.grdData1.Margin = new System.Windows.Forms.Padding(2);
            this.grdData1.Name = "grdData1";
            this.grdData1.Size = new System.Drawing.Size(870, 197);
            this.grdData1.TabIndex = 57;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(757, 319);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "Total";
            // 
            // txtTotal
            // 
            this.txtTotal.ForeColor = System.Drawing.Color.Black;
            this.txtTotal.Location = new System.Drawing.Point(793, 316);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(2);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(63, 20);
            this.txtTotal.TabIndex = 48;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(391, 68);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "Tipo de Protocolo";
            // 
            // txttypeProtocol
            // 
            this.txttypeProtocol.BackColor = System.Drawing.Color.White;
            this.txttypeProtocol.Enabled = false;
            this.txttypeProtocol.ForeColor = System.Drawing.Color.Black;
            this.txttypeProtocol.Location = new System.Drawing.Point(501, 65);
            this.txttypeProtocol.Margin = new System.Windows.Forms.Padding(2);
            this.txttypeProtocol.Name = "txttypeProtocol";
            this.txttypeProtocol.ReadOnly = true;
            this.txttypeProtocol.Size = new System.Drawing.Size(292, 20);
            this.txttypeProtocol.TabIndex = 40;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(4, 68);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Dirección";
            // 
            // txtAdress
            // 
            this.txtAdress.BackColor = System.Drawing.Color.White;
            this.txtAdress.Enabled = false;
            this.txtAdress.ForeColor = System.Drawing.Color.Black;
            this.txtAdress.Location = new System.Drawing.Point(91, 65);
            this.txtAdress.Margin = new System.Windows.Forms.Padding(2);
            this.txtAdress.Name = "txtAdress";
            this.txtAdress.ReadOnly = true;
            this.txtAdress.Size = new System.Drawing.Size(280, 20);
            this.txtAdress.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(391, 44);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Representante Legal";
            // 
            // txtContact
            // 
            this.txtContact.BackColor = System.Drawing.Color.White;
            this.txtContact.Enabled = false;
            this.txtContact.ForeColor = System.Drawing.Color.Black;
            this.txtContact.Location = new System.Drawing.Point(501, 41);
            this.txtContact.Margin = new System.Windows.Forms.Padding(2);
            this.txtContact.Name = "txtContact";
            this.txtContact.ReadOnly = true;
            this.txtContact.Size = new System.Drawing.Size(292, 20);
            this.txtContact.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(4, 44);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Empresa Cliente";
            // 
            // txtOrganitation
            // 
            this.txtOrganitation.BackColor = System.Drawing.Color.White;
            this.txtOrganitation.Enabled = false;
            this.txtOrganitation.ForeColor = System.Drawing.Color.Black;
            this.txtOrganitation.Location = new System.Drawing.Point(91, 41);
            this.txtOrganitation.Margin = new System.Windows.Forms.Padding(2);
            this.txtOrganitation.Name = "txtOrganitation";
            this.txtOrganitation.ReadOnly = true;
            this.txtOrganitation.Size = new System.Drawing.Size(280, 20);
            this.txtOrganitation.TabIndex = 1;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.White;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(753, 4);
            this.textBox6.Margin = new System.Windows.Forms.Padding(2);
            this.textBox6.MaxLength = 20;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(136, 20);
            this.textBox6.TabIndex = 47;
            this.textBox6.Text = "ORDEN DE SERVICIO";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDateTime
            // 
            this.txtDateTime.ForeColor = System.Drawing.Color.Black;
            this.txtDateTime.Location = new System.Drawing.Point(679, 19);
            this.txtDateTime.Margin = new System.Windows.Forms.Padding(2);
            this.txtDateTime.Name = "txtDateTime";
            this.txtDateTime.ReadOnly = true;
            this.txtDateTime.Size = new System.Drawing.Size(63, 20);
            this.txtDateTime.TabIndex = 43;
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGenerarReporte.BackColor = System.Drawing.SystemColors.Control;
            this.btnGenerarReporte.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGenerarReporte.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGenerarReporte.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnGenerarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporte.ForeColor = System.Drawing.Color.Black;
            this.btnGenerarReporte.Image = global::Sigesoft.Node.WinClient.UI.Resources.note_add;
            this.btnGenerarReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarReporte.Location = new System.Drawing.Point(615, 478);
            this.btnGenerarReporte.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(117, 26);
            this.btnGenerarReporte.TabIndex = 104;
            this.btnGenerarReporte.Text = "Generar Reporte";
            this.btnGenerarReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerarReporte.UseVisualStyleBackColor = false;
            this.btnGenerarReporte.Visible = false;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(815, 480);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 103;
            this.btnCancel.Text = "   Salir";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(736, 480);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 26);
            this.btnOK.TabIndex = 102;
            this.btnOK.Text = "      Guardar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.BackColor = System.Drawing.SystemColors.Control;
            this.btnSendEmail.Enabled = false;
            this.btnSendEmail.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSendEmail.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSendEmail.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSendEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendEmail.ForeColor = System.Drawing.Color.Black;
            this.btnSendEmail.Image = global::Sigesoft.Node.WinClient.UI.Resources.email_transfer;
            this.btnSendEmail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendEmail.Location = new System.Drawing.Point(188, 320);
            this.btnSendEmail.Margin = new System.Windows.Forms.Padding(2);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(103, 24);
            this.btnSendEmail.TabIndex = 106;
            this.btnSendEmail.Text = "Enviar Email";
            this.btnSendEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendEmail.UseVisualStyleBackColor = false;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click_1);
            // 
            // btnReportePDF
            // 
            this.btnReportePDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReportePDF.BackColor = System.Drawing.SystemColors.Control;
            this.btnReportePDF.Enabled = false;
            this.btnReportePDF.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnReportePDF.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnReportePDF.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnReportePDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportePDF.ForeColor = System.Drawing.Color.Black;
            this.btnReportePDF.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_white_acrobat;
            this.btnReportePDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportePDF.Location = new System.Drawing.Point(5, 320);
            this.btnReportePDF.Margin = new System.Windows.Forms.Padding(2);
            this.btnReportePDF.Name = "btnReportePDF";
            this.btnReportePDF.Size = new System.Drawing.Size(164, 26);
            this.btnReportePDF.TabIndex = 105;
            this.btnReportePDF.Text = "Generar Reporte PDF";
            this.btnReportePDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportePDF.UseVisualStyleBackColor = false;
            this.btnReportePDF.Click += new System.EventHandler(this.btnReportePDF_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.BackColor = System.Drawing.SystemColors.Control;
            this.btnAgregar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAgregar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAgregar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.Color.Black;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(718, 89);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 22);
            this.btnAgregar.TabIndex = 56;
            this.btnAgregar.Text = "&Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnSearchProtocol
            // 
            this.btnSearchProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearchProtocol.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearchProtocol.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSearchProtocol.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSearchProtocol.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSearchProtocol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchProtocol.ForeColor = System.Drawing.Color.Black;
            this.btnSearchProtocol.Image = global::Sigesoft.Node.WinClient.UI.Resources.find;
            this.btnSearchProtocol.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchProtocol.Location = new System.Drawing.Point(796, 26);
            this.btnSearchProtocol.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearchProtocol.Name = "btnSearchProtocol";
            this.btnSearchProtocol.Size = new System.Drawing.Size(75, 22);
            this.btnSearchProtocol.TabIndex = 55;
            this.btnSearchProtocol.Text = "&Buscar";
            this.btnSearchProtocol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchProtocol.UseVisualStyleBackColor = false;
            this.btnSearchProtocol.Click += new System.EventHandler(this.btnSearchProtocol_Click);
            // 
            // frmServiceOrderEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(905, 511);
            this.Controls.Add(this.btnGenerarReporte);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtDateTime);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtNroDocument);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmServiceOrderEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Orden de Servicio";
            this.Load += new System.EventHandler(this.frmServiceOrderEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraValidator1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNroDocument;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtProtocolName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txttypeProtocol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAdress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOrganitation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ddlStatusOrderServiceId;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox txtDateTime;
        private System.Windows.Forms.TextBox txtNroTrabajadores;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotal;
        private Infragistics.Win.Misc.UltraValidator ultraValidator1;
        private System.Windows.Forms.DateTimePicker dtpDelirevy;
        private System.Windows.Forms.TextBox txtComentary;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSearchProtocol;
        private System.Windows.Forms.Button btnAgregar;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdData1;
        private System.Windows.Forms.TextBox txtTotalTrabajadores;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbLineaCredito;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnGenerarReporte;
        private System.Windows.Forms.RadioButton rbSi;
        private System.Windows.Forms.RadioButton rbNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkProtocoloEspecial;
        private System.Windows.Forms.Button btnReportePDF;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnSendEmail;
    }
}