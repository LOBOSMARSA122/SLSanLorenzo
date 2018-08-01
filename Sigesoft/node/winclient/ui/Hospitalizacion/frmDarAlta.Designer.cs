namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    partial class frmDarAlta
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
            this.dtpFechaAlta = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGuardarTicket = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpFechaAlta
            // 
            this.dtpFechaAlta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaAlta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaAlta.Location = new System.Drawing.Point(73, 5);
            this.dtpFechaAlta.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFechaAlta.Name = "dtpFechaAlta";
            this.dtpFechaAlta.ShowCheckBox = true;
            this.dtpFechaAlta.Size = new System.Drawing.Size(122, 20);
            this.dtpFechaAlta.TabIndex = 111;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 110;
            this.label3.Text = "Fecha Fin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 112;
            this.label1.Text = "Comentario";
            // 
            // txtComentario
            // 
            this.txtComentario.Location = new System.Drawing.Point(80, 41);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(192, 179);
            this.txtComentario.TabIndex = 113;
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.SystemColors.Control;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.Black;
            this.btnSalir.Image = global::Sigesoft.Node.WinClient.UI.Resources.bullet_cross;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(16, 226);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(76, 23);
            this.btnSalir.TabIndex = 115;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = false;
            // 
            // btnGuardarTicket
            // 
            this.btnGuardarTicket.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnGuardarTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarTicket.Location = new System.Drawing.Point(196, 226);
            this.btnGuardarTicket.Name = "btnGuardarTicket";
            this.btnGuardarTicket.Size = new System.Drawing.Size(76, 23);
            this.btnGuardarTicket.TabIndex = 114;
            this.btnGuardarTicket.Text = "Guardar";
            this.btnGuardarTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardarTicket.UseVisualStyleBackColor = true;
            this.btnGuardarTicket.Click += new System.EventHandler(this.btnGuardarTicket_Click);
            // 
            // frmDarAlta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnGuardarTicket);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFechaAlta);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDarAlta";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dar de Alta";
            this.Load += new System.EventHandler(this.frmDarAlta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpFechaAlta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGuardarTicket;
    }
}