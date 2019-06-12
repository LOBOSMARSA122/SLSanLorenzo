using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.UI.Configuration;
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
        ServiceBL objServiceBL = new ServiceBL();
        public CalendarBL _calendarBL = new CalendarBL();
        public List<ServiceComponentList> _auxiliaryExams = null;    
        public string _serviceId;
        private string _modo;
        private string _protocolId;
        private string _type;
        private string _nroHospitalizacion;
        private string _dni;
        List<string> _ListaComponentes = null;
        private string lineId;
        List<Categoria> _DataSource = new List<Categoria>();
        #endregion

        #region Properties

        private string MedicalExamId { get; set; }
        private string MedicalExamName { get; set; }
        private string CategoryName { get; set; }
        private string ServiceComponentConcatId { get; set; }

        #endregion

        public frmAddExam(List<string> ListaComponentes, string modo, string protocolId, string type, string nroHospitalizacion, string dni, string serviceId, List<Categoria> DataSource)
        {
            _ListaComponentes = ListaComponentes;
            _dni = dni;
            _nroHospitalizacion = nroHospitalizacion;
            _modo = modo;
            _protocolId = protocolId;
            _type = type;
            _serviceId = serviceId;
            _DataSource = DataSource == null ? new List<Categoria>() : DataSource;
            InitializeComponent();

        }

        private void btnAgregarExamenAuxiliar_Click(object sender, EventArgs e)
        {
            AddAuxiliaryExam();   
        }

        private void AddAuxiliaryExam()
        {
            var findResult = lvExamenesSeleccionados.FindItemWithText(MedicalExamId);

            // El examen ya esta agregado
            if (findResult != null)
            {
                MessageBox.Show("Por favor seleccione otro examen.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var res = _ListaComponentes.Find(p => p == MedicalExamId);
            if (res != null)
            {

                var DialogResult = MessageBox.Show("El paciente ya cuenta con este examen, ¿Desea crear nuevo servicio?", "Error de Valicación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                               
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
                    //var DialogResult = DialogResult.OK;                    
                    //var frm = new frmCalendar(_nroHospitalizacion, _dni, _serviceId);
                    //frm.ShowDialog();
                }
                else
                {
                    return;
                }
                
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
            
            OperationResult objOperationResult = new OperationResult();
            if (_DataSource.Count > 0)
            {
                grdDataServiceComponent.DataSource = _DataSource;
                ultraGrid1.DataSource = _DataSource;
                groupBox1.Visible = false;
                cbLine.Visible = false;
                this.Height = 458;
                ultraGrid1.Height = 360;
                ultraGrid1.Location = new Point(3,3);
            }
            else
            {
                var ListServiceComponent = objServiceBL.GetAllComponents(ref objOperationResult, null, "");
                grdDataServiceComponent.DataSource = ListServiceComponent;
                ultraGrid1.DataSource = ListServiceComponent;
            }
            
            
            if (_modo == "HOSPI" || _modo == "ASEGU")
            {
                cboMedico.Enabled = true;
                Utils.LoadDropDownList(cboMedico, "Value1", "Id", BLL.Utils.GetProfessionalName(ref objOperationResult), DropDownListAction.Select);
                cboMedico.SelectedValue = "11";
                    if (_modo == "ASEGU")
                    {
                        #region Conexion SIGESOFT Obtener nombre del protocolo
                        ConexionSigesoft conectasam = new ConexionSigesoft();
                        conectasam.opensigesoft();
                        var cadena1 = "select PR.v_Name, OO.v_Name " +
                                  "from protocol PR " +
                                  "inner join [dbo].[plan] PL on PR.v_ProtocolId=PL.v_ProtocoloId " +
                                  "inner join organization OO on PL.v_OrganizationSeguroId=OO.v_OrganizationId " +
                                  "where v_ProtocolId='" + _protocolId + "' " +
                                  "group by PR.v_Name, OO.v_Name ";
                        SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                        SqlDataReader lector = comando.ExecuteReader();
                        string protocolName = "";
                        string aseguradoraName = "";
                        while (lector.Read())
                        {
                        protocolName = lector.GetValue(0).ToString();
                        aseguradoraName = lector.GetValue(1).ToString();
                        }
                        lector.Close();
                        //conectasam.closesigesoft();
                        lblPlan.Text = "Añadir plan de: " + aseguradoraName + "\n" + "Protocolo de Atención: " + protocolName;
                        if (lblPlan.Text.Length > 50) { lblPlan.Font = new Font("Microsoft Sans Serif", 6.25f); }
                        #endregion

                        cbLine.Select();
                        object listaLine = LlenarLines();
                        cbLine.DataSource = listaLine;
                        cbLine.DisplayMember = "v_Nombre";
                        cbLine.ValueMember = "v_IdLinea";
                        cbLine.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                        cbLine.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                        this.cbLine.DropDownWidth = 590;
                        cbLine.DisplayLayout.Bands[0].Columns[0].Width = 20;
                        cbLine.DisplayLayout.Bands[0].Columns[1].Width = 335;
                        #region Colocar el Plan en el combo y bloquearlo
                        cadena1 = "select SR.i_PlanId, PL.v_IdUnidadProductiva, LN.v_Nombre " +
                                  "from service SR " +
                                  "inner join [dbo].[plan] PL on SR.v_ProtocolId=PL.v_ProtocoloId and SR.i_PlanId=PL.i_PlanId " +
                                  "inner join [20505310072].[dbo].[linea] LN on PL.v_IdUnidadProductiva= LN.v_IdLinea " +
                                  "where v_ServiceId = '" + _serviceId + "' and SR.i_PlanId <> ''";
                        comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                        lector = comando.ExecuteReader();
                        string i_PlanId, v_IdUnidadProductiva, v_Nombre = "";
                        bool resultPlan = false;
                        while (lector.Read())
                        {
                            i_PlanId = lector.GetValue(0).ToString();
                            v_IdUnidadProductiva = lector.GetValue(1).ToString();
                            v_Nombre = lector.GetValue(2).ToString();
                            resultPlan = true;
                        }
                        lector.Close();
                        conectasam.closesigesoft();
                        if (resultPlan)
                        {
                            cbLine.Text = v_Nombre;
                            cbLine.Enabled = false;
                        }
                        #endregion
                    }
                    else
                    {
                        gbExamenesSeleccionados.Size = new Size(337, 430);
                        gbExamenesSeleccionados.Location = new Point(626, 12);
                        gbTipoAtencion.Visible = false;
                        lblPlan.Visible = false;
                        cbLine.Visible = false;
                    }
            }
            else
            {
                Utils.LoadDropDownList(cboMedico, "Value1", "Id", BLL.Utils.GetProfessionalName(ref objOperationResult), DropDownListAction.Select);
                cboMedico.SelectedValue = "11";
                cboMedico.Enabled = false;
            }
        }

        private object LlenarLines()
        {
            #region Conexion SAMBHS
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadenasam = "select LN.v_Nombre as v_Nombre ,PL.v_IdUnidadProductiva as  v_IdLinea " +
                            "from [dbo].[plan] PL " +
                            "inner join protocol PR on PL.v_ProtocoloId=PR.v_ProtocolId " +
                            "inner join [20505310072].[dbo].[linea] LN on PL.v_IdUnidadProductiva=LN.v_IdLinea " +
                            "where PR.v_ProtocolId='" + _protocolId + "'";
            var comando = new SqlCommand(cadenasam, connection: conectasam.conectarsigesoft);
            var lector = comando.ExecuteReader();
            string preciounitario = "";
            List<ListaLineas> objListaLineas = new List<ListaLineas>();

            while (lector.Read())
            {
                ListaLineas Lista = new ListaLineas();
                Lista.v_Nombre = lector.GetValue(0).ToString();
                Lista.v_IdLinea = lector.GetValue(1).ToString();
                objListaLineas.Add(Lista);
            }
            lector.Close();
            conectasam.closesigesoft();
            #endregion

            return objListaLineas;
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
                var ComponentId = fields[1].Text.Split('|');
                var NombreComponente = fields[0].Text.Split('|');

                

                MedicalExamBL objComponentBL = new MedicalExamBL();
                componentDto objComponentDto = new componentDto();

                OperationResult objOperationResult = new OperationResult();
                foreach (var scid in ComponentId)
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
                        #region OLD Logica antigua
                            #region Conexion SAM
                            //ConexionSigesoft conectasam = new ConexionSigesoft();
                            //conectasam.opensigesoft();
                            #endregion
                            #region Query
                            //var componente = NombreComponente[0].ToString();
                            //var cadena1 = "select PL.i_EsDeducible, PL.i_EsCoaseguro, PL.d_Importe, PL.d_ImporteCo from [dbo].[plan] PL where PL.v_IdUnidadProductiva='" + lineId + "' and PL.v_ProtocoloId='" + _protocolId + "' ";
                            //SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                            //SqlDataReader lector = comando.ExecuteReader();
                            //int deducible = 0;
                            //int coaseguro = 0;
                            //decimal? importe = 0;
                            //decimal? importeCo = 0;
                            //while (lector.Read())
                            //{
                            //    deducible = int.Parse(lector.GetValue(0).ToString()); coaseguro = int.Parse(lector.GetValue(1).ToString()); importe = decimal.Parse(lector.GetValue(2).ToString()); importeCo = decimal.Parse(lector.GetValue(3).ToString());
                            //}
                            //lector.Close();
                            //string factores = ""; string aseguradoraName = ""; string organizationId = "";
                            //var factorGlobal = "";
                            //var cadena2 = "select PR.r_PriceFactor, OO.v_Name, PR.v_CustomerOrganizationId from Organization OO inner join protocol PR On PR.v_AseguradoraOrganizationId = OO.v_OrganizationId where PR.v_ProtocolId ='" + _protocolId + "'";
                            //comando = new SqlCommand(cadena2, connection: conectasam.conectarsigesoft);
                            //lector = comando.ExecuteReader();
                            //while (lector.Read())
                            //{
                            //    factores = lector.GetValue(0).ToString();
                            //    var factorArray = factores.Split('|');// factores[0].ToString().Split('|');
                            //    factorGlobal = factorArray[0];
                            //    aseguradoraName = lector.GetValue(1).ToString();
                            //    organizationId = lector.GetValue(2).ToString();
                            //}
                            //lector.Close();
                            //string empresa = "";
                            //var cadena3 = "select v_Name from Organization OO  where OO.v_OrganizationId ='" + organizationId + "'";
                            //comando = new SqlCommand(cadena3, connection: conectasam.conectarsigesoft);
                            //lector = comando.ExecuteReader();
                            //while (lector.Read())
                            //{
                            //    empresa = lector.GetValue(0).ToString();
                            //}
                            //lector.Close();
                            #endregion
                            #region Lógica PARA SABER SI ES DEDUCIBLE O COASEGURO
                            //if (rbNuevaConsulta.Checked)// QUIERE DECIR QUE ES UNA NUEVA ATENCION Y DEBE SER CONSIDERADO COMO DEDUCIBLE SIN FACTOR
                            //{
                            //    factorGlobal = "1";
                            //    coaseguro = 0;
                            //    importeCo = null;
                            //}
                            //else if (rbAdicional.Checked) // QUIERE DECIR QUE ES UN COMPONENTE ADICIONAL Y DEBE SER CONSIDERADO COMO COASEGURO CON FACTOR
                            //{
                            //    deducible = 0;
                            //    importe = null;
                            //}
                            #endregion
                            #region Formulario
                            //precio_base = (float)objComponentDto.r_PriceSegus;// se cambia el precio inicial por el SEGUS
                            //frmConfigSeguros frm1 = new frmConfigSeguros(deducible, coaseguro, importe, precio_base.ToString(), factorGlobal, importeCo);
                            //frm1.Text = aseguradoraName + " / " + empresa;
                            //frm1.ShowDialog();
                            #endregion
                        #endregion

                        #region Obteniendo los campos de la BD
                        ConexionSigesoft conectasam = new ConexionSigesoft();
                        conectasam.opensigesoft();
                        var componente = NombreComponente[0].ToString();
                        var cadena = "select i_KindOfService from  component where v_ComponentId='" + objComponentDto.v_ComponentId + "'";
                        SqlCommand comando = new SqlCommand(cadena, connection: conectasam.conectarsigesoft);
                        SqlDataReader lector = comando.ExecuteReader();
                        int i_KindOfService = 0;
                        while (lector.Read())
                        {
                            try
                            {
                                i_KindOfService = int.Parse(lector.GetValue(0).ToString());
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show(exception.Message, " ¡ ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                        }
                        lector.Close();
                        var cadena1 = "select PL.i_PlanId, PL.i_EsCoaseguro, PL.d_Importe, PL.d_ImporteCo " +
                                                    "from [dbo].[plan] PL " +
                                                    "where PL.v_IdUnidadProductiva='" + lineId + "' and PL.v_ProtocoloId='" + _protocolId + "' ";
                        comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                        lector = comando.ExecuteReader();
                        string PlanId = ""; int coaseguro = 0; decimal? importe = 0; decimal? importeCo = 0;
                        while (lector.Read())
                        {
                            try
                            {
                                PlanId = lector.GetValue(0).ToString();
                                coaseguro = int.Parse(lector.GetValue(1).ToString());
                                importe = decimal.Parse(lector.GetValue(2).ToString());
                                importeCo = decimal.Parse(lector.GetValue(3).ToString());
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show(exception.Message, " ¡ ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                        }
                        lector.Close();
                        string factores = ""; string aseguradoraName = ""; string organizationId = ""; var factorGlobal = "";
                        var cadena2 = "select PR.r_PriceFactor, OO.v_Name, PR.v_CustomerOrganizationId " +
                                      "from Organization OO " +
                                      "inner join protocol PR On PR.v_AseguradoraOrganizationId = OO.v_OrganizationId " +
                                      "where PR.v_ProtocolId ='" + _protocolId + "'";
                        comando = new SqlCommand(cadena2, connection: conectasam.conectarsigesoft);
                        lector = comando.ExecuteReader();
                        while (lector.Read())
                        {
                            try
                            {
                                factores = lector.GetValue(0).ToString();
                                var factorArray = factores.Split('|');// factores[0].ToString().Split('|');
                                factorGlobal = factorArray[0];
                                aseguradoraName = lector.GetValue(1).ToString();
                                organizationId = lector.GetValue(2).ToString();
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show(exception.Message, " ¡ ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                        }
                        lector.Close();
                        #endregion

                        #region Según el tipo de componente se hace el calculo
                        switch (i_KindOfService)
                        {
                            //CLINICA
                            case 1:
                                {
                                    if (objComponentDto.r_PriceSegus != 0)
                                    {
                                        objServiceComponentDto.r_Price = objComponentDto.r_PriceSegus;
                                        objServiceComponentDto.d_SaldoPaciente = importe;
                                        objServiceComponentDto.d_SaldoAseguradora = (decimal)objComponentDto.r_PriceSegus - importe;
                                    }
                                    else
                                    {
                                        frmConfigSeguros frm1 = new frmConfigSeguros("1");
                                        frm1.ShowDialog();
                                        objServiceComponentDto.r_Price = (float) frm1.nuevoPrecio;
                                        objServiceComponentDto.d_SaldoPaciente = importe;
                                        objServiceComponentDto.d_SaldoAseguradora = frm1.nuevoPrecio - importe;
                                    }
                                    
                                }
                                break;
                            //SERVICIOS AUXILIARES
                            case 2:
                                {
                                    if (objComponentDto.r_PriceSegus != 0)
                                    {
                                        objServiceComponentDto.r_Price = objComponentDto.r_PriceSegus * float.Parse(factorGlobal);
                                        objServiceComponentDto.d_SaldoPaciente = importeCo * (decimal)objServiceComponentDto.r_Price / 100;
                                        objServiceComponentDto.d_SaldoAseguradora = (100 - importeCo) * (decimal)objServiceComponentDto.r_Price / 100;
                                    }
                                    else
                                    {
                                        frmConfigSeguros frm1 = new frmConfigSeguros(factorGlobal);
                                        frm1.ShowDialog();
                                        objServiceComponentDto.r_Price = (float) frm1.nuevoPrecio ;
                                        objServiceComponentDto.d_SaldoPaciente = importeCo * frm1.nuevoPrecio / 100;
                                        objServiceComponentDto.d_SaldoAseguradora = (100 - importeCo) * frm1.nuevoPrecio / 100;
                                    }
                                   
                                }
                                break;
                            //HONORARIOS MÉDICOS Y/O QUIRURGICOS
                            case 3:
                                {
                                    if (objComponentDto.r_PriceSegus != 0)
                                    {
                                        objServiceComponentDto.r_Price = objComponentDto.r_PriceSegus * float.Parse(factorGlobal);
                                        objServiceComponentDto.d_SaldoPaciente = importeCo * (decimal)objServiceComponentDto.r_Price / 100;
                                        objServiceComponentDto.d_SaldoAseguradora = (100 - importeCo) * (decimal)objServiceComponentDto.r_Price / 100;
                                    }
                                    else
                                    {
                                        frmConfigSeguros frm1 = new frmConfigSeguros(factorGlobal);
                                        frm1.ShowDialog();
                                        objServiceComponentDto.r_Price = (float) frm1.nuevoPrecio;
                                        objServiceComponentDto.d_SaldoPaciente = importeCo * frm1.nuevoPrecio / 100;
                                        objServiceComponentDto.d_SaldoAseguradora = (100 - importeCo) * frm1.nuevoPrecio / 100;
                                    }
                                    
                                }
                                break;
                        }
                        #endregion
                        
                        
                        
                            objServiceComponentDto.v_ServiceId = _serviceId;
                            objServiceComponentDto.i_ExternalInternalId = (int)Common.ComponenteProcedencia.Interno;
                            objServiceComponentDto.i_ServiceComponentTypeId = 1;
                            objServiceComponentDto.i_IsVisibleId = 1;
                            objServiceComponentDto.i_IsInheritedId = (int)Common.SiNo.NO;
                            objServiceComponentDto.d_StartDate = null;
                            objServiceComponentDto.d_EndDate = null;
                            objServiceComponentDto.i_index = 1;
                            objServiceComponentDto.v_ComponentId = scid;
                            objServiceComponentDto.i_IsInvoicedId = (int)Common.SiNo.NO;
                            objServiceComponentDto.i_ServiceComponentStatusId = (int)Common.ServiceStatus.PorIniciar;
                            objServiceComponentDto.i_QueueStatusId = (int)Common.QueueStatusId.LIBRE;
                            objServiceComponentDto.i_Iscalling = (int)Common.Flag_Call.NoseLlamo;
                            objServiceComponentDto.i_Iscalling_1 = (int)Common.Flag_Call.NoseLlamo;
                            objServiceComponentDto.i_IsManuallyAddedId = (int)Common.SiNo.NO;
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                            objServiceComponentDto.v_IdUnidadProductiva = txtUnidProdId.Text;
                            objServiceComponentDto.i_MedicoTratanteId = int.Parse(cboMedico.SelectedValue.ToString());
                            if (rbNuevaConsulta.Checked)
                            {
                                objServiceComponentDto.i_TipoDesc = 1;
                            }
                            else if (rbAdicional.Checked)
                            {
                                objServiceComponentDto.i_TipoDesc = 2;
                            }
                                                        
                            _ObjServiceBL.AddServiceComponent(ref objOperationResult, objServiceComponentDto, Globals.ClientSession.GetAsList());
                            #region Update a service agrega el PlanId
                            cadena1 = "update service set " +
                                      "i_PlanId = '" + PlanId + "' " +
                                      "where v_ServiceId = '" + _serviceId + "' ";
                            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                            lector = comando.ExecuteReader();
                            lector.Close();
                            #endregion
                        
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
             
                //Actualizo si son examenes adicionales
                if (_DataSource.Count > 0)
                {
                    new AdditionalExamBL().UpdateAdditionalExamByComponentIdAndServiceId(ComponentId[0], _serviceId,
                        Globals.ClientSession.i_SystemUserId);
                }
            }
          
            MessageBox.Show("Se grabo correctamente", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void ultraGrid1_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            bool row = ultraGrid1.Selected.Rows.Count > 0;
            if (!row)
            {
                return;
            }
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

        private void cbLine_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e)
        {
            #region Conexion SAM obtener id de linea
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena = "select v_IdLinea from [dbo].[linea] where v_Nombre='" + cbLine.Text + "' and i_Eliminado=0";
            SqlCommand comandou = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
            SqlDataReader lectoru = comandou.ExecuteReader();
            lineId = "";
            while (lectoru.Read())
            {
                lineId = lectoru.GetValue(0).ToString();
            }
            lectoru.Close();
            conectasam.closeSambhs();
            #endregion

            txtUnidProdId.Text = lineId;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            FilterComponents();
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                FilterComponents();
            }
        }


        private void FilterComponents()
        {
            OperationResult objOperationResult = new OperationResult();
            int? busqueda = null;
            if (rbNombreCategoria.Checked)
            {
                busqueda = (int)TipoBusqueda.NombreCategoria;
            }
            else if (rbNombreSubCategoria.Checked)
            {
                busqueda = (int)TipoBusqueda.NombreSubCategoria;
            }
            else if (rbNombreComponente.Checked)
            {
                busqueda = (int)TipoBusqueda.NombreComponent;
            }
            else if (rbPorCodigoSegus.Checked)
            {
                busqueda = (int)TipoBusqueda.CodigoSegus;
            }

            var ListServiceComponent = objServiceBL.GetAllComponents(ref objOperationResult, busqueda, txtFiltro.Text);
            grdDataServiceComponent.DataSource = ListServiceComponent;
            ultraGrid1.DataSource = ListServiceComponent;
        }

        private void cbLine_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }
    }
}
