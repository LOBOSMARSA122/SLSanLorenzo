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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.IO;
using NetPdf;
using System.Web.Configuration;


namespace Sigesoft.Server.WebClientAdmin.UI.Administrador_Servicios
{
    public partial class FrmAdmServiciosInternacional : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        ProtocolBL _objProtocolBL = new ProtocolBL();
        OrganizationBL oOrganizationBL = new OrganizationBL();
        ProtocolBL oProtocolBL = new ProtocolBL();
        ServiceBL _serviceBL = new ServiceBL();
        List<string> _filesNameToMerge = new List<string>();
        private string _serviceId;
        private string _pacientId;
        string ruta;
        private string _tempSourcePath;
        PacientBL _pacientBL = new PacientBL();
        HistoryBL _historyBL = new HistoryBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnReporte.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("../Consultorios/frmVisorReporte.aspx?Mode=Consolidado");
                OperationResult objOperationResult = new OperationResult();
                dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-1); //DateTime.Parse("25/07/2014");
                dpFechaFin.SelectedDate = DateTime.Now;// DateTime.Parse("25/07/2014");
                LoadComboBox();
                btnCambiarFechaServicio.OnClientClick = WinFechaServicio.GetSaveStateReference(hfRefresh.ClientID) + WinFechaServicio.GetShowReference("../Consultorios/FRMCAMBIOFECHASERVICIO.aspx");
               
            }
        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlAptitud, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 124), DropDownListAction.All);
            Utils.LoadDropDownList(ddlTipoESO, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 118), DropDownListAction.All);
           
            Utils.LoadDropDownList(ddlEmpresa, "Value1", "Id", oOrganizationBL.GetAllOrganizations(ref objOperationResult), DropDownListAction.All);
            var o = oProtocolBL.DevolverProtocolosPorEmpresaOnly("-1");
            Utils.LoadDropDownList(ddlProtocolo, "Value1", "Id", o, DropDownListAction.Select);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            highlightRows.Text = "";
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlTipoESO.SelectedValue.ToString() != "-1") Filters.Add("i_TypeEsoId==" + ddlTipoESO.SelectedValue);
            if (ddlAptitud.SelectedValue.ToString() != "-1") Filters.Add("i_AptitudeId==" + ddlAptitud.SelectedValue);
            if (!string.IsNullOrEmpty(txtTrabajador.Text)) Filters.Add("v_Trabajador.Contains(\"" + txtTrabajador.Text.ToUpper().Trim() + "\")");
            if (ddlProtocolo.SelectedValue.ToString() != "-1") Filters.Add("v_ProtocolId==" + "\"" + ddlProtocolo.SelectedValue + "\"");
            if (!string.IsNullOrEmpty(txtHCL.Text)) Filters.Add("v_HCL==" + "\"" + txtHCL.Text + "\"");
            //Filters.Add("v_CustomerOrganizationId==" + "\"" + Session["EmpresaClienteId"].ToString() + "\"");
            if (ddlEmpresa.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlEmpresa.SelectedValue.ToString().Split('|');
                Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
            }
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

            grdData.PageIndex = 0;
            this.BindGrid();

        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "v_ServiceId", strFilterExpression);
            grdData.DataBind();
        }

        private List<ServiceList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _serviceBL.GetAllAdminServicios(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1));

            lblContador.Text = "Se encontraron " + _objData.Count().ToString() + " registros";
            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            return _objData;
        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProtocolBL oProtocolBL = new ProtocolBL();
            if (ddlEmpresa.SelectedValue.ToString() != "-1")
            {
                var x = ddlEmpresa.SelectedValue.ToString().Split('|');
                var o = oProtocolBL.DevolverProtocolosPorEmpresa(x[0].ToString(), x[1].ToString());

                Utils.LoadDropDownList(ddlProtocolo, "v_Name", "v_ProtocolId", o, DropDownListAction.Select);
            }
         
        }

        protected void grdData_RowClick(object sender, GridRowClickEventArgs e)
        {
            LlenarLista();
        }

        private void LlenarLista()
        {
             List<MyListWeb> lista = new List<MyListWeb>();
             int selectedCount = grdData.SelectedRowIndexArray.Length;
             if (selectedCount > 0)
             {
                 for (int i = 0; i < selectedCount; i++)
                 {
                     int rowIndex = grdData.SelectedRowIndexArray[i];

                     var dataKeys = grdData.DataKeys[rowIndex];

                     //if (dataKeys[6].ToString() == "1")
                     //{
                     //    Alert.ShowInTop("Este registro no tiene APTITUD. No puede generarse sus reportes");
                     //}
                     //else
                     //{
                         lista.Add(new MyListWeb
                         {
                             IdServicio = dataKeys[0].ToString(),
                             i_StatusLiquidation = dataKeys[5] == null ? (int?)null : int.Parse(dataKeys[5].ToString())

                         });
                     //}
                    
                 }
                 Session["objListaCambioFecha"] = lista;
             }
             else
             {
                 Session["objListaCambioFecha"] = null;
             }

        }

        protected void grdData_RowCommand(object sender, GridCommandEventArgs e)
        {
            List<ServiceComponentList> ConsolidadoReportes = new List<ServiceComponentList>();
            List<ServiceComponentList> serviceComponents = new List<ServiceComponentList>();
            List<ServiceComponentList> ListaOrdenada = new List<ServiceComponentList>();
            List<string> reportesId = new List<string>();
            OperationResult objOperationResult = new OperationResult();
            List<ServiceComponentList> ListaFinalOrdena = new List<ServiceComponentList>();

            string[] InformeAnexo3121 = new string[] 
                    { 
                        Constants.EXAMEN_FISICO_ID
               
                    };
            string[] InformeFisico7C1 = new string[] 
                    { 
                        Constants.EXAMEN_FISICO_7C_ID               
                    };

            int index = e.RowIndex;
            var dataKeys = grdData.DataKeys[index];
            Session["ServiceId"] = dataKeys[0].ToString();
            Session["PersonId"] = dataKeys[1].ToString();

            if (dataKeys[6].ToString() == "1")
            {
                Alert.ShowInTop("El servicio aún no tiene una Aptitud Ocupacional. Revise, y vuelva a intentarlo.");
                return;
            }

            //var alert = _serviceBL.GetServiceComponentsCulminados(ref objOperationResult, Session["ServiceId"].ToString());
            //if (alert != null && alert.Count > 0)// consulta trae datos.... examenes que no estan evaluados
            //{
            //    Alert.ShowInTop("Algunos exámenes médicos, aún no se culminan. Revise, y vuelva a intentarlo.");
            //    return;
            //}

            if (e.CommandName == "GenerarReportes")
            {
                var ListaOrdenReportes = oOrganizationBL.GetOrdenReportes(ref objOperationResult, "N009-OO000000052");
                if (ListaOrdenReportes.Count > 0)
                {
                    serviceComponents = new List<ServiceComponentList>();
                    serviceComponents = _serviceBL.GetServiceComponentsForManagementReport(Session["ServiceId"].ToString());

                    serviceComponents.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "CONSENTIMIENTO INFORMADO ", v_ComponentId = Constants.CONSENTIMIENTO_INFORMADO });
                    serviceComponents.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD SIN Diagnósticos ", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX });
                    serviceComponents.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD EMPRESARIAL ", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL });
                    serviceComponents.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD });
                    serviceComponents.Add(new ServiceComponentList { Orden = 3, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR 1", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR });
                    serviceComponents.Add(new ServiceComponentList { Orden = 4, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR 2", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR_2 });
                    serviceComponents.Add(new ServiceComponentList { Orden = 4, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR 3", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR_3 });
                    serviceComponents.Add(new ServiceComponentList { Orden = 27, v_ComponentName = "INFORME DE LABORATORIO", v_ComponentId = Constants.INFORME_LABORATORIO_CLINICO });

                    var ResultadoAnexo312 = serviceComponents.FindAll(p => InformeAnexo3121.Contains(p.v_ComponentId)).ToList();
                    if (ResultadoAnexo312.Count() != 0)
                    {
                        serviceComponents.Add(new ServiceComponentList { Orden = 5, v_ComponentName = "ANEXO 312", v_ComponentId = Constants.INFORME_ANEXO_312 });
                    }
                    var ResultadoFisico7C = serviceComponents.FindAll(p => InformeFisico7C1.Contains(p.v_ComponentId)).ToList();
                    if (ResultadoFisico7C.Count() != 0)
                    {
                        serviceComponents.Add(new ServiceComponentList { Orden = 6, v_ComponentName = "ANEXO 7C", v_ComponentId = Constants.INFORME_ANEXO_7C });
                    }
                    serviceComponents.Add(new ServiceComponentList { Orden = 7, v_ComponentName = "HISTORIA OCUPACIONAL", v_ComponentId = Constants.INFORME_HISTORIA_OCUPACIONAL });
            
                    ListaOrdenada = new List<ServiceComponentList>();
                    ServiceComponentList oServiceComponentList = null;
                    foreach (var item in ListaOrdenReportes)
                    {
                        oServiceComponentList = new ServiceComponentList();
                        oServiceComponentList.v_ComponentName = item.v_NombreReporte;
                        oServiceComponentList.v_ComponentId = item.v_ComponenteId + '|' + item.i_NombreCrystalId;
                        ListaOrdenada.Add(oServiceComponentList);
                    }

                    foreach (var item in ListaOrdenada)
                    {
                        var array = item.v_ComponentId.Split('|');
                        foreach (var item1 in serviceComponents)
                        {
                            if (array[0].ToString() == item1.v_ComponentId)
                            {
                                ListaFinalOrdena.Add(item);
                            }
                        }
                    }
                    foreach (var item1 in ListaFinalOrdena)
                    {
                        reportesId.Add(item1.v_ComponentId);
                    }

                    GenerarReportesByService(Session["ServiceId"].ToString(), Session["PersonId"].ToString(), reportesId);
                    //actualiza Grilla de Servicios
                    BindGrid();
                }

            }

        }

        private void GenerarReportesByService(string pServicios, string pPersonId, List<string> pReportes)
        {
            OperationResult objOperationResult = new OperationResult();
            string ruta;
            List<string> filesNameToMerge = new List<string>();
            Session["filesNameToMerge"] = filesNameToMerge;
            MergeExPDF _mergeExPDF = new MergeExPDF();

            CrearReportesCrystal(pServicios, pPersonId, pReportes);

            ruta = WebConfigurationManager.AppSettings["rutaReportes"];

            var oService = _serviceBL.GetServiceShort(Session["ServiceId"].ToString());

            var x = ((List<string>)Session["filesNameToMerge"]).ToList();
            _mergeExPDF.FilesName = x;
            _mergeExPDF.DestinationFile = Server.MapPath("/TempMerge") + pServicios + ".pdf"; ;
            _mergeExPDF.DestinationFile = ruta + pServicios + ".pdf"; ;
            _mergeExPDF.Execute();


            //Cambiar de estado a generado de reportes
            _serviceBL.UpdateStatusPreLiquidation(ref objOperationResult, 2, Session["ServiceId"].ToString(), ((ClientSession)Session["objClientSession"]).GetAsList());
        }

        private void CrearReportesCrystal(string serviceId, string pPacienteId, List<string> reportesId)
        {
            OperationResult objOperationResult = new OperationResult();
            //MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();
            MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();
            string ruta;
            ruta = WebConfigurationManager.AppSettings["rutaReportes"];

            List<string> _filesNameToMerge = new List<string>();



            foreach (var com in reportesId)
            {
                //string CompnenteId = "";
                int IdCrystal = 0;
                //Obtener el Id del componente 

                var array = com.Split('|');

                if (array.Count() == 1)
                {
                    IdCrystal = 0;
                }
                else if (array[1] == "")
                {
                    IdCrystal = 0;
                }
                else
                {
                    IdCrystal = int.Parse(array[1].ToString());
                }

                ChooseReport(array[0], serviceId, pPacienteId, IdCrystal);


            }


            #region Adjuntar Archivos Adjuntos

            _filesNameToMerge = ((List<string>)Session["filesNameToMerge"]).ToList();
            string rutaInterconsulta = WebConfigurationManager.AppSettings["Interconsulta"];
            ////string rutaInterconsulta = Common.Utils.GetApplicationConfigValue("Interconsulta").ToString();

            List<string> files = Directory.GetFiles(rutaInterconsulta, "*.pdf").ToList();
            var o = _serviceBL.GetServiceShort(serviceId);

            var Resultado = files.Find(p => p == rutaInterconsulta + serviceId + "-" + o.Paciente + ".pdf");
            if (Resultado != null)
            {
                _filesNameToMerge.Add(rutaInterconsulta + _serviceId + "-" + o.Paciente + ".pdf");
                Session["filesNameToMerge"] = _filesNameToMerge;
            }


            var ListaPdf = _serviceBL.GetFilePdfsByServiceId(ref objOperationResult, _serviceId);
            if (ListaPdf != null)
            {
                if (ListaPdf.ToList().Count != 0)
                {
                    foreach (var item in ListaPdf)
                    {
                        var multimediaFile = _multimediaFileBL.GetMultimediaFileById(ref objOperationResult, item.v_MultimediaFileId);
                        var path = ruta + _serviceId + "-" + item.v_FileName;
                        File.WriteAllBytes(path, multimediaFile.ByteArrayFile);
                        _filesNameToMerge.Add(path);
                        Session["filesNameToMerge"] = _filesNameToMerge;



                    }
                }
            }

            //Obtner DNI y Fecha del servicio

            string Fecha = o.FechaServicio.Value.Day.ToString().PadLeft(2, '0') + o.FechaServicio.Value.Month.ToString().PadLeft(2, '0') + o.FechaServicio.Value.Year.ToString();
            DirectoryInfo rutaOrigen = null;


            //ELECTRO
            rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgEKGOrigen"].ToString());
            //rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgEKGOrigen").ToString());
            FileInfo[] files1 = rutaOrigen.GetFiles();

            foreach (FileInfo file in files1)
            {
                if (file.ToString().Count() > 16)
                {
                    if (file.ToString().Substring(0, 17) == o.DNI + "-" + Fecha)
                    {
                        _filesNameToMerge.Add(rutaOrigen + file.ToString());
                        Session["filesNameToMerge"] = _filesNameToMerge;
                    };
                }
            }


            //ESPIRO
            rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgESPIROOrigen"].ToString());
            //rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgESPIROOrigen").ToString());
            FileInfo[] files2 = rutaOrigen.GetFiles();

            foreach (FileInfo file in files2)
            {
                if (file.ToString().Count() > 16)
                {
                    if (file.ToString().Substring(0, 17) == o.DNI + "-" + Fecha)
                    {
                        _filesNameToMerge.Add(rutaOrigen + file.ToString());
                        Session["filesNameToMerge"] = _filesNameToMerge;
                    };
                }
            }

            ////RX
            //rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgRxOrigen").ToString());
            //FileInfo[] files3 = rutaOrigen.GetFiles();

            //foreach (FileInfo file in files3)
            //{
            //    if (file.ToString().Count() > 16)
            //    {
            //        if (file.ToString().Substring(0, 17) == o.DNI + "-" + Fecha)
            //        {
            //            _filesNameToMerge.Add(rutaOrigen + file.ToString());
            //        };
            //    }
            //}

            //LAB
            rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgLABOrigen"].ToString());
            //rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgLABOrigen").ToString());
            FileInfo[] files4 = rutaOrigen.GetFiles();

            foreach (FileInfo file in files4)
            {
                if (file.ToString().Count() > 16)
                {
                    if (file.ToString().Substring(0, 17) == o.DNI + "-" + Fecha)
                    {
                        _filesNameToMerge.Add(rutaOrigen + file.ToString());
                        Session["filesNameToMerge"] = _filesNameToMerge;
                    };
                }
            }


            //var x = Session["filesNameToMerge"];
            //_mergeExPDF.FilesName = x;
            //_mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + _serviceId + ".pdf"; ;
            //_mergeExPDF.DestinationFile = ruta + _serviceId + ".pdf"; ;
            //_mergeExPDF.Execute();

            #endregion
        }

        private void ChooseReport(string componentId, string serviceId, string pPacienteId, int pintIdCrystal)
        {
            string ruta;
            string _serviceId;
            string _pacientId;

            ruta = WebConfigurationManager.AppSettings["rutaReportes"];

            DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
            OperationResult objOperationResult = new OperationResult();
            _serviceId = serviceId;
            _pacientId = pPacienteId;
            ReportDocument rp;
            DataSet dsGetRepo = null;

            switch (componentId)
            {
                case Constants.INFORME_CERTIFICADO_APTITUD:
                    var INFORME_CERTIFICADO_APTITUD = new ServiceBL().GetAptitudeCertificate(ref objOperationResult, _serviceId);

                    if (INFORME_CERTIFICADO_APTITUD == null)
                    {
                        break;
                    }
                    DataSet ds1 = new DataSet();

                    DataTable dtINFORME_CERTIFICADO_APTITUD = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD);

                    dtINFORME_CERTIFICADO_APTITUD.TableName = "AptitudeCertificate";
                    ds1.Tables.Add(dtINFORME_CERTIFICADO_APTITUD);

                    var TipoServicio = INFORME_CERTIFICADO_APTITUD[0].i_EsoTypeId;

                    if (TipoServicio == ((int)TypeESO.Retiro).ToString())
                    {
                        rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crOccupationalMedicalAptitudeCertificateRetiros();
                        rp.SetDataSource(ds1);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        Session["filesNameToMerge"] = _filesNameToMerge;
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();

                    }
                    else
                    {
                        if (INFORME_CERTIFICADO_APTITUD[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs)
                        {
                            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crCertficadoObservado();
                            rp.SetDataSource(ds1);
                            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            objDiskOpt = new DiskFileDestinationOptions();
                            objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf";
                            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                            Session["filesNameToMerge"] = _filesNameToMerge;
                            rp.ExportOptions.DestinationOptions = objDiskOpt;
                            rp.Export();
                            rp.Close();

                        }
                        else
                        {
                            //QUITAR
                            //rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crOccupationalMedicalAptitudeCertificate();
                            //rp.SetDataSource(ds1);

                            //rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            //rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            //objDiskOpt = new DiskFileDestinationOptions();
                            //objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf";
                            //_filesNameToMerge.Add(objDiskOpt.DiskFileName);
                            //Session["filesNameToMerge"] = _filesNameToMerge;
                            //rp.ExportOptions.DestinationOptions = objDiskOpt;
                            //rp.Export();
                            //rp.Close();
                        }
                    }



                 
                    break;
                case Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL:

                    var INFORME_CERTIFICADO_APTITUD_EMPRESARIAL = new ServiceBL().GetCAPE(_serviceId);
                    dsGetRepo = new DataSet();

                    DataTable dt_INFORME_CERTIFICADO_APTITUD_EMPRESARIAL = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD_EMPRESARIAL);

                    dt_INFORME_CERTIFICADO_APTITUD_EMPRESARIAL.TableName = "AptitudeCertificate";

                    dsGetRepo.Tables.Add(dt_INFORME_CERTIFICADO_APTITUD_EMPRESARIAL);

                    TipoServicio = INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].i_EsoTypeId;

                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crCertificadoDeAptitudEmpresarial();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    break;


                case Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX:
                    //var Path3 = Application.StartupPath;
                    var INFORME_CERTIFICADO_APTITUD_SIN_DX = new ServiceBL().GetCAPSD(_serviceId, "");
                    dsGetRepo = new DataSet();
                    DataTable dtINFORME_CERTIFICADO_APTITUD_SIN_DX = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD_SIN_DX);
                    dtINFORME_CERTIFICADO_APTITUD_SIN_DX.TableName = "AptitudeCertificate";
                    dsGetRepo.Tables.Add(dtINFORME_CERTIFICADO_APTITUD_SIN_DX);

                    if (INFORME_CERTIFICADO_APTITUD_SIN_DX == null)
                    {
                        break;
                    }
                    TipoServicio = INFORME_CERTIFICADO_APTITUD_SIN_DX[0].i_EsoTypeId;
                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crCertificadoDeAptitudSinDX();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    break;

                case "N005-ME000000027":
                    //var Path3 = Application.StartupPath;
                    var ODONTOGRAMA_CI = new ServiceBL().GetOdontograma_CI(_serviceId, "N005-ME000000027");
                    dsGetRepo = new DataSet();
                    DataTable dt_ODONTOGRAMA_CI = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ODONTOGRAMA_CI);
                    dt_ODONTOGRAMA_CI.TableName = "dtOdonto_CI";
                    dsGetRepo.Tables.Add(dt_ODONTOGRAMA_CI);

                    if (ODONTOGRAMA_CI == null)
                    {
                        break;
                    }
                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crOdontoCI();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000027" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    break;

                case Constants.ALTURA_7D_ID:

                    var AscensoAlturas = new ServiceBL().ReportAscensoGrandesAlturas(_serviceId, Constants.ALTURA_7D_ID);
                    var FuncionesVitales = new ServiceBL().ReportFuncionesVitales(_serviceId, Constants.FUNCIONES_VITALES_ID);
                    var Antropometria = new ServiceBL().ReportAntropometria(_serviceId, Constants.ANTROPOMETRIA_ID);

                    dsGetRepo = new DataSet("Anexo7D");

                    DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(AscensoAlturas);
                    dt.TableName = "dtAnexo7D";
                    dsGetRepo.Tables.Add(dt);

                    DataTable dt1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(FuncionesVitales);
                    dt1.TableName = "dtFuncionesVitales";
                    dsGetRepo.Tables.Add(dt1);

                    DataTable dt2 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Antropometria);
                    dt2.TableName = "dtAntropometria";
                    dsGetRepo.Tables.Add(dt2);

                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crAnexo7D();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ALTURA_7D_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    break;

                case Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID:
                    var TOXICOLOGICO_COCAINA_MARIHUANA_ID = new ServiceBL().GetReportCocainaMarihuana(_serviceId, Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_TOXICOLOGICO_COCAINA_MARIHUANA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(TOXICOLOGICO_COCAINA_MARIHUANA_ID);
                    dt_TOXICOLOGICO_COCAINA_MARIHUANA_ID.TableName = "dtAutorizacionDosajeDrogas";
                    dsGetRepo.Tables.Add(dt_TOXICOLOGICO_COCAINA_MARIHUANA_ID);

                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crCocainaMarihuana01();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID + "01.pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();


                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crCocainaMarihuana02();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID + "02.pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;

                case Constants.ESPIROMETRIA_ID:
                    var ESPIROMETRIA_ID = new ServiceBL().GetReportCuestionarioEspirometria(_serviceId, Constants.ESPIROMETRIA_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_ESPIROMETRIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ESPIROMETRIA_ID);
                    dt_ESPIROMETRIA_ID.TableName = "dtCuestionarioEspirometria";
                    dsGetRepo.Tables.Add(dt_ESPIROMETRIA_ID);
                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crCuestionarioEspirometria();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ESPIROMETRIA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.ELECTROCARDIOGRAMA_ID:

                    var ELECTROCARDIOGRAMA_ID = new ServiceBL().GetReportEstudioElectrocardiografico(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_ELECTROCARDIOGRAMA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ELECTROCARDIOGRAMA_ID);
                    dt_ELECTROCARDIOGRAMA_ID.TableName = "dtEstudioElectrocardiografico";
                    dsGetRepo.Tables.Add(dt_ELECTROCARDIOGRAMA_ID);
                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crEstudioElectrocardiografico();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ELECTROCARDIOGRAMA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.HISTORIA_CLINICA_PSICOLOGICA_ID:
                    var HISTORIA_CLINICA_PSICOLOGICA_ID = new ServiceBL().GetHistoriaClinicaPsicologica(_serviceId, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_HISTORIA_CLINICA_PSICOLOGICA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(HISTORIA_CLINICA_PSICOLOGICA_ID);
                    dt_HISTORIA_CLINICA_PSICOLOGICA_ID.TableName = "dtHistoriaClinicaPsicologica";

                    dsGetRepo.Tables.Add(dt_HISTORIA_CLINICA_PSICOLOGICA_ID);

                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crHistoriaClinicaPsicologica();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();


                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crHistoriaClinicaPsicologica2();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + "2" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;

                case Constants.INFORME_HISTORIA_OCUPACIONAL:

                    var INFORME_HISTORIA_OCUPACIONAL = new ServiceBL().ReportHistoriaOcupacional(_serviceId);

                    dsGetRepo = new DataSet();
                    DataTable dtINFORME_HISTORIA_OCUPACIONAL = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_HISTORIA_OCUPACIONAL);
                    dtINFORME_HISTORIA_OCUPACIONAL.TableName = "HistoriaOcupacional";
                    dsGetRepo.Tables.Add(dtINFORME_HISTORIA_OCUPACIONAL);
                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crHistoriaOcupacional();
                    rp.SetDataSource(dsGetRepo);

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_HISTORIA_OCUPACIONAL + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.OIT_ID:
                    var OIT_ID = new ServiceBL().ReportInformeRadiografico(_serviceId, Constants.OIT_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_OIT_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OIT_ID);
                    dt_OIT_ID.TableName = "dtInformeRadiografico";
                    dsGetRepo.Tables.Add(dt_OIT_ID);

                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crInformeRadiograficoOIT();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.OIT_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.RX_TORAX_ID:


                    var RX_TORAX_ID = new ServiceBL().ReportRadiologico(_serviceId, Constants.RX_TORAX_ID);
                    dsGetRepo = new DataSet();

                    DataTable dt_RX_TORAX_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(RX_TORAX_ID);

                    dt_RX_TORAX_ID.TableName = "dtRadiologico";

                    dsGetRepo.Tables.Add(dt_RX_TORAX_ID);
                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crInformeRadiologico();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.RX_TORAX_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.SINTOMATICO_RESPIRATORIO:
                    var SINTOMATICO_RESPIRATORIO = new ServiceBL().ReporteSintomaticoRespiratorio(_serviceId, Constants.SINTOMATICO_RESPIRATORIO);

                    dsGetRepo = new DataSet();

                    DataTable dt_SINTOMATICO_RESPIRATORIO = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(SINTOMATICO_RESPIRATORIO);
                    dt_SINTOMATICO_RESPIRATORIO.TableName = "dtSintomaticoRes";
                    dsGetRepo.Tables.Add(dt_SINTOMATICO_RESPIRATORIO);

                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crSintomaticoResp();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();

                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.SINTOMATICO_RESPIRATORIO + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;

                case Constants.PSICOLOGIA_ID:
                    var PSICOLOGIA_ID = new PacientBL().GetFichaPsicologicaOcupacional(_serviceId);

                    dsGetRepo = new DataSet();

                    DataTable dt_PSICOLOGIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(PSICOLOGIA_ID);

                    dt_PSICOLOGIA_ID.TableName = "InformePsico";

                    dsGetRepo.Tables.Add(dt_PSICOLOGIA_ID);

                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.InformePsicologicoOcupacional();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.PSICOLOGIA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    break;


                case Constants.CONSENTIMIENTO_INFORMADO:
                    var CONSENTIMIENTO_INFORMADO = new PacientBL().GetReportConsentimiento(_serviceId);

                    dsGetRepo = new DataSet();
                    DataTable dtCONSENTIMIENTO_INFORMADO = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(CONSENTIMIENTO_INFORMADO);
                    dtCONSENTIMIENTO_INFORMADO.TableName = "dtConsentimiento";
                    dsGetRepo.Tables.Add(dtCONSENTIMIENTO_INFORMADO);
                    if (pintIdCrystal == 35)
                    {
                        rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crConsentimiento_Inter();
                    }
                    else
                    {
                        rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crConsentimiento_Inter();
                    }

                    //rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crConsentimiento_Inter();
                    rp.SetDataSource(dsGetRepo);

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.CONSENTIMIENTO_INFORMADO + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;


                case "N005-ME000000117": //Altura estructural 18  CI

                    var ALTURA_18_ID = new ServiceBL().GetAlturaEstructural_CI(_serviceId, "N005-ME000000117");

                    dsGetRepo = new DataSet();

                    DataTable dtALTURA_18_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ALTURA_18_ID);
                    dtALTURA_18_ID.TableName = "dtAltura18_CI";
                    dsGetRepo.Tables.Add(dtALTURA_18_ID);

                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crAltura1_8_Inter();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000117" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    break;

                case "N005-ME000000005": //Audiometria CI
                    var serviceBL = new ServiceBL();
                    var dsAudiometria = new DataSet();
                    var dxList_CI = serviceBL.GetDiagnosticRepositoryByComponent(_serviceId, "N005-ME000000005");
                    if (dxList_CI.Count == 0)
                    {
                        DiagnosticRepositoryList oDiagnosticRepositoryList = new DiagnosticRepositoryList();
                        List<DiagnosticRepositoryList> Lista = new List<DiagnosticRepositoryList>();
                        oDiagnosticRepositoryList.v_ServiceId = "Sin Id";
                        oDiagnosticRepositoryList.v_DiseasesName = "Sin Alteración";
                        oDiagnosticRepositoryList.v_DiagnosticRepositoryId = "Sin Id";
                        Lista.Add(oDiagnosticRepositoryList);
                        var dtDx = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Lista);
                        dtDx.TableName = "dtDiagnostic";
                        dsAudiometria.Tables.Add(dtDx);
                    }
                    else
                    {
                        List<DiagnosticRepositoryList> ListaDxsAudio = new List<DiagnosticRepositoryList>();
                        DiagnosticRepositoryList oDiagnosticRepositoryList;
                        foreach (var item in dxList_CI)
                        {
                            oDiagnosticRepositoryList = new DiagnosticRepositoryList();

                            oDiagnosticRepositoryList.v_DiseasesName = item.v_DiseasesName;
                            oDiagnosticRepositoryList.v_DiagnosticRepositoryId = item.v_DiagnosticRepositoryId;
                            oDiagnosticRepositoryList.v_ServiceId = item.v_ServiceId;
                            ListaDxsAudio.Add(oDiagnosticRepositoryList);
                        }


                        var dtDx = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ListaDxsAudio);
                        dtDx.TableName = "dtDiagnostic";
                        dsAudiometria.Tables.Add(dtDx);
                    }


                    var recom_CI = dxList_CI.SelectMany(s1 => s1.Recomendations).ToList();
                    if (recom_CI.Count == 0)
                    {
                        Sigesoft.Node.WinClient.BE.RecomendationList oRecomendationList = new Sigesoft.Node.WinClient.BE.RecomendationList();
                        List<Sigesoft.Node.WinClient.BE.RecomendationList> Lista = new List<Sigesoft.Node.WinClient.BE.RecomendationList>();

                        oRecomendationList.v_ServiceId = "Sin Id";
                        oRecomendationList.v_RecommendationName = "Sin Recomendaciones";
                        oRecomendationList.v_DiagnosticRepositoryId = "Sin Id";
                        Lista.Add(oRecomendationList);
                        var dtReco = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Lista);
                        dtReco.TableName = "dtRecomendation";
                        dsAudiometria.Tables.Add(dtReco);

                    }
                    else
                    {
                        var dtReco = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(recom_CI);
                        dtReco.TableName = "dtRecomendation";
                        dsAudiometria.Tables.Add(dtReco);
                    }


                    //-------******************************************************************************************

                    var audioUserControlList_CI = serviceBL.ReportAudiometriaUserControl(_serviceId, "N005-ME000000005");
                    var audioCabeceraList_CI = serviceBL.ReportAudiometria_CI(_serviceId, "N005-ME000000005");
                    var dtAudiometriaUserControl_CI = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioUserControlList_CI);
                    var dtCabecera_CI = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioCabeceraList_CI);

                    dtCabecera_CI.TableName = "dtAudiometria";
                    dtAudiometriaUserControl_CI.TableName = "dtAudiometriaUserControl";

                    dsAudiometria.Tables.Add(dtCabecera_CI);
                    dsAudiometria.Tables.Add(dtAudiometriaUserControl_CI);

                    if (pintIdCrystal == 11)
                    {
                        rp = new AdministradorServicios.crFichaAudiometria_inter();
                    }
                    else if (pintIdCrystal == 12)
                    {
                        rp = new AdministradorServicios.crFichaAudiometria_inter();
                    }
                    else
                    {
                        rp = new AdministradorServicios.crFichaAudiometria_inter();
                    }


                    rp.SetDataSource(dsAudiometria);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000005" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();



                    // Historia Ocupacional Audiometria
                    var dataListForReport_1_CI = new ServiceBL().ReportHistoriaOcupacionalAudiometria(_serviceId);

                    if (dataListForReport_1_CI.Count != 0)
                    {
                        dsGetRepo = new DataSet();
                        DataTable dt_dataListForReport_1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport_1_CI);

                        dt_dataListForReport_1.TableName = "dtHistoriaOcupacional";

                        dsGetRepo.Tables.Add(dt_dataListForReport_1);

                        rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crHistoriaOcupacionalAudiometria();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + "AUDIOMETRIA_ID_HISTORIA" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        Session["filesNameToMerge"] = _filesNameToMerge;
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                    }

                    break;

                case "N005-ME000000116":
                    var TAMIZAJE_DERMATOLOGIO_ID = new ServiceBL().ReportTamizajeDermatologico_CI(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID_CI);

                    dsGetRepo = new DataSet();
                    DataTable dt_TAMIZAJE_DERMATOLOGIO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(TAMIZAJE_DERMATOLOGIO_ID);
                    dt_TAMIZAJE_DERMATOLOGIO_ID.TableName = "dtEvaDermatologicoCI";
                    dsGetRepo.Tables.Add(dt_TAMIZAJE_DERMATOLOGIO_ID);

                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crEvaluacionDermatologicaCI();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.TAMIZAJE_DERMATOLOGIO_ID_CI + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;


                case "N005-ME000000028":
                    var OFTALMOLOGIA_CI_ID = new ServiceBL().ReportOftalmologia_CI(_serviceId, "N005-ME000000028");

                    dsGetRepo = new DataSet();
                    DataTable dt_OFTALMOLOGIA_CI_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OFTALMOLOGIA_CI_ID);
                    dt_OFTALMOLOGIA_CI_ID.TableName = "dtOftalomologia_CI";
                    dsGetRepo.Tables.Add(dt_OFTALMOLOGIA_CI_ID);

                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crOftalmologia_inter();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000028" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case "N005-ME000000046":
                    var OSTEOMUCULAR_CI_ID = new ServiceBL().GetMusculoEsqueletico_ClinicaInternacional(_serviceId, "N005-ME000000046");
                    dsGetRepo = new DataSet();
                    DataTable dt_OSTEOMUSCULAR_CI_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OSTEOMUCULAR_CI_ID);
                    dt_OSTEOMUSCULAR_CI_ID.TableName = "DataTable1";
                    dsGetRepo.Tables.Add(dt_OSTEOMUSCULAR_CI_ID);

                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crMuscoloEsqueletico();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000046" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();


                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crMuscoloEsqueletico2();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000046" + "-2.pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();


                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crMuscoloEsqueletico3();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000046" + "-3.pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.INFORME_ANEXO_312:
                    GenerateAnexo312(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.INFORME_ANEXO_312)), _serviceId, _pacientId);
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    break;

                case Constants.INFORME_LABORATORIO_CLINICO:
                    GenerateLaboratorioReport(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.INFORME_LABORATORIO_CLINICO)), _serviceId, _pacientId, "");
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    break;

                case Constants.INFORME_FICHA_MEDICA_TRABAJADOR_3:
                    CreateFichaMedicaTrabajador3(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR_3)), _serviceId, _pacientId, "");
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    break;
                default:
                    break;
            }
        }
      
        private void GenerateAnexo312(string pathFile, string ServicioId, string PacienteId)
        {
           
            var filiationData = _pacientBL.GetPacientReportEPS(ServicioId);
            var _listAtecedentesOcupacionales = _historyBL.GetHistoryReport(PacienteId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(PacienteId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(PacienteId);
            var _DataService = _serviceBL.GetServiceReport(ServicioId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(PacienteId);

            var Antropometria = _serviceBL.ValoresComponente(ServicioId, Constants.ANTROPOMETRIA_ID);
            var FuncionesVitales = _serviceBL.ValoresComponente(ServicioId, Constants.FUNCIONES_VITALES_ID);
            var ExamenFisico = _serviceBL.ValoresComponente(ServicioId, Constants.EXAMEN_FISICO_ID);
            var Oftalmologia = _serviceBL.ValoresComponente(ServicioId, "N005-ME000000028");
            var Psicologia = _serviceBL.ValoresExamenComponete(ServicioId, Constants.PSICOLOGIA_ID, 195);
            var OIT = _serviceBL.ValoresExamenComponete(ServicioId, Constants.OIT_ID, 211);
            var RX = _serviceBL.ValoresExamenComponete(ServicioId, Constants.RX_TORAX_ID, 135);
            var Laboratorio = _serviceBL.ValoresComponente(ServicioId, Constants.INFORME_LABORATORIO_ID);
            //var Audiometria = _serviceBL.ValoresComponente(ServicioId, Constants.AUDIOMETRIA_ID);
            var Audiometria = _serviceBL.GetDiagnosticForAudiometria(ServicioId, Constants.AUDIOMETRIA_ID);
            var Espirometria = _serviceBL.ValoresExamenComponete(ServicioId, Constants.ESPIROMETRIA_ID, 210);
            var _DiagnosticRepository = _serviceBL.GetServiceDisgnosticsReports(ServicioId);
            var _Recomendation = _serviceBL.GetServiceRecommendationByServiceId(ServicioId);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(ServicioId);
            var ValoresDxLab = _serviceBL.ValoresComponenteAMC_(ServicioId, 1);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var TestIhihara = _serviceBL.ValoresComponente(ServicioId, Constants.TEST_ISHIHARA_ID);
            var TestEstereopsis = _serviceBL.ValoresComponente(ServicioId, Constants.TEST_ESTEREOPSIS_ID);


            FichaMedicaOcupacional312_CI.CreateFichaMedicalOcupacional312Report(_DataService,
                        filiationData, _listAtecedentesOcupacionales, _listaPatologicosFamiliares,
                        _listMedicoPersonales, _listaHabitoNocivos, Antropometria, FuncionesVitales,
                        ExamenFisico, Oftalmologia, Psicologia, OIT, RX, Laboratorio, Audiometria, Espirometria,
                        _DiagnosticRepository, _Recomendation, _ExamenesServicio, ValoresDxLab, MedicalCenter, TestIhihara, TestEstereopsis,
                        pathFile);

        }

        private void GenerateInformeMedicoTrabajadorInternacional(string pathFile, string ServicioId, string PacienteId)
        {
                  }

        private void GenerateInformeMedicoTrabajador(string pathFile, string ServicioId, string PacienteId, string EmpresaCliente)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var personMedicalHistory = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var noxiousHabit = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var familyMedicalAntecedent = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var anamnesis = _serviceBL.GetAnamnesisReport(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateMedicalReportForTheWorker(filiationData,
                                            personMedicalHistory,
                                            noxiousHabit,
                                            familyMedicalAntecedent,
                                            anamnesis,
                                            serviceComponents,
                                            diagnosticRepository,
                                            "",
                                            MedicalCenter,
                                            pathFile);

        }

        private void GenerateAnexo7C(string pathFile, string ServicioId, string PacienteId, string EmpresaCliente)
        {
       

        }
        

        private void GenerateInformeExamenClinico(string pathFile, string ServicioId, string PacienteId, string EmpresaCliente)
        {
          
        }

        private void GenerateLaboratorioReport(string pathFile, string ServicioId, string PacienteId, string EmpresaCliente)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(ServicioId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(ServicioId);

            LaboratorioReport.CreateLaboratorioReport(filiationData, serviceComponents, MedicalCenter, pathFile);
        }

        private void CreateFichaMedicaTrabajador3(string pathFile, string ServicioId, string PacienteId, string EmpresaCliente)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(ServicioId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport_TODOS(ServicioId);
            var doctoPhisicalExam = _serviceBL.GetDoctoPhisicalExam(ServicioId);
            var ComponetesConcatenados = _pacientBL.DevolverComponentesConcatenados(ServicioId);
            var ComponentesLaboratorioConcatenados = _pacientBL.DevolverComponentesLaboratorioConcatenados(ServicioId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(ServicioId);
            var Restricciton = _serviceBL.GetRestrictionByServiceId(ServicioId);
            InformeTrabajador3.CreateFichaMedicaTrabajador3(filiationData, doctoPhisicalExam, diagnosticRepository, MedicalCenter, ComponetesConcatenados, ComponentesLaboratorioConcatenados, serviceComponents, Restricciton, pathFile);
        }

        protected void WinFechaServicio_Close(object sender, WindowCloseEventArgs e)
        {
            btnFilter_Click(sender, e);
        }

        protected void grdData_RowDataBound(object sender, GridRowEventArgs e)
        {
            ServiceList row = (ServiceList)e.DataItem;
            if (row.i_StatusLiquidation == (int)PreLiquidationStatus.Generada)
            {
                highlightRows.Text += e.RowIndex.ToString() + ",";
            }
        }

        protected void winEdit1_Close(object sender, WindowCloseEventArgs e)
        {

        }
  
    }
}