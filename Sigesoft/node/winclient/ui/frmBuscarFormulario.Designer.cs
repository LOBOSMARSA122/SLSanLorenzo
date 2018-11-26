namespace Sigesoft.Node.WinClient.UI
{
    partial class frmBuscarFormulario
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuscarFrm = new System.Windows.Forms.TextBox();
            this.btnBuscarFormulario = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Formulario";
            // 
            // txtBuscarFrm
            // 
            this.txtBuscarFrm.Location = new System.Drawing.Point(65, 17);
            this.txtBuscarFrm.Name = "txtBuscarFrm";
            this.txtBuscarFrm.Size = new System.Drawing.Size(293, 20);
            this.txtBuscarFrm.TabIndex = 1;
            // 
            // btnBuscarFormulario
            // 
            this.btnBuscarFormulario.Location = new System.Drawing.Point(364, 15);
            this.btnBuscarFormulario.Name = "btnBuscarFormulario";
            this.btnBuscarFormulario.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarFormulario.TabIndex = 2;
            this.btnBuscarFormulario.Text = "Buscar";
            this.btnBuscarFormulario.UseVisualStyleBackColor = true;
            this.btnBuscarFormulario.Click += new System.EventHandler(this.btnBuscarFormulario_Click);
            // 
            // frmBuscarFormulario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 61);
            this.Controls.Add(this.btnBuscarFormulario);
            this.Controls.Add(this.txtBuscarFrm);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBuscarFormulario";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar Formulario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBuscarFrm;
        private System.Windows.Forms.Button btnBuscarFormulario;
    }
}