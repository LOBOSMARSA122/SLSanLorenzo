namespace Sigesoft.Node.WinClient.UI.Reports
{
    partial class frmManagementReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManagementReports));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRecordCount1 = new System.Windows.Forms.Label();
            this.chklExamenes = new System.Windows.Forms.CheckedListBox();
            this.rbSeleccioneExamenes = new System.Windows.Forms.RadioButton();
            this.rbTodosExamenes = new System.Windows.Forms.RadioButton();
            this.btnGenerarReporteExamenes = new System.Windows.Forms.Button();
            this.btnGenerarReporteFichasMedicas = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRecordCountFichaMedica = new System.Windows.Forms.Label();
            this.chklFichasMedicas = new System.Windows.Forms.CheckedListBox();
            this.rbSeleccioneFichaMedica = new System.Windows.Forms.RadioButton();
            this.btnGenerarReporteCertificados = new System.Windows.Forms.Button();
            this.rbTodosFichaMedica = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblRecordCountCertificados = new System.Windows.Forms.Label();
            this.chkCertificados = new System.Windows.Forms.CheckedListBox();
            this.rbSeleccioneCertificados = new System.Windows.Forms.RadioButton();
            this.rbTodosCertificados = new System.Windows.Forms.RadioButton();
            this.btnExportarPdf = new System.Windows.Forms.Button();
            this.chklConsolidadoReportes = new System.Windows.Forms.CheckedListBox();
            this.btnConsolidadoReportes = new System.Windows.Forms.Button();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblRecordCount1);
            this.groupBox1.Controls.Add(this.chklExamenes);
            this.groupBox1.Controls.Add(this.rbSeleccioneExamenes);
            this.groupBox1.Controls.Add(this.rbTodosExamenes);
            this.groupBox1.Location = new System.Drawing.Point(1104, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 266);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Examenes";
            // 
            // lblRecordCount1
            // 
            this.lblRecordCount1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount1.AutoSize = true;
            this.lblRecordCount1.Location = new System.Drawing.Point(336, 17);
            this.lblRecordCount1.Name = "lblRecordCount1";
            this.lblRecordCount1.Size = new System.Drawing.Size(142, 13);
            this.lblRecordCount1.TabIndex = 24;
            this.lblRecordCount1.Text = "Se encontraron {0} registros.";
            // 
            // chklExamenes
            // 
            this.chklExamenes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chklExamenes.CheckOnClick = true;
            this.chklExamenes.Enabled = false;
            this.chklExamenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklExamenes.FormattingEnabled = true;
            this.chklExamenes.Location = new System.Drawing.Point(100, 34);
            this.chklExamenes.Name = "chklExamenes";
            this.chklExamenes.Size = new System.Drawing.Size(379, 214);
            this.chklExamenes.TabIndex = 23;
            this.chklExamenes.SelectedValueChanged += new System.EventHandler(this.chklExamenes_SelectedValueChanged);
            // 
            // rbSeleccioneExamenes
            // 
            this.rbSeleccioneExamenes.AutoSize = true;
            this.rbSeleccioneExamenes.Location = new System.Drawing.Point(6, 45);
            this.rbSeleccioneExamenes.Name = "rbSeleccioneExamenes";
            this.rbSeleccioneExamenes.Size = new System.Drawing.Size(78, 17);
            this.rbSeleccioneExamenes.TabIndex = 1;
            this.rbSeleccioneExamenes.Text = "Seleccione";
            this.rbSeleccioneExamenes.UseVisualStyleBackColor = true;
            this.rbSeleccioneExamenes.CheckedChanged += new System.EventHandler(this.rbSeleccioneExamenes_CheckedChanged);
            // 
            // rbTodosExamenes
            // 
            this.rbTodosExamenes.AutoSize = true;
            this.rbTodosExamenes.Checked = true;
            this.rbTodosExamenes.Location = new System.Drawing.Point(6, 22);
            this.rbTodosExamenes.Name = "rbTodosExamenes";
            this.rbTodosExamenes.Size = new System.Drawing.Size(55, 17);
            this.rbTodosExamenes.TabIndex = 0;
            this.rbTodosExamenes.TabStop = true;
            this.rbTodosExamenes.Text = "Todos";
            this.rbTodosExamenes.UseVisualStyleBackColor = true;
            this.rbTodosExamenes.CheckedChanged += new System.EventHandler(this.rbTodosExamenes_CheckedChanged);
            // 
            // btnGenerarReporteExamenes
            // 
            this.btnGenerarReporteExamenes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarReporteExamenes.BackColor = System.Drawing.SystemColors.Control;
            this.btnGenerarReporteExamenes.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGenerarReporteExamenes.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGenerarReporteExamenes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnGenerarReporteExamenes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporteExamenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarReporteExamenes.ForeColor = System.Drawing.Color.Black;
            this.btnGenerarReporteExamenes.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarReporteExamenes.Image")));
            this.btnGenerarReporteExamenes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarReporteExamenes.Location = new System.Drawing.Point(213, -284);
            this.btnGenerarReporteExamenes.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerarReporteExamenes.Name = "btnGenerarReporteExamenes";
            this.btnGenerarReporteExamenes.Size = new System.Drawing.Size(85, 25);
            this.btnGenerarReporteExamenes.TabIndex = 115;
            this.btnGenerarReporteExamenes.Text = "&Generar";
            this.btnGenerarReporteExamenes.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnGenerarReporteExamenes.UseVisualStyleBackColor = false;
            this.btnGenerarReporteExamenes.Click += new System.EventHandler(this.btnGenerarReporteExamenes_Click);
            // 
            // btnGenerarReporteFichasMedicas
            // 
            this.btnGenerarReporteFichasMedicas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarReporteFichasMedicas.BackColor = System.Drawing.SystemColors.Control;
            this.btnGenerarReporteFichasMedicas.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGenerarReporteFichasMedicas.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGenerarReporteFichasMedicas.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnGenerarReporteFichasMedicas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporteFichasMedicas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarReporteFichasMedicas.ForeColor = System.Drawing.Color.Black;
            this.btnGenerarReporteFichasMedicas.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarReporteFichasMedicas.Image")));
            this.btnGenerarReporteFichasMedicas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarReporteFichasMedicas.Location = new System.Drawing.Point(213, -12);
            this.btnGenerarReporteFichasMedicas.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerarReporteFichasMedicas.Name = "btnGenerarReporteFichasMedicas";
            this.btnGenerarReporteFichasMedicas.Size = new System.Drawing.Size(85, 25);
            this.btnGenerarReporteFichasMedicas.TabIndex = 131;
            this.btnGenerarReporteFichasMedicas.Text = "&Generar";
            this.btnGenerarReporteFichasMedicas.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnGenerarReporteFichasMedicas.UseVisualStyleBackColor = false;
            this.btnGenerarReporteFichasMedicas.Click += new System.EventHandler(this.btnGenerarReporteFichasMedicas_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblRecordCountFichaMedica);
            this.groupBox2.Controls.Add(this.chklFichasMedicas);
            this.groupBox2.Controls.Add(this.rbSeleccioneFichaMedica);
            this.groupBox2.Controls.Add(this.btnGenerarReporteCertificados);
            this.groupBox2.Controls.Add(this.rbTodosFichaMedica);
            this.groupBox2.Controls.Add(this.btnGenerarReporteFichasMedicas);
            this.groupBox2.Controls.Add(this.btnGenerarReporteExamenes);
            this.groupBox2.Location = new System.Drawing.Point(1149, 299);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(484, 158);
            this.groupBox2.TabIndex = 132;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fichas Médicas";
            // 
            // lblRecordCountFichaMedica
            // 
            this.lblRecordCountFichaMedica.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCountFichaMedica.AutoSize = true;
            this.lblRecordCountFichaMedica.Location = new System.Drawing.Point(336, 18);
            this.lblRecordCountFichaMedica.Name = "lblRecordCountFichaMedica";
            this.lblRecordCountFichaMedica.Size = new System.Drawing.Size(142, 13);
            this.lblRecordCountFichaMedica.TabIndex = 24;
            this.lblRecordCountFichaMedica.Text = "Se encontraron {0} registros.";
            // 
            // chklFichasMedicas
            // 
            this.chklFichasMedicas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chklFichasMedicas.CheckOnClick = true;
            this.chklFichasMedicas.Enabled = false;
            this.chklFichasMedicas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklFichasMedicas.FormattingEnabled = true;
            this.chklFichasMedicas.Location = new System.Drawing.Point(100, 37);
            this.chklFichasMedicas.Name = "chklFichasMedicas";
            this.chklFichasMedicas.Size = new System.Drawing.Size(379, 109);
            this.chklFichasMedicas.TabIndex = 23;
            this.chklFichasMedicas.SelectedValueChanged += new System.EventHandler(this.chklFichasMedicas_SelectedValueChanged);
            // 
            // rbSeleccioneFichaMedica
            // 
            this.rbSeleccioneFichaMedica.AutoSize = true;
            this.rbSeleccioneFichaMedica.Location = new System.Drawing.Point(6, 45);
            this.rbSeleccioneFichaMedica.Name = "rbSeleccioneFichaMedica";
            this.rbSeleccioneFichaMedica.Size = new System.Drawing.Size(78, 17);
            this.rbSeleccioneFichaMedica.TabIndex = 1;
            this.rbSeleccioneFichaMedica.Text = "Seleccione";
            this.rbSeleccioneFichaMedica.UseVisualStyleBackColor = true;
            this.rbSeleccioneFichaMedica.CheckedChanged += new System.EventHandler(this.rbSeleccioneFichaMedica_CheckedChanged);
            // 
            // btnGenerarReporteCertificados
            // 
            this.btnGenerarReporteCertificados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarReporteCertificados.BackColor = System.Drawing.SystemColors.Control;
            this.btnGenerarReporteCertificados.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGenerarReporteCertificados.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGenerarReporteCertificados.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnGenerarReporteCertificados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporteCertificados.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarReporteCertificados.ForeColor = System.Drawing.Color.Black;
            this.btnGenerarReporteCertificados.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarReporteCertificados.Image")));
            this.btnGenerarReporteCertificados.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarReporteCertificados.Location = new System.Drawing.Point(213, 151);
            this.btnGenerarReporteCertificados.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerarReporteCertificados.Name = "btnGenerarReporteCertificados";
            this.btnGenerarReporteCertificados.Size = new System.Drawing.Size(85, 25);
            this.btnGenerarReporteCertificados.TabIndex = 133;
            this.btnGenerarReporteCertificados.Text = "&Generar";
            this.btnGenerarReporteCertificados.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnGenerarReporteCertificados.UseVisualStyleBackColor = false;
            this.btnGenerarReporteCertificados.Click += new System.EventHandler(this.btnGenerarReporteCertificados_Click);
            // 
            // rbTodosFichaMedica
            // 
            this.rbTodosFichaMedica.AutoSize = true;
            this.rbTodosFichaMedica.Checked = true;
            this.rbTodosFichaMedica.Location = new System.Drawing.Point(6, 22);
            this.rbTodosFichaMedica.Name = "rbTodosFichaMedica";
            this.rbTodosFichaMedica.Size = new System.Drawing.Size(55, 17);
            this.rbTodosFichaMedica.TabIndex = 0;
            this.rbTodosFichaMedica.TabStop = true;
            this.rbTodosFichaMedica.Text = "Todos";
            this.rbTodosFichaMedica.UseVisualStyleBackColor = true;
            this.rbTodosFichaMedica.CheckedChanged += new System.EventHandler(this.rbTodosFichaMedica_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblRecordCountCertificados);
            this.groupBox3.Controls.Add(this.chkCertificados);
            this.groupBox3.Controls.Add(this.rbSeleccioneCertificados);
            this.groupBox3.Controls.Add(this.rbTodosCertificados);
            this.groupBox3.Location = new System.Drawing.Point(1104, 410);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(0, 0);
            this.groupBox3.TabIndex = 134;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Certificados";
            // 
            // lblRecordCountCertificados
            // 
            this.lblRecordCountCertificados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCountCertificados.AutoSize = true;
            this.lblRecordCountCertificados.Location = new System.Drawing.Point(-142, 18);
            this.lblRecordCountCertificados.Name = "lblRecordCountCertificados";
            this.lblRecordCountCertificados.Size = new System.Drawing.Size(142, 13);
            this.lblRecordCountCertificados.TabIndex = 24;
            this.lblRecordCountCertificados.Text = "Se encontraron {0} registros.";
            // 
            // chkCertificados
            // 
            this.chkCertificados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCertificados.CheckOnClick = true;
            this.chkCertificados.Enabled = false;
            this.chkCertificados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCertificados.FormattingEnabled = true;
            this.chkCertificados.Location = new System.Drawing.Point(100, 37);
            this.chkCertificados.Name = "chkCertificados";
            this.chkCertificados.Size = new System.Drawing.Size(0, 4);
            this.chkCertificados.TabIndex = 23;
            this.chkCertificados.SelectedIndexChanged += new System.EventHandler(this.chkCertificados_SelectedIndexChanged);
            this.chkCertificados.SelectedValueChanged += new System.EventHandler(this.chkCertificados_SelectedValueChanged);
            // 
            // rbSeleccioneCertificados
            // 
            this.rbSeleccioneCertificados.AutoSize = true;
            this.rbSeleccioneCertificados.Location = new System.Drawing.Point(6, 45);
            this.rbSeleccioneCertificados.Name = "rbSeleccioneCertificados";
            this.rbSeleccioneCertificados.Size = new System.Drawing.Size(78, 17);
            this.rbSeleccioneCertificados.TabIndex = 1;
            this.rbSeleccioneCertificados.Text = "Seleccione";
            this.rbSeleccioneCertificados.UseVisualStyleBackColor = true;
            this.rbSeleccioneCertificados.CheckedChanged += new System.EventHandler(this.rbSeleccioneCertificados_CheckedChanged);
            // 
            // rbTodosCertificados
            // 
            this.rbTodosCertificados.AutoSize = true;
            this.rbTodosCertificados.Checked = true;
            this.rbTodosCertificados.Location = new System.Drawing.Point(6, 22);
            this.rbTodosCertificados.Name = "rbTodosCertificados";
            this.rbTodosCertificados.Size = new System.Drawing.Size(55, 17);
            this.rbTodosCertificados.TabIndex = 0;
            this.rbTodosCertificados.TabStop = true;
            this.rbTodosCertificados.Text = "Todos";
            this.rbTodosCertificados.UseVisualStyleBackColor = true;
            this.rbTodosCertificados.CheckedChanged += new System.EventHandler(this.rbTodosCertificados_CheckedChanged);
            // 
            // btnExportarPdf
            // 
            this.btnExportarPdf.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExportarPdf.BackColor = System.Drawing.SystemColors.Control;
            this.btnExportarPdf.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnExportarPdf.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnExportarPdf.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnExportarPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarPdf.ForeColor = System.Drawing.Color.Black;
            this.btnExportarPdf.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_white_acrobat;
            this.btnExportarPdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportarPdf.Location = new System.Drawing.Point(952, 453);
            this.btnExportarPdf.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportarPdf.Name = "btnExportarPdf";
            this.btnExportarPdf.Size = new System.Drawing.Size(102, 25);
            this.btnExportarPdf.TabIndex = 135;
            this.btnExportarPdf.Text = "&Generar PDF";
            this.btnExportarPdf.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnExportarPdf.UseVisualStyleBackColor = false;
            this.btnExportarPdf.Click += new System.EventHandler(this.btnExportarPdf_Click);
            // 
            // chklConsolidadoReportes
            // 
            this.chklConsolidadoReportes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chklConsolidadoReportes.CheckOnClick = true;
            this.chklConsolidadoReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklConsolidadoReportes.FormattingEnabled = true;
            this.chklConsolidadoReportes.Location = new System.Drawing.Point(16, 39);
            this.chklConsolidadoReportes.Name = "chklConsolidadoReportes";
            this.chklConsolidadoReportes.Size = new System.Drawing.Size(379, 274);
            this.chklConsolidadoReportes.TabIndex = 25;
            this.chklConsolidadoReportes.SelectedIndexChanged += new System.EventHandler(this.chklConsolidadoReportes_SelectedIndexChanged);
            // 
            // btnConsolidadoReportes
            // 
            this.btnConsolidadoReportes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnConsolidadoReportes.BackColor = System.Drawing.SystemColors.Control;
            this.btnConsolidadoReportes.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnConsolidadoReportes.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnConsolidadoReportes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnConsolidadoReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsolidadoReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsolidadoReportes.ForeColor = System.Drawing.Color.Black;
            this.btnConsolidadoReportes.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_white_acrobat;
            this.btnConsolidadoReportes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsolidadoReportes.Location = new System.Drawing.Point(293, 325);
            this.btnConsolidadoReportes.Margin = new System.Windows.Forms.Padding(2);
            this.btnConsolidadoReportes.Name = "btnConsolidadoReportes";
            this.btnConsolidadoReportes.Size = new System.Drawing.Size(102, 25);
            this.btnConsolidadoReportes.TabIndex = 136;
            this.btnConsolidadoReportes.Text = "&Generar PDF";
            this.btnConsolidadoReportes.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnConsolidadoReportes.UseVisualStyleBackColor = false;
            this.btnConsolidadoReportes.Click += new System.EventHandler(this.btnConsolidadoReportes_Click);
            // 
            // chkTodos
            // 
            this.chkTodos.AutoSize = true;
            this.chkTodos.Location = new System.Drawing.Point(6, 19);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(115, 17);
            this.chkTodos.TabIndex = 137;
            this.chkTodos.Text = "Seleccionar Todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkTodos);
            this.groupBox4.Controls.Add(this.btnConsolidadoReportes);
            this.groupBox4.Controls.Add(this.chklConsolidadoReportes);
            this.groupBox4.Location = new System.Drawing.Point(12, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(413, 367);
            this.groupBox4.TabIndex = 138;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Reportes Seleccionados";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // frmManagementReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 389);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnExportarPdf);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManagementReports";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Administrador de Reportes";
            this.Load += new System.EventHandler(this.frmManagementReports_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbTodosExamenes;
        private System.Windows.Forms.RadioButton rbSeleccioneExamenes;
        private System.Windows.Forms.CheckedListBox chklExamenes;
        private System.Windows.Forms.Label lblRecordCount1;
        private System.Windows.Forms.Button btnGenerarReporteExamenes;
        private System.Windows.Forms.Button btnGenerarReporteFichasMedicas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblRecordCountFichaMedica;
        private System.Windows.Forms.CheckedListBox chklFichasMedicas;
        private System.Windows.Forms.RadioButton rbSeleccioneFichaMedica;
        private System.Windows.Forms.RadioButton rbTodosFichaMedica;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblRecordCountCertificados;
        private System.Windows.Forms.CheckedListBox chkCertificados;
        private System.Windows.Forms.RadioButton rbSeleccioneCertificados;
        private System.Windows.Forms.RadioButton rbTodosCertificados;
        private System.Windows.Forms.Button btnGenerarReporteCertificados;
        private System.Windows.Forms.Button btnExportarPdf;
        private System.Windows.Forms.CheckedListBox chklConsolidadoReportes;
        private System.Windows.Forms.Button btnConsolidadoReportes;
        private System.Windows.Forms.CheckBox chkTodos;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}