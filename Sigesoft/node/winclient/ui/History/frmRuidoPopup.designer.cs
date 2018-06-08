namespace Sigesoft.Node.WinClient.UI.History
{
    partial class frmRuidoPopup
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
            this.cbTiempoExposicionRuido = new System.Windows.Forms.ComboBox();
            this.cbNivelRuido = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtFuenteRuido = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbTiempoExposicionRuido
            // 
            this.cbTiempoExposicionRuido.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTiempoExposicionRuido.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTiempoExposicionRuido.FormattingEnabled = true;
            this.cbTiempoExposicionRuido.Location = new System.Drawing.Point(122, 10);
            this.cbTiempoExposicionRuido.Name = "cbTiempoExposicionRuido";
            this.cbTiempoExposicionRuido.Size = new System.Drawing.Size(181, 21);
            this.cbTiempoExposicionRuido.TabIndex = 36;
            // 
            // cbNivelRuido
            // 
            this.cbNivelRuido.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbNivelRuido.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbNivelRuido.FormattingEnabled = true;
            this.cbNivelRuido.Location = new System.Drawing.Point(122, 69);
            this.cbNivelRuido.Name = "cbNivelRuido";
            this.cbNivelRuido.Size = new System.Drawing.Size(181, 21);
            this.cbNivelRuido.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 29);
            this.label1.TabIndex = 38;
            this.label1.Text = "Tiempo de exposición al ruido (hs.)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Nivel de ruido";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Fuente de ruido";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(233, 106);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 35;
            this.btnCancel.Text = "    Salir";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(157, 106);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 24);
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "Aceptar";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFuenteRuido
            // 
            this.txtFuenteRuido.Location = new System.Drawing.Point(122, 42);
            this.txtFuenteRuido.Name = "txtFuenteRuido";
            this.txtFuenteRuido.Size = new System.Drawing.Size(181, 20);
            this.txtFuenteRuido.TabIndex = 42;
            // 
            // frmRuidoPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(306, 132);
            this.Controls.Add(this.txtFuenteRuido);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbNivelRuido);
            this.Controls.Add(this.cbTiempoExposicionRuido);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRuidoPopup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ruido";
            this.Load += new System.EventHandler(this.frmRuidoPopup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbTiempoExposicionRuido;
        private System.Windows.Forms.ComboBox cbNivelRuido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFuenteRuido;
    }
}