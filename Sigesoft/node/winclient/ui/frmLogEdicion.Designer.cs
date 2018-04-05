namespace Sigesoft.Node.WinClient.UI
{
    partial class frmLogEdicion
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtLogId = new System.Windows.Forms.TextBox();
            this.ddlEventTypeId = new System.Windows.Forms.ComboBox();
            this.txtError = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtElementItem = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProcessEntity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtExpirationDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ddlSuccess = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.txtLogId);
            this.groupBox1.Controls.Add(this.ddlEventTypeId);
            this.groupBox1.Controls.Add(this.txtError);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtElementItem);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtProcessEntity);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtExpirationDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ddlSuccess);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(318, 327);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(76, 90);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(2);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(230, 20);
            this.txtUserName.TabIndex = 24;
            // 
            // txtLogId
            // 
            this.txtLogId.Location = new System.Drawing.Point(76, 17);
            this.txtLogId.Margin = new System.Windows.Forms.Padding(2);
            this.txtLogId.Name = "txtLogId";
            this.txtLogId.ReadOnly = true;
            this.txtLogId.Size = new System.Drawing.Size(230, 20);
            this.txtLogId.TabIndex = 23;
            // 
            // ddlEventTypeId
            // 
            this.ddlEventTypeId.Enabled = false;
            this.ddlEventTypeId.FormattingEnabled = true;
            this.ddlEventTypeId.Location = new System.Drawing.Point(76, 40);
            this.ddlEventTypeId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlEventTypeId.Name = "ddlEventTypeId";
            this.ddlEventTypeId.Size = new System.Drawing.Size(230, 21);
            this.ddlEventTypeId.TabIndex = 22;
            // 
            // txtError
            // 
            this.txtError.Location = new System.Drawing.Point(77, 180);
            this.txtError.Margin = new System.Windows.Forms.Padding(2);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(230, 136);
            this.txtError.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(38, 180);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 19);
            this.label9.TabIndex = 20;
            this.label9.Text = "Error";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtElementItem
            // 
            this.txtElementItem.Location = new System.Drawing.Point(76, 136);
            this.txtElementItem.Margin = new System.Windows.Forms.Padding(2);
            this.txtElementItem.Name = "txtElementItem";
            this.txtElementItem.ReadOnly = true;
            this.txtElementItem.Size = new System.Drawing.Size(230, 20);
            this.txtElementItem.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(38, 136);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 19);
            this.label8.TabIndex = 18;
            this.label8.Text = "Ítem";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProcessEntity
            // 
            this.txtProcessEntity.Location = new System.Drawing.Point(76, 113);
            this.txtProcessEntity.Margin = new System.Windows.Forms.Padding(2);
            this.txtProcessEntity.Name = "txtProcessEntity";
            this.txtProcessEntity.ReadOnly = true;
            this.txtProcessEntity.Size = new System.Drawing.Size(230, 20);
            this.txtProcessEntity.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(24, 90);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 19);
            this.label7.TabIndex = 15;
            this.label7.Text = "Usuario";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExpirationDate
            // 
            this.txtExpirationDate.CustomFormat = "dd/MM/yyyy";
            this.txtExpirationDate.Enabled = false;
            this.txtExpirationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtExpirationDate.Location = new System.Drawing.Point(77, 158);
            this.txtExpirationDate.Margin = new System.Windows.Forms.Padding(2);
            this.txtExpirationDate.Name = "txtExpirationDate";
            this.txtExpirationDate.Size = new System.Drawing.Size(92, 20);
            this.txtExpirationDate.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(36, 159);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "Fecha";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(26, 113);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 19);
            this.label5.TabIndex = 10;
            this.label5.Text = "Proceso";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddlSuccess
            // 
            this.ddlSuccess.Enabled = false;
            this.ddlSuccess.FormattingEnabled = true;
            this.ddlSuccess.Location = new System.Drawing.Point(76, 65);
            this.ddlSuccess.Margin = new System.Windows.Forms.Padding(2);
            this.ddlSuccess.Name = "ddlSuccess";
            this.ddlSuccess.Size = new System.Drawing.Size(230, 21);
            this.ddlSuccess.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 65);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Satisfactorio";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tipo Evento";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(57, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Id";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(259, 346);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Salir";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmLogEdicion
            // 
            this.AcceptButton = this.btnCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(361, 393);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogEdicion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ver Log ";
            this.Load += new System.EventHandler(this.frmLogEdicion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker txtExpirationDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ddlSuccess;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtElementItem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProcessEntity;
        private System.Windows.Forms.ComboBox ddlEventTypeId;
        private System.Windows.Forms.TextBox txtLogId;
        private System.Windows.Forms.TextBox txtUserName;
    }
}