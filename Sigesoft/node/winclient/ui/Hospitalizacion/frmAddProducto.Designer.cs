namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    partial class frmAddProducto
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.gbReceta = new Infragistics.Win.Misc.UltraGroupBox();
            this.labelmensaje = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtMedicamento = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtCantidad = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.lblIdDetalleProd = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gbReceta)).BeginInit();
            this.gbReceta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // gbReceta
            // 
            this.gbReceta.Controls.Add(this.labelmensaje);
            this.gbReceta.Controls.Add(this.btnBuscar);
            this.gbReceta.Controls.Add(this.txtMedicamento);
            this.gbReceta.Controls.Add(this.btnSalir);
            this.gbReceta.Controls.Add(this.btnGuardar);
            this.gbReceta.Controls.Add(this.txtCantidad);
            this.gbReceta.Controls.Add(this.ultraLabel2);
            this.gbReceta.Controls.Add(this.ultraLabel1);
            this.gbReceta.Location = new System.Drawing.Point(-6, 12);
            this.gbReceta.Name = "gbReceta";
            this.gbReceta.Size = new System.Drawing.Size(407, 173);
            this.gbReceta.TabIndex = 1;
            this.gbReceta.Text = "Buscar Medicina";
            // 
            // labelmensaje
            // 
            this.labelmensaje.AutoSize = true;
            this.labelmensaje.Location = new System.Drawing.Point(100, 16);
            this.labelmensaje.MaximumSize = new System.Drawing.Size(5, 0);
            this.labelmensaje.MinimumSize = new System.Drawing.Size(1, 0);
            this.labelmensaje.Name = "labelmensaje";
            this.labelmensaje.Size = new System.Drawing.Size(5, 13);
            this.labelmensaje.TabIndex = 10;
            this.labelmensaje.Text = ".";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(351, 45);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(28, 23);
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "::::";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtMedicamento
            // 
            this.txtMedicamento.Location = new System.Drawing.Point(103, 47);
            this.txtMedicamento.Name = "txtMedicamento";
            this.txtMedicamento.Size = new System.Drawing.Size(242, 20);
            this.txtMedicamento.TabIndex = 7;
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(242, 139);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(56, 28);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(313, 139);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 28);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtCantidad
            // 
            appearance1.TextHAlignAsString = "Right";
            this.txtCantidad.Appearance = appearance1;
            this.txtCantidad.Location = new System.Drawing.Point(103, 89);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(242, 21);
            this.txtCantidad.TabIndex = 1;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.AutoSize = true;
            this.ultraLabel2.Location = new System.Drawing.Point(22, 93);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(53, 14);
            this.ultraLabel2.TabIndex = 2;
            this.ultraLabel2.Text = "Cantidad:";
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.AutoSize = true;
            this.ultraLabel1.Location = new System.Drawing.Point(18, 50);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(75, 14);
            this.ultraLabel1.TabIndex = 0;
            this.ultraLabel1.Text = "Medicamento:";
            // 
            // lblIdDetalleProd
            // 
            this.lblIdDetalleProd.AutoSize = true;
            this.lblIdDetalleProd.Location = new System.Drawing.Point(9, 185);
            this.lblIdDetalleProd.Name = "lblIdDetalleProd";
            this.lblIdDetalleProd.Size = new System.Drawing.Size(10, 13);
            this.lblIdDetalleProd.TabIndex = 9;
            this.lblIdDetalleProd.Text = ".";
            // 
            // frmAddProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 201);
            this.Controls.Add(this.lblIdDetalleProd);
            this.Controls.Add(this.gbReceta);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddProducto";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Medicamento ";
            this.Load += new System.EventHandler(this.frmAddProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbReceta)).EndInit();
            this.gbReceta.ResumeLayout(false);
            this.gbReceta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox gbReceta;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGuardar;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtCantidad;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtMedicamento;
        private System.Windows.Forms.Label lblIdDetalleProd;
        private System.Windows.Forms.Label labelmensaje;

    }
}