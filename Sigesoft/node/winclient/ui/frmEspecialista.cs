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
using Sigesoft.Node.WinClient.UI.Reports;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmEspecialista : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        string strFilterExpression;
        public List<string> _componentIds { get; set; }
        private string _serviceId;
        private string _pacientId;
        private string _protocolId;
        private string _customerOrganizationName;
        private string _personFullName;
        private string _ServiceComponentId;
        private int _CategoriaId;
        private MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();
        public frmEspecialista( int pintCategoriaId)
        {
            _CategoriaId = pintCategoriaId;
            InitializeComponent();
        }

        private void frmEspecialista_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);

            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);

            Utils.LoadDropDownList(ddComponentStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 127, null), DropDownListAction.All);
            ddComponentStatusId.SelectedValue = "6";

            //dtpDateTimeStar.Value = dtpDateTimeStar.Value.AddDays(-2);
            btnFilter_Click(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtPacient.Text)) Filters.Add("v_PacientDocument.Contains(\"" + txtPacient.Text.Trim() + "\")");
           
            var id1 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
            }

            if (ddComponentStatusId.SelectedValue.ToString() != "-1") Filters.Add("i_ServiceComponentStatusId==" + ddComponentStatusId.SelectedValue);

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

            this.BindGrid();
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, "", strFilterExpression);

            var Listafinal = objData.FindAll(p => _componentIds.Contains(p.v_ComponentId));
            grdDataService.DataSource = Listafinal;
            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            if (grdDataService.Rows.Count > 0)
                grdDataService.Rows[0].Selected = true;
        }

        private List<ServiceList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            //var _objData = _serviceBL.GetServicesPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate, _componentIds, DateTime.Parse("01/01/2000"), DateTime.Parse("01/01/2050"));

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private void grdDataService_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (_CategoriaId == 5 || _CategoriaId == 6)
            {
                btnGenerarLiquidacion.Enabled = true;
            }
            btnEditarESO.Enabled = 
            btnAdminReportes.Enabled = 
            btnInterconsulta.Enabled=
            btnCulminarExamen.Enabled=
             (grdDataService.Selected.Rows.Count > 0);

            if (grdDataService.Selected.Rows.Count == 0)
                return;

            _serviceId = grdDataService.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            _pacientId = grdDataService.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            _protocolId = grdDataService.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
            _customerOrganizationName = grdDataService.Selected.Rows[0].Cells["v_OrganizationName"].Value.ToString();
            _personFullName = grdDataService.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();
            _ServiceComponentId = grdDataService.Selected.Rows[0].Cells["v_ServiceComponentId"].Value.ToString();
        }

        private void btnEditarESO_Click(object sender, EventArgs e)
        {
          
            OperationResult operationResult = new OperationResult();


            List<FileInfoDto> multimediaFile = _multimediaFileBL.GetMultimediaFiles(ref operationResult, _ServiceComponentId);

            // Analizar el resultado de la operación
            if (operationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            #region Download file


            foreach (var item in multimediaFile)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Title = item.FileName;
                    sfd.FileName = item.FileName;

                    DialogResult dialogResult = sfd.ShowDialog();

                    if (dialogResult == DialogResult.OK)
                    {
                        if (String.IsNullOrEmpty(sfd.FileName))
                        {
                            MessageBox.Show("Escriba un nombre para el archivo.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        string path = sfd.FileName;
                        File.WriteAllBytes(path, item.ByteArrayFile);

                    }
                    else
                    {
                        //Inform the user
                    }
                }
            }
          

            #endregion
        }

        private void btnAdminReportes_Click(object sender, EventArgs e)
        {

            List<string> Lista = new List<string>();

            //Rayos X
            if (_CategoriaId == 6)
            {

                if (_componentIds.Contains(Constants.RX_TORAX_ID))
                {
                    Lista.Add(Constants.RX_TORAX_ID);
                    frmConsolidatedReports frm = new frmConsolidatedReports(_serviceId, Lista,null);
                    frm.ShowDialog();
                }
                else
                {

                    Lista.Add(Constants.INFORME_RADIOGRAFICO_OIT);
                    frmConsolidatedReports frm = new frmConsolidatedReports(_serviceId, Lista,null);
                    frm.ShowDialog();
                }


               
            }
            //Espirometría
             else if(_CategoriaId ==16)
            {
                Lista.Add(Constants.ESPIROMETRIA_CUESTIONARIO_ID);
                frmConsolidatedReports frm = new frmConsolidatedReports(_serviceId, Lista,null);

                frm.ShowDialog();
            }
            //Cardiología
            else if (_CategoriaId == 5)
            {
                Lista.Add(Constants.EVA_CARDIOLOGICA_ID);
                frmConsolidatedReports frm = new frmConsolidatedReports(_serviceId, Lista,null);

                frm.ShowDialog();
            }
            //Audiometría
            else if (_CategoriaId == 15)
            {
                Lista.Add(Constants.AUDIOMETRIA_ID);
                frmConsolidatedReports frm = new frmConsolidatedReports(_serviceId,Lista,null);

                frm.ShowDialog();

            }
        }

        private void btnInterconsulta_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            string Componentes = String.Join("|", _componentIds);
            var frm = new frmEspecialistaDiagnostico(_serviceId, Componentes);
            frm.ShowDialog();

            //OperationResult objOperationResult = new OperationResult();
            //List<DiagnosticRepositoryList> _tmpTotalDiagnosticList = null;
            //List<DiagnosticRepositoryList> _tmpTotalDiagnosticByServiceIdList = _serviceBL.GetServiceComponentDisgnosticsByServiceId(ref objOperationResult, _serviceId);

            //var frm = new Sigesoft.Node.WinClient.UI.Operations.Popups.frmAddTotalDiagnostic();
            ////frm._componentId = _componentId;
            //frm._serviceId = _serviceId;

            //if (_tmpTotalDiagnosticList != null)
            //{
            //    frm._tmpTotalDiagnosticList = _tmpTotalDiagnosticByServiceIdList;
            //}

            //frm.ShowDialog();

            //if (frm.DialogResult == DialogResult.Cancel)
            //    return;
        }

        private void btnCulminarExamen_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada
            ServiceBL oServiceBL = new ServiceBL();


            DialogResult Result = MessageBox.Show("¿Está seguro de culminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item              
                
                oServiceBL.UpdateEstadoServiceComponent(ref objOperationResult, _ServiceComponentId, (int)ServiceComponentStatus.Evaluado);

                btnFilter_Click(sender, e);
            }
        }

        private void btnGenerarLiquidacion_Click(object sender, EventArgs e)
        {
            //Cardiología
            if (_CategoriaId == 5)
            {
                frmRegistroElectrocardiograma frm = new frmRegistroElectrocardiograma(_pacientId, _ServiceComponentId);
                frm.ShowDialog();
            }
            //RAYOS X
            else if (_CategoriaId == 6)
            {
                frmRegistroRadiografiaTorax frm = new frmRegistroRadiografiaTorax(_pacientId, _ServiceComponentId);
                frm.ShowDialog();
            }
        }
    }
}
