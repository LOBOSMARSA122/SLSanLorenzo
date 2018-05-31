namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    partial class frmPopupAtencionIntegral
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboTipoAtencionIntegral = new System.Windows.Forms.ComboBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.txtProblema = new System.Windows.Forms.TextBox();
            this.cboEsControlado = new System.Windows.Forms.ComboBox();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.txtLugar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Problema";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "¿Es Controlado?";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Obs.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tipo";
            // 
            // cboTipoAtencionIntegral
            // 
            this.cboTipoAtencionIntegral.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoAtencionIntegral.FormattingEnabled = true;
            this.cboTipoAtencionIntegral.Location = new System.Drawing.Point(77, 9);
            this.cboTipoAtencionIntegral.Name = "cboTipoAtencionIntegral";
            this.cboTipoAtencionIntegral.Size = new System.Drawing.Size(721, 21);
            this.cboTipoAtencionIntegral.TabIndex = 5;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(77, 38);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(88, 21);
            this.dtpFecha.TabIndex = 6;
            // 
            // txtProblema
            // 
            this.txtProblema.Location = new System.Drawing.Point(77, 64);
            this.txtProblema.Name = "txtProblema";
            this.txtProblema.Size = new System.Drawing.Size(721, 20);
            this.txtProblema.TabIndex = 7;
            // 
            // cboEsControlado
            // 
            this.cboEsControlado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEsControlado.FormattingEnabled = true;
            this.cboEsControlado.Location = new System.Drawing.Point(77, 90);
            this.cboEsControlado.Name = "cboEsControlado";
            this.cboEsControlado.Size = new System.Drawing.Size(67, 21);
            this.cboEsControlado.TabIndex = 8;
            // 
            // txtObservacion
            // 
            this.txtObservacion.Location = new System.Drawing.Point(77, 123);
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(721, 20);
            this.txtObservacion.TabIndex = 9;
            // 
            // txtLugar
            // 
            this.txtLugar.Location = new System.Drawing.Point(77, 149);
            this.txtLugar.Name = "txtLugar";
            this.txtLugar.Size = new System.Drawing.Size(721, 20);
            this.txtLugar.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Lugar";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardar.BackColor = System.Drawing.SystemColors.Control;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(712, 182);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(87, 24);
            this.btnGuardar.TabIndex = 61;
            this.btnGuardar.Text = "      Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmPopupAtencionIntegral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 217);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtLugar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtObservacion);
            this.Controls.Add(this.cboEsControlado);
            this.Controls.Add(this.txtProblema);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.cboTipoAtencionIntegral);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPopupAtencionIntegral";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "texto";
            this.Load += new System.EventHandler(this.frmPopupAtencionIntegral_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboTipoAtencionIntegral;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.TextBox txtProblema;
        private System.Windows.Forms.ComboBox cboEsControlado;
        private System.Windows.Forms.TextBox txtObservacion;
        private System.Windows.Forms.TextBox txtLugar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGuardar;
    }
}