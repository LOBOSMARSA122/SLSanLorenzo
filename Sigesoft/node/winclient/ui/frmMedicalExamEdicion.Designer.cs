namespace Sigesoft.Node.WinClient.UI
{
    partial class frmMedicalExamEdicion
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
            this.Nombre = new System.Windows.Forms.Label();
            this.txtInsertName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.uvMedicalExamEdit = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.ddlCategoryId = new ComboTreeBox();
            this.ddlDiagnosableId = new System.Windows.Forms.ComboBox();
            this.ddlComponentTypeId = new System.Windows.Forms.ComboBox();
            this.ddlUIIsVisibleId = new System.Windows.Forms.ComboBox();
            this.ddlIsApprovedId = new System.Windows.Forms.ComboBox();
            this.unBasePrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.unUIIndex = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.unValidInDays = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlUnidadProductiva = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.uvMedicalExamEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unUIIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unValidInDays)).BeginInit();
            this.SuspendLayout();
            // 
            // Nombre
            // 
            this.Nombre.AutoSize = true;
            this.Nombre.Location = new System.Drawing.Point(15, 24);
            this.Nombre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Nombre.Name = "Nombre";
            this.Nombre.Size = new System.Drawing.Size(44, 13);
            this.Nombre.TabIndex = 0;
            this.Nombre.Text = "Nombre";
            // 
            // txtInsertName
            // 
            this.txtInsertName.Location = new System.Drawing.Point(111, 21);
            this.txtInsertName.Margin = new System.Windows.Forms.Padding(2);
            this.txtInsertName.MaxLength = 250;
            this.txtInsertName.Name = "txtInsertName";
            this.txtInsertName.Size = new System.Drawing.Size(217, 20);
            this.txtInsertName.TabIndex = 1;
            this.uvMedicalExamEdit.GetValidationSettings(this.txtInsertName).DataType = typeof(string);
            this.uvMedicalExamEdit.GetValidationSettings(this.txtInsertName).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvMedicalExamEdit.GetValidationSettings(this.txtInsertName).IsRequired = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Categoría";
            // 
            // ddlCategoryId
            // 
            this.ddlCategoryId.DropDownHeight = 500;
            this.ddlCategoryId.DroppedDown = false;
            this.ddlCategoryId.Location = new System.Drawing.Point(111, 46);
            this.ddlCategoryId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlCategoryId.Name = "ddlCategoryId";
            this.ddlCategoryId.SelectedNode = null;
            this.ddlCategoryId.ShowPath = true;
            this.ddlCategoryId.Size = new System.Drawing.Size(216, 19);
            this.ddlCategoryId.TabIndex = 12;
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlCategoryId).DataType = typeof(string);
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlCategoryId).IsRequired = true;
            // 
            // ddlDiagnosableId
            // 
            this.ddlDiagnosableId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDiagnosableId.DropDownWidth = 300;
            this.ddlDiagnosableId.FormattingEnabled = true;
            this.ddlDiagnosableId.Location = new System.Drawing.Point(111, 70);
            this.ddlDiagnosableId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlDiagnosableId.Name = "ddlDiagnosableId";
            this.ddlDiagnosableId.Size = new System.Drawing.Size(82, 21);
            this.ddlDiagnosableId.TabIndex = 22;
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlDiagnosableId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlDiagnosableId).DataType = typeof(string);
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlDiagnosableId).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlDiagnosableId).IsRequired = true;
            // 
            // ddlComponentTypeId
            // 
            this.ddlComponentTypeId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlComponentTypeId.DropDownWidth = 300;
            this.ddlComponentTypeId.FormattingEnabled = true;
            this.ddlComponentTypeId.Location = new System.Drawing.Point(111, 96);
            this.ddlComponentTypeId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlComponentTypeId.Name = "ddlComponentTypeId";
            this.ddlComponentTypeId.Size = new System.Drawing.Size(217, 21);
            this.ddlComponentTypeId.TabIndex = 24;
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlComponentTypeId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlComponentTypeId).DataType = typeof(string);
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlComponentTypeId).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlComponentTypeId).IsRequired = true;
            // 
            // ddlUIIsVisibleId
            // 
            this.ddlUIIsVisibleId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlUIIsVisibleId.DropDownWidth = 300;
            this.ddlUIIsVisibleId.FormattingEnabled = true;
            this.ddlUIIsVisibleId.Location = new System.Drawing.Point(111, 122);
            this.ddlUIIsVisibleId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlUIIsVisibleId.Name = "ddlUIIsVisibleId";
            this.ddlUIIsVisibleId.Size = new System.Drawing.Size(82, 21);
            this.ddlUIIsVisibleId.TabIndex = 26;
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlUIIsVisibleId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlUIIsVisibleId).DataType = typeof(string);
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlUIIsVisibleId).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlUIIsVisibleId).IsRequired = true;
            // 
            // ddlIsApprovedId
            // 
            this.ddlIsApprovedId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlIsApprovedId.DropDownWidth = 300;
            this.ddlIsApprovedId.FormattingEnabled = true;
            this.ddlIsApprovedId.Location = new System.Drawing.Point(111, 148);
            this.ddlIsApprovedId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlIsApprovedId.Name = "ddlIsApprovedId";
            this.ddlIsApprovedId.Size = new System.Drawing.Size(82, 21);
            this.ddlIsApprovedId.TabIndex = 30;
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlIsApprovedId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlIsApprovedId).DataType = typeof(string);
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlIsApprovedId).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvMedicalExamEdit.GetValidationSettings(this.ddlIsApprovedId).IsRequired = true;
            // 
            // unBasePrice
            // 
            this.unBasePrice.Location = new System.Drawing.Point(279, 70);
            this.unBasePrice.Margin = new System.Windows.Forms.Padding(2);
            this.unBasePrice.Name = "unBasePrice";
            this.unBasePrice.Size = new System.Drawing.Size(48, 20);
            this.unBasePrice.TabIndex = 33;
            this.uvMedicalExamEdit.GetValidationSettings(this.unBasePrice).DataType = typeof(string);
            this.uvMedicalExamEdit.GetValidationSettings(this.unBasePrice).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvMedicalExamEdit.GetValidationSettings(this.unBasePrice).IsRequired = true;
            this.unBasePrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.unBasePrice_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Precio Base";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 76);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Diagnosticable";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 102);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Tipo Componente";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.DropDownWidth = 300;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(124, 166);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(108, 21);
            this.comboBox1.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 128);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Es Visible";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(206, 125);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Nro Orden";
            // 
            // unUIIndex
            // 
            this.unUIIndex.AutoSize = false;
            this.unUIIndex.Location = new System.Drawing.Point(279, 121);
            this.unUIIndex.Margin = new System.Windows.Forms.Padding(2);
            this.unUIIndex.MaxValue = 9999;
            this.unUIIndex.Name = "unUIIndex";
            this.unUIIndex.PromptChar = ' ';
            this.unUIIndex.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.unUIIndex.Size = new System.Drawing.Size(47, 20);
            this.unUIIndex.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 154);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Aprobación";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(206, 149);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Vigencia(dias)";
            // 
            // unValidInDays
            // 
            this.unValidInDays.AutoSize = false;
            this.unValidInDays.Location = new System.Drawing.Point(279, 146);
            this.unValidInDays.Margin = new System.Windows.Forms.Padding(2);
            this.unValidInDays.MaxValue = 9999;
            this.unValidInDays.Name = "unValidInDays";
            this.unValidInDays.PromptChar = ' ';
            this.unValidInDays.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.unValidInDays.Size = new System.Drawing.Size(47, 20);
            this.unValidInDays.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 180);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Unidad Productiva";
            // 
            // ddlUnidadProductiva
            // 
            this.ddlUnidadProductiva.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlUnidadProductiva.DropDownWidth = 300;
            this.ddlUnidadProductiva.FormattingEnabled = true;
            this.ddlUnidadProductiva.Location = new System.Drawing.Point(111, 174);
            this.ddlUnidadProductiva.Margin = new System.Windows.Forms.Padding(2);
            this.ddlUnidadProductiva.Name = "ddlUnidadProductiva";
            this.ddlUnidadProductiva.Size = new System.Drawing.Size(217, 21);
            this.ddlUnidadProductiva.TabIndex = 24;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(253, 208);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Salir";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(173, 208);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 24);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "Guardar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmMedicalExamEdicion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(349, 249);
            this.ControlBox = false;
            this.Controls.Add(this.unBasePrice);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.unValidInDays);
            this.Controls.Add(this.ddlIsApprovedId);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.unUIIndex);
            this.Controls.Add(this.ddlUIIsVisibleId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ddlUnidadProductiva);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ddlComponentTypeId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ddlDiagnosableId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddlCategoryId);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtInsertName);
            this.Controls.Add(this.Nombre);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMedicalExamEdicion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Examen Médico";
            this.Load += new System.EventHandler(this.frmMedicalExamEdicion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uvMedicalExamEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unUIIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unValidInDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Nombre;
        private System.Windows.Forms.TextBox txtInsertName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Infragistics.Win.Misc.UltraValidator uvMedicalExamEdit;
        private ComboTreeBox ddlCategoryId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ddlDiagnosableId;
        private System.Windows.Forms.ComboBox ddlComponentTypeId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ddlUIIsVisibleId;
        private System.Windows.Forms.Label label7;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor unUIIndex;
        private System.Windows.Forms.ComboBox ddlIsApprovedId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor unValidInDays;
        private System.Windows.Forms.TextBox unBasePrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlUnidadProductiva;
    }
}