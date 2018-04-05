namespace Sigesoft.Node.WinClient.UI
{
    partial class frmProductEdicion
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGenericName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReferentialCostPrice = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpExpirationDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.ddlMeasurementUnitId = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAdiccionalInformation = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPresentation = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtReferentialSalesPrice = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.uvProduct = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.ddl = new ComboTreeBox();
            this.ddlCategoryId = new ComboTreeBox();
            ((System.ComponentModel.ISupportInitialize)(this.uvProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Categoría";
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Location = new System.Drawing.Point(116, 35);
            this.txtName.MaxLength = 200;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(282, 20);
            this.txtName.TabIndex = 2;
            this.uvProduct.GetValidationSettings(this.txtName).DataType = typeof(string);
            this.uvProduct.GetValidationSettings(this.txtName).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvProduct.GetValidationSettings(this.txtName).IsRequired = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(42, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 19;
            this.label5.Text = "Nombre";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGenericName
            // 
            this.txtGenericName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGenericName.Location = new System.Drawing.Point(116, 59);
            this.txtGenericName.MaxLength = 100;
            this.txtGenericName.Name = "txtGenericName";
            this.txtGenericName.Size = new System.Drawing.Size(282, 20);
            this.txtGenericName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 19);
            this.label2.TabIndex = 21;
            this.label2.Text = "Nombre Genérico";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBarCode
            // 
            this.txtBarCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBarCode.Location = new System.Drawing.Point(116, 84);
            this.txtBarCode.MaxLength = 20;
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(282, 20);
            this.txtBarCode.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 19);
            this.label3.TabIndex = 23;
            this.label3.Text = "Código";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBrand
            // 
            this.txtBrand.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBrand.Location = new System.Drawing.Point(116, 108);
            this.txtBrand.MaxLength = 100;
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(282, 20);
            this.txtBrand.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 109);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 19);
            this.label4.TabIndex = 25;
            this.label4.Text = "Marca";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtModel
            // 
            this.txtModel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtModel.Location = new System.Drawing.Point(116, 132);
            this.txtModel.MaxLength = 100;
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(282, 20);
            this.txtModel.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 133);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 19);
            this.label6.TabIndex = 27;
            this.label6.Text = "Modelo";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSerialNumber.Location = new System.Drawing.Point(116, 157);
            this.txtSerialNumber.MaxLength = 20;
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(282, 20);
            this.txtSerialNumber.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 158);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 19);
            this.label7.TabIndex = 29;
            this.label7.Text = "Nro Serie";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReferentialCostPrice
            // 
            this.txtReferentialCostPrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReferentialCostPrice.Location = new System.Drawing.Point(116, 228);
            this.txtReferentialCostPrice.MaxLength = 10;
            this.txtReferentialCostPrice.Name = "txtReferentialCostPrice";
            this.txtReferentialCostPrice.Size = new System.Drawing.Size(282, 20);
            this.txtReferentialCostPrice.TabIndex = 10;
            this.txtReferentialCostPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReferentialCostPrice_KeyPress);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(12, 227);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 19);
            this.label8.TabIndex = 31;
            this.label8.Text = "Precio Costo(Ref)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpExpirationDate
            // 
            this.dtpExpirationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpirationDate.Location = new System.Drawing.Point(116, 180);
            this.dtpExpirationDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpExpirationDate.Name = "dtpExpirationDate";
            this.dtpExpirationDate.ShowCheckBox = true;
            this.dtpExpirationDate.Size = new System.Drawing.Size(102, 20);
            this.dtpExpirationDate.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(12, 180);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 19);
            this.label9.TabIndex = 33;
            this.label9.Text = "Fecha Expiración";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddlMeasurementUnitId
            // 
            this.ddlMeasurementUnitId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMeasurementUnitId.FormattingEnabled = true;
            this.ddlMeasurementUnitId.Location = new System.Drawing.Point(116, 203);
            this.ddlMeasurementUnitId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ddlMeasurementUnitId.Name = "ddlMeasurementUnitId";
            this.ddlMeasurementUnitId.Size = new System.Drawing.Size(282, 21);
            this.ddlMeasurementUnitId.TabIndex = 9;
            this.uvProduct.GetValidationSettings(this.ddlMeasurementUnitId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvProduct.GetValidationSettings(this.ddlMeasurementUnitId).DataType = typeof(string);
            this.uvProduct.GetValidationSettings(this.ddlMeasurementUnitId).IsRequired = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 206);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Unidad Medida";
            // 
            // txtAdiccionalInformation
            // 
            this.txtAdiccionalInformation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAdiccionalInformation.Location = new System.Drawing.Point(116, 301);
            this.txtAdiccionalInformation.MaxLength = 250;
            this.txtAdiccionalInformation.Multiline = true;
            this.txtAdiccionalInformation.Name = "txtAdiccionalInformation";
            this.txtAdiccionalInformation.Size = new System.Drawing.Size(282, 70);
            this.txtAdiccionalInformation.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 301);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 19);
            this.label11.TabIndex = 41;
            this.label11.Text = "Información Adicional";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPresentation
            // 
            this.txtPresentation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPresentation.Location = new System.Drawing.Point(116, 276);
            this.txtPresentation.MaxLength = 100;
            this.txtPresentation.Name = "txtPresentation";
            this.txtPresentation.Size = new System.Drawing.Size(282, 20);
            this.txtPresentation.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(38, 276);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 19);
            this.label12.TabIndex = 39;
            this.label12.Text = "Presentación";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReferentialSalesPrice
            // 
            this.txtReferentialSalesPrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReferentialSalesPrice.Location = new System.Drawing.Point(116, 252);
            this.txtReferentialSalesPrice.MaxLength = 10;
            this.txtReferentialSalesPrice.Name = "txtReferentialSalesPrice";
            this.txtReferentialSalesPrice.Size = new System.Drawing.Size(282, 20);
            this.txtReferentialSalesPrice.TabIndex = 11;
            this.txtReferentialSalesPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReferentialSalesPrice_KeyPress);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(15, 249);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 19);
            this.label13.TabIndex = 37;
            this.label13.Text = "Precio Venta(Ref)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(326, 378);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 30);
            this.btnCancel.TabIndex = 15;
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
            this.btnOK.Location = new System.Drawing.Point(252, 378);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 30);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "Guardar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.uvProduct.GetValidationSettings(this.btnOK).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvProduct.GetValidationSettings(this.btnOK).DataType = typeof(string);
            this.uvProduct.GetValidationSettings(this.btnOK).IsRequired = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ddl
            // 
            this.ddl.DroppedDown = false;
            this.ddl.Location = new System.Drawing.Point(155, 15);
            this.ddl.Name = "ddl";
            this.ddl.SelectedNode = null;
            this.ddl.Size = new System.Drawing.Size(276, 23);
            this.ddl.TabIndex = 44;
            // 
            // ddlCategoryId
            // 
            this.ddlCategoryId.DroppedDown = false;
            this.ddlCategoryId.Location = new System.Drawing.Point(116, 12);
            this.ddlCategoryId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlCategoryId.Name = "ddlCategoryId";
            this.ddlCategoryId.SelectedNode = null;
            this.ddlCategoryId.Size = new System.Drawing.Size(279, 19);
            this.ddlCategoryId.TabIndex = 1;
            this.uvProduct.GetValidationSettings(this.ddlCategoryId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvProduct.GetValidationSettings(this.ddlCategoryId).IsRequired = true;
            // 
            // frmProductEdicion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 436);
            this.ControlBox = false;
            this.Controls.Add(this.ddlCategoryId);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtAdiccionalInformation);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtPresentation);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtReferentialSalesPrice);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.ddlMeasurementUnitId);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpExpirationDate);
            this.Controls.Add(this.txtReferentialCostPrice);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSerialNumber);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBarCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGenericName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmProductEdicion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Producto";
            this.Load += new System.EventHandler(this.frmProductEdicion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uvProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtGenericName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtReferentialCostPrice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpExpirationDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox ddlMeasurementUnitId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtAdiccionalInformation;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPresentation;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtReferentialSalesPrice;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private ComboTreeBox ddl;
        private Infragistics.Win.Misc.UltraValidator uvProduct;
        private ComboTreeBox ddlCategoryId;
    }
}