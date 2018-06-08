namespace Sigesoft.Node.WinClient.UI.History
{
    partial class frmPersonMedicalPopup
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
            this.dtpDateTimeStar = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTreatmentSite = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDxDetail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.ddlTypeDiagnosticId = new System.Windows.Forms.ComboBox();
            this.uvPersonMedicalPopup = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txtHospital = new System.Windows.Forms.TextBox();
            this.lfdj = new System.Windows.Forms.Label();
            this.txtComplicaciones = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.uvPersonMedicalPopup)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDateTimeStar
            // 
            this.dtpDateTimeStar.CustomFormat = "MM/yyyy";
            this.dtpDateTimeStar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTimeStar.Location = new System.Drawing.Point(137, 25);
            this.dtpDateTimeStar.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDateTimeStar.Name = "dtpDateTimeStar";
            this.dtpDateTimeStar.ShowUpDown = true;
            this.dtpDateTimeStar.Size = new System.Drawing.Size(102, 20);
            this.dtpDateTimeStar.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Fecha de Inicio";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtTreatmentSite
            // 
            this.txtTreatmentSite.Location = new System.Drawing.Point(137, 132);
            this.txtTreatmentSite.Margin = new System.Windows.Forms.Padding(2);
            this.txtTreatmentSite.MaxLength = 250;
            this.txtTreatmentSite.Multiline = true;
            this.txtTreatmentSite.Name = "txtTreatmentSite";
            this.txtTreatmentSite.Size = new System.Drawing.Size(209, 64);
            this.txtTreatmentSite.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 132);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Días de descanso";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 63);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Detalle del Diagnóstico";
            // 
            // txtDxDetail
            // 
            this.txtDxDetail.Location = new System.Drawing.Point(137, 60);
            this.txtDxDetail.Margin = new System.Windows.Forms.Padding(2);
            this.txtDxDetail.MaxLength = 250;
            this.txtDxDetail.Multiline = true;
            this.txtDxDetail.Name = "txtDxDetail";
            this.txtDxDetail.Size = new System.Drawing.Size(209, 68);
            this.txtDxDetail.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(297, 27);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Tipo de Diagnóstico";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(685, 207);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 30);
            this.btnCancel.TabIndex = 35;
            this.btnCancel.Text = "    Salir";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(607, 207);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(73, 30);
            this.btnOk.TabIndex = 34;
            this.btnOk.Text = "Aceptar";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ddlTypeDiagnosticId
            // 
            this.ddlTypeDiagnosticId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlTypeDiagnosticId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlTypeDiagnosticId.FormattingEnabled = true;
            this.ddlTypeDiagnosticId.Location = new System.Drawing.Point(403, 22);
            this.ddlTypeDiagnosticId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlTypeDiagnosticId.Name = "ddlTypeDiagnosticId";
            this.ddlTypeDiagnosticId.Size = new System.Drawing.Size(357, 21);
            this.ddlTypeDiagnosticId.TabIndex = 36;
            this.uvPersonMedicalPopup.GetValidationSettings(this.ddlTypeDiagnosticId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvPersonMedicalPopup.GetValidationSettings(this.ddlTypeDiagnosticId).IsRequired = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(406, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Nombre Hospital";
            // 
            // txtHospital
            // 
            this.txtHospital.Location = new System.Drawing.Point(526, 63);
            this.txtHospital.Margin = new System.Windows.Forms.Padding(2);
            this.txtHospital.MaxLength = 250;
            this.txtHospital.Multiline = true;
            this.txtHospital.Name = "txtHospital";
            this.txtHospital.Size = new System.Drawing.Size(233, 68);
            this.txtHospital.TabIndex = 39;
            // 
            // lfdj
            // 
            this.lfdj.AutoSize = true;
            this.lfdj.Location = new System.Drawing.Point(406, 135);
            this.lfdj.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lfdj.Name = "lfdj";
            this.lfdj.Size = new System.Drawing.Size(81, 13);
            this.lfdj.TabIndex = 38;
            this.lfdj.Text = "Complicaciones";
            this.lfdj.Click += new System.EventHandler(this.txtComplicaciones_Click);
            // 
            // txtComplicaciones
            // 
            this.txtComplicaciones.Location = new System.Drawing.Point(526, 135);
            this.txtComplicaciones.Margin = new System.Windows.Forms.Padding(2);
            this.txtComplicaciones.MaxLength = 250;
            this.txtComplicaciones.Multiline = true;
            this.txtComplicaciones.Name = "txtComplicaciones";
            this.txtComplicaciones.Size = new System.Drawing.Size(233, 64);
            this.txtComplicaciones.TabIndex = 37;
            // 
            // frmPersonMedicalPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(783, 251);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHospital);
            this.Controls.Add(this.lfdj);
            this.Controls.Add(this.txtComplicaciones);
            this.Controls.Add(this.ddlTypeDiagnosticId);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDxDetail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTreatmentSite);
            this.Controls.Add(this.dtpDateTimeStar);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPersonMedicalPopup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmPersonMedicalPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uvPersonMedicalPopup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDateTimeStar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTreatmentSite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDxDetail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox ddlTypeDiagnosticId;
        private Infragistics.Win.Misc.UltraValidator uvPersonMedicalPopup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHospital;
        private System.Windows.Forms.Label lfdj;
        private System.Windows.Forms.TextBox txtComplicaciones;
    }
}