namespace Sigesoft.Node.WinClient.UI
{
    partial class frmCheckingFinger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckingFinger));
            this.imgInfo = new System.Windows.Forms.PictureBox();
            this.lblresult = new System.Windows.Forms.Label();
            this.imgNO = new System.Windows.Forms.PictureBox();
            this.imgOK = new System.Windows.Forms.PictureBox();
            this.ZKFPEngX1 = new AxZKFPEngXControl.AxZKFPEngX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlPreviewTitle = new System.Windows.Forms.Panel();
            this.pbFingerPrint = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZKFPEngX1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.pnlPreviewTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // imgInfo
            // 
            this.imgInfo.Image = global::Sigesoft.Node.WinClient.UI.Resources.information;
            this.imgInfo.Location = new System.Drawing.Point(3, 273);
            this.imgInfo.Name = "imgInfo";
            this.imgInfo.Size = new System.Drawing.Size(17, 16);
            this.imgInfo.TabIndex = 47;
            this.imgInfo.TabStop = false;
            this.imgInfo.Visible = false;
            // 
            // lblresult
            // 
            this.lblresult.AutoSize = true;
            this.lblresult.Location = new System.Drawing.Point(26, 275);
            this.lblresult.Name = "lblresult";
            this.lblresult.Size = new System.Drawing.Size(0, 13);
            this.lblresult.TabIndex = 46;
            // 
            // imgNO
            // 
            this.imgNO.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.imgNO.Location = new System.Drawing.Point(5, 273);
            this.imgNO.Name = "imgNO";
            this.imgNO.Size = new System.Drawing.Size(17, 16);
            this.imgNO.TabIndex = 45;
            this.imgNO.TabStop = false;
            this.imgNO.Visible = false;
            // 
            // imgOK
            // 
            this.imgOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.imgOK.Location = new System.Drawing.Point(5, 273);
            this.imgOK.Name = "imgOK";
            this.imgOK.Size = new System.Drawing.Size(17, 16);
            this.imgOK.TabIndex = 44;
            this.imgOK.TabStop = false;
            this.imgOK.Visible = false;
            // 
            // ZKFPEngX1
            // 
            this.ZKFPEngX1.Enabled = true;
            this.ZKFPEngX1.Location = new System.Drawing.Point(1, 306);
            this.ZKFPEngX1.Name = "ZKFPEngX1";
            this.ZKFPEngX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ZKFPEngX1.OcxState")));
            this.ZKFPEngX1.Size = new System.Drawing.Size(24, 24);
            this.ZKFPEngX1.TabIndex = 43;
            this.ZKFPEngX1.OnFeatureInfo += new AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEventHandler(this.ZKFPEngX1_OnFeatureInfo);
            this.ZKFPEngX1.OnImageReceived += new AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEventHandler(this.ZKFPEngX1_OnImageReceived);
            //this.ZKFPEngX1.OnEnroll += new AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEventHandler(this.ZKFPEngX1_OnEnroll);
            this.ZKFPEngX1.OnCapture += new AxZKFPEngXControl.IZKFPEngXEvents_OnCaptureEventHandler(this.ZKFPEngX1_OnCapture);
            this.ZKFPEngX1.OnFingerTouching += new System.EventHandler(this.ZKFPEngX1_OnFingerTouching);
            this.ZKFPEngX1.OnFingerLeaving += new System.EventHandler(this.ZKFPEngX1_OnFingerLeaving);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnlPreviewTitle);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 263);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Huella";
            // 
            // pnlPreviewTitle
            // 
            this.pnlPreviewTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlPreviewTitle.BackColor = System.Drawing.Color.Silver;
            this.pnlPreviewTitle.Controls.Add(this.pbFingerPrint);
            this.pnlPreviewTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPreviewTitle.ForeColor = System.Drawing.Color.White;
            this.pnlPreviewTitle.Location = new System.Drawing.Point(6, 20);
            this.pnlPreviewTitle.Name = "pnlPreviewTitle";
            this.pnlPreviewTitle.Size = new System.Drawing.Size(282, 227);
            this.pnlPreviewTitle.TabIndex = 96;
            // 
            // pbFingerPrint
            // 
            this.pbFingerPrint.BackColor = System.Drawing.Color.Gray;
            this.pbFingerPrint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbFingerPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbFingerPrint.Location = new System.Drawing.Point(5, 8);
            this.pbFingerPrint.Name = "pbFingerPrint";
            this.pbFingerPrint.Size = new System.Drawing.Size(268, 210);
            this.pbFingerPrint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFingerPrint.TabIndex = 39;
            this.pbFingerPrint.TabStop = false;
            // 
            // frmCheckingFinger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 296);
            this.Controls.Add(this.imgInfo);
            this.Controls.Add(this.lblresult);
            this.Controls.Add(this.imgNO);
            this.Controls.Add(this.imgOK);
            this.Controls.Add(this.ZKFPEngX1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheckingFinger";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Verificación de huella digital";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCheckingFinger_FormClosing);
            this.Load += new System.EventHandler(this.frmCheckingFinger_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZKFPEngX1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.pnlPreviewTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerPrint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgInfo;
        private System.Windows.Forms.Label lblresult;
        private System.Windows.Forms.PictureBox imgNO;
        private System.Windows.Forms.PictureBox imgOK;
        private AxZKFPEngXControl.AxZKFPEngX ZKFPEngX1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnlPreviewTitle;
        private System.Windows.Forms.PictureBox pbFingerPrint;
    }
}