namespace Sigesoft.Node.WinClient.UI.UserControls
{
    partial class ucFotoTipo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFotoTipo));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNroLunares = new System.Windows.Forms.TextBox();
            this.txtNroPecas = new System.Windows.Forms.TextBox();
            this.txtNroCicatrices = new System.Windows.Forms.TextBox();
            this.txtNroManchas = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtMultimediaFileId = new System.Windows.Forms.TextBox();
            this.txtServiceComponentMultimediaId = new System.Windows.Forms.TextBox();
            this.btnDibujar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nro. Lunares";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nro. Pecas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(173, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nro. Cicatrices";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(173, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nro. Machas";
            // 
            // txtNroLunares
            // 
            this.txtNroLunares.Location = new System.Drawing.Point(97, 20);
            this.txtNroLunares.Name = "txtNroLunares";
            this.txtNroLunares.Size = new System.Drawing.Size(64, 20);
            this.txtNroLunares.TabIndex = 8;
            // 
            // txtNroPecas
            // 
            this.txtNroPecas.Location = new System.Drawing.Point(97, 46);
            this.txtNroPecas.Name = "txtNroPecas";
            this.txtNroPecas.Size = new System.Drawing.Size(64, 20);
            this.txtNroPecas.TabIndex = 9;
            // 
            // txtNroCicatrices
            // 
            this.txtNroCicatrices.Location = new System.Drawing.Point(268, 46);
            this.txtNroCicatrices.Name = "txtNroCicatrices";
            this.txtNroCicatrices.Size = new System.Drawing.Size(64, 20);
            this.txtNroCicatrices.TabIndex = 11;
            // 
            // txtNroManchas
            // 
            this.txtNroManchas.Location = new System.Drawing.Point(268, 20);
            this.txtNroManchas.Name = "txtNroManchas";
            this.txtNroManchas.Size = new System.Drawing.Size(64, 20);
            this.txtNroManchas.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Location = new System.Drawing.Point(40, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(414, 416);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtNroLunares);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtNroPecas);
            this.groupBox4.Controls.Add(this.txtNroCicatrices);
            this.groupBox4.Controls.Add(this.txtNroManchas);
            this.groupBox4.Location = new System.Drawing.Point(493, 282);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(348, 78);
            this.groupBox4.TabIndex = 28;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Coordenadas";
            // 
            // txtMultimediaFileId
            // 
            this.txtMultimediaFileId.Location = new System.Drawing.Point(499, 366);
            this.txtMultimediaFileId.Name = "txtMultimediaFileId";
            this.txtMultimediaFileId.Size = new System.Drawing.Size(157, 20);
            this.txtMultimediaFileId.TabIndex = 12;
            // 
            // txtServiceComponentMultimediaId
            // 
            this.txtServiceComponentMultimediaId.Location = new System.Drawing.Point(499, 392);
            this.txtServiceComponentMultimediaId.Name = "txtServiceComponentMultimediaId";
            this.txtServiceComponentMultimediaId.Size = new System.Drawing.Size(157, 20);
            this.txtServiceComponentMultimediaId.TabIndex = 13;
            // 
            // btnDibujar
            // 
            this.btnDibujar.Location = new System.Drawing.Point(507, 33);
            this.btnDibujar.Name = "btnDibujar";
            this.btnDibujar.Size = new System.Drawing.Size(75, 23);
            this.btnDibujar.TabIndex = 29;
            this.btnDibujar.Text = "Dibujar";
            this.btnDibujar.UseVisualStyleBackColor = true;
            this.btnDibujar.Click += new System.EventHandler(this.btnDibujar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(509, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucFotoTipo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDibujar);
            this.Controls.Add(this.txtMultimediaFileId);
            this.Controls.Add(this.txtServiceComponentMultimediaId);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel1);
            this.Name = "ucFotoTipo";
            this.Size = new System.Drawing.Size(995, 426);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtNroCicatrices;
        private System.Windows.Forms.TextBox txtNroManchas;
        private System.Windows.Forms.TextBox txtNroPecas;
        private System.Windows.Forms.TextBox txtNroLunares;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtMultimediaFileId;
        private System.Windows.Forms.TextBox txtServiceComponentMultimediaId;
        private System.Windows.Forms.Button btnDibujar;
        private System.Windows.Forms.Button button1;
    }
}
