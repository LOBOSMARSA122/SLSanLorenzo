using CrystalDecisions.Shared;
using NetPdf;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAgendaCampaña : Form
    {
        private List<string> _filesNameToMerge = new List<string>();
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        public frmAgendaCampaña()
        {
            InitializeComponent();
        }
        List<AuthorizedPersonList> _TempPacientList;
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            int Value = 0;
            bool Imported = true;
            int ErrorCounter = 0;
            DateTime ValueDateTime;
            StringBuilder sbMensaje = new StringBuilder();
            //if (_TempPacientList == null) return;
            if (_TempPacientList != null)
            {
                if (MessageBox.Show("Ya existe una lista de pacientes por agendar; ¿Desea reemplazarla?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Image Files (*.xls;*.xlsx)|*.xls;*.xlsx";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _TempPacientList = new List<AuthorizedPersonList>();

                var Ext = Path.GetExtension(openFileDialog1.FileName).ToUpper();

                try
                {
                    if (Ext == ".XLSX" || Ext == ".XLS")
                    {

                        Infragistics.Documents.Excel.Workbook workbook1 = Infragistics.Documents.Excel.Workbook.Load(openFileDialog1.FileName);

                        Infragistics.Documents.Excel.Worksheet worksheet1 = workbook1.Worksheets["PLANTILLA"];

                        AuthorizedPersonList TempPacient;

                        int i = 4;
                        int ii = 4;
                        //Validar que el excel no esta vacio
                        while (worksheet1.Rows[ii].Cells[0].Value != null)
                        {
                            if (worksheet1.Rows[ii].Cells[0].Value == null || worksheet1.Rows[ii].Cells[1].Value == null || worksheet1.Rows[ii].Cells[2].Value == null)
                            {

                                for (int y = 0; y < 3; y++)
                                {
                                    if (worksheet1.Rows[i].Cells[0].Value == null )
                                    {
                                        Imported = false;
                                        sbMensaje.Append("Registro número : ");
                                        sbMensaje.Append(worksheet1.Rows[ii].Cells[0].Value);
                                        sbMensaje.Append(". El campo " + worksheet1.Rows[3].Cells[0].Value.ToString() + " no puede estar vacio");
                                        sbMensaje.Append("\n");
                                    }
                                    if (worksheet1.Rows[i].Cells[1].Value == null )
                                    {
                                        Imported = false;
                                        sbMensaje.Append("Registro número : ");
                                        sbMensaje.Append(worksheet1.Rows[ii].Cells[0].Value);
                                        sbMensaje.Append(". El campo " + worksheet1.Rows[3].Cells[1].Value.ToString() + " no puede estar vacio");
                                        sbMensaje.Append("\n");
                                    }
                                    if (worksheet1.Rows[i].Cells[2].Value == null)
                                    {
                                        Imported = false;
                                        sbMensaje.Append("Registro número : ");
                                        sbMensaje.Append(worksheet1.Rows[ii].Cells[0].Value);
                                        sbMensaje.Append(". El campo " + worksheet1.Rows[3].Cells[2].Value.ToString() + " no puede estar vacio");
                                        sbMensaje.Append("\n");
                                    }
                                    i++;
                                }
                            }
                            ii++;
                        }

                        if (Imported == false)
                        {
                            MessageBox.Show(sbMensaje.ToString(), "Corregir registros en blanco", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }


                        while (worksheet1.Rows[i].Cells[0].Value != null)
                        {
                            TempPacient = new AuthorizedPersonList();

                            if (worksheet1.Rows[i].Cells[0].Value != null)
                            {
                                TempPacient.i_Correlative = int.Parse(worksheet1.Rows[i].Cells[0].Value.ToString());
                                Imported = true;
                            }

                            //Nombre del Protocolo
                            if (worksheet1.Rows[i].Cells[1].Value != null)
                            {
                                TempPacient.v_ProtocolName = worksheet1.Rows[i].Cells[1].Value.ToString();
                                Imported = true;
                            }
                            else
                            {
                                ErrorCounter++;
                                Imported = false;
                                sbMensaje.Append("Registro número : ");
                                sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                sbMensaje.Append(". El campo Nombre del Protocolo es inválido");
                                sbMensaje.Append("\n");
                                i++;
                                continue;
                            }

                            //Nombres
                            if (worksheet1.Rows[i].Cells[2].Value != null)
                            {
                                TempPacient.v_FirstName = worksheet1.Rows[i].Cells[2].Value.ToString();
                                Imported = true;
                            }
                            else
                            {
                                ErrorCounter++;
                                Imported = false;
                                sbMensaje.Append("Registro número : ");
                                sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                sbMensaje.Append(". El campo Nombres es inválido");
                                sbMensaje.Append("\n");
                                i++;
                                continue;
                            }

                            //Apellido Paterno
                            if (worksheet1.Rows[i].Cells[3].Value != null)
                            {
                                TempPacient.v_FirstLastName = worksheet1.Rows[i].Cells[3].Value.ToString();
                                Imported = true;
                            }
                            else
                            {
                                //ErrorCounter++;
                                //Imported = false;
                                //sbMensaje.Append("Registro número : ");
                                //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                //sbMensaje.Append(". El campo Apellido Paterno es inválido");
                                //sbMensaje.Append("\n");
                                //i++;
                                //continue;
                            }
                            //Apellido Materno
                            if (worksheet1.Rows[i].Cells[4].Value != null)
                            {
                                TempPacient.v_SecondLastName = worksheet1.Rows[i].Cells[4].Value.ToString();
                                Imported = true;
                            }
                            else
                            {
                                //ErrorCounter++;
                                //Imported = false;
                                //sbMensaje.Append("Registro número : ");
                                //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                //sbMensaje.Append(". El campo Apellido Materno es inválido");
                                //sbMensaje.Append("\n");
                                //i++;
                                //continue;
                            }
                            //ID Tipo Documento
                            if (worksheet1.Rows[i].Cells[5].Value == null)
                            {
                                //ErrorCounter++;
                                //Imported = false;
                                //sbMensaje.Append("Registro número : ");
                                //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                //sbMensaje.Append(". El campo ID Tipo Documento es inválido");
                                //sbMensaje.Append("\n");
                                //i++;
                                //continue;
                                TempPacient.i_DocTypeId = -1;
                            }
                            if (worksheet1.Rows[i].Cells[5].Value != null)
                            {
                                if (int.TryParse(worksheet1.Rows[i].Cells[5].Value.ToString(), out  Value) == false)
                                {
                                    //ErrorCounter++;
                                    //Imported = false;
                                    //sbMensaje.Append("Registro número : ");
                                    //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                    //sbMensaje.Append(". El campo ID Tipo Documento es inválido");
                                    //sbMensaje.Append("\n");
                                    //i++;
                                    //continue;
                                }
                                else
                                {
                                    Imported = true;
                                    TempPacient.i_DocTypeId = int.Parse(worksheet1.Rows[i].Cells[5].Value.ToString());
                                }
                            }

                            //Nombre Tipo Documento
                            if (worksheet1.Rows[i].Cells[6].Value != null)
                            {
                                TempPacient.v_DocTypeName = worksheet1.Rows[i].Cells[6].Value.ToString();
                                Imported = true;
                            }
                            else
                            {
                                //ErrorCounter++;
                                //Imported = false;
                                //sbMensaje.Append("Registro número : ");
                                //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                //sbMensaje.Append(". El campo Nombre Tipo Documento es inválido");
                                //sbMensaje.Append("\n");
                                //i++;
                                //continue;
                                TempPacient.v_DocTypeName = "";
                            }
                            //Número Documento
                            if (worksheet1.Rows[i].Cells[7].Value != null)
                            {
                                if (worksheet1.Rows[i].Cells[5].Value.ToString() == "1") // DNI
                                {
                                    if (worksheet1.Rows[i].Cells[7].Value.ToString().Length != 8)
                                    {
                                        //ErrorCounter++;
                                        //Imported = false;
                                        //sbMensaje.Append("Registro número : ");
                                        //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                        //sbMensaje.Append(". El campo Número de DNI debe tener 8 dígitos");
                                        //sbMensaje.Append("\n");
                                        //i++;
                                        //continue;
                                    }
                                    else
                                    {
                                        Imported = true;
                                        TempPacient.v_DocNumber = worksheet1.Rows[i].Cells[7].Value.ToString();
                                    }

                                }
                                else if (worksheet1.Rows[i].Cells[5].Value.ToString() == "2") // PASAPORTE
                                {
                                    if (worksheet1.Rows[i].Cells[7].Value.ToString().Length != 9)
                                    {
                                        //ErrorCounter++;
                                        //Imported = false;
                                        //sbMensaje.Append("Registro número : ");
                                        //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                        //sbMensaje.Append(". El Número PASAPORTE debe tener 9 dígitos");
                                        //sbMensaje.Append("\n");
                                        //i++;
                                        //continue;
                                    }
                                    else
                                    {
                                        Imported = true;
                                        TempPacient.v_DocNumber = worksheet1.Rows[i].Cells[7].Value.ToString();
                                    }
                                }
                                else if (worksheet1.Rows[i].Cells[5].Value.ToString() == "3") // LICENCIA DE CONDUCIR
                                {
                                    if (worksheet1.Rows[i].Cells[7].Value.ToString().Length != 10)
                                    {
                                        //ErrorCounter++;
                                        //Imported = false;
                                        //sbMensaje.Append("Registro número : ");
                                        //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                        //sbMensaje.Append(". El Número LICENCIA DE CONDUCIR debe tener 10 dígitos");
                                        //sbMensaje.Append("\n");
                                        //i++;
                                        //continue;
                                    }
                                    else
                                    {
                                        Imported = true;
                                        TempPacient.v_DocNumber = worksheet1.Rows[i].Cells[7].Value.ToString();
                                    }
                                }
                                else if (worksheet1.Rows[i].Cells[5].Value.ToString() == "4")// CARNET DE EXTRANJERIA
                                {
                                    if (worksheet1.Rows[i].Cells[7].Value.ToString().Length != 11)
                                    {
                                        //ErrorCounter++;
                                        //Imported = false;
                                        //sbMensaje.Append("Registro número : ");
                                        //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                        //sbMensaje.Append(". El Número CARNET DE EXTRANJERIA debe tener 11 dígitos");
                                        //sbMensaje.Append("\n");
                                        //i++;
                                        //continue;
                                    }
                                    else
                                    {
                                        Imported = true;
                                        TempPacient.v_DocNumber = worksheet1.Rows[i].Cells[7].Value.ToString();
                                    }
                                }
                            }
                            else
                            {
                                //ErrorCounter++;
                                //Imported = false;
                                //sbMensaje.Append("Registro número : ");
                                //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                //sbMensaje.Append(". El campo Número Documento es inválido");
                                //sbMensaje.Append("\n");
                                //i++;
                                //continue;
                                TempPacient.v_DocNumber = "";
                            }
                            //ID Género
                            if (worksheet1.Rows[i].Cells[8].Value == null)
                            {
                                TempPacient.i_SexTypeId = -1;
                            }
                            else
                            {
                                if (int.TryParse(worksheet1.Rows[i].Cells[8].Value.ToString(), out  Value))
                                {
                                    Imported = true;
                                    TempPacient.i_SexTypeId = int.Parse(worksheet1.Rows[i].Cells[8].Value.ToString());
                                }
                                else
                                {
                                    //ErrorCounter++;
                                    //Imported = false;
                                    //sbMensaje.Append("Registro número : ");
                                    //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                    //sbMensaje.Append(". El campo ID Género es inválido");
                                    //sbMensaje.Append("\n");
                                    //i++;
                                    //continue;
                                    TempPacient.i_SexTypeId = -1;
                                }
                            }


                            // Nombre Género
                            if (worksheet1.Rows[i].Cells[9].Value != null)
                            {
                                Imported = true;
                                TempPacient.v_SexTypeName = worksheet1.Rows[i].Cells[9].Value.ToString();
                            }
                            else
                            {
                                //ErrorCounter++;
                                //Imported = false;
                                //sbMensaje.Append("Registro número : ");
                                //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                //sbMensaje.Append(". El campo Nombre Género es inválido");
                                //sbMensaje.Append("\n");
                                //i++;
                                //continue;
                                TempPacient.v_SexTypeName = "";
                            }

                            //Fecha Nacimiento

                            if (worksheet1.Rows[i].Cells[10].Value == null)
                            {
                                //TempPacient.d_Birthdate = DateTime.Parse("01/01/1900 12:00:00");
                            }
                            else
                            {
                                if (DateTime.TryParseExact(worksheet1.Rows[i].Cells[10].Value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out  ValueDateTime) == false)
                                {
                                    //ErrorCounter++;
                                    //Imported = false;
                                    //sbMensaje.Append("Registro número : ");
                                    //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                    //sbMensaje.Append(". El campo Fecha Nacimiento es inválido");
                                    //sbMensaje.Append("\n");
                                    //i++;
                                    //continue;
                                }
                                else
                                {
                                    Imported = true;
                                    TempPacient.d_Birthdate = DateTime.ParseExact(worksheet1.Rows[i].Cells[10].Value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                }
                            }
                          

                            //Puesto
                            if (worksheet1.Rows[i].Cells[11].Value != null)
                            {
                                TempPacient.v_CurrentOccupation = worksheet1.Rows[i].Cells[11].Value.ToString();
                                Imported = true;
                            }
                            else
                            {
                                //ErrorCounter++;
                                //Imported = false;
                                //sbMensaje.Append("Registro número : ");
                                //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                //sbMensaje.Append(". El campo Puesto Actual es inválido");
                                //sbMensaje.Append("\n");
                                //i++;
                                //continue;
                                TempPacient.v_CurrentOccupation = "";
                            }

                            //Empresa
                            if (worksheet1.Rows[i].Cells[12].Value != null)
                            {
                                TempPacient.v_OrganitationName = worksheet1.Rows[i].Cells[12].Value.ToString();
                                Imported = true;
                            }
                            else
                            {
                                //ErrorCounter++;
                                //Imported = false;
                                //sbMensaje.Append("Registro número : ");
                                //sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                                //sbMensaje.Append(". El campo Empresa es inválido");
                                //sbMensaje.Append("\n");
                                //i++;
                                //continue;
                            }
                            _TempPacientList.Add(TempPacient);

                            //if (worksheet1.Rows[i].Cells[5].Value != null || worksheet1.Rows[i].Cells[6].Value != null)
                            //{
                            //    var Result = _TempPacientList.FindAll(p => p.v_DocNumber == TempPacient.v_DocNumber && p.i_DocTypeId == TempPacient.i_DocTypeId);
                            //    if (Result.Count > 1)
                            //    {
                            //        MessageBox.Show("El correlativo " + Result[0].i_Correlative + " tiene el mismo Número Documento que el correlativo " + Result[1].i_Correlative + " .Revise el Excel y corriga la duplicidad", "Error al cargar Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //        return;
                            //    }
                            //}

                            i++;

                        }

                        lblRecordCountPacients.Text = string.Format("Se encontraron {0} registros.", _TempPacientList.Count());

                        if (ErrorCounter > 0)
                        {
                            _TempPacientList = new List<AuthorizedPersonList>();
                            grdDataPeople.DataSource = new List<PacientList>();
                            MessageBox.Show(sbMensaje.ToString(), "Registros no importados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            sbMensaje = null;
                        }
                        else if (ErrorCounter == 0)
                        {
                            grdDataPeople.DataSource = _TempPacientList;
                            MessageBox.Show("Se importaron " + _TempPacientList.Count() + " registros.", "Importación correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //btnSave.Enabled = true;
                        }
                        else
                        {
                            _TempPacientList = new List<AuthorizedPersonList>();
                            grdDataPeople.DataSource = new List<PacientList>();
                            MessageBox.Show(sbMensaje.ToString(), "Registros no importados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            sbMensaje = null;
                            //btnSave.Enabled = false;
                        }
                    }
                    else
                    {
                        grdDataPeople.DataSource = new List<PacientList>();
                        MessageBox.Show("Seleccione un formato correcto (.xlsx)", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Enabled = false;
                    }

                    btnSave.Enabled = (grdDataPeople.Rows.Count > 0);

                }
                catch (Exception)
                {
                    MessageBox.Show("El archivo está en uso. Por favor cierra el documento.", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AuthorizedPersonBL oAuthorizedPersonBL = new AuthorizedPersonBL();
            authorizedpersonDto oauthorizedpersonDto;
            OperationResult objOperationResult = new OperationResult();


            frmFecha frm = new frmFecha();
            frm.ShowDialog();

            DialogResult Result = MessageBox.Show("¿Está seguro de agendar?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {

            oAuthorizedPersonBL.DeleteAuthorizedPersonAll(ref objOperationResult);

            foreach (var item in _TempPacientList)
            {
                oauthorizedpersonDto = new authorizedpersonDto();
                oauthorizedpersonDto.d_BirthDate = item.d_Birthdate;
                oauthorizedpersonDto.i_DocTypeId = item.i_DocTypeId;
                oauthorizedpersonDto.i_SexTypeId = item.i_SexTypeId;
                oauthorizedpersonDto.v_DocNumber = item.v_DocNumber;
                oauthorizedpersonDto.v_DocTypeName = item.v_DocTypeName;
                oauthorizedpersonDto.v_FirstLastName = item.v_FirstLastName == null? "": item.v_FirstLastName;
                oauthorizedpersonDto.v_FirstName = item.v_FirstName == null ? "" : item.v_FirstName;
                oauthorizedpersonDto.v_OccupationName = item.v_CurrentOccupation;
                oauthorizedpersonDto.v_OrganitationName = item.v_OrganitationName;
                oauthorizedpersonDto.v_ProtocolId = item.v_ProtocolName;
                oauthorizedpersonDto.v_ProtocolName = item.v_ProtocolName;
                oauthorizedpersonDto.v_SecondLastName = item.v_SecondLastName == null ? "" : item.v_SecondLastName;
                oauthorizedpersonDto.v_SexTypeName = item.v_SexTypeName;
                oauthorizedpersonDto.d_EntryToMedicalCenter = frm.Fecha;
                oAuthorizedPersonBL.AddAuthorizedPerson(ref objOperationResult, oauthorizedpersonDto, Globals.ClientSession.GetAsList());

            }



            if (objOperationResult.Success == 1)  // Operación sin error
            {
                //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                btnImprimir.Enabled = true;
            }
            else  // Operación con error
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Se queda en el formulario.
                btnImprimir.Enabled = false;
            }

            MessageBox.Show("El Volcado de Lista de Trabajadores al sistema se completo con exito.", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {

                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                OperationResult objOperationResult = new OperationResult();
                string ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
                var Lista = new AuthorizedPersonBL().GetAuthorizedPersonPagedAndFiltered(ref objOperationResult, 0, null, null, null);

                foreach (var item in Lista)
                {
                    var Cabecera = new CalendarBL().HojaRutaCabecera(item.v_AuthorizedPersonId);
                    var Detalle = new CalendarBL().HojaRutaDetalle(item.v_ProtocolId);
                    var rp = new Reports.crRoadMapCampania();
                    DataSet ds = new DataSet();

                    DataTable dtHeader = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Cabecera);
                    DataTable dtDetail = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Detalle);

                    dtHeader.TableName = "dtCabecera";
                    dtDetail.TableName = "dtDetalle";

                    ds.Tables.Add(dtHeader);
                    ds.Tables.Add(dtDetail);
                    rp.SetDataSource(ds);


                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Guid.NewGuid().ToString() + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();


                }

                var x = _filesNameToMerge.ToList();
                _mergeExPDF.FilesName = x;
                //_mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + _serviceId + ".pdf"; ;
                _mergeExPDF.DestinationFile = ruta + "xxx" + ".pdf"; ;
                _mergeExPDF.Execute();
                _mergeExPDF.RunFile();

            }
         
        }


    }
}
