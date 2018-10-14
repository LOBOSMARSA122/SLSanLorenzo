namespace Sigesoft.Node.WinClient.UI.Reports
{
    partial class frmReporteGerencia
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbValorizado = new System.Windows.Forms.RadioButton();
            this.rbCantidad = new System.Windows.Forms.RadioButton();
            this.ddlMasterServiceId = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cboTipoEmpresa = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ddlServiceTypeId = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ddlEsoType = new System.Windows.Forms.ComboBox();
            this.dptDateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTimeStar = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.crvGerencia = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.ddlMasterServiceId);
            this.groupBox1.Controls.Add(this.btnFilter);
            this.groupBox1.Controls.Add(this.cboTipoEmpresa);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.ddlServiceTypeId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.ddlEsoType);
            this.groupBox1.Controls.Add(this.dptDateTimeEnd);
            this.groupBox1.Controls.Add(this.dtpDateTimeStar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(10, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1227, 89);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbValorizado);
            this.groupBox2.Controls.Add(this.rbCantidad);
            this.groupBox2.Location = new System.Drawing.Point(15, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(202, 33);
            this.groupBox2.TabIndex = 107;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modo";
            // 
            // rbValorizado
            // 
            this.rbValorizado.AutoSize = true;
            this.rbValorizado.Location = new System.Drawing.Point(119, 10);
            this.rbValorizado.Name = "rbValorizado";
            this.rbValorizado.Size = new System.Drawing.Size(74, 17);
            this.rbValorizado.TabIndex = 1;
            this.rbValorizado.Text = "Valorizado";
            this.rbValorizado.UseVisualStyleBackColor = true;
            // 
            // rbCantidad
            // 
            this.rbCantidad.AutoSize = true;
            this.rbCantidad.Checked = true;
            this.rbCantidad.Location = new System.Drawing.Point(7, 10);
            this.rbCantidad.Name = "rbCantidad";
            this.rbCantidad.Size = new System.Drawing.Size(85, 17);
            this.rbCantidad.TabIndex = 0;
            this.rbCantidad.TabStop = true;
            this.rbCantidad.Text = "Por Cantidad";
            this.rbCantidad.UseVisualStyleBackColor = true;
            // 
            // ddlMasterServiceId
            // 
            this.ddlMasterServiceId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlMasterServiceId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlMasterServiceId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMasterServiceId.Enabled = false;
            this.ddlMasterServiceId.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlMasterServiceId.FormattingEnabled = true;
            this.ddlMasterServiceId.Location = new System.Drawing.Point(591, 19);
            this.ddlMasterServiceId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlMasterServiceId.Name = "ddlMasterServiceId";
            this.ddlMasterServiceId.Size = new System.Drawing.Size(144, 21);
            this.ddlMasterServiceId.TabIndex = 32;
            this.ddlMasterServiceId.SelectedIndexChanged += new System.EventHandler(this.ddlMasterServiceId_SelectedIndexChanged);
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
            this.btnFilter.Location = new System.Drawing.Point(1143, 51);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 24);
            this.btnFilter.TabIndex = 106;
            this.btnFilter.Text = "Filtrar";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // cboTipoEmpresa
            // 
            this.cboTipoEmpresa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboTipoEmpresa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTipoEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoEmpresa.DropDownWidth = 150;
            this.cboTipoEmpresa.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoEmpresa.FormattingEnabled = true;
            this.cboTipoEmpresa.Items.AddRange(new object[] {
            "--Seleccionar--",
            "Cliente",
            "Empleadora",
            "Trabajo"});
            this.cboTipoEmpresa.Location = new System.Drawing.Point(1074, 21);
            this.cboTipoEmpresa.Margin = new System.Windows.Forms.Padding(2);
            this.cboTipoEmpresa.Name = "cboTipoEmpresa";
            this.cboTipoEmpresa.Size = new System.Drawing.Size(144, 21);
            this.cboTipoEmpresa.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(543, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Servicio";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(978, 25);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Tipo de Empresa";
            // 
            // ddlServiceTypeId
            // 
            this.ddlServiceTypeId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlServiceTypeId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlServiceTypeId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlServiceTypeId.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlServiceTypeId.FormattingEnabled = true;
            this.ddlServiceTypeId.Location = new System.Drawing.Point(386, 18);
            this.ddlServiceTypeId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlServiceTypeId.Name = "ddlServiceTypeId";
            this.ddlServiceTypeId.Size = new System.Drawing.Size(144, 21);
            this.ddlServiceTypeId.TabIndex = 30;
            this.ddlServiceTypeId.SelectedIndexChanged += new System.EventHandler(this.ddlServiceTypeId_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(313, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Tipo Servicio";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(756, 23);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Tipo E.S.O";
            // 
            // ddlEsoType
            // 
            this.ddlEsoType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlEsoType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlEsoType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlEsoType.Enabled = false;
            this.ddlEsoType.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlEsoType.FormattingEnabled = true;
            this.ddlEsoType.Location = new System.Drawing.Point(812, 21);
            this.ddlEsoType.Margin = new System.Windows.Forms.Padding(2);
            this.ddlEsoType.Name = "ddlEsoType";
            this.ddlEsoType.Size = new System.Drawing.Size(144, 21);
            this.ddlEsoType.TabIndex = 17;
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
            // crvGerencia
            // 
            this.crvGerencia.ActiveViewIndex = -1;
            this.crvGerencia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crvGerencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvGerencia.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvGerencia.Location = new System.Drawing.Point(12, 105);
            this.crvGerencia.Name = "crvGerencia";
            this.crvGerencia.ShowLogo = false;
            this.crvGerencia.Size = new System.Drawing.Size(1216, 553);
            this.crvGerencia.TabIndex = 48;
            this.crvGerencia.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmReporteGerencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1248, 670);
            this.Controls.Add(this.crvGerencia);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmReporteGerencia";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte Gerencia";
            this.Load += new System.EventHandler(this.frmReporteGerencia_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ddlMasterServiceId;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox cboTipoEmpresa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox ddlServiceTypeId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox ddlEsoType;
        private System.Windows.Forms.DateTimePicker dptDateTimeEnd;
        private System.Windows.Forms.DateTimePicker dtpDateTimeStar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvGerencia;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbValorizado;
        private System.Windows.Forms.RadioButton rbCantidad;
    }
}