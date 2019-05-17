namespace Sigesoft.Node.WinClient.UI.Reports
{
    partial class frmManagementReports_Async
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbGenerarPublicar = new System.Windows.Forms.RadioButton();
            this.rbConAdjuntos = new System.Windows.Forms.RadioButton();
            this.rbSinAdjuntos = new System.Windows.Forms.RadioButton();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.btnConsolidadoReportes = new System.Windows.Forms.Button();
            this.chklConsolidadoReportes = new System.Windows.Forms.CheckedListBox();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.chkTodos);
            this.groupBox4.Controls.Add(this.btnConsolidadoReportes);
            this.groupBox4.Controls.Add(this.chklConsolidadoReportes);
            this.groupBox4.Location = new System.Drawing.Point(12, 10);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(413, 414);
            this.groupBox4.TabIndex = 139;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Reportes Seleccionados";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbGenerarPublicar);
            this.groupBox1.Controls.Add(this.rbConAdjuntos);
            this.groupBox1.Controls.Add(this.rbSinAdjuntos);
            this.groupBox1.Location = new System.Drawing.Point(16, 325);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 53);
            this.groupBox1.TabIndex = 138;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo";
            // 
            // rbGenerarPublicar
            // 
            this.rbGenerarPublicar.AutoSize = true;
            this.rbGenerarPublicar.Location = new System.Drawing.Point(210, 19);
            this.rbGenerarPublicar.Name = "rbGenerarPublicar";
            this.rbGenerarPublicar.Size = new System.Drawing.Size(163, 17);
            this.rbGenerarPublicar.TabIndex = 2;
            this.rbGenerarPublicar.Text = "Generar y publicar en la Web";
            this.rbGenerarPublicar.UseVisualStyleBackColor = true;
            // 
            // rbConAdjuntos
            // 
            this.rbConAdjuntos.AutoSize = true;
            this.rbConAdjuntos.Location = new System.Drawing.Point(106, 19);
            this.rbConAdjuntos.Name = "rbConAdjuntos";
            this.rbConAdjuntos.Size = new System.Drawing.Size(88, 17);
            this.rbConAdjuntos.TabIndex = 1;
            this.rbConAdjuntos.Text = "Con Adjuntos";
            this.rbConAdjuntos.UseVisualStyleBackColor = true;
            // 
            // rbSinAdjuntos
            // 
            this.rbSinAdjuntos.AutoSize = true;
            this.rbSinAdjuntos.Checked = true;
            this.rbSinAdjuntos.Location = new System.Drawing.Point(7, 20);
            this.rbSinAdjuntos.Name = "rbSinAdjuntos";
            this.rbSinAdjuntos.Size = new System.Drawing.Size(84, 17);
            this.rbSinAdjuntos.TabIndex = 0;
            this.rbSinAdjuntos.TabStop = true;
            this.rbSinAdjuntos.Text = "Sin Adjuntos";
            this.rbSinAdjuntos.UseVisualStyleBackColor = true;
            // 
            // chkTodos
            // 
            this.chkTodos.AutoSize = true;
            this.chkTodos.Location = new System.Drawing.Point(6, 19);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(115, 17);
            this.chkTodos.TabIndex = 137;
            this.chkTodos.Text = "Seleccionar Todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // btnConsolidadoReportes
            // 
            this.btnConsolidadoReportes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConsolidadoReportes.BackColor = System.Drawing.SystemColors.Control;
            this.btnConsolidadoReportes.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnConsolidadoReportes.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnConsolidadoReportes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnConsolidadoReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsolidadoReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsolidadoReportes.ForeColor = System.Drawing.Color.Black;
            this.btnConsolidadoReportes.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_white_acrobat;
            this.btnConsolidadoReportes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsolidadoReportes.Location = new System.Drawing.Point(293, 383);
            this.btnConsolidadoReportes.Margin = new System.Windows.Forms.Padding(2);
            this.btnConsolidadoReportes.Name = "btnConsolidadoReportes";
            this.btnConsolidadoReportes.Size = new System.Drawing.Size(102, 25);
            this.btnConsolidadoReportes.TabIndex = 136;
            this.btnConsolidadoReportes.Text = "&Generar PDF";
            this.btnConsolidadoReportes.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnConsolidadoReportes.UseVisualStyleBackColor = false;
            this.btnConsolidadoReportes.Click += new System.EventHandler(this.btnConsolidadoReportes_Click);
            // 
            // chklConsolidadoReportes
            // 
            this.chklConsolidadoReportes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chklConsolidadoReportes.CheckOnClick = true;
            this.chklConsolidadoReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklConsolidadoReportes.FormattingEnabled = true;
            this.chklConsolidadoReportes.Location = new System.Drawing.Point(16, 39);
            this.chklConsolidadoReportes.Name = "chklConsolidadoReportes";
            this.chklConsolidadoReportes.Size = new System.Drawing.Size(379, 274);
            this.chklConsolidadoReportes.TabIndex = 25;
            // 
            // frmManagementReports_Async
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 436);
            this.Controls.Add(this.groupBox4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManagementReports_Async";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administrador de Reportes";
            this.Load += new System.EventHandler(this.frmManagementReports_Async_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkTodos;
        private System.Windows.Forms.Button btnConsolidadoReportes;
        private System.Windows.Forms.CheckedListBox chklConsolidadoReportes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbGenerarPublicar;
        private System.Windows.Forms.RadioButton rbConAdjuntos;
        private System.Windows.Forms.RadioButton rbSinAdjuntos;
    }
}