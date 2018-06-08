namespace Sigesoft.Node.WinClient.UI
{
    partial class frmClonServicio
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
            this.ddlCustomerOrganization = new System.Windows.Forms.ComboBox();
            this.ddlProtocolId = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnClonar = new System.Windows.Forms.Button();
            this.dtpDateTimeStar = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.uvClon = new Infragistics.Win.Misc.UltraValidator(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uvClon)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlCustomerOrganization
            // 
            this.ddlCustomerOrganization.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlCustomerOrganization.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlCustomerOrganization.DropDownWidth = 400;
            this.ddlCustomerOrganization.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCustomerOrganization.FormattingEnabled = true;
            this.ddlCustomerOrganization.Location = new System.Drawing.Point(122, 13);
            this.ddlCustomerOrganization.Margin = new System.Windows.Forms.Padding(2);
            this.ddlCustomerOrganization.Name = "ddlCustomerOrganization";
            this.ddlCustomerOrganization.Size = new System.Drawing.Size(499, 21);
            this.ddlCustomerOrganization.TabIndex = 30;
            this.uvClon.GetValidationSettings(this.ddlCustomerOrganization).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvClon.GetValidationSettings(this.ddlCustomerOrganization).DataType = typeof(string);
            this.uvClon.GetValidationSettings(this.ddlCustomerOrganization).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvClon.GetValidationSettings(this.ddlCustomerOrganization).IsRequired = true;
            this.ddlCustomerOrganization.SelectedIndexChanged += new System.EventHandler(this.ddlCustomerOrganization_SelectedIndexChanged);
            // 
            // ddlProtocolId
            // 
            this.ddlProtocolId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlProtocolId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlProtocolId.Enabled = false;
            this.ddlProtocolId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlProtocolId.FormattingEnabled = true;
            this.ddlProtocolId.Location = new System.Drawing.Point(123, 39);
            this.ddlProtocolId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlProtocolId.Name = "ddlProtocolId";
            this.ddlProtocolId.Size = new System.Drawing.Size(498, 21);
            this.ddlProtocolId.TabIndex = 29;
            this.uvClon.GetValidationSettings(this.ddlProtocolId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvClon.GetValidationSettings(this.ddlProtocolId).DataType = typeof(string);
            this.uvClon.GetValidationSettings(this.ddlProtocolId).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvClon.GetValidationSettings(this.ddlProtocolId).IsRequired = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(19, 40);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Protocolo";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(19, 16);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 16);
            this.label10.TabIndex = 27;
            this.label10.Text = "Empresa Cliente";
            // 
            // btnClonar
            // 
            this.btnClonar.BackColor = System.Drawing.SystemColors.Control;
            this.btnClonar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnClonar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnClonar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnClonar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClonar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClonar.ForeColor = System.Drawing.Color.Black;
            this.btnClonar.Image = global::Sigesoft.Node.WinClient.UI.Resources.chart_organisation;
            this.btnClonar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClonar.Location = new System.Drawing.Point(546, 82);
            this.btnClonar.Margin = new System.Windows.Forms.Padding(2);
            this.btnClonar.Name = "btnClonar";
            this.btnClonar.Size = new System.Drawing.Size(75, 24);
            this.btnClonar.TabIndex = 107;
            this.btnClonar.Text = "Clonar";
            this.btnClonar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClonar.UseVisualStyleBackColor = false;
            this.btnClonar.Click += new System.EventHandler(this.btnClonar_Click);
            // 
            // dtpDateTimeStar
            // 
            this.dtpDateTimeStar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateTimeStar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTimeStar.Location = new System.Drawing.Point(123, 64);
            this.dtpDateTimeStar.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDateTimeStar.Name = "dtpDateTimeStar";
            this.dtpDateTimeStar.Size = new System.Drawing.Size(95, 20);
            this.dtpDateTimeStar.TabIndex = 108;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(19, 70);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 109;
            this.label1.Text = "Fecha";
            // 
            // frmClonServicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(656, 121);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpDateTimeStar);
            this.Controls.Add(this.btnClonar);
            this.Controls.Add(this.ddlCustomerOrganization);
            this.Controls.Add(this.ddlProtocolId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmClonServicio";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clonar Servicio";
            this.Load += new System.EventHandler(this.frmClonServicio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uvClon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlCustomerOrganization;
        private System.Windows.Forms.ComboBox ddlProtocolId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnClonar;
        private System.Windows.Forms.DateTimePicker dtpDateTimeStar;
        private System.Windows.Forms.Label label1;
        private Infragistics.Win.Misc.UltraValidator uvClon;

    }
}