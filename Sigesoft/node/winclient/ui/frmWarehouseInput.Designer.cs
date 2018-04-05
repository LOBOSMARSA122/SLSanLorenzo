namespace Sigesoft.Node.WinClient.UI
{
    partial class frmWarehouseInput
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProductId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProductName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CategoryName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Brand");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_SerialNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_Quantity");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_Price");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Model");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnSupplierSearch = new System.Windows.Forms.Button();
            this.txtIsProcessed = new System.Windows.Forms.TextBox();
            this.txtDocReference = new System.Windows.Forms.TextBox();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.ddlMotiveId = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnProduct = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ddlProductId = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtProductSearch = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdData = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.contextMenuGrdData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSaveRefresh = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDiscardProcess = new System.Windows.Forms.Button();
            this.uvAddItem = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.uvWarehouseInput = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.btnConfirmProcess = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.contextMenuGrdData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uvAddItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvWarehouseInput)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.btnSupplierSearch);
            this.groupBox1.Controls.Add(this.txtIsProcessed);
            this.groupBox1.Controls.Add(this.txtDocReference);
            this.groupBox1.Controls.Add(this.txtSupplier);
            this.groupBox1.Controls.Add(this.ddlMotiveId);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(970, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información de Nota Ingreso";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(590, 31);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(102, 20);
            this.dtpDate.TabIndex = 3;
            // 
            // btnSupplierSearch
            // 
            this.btnSupplierSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplierSearch.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_search;
            this.btnSupplierSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSupplierSearch.Location = new System.Drawing.Point(503, 22);
            this.btnSupplierSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSupplierSearch.Name = "btnSupplierSearch";
            this.btnSupplierSearch.Size = new System.Drawing.Size(22, 24);
            this.btnSupplierSearch.TabIndex = 2;
            this.btnSupplierSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSupplierSearch.UseVisualStyleBackColor = true;
            this.btnSupplierSearch.Click += new System.EventHandler(this.btnSupplierSearch_Click);
            // 
            // txtIsProcessed
            // 
            this.txtIsProcessed.Location = new System.Drawing.Point(933, 28);
            this.txtIsProcessed.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtIsProcessed.Name = "txtIsProcessed";
            this.txtIsProcessed.ReadOnly = true;
            this.txtIsProcessed.Size = new System.Drawing.Size(30, 20);
            this.txtIsProcessed.TabIndex = 9;
            this.uvWarehouseInput.GetValidationSettings(this.txtIsProcessed).DataType = typeof(string);
            this.uvWarehouseInput.GetValidationSettings(this.txtIsProcessed).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvWarehouseInput.GetValidationSettings(this.txtIsProcessed).IsRequired = true;
            // 
            // txtDocReference
            // 
            this.txtDocReference.Location = new System.Drawing.Point(791, 28);
            this.txtDocReference.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDocReference.MaxLength = 20;
            this.txtDocReference.Name = "txtDocReference";
            this.txtDocReference.Size = new System.Drawing.Size(70, 20);
            this.txtDocReference.TabIndex = 4;
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(280, 27);
            this.txtSupplier.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.ReadOnly = true;
            this.txtSupplier.Size = new System.Drawing.Size(219, 20);
            this.txtSupplier.TabIndex = 6;
            this.uvWarehouseInput.GetValidationSettings(this.txtSupplier).DataType = typeof(string);
            this.uvWarehouseInput.GetValidationSettings(this.txtSupplier).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvWarehouseInput.GetValidationSettings(this.txtSupplier).IsRequired = true;
            // 
            // ddlMotiveId
            // 
            this.ddlMotiveId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMotiveId.DropDownWidth = 320;
            this.ddlMotiveId.FormattingEnabled = true;
            this.ddlMotiveId.Location = new System.Drawing.Point(47, 27);
            this.ddlMotiveId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ddlMotiveId.Name = "ddlMotiveId";
            this.ddlMotiveId.Size = new System.Drawing.Size(158, 21);
            this.ddlMotiveId.TabIndex = 1;
            this.uvWarehouseInput.GetValidationSettings(this.ddlMotiveId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvWarehouseInput.GetValidationSettings(this.ddlMotiveId).DataType = typeof(string);
            this.uvWarehouseInput.GetValidationSettings(this.ddlMotiveId).IsRequired = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(872, 31);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Procesado";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(704, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Doc. Referencia";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Proveedor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(550, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Motivo";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnProduct);
            this.groupBox2.Controls.Add(this.btnAddItem);
            this.groupBox2.Controls.Add(this.txtPrice);
            this.groupBox2.Controls.Add(this.txtQuantity);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.ddlProductId);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtProductSearch);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(10, 80);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(970, 54);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Búsqueda de Productos / Artículos";
            // 
            // btnProduct
            // 
            this.btnProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProduct.Image = global::Sigesoft.Node.WinClient.UI.Resources.application_form;
            this.btnProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProduct.Location = new System.Drawing.Point(874, 14);
            this.btnProduct.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Size = new System.Drawing.Size(88, 24);
            this.btnProduct.TabIndex = 15;
            this.btnProduct.Text = "Producto";
            this.btnProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProduct.UseVisualStyleBackColor = true;
            this.btnProduct.Click += new System.EventHandler(this.btnProduct_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.Image = global::Sigesoft.Node.WinClient.UI.Resources.add;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddItem.Location = new System.Drawing.Point(776, 14);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(84, 24);
            this.btnAddItem.TabIndex = 9;
            this.btnAddItem.Text = "Agregar";
            this.btnAddItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(725, 17);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPrice.MaxLength = 10;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(48, 20);
            this.txtPrice.TabIndex = 8;
            this.uvAddItem.GetValidationSettings(this.txtPrice).DataType = typeof(string);
            this.uvAddItem.GetValidationSettings(this.txtPrice).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvAddItem.GetValidationSettings(this.txtPrice).IsRequired = true;
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(618, 17);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtQuantity.MaxLength = 10;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(53, 20);
            this.txtQuantity.TabIndex = 7;
            this.uvAddItem.GetValidationSettings(this.txtQuantity).DataType = typeof(string);
            this.uvAddItem.GetValidationSettings(this.txtQuantity).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvAddItem.GetValidationSettings(this.txtQuantity).IsRequired = true;
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(678, 21);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Precio";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(566, 21);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Cantidad";
            // 
            // ddlProductId
            // 
            this.ddlProductId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProductId.DropDownWidth = 700;
            this.ddlProductId.FormattingEnabled = true;
            this.ddlProductId.Location = new System.Drawing.Point(280, 15);
            this.ddlProductId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ddlProductId.Name = "ddlProductId";
            this.ddlProductId.Size = new System.Drawing.Size(275, 21);
            this.ddlProductId.TabIndex = 6;
            this.uvAddItem.GetValidationSettings(this.ddlProductId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvAddItem.GetValidationSettings(this.ddlProductId).DataType = typeof(string);
            this.uvAddItem.GetValidationSettings(this.ddlProductId).IsRequired = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(227, 21);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Producto";
            // 
            // txtProductSearch
            // 
            this.txtProductSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProductSearch.Location = new System.Drawing.Point(50, 19);
            this.txtProductSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtProductSearch.MaxLength = 100;
            this.txtProductSearch.Name = "txtProductSearch";
            this.txtProductSearch.Size = new System.Drawing.Size(156, 20);
            this.txtProductSearch.TabIndex = 5;
            this.txtProductSearch.TextChanged += new System.EventHandler(this.txtProductSearch_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Buscar";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.grdData);
            this.groupBox3.Location = new System.Drawing.Point(10, 160);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(970, 322);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Productos a Ingresar";
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.CausesValidation = false;
            this.grdData.ContextMenuStrip = this.contextMenuGrdData;
            appearance1.BackColor = System.Drawing.SystemColors.ControlLight;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdData.DisplayLayout.Appearance = appearance1;
            ultraGridColumn2.Header.Caption = "Id Producto";
            ultraGridColumn2.Header.VisiblePosition = 0;
            ultraGridColumn2.Width = 139;
            ultraGridColumn1.Header.Caption = "Producto";
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Width = 276;
            ultraGridColumn8.Header.Caption = "Categoría";
            ultraGridColumn8.Header.VisiblePosition = 5;
            ultraGridColumn8.Width = 310;
            ultraGridColumn12.Header.Caption = "Marca";
            ultraGridColumn12.Header.VisiblePosition = 2;
            ultraGridColumn13.Header.Caption = "Nro Serie";
            ultraGridColumn13.Header.VisiblePosition = 4;
            ultraGridColumn16.Header.Caption = "Cantidad";
            ultraGridColumn16.Header.VisiblePosition = 6;
            ultraGridColumn17.Header.Caption = "Precio";
            ultraGridColumn17.Header.VisiblePosition = 7;
            ultraGridColumn3.Header.Caption = "Modelo";
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn2,
            ultraGridColumn1,
            ultraGridColumn8,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn3});
            this.grdData.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdData.DisplayLayout.InterBandSpacing = 10;
            this.grdData.DisplayLayout.MaxColScrollRegions = 1;
            this.grdData.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdData.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdData.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdData.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdData.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdData.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdData.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdData.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdData.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdData.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.SystemColors.Control;
            appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdData.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdData.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            appearance5.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdData.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdData.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdData.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.FontData.BoldAsString = "True";
            this.grdData.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdData.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdData.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdData.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdData.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdData.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdData.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdData.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdData.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdData.Location = new System.Drawing.Point(4, 16);
            this.grdData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(962, 298);
            this.grdData.TabIndex = 43;
            this.grdData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdData_MouseDown);
            // 
            // contextMenuGrdData
            // 
            this.contextMenuGrdData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removerToolStripMenuItem});
            this.contextMenuGrdData.Name = "contextMenuGrdData";
            this.contextMenuGrdData.Size = new System.Drawing.Size(122, 26);
            // 
            // removerToolStripMenuItem
            // 
            this.removerToolStripMenuItem.Image = global::Sigesoft.Node.WinClient.UI.Resources.delete;
            this.removerToolStripMenuItem.Name = "removerToolStripMenuItem";
            this.removerToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.removerToolStripMenuItem.Text = "Remover";
            this.removerToolStripMenuItem.Click += new System.EventHandler(this.removerToolStripMenuItem_Click);
            // 
            // btnSaveRefresh
            // 
            this.btnSaveRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRefresh.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnSaveRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveRefresh.Location = new System.Drawing.Point(463, 485);
            this.btnSaveRefresh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSaveRefresh.Name = "btnSaveRefresh";
            this.btnSaveRefresh.Size = new System.Drawing.Size(130, 30);
            this.btnSaveRefresh.TabIndex = 17;
            this.btnSaveRefresh.Text = "Guardar sin Procesar";
            this.btnSaveRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveRefresh.UseVisualStyleBackColor = true;
            this.btnSaveRefresh.Click += new System.EventHandler(this.btnSaveRefresh_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(910, 485);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 30);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cerrar";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDiscardProcess
            // 
            this.btnDiscardProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiscardProcess.Enabled = false;
            this.btnDiscardProcess.Image = global::Sigesoft.Node.WinClient.UI.Resources.server_delete;
            this.btnDiscardProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDiscardProcess.Location = new System.Drawing.Point(778, 485);
            this.btnDiscardProcess.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDiscardProcess.Name = "btnDiscardProcess";
            this.btnDiscardProcess.Size = new System.Drawing.Size(127, 30);
            this.btnDiscardProcess.TabIndex = 15;
            this.btnDiscardProcess.Text = "Descartar Movimiento";
            this.btnDiscardProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDiscardProcess.UseVisualStyleBackColor = true;
            this.btnDiscardProcess.Click += new System.EventHandler(this.btnDiscardProcess_Click);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount.Location = new System.Drawing.Point(749, 140);
            this.lblRecordCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(231, 19);
            this.lblRecordCount.TabIndex = 44;
            this.lblRecordCount.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirmProcess
            // 
            this.btnConfirmProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmProcess.Image = global::Sigesoft.Node.WinClient.UI.Resources.server_add;
            this.btnConfirmProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirmProcess.Location = new System.Drawing.Point(597, 485);
            this.btnConfirmProcess.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnConfirmProcess.Name = "btnConfirmProcess";
            this.btnConfirmProcess.Size = new System.Drawing.Size(177, 30);
            this.btnConfirmProcess.TabIndex = 45;
            this.btnConfirmProcess.Text = "Guardar y Procesar Movimiento";
            this.btnConfirmProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfirmProcess.UseVisualStyleBackColor = true;
            this.btnConfirmProcess.Click += new System.EventHandler(this.btnConfirmProcess_Click);
            // 
            // frmWarehouseInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 553);
            this.Controls.Add(this.btnConfirmProcess);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.btnSaveRefresh);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDiscardProcess);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmWarehouseInput";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmWarehouseInput_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.contextMenuGrdData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uvAddItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvWarehouseInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIsProcessed;
        private System.Windows.Forms.TextBox txtDocReference;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.ComboBox ddlMotiveId;
        private System.Windows.Forms.Button btnSupplierSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox ddlProductId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtProductSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdData;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDiscardProcess;
        private System.Windows.Forms.Button btnSaveRefresh;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private Infragistics.Win.Misc.UltraValidator uvAddItem;
        private Infragistics.Win.Misc.UltraValidator uvWarehouseInput;
        private System.Windows.Forms.ContextMenuStrip contextMenuGrdData;
        private System.Windows.Forms.ToolStripMenuItem removerToolStripMenuItem;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.Button btnConfirmProcess;
        private System.Windows.Forms.Button btnProduct;
    }
}