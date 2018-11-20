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
using Infragistics.Win.UltraWinGrid;


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmOffice : Form
    {
        #region Declarations

        string _Piso;
        string _serviceId;
        string _componentId;
        string _componentName;
        string _IsCall;
        int _Flag = 0;
        int _TserviceId;
        string _CalendarId;
        List<string> _ServiceComponentId = new List<string>();
        byte[] _personImage;
        string _personName;
        int _categoryId;
        private Sigesoft.Node.WinClient.UI.Utils.CustomizedToolTip _customizedToolTip = null;
        private string _serviceStatusId;
        ServiceBL _serviceBL = new ServiceBL();
        PacientBL _pacientBL = new PacientBL();
        private string _personId;
        private int _categoriaId;
        private string _ProtocolId;
        CalendarBL _objCalendarBL = new CalendarBL();
        List<CalendarList> _objCalendarListAMC = new List<CalendarList>();
        string serviceIdGrillaLlamandoPaciente = "";
        List<string> ListaComponentes = new List<string>();
        string _strServicelId;
        #endregion

        public List<string> _componentIds { get; set; }

        public frmOffice(string pstrComponentName, int pintCategoria)
        {
            InitializeComponent();
            //timer1.Interval = 200;
            _componentId = "";
            _categoriaId = pintCategoria;
            _componentName = pstrComponentName;

        }

        private void frmOffice_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
             //Utils.LoadDropDownList(cbOficina, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);

            Utils.LoadDropDownList(cbServiceType, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, -1, null), DropDownListAction.All);
            // combo servicio
            Utils.LoadDropDownList(cbService, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, -1, null), DropDownListAction.All);

            cbServiceType.SelectedValue = ((int)ServiceType.Empresarial).ToString();
            cbService.SelectedValue = ((int)MasterService.Eso).ToString();
          
            using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
            {
                _customizedToolTip = new Sigesoft.Node.WinClient.UI.Utils.CustomizedToolTip(grdDataServiceComponent);
                btnRefresh_Click(sender, e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //label10.Visible = !label10.Visible;
        }

        private void Llamar()
        {
            OperationResult objOperationResult = new OperationResult();
            List<ServiceComponentList> ListServiceComponent = new List<ServiceComponentList>();
            CalendarBL objCalendarBL = new CalendarBL();
            CalendarList objCalendar = new CalendarList();
            List<CalendarList> objCalendarList = new List<CalendarList>();
            ProtocolBL oProtocolBL = new ProtocolBL();

            ServiceBL objServiceBL = new ServiceBL();
            servicecomponentDto objservicecomponentDto = new servicecomponentDto();

            _ServiceComponentId = new List<string>();
            // Verificar si un componente está en la categoría
            MedicalExamBL oMedicalExamBL = new MedicalExamBL();

            var ListaCategorias = BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 116, null);

            var examenesprevios = BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 306, null).Find(p => p.Value2 == _categoriaId.ToString());

            if (examenesprevios != null)
            {
                var consultorioPrevio = int.Parse(examenesprevios.Field);

                if (consultorioPrevio == -1)
                {
                    var examenesNoCulminados = objServiceBL.GetServiceComponentsCulminados(ref objOperationResult, _serviceId);
                    var exam = examenesNoCulminados.FindAll(p => p.i_CategoryId != _categoriaId);
                    if (exam.Count != 0)
                    {
                        MessageBox.Show(@"Este paciente debe primero CULIMINAR TODOS los examenes anteriores.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var listaExamenesProtocolo = objServiceBL.GetServiceComponents(ref objOperationResult, _serviceId).Find(p => p.i_CategoryId == consultorioPrevio);

                if (listaExamenesProtocolo != null)
                {
                    var examenesNoCulminados = objServiceBL.GetServiceComponentsCulminados(ref objOperationResult, _serviceId);

                        var result = examenesNoCulminados.Find(p => p.i_CategoryId == consultorioPrevio);

                        if (result != null)
                        {
                            MessageBox.Show(@"Este paciente debe primero CULIMINAR  el examen. " + ListaCategorias.Find(p => p.Id == examenesprevios.Field).Value1, "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                }


               

            }

            //Verificar si el paciente ya ha sido llamado en la BD y no en la temporal



            //var Resultado = objServiceBL.VerificarSiPacienteNoPuedeSerLlamado(grdListaLlamando.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString());
            ////var Resultado = _objCalendarListAMC.Find(p => p.v_ServiceId == grdListaLlamando.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString());
            //if (Resultado != null)
            //{
            //    MessageBox.Show("Usted ya llamó al paciente o el paciente está siendo llamdo por otro consultorio.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    //grdListaLlamando.Enabled = false;
            //    grdLlamandoPaciente.Enabled = true;
            //    //btnRefresh.Enabled = false;

            //    chkHability.Enabled = true;

            //    btnLlamar.Enabled = false;
            //    btnRefresh_Click(null, null);

            //    if (grdLlamandoPaciente.Rows.Count > 0)
            //    {
            //        grdLlamandoPaciente.Rows[0].Selected = true;
            //        btnRellamar.Enabled = true;
            //        btnAtenderVerServicio.Enabled = true;
            //        btnLiberarFinalizarCircuito.Enabled = true;
            //    }
            //    return;
            //}

            // este código sirve para obtener imc 
          //Boolean Resultado1 = oMedicalExamBL.VerificarComponentePorCategoria(_categoriaId, Constants.ELECTROCARDIOGRAMA_ID);

          //if (Resultado1)
          //{

          //  List<ServiceComponentFieldValuesList> Valores=  objServiceBL.ValoresComponente(_serviceId, Constants.ANTROPOMETRIA_ID);
          //  if (Valores.Count !=0)
          //  {
          //      decimal ValorIMCServicio = decimal.Parse(Valores.Find(p => p.v_ComponentFieldId == Constants.ANTROPOMETRIA_IMC_ID).v_Value1.ToString());
          //      decimal ValorIMCProtocolo = decimal.Parse(oProtocolBL.GetProtocolComponentByProtocol(ref objOperationResult, _ProtocolId, Constants.ELECTROCARDIOGRAMA_ID).r_Imc.ToString());

          //      if (ValorIMCServicio < ValorIMCProtocolo)
          //      {
          //          MessageBox.Show("El I.M.C. del paciente tiene valores normales, no aplica para este examen", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   
          //          return;
          //      }
          //  }        
          //}
            
            //Validación de Piso
            if (_Piso != "-1")
            {
                var ResultPiso = objServiceBL.PermitirLlamar(_serviceId, int.Parse(_Piso.ToString()));
                if (!ResultPiso)
                {
                    MessageBox.Show("El Paciente tiene consultorios por culminar, antes de ser llamado por este. Verifíquelo en unos minutos", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                    return;
                }
            }
           
            if (int.Parse(_serviceStatusId) == (int)ServiceStatus.EsperandoAptitud)
            {
                MessageBox.Show("Este paciente ya tiene el servicio en espera de Aptitud, no puede ser llamado.", "INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
             
                return;
            }

            if (_IsCall == "OcupadoLlamado")
            {
                DialogResult Result = MessageBox.Show("Este paciente está ocupado en otro consultorio. Para llamarlo de todas formas seleccione SÍ", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Result ==  System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }


            List<CalendarList> GrillaVacia = new List<CalendarList>();
            grdLlamandoPaciente.DataSource = GrillaVacia;
            grdLlamandoPaciente.ClearUndoHistory();
            // Cargar grilla de llamando al paciente  ************
            _objCalendarListAMC.Add(objCalendar);
            grdLlamandoPaciente.DataSource = _objCalendarListAMC;
            //*******************************************************
          
            objCalendar.v_Pacient = grdListaLlamando.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();
            objCalendar.v_OrganizationName = grdListaLlamando.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value.ToString();
            objCalendar.v_ServiceComponentId = grdListaLlamando.Selected.Rows[0].Cells["v_ServiceComponentId"].Value.ToString();
            objCalendar.v_ServiceId = grdListaLlamando.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            objCalendar.d_Birthdate = DateTime.Parse(grdListaLlamando.Selected.Rows[0].Cells["d_Birthdate"].Value.ToString());
            objCalendar.v_DocNumber = grdListaLlamando.Selected.Rows[0].Cells["v_DocNumber"].Value.ToString();
            objCalendar.v_WorkingOrganizationName = grdListaLlamando.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value.ToString();
            objCalendar.v_ProtocolName = grdListaLlamando.Selected.Rows[0].Cells["v_ProtocolName"].Value.ToString();
            objCalendar.v_ProtocolId = grdListaLlamando.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
            objCalendar.v_EsoTypeName = grdListaLlamando.Selected.Rows[0].Cells["v_EsoTypeName"].Value.ToString();
            objCalendar.v_PersonId = grdListaLlamando.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            objCalendar.i_QueueStatusId = (int)QueueStatusId.LLAMANDO;

            if (_categoryId == -1)
            {
                _ServiceComponentId.Add(grdListaLlamando.Selected.Rows[0].Cells["v_ServiceComponentId"].Value.ToString());
            }
            else
            {
                foreach (var item in objServiceBL.GetServiceComponentByCategoryId(ref objOperationResult, _categoryId, _serviceId))
                {
                    _ServiceComponentId.Add(item.v_ServiceComponentId);
                }
            }


            
            for (int i = 0; i < _ServiceComponentId.Count; i++)
            {           
                objservicecomponentDto = new servicecomponentDto();
                objservicecomponentDto.v_ServiceComponentId = _ServiceComponentId[i];             
                objservicecomponentDto.i_QueueStatusId = (int)Common.QueueStatusId.LLAMANDO;
                objservicecomponentDto.v_NameOfice = cbOficina.Text.ToString();
                objServiceBL.UpdateServiceComponentOfficeLlamando(objservicecomponentDto);
            }

            //Actualizar grdDataServiceComponent      
            ListServiceComponent = objServiceBL.GetServiceComponents(ref objOperationResult, _serviceId);
            grdDataServiceComponent.DataSource = ListServiceComponent;

            //grdListaLlamando.Enabled = false;
            grdLlamandoPaciente.Enabled = true;
            //btnRefresh.Enabled = false;

            chkHability.Enabled = true;

            btnLlamar.Enabled = false;
        }

        private void mnullamar_Click(object sender, EventArgs e)
        {

            if (grdLlamandoPaciente.Rows.Count > 1 && _categoriaId != (int)CategoryTypeExam.Psicologia)
            {
                MessageBox.Show("Solo puede llamar a un paciente a la vez", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Llamar();
            }
                
           
        }

        private void Atender()
        {
            OperationResult objOperationResult = new OperationResult();
            ServiceBL objServiceBL = new ServiceBL();
            servicecomponentDto objservicecomponentDto = new servicecomponentDto();
            List<ServiceComponentList> ListServiceComponent = new List<ServiceComponentList>();
            _ServiceComponentId = new List<string>();


            if (grdDataServiceComponent.Rows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar a un paciente", "INFORMACIÓN!",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (chkVerificarHuellaDigital.Checked)
            {
                var checkingFinger = new frmCheckingFinger();
                checkingFinger._PacientId = _personId;
                checkingFinger.ShowDialog();

                if (checkingFinger.DialogResult == DialogResult.Cancel)
                    return;
            }
          
            DialogResult Result = MessageBox.Show("¿Está seguro de INICIAR ATENCIÓN este registro?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (_categoryId == -1)
            {
                _ServiceComponentId.Add(grdListaLlamando.Selected.Rows[0].Cells["v_ServiceComponentId"].Value.ToString());
            }
            else
            {
                foreach (var item in objServiceBL.GetServiceComponentByCategoryId(ref objOperationResult, _categoryId, _serviceId))
                {
                    _ServiceComponentId.Add(item.v_ServiceComponentId);
                }
            }
      
            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                for (int i = 0; i < _ServiceComponentId.Count(); i++)
                {
                    objservicecomponentDto = objServiceBL.GetServiceComponent(ref objOperationResult, _ServiceComponentId[i]);
                    objservicecomponentDto.i_QueueStatusId = (int)Common.QueueStatusId.OCUPADO;
                    objservicecomponentDto.d_StartDate = DateTime.Now;
                    objServiceBL.UpdateServiceComponent(ref objOperationResult, objservicecomponentDto, Globals.ClientSession.GetAsList());
                }
                //Actualizar grdDataServiceComponent-
                //string strServicelId = grdListaLlamando.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                ListServiceComponent = objServiceBL.GetServiceComponents(ref objOperationResult, serviceIdGrillaLlamandoPaciente);
                grdDataServiceComponent.DataSource = ListServiceComponent;

                _Flag = 1;

                Form frm;
                if (_TserviceId == (int)MasterService.AtxMedicaParticular)
                {
                    frm = new Operations.frmEso(_serviceId, string.Join("|", _componentIds.Select(p => p)), null, (int)MasterService.Eso);
                    frm.ShowDialog();
                }
                else
                {
                    // refrecar la grilla
                    ListServiceComponent = objServiceBL.GetServiceComponents(ref objOperationResult, serviceIdGrillaLlamandoPaciente);
                    grdDataServiceComponent.DataSource = ListServiceComponent;

                    this.Enabled = false;
                    frm = new Operations.frmEso(_serviceId, string.Join("|", _componentIds.Select(p => p)), null, (int)MasterService.Eso);
                    frm.ShowDialog();
                    this.Enabled = true;
                    // Aviso automático de que se culminaron todos los examanes, se tendria que proceder
                    // a establecer el estado del servicio a (Culminado Esperando Aptitud)               

                    var alert = objServiceBL.GetServiceComponentsCulminados(ref objOperationResult, _serviceId);

                    if (alert != null && alert.Count > 0)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Todos los Examenes se encuentran concluidos.\nEl estado de la Atención es: En espera de Aptitud .", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        serviceDto objserviceDto = new serviceDto();
                        objserviceDto = objServiceBL.GetService(ref objOperationResult, objservicecomponentDto.v_ServiceId);
                        objserviceDto.i_ServiceStatusId = (int)ServiceStatus.EsperandoAptitud;
                        objserviceDto.v_Motive = "Esperando Aptitud";

                        objServiceBL.UpdateService(ref objOperationResult, objserviceDto, Globals.ClientSession.GetAsList());
                    }

                }

                // refrecar la grilla
                ListServiceComponent = objServiceBL.GetServiceComponents(ref objOperationResult, _serviceId);
                grdDataServiceComponent.DataSource = ListServiceComponent;
            }
        }

        private void mnuAtender_Click(object sender, EventArgs e)
        {
            Atender();
        }

        private void Liberar()
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();
                ServiceBL objServiceBL = new ServiceBL();
                _ServiceComponentId = new List<string>();
                int? i_ServiceComponentStatusId_Antiguo = null;
                servicecomponentDto objservicecomponentDto = null;
                List<ServiceComponentList> ListServiceComponent = new List<ServiceComponentList>();


                if (grdDataServiceComponent.Rows.Count() == 0)
                {
                    MessageBox.Show("Debe seleccionar un paciente para poder LIBERARLO", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_categoryId == -1)
                {
                    _ServiceComponentId.Add(grdLlamandoPaciente.Selected.Rows[0].Cells["v_ServiceComponentId"].Value.ToString());
                }
                else
                {
                    var servCompCat = objServiceBL.GetServiceComponentByCategoryId(ref objOperationResult, _categoryId, _serviceId);
                    i_ServiceComponentStatusId_Antiguo = servCompCat[0].i_ServiceComponentStatusId;
                    foreach (var item in servCompCat)
                    {
                        _ServiceComponentId.Add(item.v_ServiceComponentId);
                    }
                }

                List<servicecomponentDto> list = new List<servicecomponentDto>();

                for (int i = 0; i < _ServiceComponentId.Count; i++)
                {
                    objservicecomponentDto = new servicecomponentDto();
                    objservicecomponentDto.v_ServiceComponentId = _ServiceComponentId[i];
                    objservicecomponentDto.i_QueueStatusId = (int)Common.QueueStatusId.LIBRE;
                    objservicecomponentDto.i_Iscalling = (int)SiNo.NO;
                    objservicecomponentDto.i_Iscalling_1 = (int)SiNo.NO;
                    objservicecomponentDto.d_EndDate = DateTime.Now;
                    objservicecomponentDto.i_ServiceComponentStatusId = i_ServiceComponentStatusId_Antiguo == 1 ? (int)Common.ServiceComponentStatus.PorAprobacion : i_ServiceComponentStatusId_Antiguo;
                   
                  

                    list.Add(objservicecomponentDto);
                    //Buscar en la lista y reemplazar el i_QueueStatusId

                    foreach (var item in _objCalendarListAMC.Where(c => c.v_ServiceComponentId == _ServiceComponentId[i]))
                    {
                        item.i_QueueStatusId = (int)Common.QueueStatusId.LIBRE;
                    }

                }

                // update
                if (_componentName == "LABORATORIO")
                {
                    objServiceBL.UpdateServiceComponentOfficeLaboratorio(list);
                }
                else
                {
                    objServiceBL.UpdateServiceComponentOffice(list);
                }


                #region Check de salir de circuito
                              
                if (chkHability.Checked == true) // finaliza el servicio y actualiza el estado del servicio
                {
                    if (ddlServiceStatusId.SelectedValue.ToString() == ((int)ServiceStatus.Iniciado).ToString())
                    {
                        MessageBox.Show("Debe elegir cualquier otro estado que no sea (Iniciado)\nSi desea Liberar y/o Finalizar Circuito.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    serviceDto objserviceDto = new serviceDto();
                  
                    objserviceDto.v_ServiceId = _serviceId;
                    objserviceDto.i_ServiceStatusId = int.Parse(ddlServiceStatusId.SelectedValue.ToString());
                    objserviceDto.v_Motive = txtReason.Text;

                    objServiceBL.UpdateServiceOffice(ref objOperationResult, objserviceDto, Globals.ClientSession.GetAsList());

                    //Actualizamos el estado de la linea de la agenda como fuera de circuito
                    CalendarBL objCalendarBL = new CalendarBL();
                    calendarDto objcalendarDto = new calendarDto();
                    objcalendarDto = objCalendarBL.GetCalendar(ref objOperationResult, _CalendarId);
                    objcalendarDto.i_LineStatusId = 2;// int.Parse(Common.LineStatus.FueraCircuito.ToString());
                    objCalendarBL.UpdateCalendar(ref objOperationResult, objcalendarDto, Globals.ClientSession.GetAsList());

                }

                #endregion

                //Actualizar grdDataServiceComponent
              
                ListServiceComponent = objServiceBL.GetServiceComponents(ref objOperationResult, _serviceId);
                grdDataServiceComponent.DataSource = ListServiceComponent;
             
                btnRefresh_Click(null, null);

                txtReason.Text = "";
                grdListaLlamando.Enabled = true;
                //grdLlamandoPaciente.Enabled = false;
                btnRefresh.Enabled = true;
                chkHability.Enabled = false;
                chkHability.Checked = false;
                groupBox3.Enabled = false;

                List<CalendarList> GrillaVacia = new List<CalendarList>();
                grdLlamandoPaciente.DataSource = GrillaVacia;

                _objCalendarListAMC.RemoveAll(x => x.i_QueueStatusId == 1);
                grdLlamandoPaciente.DataSource = _objCalendarListAMC;
                //grdLlamandoPaciente.DataSource = _objCalendarListAMC.FindAll(p => p.i_QueueStatusId != 1);

                //grdLlamandoPaciente.DataSource = new List<CalendarList>();

                if (_objCalendarListAMC.Count == 0)
                {

                    if (_objCalendarListAMC.Count > 0)
                    {
                        grdLlamandoPaciente.Rows[0].Selected = true;
                    }

                    btnRellamar.Enabled = false;
                    btnAtenderVerServicio.Enabled = false;
                    btnLiberarFinalizarCircuito.Enabled = false;
                    grdLlamandoPaciente.Enabled = false;
                    //grdLlamandoPaciente_Click(null, null);
                }

                else
                {
                    grdLlamandoPaciente.Rows[0].Selected = true;

                }

                //grdLlamandoPaciente.DataSource = new List<CalendarList>();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(Common.Utils.ExceptionFormatter(ex), "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void mnuLiberar_Click(object sender, EventArgs e)
        {
            Liberar();
        }

        private void grdListaLlamando_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var serviceId = e.Row.Cells["v_ServiceId"].Value.ToString();
            var Piso = e.Row.Cells["Piso"].Value.ToString();

            var serviceComponents = _serviceBL.GetServiceComponents(ref objOperationResult, serviceId);

            var atendido = serviceComponents.Find(p => _componentIds.Contains(p.v_ComponentId) &&
                                                (p.i_ServiceComponentStatusId == (int)Common.ServiceComponentStatus.Evaluado
                                                || p.i_ServiceComponentStatusId == (int)Common.ServiceComponentStatus.PorAprobacion
                                                //|| p.i_ServiceComponentStatusId == (int)Common.ServiceComponentStatus.Especialista
                                                )
                                                
                                                );
            
            //BETO

            var ResultPiso = _serviceBL.PermitirLlamar(serviceId, int.Parse(Piso.ToString()));
            if (!ResultPiso)
            {
                e.Row.Cells["b_NoLlamar"].Value = Resources.user_cross;
            }
            else
            {
                e.Row.Cells["b_NoLlamar"].Value = Resources.user_earth;
            }


            if (atendido != null)
            {
                e.Row.Cells["b_IsAttended"].Value = Resources.bullet_tick;
                e.Row.Cells["b_IsAttended"].ToolTipText = atendido.v_ServiceComponentStatusName;
            }
            else
            {
                var noAtendido = serviceComponents.Find(p => _componentIds.Contains(p.v_ComponentId));
                                           
                e.Row.Cells["b_IsAttended"].Value = Resources.bullet_red;
                e.Row.Cells["b_IsAttended"].ToolTipText = noAtendido.v_ServiceComponentStatusName;
            }

            //Si el contenido de la columna Vip es igual a SI
            if (e.Row.Cells["v_IsVipName"].Value.ToString().Trim() == Common.SiNo.SI.ToString())
            {
                //Escojo 2 colores
                e.Row.Appearance.BackColor = Color.White;
                e.Row.Appearance.BackColor2 = Color.Pink;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
              
            }
            else
            {
              
                //label10.Visible = false;
            }
        }

        private void grdDataServiceComponent_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            //Si el contenido de la columna Vip es igual a SI
            if (e.Row.Cells["v_QueueStatusName"].Value.ToString().Trim() == Common.QueueStatusId.OCUPADO.ToString())
            {
                //Escojo 2 colores
                e.Row.Appearance.BackColor = Color.White;
                e.Row.Appearance.BackColor2 = Color.Pink;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                //timer1.Start();
            }
            if (e.Row.Cells["v_QueueStatusName"].Value.ToString().Trim() == Common.QueueStatusId.LLAMANDO.ToString())
            {
                //Escojo 2 colores
                e.Row.Appearance.BackColor = Color.Orange;
                //e.Row.Appearance.BackColor2 = Color.Pink;
                //Y doy el efecto degradado vertical
                //e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                //timer1.Start();
            }

            //Si el contenido de la columna Vip es igual a SI
            if (e.Row.Cells["i_ServiceComponentStatusId"].Value.ToString() == ((int)Common.ServiceComponentStatus.Evaluado).ToString()
                || e.Row.Cells["i_ServiceComponentStatusId"].Value.ToString() == ((int)Common.ServiceComponentStatus.PorAprobacion).ToString())
            {
                //Escojo 2 colores
                e.Row.Appearance.BackColor = Color.White;
                e.Row.Appearance.BackColor2 = Color.Cyan;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();
                CalendarBL objCalendarBL = new CalendarBL();
                List<CalendarList> objCalendarList = new List<CalendarList>();
                List<ServiceComponentList> ListServiceComponent = new List<ServiceComponentList>();

                if (cbServiceType.SelectedValue.ToString() == "1")
                {
                    objCalendarList = objCalendarBL.GetPacientInLineByComponentId1(ref objOperationResult, 0, null, "d_ServiceDate ASC", _componentId, DateTime.Now.Date, _componentIds.ToArray(), int.Parse(cbService.SelectedValue.ToString()));
              
                }
                else
                {
                  var client = Globals.ClientSession.GetAsList();
                  objCalendarList = objCalendarBL.GetPacientInLineByComponentId1_ATX(ref objOperationResult, 0, null, "d_ServiceDate ASC", _componentId, DateTime.Now.Date, _componentIds.ToArray(), int.Parse(cbService.SelectedValue.ToString()), Int32.Parse(client[2]));
              
                }

                 grdListaLlamando.DataSource = objCalendarList;
            
                lblNameComponent.Text = _componentName;

                grdDataServiceComponent.DataSource = ListServiceComponent;       
            }
            catch (Exception ex)
            {                
                MessageBox.Show(Common.Utils.ExceptionFormatter(ex), "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void chkHability_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHability.Checked == true)
            {
                groupBox3.Enabled = true;
            }
            else
            {
                groupBox3.Enabled = false;
            }
        }

        private void btnEndCircuit_Click(object sender, EventArgs e) { }

        private void grdListaLlamando_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            List<ServiceComponentList> ListServiceComponent = new List<ServiceComponentList>();

            btnLlamar.Enabled = (grdListaLlamando.Selected.Rows.Count > 0);

            if (grdListaLlamando.Selected.Rows.Count == 0)
                return;

            btnRellamar.Enabled = false;
            btnAtenderVerServicio.Enabled = false;
            btnLiberarFinalizarCircuito.Enabled = false;

            //Obtener Piso Actual

            _Piso = grdListaLlamando.Selected.Rows[0].Cells["Piso"].Value.ToString();

            txtPacient.Text = grdListaLlamando.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();
            DateTime FechaNacimiento = (DateTime)grdListaLlamando.Selected.Rows[0].Cells["d_Birthdate"].Value;
            int PacientAge = DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1;
            txtAge.Text = PacientAge.ToString();
            txtDni.Text = grdListaLlamando.Selected.Rows[0].Cells["v_DocNumber"].Value.ToString();
            //txtFecNac.Text = grdListaLlamando.Selected.Rows[0].Cells["d_Birthdate"].Value.ToString().Substring(0, 10);
            WorkingOrganization.Text = grdListaLlamando.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value.ToString();
            txtProtocol.Text = grdListaLlamando.Selected.Rows[0].Cells["v_ProtocolName"].Value.ToString();
            if (grdListaLlamando.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString() == Constants.CONSULTAMEDICA)
            {
                txtTypeESO.Text = "";
            }
            else
            {
                txtTypeESO.Text = grdListaLlamando.Selected.Rows[0].Cells["v_EsoTypeName"].Value.ToString();
            }
            _personName = txtPacient.Text;
          







            
            _serviceId = grdListaLlamando.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            _TserviceId = int.Parse(grdListaLlamando.Selected.Rows[0].Cells["i_ServiceId"].Value.ToString());
            _CalendarId = grdListaLlamando.Selected.Rows[0].Cells["v_CalendarId"].Value.ToString();
            _categoryId = (int)grdListaLlamando.Selected.Rows[0].Cells["i_CategoryId"].Value;
            _serviceStatusId = grdListaLlamando.Selected.Rows[0].Cells["i_ServiceStatusId"].Value.ToString();
            _personId = grdListaLlamando.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();

            ListServiceComponent = _serviceBL.GetServiceComponents(ref objOperationResult, _serviceId);
            grdDataServiceComponent.DataSource = ListServiceComponent;

          

            txtDni.Text = grdListaLlamando.Selected.Rows[0].Cells["v_DocNumber"].Value.ToString();
            txtProtocol.Text = grdListaLlamando.Selected.Rows[0].Cells["v_ProtocolName"].Value.ToString();
            _ProtocolId = grdListaLlamando.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
            if (grdListaLlamando.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString() == Constants.CONSULTAMEDICA)
            {
                txtTypeESO.Text = "";
            }
            else
            {
                txtTypeESO.Text = grdListaLlamando.Selected.Rows[0].Cells["v_EsoTypeName"].Value.ToString();
            }


            // Mostrar la foto del paciente
            var personImage = _pacientBL.GetPersonImage(_personId);

            if (personImage != null)
            {
                pbImage.Image = Common.Utils.BytesArrayToImageOficce(personImage.b_PersonImage, pbImage);
                _personImage = personImage.b_PersonImage;
            }
       
            // Verificar el estado de la cola
            var ocupation = ListServiceComponent.Find(p => p.i_QueueStatusId == (int)Common.QueueStatusId.LLAMANDO
                                                        || p.i_QueueStatusId == (int)Common.QueueStatusId.OCUPADO);
            if (ocupation != null)        
                _IsCall = "OcupadoLlamado";
            else
                _IsCall = "Libre";

            ddlServiceStatusId.SelectedValue = ListServiceComponent[0].ServiceStatusId.ToString();
            txtReason.Text = ListServiceComponent[0].v_Motive.ToString();
            
        }

        private void Rellamar()
        {
            OperationResult objOperationResult = new OperationResult();
            ServiceBL objServiceBL = new ServiceBL();
            _ServiceComponentId = new List<string>();
           

            foreach (var item in objServiceBL.GetServiceComponentByCategoryId(ref objOperationResult, _categoryId, _serviceId))
            {
                _ServiceComponentId.Add(item.v_ServiceComponentId);
            }


            foreach (var scid in _ServiceComponentId)
            {
                _serviceBL.UpdateServiceComponentVisor(ref objOperationResult, scid, (int)SiNo.NO);
            }
                
            //}

        }

        private void mnuRellamar_Click(object sender, EventArgs e)
        {
            Rellamar();         
        }

        private void pbImage_Click(object sender, EventArgs e)
        {
            if (_personImage != null)
            {
                var frm = new Sigesoft.Node.WinClient.UI.Operations.Popups.frmPreviewImagePerson(_personImage, _personName);
                frm.ShowDialog();
            }

        }

        private void grdDataServiceComponent_MouseLeaveElement(object sender, Infragistics.Win.UIElementEventArgs e)
        {
            // if we are not leaving a cell, then don't anything
            if (!(e.Element is CellUIElement))
            {
                return;
            }

            // prevent the timer from ticking again
            _customizedToolTip.StopTimerToolTip();

            // destroy the tooltip
            _customizedToolTip.DestroyToolTip(this);
        }

        private void btnLlamar_Click(object sender, EventArgs e)
        {
            //OperationResult objOperationResult = new OperationResult();
            //List<ServiceComponentList> ListServiceComponent = new List<ServiceComponentList>();
            //ServiceBL objServiceBL = new ServiceBL();

            //var Resultado = objServiceBL.VerificarSiPacienteNoPuedeSerLlamado(grdListaLlamando.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString());

            //if (Resultado != null)
            //{

            //    MessageBox.Show("El Paciente acaba de ser llamado por otro consultorio. Seleccione a otro paciente", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    ListServiceComponent = objServiceBL.GetServiceComponents(ref objOperationResult, _serviceId);
            //    grdDataServiceComponent.DataSource = ListServiceComponent;
            //    //grdListaLlamando.Enabled = false;
            //    grdLlamandoPaciente.Enabled = true;
            //    btnRefresh.Enabled = false;

            //    chkHability.Enabled = true;

            //    btnLlamar.Enabled = false;
            //    //btnRefresh_Click(sender, e);
            //}
            //else
            //{
            if (grdLlamandoPaciente.Rows.Count == 1 && _categoriaId != (int)CategoryTypeExam.Psicologia)
            {
                MessageBox.Show("Solo puede llamar a un paciente a la vez", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Llamar();
            }
            //}
        }

        private void btnRellamar_Click(object sender, EventArgs e)
        {
            Rellamar();
        }

        private void btnAtenderVerServicio_Click(object sender, EventArgs e)
        {
            Atender();
        }

        private void btnLiberarFinalizarCircuito_Click(object sender, EventArgs e)
        {
            Liberar();
        }

        private void grdLlamandoPaciente_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {


            if (_objCalendarListAMC.Count == 0 || grdLlamandoPaciente.Rows.Count == 0) return;
            if (grdLlamandoPaciente.Selected.Rows.Count == 0) return;
            btnRellamar.Enabled = true;
            btnAtenderVerServicio.Enabled = true;
            btnLiberarFinalizarCircuito.Enabled = true;

            OperationResult objOperationResult = new OperationResult();
            ServiceBL objServiceBL = new ServiceBL();
            _serviceId = grdLlamandoPaciente.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();//_objCalendarListAMC[0].v_ServiceId;

            //Cabecera
            txtPacient.Text = grdLlamandoPaciente.Selected.Rows[0].Cells["v_Pacient"].Value.ToString(); //_objCalendarListAMC[0].v_Pacient;
            DateTime FechaNacimiento = DateTime.Parse(grdLlamandoPaciente.Selected.Rows[0].Cells["d_Birthdate"].Value.ToString()); //_objCalendarListAMC[0].d_Birthdate;
            int PacientAge = DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1;
            txtAge.Text = PacientAge.ToString();

            txtDni.Text = grdLlamandoPaciente.Selected.Rows[0].Cells["v_DocNumber"].Value.ToString(); //_objCalendarListAMC[0].v_DocNumber;
            WorkingOrganization.Text = grdLlamandoPaciente.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value.ToString(); //_objCalendarListAMC[0].v_WorkingOrganizationName;
            txtProtocol.Text = grdLlamandoPaciente.Selected.Rows[0].Cells["v_ProtocolName"].Value.ToString(); //_objCalendarListAMC[0].v_ProtocolName;
            string PersonId = grdLlamandoPaciente.Selected.Rows[0].Cells["v_PersonId"].Value.ToString(); //_objCalendarListAMC[0].v_PersonId;
            if (grdLlamandoPaciente.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString() == Constants.CONSULTAMEDICA)
            {
                txtTypeESO.Text = "";
            }
            else
            {
                txtTypeESO.Text = grdLlamandoPaciente.Selected.Rows[0].Cells["v_EsoTypeName"].Value.ToString(); //_objCalendarListAMC[0].v_EsoTypeName;
            }



            serviceIdGrillaLlamandoPaciente = grdLlamandoPaciente.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString(); //_objCalendarListAMC[0].v_ServiceId;
            var ListServiceComponent = objServiceBL.GetServiceComponents(ref objOperationResult, serviceIdGrillaLlamandoPaciente);
            grdDataServiceComponent.DataSource = ListServiceComponent;


            // Mostrar la foto del paciente
            var personImage = _pacientBL.GetPersonImage(PersonId);

            if (personImage != null)
            {
                pbImage.Image = Common.Utils.BytesArrayToImageOficce(personImage.b_PersonImage, pbImage);
                //_personImage = personImage.b_PersonImage;
            }


        }

        private void frmOffice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (grdLlamandoPaciente.Rows.Count > 0)
            {
                grdLlamandoPaciente.Rows[0].Selected = true;
                var message = string.Format("Debe liberar a todos los trabajadores antes de cerrar el formulario");
                MessageBox.Show(message, "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void btnAgregarAdiconal_Click(object sender, EventArgs e)
        {
            //ServiceBL oServiceBL = new ServiceBL();
            //var frm = new frmAddExam(ListaComponentes);
            //frm._serviceId = _serviceId;
            //frm.ShowDialog();

            //if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            //    return;

            //OperationResult objOperationResult = new OperationResult();
            //var ListServiceComponent = oServiceBL.GetServiceComponents(ref objOperationResult, _strServicelId);
            //grdDataServiceComponent.DataSource = ListServiceComponent;
            ServiceBL oServiceBL = new ServiceBL();
            var frm = new frmAddExam(ListaComponentes,"",_ProtocolId,"","","","");
            frm._serviceId = _serviceId;
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;

            OperationResult objOperationResult = new OperationResult();
            var ListServiceComponent = oServiceBL.GetServiceComponents(ref objOperationResult, _strServicelId);
            grdDataServiceComponent.DataSource = ListServiceComponent;
     
        }

        private void btnRemoverEsamen_Click(object sender, EventArgs e)
        {
            ServiceBL oServiceBL = new ServiceBL();
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?", "ADVERTENCIA!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.OK)
            {

                var _auxiliaryExams = new List<ServiceComponentList>();
                OperationResult objOperationResult = new OperationResult();

                int categoryId = int.Parse(grdDataServiceComponent.Selected.Rows[0].Cells["i_CategoryId"].Value.ToString());
                var serviceComponentId = grdDataServiceComponent.Selected.Rows[0].Cells["v_ServiceComponentId"].Value.ToString();

                if (categoryId == -1)
                {
                    ServiceComponentList auxiliaryExam = new ServiceComponentList();
                    auxiliaryExam.v_ServiceComponentId = serviceComponentId;
                    _auxiliaryExams.Add(auxiliaryExam);
                }
                else
                {
                    var oServiceComponentList = oServiceBL.GetServiceComponentByCategoryId(ref objOperationResult, categoryId, _serviceId);

                    foreach (var scid in oServiceComponentList)
                    {
                        ServiceComponentList auxiliaryExam = new ServiceComponentList();
                        auxiliaryExam.v_ServiceComponentId = scid.v_ServiceComponentId;
                        _auxiliaryExams.Add(auxiliaryExam);
                    }

                }

                _objCalendarBL.UpdateAdditionalExam(_auxiliaryExams, _serviceId, (int?)SiNo.NO, Globals.ClientSession.GetAsList());
                var ListServiceComponent = oServiceBL.GetServiceComponents(ref objOperationResult, _serviceId);
                grdDataServiceComponent.DataSource = ListServiceComponent;
                //MessageBox.Show("Se grabo correctamente", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grdListaLlamando_Click(object sender, EventArgs e)
        {

        }

        private void grdLlamandoPaciente_Click(object sender, EventArgs e)
        {
            if (_objCalendarListAMC.Count == 0 || grdLlamandoPaciente.Rows.Count == 0) return;
            if (grdLlamandoPaciente.Selected.Rows.Count == 0) return;
            btnRellamar.Enabled = true;
            btnAtenderVerServicio.Enabled = true;
            btnLiberarFinalizarCircuito.Enabled = true;

            OperationResult objOperationResult = new OperationResult();
            ServiceBL objServiceBL = new ServiceBL();
            _serviceId = grdLlamandoPaciente.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();//_objCalendarListAMC[0].v_ServiceId;

            //Cabecera
            txtPacient.Text = grdLlamandoPaciente.Selected.Rows[0].Cells["v_Pacient"].Value.ToString(); //_objCalendarListAMC[0].v_Pacient;
            DateTime FechaNacimiento = DateTime.Parse(grdLlamandoPaciente.Selected.Rows[0].Cells["d_Birthdate"].Value.ToString()); //_objCalendarListAMC[0].d_Birthdate;
            int PacientAge = DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1;
            txtAge.Text = PacientAge.ToString();

            txtDni.Text = grdLlamandoPaciente.Selected.Rows[0].Cells["v_DocNumber"].Value.ToString(); //_objCalendarListAMC[0].v_DocNumber;
            WorkingOrganization.Text = grdLlamandoPaciente.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value.ToString(); //_objCalendarListAMC[0].v_WorkingOrganizationName;
            txtProtocol.Text = grdLlamandoPaciente.Selected.Rows[0].Cells["v_ProtocolName"].Value.ToString(); //_objCalendarListAMC[0].v_ProtocolName;
            string PersonId = grdLlamandoPaciente.Selected.Rows[0].Cells["v_PersonId"].Value.ToString(); //_objCalendarListAMC[0].v_PersonId;
            if (grdLlamandoPaciente.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString() == Constants.CONSULTAMEDICA)
            {
                txtTypeESO.Text = "";
            }
            else
            {
                txtTypeESO.Text = grdLlamandoPaciente.Selected.Rows[0].Cells["v_EsoTypeName"].Value.ToString(); //_objCalendarListAMC[0].v_EsoTypeName;
            }



            serviceIdGrillaLlamandoPaciente = grdLlamandoPaciente.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString(); //_objCalendarListAMC[0].v_ServiceId;
            var ListServiceComponent = objServiceBL.GetServiceComponents(ref objOperationResult, serviceIdGrillaLlamandoPaciente);
            grdDataServiceComponent.DataSource = ListServiceComponent;


            // Mostrar la foto del paciente
            var personImage = _pacientBL.GetPersonImage(PersonId);

            if (personImage != null)
            {
                pbImage.Image = Common.Utils.BytesArrayToImageOficce(personImage.b_PersonImage, pbImage);
                //_personImage = personImage.b_PersonImage;
            }
        }

        private void grdListaLlamando_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cbServiceType_TextChanged(object sender, EventArgs e)
        {
            if (cbServiceType.SelectedIndex == 0 || cbServiceType.SelectedIndex == -1)
                return;

            OperationResult objOperationResult = new OperationResult();
            var id = int.Parse(cbServiceType.SelectedValue.ToString());
            Utils.LoadDropDownList(cbService, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, id, null), DropDownListAction.Select);

        }

        private void cbService_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbService_SelectedValueChanged(object sender, EventArgs e)
        {

            //btnRefresh_Click(sender, e);
        }

    }
}
