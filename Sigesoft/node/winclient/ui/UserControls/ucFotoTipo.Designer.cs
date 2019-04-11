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
            this.txtServiceComponentMultimediaId = new System.Windows.Forms.TextBox();
            this.txtMultimediaFileId = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPintar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtServiceComponentMultimediaId
            // 
            this.txtServiceComponentMultimediaId.Location = new System.Drawing.Point(499, 392);
            this.txtServiceComponentMultimediaId.Name = "txtServiceComponentMultimediaId";
            this.txtServiceComponentMultimediaId.Size = new System.Drawing.Size(157, 20);
            this.txtServiceComponentMultimediaId.TabIndex = 13;
            // 
            // txtMultimediaFileId
            // 
            this.txtMultimediaFileId.Location = new System.Drawing.Point(499, 366);
            this.txtMultimediaFileId.Name = "txtMultimediaFileId";
            this.txtMultimediaFileId.Size = new System.Drawing.Size(157, 20);
            this.txtMultimediaFileId.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pictureBox1.BackgroundImage = global::Sigesoft.Node.WinClient.UI.Properties.Resources.fototipo;
            this.pictureBox1.Location = new System.Drawing.Point(19, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(419, 395);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // btnPintar
            // 
            this.btnPintar.Location = new System.Drawing.Point(577, 95);
            this.btnPintar.Name = "btnPintar";
            this.btnPintar.Size = new System.Drawing.Size(110, 23);
            this.btnPintar.TabIndex = 15;
            this.btnPintar.Text = "Pintar Lunares";
            this.btnPintar.UseVisualStyleBackColor = true;
            this.btnPintar.Click += new System.EventHandler(this.btnPintar_Click);
            // 
            // ucFotoTipo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.btnPintar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtMultimediaFileId);
            this.Controls.Add(this.txtServiceComponentMultimediaId);
            this.Name = "ucFotoTipo";
            this.Size = new System.Drawing.Size(995, 426);
            this.Load += new System.EventHandler(this.MainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServiceComponentMultimediaId;
        private System.Windows.Forms.TextBox txtMultimediaFileId;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnPintar;
    }
}
