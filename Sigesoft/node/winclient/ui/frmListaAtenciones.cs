using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;
using System.IO;
using NetPdf;
using Infragistics.Win.UltraWinGrid;


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmListaAtenciones : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();
        string strFilterExpression;
        List<string> _componentIds;

        public frmListaAtenciones()
        {
            InitializeComponent();
        }

        private void frmListaAtenciones_Load(object sender, EventArgs e)
        {
            UltraGridBand band = this.grdData.DisplayLayout.Bands[0];

            //Arrange the band's column in Group Layout style
            this.grdData.DisplayLayout.Bands[0].RowLayoutStyle = RowLayoutStyle.GroupLayout;

            //Enbale Column/Group moving
            this.grdData.DisplayLayout.Override.AllowRowLayoutColMoving = Infragistics.Win.Layout.GridBagLayoutAllowMoving.AllowAll;

            UltraGridGroup GrupoDatosPersonales = band.Groups.Add("GrupoDatosGenerales", "DatosGenerales");
            band.Groups["GrupoDatosGenerales"].Header.Appearance.BackColor = System.Drawing.Color.Tomato;

            band.Columns["v_ServiceId"].RowLayoutColumnInfo.ParentGroup = GrupoDatosPersonales;
            band.Columns["d_ServiceDate"].RowLayoutColumnInfo.ParentGroup = GrupoDatosPersonales;
            band.Columns["v_Pacient"].RowLayoutColumnInfo.ParentGroup = GrupoDatosPersonales;
            band.Columns["v_AptitudeStatusName"].RowLayoutColumnInfo.ParentGroup = GrupoDatosPersonales;

            //Create a parent group for 1stQuarter and 2ndQuarter columns 
            UltraGridGroup GrupoCertificados = band.Groups.Add("GrupoCertificado", "Certificados");
            band.Groups["GrupoCertificado"].Header.Appearance.BackColor = System.Drawing.Color.Green;

            band.Columns["CertGenerado"].RowLayoutColumnInfo.ParentGroup = GrupoCertificados;
            band.Columns["CertEnviado"].RowLayoutColumnInfo.ParentGroup = GrupoCertificados;


            //Create a parent group for 1stQuarter and 2ndQuarter columns 
            UltraGridGroup GrupoHistorias = band.Groups.Add("GrupoHistorias", "Historias");
            band.Groups["GrupoHistorias"].Header.Appearance.BackColor = System.Drawing.Color.Yellow;

            band.Columns["HistoriaGenerada"].RowLayoutColumnInfo.ParentGroup = GrupoHistorias;
            band.Columns["HistoriaEnviada"].RowLayoutColumnInfo.ParentGroup = GrupoHistorias;


          

        
            dtpDateTimeStar.Value = dtpDateTimeStar.Value.AddDays(-1);

            OperationResult objOperationResult = new OperationResult();

               var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);
                 // Obtener permisos de cada examen de un rol especifico
            var componentProfile = _serviceBL.GetRoleNodeComponentProfileByRoleNodeId(Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value);

            _componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);

            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

            groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));
            // Remover los componentes que no estan asignados al rol del usuario
            var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));



            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
            // Establecer el filtro inicial para los datos
            strFilterExpression = null;

        }

        private void ddlConsultorio_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void ddlServiceTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ddlMasterServiceId_SelectedIndexChanged(object sender, EventArgs e)
        {
          

        }

        private void ddlCustomerOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

            // Get the filters from the UI
            List<string> Filters = new List<string>();
          
          
            var id1 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
            }
            
          
            // Create the Filter Expression
            strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.BindGrid();
            };
        }
        private void BindGrid()
        {
            var objData = GetData(0, null, "", strFilterExpression);
            grdData.DataSource = objData;
            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            if (grdData.Rows.Count > 0)
            {
                grdData.Rows[0].Selected = true;
            }

        }

        private List<ListaAtenciones> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var _objData = _serviceBL.DevolverListaAtenciones(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);
               
            return _objData;
        }

        private void grdData_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void grdData_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            int Result1 = e.Row.Cells["i_EnvioHistoria"].Value == null ? 0 : (int)e.Row.Cells["i_EnvioHistoria"].Value;

            if (Result1 == (int)SiNo.NO)
            {
                e.Row.Cells["HistoriaEnviada"].Value = Resources.delete;
                
            }
            else
            {
                e.Row.Cells["HistoriaEnviada"].Value = Resources.accept;
            }

            int Result2 = e.Row.Cells["i_EnvioCertificado"].Value == null ? 0 : (int)e.Row.Cells["i_EnvioCertificado"].Value;

            if (Result2 == (int)SiNo.NO)
            {
                e.Row.Cells["CertEnviado"].Value = Resources.delete;

            }
            else
            {
                e.Row.Cells["CertEnviado"].Value = Resources.accept;
            }



            int Result3 = e.Row.Cells["i_StatusLiquidation"].Value == null ? 1 : (int)e.Row.Cells["i_StatusLiquidation"].Value;

            if (Result3 == (int)PreLiquidationStatus.Generada)
            {
                e.Row.Cells["HistoriaGenerada"].Value = Resources.accept;
                e.Row.Cells["CertGenerado"].Value = Resources.accept;
            }
            else
            {
                e.Row.Cells["HistoriaGenerada"].Value = Resources.delete;
                e.Row.Cells["CertGenerado"].Value = Resources.delete;
            }


        }


    }
}
