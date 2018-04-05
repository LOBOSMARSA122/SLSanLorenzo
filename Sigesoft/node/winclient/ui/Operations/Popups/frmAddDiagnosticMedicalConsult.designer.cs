namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    partial class frmAddDiagnosticMedicalConsult
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
            this.cbEnviarAntecedentes = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.cbTipoOcurrencia = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.lblDiagnostico = new System.Windows.Forms.Label();
            this.cbCalificacionFinal = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.uvAddTotalDiagnostic = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.cbOrigenOcurrencia = new System.Windows.Forms.ComboBox();
            this.cbParteCuerpo = new System.Windows.Forms.ComboBox();
            this.cbFormaAccidente = new System.Windows.Forms.ComboBox();
            this.cbClasificacionAccLab = new System.Windows.Forms.ComboBox();
            this.cbClasificacionEnfLab = new System.Windows.Forms.ComboBox();
            this.cbFactorRiesgo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbAccidenteLaboral = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gbEnfermedadLaboral = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.uvAddTotalDiagnostic)).BeginInit();
            this.gbAccidenteLaboral.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbEnfermedadLaboral.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbEnviarAntecedentes
            // 
            this.cbEnviarAntecedentes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEnviarAntecedentes.FormattingEnabled = true;
            this.cbEnviarAntecedentes.Location = new System.Drawing.Point(315, 46);
            this.cbEnviarAntecedentes.Name = "cbEnviarAntecedentes";
            this.cbEnviarAntecedentes.Size = new System.Drawing.Size(153, 21);
            this.cbEnviarAntecedentes.TabIndex = 21;
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbEnviarAntecedentes).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbEnviarAntecedentes).DataType = typeof(string);
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbEnviarAntecedentes).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbEnviarAntecedentes).IsRequired = true;
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(239, 42);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(73, 29);
            this.label32.TabIndex = 20;
            this.label32.Text = "Enviar a Antecedentes";
            // 
            // cbTipoOcurrencia
            // 
            this.cbTipoOcurrencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoOcurrencia.DropDownWidth = 200;
            this.cbTipoOcurrencia.FormattingEnabled = true;
            this.cbTipoOcurrencia.Location = new System.Drawing.Point(539, 19);
            this.cbTipoOcurrencia.Name = "cbTipoOcurrencia";
            this.cbTipoOcurrencia.Size = new System.Drawing.Size(156, 21);
            this.cbTipoOcurrencia.TabIndex = 19;
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbTipoOcurrencia).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbTipoOcurrencia).DataType = typeof(string);
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbTipoOcurrencia).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbTipoOcurrencia).IsRequired = true;
            this.cbTipoOcurrencia.SelectedValueChanged += new System.EventHandler(this.cbTipoOcurrencia_SelectedValueChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(486, 23);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(28, 13);
            this.label31.TabIndex = 18;
            this.label31.Text = "Tipo";
            // 
            // lblDiagnostico
            // 
            this.lblDiagnostico.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblDiagnostico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDiagnostico.Location = new System.Drawing.Point(11, 23);
            this.lblDiagnostico.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDiagnostico.Name = "lblDiagnostico";
            this.lblDiagnostico.Size = new System.Drawing.Size(225, 44);
            this.lblDiagnostico.TabIndex = 17;
            // 
            // cbCalificacionFinal
            // 
            this.cbCalificacionFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCalificacionFinal.DropDownWidth = 125;
            this.cbCalificacionFinal.FormattingEnabled = true;
            this.cbCalificacionFinal.Location = new System.Drawing.Point(315, 19);
            this.cbCalificacionFinal.Name = "cbCalificacionFinal";
            this.cbCalificacionFinal.Size = new System.Drawing.Size(153, 21);
            this.cbCalificacionFinal.TabIndex = 8;
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbCalificacionFinal).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbCalificacionFinal).DataType = typeof(string);
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbCalificacionFinal).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbCalificacionFinal).IsRequired = true;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(239, 23);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(61, 13);
            this.label30.TabIndex = 0;
            this.label30.Text = "Calificación";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(629, 175);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 54;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(545, 175);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.TabIndex = 53;
            this.btnOK.Text = "Aceptar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbOrigenOcurrencia
            // 
            this.cbOrigenOcurrencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrigenOcurrencia.FormattingEnabled = true;
            this.cbOrigenOcurrencia.Location = new System.Drawing.Point(539, 46);
            this.cbOrigenOcurrencia.Name = "cbOrigenOcurrencia";
            this.cbOrigenOcurrencia.Size = new System.Drawing.Size(156, 21);
            this.cbOrigenOcurrencia.TabIndex = 84;
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbOrigenOcurrencia).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbOrigenOcurrencia).DataType = typeof(string);
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbOrigenOcurrencia).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbOrigenOcurrencia).IsRequired = true;
            this.cbOrigenOcurrencia.SelectedValueChanged += new System.EventHandler(this.cbOrigenOcurrencia_SelectedValueChanged);
            // 
            // cbParteCuerpo
            // 
            this.cbParteCuerpo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParteCuerpo.DropDownWidth = 400;
            this.cbParteCuerpo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbParteCuerpo.FormattingEnabled = true;
            this.cbParteCuerpo.Location = new System.Drawing.Point(96, 40);
            this.cbParteCuerpo.Name = "cbParteCuerpo";
            this.cbParteCuerpo.Size = new System.Drawing.Size(600, 21);
            this.cbParteCuerpo.TabIndex = 88;
            // 
            // cbFormaAccidente
            // 
            this.cbFormaAccidente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormaAccidente.DropDownWidth = 400;
            this.cbFormaAccidente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFormaAccidente.FormattingEnabled = true;
            this.cbFormaAccidente.Location = new System.Drawing.Point(96, 17);
            this.cbFormaAccidente.Name = "cbFormaAccidente";
            this.cbFormaAccidente.Size = new System.Drawing.Size(600, 21);
            this.cbFormaAccidente.TabIndex = 86;
            // 
            // cbClasificacionAccLab
            // 
            this.cbClasificacionAccLab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClasificacionAccLab.DropDownWidth = 300;
            this.cbClasificacionAccLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbClasificacionAccLab.FormattingEnabled = true;
            this.cbClasificacionAccLab.Location = new System.Drawing.Point(96, 62);
            this.cbClasificacionAccLab.Name = "cbClasificacionAccLab";
            this.cbClasificacionAccLab.Size = new System.Drawing.Size(253, 21);
            this.cbClasificacionAccLab.TabIndex = 90;
            // 
            // cbClasificacionEnfLab
            // 
            this.cbClasificacionEnfLab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClasificacionEnfLab.DropDownWidth = 260;
            this.cbClasificacionEnfLab.FormattingEnabled = true;
            this.cbClasificacionEnfLab.Location = new System.Drawing.Point(472, 16);
            this.cbClasificacionEnfLab.Name = "cbClasificacionEnfLab";
            this.cbClasificacionEnfLab.Size = new System.Drawing.Size(224, 21);
            this.cbClasificacionEnfLab.TabIndex = 88;
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbClasificacionEnfLab).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbClasificacionEnfLab).DataType = typeof(string);
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbClasificacionEnfLab).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbClasificacionEnfLab).IsRequired = true;
            // 
            // cbFactorRiesgo
            // 
            this.cbFactorRiesgo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFactorRiesgo.DropDownWidth = 260;
            this.cbFactorRiesgo.FormattingEnabled = true;
            this.cbFactorRiesgo.Location = new System.Drawing.Point(91, 16);
            this.cbFactorRiesgo.Name = "cbFactorRiesgo";
            this.cbFactorRiesgo.Size = new System.Drawing.Size(296, 21);
            this.cbFactorRiesgo.TabIndex = 86;
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbFactorRiesgo).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbFactorRiesgo).DataType = typeof(string);
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbFactorRiesgo).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvAddTotalDiagnostic.GetValidationSettings(this.cbFactorRiesgo).IsRequired = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(486, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 83;
            this.label1.Text = "Origen";
            // 
            // gbAccidenteLaboral
            // 
            this.gbAccidenteLaboral.Controls.Add(this.cbClasificacionAccLab);
            this.gbAccidenteLaboral.Controls.Add(this.label5);
            this.gbAccidenteLaboral.Controls.Add(this.cbParteCuerpo);
            this.gbAccidenteLaboral.Controls.Add(this.label2);
            this.gbAccidenteLaboral.Controls.Add(this.cbFormaAccidente);
            this.gbAccidenteLaboral.Controls.Add(this.label3);
            this.gbAccidenteLaboral.Location = new System.Drawing.Point(5, 85);
            this.gbAccidenteLaboral.Name = "gbAccidenteLaboral";
            this.gbAccidenteLaboral.Size = new System.Drawing.Size(711, 89);
            this.gbAccidenteLaboral.TabIndex = 85;
            this.gbAccidenteLaboral.TabStop = false;
            this.gbAccidenteLaboral.Text = "Accidente laboral";
            this.gbAccidenteLaboral.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 89;
            this.label5.Text = "Clasificación";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 87;
            this.label2.Text = "Parte del cuerpo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 85;
            this.label3.Text = "Forma. Acc.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(405, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 87;
            this.label7.Text = "Clasificación";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 85;
            this.label8.Text = "Factor de Riesgo";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbCalificacionFinal);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.cbOrigenOcurrencia);
            this.groupBox3.Controls.Add(this.cbTipoOcurrencia);
            this.groupBox3.Controls.Add(this.cbEnviarAntecedentes);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Controls.Add(this.lblDiagnostico);
            this.groupBox3.Location = new System.Drawing.Point(5, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(711, 77);
            this.groupBox3.TabIndex = 87;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Diagnóstico";
            // 
            // gbEnfermedadLaboral
            // 
            this.gbEnfermedadLaboral.Controls.Add(this.cbClasificacionEnfLab);
            this.gbEnfermedadLaboral.Controls.Add(this.cbFactorRiesgo);
            this.gbEnfermedadLaboral.Controls.Add(this.label7);
            this.gbEnfermedadLaboral.Controls.Add(this.label8);
            this.gbEnfermedadLaboral.Location = new System.Drawing.Point(5, 85);
            this.gbEnfermedadLaboral.Name = "gbEnfermedadLaboral";
            this.gbEnfermedadLaboral.Size = new System.Drawing.Size(711, 49);
            this.gbEnfermedadLaboral.TabIndex = 88;
            this.gbEnfermedadLaboral.TabStop = false;
            this.gbEnfermedadLaboral.Text = "Enfermedad Laboral";
            this.gbEnfermedadLaboral.Visible = false;
            // 
            // frmAddDiagnosticMedicalConsult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 208);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbAccidenteLaboral);
            this.Controls.Add(this.gbEnfermedadLaboral);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddDiagnosticMedicalConsult";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar Nuevo Diagnóstico";
            this.Load += new System.EventHandler(this.frmAddTotalDiagnostic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uvAddTotalDiagnostic)).EndInit();
            this.gbAccidenteLaboral.ResumeLayout(false);
            this.gbAccidenteLaboral.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbEnfermedadLaboral.ResumeLayout(false);
            this.gbEnfermedadLaboral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbEnviarAntecedentes;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox cbTipoOcurrencia;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lblDiagnostico;
        private System.Windows.Forms.ComboBox cbCalificacionFinal;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Infragistics.Win.Misc.UltraValidator uvAddTotalDiagnostic;
        private System.Windows.Forms.ComboBox cbOrigenOcurrencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbAccidenteLaboral;
        private System.Windows.Forms.ComboBox cbClasificacionEnfLab;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbFactorRiesgo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbClasificacionAccLab;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbParteCuerpo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFormaAccidente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox gbEnfermedadLaboral;
    }
}