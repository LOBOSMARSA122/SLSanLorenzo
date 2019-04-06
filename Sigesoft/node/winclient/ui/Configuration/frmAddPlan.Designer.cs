namespace Sigesoft.Node.WinClient.UI.Configuration
{
    partial class frmAddPlan
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
            this.cbLine = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.label4 = new System.Windows.Forms.Label();
            this.chkDeducible = new System.Windows.Forms.CheckBox();
            this.chkCoaseguro = new System.Windows.Forms.CheckBox();
            this.txtDeducible = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCoaseguro = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtUnidadProdId = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.cbLine)).BeginInit();
            this.SuspendLayout();
            // 
            // cbLine
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbLine.DisplayLayout.Appearance = appearance1;
            this.cbLine.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cbLine.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.cbLine.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cbLine.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.cbLine.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cbLine.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbLine.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cbLine.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.cbLine.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            this.cbLine.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cbLine.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.cbLine.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cbLine.DisplayLayout.Override.CellAppearance = appearance8;
            this.cbLine.DisplayLayout.Override.CellPadding = 0;
            this.cbLine.DisplayLayout.Override.ColumnSizingArea = Infragistics.Win.UltraWinGrid.ColumnSizingArea.EntireColumn;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.cbLine.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.cbLine.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.cbLine.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.cbLine.DisplayLayout.Override.RowAppearance = appearance11;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbLine.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.cbLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLine.Location = new System.Drawing.Point(109, 12);
            this.cbLine.Name = "cbLine";
            this.cbLine.Size = new System.Drawing.Size(346, 22);
            this.cbLine.TabIndex = 122;
            this.cbLine.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.cbLine_RowSelected);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 123;
            this.label4.Text = "Unidad Productiva: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDeducible
            // 
            this.chkDeducible.AutoSize = true;
            this.chkDeducible.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDeducible.Location = new System.Drawing.Point(32, 53);
            this.chkDeducible.Name = "chkDeducible";
            this.chkDeducible.Size = new System.Drawing.Size(133, 17);
            this.chkDeducible.TabIndex = 124;
            this.chkDeducible.Text = "Deducible(Copago fijo)";
            this.chkDeducible.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDeducible.UseVisualStyleBackColor = true;
            // 
            // chkCoaseguro
            // 
            this.chkCoaseguro.AutoSize = true;
            this.chkCoaseguro.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCoaseguro.Location = new System.Drawing.Point(5, 88);
            this.chkCoaseguro.Name = "chkCoaseguro";
            this.chkCoaseguro.Size = new System.Drawing.Size(160, 17);
            this.chkCoaseguro.TabIndex = 124;
            this.chkCoaseguro.Text = "Coaseguro(Copago variable)";
            this.chkCoaseguro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCoaseguro.UseVisualStyleBackColor = true;
            // 
            // txtDeducible
            // 
            this.txtDeducible.Location = new System.Drawing.Point(354, 51);
            this.txtDeducible.Name = "txtDeducible";
            this.txtDeducible.Size = new System.Drawing.Size(100, 20);
            this.txtDeducible.TabIndex = 125;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 123;
            this.label1.Text = "Monto en efectivo paga paciente: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 13);
            this.label2.TabIndex = 123;
            this.label2.Text = "Monto en porcentaje paga paciente: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCoaseguro
            // 
            this.txtCoaseguro.Location = new System.Drawing.Point(355, 86);
            this.txtCoaseguro.Name = "txtCoaseguro";
            this.txtCoaseguro.Size = new System.Drawing.Size(100, 20);
            this.txtCoaseguro.TabIndex = 125;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.add;
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(109, 133);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 126;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.delete;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(379, 133);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 127;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtUnidadProdId
            // 
            this.txtUnidadProdId.Location = new System.Drawing.Point(354, 107);
            this.txtUnidadProdId.Name = "txtUnidadProdId";
            this.txtUnidadProdId.Size = new System.Drawing.Size(100, 20);
            this.txtUnidadProdId.TabIndex = 125;
            this.txtUnidadProdId.Visible = false;
            // 
            // frmAddPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 177);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtUnidadProdId);
            this.Controls.Add(this.txtCoaseguro);
            this.Controls.Add(this.txtDeducible);
            this.Controls.Add(this.chkCoaseguro);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkDeducible);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbLine);
            this.Name = "frmAddPlan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Listado de Beneficios";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAddPlan_FormClosed);
            this.Load += new System.EventHandler(this.frmAddPlan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbLine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraCombo cbLine;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkDeducible;
        private System.Windows.Forms.CheckBox chkCoaseguro;
        private System.Windows.Forms.TextBox txtDeducible;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCoaseguro;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtUnidadProdId;
    }
}