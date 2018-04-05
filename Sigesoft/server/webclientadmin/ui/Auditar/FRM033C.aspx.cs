using FineUI;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sigesoft.Server.WebClientAdmin.UI.Auditar
{
    public partial class FRM033C : System.Web.UI.Page
    {
        ServiceBL oServiceBL = new ServiceBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCombo();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
                //btnCie10.OnClientClick = WindowCie10.GetSaveStateReference(hfRefresh.ClientID) + WindowCie10.GetShowReference("FRM033F.aspx?Mode=New");
            }
        }

        private void loadCombo()
        {
            OperationResult objOperationResult = new OperationResult();

            var _componentListTemp = oServiceBL.GetAllComponents(ref objOperationResult);

            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

            groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));


            Utils.LoadDropDownList(ddlConsultorio, "Value1", "Value4", groupComponentList, DropDownListAction.Select);
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {


            OperationResult objOperationResult = new OperationResult();
            diseasesDto objDiseaseDto = new diseasesDto();
            diseasesDto objDiseaseDto1 = new diseasesDto();

            if (Session["DiseasesId"] != null)
            {
                objDiseaseDto = oServiceBL.GetDiseases(ref  objOperationResult, Session["DiseasesId"].ToString());

                objDiseaseDto.v_CIE10Id = Session["Cie10Id"].ToString();
                objDiseaseDto.v_Name = txtDxModificado.Text;
                oServiceBL.UpdateDiseases(ref objOperationResult, objDiseaseDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                //_objDiseasesList.v_DiseasesId = objDiseaseDto.v_DiseasesId;
                //_objDiseasesList.v_CIE10Id = objDiseaseDto.v_CIE10Id;
                //_objDiseasesList.v_Name = objDiseaseDto.v_Name;
            }
            else
            {
                objDiseaseDto.v_CIE10Id = Session["Cie10Id"].ToString();
                objDiseaseDto.v_Name = txtDxModificado.Text;


                objDiseaseDto1 = oServiceBL.GetIsValidateDiseases(ref objOperationResult, objDiseaseDto.v_Name);

                if (objDiseaseDto1 == null)
                {
                    objDiseaseDto.v_DiseasesId = oServiceBL.AddDiseases(ref objOperationResult, objDiseaseDto, ((ClientSession)Session["objClientSession"]).GetAsList());
                }
                else
                {
                    Alert.Show("Escoja uno que tenga código interno", "Error de validación", MessageBoxIcon.Warning);
                    return;
                }
            }

            //Grabar el Dx en el servicio


            Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList _diagnosticRepository = new Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList();
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> _ListadiagnosticRepository = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();

            _diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
            _diagnosticRepository.v_DiseasesId = Session["DiseasesId"] == null ? objDiseaseDto.v_DiseasesId : Session["DiseasesId"].ToString();
            _diagnosticRepository.i_AutoManualId = 1;
            _diagnosticRepository.i_PreQualificationId = 1;
            _diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Definitivo;
            _diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;

            _diagnosticRepository.v_ServiceId = Session["ServiceId"].ToString();
            _diagnosticRepository.v_ComponentId = ddlExamen.SelectedValue.ToString();  //_componentId;
            _diagnosticRepository.v_DiseasesName = objDiseaseDto.v_Name;
            _diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
            _diagnosticRepository.i_RecordType = (int)RecordType.Temporal;
            _diagnosticRepository.Restrictions = null;
            _diagnosticRepository.Recomendations = null;

            _ListadiagnosticRepository.Add(_diagnosticRepository);

            oServiceBL.AddDiagnosticRepository(ref objOperationResult, _ListadiagnosticRepository, null, ((ClientSession)Session["objClientSession"]).GetAsList(), true);

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

        protected void WindowCie10_Close(object sender, EventArgs e)
        {

        }

        protected void ddlConsultorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Obtener Componentes del consultorio en función de su protoclo
            var CategoriaId = ddlConsultorio.SelectedValue;

            //Obtener los componetes de ese servicio en función de la categoría seleccionada

            var Examenes = oServiceBL.DevolverExamenesPorCategoria(Session["ServiceId"].ToString(), int.Parse(CategoriaId));

            Utils.LoadDropDownList(ddlExamen, "Value1", "Id", Examenes, DropDownListAction.Select);

        }

        protected void grdCie10_RowClick(object sender, FineUI.GridRowClickEventArgs e)
        {

            int index = e.RowIndex;
            var dataKeys = grdCie10.DataKeys[index];
            Session["Cie10Id"] = dataKeys[0];
            Session["DiseasesId"] = dataKeys[2];
            txtDxModificado.Text = dataKeys[1].ToString();


        }

        protected void btnCie10_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtDx.Text)) Filters.Add("v_CIE10Idv_Name.Contains(\"" + txtDx.Text.Trim() + "\")");
            string strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }

            // Save the Filter expression in the Session
            Session["strFilterExpression"] = strFilterExpression;

            // Refresh the grid
            grdCie10.PageIndex = 0;
            this.BindGrid();
        }

        protected void grdCie10_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grdCie10.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        private int GetTotalCount()
        {
            OperationResult objOperationResult = new OperationResult();
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            return oServiceBL.GetCIE10Count(ref objOperationResult, strFilterExpression);
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            OperationResult objOperationResult = new OperationResult();
            grdCie10.RecordCount = GetTotalCount();
            grdCie10.DataSource = oServiceBL.GetDiseasesPagedAndFiltered(ref objOperationResult, grdCie10.PageIndex, grdCie10.PageSize, "v_CIE10Id", strFilterExpression);
            grdCie10.DataBind();
        }
    }
}