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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            this.ddlCustomerOrganization = new System.Windows.Forms.ComboBox();
            this.ddlProtocolId = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnClonar = new System.Windows.Forms.Button();
            this.dtpDateTimeStar = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.uvClon = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.txtCCosto = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.label28 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uvClon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCCosto)).BeginInit();
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
            this.btnClonar.Location = new System.Drawing.Point(546, 95);
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
            // txtCCosto
            // 
            this.txtCCosto.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCCosto.DisplayLayout.Appearance = appearance1;
            this.txtCCosto.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.txtCCosto.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.txtCCosto.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtCCosto.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.txtCCosto.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtCCosto.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCCosto.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtCCosto.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.txtCCosto.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            this.txtCCosto.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.txtCCosto.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.txtCCosto.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.txtCCosto.DisplayLayout.Override.CellAppearance = appearance8;
            this.txtCCosto.DisplayLayout.Override.CellPadding = 0;
            this.txtCCosto.DisplayLayout.Override.ColumnSizingArea = Infragistics.Win.UltraWinGrid.ColumnSizingArea.EntireColumn;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.txtCCosto.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.txtCCosto.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.txtCCosto.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.txtCCosto.DisplayLayout.Override.RowAppearance = appearance11;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtCCosto.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.txtCCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCosto.Location = new System.Drawing.Point(330, 64);
            this.txtCCosto.Name = "txtCCosto";
            this.txtCCosto.Size = new System.Drawing.Size(291, 22);
            this.txtCCosto.TabIndex = 162;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(241, 72);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(47, 13);
            this.label28.TabIndex = 163;
            this.label28.Text = "C. Costo";
            // 
            // frmClonServicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(656, 130);
            this.Controls.Add(this.txtCCosto);
            this.Controls.Add(this.label28);
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
            ((System.ComponentModel.ISupportInitialize)(this.txtCCosto)).EndInit();
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
        private Infragistics.Win.UltraWinGrid.UltraCombo txtCCosto;
        private System.Windows.Forms.Label label28;

    }
}