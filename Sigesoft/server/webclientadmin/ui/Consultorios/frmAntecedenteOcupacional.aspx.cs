using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;

namespace Sigesoft.Server.WebClientAdmin.UI.Consultorios
{
    public partial class frmAntecedenteOcupacional : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        HistoryBL _objHistoryBL = new HistoryBL();
        historyDto _objhistoryDto;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OperationResult objOperationResult = new OperationResult();
                Utils.LoadDropDownList(ddlTipoOperacion, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 177), DropDownListAction.Select);
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();

               
            }
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
            string Mode = Request.QueryString["Mode"].ToString();
            string HistoryId = "";

            if (Request.QueryString["v_HistoryId"] != null)
                HistoryId = Request.QueryString["v_HistoryId"].ToString();

            if (Mode == "New")
            {
                dtcFechaInicio.SelectedDate = DateTime.Now;
                dtcFechaFin.SelectedDate = DateTime.Now;
                chkSoloAnio.Checked = true;
                txtEmpresa.Text = "";
                txtArea.Text = "";
                txtPuestoTrabajo.Text = "";
                chkPuestoActual.Checked = false;
                txtAlturaGeografica.Text = "";
                ddlTipoOperacion.SelectedValue = "1";

            }
            else if (Mode == "Edit")
            {
                _objhistoryDto = new historyDto();

                _objhistoryDto = _objHistoryBL.GetHistory(ref objOperationResult, HistoryId);

                Session["objEntity"] = _objhistoryDto;
                dtcFechaInicio.SelectedDate = _objhistoryDto.d_StartDate.Value;
                dtcFechaFin.SelectedDate = _objhistoryDto.d_EndDate.Value;
                chkSoloAnio.Checked = _objhistoryDto.i_SoloAnio == 1 ? true : false;
                txtEmpresa.Text = _objhistoryDto.v_Organization;
                txtArea.Text = _objhistoryDto.v_TypeActivity;
                txtPuestoTrabajo.Text = _objhistoryDto.v_workstation;
                ddlTipoOperacion.SelectedValue = _objhistoryDto.i_TypeOperationId.ToString();
                txtAlturaGeografica.Text = _objhistoryDto.i_GeografixcaHeight == 0 ? "" : _objhistoryDto.i_GeografixcaHeight.ToString();

                if (_objhistoryDto.i_TrabajoActual != null)
                {
                    if (_objhistoryDto.i_TrabajoActual == 1)
                    {
                        chkPuestoActual.Checked = true;
                    }
                    else
                    {
                        chkPuestoActual.Checked = false;
                    }
                }
                
            }


        }
        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString["Mode"].ToString();
            string PersonId = "";
            if (Request.QueryString["v_PersonId"] != null)
                PersonId = Request.QueryString["v_PersonId"].ToString();

            OperationResult objOperationResult = new OperationResult();
            
            if (Mode == "New")
            {
                _objhistoryDto = new historyDto();

                _objhistoryDto.v_PersonId = Session["PersonId"].ToString();
                _objhistoryDto.d_StartDate = dtcFechaInicio.SelectedDate;
                _objhistoryDto.d_EndDate = dtcFechaFin.SelectedDate;
                _objhistoryDto.v_Organization = txtEmpresa.Text;
                _objhistoryDto.i_SoloAnio = chkSoloAnio.Checked == true ? 1 : 0;
                _objhistoryDto.v_TypeActivity = txtArea.Text;
                _objhistoryDto.v_workstation = txtPuestoTrabajo.Text;
                _objhistoryDto.i_TrabajoActual = chkPuestoActual.Checked == true ? 1 : 0;
                _objhistoryDto.i_TypeOperationId = int.Parse(ddlTipoOperacion.SelectedValue.ToString());
                _objhistoryDto.i_GeografixcaHeight = txtAlturaGeografica.Text == "" ? 0 : int.Parse(txtAlturaGeografica.Text.ToString());

                _objHistoryBL.AddHistory(ref objOperationResult,_objhistoryDto, ((ClientSession)Session["objClientSession"]).GetAsList());

            }
            else if (Mode == "Edit")
            {
                historyDto objEntity = (historyDto)Session["objEntity"];

                objEntity.d_StartDate = dtcFechaInicio.SelectedDate;
                objEntity.d_EndDate = dtcFechaFin.SelectedDate;
                objEntity.v_Organization = txtEmpresa.Text;
                objEntity.i_SoloAnio = chkSoloAnio.Checked == true ? 1 : 0;
                objEntity.v_TypeActivity = txtArea.Text;
                objEntity.v_workstation = txtPuestoTrabajo.Text;
                objEntity.i_TrabajoActual = chkPuestoActual.Checked == true ? 1 : 0;
                objEntity.i_TypeOperationId = int.Parse(ddlTipoOperacion.SelectedValue.ToString());
                objEntity.i_GeografixcaHeight = txtAlturaGeografica.Text == "" ? 0 : int.Parse(txtAlturaGeografica.Text.ToString());

                _objHistoryBL.UpdateHistory(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());

            }

            //Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                // Cerrar página actual y hacer postback en el padre para actualizar
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else  // Operación con error
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                // Se queda en el formulario.
            }
        }
    }
}