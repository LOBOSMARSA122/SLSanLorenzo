namespace Sigesoft.Node.WinClient.UI
{
    partial class frmRegistroRadiografiaTorax
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtConclusiones = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtIndice = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMediastinos = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSenosDiag = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCamposPulmonares = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPartesBlandas = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSiluetaCardia = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSenosCardio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHilios = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVertices = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(545, 342);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 30);
            this.btnCancel.TabIndex = 52;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(469, 342);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 30);
            this.btnOK.TabIndex = 51;
            this.btnOK.Text = "Guardar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtConclusiones
            // 
            this.txtConclusiones.Location = new System.Drawing.Point(461, 264);
            this.txtConclusiones.Multiline = true;
            this.txtConclusiones.Name = "txtConclusiones";
            this.txtConclusiones.Size = new System.Drawing.Size(153, 57);
            this.txtConclusiones.TabIndex = 48;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(318, 267);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "Conclusiones Radiográficas";
            // 
            // txtIndice
            // 
            this.txtIndice.Location = new System.Drawing.Point(461, 201);
            this.txtIndice.Multiline = true;
            this.txtIndice.Name = "txtIndice";
            this.txtIndice.Size = new System.Drawing.Size(153, 57);
            this.txtIndice.TabIndex = 46;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(343, 204);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "Índice Cardio-Toráxico";
            // 
            // txtMediastinos
            // 
            this.txtMediastinos.Location = new System.Drawing.Point(461, 138);
            this.txtMediastinos.Multiline = true;
            this.txtMediastinos.Name = "txtMediastinos";
            this.txtMediastinos.Size = new System.Drawing.Size(153, 57);
            this.txtMediastinos.TabIndex = 44;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(390, 141);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 43;
            this.label10.Text = "Mediastinos:";
            // 
            // txtSenosDiag
            // 
            this.txtSenosDiag.Location = new System.Drawing.Point(461, 75);
            this.txtSenosDiag.Multiline = true;
            this.txtSenosDiag.Name = "txtSenosDiag";
            this.txtSenosDiag.Size = new System.Drawing.Size(153, 57);
            this.txtSenosDiag.TabIndex = 42;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(346, 78);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 13);
            this.label11.TabIndex = 41;
            this.label11.Text = "Senos Diafragmáticos";
            // 
            // txtCamposPulmonares
            // 
            this.txtCamposPulmonares.Location = new System.Drawing.Point(461, 12);
            this.txtCamposPulmonares.Multiline = true;
            this.txtCamposPulmonares.Name = "txtCamposPulmonares";
            this.txtCamposPulmonares.Size = new System.Drawing.Size(153, 57);
            this.txtCamposPulmonares.TabIndex = 40;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(350, 15);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 13);
            this.label12.TabIndex = 39;
            this.label12.Text = "Campos Pulmonares:";
            // 
            // txtPartesBlandas
            // 
            this.txtPartesBlandas.Location = new System.Drawing.Point(133, 267);
            this.txtPartesBlandas.Multiline = true;
            this.txtPartesBlandas.Name = "txtPartesBlandas";
            this.txtPartesBlandas.Size = new System.Drawing.Size(153, 57);
            this.txtPartesBlandas.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 270);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Partes Blandas y Óseas:";
            // 
            // txtSiluetaCardia
            // 
            this.txtSiluetaCardia.Location = new System.Drawing.Point(133, 204);
            this.txtSiluetaCardia.Multiline = true;
            this.txtSiluetaCardia.Name = "txtSiluetaCardia";
            this.txtSiluetaCardia.Size = new System.Drawing.Size(153, 57);
            this.txtSiluetaCardia.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(47, 207);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Silueta Cardica:";
            // 
            // txtSenosCardio
            // 
            this.txtSenosCardio.Location = new System.Drawing.Point(133, 141);
            this.txtSenosCardio.Multiline = true;
            this.txtSenosCardio.Name = "txtSenosCardio";
            this.txtSenosCardio.Size = new System.Drawing.Size(153, 57);
            this.txtSenosCardio.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(21, 144);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Senos Cardiofrénicos:";
            // 
            // txtHilios
            // 
            this.txtHilios.Location = new System.Drawing.Point(130, 78);
            this.txtHilios.Multiline = true;
            this.txtHilios.Name = "txtHilios";
            this.txtHilios.Size = new System.Drawing.Size(153, 57);
            this.txtHilios.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(91, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Hílios:";
            // 
            // txtVertices
            // 
            this.txtVertices.Location = new System.Drawing.Point(130, 15);
            this.txtVertices.Multiline = true;
            this.txtVertices.Name = "txtVertices";
            this.txtVertices.Size = new System.Drawing.Size(153, 57);
            this.txtVertices.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(80, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Vértices:";
            // 
            // frmRegistroRadiografiaTorax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(630, 383);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtConclusiones);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtIndice);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtMediastinos);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtSenosDiag);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtCamposPulmonares);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtPartesBlandas);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSiluetaCardia);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSenosCardio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHilios);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtVertices);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRegistroRadiografiaTorax";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Radiografía de Torax";
            this.Load += new System.EventHandler(this.frmRegistroRadiografiaTorax_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtConclusiones;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtIndice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMediastinos;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSenosDiag;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCamposPulmonares;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPartesBlandas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSiluetaCardia;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSenosCardio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHilios;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVertices;
        private System.Windows.Forms.Label label1;
    }
}