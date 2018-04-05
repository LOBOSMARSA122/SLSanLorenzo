namespace Sigesoft.Node.WinClient.UI
{
    partial class frmParametersManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescriptionFilter = new System.Windows.Forms.TextBox();
            this.txtParameterIdFilter = new System.Windows.Forms.TextBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.i_GroupId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.i_ParameterId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.v_Value1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.v_CreationUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d_CreationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.v_UpdateUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d_UpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.procesarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtDescriptionFilter);
            this.groupBox1.Controls.Add(this.txtParameterIdFilter);
            this.groupBox1.Controls.Add(this.btnFilter);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(735, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Búsqueda / Filtro";
            // 
            // txtDescriptionFilter
            // 
            this.txtDescriptionFilter.Location = new System.Drawing.Point(327, 18);
            this.txtDescriptionFilter.Name = "txtDescriptionFilter";
            this.txtDescriptionFilter.Size = new System.Drawing.Size(159, 20);
            this.txtDescriptionFilter.TabIndex = 1;
            // 
            // txtParameterIdFilter
            // 
            this.txtParameterIdFilter.Location = new System.Drawing.Point(85, 20);
            this.txtParameterIdFilter.Name = "txtParameterIdFilter";
            this.txtParameterIdFilter.Size = new System.Drawing.Size(159, 20);
            this.txtParameterIdFilter.TabIndex = 0;
            // 
            // btnFilter
            // 
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.Image = global::Sigesoft.Node.WinClient.UI.Resources.find;
            this.btnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilter.Location = new System.Drawing.Point(502, 14);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(60, 24);
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "Filtrar";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(247, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Value 1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Parámetro Id";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdData);
            this.groupBox2.Location = new System.Drawing.Point(4, 93);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(735, 397);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resultados";
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.grdData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.i_GroupId,
            this.i_ParameterId,
            this.v_Value1,
            this.v_CreationUser,
            this.d_CreationDate,
            this.v_UpdateUser,
            this.d_UpdateDate});
            this.grdData.ContextMenuStrip = this.contextMenuStrip1;
            this.grdData.Location = new System.Drawing.Point(4, 17);
            this.grdData.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.MultiSelect = false;
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.RowTemplate.Height = 24;
            this.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdData.Size = new System.Drawing.Size(726, 372);
            this.grdData.TabIndex = 0;
            // 
            // i_GroupId
            // 
            this.i_GroupId.DataPropertyName = "i_GroupId";
            this.i_GroupId.HeaderText = "Grupo Id";
            this.i_GroupId.Name = "i_GroupId";
            this.i_GroupId.ReadOnly = true;
            this.i_GroupId.Width = 53;
            // 
            // i_ParameterId
            // 
            this.i_ParameterId.DataPropertyName = "i_ParameterId";
            this.i_ParameterId.HeaderText = "Parámetro Id";
            this.i_ParameterId.Name = "i_ParameterId";
            this.i_ParameterId.ReadOnly = true;
            this.i_ParameterId.Width = 81;
            // 
            // v_Value1
            // 
            this.v_Value1.DataPropertyName = "v_Value1";
            this.v_Value1.HeaderText = "Value 1";
            this.v_Value1.Name = "v_Value1";
            this.v_Value1.ReadOnly = true;
            this.v_Value1.Width = 290;
            // 
            // v_CreationUser
            // 
            this.v_CreationUser.DataPropertyName = "v_CreationUser";
            this.v_CreationUser.HeaderText = "Usuario Creación";
            this.v_CreationUser.Name = "v_CreationUser";
            this.v_CreationUser.ReadOnly = true;
            this.v_CreationUser.Width = 78;
            // 
            // d_CreationDate
            // 
            this.d_CreationDate.DataPropertyName = "d_CreationDate";
            this.d_CreationDate.HeaderText = "Fecha Creación";
            this.d_CreationDate.Name = "d_CreationDate";
            this.d_CreationDate.ReadOnly = true;
            this.d_CreationDate.Width = 146;
            // 
            // v_UpdateUser
            // 
            this.v_UpdateUser.DataPropertyName = "v_UpdateUser";
            this.v_UpdateUser.HeaderText = "Usuario Actualización";
            this.v_UpdateUser.Name = "v_UpdateUser";
            this.v_UpdateUser.ReadOnly = true;
            // 
            // d_UpdateDate
            // 
            this.d_UpdateDate.DataPropertyName = "d_UpdateDate";
            this.d_UpdateDate.HeaderText = "Fecha Actualización";
            this.d_UpdateDate.Name = "d_UpdateDate";
            this.d_UpdateDate.ReadOnly = true;
            this.d_UpdateDate.Width = 140;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.eliminarToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem3,
            this.toolStripSeparator2,
            this.procesarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(126, 126);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::Sigesoft.Node.WinClient.UI.Resources.add;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(125, 22);
            this.toolStripMenuItem1.Text = "Nuevo";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::Sigesoft.Node.WinClient.UI.Resources.pencil;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(125, 22);
            this.toolStripMenuItem2.Text = "Modificar";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::Sigesoft.Node.WinClient.UI.Resources.delete;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = global::Sigesoft.Node.WinClient.UI.Resources.arrow_refresh;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(125, 22);
            this.toolStripMenuItem3.Text = "Refrescar";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(122, 6);
            // 
            // procesarToolStripMenuItem
            // 
            this.procesarToolStripMenuItem.Image = global::Sigesoft.Node.WinClient.UI.Resources.calculator;
            this.procesarToolStripMenuItem.Name = "procesarToolStripMenuItem";
            this.procesarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.procesarToolStripMenuItem.Text = "Procesar";
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount.Location = new System.Drawing.Point(506, 78);
            this.lblRecordCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(231, 19);
            this.lblRecordCount.TabIndex = 1;
            this.lblRecordCount.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::Sigesoft.Node.WinClient.UI.Resources.add;
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(4, 59);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(103, 30);
            this.btnNuevo.TabIndex = 2;
            this.btnNuevo.Text = "Nuevo Grupo";
            this.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // frmParametersManagement
            // 
            this.AcceptButton = this.btnFilter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 492);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmParametersManagement";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administración de Parámetros";
            this.Load += new System.EventHandler(this.frmAdministracion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem procesarToolStripMenuItem;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TextBox txtParameterIdFilter;
        private System.Windows.Forms.TextBox txtDescriptionFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn i_GroupId;
        private System.Windows.Forms.DataGridViewTextBoxColumn i_ParameterId;
        private System.Windows.Forms.DataGridViewTextBoxColumn v_Value1;
        private System.Windows.Forms.DataGridViewTextBoxColumn v_CreationUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn d_CreationDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn v_UpdateUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn d_UpdateDate;
    }
}

