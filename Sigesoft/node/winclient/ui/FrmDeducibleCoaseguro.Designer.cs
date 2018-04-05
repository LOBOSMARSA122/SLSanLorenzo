namespace Sigesoft.Node.WinClient.UI
{
    partial class FrmDeducibleCoaseguro
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
            this.rbDeducible = new System.Windows.Forms.RadioButton();
            this.rbCoaseguro = new System.Windows.Forms.RadioButton();
            this.txtMondoDedCoas = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.btnGrabarDeducible = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMondoDedCoas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDeducible);
            this.groupBox1.Controls.Add(this.rbCoaseguro);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 46);
            this.groupBox1.TabIndex = 105;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccionar Deducible / Coaseguro";
            // 
            // rbDeducible
            // 
            this.rbDeducible.AutoSize = true;
            this.rbDeducible.Checked = true;
            this.rbDeducible.Location = new System.Drawing.Point(41, 19);
            this.rbDeducible.Name = "rbDeducible";
            this.rbDeducible.Size = new System.Drawing.Size(73, 17);
            this.rbDeducible.TabIndex = 25;
            this.rbDeducible.TabStop = true;
            this.rbDeducible.Text = "Deducible";
            this.rbDeducible.UseVisualStyleBackColor = true;
            // 
            // rbCoaseguro
            // 
            this.rbCoaseguro.AutoSize = true;
            this.rbCoaseguro.Location = new System.Drawing.Point(133, 19);
            this.rbCoaseguro.Name = "rbCoaseguro";
            this.rbCoaseguro.Size = new System.Drawing.Size(76, 17);
            this.rbCoaseguro.TabIndex = 26;
            this.rbCoaseguro.TabStop = true;
            this.rbCoaseguro.Text = "Coaseguro";
            this.rbCoaseguro.UseVisualStyleBackColor = true;
            // 
            // txtMondoDedCoas
            // 
            this.txtMondoDedCoas.AutoSize = false;
            this.txtMondoDedCoas.Location = new System.Drawing.Point(103, 63);
            this.txtMondoDedCoas.Margin = new System.Windows.Forms.Padding(2);
            this.txtMondoDedCoas.MaxValue = 9999;
            this.txtMondoDedCoas.Name = "txtMondoDedCoas";
            this.txtMondoDedCoas.PromptChar = ' ';
            this.txtMondoDedCoas.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMondoDedCoas.Size = new System.Drawing.Size(69, 28);
            this.txtMondoDedCoas.TabIndex = 27;
            // 
            // btnGrabarDeducible
            // 
            this.btnGrabarDeducible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrabarDeducible.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnGrabarDeducible.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrabarDeducible.Location = new System.Drawing.Point(65, 112);
            this.btnGrabarDeducible.Margin = new System.Windows.Forms.Padding(2);
            this.btnGrabarDeducible.Name = "btnGrabarDeducible";
            this.btnGrabarDeducible.Size = new System.Drawing.Size(75, 24);
            this.btnGrabarDeducible.TabIndex = 13;
            this.btnGrabarDeducible.Text = "Guardar";
            this.btnGrabarDeducible.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabarDeducible.UseVisualStyleBackColor = true;
            this.btnGrabarDeducible.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(145, 112);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Salir";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmDeducibleCoaseguro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 147);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtMondoDedCoas);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGrabarDeducible);
            this.Name = "FrmDeducibleCoaseguro";
            this.Text = "Seleccionar Descuento";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMondoDedCoas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDeducible;
        private System.Windows.Forms.RadioButton rbCoaseguro;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor txtMondoDedCoas;
        private System.Windows.Forms.Button btnGrabarDeducible;
        private System.Windows.Forms.Button btnCancel;
    }
}