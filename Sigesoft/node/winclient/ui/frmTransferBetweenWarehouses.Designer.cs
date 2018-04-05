namespace Sigesoft.Node.WinClient.UI
{
    partial class frmTransferBetweenWarehouses
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Model");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddlOrganizationLocationSourceId = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ddlWarehouseSourceId = new System.Windows.Forms.ComboBox();
            this.lblNodeSource = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ddlNodeDestinationId = new System.Windows.Forms.ComboBox();
            this.rbRemote = new System.Windows.Forms.RadioButton();
            this.rbLocal = new System.Windows.Forms.RadioButton();
            this.ddlOrganizationLocationDestinationId = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ddlWarehouseDestinationId = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtIsProcessed = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDocReference = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ddlProductId = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtProductSearch = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.grdData = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.contextMenuGrdData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConfirmProcess = new System.Windows.Forms.Button();
            this.btnSaveRefresh = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDiscardProcess = new System.Windows.Forms.Button();
            this.uvTransferWarehouse = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.uvAddItem = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.contextMenuGrdData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uvTransferWarehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvAddItem)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ddlOrganizationLocationSourceId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ddlWarehouseSourceId);
            this.groupBox1.Controls.Add(this.lblNodeSource);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 17);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(410, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Almacén de Origen";
            // 
            // ddlOrganizationLocationSourceId
            // 
            this.ddlOrganizationLocationSourceId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlOrganizationLocationSourceId.DropDownWidth = 400;
            this.ddlOrganizationLocationSourceId.FormattingEnabled = true;
            this.ddlOrganizationLocationSourceId.Location = new System.Drawing.Point(68, 46);
            this.ddlOrganizationLocationSourceId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ddlOrganizationLocationSourceId.Name = "ddlOrganizationLocationSourceId";
            this.ddlOrganizationLocationSourceId.Size = new System.Drawing.Size(332, 21);
            this.ddlOrganizationLocationSourceId.TabIndex = 29;
            this.uvTransferWarehouse.GetValidationSettings(this.ddlOrganizationLocationSourceId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvTransferWarehouse.GetValidationSettings(this.ddlOrganizationLocationSourceId).IsRequired = true;
            this.ddlOrganizationLocationSourceId.SelectedIndexChanged += new System.EventHandler(this.ddlOrganizationLocationSourceId_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 32);
            this.label3.TabIndex = 27;
            this.label3.Text = "Empresa / Sede";
            // 
            // ddlWarehouseSourceId
            // 
            this.ddlWarehouseSourceId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlWarehouseSourceId.DropDownWidth = 400;
            this.ddlWarehouseSourceId.FormattingEnabled = true;
            this.ddlWarehouseSourceId.Location = new System.Drawing.Point(69, 79);
            this.ddlWarehouseSourceId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ddlWarehouseSourceId.Name = "ddlWarehouseSourceId";
            this.ddlWarehouseSourceId.Size = new System.Drawing.Size(331, 21);
            this.ddlWarehouseSourceId.TabIndex = 30;
            this.uvTransferWarehouse.GetValidationSettings(this.ddlWarehouseSourceId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvTransferWarehouse.GetValidationSettings(this.ddlWarehouseSourceId).IsRequired = true;
            this.ddlWarehouseSourceId.SelectedIndexChanged += new System.EventHandler(this.ddlWarehouseSourceId_SelectedIndexChanged);
            // 
            // lblNodeSource
            // 
            this.lblNodeSource.BackColor = System.Drawing.SystemColors.Info;
            this.lblNodeSource.Location = new System.Drawing.Point(67, 21);
            this.lblNodeSource.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNodeSource.Name = "lblNodeSource";
            this.lblNodeSource.Size = new System.Drawing.Size(332, 14);
            this.lblNodeSource.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(7, 81);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 17);
            this.label7.TabIndex = 28;
            this.label7.Text = "Almacén";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nodo";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ddlNodeDestinationId);
            this.groupBox2.Controls.Add(this.rbRemote);
            this.groupBox2.Controls.Add(this.rbLocal);
            this.groupBox2.Controls.Add(this.ddlOrganizationLocationDestinationId);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.ddlWarehouseDestinationId);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(491, 17);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(411, 125);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Almacén de Destino";
            // 
            // ddlNodeDestinationId
            // 
            this.ddlNodeDestinationId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlNodeDestinationId.DropDownWidth = 400;
            this.ddlNodeDestinationId.FormattingEnabled = true;
            this.ddlNodeDestinationId.Location = new System.Drawing.Point(68, 40);
            this.ddlNodeDestinationId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ddlNodeDestinationId.Name = "ddlNodeDestinationId";
            this.ddlNodeDestinationId.Size = new System.Drawing.Size(332, 21);
            this.ddlNodeDestinationId.TabIndex = 33;
            this.uvTransferWarehouse.GetValidationSettings(this.ddlNodeDestinationId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvTransferWarehouse.GetValidationSettings(this.ddlNodeDestinationId).IsRequired = true;
            this.ddlNodeDestinationId.SelectedIndexChanged += new System.EventHandler(this.ddlNodeDestinationId_SelectedIndexChanged);
            // 
            // rbRemote
            // 
            this.rbRemote.AutoSize = true;
            this.rbRemote.Location = new System.Drawing.Point(127, 18);
            this.rbRemote.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbRemote.Name = "rbRemote";
            this.rbRemote.Size = new System.Drawing.Size(62, 17);
            this.rbRemote.TabIndex = 32;
            this.rbRemote.Text = "Remoto";
            this.rbRemote.UseVisualStyleBackColor = true;
            this.rbRemote.CheckedChanged += new System.EventHandler(this.rbRemote_CheckedChanged);
            // 
            // rbLocal
            // 
            this.rbLocal.AutoSize = true;
            this.rbLocal.Checked = true;
            this.rbLocal.Location = new System.Drawing.Point(67, 18);
            this.rbLocal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbLocal.Name = "rbLocal";
            this.rbLocal.Size = new System.Drawing.Size(51, 17);
            this.rbLocal.TabIndex = 31;
            this.rbLocal.TabStop = true;
            this.rbLocal.Text = "Local";
            this.rbLocal.UseVisualStyleBackColor = true;
            this.rbLocal.CheckedChanged += new System.EventHandler(this.rbLocal_CheckedChanged);
            // 
            // ddlOrganizationLocationDestinationId
            // 
            this.ddlOrganizationLocationDestinationId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlOrganizationLocationDestinationId.DropDownWidth = 400;
            this.ddlOrganizationLocationDestinationId.FormattingEnabled = true;
            this.ddlOrganizationLocationDestinationId.Location = new System.Drawing.Point(68, 68);
            this.ddlOrganizationLocationDestinationId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ddlOrganizationLocationDestinationId.Name = "ddlOrganizationLocationDestinationId";
            this.ddlOrganizationLocationDestinationId.Size = new System.Drawing.Size(332, 21);
            this.ddlOrganizationLocationDestinationId.TabIndex = 29;
            this.uvAddItem.GetValidationSettings(this.ddlOrganizationLocationDestinationId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvAddItem.GetValidationSettings(this.ddlOrganizationLocationDestinationId).IsRequired = true;
            this.uvTransferWarehouse.GetValidationSettings(this.ddlOrganizationLocationDestinationId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvTransferWarehouse.GetValidationSettings(this.ddlOrganizationLocationDestinationId).IsRequired = true;
            this.ddlOrganizationLocationDestinationId.SelectedIndexChanged += new System.EventHandler(this.ddlOrganizationLocationDestinationId_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 68);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 32);
            this.label4.TabIndex = 27;
            this.label4.Text = "Empresa / Sede";
            // 
            // ddlWarehouseDestinationId
            // 
            this.ddlWarehouseDestinationId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlWarehouseDestinationId.DropDownWidth = 400;
            this.ddlWarehouseDestinationId.FormattingEnabled = true;
            this.ddlWarehouseDestinationId.Location = new System.Drawing.Point(69, 101);
            this.ddlWarehouseDestinationId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ddlWarehouseDestinationId.Name = "ddlWarehouseDestinationId";
            this.ddlWarehouseDestinationId.Size = new System.Drawing.Size(331, 21);
            this.ddlWarehouseDestinationId.TabIndex = 30;
            this.uvAddItem.GetValidationSettings(this.ddlWarehouseDestinationId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvAddItem.GetValidationSettings(this.ddlWarehouseDestinationId).IsRequired = true;
            this.uvTransferWarehouse.GetValidationSettings(this.ddlWarehouseDestinationId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvTransferWarehouse.GetValidationSettings(this.ddlWarehouseDestinationId).IsRequired = true;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 103);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 17);
            this.label6.TabIndex = 28;
            this.label6.Text = "Almacén";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 43);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Nodo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 147);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Fecha";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(73, 143);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(102, 20);
            this.dtpDate.TabIndex = 32;
            // 
            // txtIsProcessed
            // 
            this.txtIsProcessed.Location = new System.Drawing.Point(260, 143);
            this.txtIsProcessed.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtIsProcessed.Name = "txtIsProcessed";
            this.txtIsProcessed.ReadOnly = true;
            this.txtIsProcessed.Size = new System.Drawing.Size(30, 20);
            this.txtIsProcessed.TabIndex = 34;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(199, 147);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Procesado";
            // 
            // txtDocReference
            // 
            this.txtDocReference.Location = new System.Drawing.Point(408, 143);
            this.txtDocReference.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDocReference.MaxLength = 20;
            this.txtDocReference.Name = "txtDocReference";
            this.txtDocReference.Size = new System.Drawing.Size(80, 20);
            this.txtDocReference.TabIndex = 36;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(321, 147);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "Doc. Referencia";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAddItem);
            this.groupBox3.Controls.Add(this.txtQuantity);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.ddlProductId);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtProductSearch);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(9, 187);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(913, 54);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Búsqueda de Productos / Artículos";
            // 
            // btnAddItem
            // 
            this.btnAddItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.Image = global::Sigesoft.Node.WinClient.UI.Resources.add;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddItem.Location = new System.Drawing.Point(834, 14);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(68, 24);
            this.btnAddItem.TabIndex = 9;
            this.btnAddItem.Text = "Agregar";
            this.btnAddItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(777, 20);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtQuantity.MaxLength = 10;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(54, 20);
            this.txtQuantity.TabIndex = 7;
            this.uvAddItem.GetValidationSettings(this.txtQuantity).DataType = typeof(uint);
            this.uvAddItem.GetValidationSettings(this.txtQuantity).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvAddItem.GetValidationSettings(this.txtQuantity).IsRequired = true;
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(724, 24);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Cantidad";
            // 
            // ddlProductId
            // 
            this.ddlProductId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProductId.DropDownWidth = 700;
            this.ddlProductId.FormattingEnabled = true;
            this.ddlProductId.Location = new System.Drawing.Point(323, 19);
            this.ddlProductId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ddlProductId.Name = "ddlProductId";
            this.ddlProductId.Size = new System.Drawing.Size(398, 21);
            this.ddlProductId.TabIndex = 6;
            this.uvAddItem.GetValidationSettings(this.ddlProductId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvAddItem.GetValidationSettings(this.ddlProductId).IsRequired = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(270, 24);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Producto";
            // 
            // txtProductSearch
            // 
            this.txtProductSearch.Location = new System.Drawing.Point(50, 20);
            this.txtProductSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtProductSearch.MaxLength = 100;
            this.txtProductSearch.Name = "txtProductSearch";
            this.txtProductSearch.Size = new System.Drawing.Size(207, 20);
            this.txtProductSearch.TabIndex = 5;
            this.txtProductSearch.TextChanged += new System.EventHandler(this.txtProductSearch_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 24);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "Buscar";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.txtDocReference);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.txtIsProcessed);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.dtpDate);
            this.groupBox4.Location = new System.Drawing.Point(9, 10);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Size = new System.Drawing.Size(913, 172);
            this.groupBox4.TabIndex = 38;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Información de Transferencia";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.grdData);
            this.groupBox5.Location = new System.Drawing.Point(9, 259);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox5.Size = new System.Drawing.Size(913, 193);
            this.groupBox5.TabIndex = 39;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Productos a Transferir";
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
            ultraGridColumn1.Width = 417;
            ultraGridColumn8.Header.Caption = "Categoría";
            ultraGridColumn8.Header.VisiblePosition = 5;
            ultraGridColumn8.Width = 278;
            ultraGridColumn12.Header.Caption = "Marca";
            ultraGridColumn12.Header.VisiblePosition = 2;
            ultraGridColumn13.Header.Caption = "Nro Serie";
            ultraGridColumn13.Header.VisiblePosition = 4;
            ultraGridColumn16.Header.Caption = "Cantidad";
            ultraGridColumn16.Header.VisiblePosition = 6;
            ultraGridColumn3.Header.Caption = "Modelo";
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn2,
            ultraGridColumn1,
            ultraGridColumn8,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn16,
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
            this.grdData.Size = new System.Drawing.Size(904, 172);
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
            // btnConfirmProcess
            // 
            this.btnConfirmProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmProcess.Image = global::Sigesoft.Node.WinClient.UI.Resources.server_add;
            this.btnConfirmProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirmProcess.Location = new System.Drawing.Point(538, 457);
            this.btnConfirmProcess.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnConfirmProcess.Name = "btnConfirmProcess";
            this.btnConfirmProcess.Size = new System.Drawing.Size(177, 30);
            this.btnConfirmProcess.TabIndex = 53;
            this.btnConfirmProcess.Text = "Guardar y Procesar Movimiento";
            this.btnConfirmProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfirmProcess.UseVisualStyleBackColor = true;
            this.btnConfirmProcess.Click += new System.EventHandler(this.btnConfirmProcess_Click);
            // 
            // btnSaveRefresh
            // 
            this.btnSaveRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRefresh.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnSaveRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveRefresh.Location = new System.Drawing.Point(404, 457);
            this.btnSaveRefresh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSaveRefresh.Name = "btnSaveRefresh";
            this.btnSaveRefresh.Size = new System.Drawing.Size(130, 30);
            this.btnSaveRefresh.TabIndex = 52;
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
            this.btnCancel.Location = new System.Drawing.Point(851, 457);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 30);
            this.btnCancel.TabIndex = 51;
            this.btnCancel.Text = "Cerrar";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDiscardProcess
            // 
            this.btnDiscardProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiscardProcess.Image = global::Sigesoft.Node.WinClient.UI.Resources.server_delete;
            this.btnDiscardProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDiscardProcess.Location = new System.Drawing.Point(720, 457);
            this.btnDiscardProcess.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDiscardProcess.Name = "btnDiscardProcess";
            this.btnDiscardProcess.Size = new System.Drawing.Size(127, 30);
            this.btnDiscardProcess.TabIndex = 50;
            this.btnDiscardProcess.Text = "Descartar Movimiento";
            this.btnDiscardProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDiscardProcess.UseVisualStyleBackColor = true;
            this.btnDiscardProcess.Click += new System.EventHandler(this.btnDiscardProcess_Click);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount.Location = new System.Drawing.Point(691, 242);
            this.lblRecordCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(231, 19);
            this.lblRecordCount.TabIndex = 55;
            this.lblRecordCount.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmTransferBetweenWarehouses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 520);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.btnConfirmProcess);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnSaveRefresh);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDiscardProcess);
            this.Controls.Add(this.groupBox4);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimizeBox = false;
            this.Name = "frmTransferBetweenWarehouses";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transferencia entre Almacenes";
            this.Load += new System.EventHandler(this.frmTransferBetweenWarehouses_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.contextMenuGrdData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uvTransferWarehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvAddItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNodeSource;
        private System.Windows.Forms.ComboBox ddlOrganizationLocationSourceId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ddlWarehouseSourceId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox ddlOrganizationLocationDestinationId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ddlWarehouseDestinationId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbRemote;
        private System.Windows.Forms.RadioButton rbLocal;
        private System.Windows.Forms.ComboBox ddlNodeDestinationId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtIsProcessed;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDocReference;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ddlProductId;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtProductSearch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdData;
        private System.Windows.Forms.Button btnConfirmProcess;
        private System.Windows.Forms.Button btnSaveRefresh;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDiscardProcess;
        private Infragistics.Win.Misc.UltraValidator uvTransferWarehouse;
        private System.Windows.Forms.ContextMenuStrip contextMenuGrdData;
        private System.Windows.Forms.ToolStripMenuItem removerToolStripMenuItem;
        private Infragistics.Win.Misc.UltraValidator uvAddItem;
        private System.Windows.Forms.Label lblRecordCount;
    }
}