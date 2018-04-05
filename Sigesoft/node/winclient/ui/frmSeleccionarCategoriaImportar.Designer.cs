namespace Sigesoft.Node.WinClient.UI
{
    partial class frmSeleccionarCategoriaImportar
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
            this.components = new System.ComponentModel.Container();
            this.ddlConsultorio = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnImportarExcel = new System.Windows.Forms.Button();
            this.uvImportacion = new Infragistics.Win.Misc.UltraValidator(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uvImportacion)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlConsultorio
            // 
            this.ddlConsultorio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlConsultorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlConsultorio.FormattingEnabled = true;
            this.ddlConsultorio.Location = new System.Drawing.Point(79, 21);
            this.ddlConsultorio.Margin = new System.Windows.Forms.Padding(2);
            this.ddlConsultorio.Name = "ddlConsultorio";
            this.ddlConsultorio.Size = new System.Drawing.Size(337, 21);
            this.ddlConsultorio.TabIndex = 110;
            this.uvImportacion.GetValidationSettings(this.ddlConsultorio).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvImportacion.GetValidationSettings(this.ddlConsultorio).IsRequired = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(16, 24);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 109;
            this.label11.Text = "Consultorio";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnImportarExcel
            // 
            this.btnImportarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportarExcel.BackColor = System.Drawing.SystemColors.Control;
            this.btnImportarExcel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnImportarExcel.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnImportarExcel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnImportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportarExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportarExcel.ForeColor = System.Drawing.Color.Black;
            this.btnImportarExcel.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_excel;
            this.btnImportarExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportarExcel.Location = new System.Drawing.Point(290, 56);
            this.btnImportarExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportarExcel.Name = "btnImportarExcel";
            this.btnImportarExcel.Size = new System.Drawing.Size(126, 26);
            this.btnImportarExcel.TabIndex = 141;
            this.btnImportarExcel.Text = "&Seleccionar Excel";
            this.btnImportarExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImportarExcel.UseVisualStyleBackColor = false;
            this.btnImportarExcel.Click += new System.EventHandler(this.btnImportarExcel_Click);
            // 
            // frmSeleccionarCategoriaImportar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(431, 93);
            this.Controls.Add(this.btnImportarExcel);
            this.Controls.Add(this.ddlConsultorio);
            this.Controls.Add(this.label11);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSeleccionarCategoriaImportar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pantalla de Importación";
            this.Load += new System.EventHandler(this.frmSeleccionarCategoriaImportar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uvImportacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlConsultorio;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnImportarExcel;
        private Infragistics.Win.Misc.UltraValidator uvImportacion;
    }
}