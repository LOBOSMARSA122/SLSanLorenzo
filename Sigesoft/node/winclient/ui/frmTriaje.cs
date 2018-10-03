using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmTriaje : Form
    {
        string _PersonId;
        string _ServiceComponentId;
        string _ServiceId;
        public frmTriaje(string pstrPersonId, string pstrServiceComponentId, string pstrServiceId)
        {
            InitializeComponent();
            _PersonId = pstrPersonId;
            _ServiceComponentId = pstrServiceComponentId;
            _ServiceId = pstrServiceId;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (uvValidador.Validate(true, false).IsValid)
            {

                if (double.Parse(txtTalla.Text) < 0.5 || double.Parse(txtTalla.Text) > 2.5)
                {
                    MessageBox.Show("Talla: valor entre 0.5 y 2.5  ", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (double.Parse(txtPeso.Text) < 1 || double.Parse(txtPeso.Text) > 200)
                {
                    MessageBox.Show("Peso: valor entre 1 y 200  ", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtPerAbd.Text != "")
                {
                    if (double.Parse(txtPerAbd.Text) < 1 || double.Parse(txtPerAbd.Text) > 300)
                    {
                        MessageBox.Show("Perímetro Abdominal: valor entre 1 y 300  ", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                if (txtPerCad.Text != "")
                {
                    if (double.Parse(txtPerCad.Text) < 1 || double.Parse(txtPerCad.Text) > 300)
                    {
                        MessageBox.Show("Perímetro Cadera: valor entre 1 y 300  ", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (txtTemperatura.Text != "")
                {
                    if (double.Parse(txtTemperatura.Text) < 15 || double.Parse(txtTemperatura.Text) > 50)
                    {
                        MessageBox.Show("Temperatura: valor entre 15 y 50  ", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (int.Parse(txtPAS.Text) < 40 || int.Parse(txtPAS.Text) > 350)
                {
                    MessageBox.Show("PAS: valor entre 40 y 350  ", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (int.Parse(txtPAD.Text) < 40 || int.Parse(txtPAD.Text) > 350)
                {
                    MessageBox.Show("PAD: valor entre 40 y 350  ", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtFrecResp.Text != "")
                {
                    if (int.Parse(txtFrecResp.Text) < 0 || int.Parse(txtFrecResp.Text) > 60)
                    {
                        MessageBox.Show("Frecuencia Respiratoria: valor entre 0 y 60  ", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (txtFrecCard.Text != "")
                {
                    if (int.Parse(txtFrecCard.Text) < 0 || int.Parse(txtFrecCard.Text) > 250)
                    {
                        MessageBox.Show("Frecuencia Cardiaca: valor entre 0 y 250  ", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

            OperationResult objOperationResult = new OperationResult();
            ServiceBL _serviceBL = new ServiceBL();
            ServiceComponentFieldValuesList serviceComponentFieldValues = null;
            ServiceComponentFieldsList serviceComponentFields = null;
            List<ServiceComponentFieldValuesList> _serviceComponentFieldValuesList = null;
            List<ServiceComponentFieldsList> _serviceComponentFieldsList = null;

            if (_serviceComponentFieldsList == null)
                _serviceComponentFieldsList = new List<ServiceComponentFieldsList>();


            //Talla**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ANTROPOMETRIA_TALLA_ID;
            serviceComponentFields.v_ServiceComponentId = _ServiceComponentId;

            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtTalla.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //Temperatura**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_TEMPERATURA_ID;
            serviceComponentFields.v_ServiceComponentId = _ServiceComponentId;

            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtTemperatura.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //Peso**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ANTROPOMETRIA_PESO_ID;
            serviceComponentFields.v_ServiceComponentId = _ServiceComponentId;

            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtPeso.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);



            //PAS**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_PAS_ID;
            serviceComponentFields.v_ServiceComponentId = _ServiceComponentId;

            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtPAS.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //IMC**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ANTROPOMETRIA_IMC_ID;
            serviceComponentFields.v_ServiceComponentId = _ServiceComponentId;

            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtImc.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //PAD**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_PAD_ID;
            serviceComponentFields.v_ServiceComponentId = _ServiceComponentId;

            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtPAD.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);




            //Perímetro Abdomen**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID;
            serviceComponentFields.v_ServiceComponentId = _ServiceComponentId;

            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtPerAbd.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //Frecuencia Cardiaca**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID;
            serviceComponentFields.v_ServiceComponentId = _ServiceComponentId;

            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtFrecCard.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //Perímetro Cadera**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID;
            serviceComponentFields.v_ServiceComponentId = _ServiceComponentId;

            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtPerCad.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //Frecuencia Respiratoria**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID;
            serviceComponentFields.v_ServiceComponentId = _ServiceComponentId;

            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtFrecResp.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //ICC**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ANTROPOMETRIA_INDICE_CINTURA_ID;
            serviceComponentFields.v_ServiceComponentId = _ServiceComponentId;

            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtICC.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //Saturación Oxígeno**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_SAT_O2_ID;
            serviceComponentFields.v_ServiceComponentId = _ServiceComponentId;

            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtSatOx.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);




            
            var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                                 _serviceComponentFieldsList,
                                                                 Globals.ClientSession.GetAsList(),
                                                                 _PersonId,
                                                                 _ServiceComponentId);

            //lIMPIAR LA LISTA DE DXS
            List<DiagnosticRepositoryList> ListaDxByComponent = new List<DiagnosticRepositoryList>();
            MedicalExamFieldValuesBL oMedicalExamFieldValuesBL = new MedicalExamFieldValuesBL();
            //Elminar los Dx antiguos
            _serviceBL.EliminarDxAniguosPorComponente(_ServiceId, Constants.FUNCIONES_VITALES_ID, Globals.ClientSession.GetAsList());
            _serviceBL.EliminarDxAniguosPorComponente(_ServiceId, Constants.ANTROPOMETRIA_ID, Globals.ClientSession.GetAsList());
            ListaDxByComponent = new List<DiagnosticRepositoryList>();

            if (txtImc.Text != "")
            {
                double IMC = double.Parse(txtImc.Text.ToString());
                DiagnosticRepositoryList DxByComponent = new DiagnosticRepositoryList();

                List<RecomendationList> Recomendations = new List<RecomendationList>();
                List<RestrictionList> Restrictions = new List<RestrictionList>();

                DxByComponent.i_AutoManualId = 1;
                DxByComponent.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                DxByComponent.i_PreQualificationId = 1;
                DxByComponent.v_ComponentFieldsId = Constants.ANTROPOMETRIA_IMC_ID;

                //Obtener el Componente que está amarrado al DX
                string ComponentDx = oMedicalExamFieldValuesBL.ObtenerComponentDx(Constants.ANTROPOMETRIA_IMC_ID);
                string DiseasesId = "";
                if (IMC <= 18.49)
                {
                    DiseasesId = "N009-DD000000300";
                }
                else if (IMC >= 18.5 && IMC <= 24.99)
                {
                    DiseasesId = "N009-DD000000788";
                }
                else if (IMC >= 25 && IMC <=29.99)
                {
                    DiseasesId = "N009-DD000000601";
                }
                else if (IMC >= 30 && IMC <= 34.99)
                {
                    DiseasesId = "N009-DD000000602";
                }
                else if (IMC >= 35 && IMC <= 39.99)
                {
                    DiseasesId = "N009-DD000000603";
                }
                else if (IMC >= 40 )
                {
                    DiseasesId = "N009-DD000000604";
                }
                string ComponentFieldId = Constants.ANTROPOMETRIA_IMC_ID;

                DiagnosticRepositoryList oDiagnosticRepositoryListOld = _serviceBL.VerificarDxExistente(_ServiceId, DiseasesId, ComponentDx, ComponentFieldId);
                if (oDiagnosticRepositoryListOld != null)
                {
                    oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId = oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId;
                    oDiagnosticRepositoryListOld.i_RecordType = (int)RecordType.NoTemporal;
                    oDiagnosticRepositoryListOld.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                    oDiagnosticRepositoryListOld.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                    ListaDxByComponent.Add(oDiagnosticRepositoryListOld);
                }

                DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                DxByComponent.i_RecordType = (int)RecordType.Temporal;
                DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;




                DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;

                string ComponentFieldValuesId = oMedicalExamFieldValuesBL.ObtenerIdComponentFieldValues(ComponentFieldId, DiseasesId);
                DxByComponent.v_ComponentFieldValuesId = ComponentFieldValuesId;


                DxByComponent.v_ComponentId = ComponentDx;
                DxByComponent.v_DiseasesId = DiseasesId;
                DxByComponent.v_ServiceId = _ServiceId;


                //Obtener las recomendaciones

                DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendaciones(ComponentFieldValuesId, _ServiceId, Constants.FUNCIONES_VITALES_ID);

                ListaDxByComponent.Add(DxByComponent);



                //Llenar entidad ServiceComponent
                servicecomponentDto serviceComponentDto = new servicecomponentDto();
                serviceComponentDto.v_ServiceComponentId = _ServiceComponentId;
                serviceComponentDto.v_Comment = "";
                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                serviceComponentDto.v_ComponentId = Constants.FUNCIONES_VITALES_ID;
                serviceComponentDto.v_ServiceId = _ServiceId;


                _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                            ListaDxByComponent,
                                            serviceComponentDto,
                                            Globals.ClientSession.GetAsList(),
                                            true,null);
            }

             ListaDxByComponent = new List<DiagnosticRepositoryList>();

            if (txtPAS.Text != "")
            {
                DiagnosticRepositoryList DxByComponent = new DiagnosticRepositoryList();

                List<RecomendationList> Recomendations = new List<RecomendationList>();
                List<RestrictionList> Restrictions = new List<RestrictionList>();
                int PAS = int.Parse(txtPAS.Text.ToString());

                DxByComponent.i_AutoManualId = 1;
                DxByComponent.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                DxByComponent.i_PreQualificationId = 1;
                DxByComponent.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_PAS_ID;
                //Obtener el Componente que está amarrado al DX
                string ComponentDx = oMedicalExamFieldValuesBL.ObtenerComponentDx(Constants.FUNCIONES_VITALES_PAS_ID);
                string DiseasesId = "";
                if (PAS > 140)
                {
                    DiseasesId = "N009-DD000000606";
                    string ComponentFieldId = Constants.FUNCIONES_VITALES_PAS_ID;

                    DiagnosticRepositoryList oDiagnosticRepositoryListOld = _serviceBL.VerificarDxExistente(_ServiceId, DiseasesId, ComponentDx, ComponentFieldId);
                    if (oDiagnosticRepositoryListOld != null)
                    {
                        oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId = oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId;
                        oDiagnosticRepositoryListOld.i_RecordType = (int)RecordType.NoTemporal;
                        oDiagnosticRepositoryListOld.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                        oDiagnosticRepositoryListOld.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                        ListaDxByComponent.Add(oDiagnosticRepositoryListOld);
                    }

                    DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                    DxByComponent.i_RecordType = (int)RecordType.Temporal;
                    DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                    DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;




                    DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;

                    string ComponentFieldValuesId = oMedicalExamFieldValuesBL.ObtenerIdComponentFieldValues(ComponentFieldId, DiseasesId);
                    DxByComponent.v_ComponentFieldValuesId = ComponentFieldValuesId;


                    DxByComponent.v_ComponentId = ComponentDx;
                    DxByComponent.v_DiseasesId = DiseasesId;
                    DxByComponent.v_ServiceId = _ServiceId;


                    //Obtener las recomendaciones

                    DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendaciones(ComponentFieldValuesId, _ServiceId, Constants.FUNCIONES_VITALES_ID);

                    ListaDxByComponent.Add(DxByComponent);

                    //Llenar entidad ServiceComponent
                    servicecomponentDto serviceComponentDto = new servicecomponentDto();
                    serviceComponentDto.v_ServiceComponentId = _ServiceComponentId;
                    serviceComponentDto.v_Comment = "";
                    serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                    serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                    serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                    serviceComponentDto.v_ComponentId = Constants.FUNCIONES_VITALES_ID;
                    serviceComponentDto.v_ServiceId = _ServiceId;


                    _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                ListaDxByComponent,
                                                serviceComponentDto,
                                                Globals.ClientSession.GetAsList(),
                                                true, false);
                }
            }


            ListaDxByComponent = new List<DiagnosticRepositoryList>();
            if (txtICC.Text != "")
            {
                DiagnosticRepositoryList DxByComponent = new DiagnosticRepositoryList();

                List<RecomendationList> Recomendations = new List<RecomendationList>();
                List<RestrictionList> Restrictions = new List<RestrictionList>();
                double ICC = double.Parse(txtICC.Text.ToString());

                DxByComponent.i_AutoManualId = 1;
                DxByComponent.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                DxByComponent.i_PreQualificationId = 1;
                DxByComponent.v_ComponentFieldsId = Constants.ANTROPOMETRIA_INDICE_CINTURA_ID;
                //Obtener el Componente que está amarrado al DX
                string ComponentDx = oMedicalExamFieldValuesBL.ObtenerComponentDx(Constants.ANTROPOMETRIA_INDICE_CINTURA_ID);
                string DiseasesId = "";
                if (ICC > 1)
                {
                    DiseasesId = "N009-DD000000605";

                    string ComponentFieldId = Constants.ANTROPOMETRIA_INDICE_CINTURA_ID;

                    DiagnosticRepositoryList oDiagnosticRepositoryListOld = _serviceBL.VerificarDxExistente(_ServiceId, DiseasesId, ComponentDx, ComponentFieldId);
                    if (oDiagnosticRepositoryListOld != null)
                    {
                        oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId = oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId;
                        oDiagnosticRepositoryListOld.i_RecordType = (int)RecordType.NoTemporal;
                        oDiagnosticRepositoryListOld.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                        oDiagnosticRepositoryListOld.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                        ListaDxByComponent.Add(oDiagnosticRepositoryListOld);
                    }

                    DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                    DxByComponent.i_RecordType = (int)RecordType.Temporal;
                    DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                    DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;




                    DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;

                    string ComponentFieldValuesId = oMedicalExamFieldValuesBL.ObtenerIdComponentFieldValues(ComponentFieldId, DiseasesId);
                    DxByComponent.v_ComponentFieldValuesId = ComponentFieldValuesId;


                    DxByComponent.v_ComponentId = ComponentDx;
                    DxByComponent.v_DiseasesId = DiseasesId;
                    DxByComponent.v_ServiceId = _ServiceId;


                    //Obtener las recomendaciones

                    DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendaciones(ComponentFieldValuesId, _ServiceId, Constants.FUNCIONES_VITALES_ID);

                    ListaDxByComponent.Add(DxByComponent);

                    //Llenar entidad ServiceComponent
                    servicecomponentDto serviceComponentDto = new servicecomponentDto();
                    serviceComponentDto.v_ServiceComponentId = _ServiceComponentId;
                    serviceComponentDto.v_Comment = "";
                    serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                    serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                    serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                    serviceComponentDto.v_ComponentId = Constants.FUNCIONES_VITALES_ID;
                    serviceComponentDto.v_ServiceId = _ServiceId;


                    _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                ListaDxByComponent,
                                                serviceComponentDto,
                                                Globals.ClientSession.GetAsList(),
                                                true, false);
                }
                

               
            }
            _serviceBL.ActualizarEstadoComponentesPorCategoria(ref objOperationResult, 10,_ServiceId, (int)ServiceComponentStatus.Evaluado, Globals.ClientSession.GetAsList());


                MessageBox.Show("Los datos se grabaron correctamente", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void txtPeso_TextChanged(object sender, EventArgs e)
        {
            if (txtPeso.Text !="" && txtTalla.Text !="")
            {                
                double Peso = double.Parse(txtPeso.Text.ToString());
                double Talla = double.Parse(txtTalla.Text.ToString());

                txtImc.Text = ((Peso / Talla) / Talla).ToString("#.##");
            }

        }

        private void txtTalla_TextChanged(object sender, EventArgs e)
        {
            if (txtPeso.Text != "" && txtTalla.Text != "")
            {
                double Peso = double.Parse(txtPeso.Text.ToString());
                double Talla = double.Parse(txtTalla.Text.ToString());

                txtImc.Text = ((Peso / Talla) / Talla).ToString("#.##");
            }
        }

        private void frmTriaje_Load(object sender, EventArgs e)
        {
            ServiceBL _serviceBL = new ServiceBL();

            var oExamenFisico = _serviceBL.ObtenerIdsParaImportacionExcel(new List<string> { _ServiceId }, 10);
            var objExamenFisico = _serviceBL.GetServiceComponentFields_(oExamenFisico == null ? "" : oExamenFisico[0].ServicioComponentId, _ServiceId);

            if (objExamenFisico.Count != 0)
            {
                SearchControlAndLoadData(this, objExamenFisico[0].v_ServiceComponentId, objExamenFisico);
            }
        }

        private void txtTalla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtTalla.Text.Length; i++)
            {
                if (txtTalla.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }

            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtPeso.Text.Length; i++)
            {
                if (txtPeso.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }

            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtPerAbd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtPerAbd.Text.Length; i++)
            {
                if (txtPerAbd.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }

            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtPerCad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtPerCad.Text.Length; i++)
            {
                if (txtPerCad.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }

            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtTemperatura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtTemperatura.Text.Length; i++)
            {
                if (txtTemperatura.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }

            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtICC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtICC.Text.Length; i++)
            {
                if (txtICC.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }

            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtImc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtImc.Text.Length; i++)
            {
                if (txtImc.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }

            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtPAS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
        }

        private void txtPAD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
        }

        private void txtFrecCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
        }

        private void txtFrecResp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
        }

        private void txtSatOx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
        }

        private void txtPerAbd_TextChanged(object sender, EventArgs e)
        {
            if (txtPerAbd.Text != "" && txtPerCad.Text != "")
            {
                double PerAbd = double.Parse(txtPerAbd.Text.ToString());
                double PerCad = double.Parse(txtPerCad.Text.ToString());

                txtICC.Text = (PerAbd / PerCad).ToString("#.##");
            }
        }

        private void txtPerCad_TextChanged(object sender, EventArgs e)
        {
            if (txtPerAbd.Text != "" && txtPerCad.Text != "")
            {
                double PerAbd = double.Parse(txtPerAbd.Text.ToString());
                double PerCad = double.Parse(txtPerCad.Text.ToString());

                txtICC.Text = (PerAbd / PerCad).ToString("#.##");
            }
        }

        private void SearchControlAndLoadData(Control ctrlContainer, string ServiceComponentId, List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList> ListaValores)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                //if (ctrl is DropDownList)
                //{
                //    if (((DropDownList)ctrl).Attributes.GetValue("Tag") != null)
                //    {
                //        string ComponentFieldId = ((DropDownList)ctrl).Attributes.GetValue("Tag").ToString();
                //        ((DropDownList)ctrl).SelectedValue = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? "" : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1;
                //    }
                //}

                //if (ctrl is TextArea)
                //{
                //    if (((TextArea)ctrl).Attributes.GetValue("Tag") != null)
                //    {
                //        string ComponentFieldId = ((TextArea)ctrl).Attributes.GetValue("Tag").ToString();
                //        ((TextArea)ctrl).Text = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? "" : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1;

                //    }
                //}

                if (ctrl is TextBox)
                {
                    if (((TextBox)ctrl).Tag != null)
                    {
                        //AMC
                        string ComponentFieldId = ((TextBox)ctrl).Tag.ToString();
                        ((TextBox)ctrl).Text = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? "" : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1;

                    }
                }

                //if (ctrl is CheckBox)
                //{
                //    if (((CheckBox)ctrl).Attributes.GetValue("Tag") != null)
                //    {
                //        string ComponentFieldId = ((CheckBox)ctrl).Attributes.GetValue("Tag").ToString();
                //        ((CheckBox)ctrl).Checked = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? false : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                //    }
                //}

                if (ctrl.HasChildren)
                    SearchControlAndLoadData(ctrl, ServiceComponentId, ListaValores);

            }
        }

    }
}
