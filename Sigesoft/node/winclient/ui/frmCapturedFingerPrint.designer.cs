namespace Sigesoft.Node.WinClient.UI
{
    partial class frmCapturedFingerPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCapturedFingerPrint));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbFirma = new System.Windows.Forms.PictureBox();
            this.sigPlusNET1 = new Topaz.SigPlusNET();
            this.btnDelSignature = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlPreviewTitle = new System.Windows.Forms.Panel();
            this.pbFingerPrint = new System.Windows.Forms.PictureBox();
            this.btnDelFingerPrint = new System.Windows.Forms.Button();
            this.ZKFPEngX1 = new AxZKFPEngXControl.AxZKFPEngX();
            this.memoHint = new System.Windows.Forms.TextBox();
            this.btnEnroll = new System.Windows.Forms.Button();
            this.lblresult = new System.Windows.Forms.Label();
            this.btnAutoverify = new System.Windows.Forms.Button();
            this.imgInfo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imgNO = new System.Windows.Forms.PictureBox();
            this.imgOK = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.imgInfoFirma = new System.Windows.Forms.PictureBox();
            this.imgNoFirma = new System.Windows.Forms.PictureBox();
            this.imgOkFirma = new System.Windows.Forms.PictureBox();
            this.lblResultFirma = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFirma)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.pnlPreviewTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZKFPEngX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgInfoFirma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgNoFirma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOkFirma)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox1.Location = new System.Drawing.Point(326, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 263);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Firma";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.pbFirma);
            this.panel1.Controls.Add(this.sigPlusNET1);
            this.panel1.Controls.Add(this.btnDelSignature);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(8, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 228);
            this.panel1.TabIndex = 97;
            // 
            // pbFirma
            // 
            this.pbFirma.BackColor = System.Drawing.Color.White;
            this.pbFirma.Location = new System.Drawing.Point(9, 27);
            this.pbFirma.Name = "pbFirma";
            this.pbFirma.Size = new System.Drawing.Size(375, 187);
            this.pbFirma.TabIndex = 96;
            this.pbFirma.TabStop = false;
            this.pbFirma.Visible = false;
            // 
            // sigPlusNET1
            // 
            this.sigPlusNET1.BackColor = System.Drawing.Color.White;
            this.sigPlusNET1.ForeColor = System.Drawing.Color.Black;
            this.sigPlusNET1.Location = new System.Drawing.Point(5, 23);
            this.sigPlusNET1.Name = "sigPlusNET1";
            this.sigPlusNET1.Size = new System.Drawing.Size(384, 197);
            this.sigPlusNET1.TabIndex = 49;
            this.sigPlusNET1.Text = "sigPlusNET1";
            // 
            // btnDelSignature
            // 
            this.btnDelSignature.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnDelSignature.Location = new System.Drawing.Point(371, 1);
            this.btnDelSignature.Name = "btnDelSignature";
            this.btnDelSignature.Size = new System.Drawing.Size(20, 19);
            this.btnDelSignature.TabIndex = 0;
            this.btnDelSignature.UseVisualStyleBackColor = true;
            this.btnDelSignature.Click += new System.EventHandler(this.btnDelSignature_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnlPreviewTitle);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox2.Location = new System.Drawing.Point(17, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 263);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Huella";
            // 
            // pnlPreviewTitle
            // 
            this.pnlPreviewTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlPreviewTitle.BackColor = System.Drawing.Color.Silver;
            this.pnlPreviewTitle.Controls.Add(this.pbFingerPrint);
            this.pnlPreviewTitle.Controls.Add(this.btnDelFingerPrint);
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
            this.pbFingerPrint.Location = new System.Drawing.Point(5, 22);
            this.pbFingerPrint.Name = "pbFingerPrint";
            this.pbFingerPrint.Size = new System.Drawing.Size(268, 197);
            this.pbFingerPrint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFingerPrint.TabIndex = 39;
            this.pbFingerPrint.TabStop = false;
            // 
            // btnDelFingerPrint
            // 
            this.btnDelFingerPrint.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnDelFingerPrint.Location = new System.Drawing.Point(255, 1);
            this.btnDelFingerPrint.Name = "btnDelFingerPrint";
            this.btnDelFingerPrint.Size = new System.Drawing.Size(20, 19);
            this.btnDelFingerPrint.TabIndex = 0;
            this.btnDelFingerPrint.UseVisualStyleBackColor = true;
            this.btnDelFingerPrint.Click += new System.EventHandler(this.btnDelFingerPrint_Click);
            // 
            // ZKFPEngX1
            // 
            this.ZKFPEngX1.Enabled = true;
            this.ZKFPEngX1.Location = new System.Drawing.Point(15, 317);
            this.ZKFPEngX1.Name = "ZKFPEngX1";
            this.ZKFPEngX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ZKFPEngX1.OcxState")));
            this.ZKFPEngX1.Size = new System.Drawing.Size(24, 24);
            this.ZKFPEngX1.TabIndex = 18;
            this.ZKFPEngX1.OnFeatureInfo += new AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEventHandler(this.ZKFPEngX1_OnFeatureInfo);
            this.ZKFPEngX1.OnImageReceived += new AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEventHandler(this.ZKFPEngX1_OnImageReceived);
            this.ZKFPEngX1.OnEnroll += new AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEventHandler(this.ZKFPEngX1_OnEnroll);
            this.ZKFPEngX1.OnCapture += new AxZKFPEngXControl.IZKFPEngXEvents_OnCaptureEventHandler(this.ZKFPEngX1_OnCapture);
            this.ZKFPEngX1.OnFingerTouching += new System.EventHandler(this.ZKFPEngX1_OnFingerTouching);
            this.ZKFPEngX1.OnFingerLeaving += new System.EventHandler(this.ZKFPEngX1_OnFingerLeaving);
            // 
            // memoHint
            // 
            this.memoHint.Location = new System.Drawing.Point(786, 18);
            this.memoHint.Multiline = true;
            this.memoHint.Name = "memoHint";
            this.memoHint.Size = new System.Drawing.Size(103, 183);
            this.memoHint.TabIndex = 21;
            // 
            // btnEnroll
            // 
            this.btnEnroll.Location = new System.Drawing.Point(938, 176);
            this.btnEnroll.Name = "btnEnroll";
            this.btnEnroll.Size = new System.Drawing.Size(75, 25);
            this.btnEnroll.TabIndex = 22;
            this.btnEnroll.Text = "Register";
            this.btnEnroll.UseVisualStyleBackColor = true;
            this.btnEnroll.Click += new System.EventHandler(this.EnrollFingerPrint);
            // 
            // lblresult
            // 
            this.lblresult.AutoSize = true;
            this.lblresult.Location = new System.Drawing.Point(40, 286);
            this.lblresult.Name = "lblresult";
            this.lblresult.Size = new System.Drawing.Size(0, 13);
            this.lblresult.TabIndex = 23;
            // 
            // btnAutoverify
            // 
            this.btnAutoverify.Location = new System.Drawing.Point(1019, 176);
            this.btnAutoverify.Name = "btnAutoverify";
            this.btnAutoverify.Size = new System.Drawing.Size(102, 25);
            this.btnAutoverify.TabIndex = 24;
            this.btnAutoverify.Text = "Auto Identify";
            this.btnAutoverify.UseVisualStyleBackColor = true;
            this.btnAutoverify.Click += new System.EventHandler(this.btnAutoverify_Click);
            // 
            // imgInfo
            // 
            this.imgInfo.Image = global::Sigesoft.Node.WinClient.UI.Resources.information;
            this.imgInfo.Location = new System.Drawing.Point(39, 284);
            this.imgInfo.Name = "imgInfo";
            this.imgInfo.Size = new System.Drawing.Size(17, 16);
            this.imgInfo.TabIndex = 41;
            this.imgInfo.TabStop = false;
            this.imgInfo.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gray;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(907, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(185, 133);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 40;
            this.pictureBox1.TabStop = false;
            // 
            // imgNO
            // 
            this.imgNO.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.imgNO.Location = new System.Drawing.Point(28, 295);
            this.imgNO.Name = "imgNO";
            this.imgNO.Size = new System.Drawing.Size(17, 16);
            this.imgNO.TabIndex = 20;
            this.imgNO.TabStop = false;
            this.imgNO.Visible = false;
            // 
            // imgOK
            // 
            this.imgOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.imgOK.Location = new System.Drawing.Point(19, 284);
            this.imgOK.Name = "imgOK";
            this.imgOK.Size = new System.Drawing.Size(17, 16);
            this.imgOK.TabIndex = 19;
            this.imgOK.TabStop = false;
            this.imgOK.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(640, 317);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 30);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Salir  ";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(567, 317);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 30);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "Guardar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // imgInfoFirma
            // 
            this.imgInfoFirma.Image = global::Sigesoft.Node.WinClient.UI.Resources.information;
            this.imgInfoFirma.Location = new System.Drawing.Point(327, 284);
            this.imgInfoFirma.Name = "imgInfoFirma";
            this.imgInfoFirma.Size = new System.Drawing.Size(17, 16);
            this.imgInfoFirma.TabIndex = 45;
            this.imgInfoFirma.TabStop = false;
            this.imgInfoFirma.Visible = false;
            // 
            // imgNoFirma
            // 
            this.imgNoFirma.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.imgNoFirma.Location = new System.Drawing.Point(328, 284);
            this.imgNoFirma.Name = "imgNoFirma";
            this.imgNoFirma.Size = new System.Drawing.Size(17, 16);
            this.imgNoFirma.TabIndex = 46;
            this.imgNoFirma.TabStop = false;
            this.imgNoFirma.Visible = false;
            // 
            // imgOkFirma
            // 
            this.imgOkFirma.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.imgOkFirma.Location = new System.Drawing.Point(327, 284);
            this.imgOkFirma.Name = "imgOkFirma";
            this.imgOkFirma.Size = new System.Drawing.Size(17, 16);
            this.imgOkFirma.TabIndex = 47;
            this.imgOkFirma.TabStop = false;
            this.imgOkFirma.Visible = false;
            // 
            // lblResultFirma
            // 
            this.lblResultFirma.AutoSize = true;
            this.lblResultFirma.Location = new System.Drawing.Point(338, 286);
            this.lblResultFirma.Name = "lblResultFirma";
            this.lblResultFirma.Size = new System.Drawing.Size(0, 13);
            this.lblResultFirma.TabIndex = 48;
            // 
            // button5
            // 
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(284, 279);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(27, 26);
            this.button5.TabIndex = 94;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(705, 279);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 26);
            this.button1.TabIndex = 95;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmCapturedFingerPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(751, 358);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.imgInfoFirma);
            this.Controls.Add(this.lblResultFirma);
            this.Controls.Add(this.imgOkFirma);
            this.Controls.Add(this.imgNoFirma);
            this.Controls.Add(this.imgInfo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAutoverify);
            this.Controls.Add(this.lblresult);
            this.Controls.Add(this.btnEnroll);
            this.Controls.Add(this.memoHint);
            this.Controls.Add(this.imgNO);
            this.Controls.Add(this.imgOK);
            this.Controls.Add(this.ZKFPEngX1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCapturedFingerPrint";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Huella y Firma";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCapturedFingerPrint_FormClosing);
            this.Load += new System.EventHandler(this.frmCapturedFingerPrint_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFirma)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.pnlPreviewTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZKFPEngX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgInfoFirma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgNoFirma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOkFirma)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pbFingerPrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelSignature;
        private System.Windows.Forms.Panel pnlPreviewTitle;
        private System.Windows.Forms.Button btnDelFingerPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private AxZKFPEngXControl.AxZKFPEngX ZKFPEngX1;
        private System.Windows.Forms.PictureBox imgNO;
        private System.Windows.Forms.PictureBox imgOK;
        private System.Windows.Forms.TextBox memoHint;
        private System.Windows.Forms.Button btnEnroll;
        private System.Windows.Forms.Label lblresult;
        private System.Windows.Forms.Button btnAutoverify;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox imgInfo;
        private Topaz.SigPlusNET sigPlusNET1;
        private System.Windows.Forms.PictureBox imgInfoFirma;
        private System.Windows.Forms.PictureBox imgNoFirma;
        private System.Windows.Forms.PictureBox imgOkFirma;
        private System.Windows.Forms.Label lblResultFirma;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pbFirma;
    }
}