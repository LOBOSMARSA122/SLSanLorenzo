namespace Sigesoft.Node.WinClient.UI
{
    partial class frmTramasSusalud
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
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            this.tabAmbulatorio = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.tabEmergencia = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.tabHospi = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.tabProcedimientos = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.tabPartos = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.tabCirugias = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.utcSusalud = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.btnAgregar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.utcSusalud)).BeginInit();
            this.utcSusalud.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabAmbulatorio
            // 
            this.tabAmbulatorio.Location = new System.Drawing.Point(1, 23);
            this.tabAmbulatorio.Name = "tabAmbulatorio";
            this.tabAmbulatorio.Size = new System.Drawing.Size(634, 296);
            // 
            // tabEmergencia
            // 
            this.tabEmergencia.Location = new System.Drawing.Point(-10000, -10000);
            this.tabEmergencia.Name = "tabEmergencia";
            this.tabEmergencia.Size = new System.Drawing.Size(634, 296);
            // 
            // tabHospi
            // 
            this.tabHospi.Location = new System.Drawing.Point(-10000, -10000);
            this.tabHospi.Name = "tabHospi";
            this.tabHospi.Size = new System.Drawing.Size(634, 296);
            // 
            // tabProcedimientos
            // 
            this.tabProcedimientos.Location = new System.Drawing.Point(-10000, -10000);
            this.tabProcedimientos.Name = "tabProcedimientos";
            this.tabProcedimientos.Size = new System.Drawing.Size(634, 296);
            // 
            // tabPartos
            // 
            this.tabPartos.Location = new System.Drawing.Point(-10000, -10000);
            this.tabPartos.Name = "tabPartos";
            this.tabPartos.Size = new System.Drawing.Size(634, 296);
            // 
            // tabCirugias
            // 
            this.tabCirugias.Location = new System.Drawing.Point(-10000, -10000);
            this.tabCirugias.Name = "tabCirugias";
            this.tabCirugias.Size = new System.Drawing.Size(634, 296);
            // 
            // utcSusalud
            // 
            this.utcSusalud.Controls.Add(this.ultraTabSharedControlsPage1);
            this.utcSusalud.Controls.Add(this.tabAmbulatorio);
            this.utcSusalud.Controls.Add(this.tabEmergencia);
            this.utcSusalud.Controls.Add(this.tabHospi);
            this.utcSusalud.Controls.Add(this.tabProcedimientos);
            this.utcSusalud.Controls.Add(this.tabPartos);
            this.utcSusalud.Controls.Add(this.tabCirugias);
            this.utcSusalud.Location = new System.Drawing.Point(12, 50);
            this.utcSusalud.Name = "utcSusalud";
            this.utcSusalud.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.utcSusalud.Size = new System.Drawing.Size(638, 322);
            this.utcSusalud.TabIndex = 0;
            ultraTab1.TabPage = this.tabAmbulatorio;
            ultraTab1.Text = "Ambulatorio";
            ultraTab2.TabPage = this.tabEmergencia;
            ultraTab2.Text = "Emergencia";
            ultraTab3.TabPage = this.tabHospi;
            ultraTab3.Text = "Hospitalización";
            ultraTab4.TabPage = this.tabProcedimientos;
            ultraTab4.Text = "Procedimientos";
            ultraTab5.TabPage = this.tabPartos;
            ultraTab5.Text = "Partos";
            ultraTab6.TabPage = this.tabCirugias;
            ultraTab6.Text = "Cirugías";
            this.utcSusalud.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2,
            ultraTab3,
            ultraTab4,
            ultraTab5,
            ultraTab6});
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(634, 296);
            // 
            // btnAgregar
            // 
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnAgregar.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.add;
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(697, 73);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(85, 31);
            this.btnAgregar.TabIndex = 1;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // frmTramasSusalud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 414);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.utcSusalud);
            this.Name = "frmTramasSusalud";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tramas SUSALUD";
            ((System.ComponentModel.ISupportInitialize)(this.utcSusalud)).EndInit();
            this.utcSusalud.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinTabControl.UltraTabControl utcSusalud;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabAmbulatorio;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabEmergencia;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabHospi;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabProcedimientos;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabPartos;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabCirugias;
        private System.Windows.Forms.Button btnAgregar;

    }
}