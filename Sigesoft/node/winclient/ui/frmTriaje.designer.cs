namespace Sigesoft.Node.WinClient.UI
{
    partial class frmTriaje
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPeso = new System.Windows.Forms.TextBox();
            this.txtTalla = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtImc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPAD = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTemperatura = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPAS = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.txtICC = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPerAbd = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPerCad = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSatOx = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFrecCard = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFrecResp = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.uvValidador = new Infragistics.Win.Misc.UltraValidator(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uvValidador)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Peso: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPeso
            // 
            this.txtPeso.Location = new System.Drawing.Point(56, 42);
            this.txtPeso.Name = "txtPeso";
            this.txtPeso.Size = new System.Drawing.Size(100, 20);
            this.txtPeso.TabIndex = 1;
            this.txtPeso.Tag = "N002-MF000000008";
            this.uvValidador.GetValidationSettings(this.txtPeso).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvValidador.GetValidationSettings(this.txtPeso).IsRequired = true;
            this.txtPeso.TextChanged += new System.EventHandler(this.txtPeso_TextChanged);
            this.txtPeso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPeso_KeyPress);
            // 
            // txtTalla
            // 
            this.txtTalla.Location = new System.Drawing.Point(56, 13);
            this.txtTalla.Name = "txtTalla";
            this.txtTalla.Size = new System.Drawing.Size(100, 20);
            this.txtTalla.TabIndex = 0;
            this.txtTalla.Tag = "N002-MF000000007";
            this.uvValidador.GetValidationSettings(this.txtTalla).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvValidador.GetValidationSettings(this.txtTalla).IsRequired = true;
            this.txtTalla.TextChanged += new System.EventHandler(this.txtTalla_TextChanged);
            this.txtTalla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTalla_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Talla:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtImc
            // 
            this.txtImc.Enabled = false;
            this.txtImc.Location = new System.Drawing.Point(56, 71);
            this.txtImc.Name = "txtImc";
            this.txtImc.Size = new System.Drawing.Size(100, 20);
            this.txtImc.TabIndex = 2;
            this.txtImc.Tag = "N002-MF000000009";
            this.txtImc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImc_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "IMC:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPAD
            // 
            this.txtPAD.Location = new System.Drawing.Point(278, 71);
            this.txtPAD.Name = "txtPAD";
            this.txtPAD.Size = new System.Drawing.Size(100, 20);
            this.txtPAD.TabIndex = 8;
            this.txtPAD.Tag = "N002-MF000000002";
            this.uvValidador.GetValidationSettings(this.txtPAD).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvValidador.GetValidationSettings(this.txtPAD).IsRequired = true;
            this.txtPAD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPAD_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(219, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 26);
            this.label4.TabIndex = 10;
            this.label4.Text = "Presion A. \r\nDiastólica:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTemperatura
            // 
            this.txtTemperatura.Location = new System.Drawing.Point(278, 13);
            this.txtTemperatura.Name = "txtTemperatura";
            this.txtTemperatura.Size = new System.Drawing.Size(100, 20);
            this.txtTemperatura.TabIndex = 6;
            this.txtTemperatura.Tag = "N002-MF000000004";
            this.txtTemperatura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTemperatura_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(207, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Temperatura:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPAS
            // 
            this.txtPAS.Location = new System.Drawing.Point(278, 42);
            this.txtPAS.Name = "txtPAS";
            this.txtPAS.Size = new System.Drawing.Size(100, 20);
            this.txtPAS.TabIndex = 7;
            this.txtPAS.Tag = "N002-MF000000001";
            this.uvValidador.GetValidationSettings(this.txtPAS).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvValidador.GetValidationSettings(this.txtPAS).IsRequired = true;
            this.txtPAS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPAS_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 26);
            this.label6.TabIndex = 6;
            this.label6.Text = "Presion A. \r\nSitólica:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(303, 194);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "   Salir";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGrabar.BackColor = System.Drawing.SystemColors.Control;
            this.btnGrabar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGrabar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGrabar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnGrabar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrabar.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrabar.Location = new System.Drawing.Point(208, 194);
            this.btnGrabar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 24);
            this.btnGrabar.TabIndex = 12;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabar.UseVisualStyleBackColor = false;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // txtICC
            // 
            this.txtICC.Enabled = false;
            this.txtICC.Location = new System.Drawing.Point(56, 158);
            this.txtICC.Name = "txtICC";
            this.txtICC.Size = new System.Drawing.Size(100, 20);
            this.txtICC.TabIndex = 5;
            this.txtICC.Tag = "N002-MF000000012";
            this.txtICC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtICC_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "ICC:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPerAbd
            // 
            this.txtPerAbd.Location = new System.Drawing.Point(56, 100);
            this.txtPerAbd.Name = "txtPerAbd";
            this.txtPerAbd.Size = new System.Drawing.Size(100, 20);
            this.txtPerAbd.TabIndex = 3;
            this.txtPerAbd.Tag = "N002-MF000000010";
            this.txtPerAbd.TextChanged += new System.EventHandler(this.txtPerAbd_TextChanged);
            this.txtPerAbd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPerAbd_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 26);
            this.label8.TabIndex = 15;
            this.label8.Text = "Perímetro \r\nAbdomen:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPerCad
            // 
            this.txtPerCad.Location = new System.Drawing.Point(56, 129);
            this.txtPerCad.Name = "txtPerCad";
            this.txtPerCad.Size = new System.Drawing.Size(100, 20);
            this.txtPerCad.TabIndex = 4;
            this.txtPerCad.Tag = "N002-MF000000011";
            this.txtPerCad.TextChanged += new System.EventHandler(this.txtPerCad_TextChanged);
            this.txtPerCad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPerCad_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 26);
            this.label9.TabIndex = 12;
            this.label9.Text = "Perímetro\r\nCadera:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSatOx
            // 
            this.txtSatOx.Location = new System.Drawing.Point(278, 158);
            this.txtSatOx.Name = "txtSatOx";
            this.txtSatOx.Size = new System.Drawing.Size(100, 20);
            this.txtSatOx.TabIndex = 11;
            this.txtSatOx.Tag = "N002-MF000000006";
            this.txtSatOx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSatOx_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(211, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 26);
            this.label10.TabIndex = 22;
            this.label10.Text = "Satuación\r\nde Oxígeno:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFrecCard
            // 
            this.txtFrecCard.Location = new System.Drawing.Point(278, 100);
            this.txtFrecCard.Name = "txtFrecCard";
            this.txtFrecCard.Size = new System.Drawing.Size(100, 20);
            this.txtFrecCard.TabIndex = 9;
            this.txtFrecCard.Tag = "N002-MF000000003";
            this.txtFrecCard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrecCard_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(217, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 26);
            this.label11.TabIndex = 21;
            this.label11.Text = "Frecuencia\r\nCardiaca:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFrecResp
            // 
            this.txtFrecResp.Location = new System.Drawing.Point(278, 129);
            this.txtFrecResp.Name = "txtFrecResp";
            this.txtFrecResp.Size = new System.Drawing.Size(100, 20);
            this.txtFrecResp.TabIndex = 10;
            this.txtFrecResp.Tag = "N002-MF000000005";
            this.txtFrecResp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrecResp_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(211, 126);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 26);
            this.label12.TabIndex = 20;
            this.label12.Text = "Frecuencia\r\nRespiratoria:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmTriaje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(396, 229);
            this.Controls.Add(this.txtSatOx);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtFrecCard);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtFrecResp);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtICC);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPerAbd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPerCad);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.txtPAD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTemperatura);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPAS);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtImc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTalla);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPeso);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTriaje";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Peso / Talla";
            this.Load += new System.EventHandler(this.frmTriaje_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uvValidador)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPeso;
        private System.Windows.Forms.TextBox txtTalla;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtImc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPAD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTemperatura;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPAS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.TextBox txtICC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPerAbd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPerCad;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSatOx;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFrecCard;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFrecResp;
        private System.Windows.Forms.Label label12;
        private Infragistics.Win.Misc.UltraValidator uvValidador;
    }
}