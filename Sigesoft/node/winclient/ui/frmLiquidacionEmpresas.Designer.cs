namespace Sigesoft.Node.WinClient.UI
{
    partial class frmLiquidacionEmpresas
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
            this.rbEstadoCuentaEmpresa = new System.Windows.Forms.RadioButton();
            this.rbCuentasXCobrar = new System.Windows.Forms.RadioButton();
            this.rbResumenCuentasXCobrar = new System.Windows.Forms.RadioButton();
            this.rbLiqPendFacturar = new System.Windows.Forms.RadioButton();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.btnGenerar);
            this.groupBox1.Controls.Add(this.rbLiqPendFacturar);
            this.groupBox1.Controls.Add(this.rbResumenCuentasXCobrar);
            this.groupBox1.Controls.Add(this.rbCuentasXCobrar);
            this.groupBox1.Controls.Add(this.rbEstadoCuentaEmpresa);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 212);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FORMATOS:";
            // 
            // rbEstadoCuentaEmpresa
            // 
            this.rbEstadoCuentaEmpresa.AutoSize = true;
            this.rbEstadoCuentaEmpresa.Location = new System.Drawing.Point(6, 39);
            this.rbEstadoCuentaEmpresa.Name = "rbEstadoCuentaEmpresa";
            this.rbEstadoCuentaEmpresa.Size = new System.Drawing.Size(168, 17);
            this.rbEstadoCuentaEmpresa.TabIndex = 108;
            this.rbEstadoCuentaEmpresa.TabStop = true;
            this.rbEstadoCuentaEmpresa.Text = "Estado de cuenta de Empresa";
            this.rbEstadoCuentaEmpresa.UseVisualStyleBackColor = true;
            this.rbEstadoCuentaEmpresa.CheckedChanged += new System.EventHandler(this.rbEstadoCuentaEmpresa_CheckedChanged);
            // 
            // rbCuentasXCobrar
            // 
            this.rbCuentasXCobrar.AutoSize = true;
            this.rbCuentasXCobrar.Location = new System.Drawing.Point(6, 76);
            this.rbCuentasXCobrar.Name = "rbCuentasXCobrar";
            this.rbCuentasXCobrar.Size = new System.Drawing.Size(116, 17);
            this.rbCuentasXCobrar.TabIndex = 109;
            this.rbCuentasXCobrar.TabStop = true;
            this.rbCuentasXCobrar.Text = "Cuentas por Cobrar";
            this.rbCuentasXCobrar.UseVisualStyleBackColor = true;
            this.rbCuentasXCobrar.CheckedChanged += new System.EventHandler(this.rbCuentasXCobrar_CheckedChanged);
            // 
            // rbResumenCuentasXCobrar
            // 
            this.rbResumenCuentasXCobrar.AutoSize = true;
            this.rbResumenCuentasXCobrar.Location = new System.Drawing.Point(6, 112);
            this.rbResumenCuentasXCobrar.Name = "rbResumenCuentasXCobrar";
            this.rbResumenCuentasXCobrar.Size = new System.Drawing.Size(164, 17);
            this.rbResumenCuentasXCobrar.TabIndex = 110;
            this.rbResumenCuentasXCobrar.TabStop = true;
            this.rbResumenCuentasXCobrar.Text = "Resumen Cuentas por Cobrar";
            this.rbResumenCuentasXCobrar.UseVisualStyleBackColor = true;
            this.rbResumenCuentasXCobrar.CheckedChanged += new System.EventHandler(this.rbResumenCuentasXCobrar_CheckedChanged);
            // 
            // rbLiqPendFacturar
            // 
            this.rbLiqPendFacturar.AutoSize = true;
            this.rbLiqPendFacturar.Location = new System.Drawing.Point(6, 145);
            this.rbLiqPendFacturar.Name = "rbLiqPendFacturar";
            this.rbLiqPendFacturar.Size = new System.Drawing.Size(203, 17);
            this.rbLiqPendFacturar.TabIndex = 111;
            this.rbLiqPendFacturar.TabStop = true;
            this.rbLiqPendFacturar.Text = "Liquidaciones Pendientes de Facturar";
            this.rbLiqPendFacturar.UseVisualStyleBackColor = true;
            this.rbLiqPendFacturar.CheckedChanged += new System.EventHandler(this.rbLiqPendFacturar_CheckedChanged);
            // 
            // btnGenerar
            // 
            this.btnGenerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerar.Enabled = false;
            this.btnGenerar.Location = new System.Drawing.Point(347, 183);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(75, 23);
            this.btnGenerar.TabIndex = 112;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(246, 183);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 113;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Sigesoft.Node.WinClient.UI.Resources.repor_4;
            this.pictureBox1.Location = new System.Drawing.Point(313, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 107;
            this.pictureBox1.TabStop = false;
            // 
            // frmLiquidacionEmpresas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 236);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLiquidacionEmpresas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REPORTES EMPRESAS";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton rbEstadoCuentaEmpresa;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.RadioButton rbLiqPendFacturar;
        private System.Windows.Forms.RadioButton rbResumenCuentasXCobrar;
        private System.Windows.Forms.RadioButton rbCuentasXCobrar;
        private System.Windows.Forms.Button btnCancelar;

    }
}