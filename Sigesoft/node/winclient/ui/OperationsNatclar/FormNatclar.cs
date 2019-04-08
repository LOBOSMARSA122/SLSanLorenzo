using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.UI.NatclarXML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using System.Threading.Tasks;

namespace Sigesoft.Node.WinClient.UI.OperationsNatclar
{
    public partial class FormNatclar : Form
    {
        OperationsNatclarBl oNataclarBl = new OperationsNatclarBl(); 
        List<OperationsNatclarBe> data =  new List<OperationsNatclarBe>();
        WSRIProveedorExternoClient client = new WSRIProveedorExternoClient();
        Natclar oNatclarBL = new Natclar();
        OperationsNatclarBl oOperationsNatclarBl = new OperationsNatclarBl();

        public FormNatclar()
        {
            InitializeComponent();
        }
        
        private void btnFilter_Click(object sender, EventArgs e)
        {
            data = oNataclarBl.Filter(dtpDateTimeStar.Value, dptDateTimeEnd.Value);
            grdDataService.DataSource = data;
        }

        private void grdDataService_KeyDown(object sender, KeyEventArgs e)
        {
         

        }

        private void grdDataService_ClickCell(object sender, ClickCellEventArgs e)
        {
            //var grid = (UltraGrid)sender;
            //if (grid.ActiveCell == null) return;

            //var column = grid.ActiveCell.Column.Key;

            //if (column == "DatosPersonales")
            //{
            //    MessageBox.Show("ssss");
            //}
            if ((e.Cell.Column.Key == "b_select"))
            {
                if ((e.Cell.Value.ToString() == "False"))
                {
                    e.Cell.Value = true;

                    //btnFechaEntrega.Enabled = true;
                    //btnAdjuntarArchivo.Enabled = true;
                }
                else
                {
                    e.Cell.Value = false;
                    //btnFechaEntrega.Enabled = false;
                    //btnAdjuntarArchivo.Enabled = false;
                }

            }

        }

        private void grdDataService_ClickCellButton(object sender, CellEventArgs e)
        {
            var serviceId = e.Cell.Row.Cells["v_ServiceId"].Value.ToString();
            var personId = e.Cell.Row.Cells["v_PersonId"].Value.ToString();
            var nameColumn = e.Cell.Column.Key;

            Task.Factory.StartNew(() =>
            {
                e.Cell.ButtonAppearance.Image = Resources.cog_stop;
                ProcessInformation(nameColumn, serviceId, personId, e);
            },TaskCreationOptions.LongRunning);

        }

