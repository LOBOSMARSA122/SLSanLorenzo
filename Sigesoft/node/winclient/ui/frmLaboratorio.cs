using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmLaboratorio : Form
    {
        public frmLaboratorio()
        {
            InitializeComponent();
        }

        private void frmLaboratorio_Load(object sender, EventArgs e)
        {

            UltraGridBand band = this.ultraGrid1.DisplayLayout.Bands[0];

            //Arrange the band's column in Group Layout style
            this.ultraGrid1.DisplayLayout.Bands[0].RowLayoutStyle = RowLayoutStyle.GroupLayout;

            //Enbale Column/Group moving
            this.ultraGrid1.DisplayLayout.Override.AllowRowLayoutColMoving = Infragistics.Win.Layout.GridBagLayoutAllowMoving.AllowAll;


            #region Grupo Acido Urico
            //Create a parent group for 1stQuarter and 2ndQuarter columns 
            UltraGridGroup GrupoAcidoUrico = band.Groups.Add("GrupoAcidoUrico", "Ácido Úrico");
        
            band.Columns["AcidoUrico"].RowLayoutColumnInfo.ParentGroup = GrupoAcidoUrico;
            band.Columns["AUricoDeseable"].RowLayoutColumnInfo.ParentGroup = GrupoAcidoUrico;
            #endregion

            #region Aglutaciones
            UltraGridGroup GrupoAglutaciones = band.Groups.Add("Aglutaciones", "Aglutaciones en Lámina");
            band.Groups["Aglutaciones"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Groups["Aglutaciones"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;

            band.Columns["ParatificoA"].RowLayoutColumnInfo.ParentGroup = GrupoAglutaciones;
            band.Columns["ParatificoADeseable"].RowLayoutColumnInfo.ParentGroup = GrupoAglutaciones;
            band.Columns["tificoO"].RowLayoutColumnInfo.ParentGroup = GrupoAglutaciones;
            band.Columns["tificoODeseable"].RowLayoutColumnInfo.ParentGroup = GrupoAglutaciones;
            band.Columns["TificoH"].RowLayoutColumnInfo.ParentGroup = GrupoAglutaciones;
            band.Columns["TificoHDeseable"].RowLayoutColumnInfo.ParentGroup = GrupoAglutaciones;
            band.Columns["ParatificoB"].RowLayoutColumnInfo.ParentGroup = GrupoAglutaciones;
            band.Columns["ParatificoBDeseable"].RowLayoutColumnInfo.ParentGroup = GrupoAglutaciones;

            band.Columns["ParatificoA"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Columns["ParatificoA"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;
            band.Columns["ParatificoADeseable"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Columns["ParatificoADeseable"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;
            band.Columns["tificoO"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Columns["tificoO"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;
            band.Columns["tificoODeseable"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Columns["tificoODeseable"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;
            band.Columns["TificoH"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Columns["TificoH"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;
            band.Columns["TificoHDeseable"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Columns["TificoHDeseable"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;
            band.Columns["ParatificoB"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Columns["ParatificoB"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;
            band.Columns["ParatificoBDeseable"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Columns["ParatificoBDeseable"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;

            #endregion

            #region Antígeno Prostático
            UltraGridGroup GrupoAntigenoProstatico = band.Groups.Add("GrupoAntigenoProstatico", "Ántigeno Prostático");
            band.Columns["PASDeseable"].RowLayoutColumnInfo.ParentGroup = GrupoAntigenoProstatico;
            band.Columns["AntigenoProstatico"].RowLayoutColumnInfo.ParentGroup = GrupoAntigenoProstatico;
            #endregion

            #region BK - Directo
            UltraGridGroup GrupoBKDirecto = band.Groups.Add("GrupoBKDirecto", "BK Directo");
            band.Groups["GrupoBKDirecto"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Groups["GrupoBKDirecto"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;

            band.Columns["ResultadoBK"].RowLayoutColumnInfo.ParentGroup = GrupoBKDirecto;
            band.Columns["MuestraBK"].RowLayoutColumnInfo.ParentGroup = GrupoBKDirecto;
            band.Columns["ColoracionBK"].RowLayoutColumnInfo.ParentGroup = GrupoBKDirecto;

            band.Columns["ResultadoBK"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Columns["ResultadoBK"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;
            band.Columns["MuestraBK"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Columns["MuestraBK"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;
            band.Columns["ColoracionBK"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;
            band.Columns["ColoracionBK"].Header.Appearance.BackColor2 = System.Drawing.Color.Tomato;

            #endregion






        }


    }
}
