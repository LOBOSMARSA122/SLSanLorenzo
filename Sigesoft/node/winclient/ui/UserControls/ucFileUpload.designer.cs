namespace Sigesoft.Node.WinClient.UI.UserControls
{
    partial class ucFileUpload
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFileUpload));
            this.gbImage = new System.Windows.Forms.GroupBox();
            this.pbProductImage = new System.Windows.Forms.PictureBox();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThumbnailFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MultimediaFileId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceComponentMultimediaId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbFileList = new System.Windows.Forms.GroupBox();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.btnDescargar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.gbImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProductImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.gbFileList.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbImage
            // 
            this.gbImage.Controls.Add(this.pbProductImage);
            this.gbImage.Location = new System.Drawing.Point(438, 3);
            this.gbImage.Name = "gbImage";
            this.gbImage.Size = new System.Drawing.Size(200, 179);
            this.gbImage.TabIndex = 50;
            this.gbImage.TabStop = false;
            this.gbImage.Text = "Imagen";
            // 
            // pbProductImage
            // 
            this.pbProductImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProductImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbProductImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbProductImage.Image = ((System.Drawing.Image)(resources.GetObject("pbProductImage.Image")));
            this.pbProductImage.Location = new System.Drawing.Point(6, 15);
            this.pbProductImage.Name = "pbProductImage";
            this.pbProductImage.Size = new System.Drawing.Size(185, 154);
            this.pbProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbProductImage.TabIndex = 81;
            this.pbProductImage.TabStop = false;
            this.pbProductImage.Click += new System.EventHandler(this.pbProductImage_Click);
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.AllowUserToOrderColumns = true;
            this.dgvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.FileName,
            this.Description,
            this.ThumbnailFile,
            this.MultimediaFileId,
            this.ServiceComponentMultimediaId});
            this.dgvFiles.Location = new System.Drawing.Point(6, 23);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.ReadOnly = true;
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFiles.Size = new System.Drawing.Size(312, 124);
            this.dgvFiles.TabIndex = 84;
            this.dgvFiles.SelectionChanged += new System.EventHandler(this.dgvFiles_SelectionChanged);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 38;
            // 
            // FileName
            // 
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "Archivo";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 170;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Nota";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 150;
            // 
            // ThumbnailFile
            // 
            this.ThumbnailFile.DataPropertyName = "ThumbnailFile";
            this.ThumbnailFile.HeaderText = "Array";
            this.ThumbnailFile.Name = "ThumbnailFile";
            this.ThumbnailFile.ReadOnly = true;
            this.ThumbnailFile.Visible = false;
            this.ThumbnailFile.Width = 300;
            // 
            // MultimediaFileId
            // 
            this.MultimediaFileId.DataPropertyName = "MultimediaFileId";
            this.MultimediaFileId.HeaderText = "MultimediaFileId";
            this.MultimediaFileId.Name = "MultimediaFileId";
            this.MultimediaFileId.ReadOnly = true;
            this.MultimediaFileId.Visible = false;
            // 
            // ServiceComponentMultimediaId
            // 
            this.ServiceComponentMultimediaId.DataPropertyName = "ServiceComponentMultimediaId";
            this.ServiceComponentMultimediaId.HeaderText = "ServiceComponentMultimediaId";
            this.ServiceComponentMultimediaId.Name = "ServiceComponentMultimediaId";
            this.ServiceComponentMultimediaId.ReadOnly = true;
            this.ServiceComponentMultimediaId.Visible = false;
            // 
            // gbFileList
            // 
            this.gbFileList.Controls.Add(this.lblRecordCount);
            this.gbFileList.Controls.Add(this.btnDescargar);
            this.gbFileList.Controls.Add(this.btnEliminar);
            this.gbFileList.Controls.Add(this.dgvFiles);
            this.gbFileList.Controls.Add(this.btnNuevo);
            this.gbFileList.Controls.Add(this.btnModificar);
            this.gbFileList.Location = new System.Drawing.Point(3, 3);
            this.gbFileList.Name = "gbFileList";
            this.gbFileList.Size = new System.Drawing.Size(429, 179);
            this.gbFileList.TabIndex = 85;
            this.gbFileList.TabStop = false;
            this.gbFileList.Text = "Lista de Archivos Agregados";
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRecordCount.ForeColor = System.Drawing.Color.Black;
            this.lblRecordCount.Location = new System.Drawing.Point(6, 152);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(312, 17);
            this.lblRecordCount.TabIndex = 87;
            this.lblRecordCount.Text = "{0} Archivo(s) agregado(s)";
            // 
            // btnDescargar
            // 
            this.btnDescargar.Enabled = false;
            this.btnDescargar.Image = ((System.Drawing.Image)(resources.GetObject("btnDescargar.Image")));
            this.btnDescargar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDescargar.Location = new System.Drawing.Point(324, 116);
            this.btnDescargar.Name = "btnDescargar";
            this.btnDescargar.Size = new System.Drawing.Size(98, 31);
            this.btnDescargar.TabIndex = 91;
            this.btnDescargar.Text = "&Descargar";
            this.btnDescargar.UseVisualStyleBackColor = true;
            this.btnDescargar.Click += new System.EventHandler(this.btnDescargar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Enabled = false;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(324, 85);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(98, 31);
            this.btnEliminar.TabIndex = 90;
            this.btnEliminar.Text = "&Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(324, 23);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(98, 31);
            this.btnNuevo.TabIndex = 88;
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Enabled = false;
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.Location = new System.Drawing.Point(324, 54);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(98, 31);
            this.btnModificar.TabIndex = 89;
            this.btnModificar.Text = "&Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ucFileUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFileList);
            this.Controls.Add(this.gbImage);
            this.Name = "ucFileUpload";
            this.Size = new System.Drawing.Size(647, 223);
            this.Load += new System.EventHandler(this.ucFileUpload_Load);
            this.gbImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbProductImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.gbFileList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbImage;
        private System.Windows.Forms.PictureBox pbProductImage;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.GroupBox gbFileList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnDescargar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThumbnailFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn MultimediaFileId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceComponentMultimediaId;
    }
}
