namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    partial class frmPreviewImagePerson
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
            this.pbImagePerson = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagePerson)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImagePerson
            // 
            this.pbImagePerson.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbImagePerson.BackColor = System.Drawing.Color.Gray;
            this.pbImagePerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImagePerson.Location = new System.Drawing.Point(20, 20);
            this.pbImagePerson.Name = "pbImagePerson";
            this.pbImagePerson.Size = new System.Drawing.Size(666, 469);
            this.pbImagePerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImagePerson.TabIndex = 82;
            this.pbImagePerson.TabStop = false;
            // 
            // frmPreviewImagePerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(708, 513);
            this.Controls.Add(this.pbImagePerson);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPreviewImagePerson";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPreviewImagePerson_FormClosing);
            this.Load += new System.EventHandler(this.frmPreviewImagePerson_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImagePerson)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImagePerson;
    }
}