        private void ProcessInformation(string column, string serviceId, string personId, CellEventArgs e)
        {
            var lError = "";
            try
            {

            #region Casos

            switch (column)
            {
                case "DatosPersonales":
                    lError = "oNatclarBL.DatosXmlNatclar";
                    var datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                    if (datosPaciente == null)
                    {
                        MessageBox.Show("No se pudo mandar datos a Natclar", "Validación!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    lError = "oNatclarBL.DevolverUbigue";
                    var ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento, datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);
                    var obj = new EstructuraDatosAPMedicos();
                    obj.DatosPaciente = new XmlDatosPaciente();
                    lError = "datosPaciente.Nombre";
                    obj.DatosPaciente.Nombre = datosPaciente.Nombre;
                    lError = "datosPaciente.Dni";
                    obj.DatosPaciente.DNI = datosPaciente.Dni;
                    lError = "datosPaciente.DepartamentoNacimiento ";
                    obj.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                    lError = "datosPaciente.Direccion";
                    obj.DatosPaciente.Direccion = datosPaciente.Direccion;
                    lError = "datosPaciente.DistritoNacimiento";
                    obj.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                    lError = "datosPaciente.Email";
                    obj.DatosPaciente.Email = datosPaciente.Email;
                    lError = "datosPaciente.EstadoCivil";
                    obj.DatosPaciente.EstadoCivil = "S";
                    lError = "datosPaciente.FechaNacimiento";
                    obj.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                    lError = "datosPaciente.Hc";
                    obj.DatosPaciente.HC = datosPaciente.Hc;
                    lError = "datosPaciente.PrimerApellido";
                    obj.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                    lError = "datosPaciente.ProvinciaNacimiento";
                    obj.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                    lError = "datosPaciente.ResidenciaActual";
                    obj.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                    lError = "datosPaciente.Sexo";
                    obj.DatosPaciente.Sexo = "M";
                    lError = "datosPaciente.TipoDocumento";
                    obj.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());
                    lError = "datosPaciente.EnviarDatosAPMedicos";

                    lError = "datosPaciente.LanzarDatos";
                    LanzarDatos(client.EnviarDatosAPMedicos(obj),e,column, serviceId );
                
                    break;
                case "DatosApMedicos":
                    //do something

                    break;
                case "DatosApQuirurgicos":
                    //do something
                    OperationResult objOperationResult = new OperationResult();
                    var objQuiru = new EstructuraDatosAPQuirurgicos();
                    objQuiru.APQUIRURGICOS = objQuiru.APQUIRURGICOS = new XmlAntecedentesPatologicosQuirurgicos();
                   
                    objQuiru.DatosPaciente = new XmlDatosPaciente();

                    //objQuiru.DatosExamen = new XmlDatosExamen();


                    #region Antecedentes Quirurgico
                    var antecedentesQuirurgicos = new HistoryBL().GetPersonMedicalHistoryPagedAndFilteredByPersonId(ref objOperationResult, 0, null, "d_StartDate DESC", null, personId).FindAll(p => p.v_DiseasesId == "N009-DD000000637");

                    foreach (var item in antecedentesQuirurgicos)
                    {
                        lError = "APQUIRURGICOS.CodigoCIE";
                        objQuiru.APQUIRURGICOS.CodigoCIE = item.v_CIE10Id;
                        lError = "APQUIRURGICOS.Descripcion";
                        objQuiru.APQUIRURGICOS.Descripcion = item.v_TreatmentSite;
                        lError = "APQUIRURGICOS.FechaInicio";
                        if (item.d_StartDate != null)
                            objQuiru.APQUIRURGICOS.FechaInicio = item.d_StartDate.Value.ToString("dd/MM/yyyy");
                        lError = "APQUIRURGICOS.FechaFin";
                        if (item.d_StartDate!= null)
                            objQuiru.APQUIRURGICOS.FechaFin = item.d_StartDate.Value.ToString("dd/MM/yyyy");
                        lError = "APQUIRURGICOS.AntecedenteLaboral";
                        objQuiru.APQUIRURGICOS.AntecedenteLaboral = item.i_TypeDiagnosticId == 4 ? "S" : "N";
                        lError = "APQUIRURGICOS.LanzarDatos";
                        LanzarDatos(client.EnviarDatosAPQuirurgicos(objQuiru), e, column, serviceId);
                    }

                    #endregion

                    break;
                case "DatosExamen":
                    //do something
                    break;
                case "DatosAudiometria":
                    //do something
                    break;
                case "DatosConstantes":
                    //do something
                    break;
                case "DatosEpisodios":
                    //do something
                    break;
                case "DatosExamenFisico":
                    //do something
                    break;
                case "DatosExamenOcularVB":
                    //do something
                    break;
                case "DatosHabitosToxicos":
                    //do something
                    break;
                case "DatosHistoriaFamiliar":
                    //do something
                    break;
                case "DatosHistoriaFisiologico":
                    //do something
                    break;
                case "DatosHistoriaLaboral":
                    //do something
                    break;
                case "DatosHistoriaLaboralRl":
                    //do something
                    break;
                case "DatosMedicaciones":
                    //do something
                    break;
                case "DatosVacunas":
                    //do something
                    break;
                case "DatosVigilanciaSalus":
                    //do something
                    break;
                case "DatosLecturaOIT":
                    //do something
                    break;
                case "DatosLecturaPsicologia":
                    //do something
                    break;
                case "DatosAnalitica":
                    //do something
                    break;


            }

            #endregion

            }
            catch (Exception exception)
            {
                e.Cell.ButtonAppearance.Image = Resources.cog;
                MessageBox.Show(lError + "\n" + exception.Message, "Validación!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            
        }

        private void LanzarDatos(Resultado resultado, CellEventArgs e, string column, string serviceId)
        {
            envionatclarDto oenvionatclarDto = new envionatclarDto();
            if (resultado.EstadoXML == "OK")
            {
                e.Cell.ButtonAppearance.Image = Resources.accept;
                oenvionatclarDto.v_ServiceId = serviceId;
                oenvionatclarDto.v_Paquete = column;
                oenvionatclarDto.i_EstadoId =  (int)EstadoEnvioNatclar.Ok;

            }
            else
            {
                e.Cell.ButtonAppearance.Image = Resources.system_close;
                oenvionatclarDto.v_ServiceId = serviceId;
                oenvionatclarDto.v_Paquete = column;
                oenvionatclarDto.i_EstadoId = (int)EstadoEnvioNatclar.Fail;
            }
                  
                   
            oOperationsNatclarBl.GrabarEnvio(oenvionatclarDto, Globals.ClientSession.GetAsList());

        }

        private void FormNatclar_Load(object sender, EventArgs e)
        {
            UltraGridColumn c = grdDataService.DisplayLayout.Bands[0].Columns["b_select"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;

            dtpDateTimeStar.Focus();
        }

        private void grdDataService_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
        }

        private void grdDataService_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            var serviceId = e.Row.Cells["v_ServiceId"].Value.ToString();


            for (int i = 5; i < e.Row.Cells.Count; i++)
            {
                var paquete = e.Row.Cells[i].Column.Key;
                //if (paquete.Column.Key != "v_Pacient" && paquete.Column.Key != "v_ServiceId" &&
                //    paquete.Column.Key != "d_ServiceDate")
                //{
              
                
                //ERROR EN PAQUETE NULL

                var x = data.Find(p => p.v_ServiceId == serviceId);
                if (x.v_Paquete == null)
                {
                    evaluarCelda(paquete, "null", e);
                }
                else
                {
                    var estado = x.i_EstadoId;
                    paquete = data.Find(p => p.i_EstadoId == estado && p.v_ServiceId == serviceId).v_Paquete;
                        //data.Find(p => p.v_ServiceId == serviceId && p.v_Paquete.ToString() == paquete.ToString())
                        //    .i_EstadoId == null
                        //    ? ""
                        //    : data.Find(p => p.v_ServiceId == serviceId && p.v_Paquete.ToString() == paquete.ToString())
                        //        .i_EstadoId.ToString();
                    evaluarCelda(paquete, estado.ToString(), e);
                }
                    
                //}
                //else
                //{
                //    return;
                //}
            }

           
                    

                
                #region ...
                //if (paquete.Column.Key == "DatosPersonales")
                //{
                //    evaluarCelda(paquete.Column.Key, estado,e);

                //}
                //else if (paquete.Column.Key == "DatosApMedicos")
                //{

                //}
                //else if (paquete.Column.Key == "DatosApQuirurgicos")
                //{

                //}
                //else if (paquete.Column.Key == "DatosExamen")
                //{

                //}
                //else if (paquete.Column.Key == "DatosAudiometria")
                //{

                //}
                //else if (paquete.Column.Key == "DatosConstantes")
                //{

                //}
                //else if (paquete.Column.Key == "DatosEpisodios")
                //{

                //}
                //else if (paquete.Column.Key == "DatosExamenFisico")
                //{

                //}
                //else if (paquete.Column.Key == "DatosExamenOcularVB")
                //{

                //}
                //else if (paquete.Column.Key == "DatosHabitosToxicos")
                //{

                //}
                //else if (paquete.Column.Key == "DatosHistoriaFamiliar")
                //{

                //}
                //else if (paquete.Column.Key == "DatosHistoriaFisiologico")
                //{

                //}
                //else if (paquete.Column.Key == "DatosHistoriaLaboral")
                //{

                //}
                //else if (paquete.Column.Key == "DatosHistoriaLaboralRl")
                //{

                //}
                //else if (paquete.Column.Key == "DatosMedicaciones")
                //{

                //}
                //else if (paquete.Column.Key == "DatosVacunas")
                //{

                //}
                //else if (paquete.Column.Key == "DatosVigilanciaSalus")
                //{

                //}
                //else if (paquete.Column.Key == "DatosLecturaOIT")
                //{

                //}
                //else if (paquete.Column.Key == "DatosLecturaPsicologia")
                //{

                //}
                //else if (paquete.Column.Key == "DatosAnalitica")
                //{

                //}
                

                #endregion
        }

        private void evaluarCelda(string p, string estado, InitializeRowEventArgs e)
        {
            //e.Cell.ButtonAppearance.Image = Resources.accept;
            if (estado == "null")
                e.Row.Cells[p].ButtonAppearance.Image = Resources.cog;
            else if (estado == "1")
                e.Row.Cells["DatosPersonales"].ButtonAppearance.Image = Resources.accept;
            else
                e.Row.Cells[p].ButtonAppearance.Image = Resources.system_close;
        }

        private void btnEnviarBloque_Click(object sender, EventArgs e)
        {
            var arry = new List<string>();
            foreach (var item in grdDataService.Rows)
            {
                if ((bool)item.Cells["b_select"].Value)
                {
                    string serviceId = item.Cells["v_ServiceId"].Value.ToString();
                    
                    arry.Add(serviceId);
                }
            }

        }
    }
}
