﻿namespace Sigesoft.Node.Contasol.Integration
{
    partial class frmAddRecipe
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
            Infragistics.Win.UltraWinEditors.EditorButton editorButton1 = new Infragistics.Win.UltraWinEditors.EditorButton();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this.frmAddRecipe_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.gbReceta = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtMedicamento = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.txtDuracion = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtPosologia = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtCantidad = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this._frmAddRecipe_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmAddRecipe_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmAddRecipe_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmAddRecipe_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this.uvDatos = new Infragistics.Win.Misc.UltraValidator(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.frmAddRecipe_Fill_Panel.ClientArea.SuspendLayout();
            this.frmAddRecipe_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbReceta)).BeginInit();
            this.gbReceta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMedicamento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuracion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPosologia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            // 
            // frmAddRecipe_Fill_Panel
            // 
            // 
            // frmAddRecipe_Fill_Panel.ClientArea
            // 
            this.frmAddRecipe_Fill_Panel.ClientArea.Controls.Add(this.gbReceta);
            this.frmAddRecipe_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frmAddRecipe_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmAddRecipe_Fill_Panel.Location = new System.Drawing.Point(4, 27);
            this.frmAddRecipe_Fill_Panel.Name = "frmAddRecipe_Fill_Panel";
            this.frmAddRecipe_Fill_Panel.Size = new System.Drawing.Size(423, 231);
            this.frmAddRecipe_Fill_Panel.TabIndex = 0;
            // 
            // gbReceta
            // 
            this.gbReceta.Controls.Add(this.txtMedicamento);
            this.gbReceta.Controls.Add(this.btnSalir);
            this.gbReceta.Controls.Add(this.btnGuardar);
            this.gbReceta.Controls.Add(this.dtpFechaFin);
            this.gbReceta.Controls.Add(this.txtDuracion);
            this.gbReceta.Controls.Add(this.txtPosologia);
            this.gbReceta.Controls.Add(this.txtCantidad);
            this.gbReceta.Controls.Add(this.ultraLabel5);
            this.gbReceta.Controls.Add(this.ultraLabel4);
            this.gbReceta.Controls.Add(this.ultraLabel3);
            this.gbReceta.Controls.Add(this.ultraLabel2);
            this.gbReceta.Controls.Add(this.ultraLabel1);
            this.gbReceta.Location = new System.Drawing.Point(8, 6);
            this.gbReceta.Name = "gbReceta";
            this.gbReceta.Size = new System.Drawing.Size(407, 215);
            this.gbReceta.TabIndex = 0;
            this.gbReceta.Text = "Receta";
            // 
            // txtMedicamento
            // 
            this.txtMedicamento.ButtonsRight.Add(editorButton1);
            this.txtMedicamento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMedicamento.Location = new System.Drawing.Point(103, 25);
            this.txtMedicamento.Name = "txtMedicamento";
            this.txtMedicamento.ReadOnly = true;
            this.txtMedicamento.Size = new System.Drawing.Size(292, 21);
            this.txtMedicamento.TabIndex = 0;
            this.uvDatos.GetValidationSettings(this.txtMedicamento).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvDatos.GetValidationSettings(this.txtMedicamento).IsRequired = true;
            this.txtMedicamento.EditorButtonClick += new Infragistics.Win.UltraWinEditors.EditorButtonEventHandler(this.txtMedicamento_EditorButtonClick);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(258, 183);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(56, 28);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(320, 183);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 28);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(103, 145);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(110, 20);
            this.dtpFechaFin.TabIndex = 4;
            // 
            // txtDuracion
            // 
            this.txtDuracion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDuracion.Location = new System.Drawing.Point(103, 115);
            this.txtDuracion.Name = "txtDuracion";
            this.txtDuracion.Size = new System.Drawing.Size(292, 21);
            this.txtDuracion.TabIndex = 3;
            this.uvDatos.GetValidationSettings(this.txtDuracion).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvDatos.GetValidationSettings(this.txtDuracion).IsRequired = true;
            // 
            // txtPosologia
            // 
            this.txtPosologia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPosologia.Location = new System.Drawing.Point(103, 85);
            this.txtPosologia.Name = "txtPosologia";
            this.txtPosologia.Size = new System.Drawing.Size(292, 21);
            this.txtPosologia.TabIndex = 2;
            this.uvDatos.GetValidationSettings(this.txtPosologia).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvDatos.GetValidationSettings(this.txtPosologia).IsRequired = true;
            // 
            // txtCantidad
            // 
            appearance1.TextHAlignAsString = "Right";
            this.txtCantidad.Appearance = appearance1;
            this.txtCantidad.Location = new System.Drawing.Point(103, 55);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(110, 21);
            this.txtCantidad.TabIndex = 1;
            this.uvDatos.GetValidationSettings(this.txtCantidad).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvDatos.GetValidationSettings(this.txtCantidad).IsRequired = true;
            this.txtCantidad.Validating += new System.ComponentModel.CancelEventHandler(this.txtCantidad_Validating);
            // 
            // ultraLabel5
            // 
            this.ultraLabel5.AutoSize = true;
            this.ultraLabel5.Location = new System.Drawing.Point(22, 149);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(58, 14);
            this.ultraLabel5.TabIndex = 5;
            this.ultraLabel5.Text = "Fecha Fin:";
            // 
            // ultraLabel4
            // 
            this.ultraLabel4.AutoSize = true;
            this.ultraLabel4.Location = new System.Drawing.Point(22, 119);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(53, 14);
            this.ultraLabel4.TabIndex = 4;
            this.ultraLabel4.Text = "Duración:";
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.AutoSize = true;
            this.ultraLabel3.Location = new System.Drawing.Point(22, 89);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(57, 14);
            this.ultraLabel3.TabIndex = 3;
            this.ultraLabel3.Text = "Posología:";
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.AutoSize = true;
            this.ultraLabel2.Location = new System.Drawing.Point(22, 59);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(53, 14);
            this.ultraLabel2.TabIndex = 2;
            this.ultraLabel2.Text = "Cantidad:";
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.AutoSize = true;
            this.ultraLabel1.Location = new System.Drawing.Point(22, 29);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(75, 14);
            this.ultraLabel1.TabIndex = 0;
            this.ultraLabel1.Text = "Medicamento:";
            // 
            // _frmAddRecipe_UltraFormManager_Dock_Area_Left
            // 
            this._frmAddRecipe_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmAddRecipe_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 4;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
            this._frmAddRecipe_UltraFormManager_Dock_Area_Left.Name = "_frmAddRecipe_UltraFormManager_Dock_Area_Left";
            this._frmAddRecipe_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(4, 231);
            // 
            // _frmAddRecipe_UltraFormManager_Dock_Area_Right
            // 
            this._frmAddRecipe_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmAddRecipe_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 4;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(427, 27);
            this._frmAddRecipe_UltraFormManager_Dock_Area_Right.Name = "_frmAddRecipe_UltraFormManager_Dock_Area_Right";
            this._frmAddRecipe_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(4, 231);
            // 
            // _frmAddRecipe_UltraFormManager_Dock_Area_Top
            // 
            this._frmAddRecipe_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmAddRecipe_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Top.FormManager = this.ultraFormManager1;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._frmAddRecipe_UltraFormManager_Dock_Area_Top.Name = "_frmAddRecipe_UltraFormManager_Dock_Area_Top";
            this._frmAddRecipe_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(431, 27);
            // 
            // _frmAddRecipe_UltraFormManager_Dock_Area_Bottom
            // 
            this._frmAddRecipe_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmAddRecipe_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
            this._frmAddRecipe_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 258);
            this._frmAddRecipe_UltraFormManager_Dock_Area_Bottom.Name = "_frmAddRecipe_UltraFormManager_Dock_Area_Bottom";
            this._frmAddRecipe_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(431, 4);
            // 
            // frmAddRecipe
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(431, 262);
            this.Controls.Add(this.frmAddRecipe_Fill_Panel);
            this.Controls.Add(this._frmAddRecipe_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._frmAddRecipe_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._frmAddRecipe_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._frmAddRecipe_UltraFormManager_Dock_Area_Bottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddRecipe";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmAddRecipe";
            this.Load += new System.EventHandler(this.frmAddRecipe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.frmAddRecipe_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frmAddRecipe_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbReceta)).EndInit();
            this.gbReceta.ResumeLayout(false);
            this.gbReceta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMedicamento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuracion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPosologia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvDatos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.Misc.UltraPanel frmAddRecipe_Fill_Panel;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmAddRecipe_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmAddRecipe_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmAddRecipe_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmAddRecipe_UltraFormManager_Dock_Area_Bottom;
        private Infragistics.Win.Misc.UltraGroupBox gbReceta;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtDuracion;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPosologia;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtCantidad;
        private Infragistics.Win.Misc.UltraLabel ultraLabel5;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.Misc.UltraValidator uvDatos;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtMedicamento;
    }
}