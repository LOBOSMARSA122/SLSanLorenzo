namespace Sigesoft.Node.WinClient.UI
{
    partial class frmInterconsulta
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
            this.txtAltitudLabor = new System.Windows.Forms.TextBox();
            this.txtEspConsultar = new System.Windows.Forms.TextBox();
            this.txtRiegoLabor = new System.Windows.Forms.TextBox();
            this.txtSolicita = new System.Windows.Forms.TextBox();
            this.chklistDx = new System.Windows.Forms.CheckedListBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Altitud de labor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Especialidad a consultar";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Riesgo de labor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Se solicita";
            // 
            // txtAltitudLabor
            // 
            this.txtAltitudLabor.Location = new System.Drawing.Point(133, 18);
            this.txtAltitudLabor.Name = "txtAltitudLabor";
            this.txtAltitudLabor.Size = new System.Drawing.Size(208, 20);
            this.txtAltitudLabor.TabIndex = 4;
            // 
            // txtEspConsultar
            // 
            this.txtEspConsultar.Location = new System.Drawing.Point(133, 52);
            this.txtEspConsultar.Name = "txtEspConsultar";
            this.txtEspConsultar.Size = new System.Drawing.Size(208, 20);
            this.txtEspConsultar.TabIndex = 5;
            // 
            // txtRiegoLabor
            // 
            this.txtRiegoLabor.Location = new System.Drawing.Point(133, 91);
            this.txtRiegoLabor.Name = "txtRiegoLabor";
            this.txtRiegoLabor.Size = new System.Drawing.Size(208, 20);
            this.txtRiegoLabor.TabIndex = 6;
            // 
            // txtSolicita
            // 
            this.txtSolicita.Location = new System.Drawing.Point(133, 125);
            this.txtSolicita.Name = "txtSolicita";
            this.txtSolicita.Size = new System.Drawing.Size(208, 20);
            this.txtSolicita.TabIndex = 7;
            // 
            // chklistDx
            // 
            this.chklistDx.FormattingEnabled = true;
            this.chklistDx.Location = new System.Drawing.Point(15, 299);
            this.chklistDx.Name = "chklistDx";
            this.chklistDx.Size = new System.Drawing.Size(326, 154);
            this.chklistDx.TabIndex = 8;
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.SystemColors.Control;
            this.btnFilter.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFilter.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFilter.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.btnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilter.Location = new System.Drawing.Point(266, 458);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 24);
            this.btnFilter.TabIndex = 107;
            this.btnFilter.Text = "Generar";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(133, 151);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(208, 133);
            this.txtObservaciones.TabIndex = 109;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 108;
            this.label5.Text = "Motivo de Interconsulta";
            // 
            // frmInterconsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 490);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.chklistDx);
            this.Controls.Add(this.txtSolicita);
            this.Controls.Add(this.txtRiegoLabor);
            this.Controls.Add(this.txtEspConsultar);
            this.Controls.Add(this.txtAltitudLabor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInterconsulta";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar Interconsulta";
            this.Load += new System.EventHandler(this.frmInterconsulta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAltitudLabor;
        private System.Windows.Forms.TextBox txtEspConsultar;
        private System.Windows.Forms.TextBox txtRiegoLabor;
        private System.Windows.Forms.TextBox txtSolicita;
        private System.Windows.Forms.CheckedListBox chklistDx;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label5;
    }
}