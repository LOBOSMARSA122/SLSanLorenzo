namespace Sigesoft.Node.WinClient.UI
{
    partial class frmSupplierEdicion
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
            this.label18 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.uvOrganization = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.ddlSectorTypeId = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtIdentificationNumber = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.uvOrganization)).BeginInit();
            this.SuspendLayout();
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(60, 167);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 23);
            this.label18.TabIndex = 47;
            this.label18.Text = "Email";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(39, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 23);
            this.label8.TabIndex = 46;
            this.label8.Text = "Teléfono";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(49, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 23);
            this.label7.TabIndex = 45;
            this.label7.Text = "RUC";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(32, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 23);
            this.label5.TabIndex = 44;
            this.label5.Text = "Dirección";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 23);
            this.label4.TabIndex = 41;
            this.label4.Text = "Razón Social";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(50, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 23);
            this.label3.TabIndex = 38;
            this.label3.Text = "Sector";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(436, 220);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 37);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(338, 220);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(92, 37);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "Guardar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ddlSectorTypeId
            // 
            this.ddlSectorTypeId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSectorTypeId.FormattingEnabled = true;
            this.ddlSectorTypeId.Location = new System.Drawing.Point(112, 22);
            this.ddlSectorTypeId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ddlSectorTypeId.Name = "ddlSectorTypeId";
            this.ddlSectorTypeId.Size = new System.Drawing.Size(416, 24);
            this.ddlSectorTypeId.TabIndex = 1;
            this.uvOrganization.GetValidationSettings(this.ddlSectorTypeId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvOrganization.GetValidationSettings(this.ddlSectorTypeId).DataType = typeof(string);
            this.uvOrganization.GetValidationSettings(this.ddlSectorTypeId).IsRequired = true;
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Location = new System.Drawing.Point(112, 51);
            this.txtName.MaxLength = 250;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(417, 22);
            this.txtName.TabIndex = 2;
            this.uvOrganization.GetValidationSettings(this.txtName).DataType = typeof(string);
            this.uvOrganization.GetValidationSettings(this.txtName).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvOrganization.GetValidationSettings(this.txtName).IsRequired = true;
            // 
            // txtIdentificationNumber
            // 
            this.txtIdentificationNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdentificationNumber.Location = new System.Drawing.Point(112, 79);
            this.txtIdentificationNumber.MaxLength = 11;
            this.txtIdentificationNumber.Name = "txtIdentificationNumber";
            this.txtIdentificationNumber.Size = new System.Drawing.Size(417, 22);
            this.txtIdentificationNumber.TabIndex = 3;
            this.uvOrganization.GetValidationSettings(this.txtIdentificationNumber).DataType = typeof(string);
            this.uvOrganization.GetValidationSettings(this.txtIdentificationNumber).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvOrganization.GetValidationSettings(this.txtIdentificationNumber).IsRequired = true;
            this.txtIdentificationNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIdentificationNumber_KeyPress);
            // 
            // txtAddress
            // 
            this.txtAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress.Location = new System.Drawing.Point(112, 107);
            this.txtAddress.MaxLength = 250;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAddress.Size = new System.Drawing.Size(417, 52);
            this.txtAddress.TabIndex = 4;
            this.uvOrganization.GetValidationSettings(this.txtAddress).DataType = typeof(string);
            this.uvOrganization.GetValidationSettings(this.txtAddress).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            // 
            // txtEmail
            // 
            this.txtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmail.Location = new System.Drawing.Point(112, 165);
            this.txtEmail.MaxLength = 200;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(416, 22);
            this.txtEmail.TabIndex = 5;
            this.uvOrganization.GetValidationSettings(this.txtEmail).DataType = typeof(string);
            this.uvOrganization.GetValidationSettings(this.txtEmail).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPhoneNumber.Location = new System.Drawing.Point(112, 193);
            this.txtPhoneNumber.MaxLength = 15;
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(416, 22);
            this.txtPhoneNumber.TabIndex = 6;
            this.uvOrganization.GetValidationSettings(this.txtPhoneNumber).DataType = typeof(string);
            this.uvOrganization.GetValidationSettings(this.txtPhoneNumber).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            // 
            // frmSupplierEdicion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 292);
            this.ControlBox = false;
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtIdentificationNumber);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.ddlSectorTypeId);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmSupplierEdicion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proveedor";
            this.Load += new System.EventHandler(this.frmSupplierEdicion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uvOrganization)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Infragistics.Win.Misc.UltraValidator uvOrganization;
        private System.Windows.Forms.ComboBox ddlSectorTypeId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtIdentificationNumber;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhoneNumber;
    }
}