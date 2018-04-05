using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bytescout.Spreadsheet;
using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace Sigesoft.Server.WebClientAdmin.UI.CargaData
{
    public partial class FRM050 : System.Web.UI.Page
    {
        List<Sigesoft.Node.WinClient.BE.PacientList> _TempPacientList;
        ProtocolBL oProtocolBL = new ProtocolBL();
        OrganizationBL oOrganizationBL = new OrganizationBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OperationResult objOperationResult = new OperationResult();
                btnAgregar.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM052.aspx");
                btnDescargarPlantilla.OnClientClick = Window1.GetSaveStateReference(hfRefresh.ClientID) + Window1.GetShowReference("FRM050B.aspx");
                dpFechaInicio.SelectedDate = DateTime.Now;

                var ObtenerEmpresasCliente = new ProtocolBL().GetOrganizationCustumerByProtocolSystemUser(ref objOperationResult, ((ClientSession)Session["objClientSession"]).i_SystemUserId);
                Session["EmpresaClienteId"] = ObtenerEmpresasCliente[0].IdEmpresaCliente;
                Utils.LoadDropDownList(ddlEmpresaCliente, "CustomerOrganizationName", "IdEmpresaCliente", ObtenerEmpresasCliente, DropDownListAction.All);

                ddlEmpresaCliente.SelectedValue = Session["EmpresaClienteId"].ToString();
                Utils.LoadDropDownList(ddlProtocoloId, "Value1", "Id", oProtocolBL.GetProtocolBySystemUser(ref objOperationResult, ((ClientSession)Session["objClientSession"]).i_SystemUserId), DropDownListAction.Select);

            }
        }

        protected void fileDoc_FileSelected(object sender, EventArgs e)
        {
            try
            {
                int Value = 0;
                bool Imported = true;
                int ErrorCounter = 0;
                DateTime ValueDateTime;
                StringBuilder sbMensaje = new StringBuilder();

                if (ddlProtocoloId.SelectedValue == "-1")
                {
                    Alert.ShowInTop("Debe seleccionar un protocolo", "Pregunta");   
                    return;
                }

                if (_TempPacientList != null)
                {
                   Alert.ShowInTop("Ya existe una lista de pacientes por agendar", "Pregunta");                   
                    return;                  
                }

                     string fileName = fileDoc.FileName;
                
                     if (fileName != "")
                     {
                         string Ext = fileName.Substring(fileName.IndexOf('.') + 0).ToUpper();
                         _TempPacientList = new List<Sigesoft.Node.WinClient.BE.PacientList>();
                         if (Ext == ".XLSX" || Ext == ".XLS")
                         {

                             fileDoc.SaveAs(Server.MapPath("~/upload/" + fileName));

                             Spreadsheet document = new Spreadsheet();
                             document.LoadFromFile(Server.MapPath("~/upload/" + fileName));
                             Worksheet worksheet1 = document.Workbook.Worksheets.ByName("PLANTILLA");

                             Sigesoft.Node.WinClient.BE.PacientList TempPacient;

                             int i = 4;
                             int ii = 4;
                             //Validar que el excel no esta vacio
                             while (worksheet1.Cell(ii,0).ValueAsString != "")
                             {
                                 if (worksheet1.Cell(ii, 0).ValueAsString == null || worksheet1.Cell(ii, 1).ValueAsString == null || worksheet1.Cell(ii, 2).ValueAsString == null || worksheet1.Cell(ii, 3).ValueAsString == null || worksheet1.Cell(ii, 4).ValueAsString == null || worksheet1.Cell(ii, 5).ValueAsString == null || worksheet1.Cell(ii, 6).ValueAsString == null || worksheet1.Cell(ii, 7).ValueAsString == null || worksheet1.Cell(ii, 8).ValueAsString == null || worksheet1.Cell(ii, 9).ValueAsString == null || worksheet1.Cell(ii, 10).ValueAsString == null || worksheet1.Cell(ii, 11).ValueAsString == null || worksheet1.Cell(ii, 12).ValueAsString == null)
                                 {

                                     for (int y = 0; y <= 12; y++)
                                     {
                                         if (worksheet1.Cell(ii, y).ValueAsString == null)
                                         {
                                             Imported = false;
                                             sbMensaje.Append("Registro número : ");
                                             sbMensaje.Append(worksheet1.Cell(ii, 0).ValueAsString);
                                             sbMensaje.Append(". El campo " + worksheet1.Cell(3, y).ValueAsString.ToString() + " no puede estar vacio");
                                             sbMensaje.Append("\n");
                                         }
                                     }
                                 }
                                 ii++;
                             }
                             if (Imported == false)
                             {
                                 Alert.ShowInTop(sbMensaje.ToString(), "Corregir registros en blanco");
                                 return;
                             }


                             while (worksheet1.Cell(i,0).ValueAsString != "")
                             {
                                 TempPacient = new Sigesoft.Node.WinClient.BE.PacientList();

                                 if (worksheet1.Cell(i,0).ValueAsString != "")
                                 {
                                     TempPacient.i_Correlative = int.Parse(worksheet1.Cell(i,0).ValueAsString.ToString());
                                     Imported = true;
                                 }
                                 //Nombres
                                 if (worksheet1.Cell(i,1).ValueAsString != "")
                                 {
                                     TempPacient.v_FirstName = worksheet1.Cell(i,1).ValueAsString.ToString();
                                     Imported = true;
                                 }
                                 else
                                 {
                                     ErrorCounter++;
                                     Imported = false;
                                     sbMensaje.Append("Registro número : ");
                                     sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                     sbMensaje.Append(". El campo Nombres es inválido");
                                     sbMensaje.Append("\n");
                                     i++;
                                     continue;
                                 }
                                 //Apellido Paterno
                                 if (worksheet1.Cell(i,2).ValueAsString != "")
                                 {
                                     TempPacient.v_FirstLastName = worksheet1.Cell(i,2).ValueAsString.ToString();
                                     Imported = true;
                                 }
                                 else
                                 {
                                     ErrorCounter++;
                                     Imported = false;
                                     sbMensaje.Append("Registro número : ");
                                     sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                     sbMensaje.Append(". El campo Apellido Paterno es inválido");
                                     sbMensaje.Append("\n");
                                     i++;
                                     continue;
                                 }
                                 //Apellido Materno
                                 if (worksheet1.Cell(i,3).ValueAsString != "")
                                 {
                                     TempPacient.v_SecondLastName = worksheet1.Cell(i,3).ValueAsString.ToString();
                                     Imported = true;
                                 }
                                 else
                                 {
                                     ErrorCounter++;
                                     Imported = false;
                                     sbMensaje.Append("Registro número : ");
                                     sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                     sbMensaje.Append(". El campo Apellido Materno es inválido");
                                     sbMensaje.Append("\n");
                                     i++;
                                     continue;
                                 }
                                 //ID Tipo Documento
                                 if (worksheet1.Cell(i,4).ValueAsString == null)
                                 {
                                     ErrorCounter++;
                                     Imported = false;
                                     sbMensaje.Append("Registro número : ");
                                     sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                     sbMensaje.Append(". El campo ID Tipo Documento es inválido");
                                     sbMensaje.Append("\n");
                                     i++;
                                     continue;
                                 }
                                 if (int.TryParse(worksheet1.Cell(i,4).ValueAsString.ToString(), out  Value) == false)
                                 {
                                     ErrorCounter++;
                                     Imported = false;
                                     sbMensaje.Append("Registro número : ");
                                     sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                     sbMensaje.Append(". El campo ID Tipo Documento es inválido");
                                     sbMensaje.Append("\n");
                                     i++;
                                     continue;
                                 }
                                 else
                                 {
                                     Imported = true;
                                     TempPacient.i_DocTypeId = int.Parse(worksheet1.Cell(i,4).ValueAsString.ToString());
                                 }
                                 //Nombre Tipo Documento
                                 if (worksheet1.Cell(i,5).ValueAsString != "")
                                 {
                                     TempPacient.v_DocTypeName = worksheet1.Cell(i,5).ValueAsString.ToString();
                                     Imported = true;
                                 }
                                 else
                                 {
                                     ErrorCounter++;
                                     Imported = false;
                                     sbMensaje.Append("Registro número : ");
                                     sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                     sbMensaje.Append(". El campo Nombre Tipo Documento es inválido");
                                     sbMensaje.Append("\n");
                                     i++;
                                     continue;
                                 }
                                 //Número Documento
                                 if (worksheet1.Cell(i,6).ValueAsString != "")
                                 {

                                     if (worksheet1.Cell(i,4).ValueAsString.ToString() == "1") // DNI
                                     {
                                         if (worksheet1.Cell(i,6).ValueAsString.ToString().Length != 8)
                                         {
                                             ErrorCounter++;
                                             Imported = false;
                                             sbMensaje.Append("Registro número : ");
                                             sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                             sbMensaje.Append(". El campo Número de DNI debe tener 8 dígitos");
                                             sbMensaje.Append("\n");
                                             i++;
                                             continue;
                                         }
                                         else
                                         {
                                             Imported = true;
                                             TempPacient.v_DocNumber = worksheet1.Cell(i,6).ValueAsString.ToString();
                                         }

                                     }
                                     else if (worksheet1.Cell(i,4).ValueAsString.ToString() == "2") // PASAPORTE
                                     {
                                         if (worksheet1.Cell(i,6).ValueAsString.ToString().Length != 9)
                                         {
                                             ErrorCounter++;
                                             Imported = false;
                                             sbMensaje.Append("Registro número : ");
                                             sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                             sbMensaje.Append(". El Número PASAPORTE debe tener 9 dígitos");
                                             sbMensaje.Append("\n");
                                             i++;
                                             continue;
                                         }
                                         else
                                         {
                                             Imported = true;
                                             TempPacient.v_DocNumber = worksheet1.Cell(i,6).ValueAsString.ToString();
                                         }
                                     }
                                     else if (worksheet1.Cell(i,4).ValueAsString.ToString() == "3") // LICENCIA DE CONDUCIR
                                     {
                                         if (worksheet1.Cell(i,6).ValueAsString.ToString().Length != 10)
                                         {
                                             ErrorCounter++;
                                             Imported = false;
                                             sbMensaje.Append("Registro número : ");
                                             sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                             sbMensaje.Append(". El Número LICENCIA DE CONDUCIR debe tener 10 dígitos");
                                             sbMensaje.Append("\n");
                                             i++;
                                             continue;
                                         }
                                         else
                                         {
                                             Imported = true;
                                             TempPacient.v_DocNumber = worksheet1.Cell(i,6).ValueAsString.ToString();
                                         }
                                     }
                                     else if (worksheet1.Cell(i,4).ValueAsString.ToString() == "4")// CARNET DE EXTRANJERIA
                                     {
                                         if (worksheet1.Cell(i,6).ValueAsString.ToString().Length != 11)
                                         {
                                             ErrorCounter++;
                                             Imported = false;
                                             sbMensaje.Append("Registro número : ");
                                             sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                             sbMensaje.Append(". El Número CARNET DE EXTRANJERIA debe tener 11 dígitos");
                                             sbMensaje.Append("\n");
                                             i++;
                                             continue;
                                         }
                                         else
                                         {
                                             Imported = true;
                                             TempPacient.v_DocNumber = worksheet1.Cell(i,6).ValueAsString.ToString();
                                         }
                                     }
                                 }
                                 else
                                 {
                                     ErrorCounter++;
                                     Imported = false;
                                     sbMensaje.Append("Registro número : ");
                                     sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                     sbMensaje.Append(". El campo Número Documento es inválido");
                                     sbMensaje.Append("\n");
                                     i++;
                                     continue;
                                 }
                                 //ID Género
                                 if (int.TryParse(worksheet1.Cell(i,7).ValueAsString.ToString(), out  Value))
                                 {
                                     Imported = true;
                                     TempPacient.i_SexTypeId = int.Parse(worksheet1.Cell(i,7).ValueAsString.ToString());
                                 }
                                 else
                                 {
                                     ErrorCounter++;
                                     Imported = false;
                                     sbMensaje.Append("Registro número : ");
                                     sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                     sbMensaje.Append(". El campo ID Género es inválido");
                                     sbMensaje.Append("\n");
                                     i++;
                                     continue;
                                 }
                                 // Nombre Género
                                 if (worksheet1.Cell(i,8).ValueAsString != "")
                                 {
                                     Imported = true;
                                     TempPacient.v_SexTypeName = worksheet1.Cell(i,8).ValueAsString.ToString();
                                 }
                                 else
                                 {
                                     ErrorCounter++;
                                     Imported = false;
                                     sbMensaje.Append("Registro número : ");
                                     sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                     sbMensaje.Append(". El campo Nombre Género es inválido");
                                     sbMensaje.Append("\n");
                                     i++;
                                     continue;
                                 }
                                 //Fecha Nacimiento
                                 if (worksheet1.Cell(i,12).ValueAsString == null)
                                 {
                                     ErrorCounter++;
                                     Imported = false;
                                     sbMensaje.Append("Registro número : ");
                                     sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                     sbMensaje.Append(". El campo Fecha Nacimiento es inválido");
                                     sbMensaje.Append("\n");
                                     i++;
                                     continue;
                                 }
                                 if (DateTime.TryParseExact(worksheet1.Cell(i,12).ValueAsString.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out  ValueDateTime) == false)
                                 {
                                     ErrorCounter++;
                                     Imported = false;
                                     sbMensaje.Append("Registro número : ");
                                     sbMensaje.Append(worksheet1.Cell(i,0).ValueAsString);
                                     sbMensaje.Append(". El campo Fecha Nacimiento es inválido");
                                     sbMensaje.Append("\n");
                                     i++;
                                     continue;
                                 }
                                 else
                                 {
                                     Imported = true;
                                     TempPacient.d_Birthdate = DateTime.ParseExact(worksheet1.Cell(i,12).ValueAsString.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                 }


                                 //Puesto de Trabajo
                                 if (worksheet1.Cell(i,13).ValueAsString != "")
                                 {
                                     TempPacient.v_CurrentOccupation = worksheet1.Cell(i,13).ValueAsString.ToString();
                                     Imported = true;
                                 }
                                 else
                                 {
                                     //ErrorCounter++;
                                     //Imported = false;
                                     //sbMensaje.Append("Registro número : ");
                                     //sbMensaje.Append(worksheet1.Cell(i,10).ValueAsString);
                                     //sbMensaje.Append(". El campo Nombres es inválido");
                                     //sbMensaje.Append("\n");
                                     //i++;
                                     //continue;
                                     TempPacient.v_CurrentOccupation = string.Empty;
                                 }

                                 //PROTOCOLO ID
                                 if (ddlProtocoloId.SelectedValue != "-1")
                                 {
                                     TempPacient.v_ProtocoloId = ddlProtocoloId.SelectedText.ToString();
                                     Imported = true;
                                 }
                                 else
                                 {
                                     TempPacient.v_ProtocoloId = string.Empty;
                                 }
                                 _TempPacientList.Add(TempPacient);
                                 Session["_TempPacientList"] = _TempPacientList;
                                 var Result = _TempPacientList.FindAll(p => p.v_DocNumber == TempPacient.v_DocNumber && p.i_DocTypeId == TempPacient.i_DocTypeId);
                                 if (Result.Count > 1)
                                 {
                                     Alert.ShowInTop("El correlativo " + Result[0].i_Correlative + " tiene el mismo Número Documento que el correlativo " + Result[1].i_Correlative + " .Revise el Excel y corriga la duplicidad", "Error al cargar Excel");
                                     return;
                                 }
                                 i++;

                             }

                             //lblRecordCountPacients.Text = string.Format("Se encontraron {0} registros.", _TempPacientList.Count());

                             if (ErrorCounter > 0)
                             {
                                 _TempPacientList = new List<Sigesoft.Node.WinClient.BE.PacientList>();
                                 grdData.DataSource = new List<PacientList>();
                                 grdData.DataBind();
                                 Alert.ShowInTop(sbMensaje.ToString(), "Registros no importados");
                                 sbMensaje = null;
                                 btnSubir.Hidden = true;
                             }
                             else if (ErrorCounter == 0)
                             {
                                 grdData.DataSource = _TempPacientList;
                                 grdData.DataBind();
                                 Alert.ShowInTop("Se importaron " + _TempPacientList.Count() + " registros.", "Importación correcta");
                                 btnSubir.Hidden = false;
                             }
                             else
                             {
                                 _TempPacientList = new List<Sigesoft.Node.WinClient.BE.PacientList>();
                                 grdData.DataSource = new List<Sigesoft.Node.WinClient.BE.PacientList>();
                                 grdData.DataBind();
                                 Alert.ShowInTop(sbMensaje.ToString(), "Registros no importados");
                                 sbMensaje = null;
                             }
                         }
                         else
                         {
                             Alert.ShowInTop("Solo se puede subir archivos con esta extensión : .XLSX");
                             return;
                         }

                     }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            CalendarBL objCalendarBL = new CalendarBL();
            calendarDto objCalendarDto = new calendarDto();
            PacientBL objPacientBL = new PacientBL();
            pacientDto objPacientDto = new pacientDto();
            PacientList PacientList = new PacientList();

            if (dpFechaInicio.SelectedDate.Value < DateTime.Now.Date)
            {
                Alert.ShowInTop("No se permite agendar con una fecha anterior a la actual.", "Error de validación");
                return;
            }

            string CalendarId;
            string PacientId = "";

            StringBuilder sbDatos = new StringBuilder();

            var ListaGrilla = (List<Sigesoft.Node.WinClient.BE.PacientList>)Session["_TempPacientList"];
            foreach (var item in ListaGrilla)
                {
                    personDto objPersonDto = new personDto();
                    //Validar si el trabajador existe
                    objPersonDto = objPacientBL.GetPersonByNroDocument(ref objOperationResult, item.v_DocNumber);
                    if (objPersonDto != null)
                    {
                        objPersonDto.v_FirstName = item.v_FirstName.Trim();
                        objPersonDto.v_FirstLastName = item.v_FirstLastName.Trim();
                        objPersonDto.v_SecondLastName = item.v_SecondLastName.Trim();
                        objPersonDto.i_DocTypeId = item.i_DocTypeId;
                        objPersonDto.v_DocNumber = item.v_DocNumber;
                        objPersonDto.i_SexTypeId = item.i_SexTypeId;
                        objPersonDto.d_Birthdate = item.d_Birthdate;
                        objPersonDto.i_LevelOfId = -1;
                        objPersonDto.i_MaritalStatusId = -1;

                        objPersonDto.i_BloodGroupId = -1;
                        objPersonDto.i_BloodFactorId = -1;
                        objPersonDto.i_DepartmentId = -1;
                        objPersonDto.i_ProvinceId = -1;
                        objPersonDto.i_DistrictId = -1;
                        objPersonDto.i_ResidenceInWorkplaceId = -1;
                        objPersonDto.i_TypeOfInsuranceId = -1;
                        objPersonDto.i_OccupationTypeId = -1;
                        objPersonDto.i_AltitudeWorkId = -1;
                        objPersonDto.i_PlaceWorkId = -1;
                        objPersonDto.i_Relationship = -1;

                        objPersonDto.v_CurrentOccupation = item.v_CurrentOccupation;
                        objPacientBL.UpdatePacient(ref objOperationResult, objPersonDto, ((ClientSession)Session["objClientSession"]).GetAsList(), objPersonDto.v_DocNumber, objPersonDto.v_DocNumber);
                        PacientId = objPersonDto.v_PersonId;
                    }
                    else
                    {
                        objPersonDto = new personDto();
                        objPersonDto.v_FirstName = item.v_FirstName.Trim();
                        objPersonDto.v_FirstLastName = item.v_FirstLastName.Trim();
                        objPersonDto.v_SecondLastName = item.v_SecondLastName.Trim();
                        objPersonDto.i_DocTypeId = item.i_DocTypeId;
                        objPersonDto.v_DocNumber = item.v_DocNumber;
                        objPersonDto.i_SexTypeId = item.i_SexTypeId;
                        objPersonDto.d_Birthdate = item.d_Birthdate;
                        objPersonDto.i_LevelOfId = -1;
                        objPersonDto.i_MaritalStatusId = -1;
                        objPersonDto.i_BloodGroupId = -1;
                        objPersonDto.i_BloodFactorId = -1;
                        objPersonDto.i_DepartmentId = -1;
                        objPersonDto.i_ProvinceId = -1;
                        objPersonDto.i_DistrictId = -1;
                        objPersonDto.i_ResidenceInWorkplaceId = -1;
                        objPersonDto.i_TypeOfInsuranceId = -1;
                        objPersonDto.i_OccupationTypeId = -1;
                        objPersonDto.i_AltitudeWorkId = -1;
                        objPersonDto.i_PlaceWorkId = -1;
                        objPersonDto.i_Relationship = -1;
                        objPersonDto.v_Deducible = 0;
                        //objPersonDto.v_Password = item.v_DocNumber;
                        objPersonDto.v_CurrentOccupation = item.v_CurrentOccupation;

                        PacientId = objPacientBL.AddPacient(ref objOperationResult, objPersonDto, ((ClientSession)Session["objClientSession"]).GetAsList());
                    }

                    var Verificar = objPacientBL.GetBlackList(ref objOperationResult, objPersonDto.v_PersonId);


                    if (PacientId != null && Verificar == null)  // Se grabo el paciente y se lo agenda
                    {
                        objCalendarDto.v_PersonId = PacientId;
                        objCalendarDto.d_DateTimeCalendar = dpFechaInicio.SelectedDate.Value;
                        objCalendarDto.i_ServiceTypeId = (int)ServiceType.Empresarial;
                        objCalendarDto.i_CalendarStatusId = (int)CalendarStatus.Agendado;
                        objCalendarDto.i_ServiceId = (int)MasterService.Eso;
                        //objCalendarDto.v_ProtocolId = _ProtocolId;
                        //Obtener Id de Protocolo por medio del código del protocolo
                        var ProtocolId = oProtocolBL.ObtenerProtocoloIdPorCodigoProtocolo(ddlProtocoloId.SelectedText);
                        if (ProtocolId == null)
                        {
                            Alert.ShowInTop("El protocolo no existe en el sistema.", "ERRROR!");
                            return;
                        }
                        objCalendarDto.v_ProtocolId = ProtocolId;
                        objCalendarDto.i_NewContinuationId = (int)modality.NuevoServicio;
                        objCalendarDto.i_LineStatusId = (int)LineStatus.FueraCircuito;
                        objCalendarDto.i_IsVipId = (int)SiNo.NO;

                        CalendarId = objPacientBL.AddShedule(ref objOperationResult, objCalendarDto, ((ClientSession)Session["objClientSession"]).GetAsList(), ProtocolId, PacientId, (int)MasterService.Eso, "Nuevo");

                        objCalendarBL.CircuitStart(ref objOperationResult, CalendarId, DateTime.Now, ((ClientSession)Session["objClientSession"]).GetAsList());

                    }
                    else  // no se grabro el paciente
                    {
                        sbDatos.Append("PACIENTE :  ");
                        sbDatos.Append(objPersonDto.v_FirstName + " " + objPersonDto.v_FirstLastName + " " + objPersonDto.v_SecondLastName);
                        sbDatos.Append("  DOCUMENTO :  ");
                        sbDatos.Append(objPersonDto.v_DocNumber);
                        sbDatos.Append("\n");
                    }
                }

            //iniciar circuitos 


                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    Alert.Show("Se agendó correctamente.");
                }
                else// Operación con error
                {
                    Alert.ShowInTop("Error al agendar, por favor comuníquese con su proveedor", "ERROR!");
                   
                }
            }

        protected void winEdit_Close(object sender, WindowCloseEventArgs e)
        {
            List<Sigesoft.Node.WinClient.BE.PacientList> _TempPacientList;
            _TempPacientList = new List<Sigesoft.Node.WinClient.BE.PacientList>();

            _TempPacientList = (List<Sigesoft.Node.WinClient.BE.PacientList>)Session["_TempPacientList"];
            grdData.DataSource = _TempPacientList;
            grdData.DataBind();

            btnSubir.Hidden = false;
        }

        protected void grdData_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAction")
            {
                //DeleteItem();
                //BindGrid();
            }
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
          

        }

        protected void grdData_RowDataBound(object sender, GridRowEventArgs e)
        {
           
        }

        protected void ddlProtocoloId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ProtocoloNombre"] = ddlProtocoloId.SelectedText.ToString();
        }
  
      
    }
}