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
            this.btnBuscarFormulario = new System.Windows.Forms.Button();
            this.cboFormularios = new System.Windows.Forms.ComboBox();
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
            // btnBuscarFormulario
            // 
            this.btnBuscarFormulario.Location = new System.Drawing.Point(321, 41);
            this.btnBuscarFormulario.Name = "btnBuscarFormulario";
            this.btnBuscarFormulario.Size = new System.Drawing.Size(49, 23);
            this.btnBuscarFormulario.TabIndex = 1;
            this.btnBuscarFormulario.Text = "Ir";
            this.btnBuscarFormulario.UseVisualStyleBackColor = true;
            this.btnBuscarFormulario.Click += new System.EventHandler(this.btnBuscarFormulario_Click);
            // 
            // cboFormularios
            // 
            this.cboFormularios.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboFormularios.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFormularios.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFormularios.FormattingEnabled = true;
            this.cboFormularios.Location = new System.Drawing.Point(68, 15);
            this.cboFormularios.Margin = new System.Windows.Forms.Padding(2);
            this.cboFormularios.Name = "cboFormularios";
            this.cboFormularios.Size = new System.Drawing.Size(302, 21);
            this.cboFormularios.TabIndex = 0;
            // 
            // frmBuscarFormulario
            // 
            this.AcceptButton = this.btnBuscarFormulario;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 73);
            this.Controls.Add(this.cboFormularios);
            this.Controls.Add(this.btnBuscarFormulario);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBuscarFormulario";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar Formulario";
            this.Load += new System.EventHandler(this.frmBuscarFormulario_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBuscarFormulario_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscarFormulario;
        private System.Windows.Forms.ComboBox cboFormularios;
    }
}