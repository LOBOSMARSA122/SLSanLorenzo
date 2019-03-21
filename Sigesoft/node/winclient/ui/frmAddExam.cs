using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.UI.Hospitalizacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAddExam : Form
    {
        #region Declarations
        public CalendarBL _calendarBL = new CalendarBL();
        public List<ServiceComponentList> _auxiliaryExams = null;    
        public string _serviceId;
        private string _modo;
        private string _protocolId;
        private string _type;
        private string _nroHospitalizacion;
        private string _dni;
        List<string> _ListaComponentes = null;
        #endregion

        #region Properties

        private string MedicalExamId { get; set; }
        private string MedicalExamName { get; set; }
        private string CategoryName { get; set; }
        private string ServiceComponentConcatId { get; set; }

        #endregion

        public frmAddExam(List<string> ListaComponentes, string modo, string protocolId, string type, string nroHospitalizacion, string dni, string serviceId)
        {
            _ListaComponentes = ListaComponentes;
            _dni = dni;
            _nroHospitalizacion = nroHospitalizacion;
            _modo = modo;
            _protocolId = protocolId;
            _type = type;
            _serviceId = serviceId;
            InitializeComponent();

        }

        private void btnAgregarExamenAuxiliar_Click(object sender, EventArgs e)
        {
            AddAuxiliaryExam();   
        }

        private void AddAuxiliaryExam()
        {
            var findResult = lvExamenesSeleccionados.FindItemWithText(MedicalExamId);

            var res = _ListaComponentes.Find(p => p == MedicalExamId);
            if (res != null)
            {
                this.DialogResult = MessageBox.Show("Este examen ya se encuentra agregado, ¿Desea crear nuevo servicio?", "Error de validación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                               
                if (DialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    #region Agenda Automática
                    CalendarBL calendarBl =  new CalendarBL();
                    OperationResult objOperationResult = new OperationResult();

                    var protocolId = Constants.Prot_Hospi_Adic;
                    
                    var objCalendarDto = new calendarDto();
                    objCalendarDto.v_PersonId = new PacientBL().GetPersonByNroDocument(ref objOperationResult,_dni).v_PersonId;// item.PersonId;
                    objCalendarDto.d_DateTimeCalendar = DateTime.Now;
                    objCalendarDto.d_CircuitStartDate = DateTime.Now;
                    objCalendarDto.d_EntryTimeCM = DateTime.Now;
                    objCalendarDto.i_ServiceTypeId = (int)ServiceType.Particular;
                    objCalendarDto.i_ServiceId = (int)MasterService.Hospitalizacion;
                    
                    objCalendarDto.i_CalendarStatusId = (int)CalendarStatus.Agendado;
                    objCalendarDto.i_LineStatusId = (int) LineStatus.EnCircuito; 
                    objCalendarDto.v_ProtocolId = protocolId;
                    objCalendarDto.i_NewContinuationId = 1;
                    objCalendarDto.i_LineStatusId = (int)LineStatus.EnCircuito;
                    objCalendarDto.i_IsVipId = (int)SiNo.NO;

                    var serviceId =  calendarBl.AddShedule(ref objOperationResult, objCalendarDto, Globals.ClientSession.GetAsList(), protocolId, objCalendarDto.v_PersonId, (int)MasterService.Eso, "Nuevo");
                    
                    serviceDto objServiceDto = new serviceDto();
                    objServiceDto = new ServiceBL().GetService(ref objOperationResult, serviceId);
                    objServiceDto.d_ServiceDate = DateTime.Now;
                    
                    objServiceDto.i_ServiceStatusId = (int)Common.ServiceStatus.Iniciado;
                    new ServiceBL().UpdateService(ref objOperationResult, objServiceDto, Globals.ClientSession.GetAsList());


                    var servicesComponents = new ServiceBL().GetServiceComponents(ref objOperationResult,serviceId);

                    foreach (var servicesComponent in servicesComponents)
                    {
                        servicecomponentDto  oservicecomponentDto  = new servicecomponentDto();
                        oservicecomponentDto = new ServiceBL().GetServiceComponent(ref objOperationResult,
                            servicesComponent.v_ServiceComponentId);
                        oservicecomponentDto.i_MedicoTratanteId = 11;
                        oservicecomponentDto.i_IsVisibleId = 1;
                        oservicecomponentDto.v_ServiceComponentId = servicesComponent.v_ServiceComponentId;
                        new ServiceBL().UpdateServiceComponent(ref objOperationResult, oservicecomponentDto, Globals.ClientSession.GetAsList());
                    }
                    



                    var oHospitalizacionserviceDto = new hospitalizacionserviceDto();

                    oHospitalizacionserviceDto.v_HopitalizacionId = _nroHospitalizacion;
                    oHospitalizacionserviceDto.v_ServiceId = serviceId;

                    new HospitalizacionBL().AddHospitalizacionService(ref objOperationResult, oHospitalizacionserviceDto, Globals.ClientSession.GetAsList());
                    #endregion

                    MessageBox.Show("Se generó el servicio: " + serviceId, " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;                    
                    //var frm = new frmCalendar(_nroHospitalizacion, _dni, _serviceId);
                    //frm.ShowDialog();
                }
                
            }
            // El examen ya esta agregado
            if (findResult != null)
            {
                MessageBox.Show("Por favor seleccione otro examen.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = new ListViewItem(new[] { MedicalExamName, MedicalExamId, ServiceComponentConcatId });

            lvExamenesSeleccionados.Items.Add(row);

            gbExamenesSeleccionados.Text = string.Format("Examenes Seleccionados {0}", lvExamenesSeleccionados.Items.Count);
        }

        private void grdDataServiceComponent_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnAgregarExamenAuxiliar.Enabled = (grdDataServiceComponent.Selected.Rows.Count > 0);
            lvExamenesSeleccionados.SelectedItems.Clear();

            if (grdDataServiceComponent.Selected.Rows.Count == 0)
                return;

            MedicalExamId = grdDataServiceComponent.Selected.Rows[0].Cells["v_ComponentId"].Value.ToString();
            MedicalExamName = grdDataServiceComponent.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            ServiceComponentConcatId = grdDataServiceComponent.Selected.Rows[0].Cells["v_ServiceComponentConcatId"].Value.ToString();

            if (grdDataServiceComponent.Selected.Rows[0].Cells["v_CategoryName"].Value != null)
            {
                CategoryName = grdDataServiceComponent.Selected.Rows[0].Cells["v_CategoryName"].Value.ToString();
            }
            else
            {
                CategoryName = string.Empty;
            }

          

        }

        private void btnRemoverExamenAuxiliar_Click(object sender, EventArgs e)
        {
            var selectedItem = lvExamenesSeleccionados.SelectedItems[0];
            var medicalExamId = selectedItem.SubItems[1].Text;

            // Eliminacion fisica
            lvExamenesSeleccionados.Items.Remove(selectedItem);
            gbExamenesSeleccionados.Text = string.Format("Examenes Seleccionados {0}", lvExamenesSeleccionados.Items.Count);

           

        }

        private void frmAddAdditionalExam_Load(object sender, EventArgs e)
        {
            ServiceBL objServiceBL = new ServiceBL();
            OperationResult objOperationResult = new OperationResult();

            var ListServiceComponent = objServiceBL.GetAllComponents(ref objOperationResult);
            grdDataServiceComponent.DataSource = ListServiceComponent;
            ultraGrid1.DataSource = ListServiceComponent;
            if (_modo == "HOSPI" || _modo == "ASEGU")
            {
                cboMedico.Enabled = true;
                Utils.LoadDropDownList(cboMedico, "Value1", "Id", BLL.Utils.GetProfessionalName(ref objOperationResult), DropDownListAction.Select);
                cboMedico.SelectedValue = "11";
            }
            else
            {
                Utils.LoadDropDownList(cboMedico, "Value1", "Id", BLL.Utils.GetProfessionalName(ref objOperationResult), DropDownListAction.Select);
                cboMedico.SelectedValue = "11";
                cboMedico.Enabled = false;
            }
        }

        private void lvExamenesSeleccionados_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            btnRemoverExamenAuxiliar.Enabled = (lvExamenesSeleccionados.SelectedItems.Count > 0);
        }

        private void grdDataServiceComponent_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            AddAuxiliaryExam();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cboMedico.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Seleccionar un médico tratante", " ¡ VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (_auxiliaryExams == null)
                _auxiliaryExams = new List<ServiceComponentList>();

            // Save ListView / recorrer la lista de examenes seleccionados
            foreach (ListViewItem item in lvExamenesSeleccionados.Items)
            {
                var fields = item.SubItems;
                var serviceComponentConcatId = fields[1].Text.Split('|');
                var NombreComponente = fields[0].Text.Split('|');
                MedicalExamBL objComponentBL = new MedicalExamBL();
                componentDto objComponentDto = new componentDto();

                OperationResult objOperationResult = new OperationResult();
                foreach (var scid in serviceComponentConcatId)
                {
                    var conCargoA = -1;
                    if (_type == "Hospi")
                    {
                        var oFrmType = new frmType();
                        oFrmType.ShowDialog();

                        if (oFrmType._conCargoA == "Médico")
                        {
                            conCargoA = 1;
                        }
                        else
                        {
                            conCargoA = 2;
                        }
                    }

                    objComponentDto = objComponentBL.GetMedicalExam(ref objOperationResult, scid);
                    SystemParameterBL oSp = new SystemParameterBL();
                    var o = oSp.GetSystemParameter(ref objOperationResult, 116, int.Parse(objComponentDto.i_CategoryId.ToString()));
                    //Lógica de Aumento de Precio Base

                    var porcentajes = o.v_Field.Split('-');

                    float p1 = porcentajes[0] == null ? 0 : float.Parse(porcentajes[0].ToString());

                    float p2 = porcentajes[1] == null ? 0 : float.Parse(porcentajes[1].ToString());

                    float pb = objComponentDto.r_BasePrice.Value;
                    var precio_base = pb + (pb * p1 / 100) + (pb * p2 / 100);
                    //FormPrecioComponente frm = new FormPrecioComponente("", "", "");
                    //frmConfigSeguros frm1 = new frmConfigSeguros(0, 0, 0, "", "");
                    ServiceComponentList auxiliaryExam = new ServiceComponentList();
                    servicecomponentDto objServiceComponentDto = new servicecomponentDto();
                    ServiceBL _ObjServiceBL = new ServiceBL();
                    TicketBL oTicketBL = new TicketBL();
                    if (_modo == "ASEGU")
                    {
                        #region Conexion SAM
                        ConexionSigesoft conectasam = new ConexionSigesoft();
                        conectasam.opensigesoft();
                        #endregion
                        #region Query
                        var componente = NombreComponente[0].ToString();
                        var cadena1 = "select v_ComponentId from component where v_Name='" + componente + "'";
                        SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                        SqlDataReader lector = comando.ExecuteReader();
                        while (lector.Read())
                        {
                            componente = lector.GetValue(0).ToString();
                        }
                        lector.Close();
                        var protocol = _protocolId;
                        var cadena = "select PL.i_EsDeducible as Deducible, PL.i_EsCoaseguro as Coaseguro, PL.d_Importe as Importe from [dbo].[plan] PL inner join component CP ON CP.v_IdUnidadProductiva =  PL.v_IdUnidadProductiva inner join protocol PT ON PT.v_ProtocolId = PL.v_ProtocoloId where PL.v_ProtocoloId='" + protocol + "' and CP.v_ComponentId='" + componente + "'";
                        comando = new SqlCommand(cadena, connection: conectasam.conectarsigesoft);
                        lector = comando.ExecuteReader();
                        int deducible = 0;
                        int coaseguro = 0;
                        decimal importe = 0;
                        while (lector.Read())
                        {
                            deducible = int.Parse(lector.GetValue(0).ToString()); coaseguro = int.Parse(lector.GetValue(1).ToString()); importe = decimal.Parse(lector.GetValue(2).ToString());
                        }
                        lector.Close();
                        string factores = ""; string aseguradoraName = ""; string organizationId = "";
                        var factorGlobal = "";
                        var cadena2 = "select OO.r_Factor, OO.v_Name, PR.v_CustomerOrganizationId from Organization OO inner join protocol PR On PR.v_AseguradoraOrganizationId = OO.v_OrganizationId where PR.v_ProtocolId ='" + protocol + "'";
                        comando = new SqlCommand(cadena2, connection: conectasam.conectarsigesoft);
                        lector = comando.ExecuteReader();
                        while (lector.Read())
                        {
                            factores = lector.GetValue(0).ToString();
                            var factorArray = factores.Split('|');// factores[0].ToString().Split('|');
                            factorGlobal = factorArray[0];
                            aseguradoraName = lector.GetValue(1).ToString();
                            organizationId = lector.GetValue(2).ToString();
                        }
                        lector.Close();
                        string empresa = "";
                        var cadena3 = "select v_Name from Organization OO  where OO.v_OrganizationId ='" + organizationId + "'";
                        comando = new SqlCommand(cadena3, connection: conectasam.conectarsigesoft);
                        lector = comando.ExecuteReader();
                        while (lector.Read())
                        {
                            empresa = lector.GetValue(0).ToString();
                        }
                        lector.Close();
                        #endregion
                        frmConfigSeguros frm1 = new frmConfigSeguros(deducible, coaseguro, importe, precio_base.ToString(), factorGlobal);
                        frm1.Text = aseguradoraName + " / " + empresa;
                        frm1.ShowDialog();
                        if (frm1.result == false)
                        {
                            return;
                        }
                        else
                        {
                            objServiceComponentDto.v_ServiceId = _serviceId;
                            objServiceComponentDto.i_ExternalInternalId = (int)Common.ComponenteProcedencia.Interno;
                            objServiceComponentDto.i_ServiceComponentTypeId = 1;
                            objServiceComponentDto.i_IsVisibleId = 1;
                            objServiceComponentDto.i_IsInheritedId = (int)Common.SiNo.NO;
                            objServiceComponentDto.d_StartDate = null;
                            objServiceComponentDto.d_EndDate = null;
                            objServiceComponentDto.i_index = 1;
                            objServiceComponentDto.r_Price = frm1.precio;
                            objServiceComponentDto.v_ComponentId = scid;
                            objServiceComponentDto.i_IsInvoicedId = (int)Common.SiNo.NO;
                            objServiceComponentDto.i_ServiceComponentStatusId = (int)Common.ServiceStatus.PorIniciar;
                            objServiceComponentDto.i_QueueStatusId = (int)Common.QueueStatusId.LIBRE;
                            objServiceComponentDto.i_Iscalling = (int)Common.Flag_Call.NoseLlamo;
                            objServiceComponentDto.i_Iscalling_1 = (int)Common.Flag_Call.NoseLlamo;
                            objServiceComponentDto.i_IsManuallyAddedId = (int)Common.SiNo.NO;
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                            objServiceComponentDto.v_IdUnidadProductiva = objComponentDto.v_IdUnidadProductiva;
                            objServiceComponentDto.i_MedicoTratanteId = int.Parse(cboMedico.SelectedValue.ToString());
                            objServiceComponentDto.d_SaldoPaciente = frm1.paciente;
                            objServiceComponentDto.d_SaldoAseguradora = frm1.aseguradora;
                            _ObjServiceBL.AddServiceComponent(ref objOperationResult, objServiceComponentDto, Globals.ClientSession.GetAsList());
                        }
                    }
                    else 
                    {
                        FormPrecioComponente frm = new FormPrecioComponente(NombreComponente[0].ToString(), precio_base.ToString(), "");
                        frm.ShowDialog();
                        objServiceComponentDto.i_ConCargoA = conCargoA;
                        objServiceComponentDto.v_ServiceId = _serviceId;
                        objServiceComponentDto.i_ExternalInternalId = (int)Common.ComponenteProcedencia.Interno;
                        objServiceComponentDto.i_ServiceComponentTypeId = 1;
                        objServiceComponentDto.i_IsVisibleId = 1;
                        objServiceComponentDto.i_IsInheritedId = (int)Common.SiNo.NO;
                        objServiceComponentDto.d_StartDate = null;
                        objServiceComponentDto.d_EndDate = null;
                        objServiceComponentDto.i_index = 1;
                        objServiceComponentDto.r_Price = frm.Precio;
                        objServiceComponentDto.v_ComponentId = scid;
                        objServiceComponentDto.i_IsInvoicedId = (int)Common.SiNo.NO;
                        objServiceComponentDto.i_ServiceComponentStatusId = (int)Common.ServiceStatus.PorIniciar;
                        objServiceComponentDto.i_QueueStatusId = (int)Common.QueueStatusId.LIBRE;
                        //objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        objServiceComponentDto.i_Iscalling = (int)Common.Flag_Call.NoseLlamo;
                        objServiceComponentDto.i_Iscalling_1 = (int)Common.Flag_Call.NoseLlamo;
                        objServiceComponentDto.i_IsManuallyAddedId = (int)Common.SiNo.NO;
                        objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        objServiceComponentDto.v_IdUnidadProductiva = objComponentDto.v_IdUnidadProductiva;
                        objServiceComponentDto.i_MedicoTratanteId = int.Parse(cboMedico.SelectedValue.ToString());
                        objServiceComponentDto.d_SaldoPaciente = 0;
                        objServiceComponentDto.d_SaldoAseguradora = 0;
                        _ObjServiceBL.AddServiceComponent(ref objOperationResult, objServiceComponentDto, Globals.ClientSession.GetAsList());
                    }
                }             
            }
          
            MessageBox.Show("Se grabo correctamente", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void ultraGrid1_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {

            if (ultraGrid1.Selected.Rows[0].Cells["v_ComponentId"].Value == null)
            {
                btnAgregarExamenAuxiliar.Enabled =false;

                return;
            }
            else
            {
                btnAgregarExamenAuxiliar.Enabled = true;
            }

            lvExamenesSeleccionados.SelectedItems.Clear();

            if (ultraGrid1.Selected.Rows.Count == 0)
                return;

            MedicalExamId = ultraGrid1.Selected.Rows[0].Cells["v_ComponentId"].Value.ToString();
            MedicalExamName = ultraGrid1.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            //ServiceComponentConcatId = ultraGrid1.Selected.Rows[0].Cells["v_ServiceComponentId"].Value.ToString();

            if (ultraGrid1.Selected.Rows[0].Cells["v_ComponentId"].Value != null)
            {
                MedicalExamName = ultraGrid1.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            }
            else
            {
                MedicalExamName = string.Empty;
            }
        }
        
    }
}
