namespace Sigesoft.Node.WinClient.UI.Configuration
{
    partial class FrmMaintenance
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProtocolId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Protocol");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Organization");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_GroupOccupation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_MasterServiceName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_EsoType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_IntermediaryOrganization");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_OrganizationInvoice");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("select", 0, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNameExternalUser = new System.Windows.Forms.Label();
            this.cboExternalUser = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPassword2 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtPassword1 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSaveExternalUser = new System.Windows.Forms.Button();
            this.txtDocNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.ddlDocType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFirstLastName = new System.Windows.Forms.TextBox();
            this.txtSecondLastName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbFEchaExpiracion = new System.Windows.Forms.RadioButton();
            this.rbNuncaCaduca = new System.Windows.Forms.RadioButton();
            this.dtpExpiredDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chklNotificaciones = new System.Windows.Forms.CheckedListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chklPermisosOpciones = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdData = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnUpdateProtocolSystemUser = new System.Windows.Forms.Button();
            this.btnAddExternalUser = new System.Windows.Forms.Button();
            this.bgwSendEmail = new System.ComponentModel.BackgroundWorker();
            this.uvPacient = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.chkEmpresas = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvPacient)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuarios";
            // 
            // lblNameExternalUser
            // 
            this.lblNameExternalUser.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblNameExternalUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameExternalUser.ForeColor = System.Drawing.Color.Black;
            this.lblNameExternalUser.Location = new System.Drawing.Point(249, 20);
            this.lblNameExternalUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNameExternalUser.Name = "lblNameExternalUser";
            this.lblNameExternalUser.Size = new System.Drawing.Size(315, 20);
            this.lblNameExternalUser.TabIndex = 108;
            this.lblNameExternalUser.Text = "Nombres y Apellidos del Profesional";
            // 
            // cboExternalUser
            // 
            this.cboExternalUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExternalUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboExternalUser.FormattingEnabled = true;
            this.cboExternalUser.Location = new System.Drawing.Point(72, 20);
            this.cboExternalUser.Margin = new System.Windows.Forms.Padding(2);
            this.cboExternalUser.Name = "cboExternalUser";
            this.cboExternalUser.Size = new System.Drawing.Size(173, 21);
            this.cboExternalUser.TabIndex = 109;
            this.cboExternalUser.SelectedIndexChanged += new System.EventHandler(this.cboExternalUser_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNameExternalUser);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboExternalUser);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(659, 58);
            this.groupBox1.TabIndex = 110;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.groupBox7);
            this.groupBox2.Controls.Add(this.txtMail);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtDocNumber);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.ddlDocType);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtFirstLastName);
            this.groupBox2.Controls.Add(this.txtSecondLastName);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(432, 564);
            this.groupBox2.TabIndex = 111;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos de Usuario Externo";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chkEmpresas);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.btnSaveExternalUser);
            this.groupBox7.Controls.Add(this.txtPassword2);
            this.groupBox7.Controls.Add(this.label25);
            this.groupBox7.Controls.Add(this.txtPassword1);
            this.groupBox7.Controls.Add(this.label26);
            this.groupBox7.Controls.Add(this.txtUserName);
            this.groupBox7.Controls.Add(this.label24);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.ForeColor = System.Drawing.Color.Red;
            this.groupBox7.Location = new System.Drawing.Point(8, 119);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(412, 439);
            this.groupBox7.TabIndex = 31;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Usuario";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(6, 65);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 13);
            this.label17.TabIndex = 25;
            this.label17.Text = "Empresa";
            // 
            // txtPassword2
            // 
            this.txtPassword2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword2.Location = new System.Drawing.Point(295, 38);
            this.txtPassword2.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword2.MaxLength = 50;
            this.txtPassword2.Name = "txtPassword2";
            this.txtPassword2.Size = new System.Drawing.Size(108, 20);
            this.txtPassword2.TabIndex = 23;
            this.txtPassword2.UseSystemPasswordChar = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(202, 41);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(90, 13);
            this.label25.TabIndex = 22;
            this.label25.Text = "Repetir Password";
            // 
            // txtPassword1
            // 
            this.txtPassword1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword1.Location = new System.Drawing.Point(64, 38);
            this.txtPassword1.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword1.MaxLength = 50;
            this.txtPassword1.Name = "txtPassword1";
            this.txtPassword1.Size = new System.Drawing.Size(112, 20);
            this.txtPassword1.TabIndex = 21;
            this.txtPassword1.UseSystemPasswordChar = true;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(6, 41);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 13);
            this.label26.TabIndex = 20;
            this.label26.Text = "Password";
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(64, 14);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(2);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(339, 20);
            this.txtUserName.TabIndex = 19;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(6, 17);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(43, 13);
            this.label24.TabIndex = 18;
            this.label24.Text = "Usuario";
            // 
            // txtMail
            // 
            this.txtMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMail.Location = new System.Drawing.Point(58, 99);
            this.txtMail.Margin = new System.Windows.Forms.Padding(2);
            this.txtMail.MaxLength = 50;
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(369, 20);
            this.txtMail.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(6, 102);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Email";
            // 
            // btnSaveExternalUser
            // 
            this.btnSaveExternalUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSaveExternalUser.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnSaveExternalUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveExternalUser.Location = new System.Drawing.Point(217, 411);
            this.btnSaveExternalUser.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveExternalUser.Name = "btnSaveExternalUser";
            this.btnSaveExternalUser.Size = new System.Drawing.Size(186, 23);
            this.btnSaveExternalUser.TabIndex = 28;
            this.btnSaveExternalUser.Text = "Guardar Datos de Usuario Externo";
            this.btnSaveExternalUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveExternalUser.UseVisualStyleBackColor = true;
            this.btnSaveExternalUser.Click += new System.EventHandler(this.btnSaveExternalUser_Click);
            // 
            // txtDocNumber
            // 
            this.txtDocNumber.Enabled = false;
            this.txtDocNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocNumber.Location = new System.Drawing.Point(304, 51);
            this.txtDocNumber.Margin = new System.Windows.Forms.Padding(2);
            this.txtDocNumber.MaxLength = 20;
            this.txtDocNumber.Name = "txtDocNumber";
            this.txtDocNumber.Size = new System.Drawing.Size(124, 20);
            this.txtDocNumber.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(5, 29);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Nombres";
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(58, 26);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(142, 20);
            this.txtName.TabIndex = 16;
            // 
            // ddlDocType
            // 
            this.ddlDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDocType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDocType.FormattingEnabled = true;
            this.ddlDocType.Location = new System.Drawing.Point(303, 26);
            this.ddlDocType.Margin = new System.Windows.Forms.Padding(2);
            this.ddlDocType.Name = "ddlDocType";
            this.ddlDocType.Size = new System.Drawing.Size(124, 21);
            this.ddlDocType.TabIndex = 21;
            this.ddlDocType.SelectedIndexChanged += new System.EventHandler(this.ddlDocType_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(213, 54);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Núm Documento";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Ape. Pat.";
            // 
            // txtFirstLastName
            // 
            this.txtFirstLastName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFirstLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstLastName.Location = new System.Drawing.Point(58, 51);
            this.txtFirstLastName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFirstLastName.MaxLength = 50;
            this.txtFirstLastName.Name = "txtFirstLastName";
            this.txtFirstLastName.Size = new System.Drawing.Size(142, 20);
            this.txtFirstLastName.TabIndex = 17;
            // 
            // txtSecondLastName
            // 
            this.txtSecondLastName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSecondLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSecondLastName.Location = new System.Drawing.Point(58, 75);
            this.txtSecondLastName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSecondLastName.MaxLength = 50;
            this.txtSecondLastName.Name = "txtSecondLastName";
            this.txtSecondLastName.Size = new System.Drawing.Size(142, 20);
            this.txtSecondLastName.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(213, 29);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Tipo Documento";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 78);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Ape. Mat.";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbFEchaExpiracion);
            this.groupBox6.Controls.Add(this.rbNuncaCaduca);
            this.groupBox6.Controls.Add(this.dtpExpiredDate);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox6.Location = new System.Drawing.Point(6, 488);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(422, 49);
            this.groupBox6.TabIndex = 25;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Expiración de cuenta";
            // 
            // rbFEchaExpiracion
            // 
            this.rbFEchaExpiracion.AutoSize = true;
            this.rbFEchaExpiracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFEchaExpiracion.ForeColor = System.Drawing.Color.Black;
            this.rbFEchaExpiracion.Location = new System.Drawing.Point(118, 18);
            this.rbFEchaExpiracion.Name = "rbFEchaExpiracion";
            this.rbFEchaExpiracion.Size = new System.Drawing.Size(122, 17);
            this.rbFEchaExpiracion.TabIndex = 20;
            this.rbFEchaExpiracion.Text = "Fecha de Expiración";
            this.rbFEchaExpiracion.UseVisualStyleBackColor = true;
            // 
            // rbNuncaCaduca
            // 
            this.rbNuncaCaduca.AutoSize = true;
            this.rbNuncaCaduca.Checked = true;
            this.rbNuncaCaduca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNuncaCaduca.ForeColor = System.Drawing.Color.Black;
            this.rbNuncaCaduca.Location = new System.Drawing.Point(5, 18);
            this.rbNuncaCaduca.Name = "rbNuncaCaduca";
            this.rbNuncaCaduca.Size = new System.Drawing.Size(89, 17);
            this.rbNuncaCaduca.TabIndex = 19;
            this.rbNuncaCaduca.TabStop = true;
            this.rbNuncaCaduca.Text = "Nunca Expira";
            this.rbNuncaCaduca.UseVisualStyleBackColor = true;
            // 
            // dtpExpiredDate
            // 
            this.dtpExpiredDate.Enabled = false;
            this.dtpExpiredDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpExpiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiredDate.Location = new System.Drawing.Point(314, 15);
            this.dtpExpiredDate.Name = "dtpExpiredDate";
            this.dtpExpiredDate.Size = new System.Drawing.Size(98, 20);
            this.dtpExpiredDate.TabIndex = 21;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chklNotificaciones);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox4.Location = new System.Drawing.Point(432, 368);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 115);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Notificaciones";
            // 
            // chklNotificaciones
            // 
            this.chklNotificaciones.CheckOnClick = true;
            this.chklNotificaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklNotificaciones.FormattingEnabled = true;
            this.chklNotificaciones.Location = new System.Drawing.Point(11, 23);
            this.chklNotificaciones.Name = "chklNotificaciones";
            this.chklNotificaciones.Size = new System.Drawing.Size(243, 79);
            this.chklNotificaciones.TabIndex = 23;
            this.chklNotificaciones.SelectedValueChanged += new System.EventHandler(this.chklNotificaciones_SelectedValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chklPermisosOpciones);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox5.Location = new System.Drawing.Point(6, 360);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(420, 123);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Permisos / Opciones";
            // 
            // chklPermisosOpciones
            // 
            this.chklPermisosOpciones.CheckOnClick = true;
            this.chklPermisosOpciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklPermisosOpciones.FormattingEnabled = true;
            this.chklPermisosOpciones.Location = new System.Drawing.Point(10, 19);
            this.chklPermisosOpciones.Name = "chklPermisosOpciones";
            this.chklPermisosOpciones.Size = new System.Drawing.Size(404, 94);
            this.chklPermisosOpciones.TabIndex = 22;
            this.chklPermisosOpciones.SelectedValueChanged += new System.EventHandler(this.chklPermisosOpciones_SelectedValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.grdData);
            this.groupBox3.Controls.Add(this.btnUpdateProtocolSystemUser);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Location = new System.Drawing.Point(450, 76);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(698, 564);
            this.groupBox3.TabIndex = 112;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Protocolos";
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdData.DisplayLayout.Appearance = appearance1;
            ultraGridColumn7.Header.Caption = "ProtocolId";
            ultraGridColumn7.Header.VisiblePosition = 8;
            ultraGridColumn7.Width = 108;
            ultraGridColumn20.Header.Caption = "Protocolo";
            ultraGridColumn20.Header.VisiblePosition = 1;
            ultraGridColumn20.Width = 304;
            ultraGridColumn21.Header.Caption = "Emp. Empleadora (Contratista)";
            ultraGridColumn21.Header.VisiblePosition = 7;
            ultraGridColumn21.Width = 240;
            ultraGridColumn26.Header.Caption = "GESO";
            ultraGridColumn26.Header.VisiblePosition = 3;
            ultraGridColumn27.Header.Caption = "Servicio";
            ultraGridColumn27.Header.VisiblePosition = 4;
            ultraGridColumn13.Header.Caption = "Tipo Eso";
            ultraGridColumn13.Header.VisiblePosition = 5;
            ultraGridColumn14.Header.Caption = "Emp. de Trabajo / Sede";
            ultraGridColumn14.Header.VisiblePosition = 6;
            ultraGridColumn14.Width = 222;
            ultraGridColumn28.Header.Caption = "Emp. Cliente / Sede";
            ultraGridColumn28.Header.VisiblePosition = 2;
            ultraGridColumn28.Width = 255;
            ultraGridColumn1.DataType = typeof(bool);
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn7,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn28,
            ultraGridColumn1});
            appearance2.FontData.SizeInPoints = 8F;
            ultraGridBand1.Header.Appearance = appearance2;
            this.grdData.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdData.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.grdData.DisplayLayout.DefaultSelectedBackColor = System.Drawing.SystemColors.Desktop;
            this.grdData.DisplayLayout.DefaultSelectedForeColor = System.Drawing.SystemColors.Control;
            this.grdData.DisplayLayout.InterBandSpacing = 10;
            this.grdData.DisplayLayout.MaxColScrollRegions = 1;
            this.grdData.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdData.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdData.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdData.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdData.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdData.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdData.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdData.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.Color.Transparent;
            this.grdData.DisplayLayout.Override.CardAreaAppearance = appearance3;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.White;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdData.DisplayLayout.Override.CellAppearance = appearance4;
            this.grdData.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdData.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            this.grdData.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            appearance5.BackColor = System.Drawing.Color.White;
            appearance5.BackColor2 = System.Drawing.Color.LightGray;
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance5.BorderAlpha = Infragistics.Win.Alpha.Opaque;
            appearance5.BorderColor = System.Drawing.Color.DarkGray;
            appearance5.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdData.DisplayLayout.Override.HeaderAppearance = appearance5;
            this.grdData.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance6.AlphaLevel = ((short)(187));
            appearance6.BackColor = System.Drawing.Color.Gainsboro;
            appearance6.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance6.ForeColor = System.Drawing.Color.Black;
            appearance6.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdData.DisplayLayout.Override.RowAlternateAppearance = appearance6;
            appearance7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdData.DisplayLayout.Override.RowSelectorAppearance = appearance7;
            this.grdData.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance8.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance8.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance8.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance8.FontData.BoldAsString = "False";
            appearance8.ForeColor = System.Drawing.Color.Black;
            this.grdData.DisplayLayout.Override.SelectedRowAppearance = appearance8;
            this.grdData.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdData.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdData.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdData.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdData.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdData.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdData.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdData.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdData.Location = new System.Drawing.Point(5, 14);
            this.grdData.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(688, 341);
            this.grdData.TabIndex = 114;
            this.grdData.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.grdData_ClickCell);
            // 
            // btnUpdateProtocolSystemUser
            // 
            this.btnUpdateProtocolSystemUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateProtocolSystemUser.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_save;
            this.btnUpdateProtocolSystemUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateProtocolSystemUser.Location = new System.Drawing.Point(486, 507);
            this.btnUpdateProtocolSystemUser.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateProtocolSystemUser.Name = "btnUpdateProtocolSystemUser";
            this.btnUpdateProtocolSystemUser.Size = new System.Drawing.Size(207, 30);
            this.btnUpdateProtocolSystemUser.TabIndex = 113;
            this.btnUpdateProtocolSystemUser.Text = "Grabar Configuración de Protocolos";
            this.btnUpdateProtocolSystemUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdateProtocolSystemUser.UseVisualStyleBackColor = true;
            this.btnUpdateProtocolSystemUser.Click += new System.EventHandler(this.btnUpdateProtocolSystemUser_Click);
            // 
            // btnAddExternalUser
            // 
            this.btnAddExternalUser.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.add;
            this.btnAddExternalUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddExternalUser.Location = new System.Drawing.Point(676, 47);
            this.btnAddExternalUser.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddExternalUser.Name = "btnAddExternalUser";
            this.btnAddExternalUser.Size = new System.Drawing.Size(142, 23);
            this.btnAddExternalUser.TabIndex = 31;
            this.btnAddExternalUser.Text = "Nuevo Usuario Externo";
            this.btnAddExternalUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddExternalUser.UseVisualStyleBackColor = true;
            this.btnAddExternalUser.Click += new System.EventHandler(this.btnAddExternalUser_Click);
            // 
            // bgwSendEmail
            // 
            this.bgwSendEmail.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSendEmail_DoWork);
            this.bgwSendEmail.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwSendEmail_ProgressChanged);
            this.bgwSendEmail.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwSendEmail_RunWorkerCompleted);
            // 
            // chkEmpresas
            // 
            this.chkEmpresas.CheckOnClick = true;
            this.chkEmpresas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEmpresas.FormattingEnabled = true;
            this.chkEmpresas.Location = new System.Drawing.Point(9, 90);
            this.chkEmpresas.Name = "chkEmpresas";
            this.chkEmpresas.Size = new System.Drawing.Size(394, 319);
            this.chkEmpresas.TabIndex = 32;
            // 
            // FrmMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 652);
            this.Controls.Add(this.btnAddExternalUser);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmMaintenance";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Usuarios Externos";
            this.Load += new System.EventHandler(this.FrmMaintenance_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvPacient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNameExternalUser;
        private System.Windows.Forms.ComboBox cboExternalUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnUpdateProtocolSystemUser;
        private System.Windows.Forms.TextBox txtDocNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox ddlDocType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFirstLastName;
        private System.Windows.Forms.TextBox txtSecondLastName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbFEchaExpiracion;
        private System.Windows.Forms.RadioButton rbNuncaCaduca;
        private System.Windows.Forms.DateTimePicker dtpExpiredDate;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckedListBox chklNotificaciones;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox chklPermisosOpciones;
        private System.Windows.Forms.Button btnSaveExternalUser;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label label7;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdData;
        private System.Windows.Forms.Button btnAddExternalUser;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtPassword1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtPassword2;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label17;
        private System.ComponentModel.BackgroundWorker bgwSendEmail;
        private Infragistics.Win.Misc.UltraValidator uvPacient;
        private System.Windows.Forms.CheckedListBox chkEmpresas;
    }
}