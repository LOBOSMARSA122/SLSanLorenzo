namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    partial class frmHospitalizados_
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_HopitalizacionId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Paciente");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_FechaIngreso");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_FechaAlta");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_PrecioTotal");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Comentario");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Servicios");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Habitaciones");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Servicios", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ServiceId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_ServiceDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProtocolName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tickets");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Componentes");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Tickets", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_TicketId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_Fecha");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Productos");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Productos", 2);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Descripcion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_Cantidad");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EsDespachado");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_PrecioVenta");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Total");
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings1 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Sum, null, "Total", 4, true, "Productos", 3, Infragistics.Win.UltraWinGrid.SummaryPosition.Right, null, -1, false);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Componentes", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Categoria");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Componente");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MedicoTratante");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Precio");
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings2 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Sum, null, "Precio", 3, true, "Componentes", 4, Infragistics.Win.UltraWinGrid.SummaryPosition.Right, null, -1, false);
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand6 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Habitaciones", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NroHabitacion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_StartDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_EndDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_Precio");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Total");
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings3 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Sum, null, "Total", 4, true, "Habitaciones", 5, Infragistics.Win.UltraWinGrid.SummaryPosition.Right, null, -1, false);
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("d_CreationDate");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("d_ServiceDate");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("v_UpdateUser");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn4 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("d_UpdateDate");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn5 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("v_MasterServiceName");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn6 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("v_ServiceStatusName");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn7 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("v_OrganizationName");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn8 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("v_LocationName");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn9 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("v_ServiceTypeName");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn10 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("v_ProtocolName");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn11 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("v_Pacient");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn12 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("v_AptitudeStatusName");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn13 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("d_FechaEntrega");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn14 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Liq");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn15 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Moneda");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn16 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Valor");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn17 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Diagnosticos");
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtPacient = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dptDateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTimeStar = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.grdData = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnDarAlta = new System.Windows.Forms.Button();
            this.btnReportePDF = new System.Windows.Forms.Button();
            this.btnEditarHabitacion = new System.Windows.Forms.Button();
            this.btnAsignarHabitacion = new System.Windows.Forms.Button();
            this.btnAgregarExamenes = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnEliminarTicket = new System.Windows.Forms.Button();
            this.btnEditarTicket = new System.Windows.Forms.Button();
            this.btnTicket = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFilter);
            this.groupBox1.Controls.Add(this.txtPacient);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dptDateTimeEnd);
            this.groupBox1.Controls.Add(this.dtpDateTimeStar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(11, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1227, 54);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro";
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.SystemColors.Control;
            this.btnFilter.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFilter.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFilter.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Image = global::Sigesoft.Node.WinClient.UI.Resources.find;
            this.btnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilter.Location = new System.Drawing.Point(730, 16);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 24);
            this.btnFilter.TabIndex = 106;
            this.btnFilter.Text = "Filtrar";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtPacient
            // 
            this.txtPacient.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPacient.Location = new System.Drawing.Point(470, 17);
            this.txtPacient.Margin = new System.Windows.Forms.Padding(2);
            this.txtPacient.Name = "txtPacient";
            this.txtPacient.Size = new System.Drawing.Size(230, 21);
            this.txtPacient.TabIndex = 9;
            this.txtPacient.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHospitalizados_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(335, 21);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Paciente / Nro Documento";
            // 
            // dptDateTimeEnd
            // 
            this.dptDateTimeEnd.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dptDateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dptDateTimeEnd.Location = new System.Drawing.Point(209, 17);
            this.dptDateTimeEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dptDateTimeEnd.Name = "dptDateTimeEnd";
            this.dptDateTimeEnd.Size = new System.Drawing.Size(95, 21);
            this.dptDateTimeEnd.TabIndex = 3;
            this.dptDateTimeEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.calendar2Hospitalizados_KeyPress);
            // 
            // dtpDateTimeStar
            // 
            this.dtpDateTimeStar.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateTimeStar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTimeStar.Location = new System.Drawing.Point(96, 17);
            this.dtpDateTimeStar.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDateTimeStar.Name = "dtpDateTimeStar";
            this.dtpDateTimeStar.Size = new System.Drawing.Size(95, 21);
            this.dtpDateTimeStar.TabIndex = 2;
            this.dtpDateTimeStar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.calendar1Hospitalizados_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(194, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha Atención";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.lblRecordCount);
            this.groupBox2.Controls.Add(this.grdData);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(6, 80);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(1128, 490);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista de Hopitalizados";
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCount.Location = new System.Drawing.Point(886, 8);
            this.lblRecordCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(231, 19);
            this.lblRecordCount.TabIndex = 52;
            this.lblRecordCount.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdData.DisplayLayout.Appearance = appearance1;
            ultraGridColumn16.Header.Caption = "Nro. Hospitalización";
            ultraGridColumn16.Header.VisiblePosition = 0;
            ultraGridColumn16.Width = 177;
            ultraGridColumn17.Header.Caption = "Nombre de Paciente";
            ultraGridColumn17.Header.VisiblePosition = 1;
            ultraGridColumn17.Width = 146;
            ultraGridColumn18.Header.Caption = "Fecha Ingreso";
            ultraGridColumn18.Header.VisiblePosition = 2;
            ultraGridColumn19.Header.Caption = "Fecha Alta";
            ultraGridColumn19.Header.VisiblePosition = 3;
            ultraGridColumn20.Header.Caption = "Precio Total";
            ultraGridColumn20.Header.VisiblePosition = 4;
            ultraGridColumn1.Header.VisiblePosition = 5;
            ultraGridColumn21.Header.VisiblePosition = 6;
            ultraGridColumn22.Header.VisiblePosition = 7;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn1,
            ultraGridColumn21,
            ultraGridColumn22});
            ultraGridColumn23.Header.Caption = "Nro. Servicio";
            ultraGridColumn23.Header.VisiblePosition = 0;
            ultraGridColumn24.Header.Caption = "Fecha Servicio";
            ultraGridColumn24.Header.VisiblePosition = 1;
            ultraGridColumn25.Header.Caption = "Protocolo";
            ultraGridColumn25.Header.VisiblePosition = 2;
            ultraGridColumn26.Header.VisiblePosition = 3;
            ultraGridColumn27.Header.VisiblePosition = 4;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27});
            ultraGridColumn28.Header.Caption = "Nro. Ticket";
            ultraGridColumn28.Header.VisiblePosition = 0;
            ultraGridColumn29.Header.Caption = "Fecha";
            ultraGridColumn29.Header.VisiblePosition = 1;
            ultraGridColumn30.Header.VisiblePosition = 2;
            ultraGridColumn30.Hidden = true;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30});
            ultraGridColumn31.Header.Caption = "Producto";
            ultraGridColumn31.Header.VisiblePosition = 0;
            ultraGridColumn32.Header.Caption = "Cantidad";
            ultraGridColumn32.Header.VisiblePosition = 2;
            ultraGridColumn33.Header.VisiblePosition = 1;
            ultraGridColumn37.Header.VisiblePosition = 3;
            ultraGridColumn39.Header.VisiblePosition = 4;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn37,
            ultraGridColumn39});
            summarySettings1.GroupBySummaryValueAppearance = appearance2;
            ultraGridBand4.Summaries.AddRange(new Infragistics.Win.UltraWinGrid.SummarySettings[] {
            summarySettings1});
            ultraGridColumn48.Header.VisiblePosition = 0;
            ultraGridColumn51.Header.VisiblePosition = 1;
            ultraGridColumn52.Header.Caption = "Medico Tratante";
            ultraGridColumn52.Header.VisiblePosition = 2;
            ultraGridColumn53.Header.VisiblePosition = 3;
            ultraGridBand5.Columns.AddRange(new object[] {
            ultraGridColumn48,
            ultraGridColumn51,
            ultraGridColumn52,
            ultraGridColumn53});
            summarySettings2.GroupBySummaryValueAppearance = appearance3;
            ultraGridBand5.Summaries.AddRange(new Infragistics.Win.UltraWinGrid.SummarySettings[] {
            summarySettings2});
            ultraGridBand5.SummaryFooterCaption = "Summaries for [BANDHEADER]: [SCROLLTIPFIELD]";
            ultraGridColumn54.Header.Caption = "Nro. Habitacion";
            ultraGridColumn54.Header.VisiblePosition = 0;
            ultraGridColumn55.Header.Caption = "Fecha Inicio";
            ultraGridColumn55.Header.VisiblePosition = 1;
            ultraGridColumn56.Header.Caption = "Fecha Fin";
            ultraGridColumn56.Header.VisiblePosition = 2;
            ultraGridColumn57.Header.Caption = "Precio";
            ultraGridColumn57.Header.VisiblePosition = 3;
            ultraGridColumn58.Header.VisiblePosition = 4;
            ultraGridBand6.Columns.AddRange(new object[] {
            ultraGridColumn54,
            ultraGridColumn55,
            ultraGridColumn56,
            ultraGridColumn57,
            ultraGridColumn58});
            summarySettings3.GroupBySummaryValueAppearance = appearance4;
            ultraGridBand6.Summaries.AddRange(new Infragistics.Win.UltraWinGrid.SummarySettings[] {
            summarySettings3});
            ultraGridBand6.SummaryFooterCaption = "Summaries for [BANDHEADER]: [SCROLLTIPFIELD]";
            this.grdData.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdData.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdData.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.grdData.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            this.grdData.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            this.grdData.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
            this.grdData.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.grdData.DisplayLayout.InterBandSpacing = 10;
            this.grdData.DisplayLayout.MaxColScrollRegions = 1;
            this.grdData.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdData.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdData.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdData.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdData.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdData.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdData.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdData.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance5.BackColor = System.Drawing.Color.Transparent;
            this.grdData.DisplayLayout.Override.CardAreaAppearance = appearance5;
            appearance6.BackColor = System.Drawing.Color.White;
            appearance6.BackColor2 = System.Drawing.Color.White;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdData.DisplayLayout.Override.CellAppearance = appearance6;
            this.grdData.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.Color.White;
            appearance7.BackColor2 = System.Drawing.Color.LightGray;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance7.BorderColor = System.Drawing.Color.DarkGray;
            appearance7.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdData.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grdData.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance8.AlphaLevel = ((short)(187));
            appearance8.BackColor = System.Drawing.Color.Gainsboro;
            appearance8.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance8.ForeColor = System.Drawing.Color.Black;
            appearance8.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdData.DisplayLayout.Override.RowAlternateAppearance = appearance8;
            appearance9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdData.DisplayLayout.Override.RowSelectorAppearance = appearance9;
            this.grdData.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance10.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance10.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance10.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance10.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance10.FontData.BoldAsString = "False";
            appearance10.ForeColor = System.Drawing.Color.Black;
            this.grdData.DisplayLayout.Override.SelectedRowAppearance = appearance10;
            this.grdData.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdData.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdData.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdData.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdData.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdData.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grdData.Location = new System.Drawing.Point(14, 29);
            this.grdData.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(1103, 447);
            this.grdData.TabIndex = 44;
            this.grdData.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grdData_InitializeLayout);
            this.grdData.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.grdData_InitializeRow);
            this.grdData.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grd_AfterSelectChange);
            // 
            // ultraDataSource1
            // 
            this.ultraDataSource1.Band.Columns.AddRange(new object[] {
            ultraDataColumn1,
            ultraDataColumn2,
            ultraDataColumn3,
            ultraDataColumn4,
            ultraDataColumn5,
            ultraDataColumn6,
            ultraDataColumn7,
            ultraDataColumn8,
            ultraDataColumn9,
            ultraDataColumn10,
            ultraDataColumn11,
            ultraDataColumn12,
            ultraDataColumn13,
            ultraDataColumn14,
            ultraDataColumn15,
            ultraDataColumn16,
            ultraDataColumn17});
            // 
            // btnDarAlta
            // 
            this.btnDarAlta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDarAlta.BackColor = System.Drawing.SystemColors.Control;
            this.btnDarAlta.Enabled = false;
            this.btnDarAlta.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnDarAlta.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnDarAlta.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnDarAlta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDarAlta.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDarAlta.ForeColor = System.Drawing.Color.Black;
            this.btnDarAlta.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.btnDarAlta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDarAlta.Location = new System.Drawing.Point(1142, 299);
            this.btnDarAlta.Margin = new System.Windows.Forms.Padding(2);
            this.btnDarAlta.Name = "btnDarAlta";
            this.btnDarAlta.Size = new System.Drawing.Size(85, 25);
            this.btnDarAlta.TabIndex = 108;
            this.btnDarAlta.Text = "Dar de Alta";
            this.btnDarAlta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDarAlta.UseVisualStyleBackColor = false;
            this.btnDarAlta.Click += new System.EventHandler(this.btnDarAlta_Click);
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
            this.btnReportePDF.Location = new System.Drawing.Point(1142, 472);
            this.btnReportePDF.Margin = new System.Windows.Forms.Padding(2);
            this.btnReportePDF.Name = "btnReportePDF";
            this.btnReportePDF.Size = new System.Drawing.Size(85, 46);
            this.btnReportePDF.TabIndex = 107;
            this.btnReportePDF.Text = "Reporte";
            this.btnReportePDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportePDF.UseVisualStyleBackColor = false;
            this.btnReportePDF.Click += new System.EventHandler(this.btnReportePDF_Click);
            // 
            // btnEditarHabitacion
            // 
            this.btnEditarHabitacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditarHabitacion.BackColor = System.Drawing.SystemColors.Control;
            this.btnEditarHabitacion.Enabled = false;
            this.btnEditarHabitacion.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnEditarHabitacion.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEditarHabitacion.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnEditarHabitacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarHabitacion.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarHabitacion.ForeColor = System.Drawing.Color.Black;
            this.btnEditarHabitacion.Image = global::Sigesoft.Node.WinClient.UI.Resources.book_edit;
            this.btnEditarHabitacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditarHabitacion.Location = new System.Drawing.Point(1142, 399);
            this.btnEditarHabitacion.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditarHabitacion.Name = "btnEditarHabitacion";
            this.btnEditarHabitacion.Size = new System.Drawing.Size(85, 46);
            this.btnEditarHabitacion.TabIndex = 106;
            this.btnEditarHabitacion.Text = "Editar Habitación";
            this.btnEditarHabitacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditarHabitacion.UseVisualStyleBackColor = false;
            this.btnEditarHabitacion.Click += new System.EventHandler(this.btnEditarHabitacion_Click);
            // 
            // btnAsignarHabitacion
            // 
            this.btnAsignarHabitacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAsignarHabitacion.BackColor = System.Drawing.SystemColors.Control;
            this.btnAsignarHabitacion.Enabled = false;
            this.btnAsignarHabitacion.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAsignarHabitacion.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAsignarHabitacion.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAsignarHabitacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsignarHabitacion.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarHabitacion.ForeColor = System.Drawing.Color.Black;
            this.btnAsignarHabitacion.Image = global::Sigesoft.Node.WinClient.UI.Resources.book_open;
            this.btnAsignarHabitacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAsignarHabitacion.Location = new System.Drawing.Point(1142, 349);
            this.btnAsignarHabitacion.Margin = new System.Windows.Forms.Padding(2);
            this.btnAsignarHabitacion.Name = "btnAsignarHabitacion";
            this.btnAsignarHabitacion.Size = new System.Drawing.Size(85, 46);
            this.btnAsignarHabitacion.TabIndex = 105;
            this.btnAsignarHabitacion.Text = "Asignar Habitación";
            this.btnAsignarHabitacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAsignarHabitacion.UseVisualStyleBackColor = false;
            this.btnAsignarHabitacion.Click += new System.EventHandler(this.btnAsignarHabitacion_Click);
            // 
            // btnAgregarExamenes
            // 
            this.btnAgregarExamenes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarExamenes.BackColor = System.Drawing.SystemColors.Control;
            this.btnAgregarExamenes.Enabled = false;
            this.btnAgregarExamenes.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAgregarExamenes.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAgregarExamenes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAgregarExamenes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarExamenes.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarExamenes.ForeColor = System.Drawing.Color.Black;
            this.btnAgregarExamenes.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.add;
            this.btnAgregarExamenes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarExamenes.Location = new System.Drawing.Point(1142, 238);
            this.btnAgregarExamenes.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarExamenes.Name = "btnAgregarExamenes";
            this.btnAgregarExamenes.Size = new System.Drawing.Size(85, 46);
            this.btnAgregarExamenes.TabIndex = 104;
            this.btnAgregarExamenes.Text = "Agregar Examenes";
            this.btnAgregarExamenes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarExamenes.UseVisualStyleBackColor = false;
            this.btnAgregarExamenes.Click += new System.EventHandler(this.btnAgregarExamenes_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Enabled = false;
            this.btnExport.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_excel;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(1142, 523);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(85, 46);
            this.btnExport.TabIndex = 103;
            this.btnExport.Text = "Exportar a Excel";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnEliminarTicket
            // 
            this.btnEliminarTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminarTicket.BackColor = System.Drawing.SystemColors.Control;
            this.btnEliminarTicket.Enabled = false;
            this.btnEliminarTicket.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnEliminarTicket.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEliminarTicket.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnEliminarTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarTicket.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarTicket.ForeColor = System.Drawing.Color.Black;
            this.btnEliminarTicket.Image = global::Sigesoft.Node.WinClient.UI.Resources.application_delete;
            this.btnEliminarTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminarTicket.Location = new System.Drawing.Point(1142, 188);
            this.btnEliminarTicket.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminarTicket.Name = "btnEliminarTicket";
            this.btnEliminarTicket.Size = new System.Drawing.Size(85, 46);
            this.btnEliminarTicket.TabIndex = 53;
            this.btnEliminarTicket.Text = "Eliminar Ticket";
            this.btnEliminarTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminarTicket.UseVisualStyleBackColor = false;
            // 
            // btnEditarTicket
            // 
            this.btnEditarTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditarTicket.BackColor = System.Drawing.SystemColors.Control;
            this.btnEditarTicket.Enabled = false;
            this.btnEditarTicket.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnEditarTicket.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEditarTicket.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnEditarTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarTicket.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarTicket.ForeColor = System.Drawing.Color.Black;
            this.btnEditarTicket.Image = global::Sigesoft.Node.WinClient.UI.Resources.application_edit;
            this.btnEditarTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditarTicket.Location = new System.Drawing.Point(1142, 138);
            this.btnEditarTicket.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditarTicket.Name = "btnEditarTicket";
            this.btnEditarTicket.Size = new System.Drawing.Size(85, 46);
            this.btnEditarTicket.TabIndex = 52;
            this.btnEditarTicket.Text = "Editar       Ticket";
            this.btnEditarTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditarTicket.UseVisualStyleBackColor = false;
            this.btnEditarTicket.Click += new System.EventHandler(this.btnEditarTicket_Click);
            // 
            // btnTicket
            // 
            this.btnTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTicket.BackColor = System.Drawing.SystemColors.Control;
            this.btnTicket.Enabled = false;
            this.btnTicket.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnTicket.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnTicket.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTicket.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTicket.ForeColor = System.Drawing.Color.Black;
            this.btnTicket.Image = global::Sigesoft.Node.WinClient.UI.Resources.application_form_add;
            this.btnTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTicket.Location = new System.Drawing.Point(1142, 88);
            this.btnTicket.Margin = new System.Windows.Forms.Padding(2);
            this.btnTicket.Name = "btnTicket";
            this.btnTicket.Size = new System.Drawing.Size(85, 46);
            this.btnTicket.TabIndex = 51;
            this.btnTicket.Text = "Nuevo        Ticket";
            this.btnTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTicket.UseVisualStyleBackColor = false;
            this.btnTicket.Click += new System.EventHandler(this.btnTicket_Click);
            // 
            // frmHospitalizados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 581);
            this.Controls.Add(this.btnDarAlta);
            this.Controls.Add(this.btnReportePDF);
            this.Controls.Add(this.btnEditarHabitacion);
            this.Controls.Add(this.btnAsignarHabitacion);
            this.Controls.Add(this.btnAgregarExamenes);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnEliminarTicket);
            this.Controls.Add(this.btnEditarTicket);
            this.Controls.Add(this.btnTicket);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHospitalizados";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bandeja de Hospitalizados";
            this.Load += new System.EventHandler(this.frmHospitalizados_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtPacient;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dptDateTimeEnd;
        private System.Windows.Forms.DateTimePicker dtpDateTimeStar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.ComboBox ddlCalendarStatusId;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdData;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
        private System.Windows.Forms.Button btnTicket;
        private System.Windows.Forms.Button btnEditarTicket;
        private System.Windows.Forms.Button btnEliminarTicket;
        private System.Windows.Forms.Button btnExport;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.Button btnAgregarExamenes;
        private System.Windows.Forms.Button btnAsignarHabitacion;
        private System.Windows.Forms.Button btnEditarHabitacion;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnReportePDF;
        private System.Windows.Forms.Button btnDarAlta;
    }
}