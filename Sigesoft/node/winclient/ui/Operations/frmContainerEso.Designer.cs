namespace Sigesoft.Node.WinClient.UI.Operations
{
    partial class frmContainerEso
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
            this.tcContEso = new System.Windows.Forms.TabControl();
            this.ddlExamenesAnterioes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tcContEso
            // 
            this.tcContEso.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcContEso.Location = new System.Drawing.Point(9, 36);
            this.tcContEso.Name = "tcContEso";
            this.tcContEso.SelectedIndex = 0;
            this.tcContEso.Size = new System.Drawing.Size(1325, 650);
            this.tcContEso.TabIndex = 2;
            // 
            // ddlExamenesAnterioes
            // 
            this.ddlExamenesAnterioes.FormattingEnabled = true;
            this.ddlExamenesAnterioes.Location = new System.Drawing.Point(131, 10);
            this.ddlExamenesAnterioes.Name = "ddlExamenesAnterioes";
            this.ddlExamenesAnterioes.Size = new System.Drawing.Size(261, 21);
            this.ddlExamenesAnterioes.TabIndex = 3;
            this.ddlExamenesAnterioes.SelectedIndexChanged += new System.EventHandler(this.ddlExamenesAnterioes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Exámenes anterioes";
            // 
            // frmContainerEso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 705);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddlExamenesAnterioes);
            this.Controls.Add(this.tcContEso);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmContainerEso";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exámenes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmContainerEso_FormClosing);
            this.Load += new System.EventHandler(this.frmContainerEso_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcContEso;
        private System.Windows.Forms.ComboBox ddlExamenesAnterioes;
        private System.Windows.Forms.Label label1;
    }
}