namespace Sigesoft.Node.WinClient.UI.UserControls
{
    partial class frmFileUploadEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileUploadEdit));
            this.gbPreview = new System.Windows.Forms.GroupBox();
            this.pbFile = new System.Windows.Forms.PictureBox();
            this.pnlPreviewTitle = new System.Windows.Forms.Panel();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.lblExt = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.gbFileUpload = new System.Windows.Forms.GroupBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.prgFileUpload = new System.Windows.Forms.ProgressBar();
            this.lblPercent = new System.Windows.Forms.Label();
            this.gbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFile)).BeginInit();
            this.pnlPreviewTitle.SuspendLayout();
            this.gbFileUpload.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPreview
            // 
            this.gbPreview.Controls.Add(this.pbFile);
            this.gbPreview.Controls.Add(this.pnlPreviewTitle);
            this.gbPreview.Location = new System.Drawing.Point(332, 6);
            this.gbPreview.Name = "gbPreview";
            this.gbPreview.Size = new System.Drawing.Size(198, 180);
            this.gbPreview.TabIndex = 91;
            this.gbPreview.TabStop = false;
            this.gbPreview.Text = "Vista Previa";
            // 
            // pbFile
            // 
            this.pbFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFile.Location = new System.Drawing.Point(5, 35);
            this.pbFile.Name = "pbFile";
            this.pbFile.Size = new System.Drawing.Size(189, 135);
            this.pbFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFile.TabIndex = 81;
            this.pbFile.TabStop = false;
            // 
            // pnlPreviewTitle
            // 
            this.pnlPreviewTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPreviewTitle.BackColor = System.Drawing.Color.Transparent;
            this.pnlPreviewTitle.Controls.Add(this.lblFileSize);
            this.pnlPreviewTitle.Controls.Add(this.lblExt);
            this.pnlPreviewTitle.Controls.Add(this.btnDelete);
            this.pnlPreviewTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPreviewTitle.ForeColor = System.Drawing.Color.White;
            this.pnlPreviewTitle.Location = new System.Drawing.Point(2, 13);
            this.pnlPreviewTitle.Name = "pnlPreviewTitle";
            this.pnlPreviewTitle.Size = new System.Drawing.Size(195, 161);
            this.pnlPreviewTitle.TabIndex = 95;
            // 
            // lblFileSize
            // 
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Location = new System.Drawing.Point(109, 4);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(64, 13);
            this.lblFileSize.TabIndex = 89;
            this.lblFileSize.Text = "lblFileSize";
            this.lblFileSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblExt
            // 
            this.lblExt.AutoSize = true;
            this.lblExt.Location = new System.Drawing.Point(4, 4);
            this.lblExt.Name = "lblExt";
            this.lblExt.Size = new System.Drawing.Size(38, 13);
            this.lblExt.TabIndex = 87;
            this.lblExt.Text = "lblExt";
            this.lblExt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnDelete.Location = new System.Drawing.Point(174, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(20, 19);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // gbFileUpload
            // 
            this.gbFileUpload.Controls.Add(this.txtFileName);
            this.gbFileUpload.Controls.Add(this.label2);
            this.gbFileUpload.Controls.Add(this.label1);
            this.gbFileUpload.Controls.Add(this.txtDescripcion);
            this.gbFileUpload.Controls.Add(this.btnBrowser);
            this.gbFileUpload.Location = new System.Drawing.Point(3, 6);
            this.gbFileUpload.Name = "gbFileUpload";
            this.gbFileUpload.Size = new System.Drawing.Size(323, 180);
            this.gbFileUpload.TabIndex = 90;
            this.gbFileUpload.TabStop = false;
            this.gbFileUpload.Text = "Información de Archivos";
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.SystemColors.Control;
            this.txtFileName.Location = new System.Drawing.Point(58, 20);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(222, 20);
            this.txtFileName.TabIndex = 88;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "Nombre";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 85;
            this.label1.Text = "Nota";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(57, 56);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(260, 118);
            this.txtDescripcion.TabIndex = 84;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowser.Image")));
            this.btnBrowser.Location = new System.Drawing.Point(286, 14);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(32, 31);
            this.btnBrowser.TabIndex = 82;
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnAceptar);
            this.panel3.Location = new System.Drawing.Point(0, 203);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(531, 43);
            this.panel3.TabIndex = 93;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(428, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 31);
            this.btnCancel.TabIndex = 91;
            this.btnCancel.Text = "&Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(328, 6);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(98, 31);
            this.btnAceptar.TabIndex = 87;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // prgFileUpload
            // 
            this.prgFileUpload.Location = new System.Drawing.Point(327, 188);
            this.prgFileUpload.Name = "prgFileUpload";
            this.prgFileUpload.Size = new System.Drawing.Size(204, 9);
            this.prgFileUpload.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prgFileUpload.TabIndex = 82;
            this.prgFileUpload.Visible = false;
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(12, 184);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(60, 13);
            this.lblPercent.TabIndex = 94;
            this.lblPercent.Text = "[lblPercent]";
            // 
            // frmFileUploadEdit
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 245);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.prgFileUpload);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.gbPreview);
            this.Controls.Add(this.gbFileUpload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFileUploadEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar Archivos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFileUploadA_FormClosing);
            this.Load += new System.EventHandler(this.frmFileUploadA_Load);
            this.gbPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFile)).EndInit();
            this.pnlPreviewTitle.ResumeLayout(false);
            this.pnlPreviewTitle.PerformLayout();
            this.gbFileUpload.ResumeLayout(false);
            this.gbFileUpload.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPreview;
        private System.Windows.Forms.PictureBox pbFile;
        private System.Windows.Forms.GroupBox gbFileUpload;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar prgFileUpload;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel pnlPreviewTitle;
        private System.Windows.Forms.Label lblExt;
        private System.Windows.Forms.Label lblFileSize;
    }
}