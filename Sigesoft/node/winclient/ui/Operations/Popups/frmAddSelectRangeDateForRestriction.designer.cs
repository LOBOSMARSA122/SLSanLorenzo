namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    partial class frmAddSelectRangeDateForRestriction
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
            this.rbSinFecha = new System.Windows.Forms.RadioButton();
            this.rbFechaDuracion = new System.Windows.Forms.RadioButton();
            this.dtpFechaFinRestricciones = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaIniRestricciones = new System.Windows.Forms.DateTimePicker();
            this.label41 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbSinFecha
            // 
            this.rbSinFecha.AutoSize = true;
            this.rbSinFecha.Checked = true;
            this.rbSinFecha.Location = new System.Drawing.Point(7, 19);
            this.rbSinFecha.Name = "rbSinFecha";
            this.rbSinFecha.Size = new System.Drawing.Size(73, 17);
            this.rbSinFecha.TabIndex = 67;
            this.rbSinFecha.TabStop = true;
            this.rbSinFecha.Text = "Sin Fecha";
            this.rbSinFecha.UseVisualStyleBackColor = true;
            this.rbSinFecha.CheckedChanged += new System.EventHandler(this.rbSinFecha_CheckedChanged);
            // 
            // rbFechaDuracion
            // 
            this.rbFechaDuracion.AutoSize = true;
            this.rbFechaDuracion.Location = new System.Drawing.Point(7, 41);
            this.rbFechaDuracion.Name = "rbFechaDuracion";
            this.rbFechaDuracion.Size = new System.Drawing.Size(116, 17);
            this.rbFechaDuracion.TabIndex = 66;
            this.rbFechaDuracion.Text = "Fecha de Duración";
            this.rbFechaDuracion.UseVisualStyleBackColor = true;
            this.rbFechaDuracion.CheckedChanged += new System.EventHandler(this.rbFechaDuracion_CheckedChanged);
            // 
            // dtpFechaFinRestricciones
            // 
            this.dtpFechaFinRestricciones.Enabled = false;
            this.dtpFechaFinRestricciones.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFinRestricciones.Location = new System.Drawing.Point(265, 39);
            this.dtpFechaFinRestricciones.Name = "dtpFechaFinRestricciones";
            this.dtpFechaFinRestricciones.Size = new System.Drawing.Size(97, 20);
            this.dtpFechaFinRestricciones.TabIndex = 65;
            this.dtpFechaFinRestricciones.Validating += new System.ComponentModel.CancelEventHandler(this.dtpFechaFinRestricciones_Validating);
            // 
            // dtpFechaIniRestricciones
            // 
            this.dtpFechaIniRestricciones.Enabled = false;
            this.dtpFechaIniRestricciones.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIniRestricciones.Location = new System.Drawing.Point(127, 39);
            this.dtpFechaIniRestricciones.Name = "dtpFechaIniRestricciones";
            this.dtpFechaIniRestricciones.Size = new System.Drawing.Size(98, 20);
            this.dtpFechaIniRestricciones.TabIndex = 64;
            this.dtpFechaIniRestricciones.Validating += new System.ComponentModel.CancelEventHandler(this.dtpFechaIniRestricciones_Validating);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(230, 43);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(35, 13);
            this.label41.TabIndex = 63;
            this.label41.Text = "Hasta";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaIniRestricciones);
            this.groupBox1.Controls.Add(this.rbSinFecha);
            this.groupBox1.Controls.Add(this.rbFechaDuracion);
            this.groupBox1.Controls.Add(this.label41);
            this.groupBox1.Controls.Add(this.dtpFechaFinRestricciones);
            this.groupBox1.Location = new System.Drawing.Point(4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 77);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Duración de Restricción";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(299, 83);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 70;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::Sigesoft.Node.WinClient.UI.Resources.accept;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(215, 83);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.TabIndex = 69;
            this.btnOK.Text = "Aceptar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmAddSelectRangeDateForRestriction
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 113);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddSelectRangeDateForRestriction";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Seleccione un tiempo para la restricción";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbSinFecha;
        private System.Windows.Forms.RadioButton rbFechaDuracion;
        private System.Windows.Forms.DateTimePicker dtpFechaFinRestricciones;
        private System.Windows.Forms.DateTimePicker dtpFechaIniRestricciones;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}