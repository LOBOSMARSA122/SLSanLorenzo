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
            ServiceBL _ServiceBL = new ServiceBL();
            HistoryBL _HistoryBL = new HistoryBL();
            OperationResult objOperationResult = new OperationResult();
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
                case "DatosApQuirurgicos":

                    var objQuiru = new EstructuraDatosAPQuirurgicos();

                    datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                    ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento, datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                    objQuiru.APQUIRURGICOS = new XmlAntecedentesPatologicosQuirurgicos();

                    #region DatosPaciente

                    objQuiru.DatosPaciente = new XmlDatosPaciente();

                    lError = "datosPaciente.Nombre";
                    objQuiru.DatosPaciente.Nombre = datosPaciente.Nombre;
                    lError = "datosPaciente.Dni";
                    objQuiru.DatosPaciente.DNI = datosPaciente.Dni;
                    lError = "datosPaciente.DepartamentoNacimiento ";
                    objQuiru.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                    lError = "datosPaciente.Direccion";
                    objQuiru.DatosPaciente.Direccion = datosPaciente.Direccion;
                    lError = "datosPaciente.DistritoNacimiento";
                    objQuiru.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                    lError = "datosPaciente.Email";
                    objQuiru.DatosPaciente.Email = datosPaciente.Email;
                    lError = "datosPaciente.EstadoCivil";
                    objQuiru.DatosPaciente.EstadoCivil = "S";
                    lError = "datosPaciente.FechaNacimiento";
                    objQuiru.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                    lError = "datosPaciente.Hc";
                    objQuiru.DatosPaciente.HC = datosPaciente.Hc;
                    lError = "datosPaciente.PrimerApellido";
                    objQuiru.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                    lError = "datosPaciente.SegundoApellido";
                    objQuiru.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                    lError = "datosPaciente.ProvinciaNacimiento";
                    objQuiru.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                    lError = "datosPaciente.ResidenciaActual";
                    objQuiru.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                    lError = "datosPaciente.Sexo";
                    objQuiru.DatosPaciente.Sexo = "M";
                    lError = "datosPaciente.TipoDocumento";
                    objQuiru.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                    #endregion

                    #region DatosExamen

                    objQuiru.DatosExamen = new XmlDatosExamen();

                    lError = "datosPaciente.IDActuacion";
                    objQuiru.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                    lError = "datosPaciente.IDCentro";
                    objQuiru.DatosExamen.IDCentro = datosPaciente.IDCentro;
                    lError = "datosPaciente.FechaRegistro";
                    objQuiru.DatosExamen.FechaRegistro = datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                    lError = "datosPaciente.IDEstado";
                    objQuiru.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                    lError = "datosPaciente.IDEstructura";
                    objQuiru.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                    lError = "datosPaciente.IDExamen";
                    objQuiru.DatosExamen.IDExamen = datosPaciente.IDExamen;
                    lError = "datosPaciente.TipoExamen";
                    objQuiru.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                    #endregion
                    
                    #region Antecedentes Quirurgico
                    var antecedentesQuirurgicos = new HistoryBL().GetPersonMedicalHistoryPagedAndFilteredByPersonId(ref objOperationResult, 0, null, "d_StartDate DESC", null, personId).FindAll(p => p.v_DiseasesId == "N009-DD000000637");

                    if (antecedentesQuirurgicos.Count == 0)
                    {
                        MessageBox.Show("El paciente " + datosPaciente.Nombre + " " + datosPaciente.PrimerApellido + " " + datosPaciente.SegundoApellido + " no cuenta con exámenes quirúrgicos.", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


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
                    var objDatosExamen = new XmlDatosExamen();

                    datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);

                    lError = "datosPaciente.IDActuacion";
                    objDatosExamen.IDActuacion = datosPaciente.IDActuacion;
                    lError = "datosPaciente.IDCentro";
                    objDatosExamen.IDCentro = datosPaciente.IDCentro;
                    lError = "datosPaciente.FechaRegistro";
                    objDatosExamen.FechaRegistro = datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                    lError = "datosPaciente.IDEstado";
                    objDatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                    lError = "datosPaciente.IDEstructura";
                    objDatosExamen.IDEstructura = datosPaciente.IDEstructura;
                    lError = "datosPaciente.IDExamen";
                    objDatosExamen.IDExamen = datosPaciente.IDExamen;
                    lError = "datosPaciente.TipoExamen";
                    objDatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                    lError = "datosPaciente.LanzarDatos";
                    LanzarDatos(client.EnviarDatosExamen(objDatosExamen), e, column, serviceId);
                    //var objDatosExmane = new EstructuraDatosExamen();


                    break;
                case "DatosAudiometria":
                    

                    var objAud = new EstructuraDatosAudiometria();

                    var audiometriaValores = _ServiceBL.ValoresComponentesUserControl(serviceId, Constants.AUDIOMETRIA_ID);
                    
                    datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                    ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento, datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);


                    #region DatosPaciente

                    objAud.DatosPaciente = new XmlDatosPaciente();

                    lError = "datosPaciente.Nombre";
                    objAud.DatosPaciente.Nombre = datosPaciente.Nombre;
                    lError = "datosPaciente.Dni";
                    objAud.DatosPaciente.DNI = datosPaciente.Dni;
                    lError = "datosPaciente.DepartamentoNacimiento ";
                    objAud.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                    lError = "datosPaciente.Direccion";
                    objAud.DatosPaciente.Direccion = datosPaciente.Direccion;
                    lError = "datosPaciente.DistritoNacimiento";
                    objAud.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                    lError = "datosPaciente.Email";
                    objAud.DatosPaciente.Email = datosPaciente.Email;
                    lError = "datosPaciente.EstadoCivil";
                    objAud.DatosPaciente.EstadoCivil = "S";
                    lError = "datosPaciente.FechaNacimiento";
                    objAud.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                    lError = "datosPaciente.Hc";
                    objAud.DatosPaciente.HC = datosPaciente.Hc;
                    lError = "datosPaciente.PrimerApellido";
                    objAud.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                    lError = "datosPaciente.SegundoApellido";
                    objAud.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                    lError = "datosPaciente.ProvinciaNacimiento";
                    objAud.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                    lError = "datosPaciente.ResidenciaActual";
                    objAud.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                    lError = "datosPaciente.Sexo";
                    objAud.DatosPaciente.Sexo = "M";
                    lError = "datosPaciente.TipoDocumento";
                    objAud.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                    #endregion

                    #region DatosExamen

                    objAud.DatosExamen = new XmlDatosExamen();

                    lError = "datosPaciente.IDActuacion";
                    objAud.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                    lError = "datosPaciente.IDCentro";
                    objAud.DatosExamen.IDCentro = datosPaciente.IDCentro;
                    lError = "datosPaciente.FechaRegistro";
                    objAud.DatosExamen.FechaRegistro = datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                    lError = "datosPaciente.IDEstado";
                    objAud.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                    lError = "datosPaciente.IDEstructura";
                    objAud.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                    lError = "datosPaciente.IDExamen";
                    objAud.DatosExamen.IDExamen = datosPaciente.IDExamen;
                    lError = "datosPaciente.TipoExamen";
                    objAud.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                    #endregion

                    #region DatosAudiometria

                    objAud.Audiometria = objAudiometriaValues(audiometriaValores);

                    #endregion

                    lError = "datosAudiometría.LanzarDatos";
                    LanzarDatos(client.EnviarDatosAudiometria(objAud), e, column, serviceId);

                    break;
                case "DatosConstantes":
                    
                    var objConstantes = new EstructuraDatosConstantes();

                    var constantes = _ServiceBL.ValoresComponenteconstantes(serviceId);


                    datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                    ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento, datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);


                    #region DatosPaciente

                    objConstantes.DatosPaciente = new XmlDatosPaciente();

                    lError = "datosPaciente.Nombre";
                    objConstantes.DatosPaciente.Nombre = datosPaciente.Nombre;
                    lError = "datosPaciente.Dni";
                    objConstantes.DatosPaciente.DNI = datosPaciente.Dni;
                    lError = "datosPaciente.DepartamentoNacimiento ";
                    objConstantes.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                    lError = "datosPaciente.Direccion";
                    objConstantes.DatosPaciente.Direccion = datosPaciente.Direccion;
                    lError = "datosPaciente.DistritoNacimiento";
                    objConstantes.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                    lError = "datosPaciente.Email";
                    objConstantes.DatosPaciente.Email = datosPaciente.Email;
                    lError = "datosPaciente.EstadoCivil";
                    objConstantes.DatosPaciente.EstadoCivil = "S";
                    lError = "datosPaciente.FechaNacimiento";
                    objConstantes.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                    lError = "datosPaciente.Hc";
                    objConstantes.DatosPaciente.HC = datosPaciente.Hc;
                    lError = "datosPaciente.PrimerApellido";
                    objConstantes.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                    lError = "datosPaciente.SegundoApellido";
                    objConstantes.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                    lError = "datosPaciente.ProvinciaNacimiento";
                    objConstantes.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                    lError = "datosPaciente.ResidenciaActual";
                    objConstantes.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                    lError = "datosPaciente.Sexo";
                    objConstantes.DatosPaciente.Sexo = "M";
                    lError = "datosPaciente.TipoDocumento";
                    objConstantes.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                    #endregion

                    #region DatosExamen

                    objConstantes.DatosExamen = new XmlDatosExamen();

                    lError = "datosPaciente.IDActuacion";
                    objConstantes.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                    lError = "datosPaciente.IDCentro";
                    objConstantes.DatosExamen.IDCentro = datosPaciente.IDCentro;
                    lError = "datosPaciente.FechaRegistro";
                    objConstantes.DatosExamen.FechaRegistro = datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                    lError = "datosPaciente.IDEstado";
                    objConstantes.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                    lError = "datosPaciente.IDEstructura";
                    objConstantes.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                    lError = "datosPaciente.IDExamen";
                    objConstantes.DatosExamen.IDExamen = datosPaciente.IDExamen;
                    lError = "datosPaciente.TipoExamen";
                    objConstantes.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                    #endregion

                    #region DatosConstantes

                    objConstantes.Constantes = new XmlConstantes();

                    foreach (var item in constantes)
                    {
                        if (item.v_ComponentFieldId == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID)
                        {
                            objConstantes.Constantes.Cintura = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID)
                        {
                            objConstantes.Constantes.Cadera = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.FUNCIONES_VITALES_SAT_O2_ID)
                        {
                            objConstantes.Constantes.SaturacionOxigeno = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID)
                        {
                            objConstantes.Constantes.Pulso = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.FUNCIONES_VITALES_TEMPERATURA_ID)
                        {
                            objConstantes.Constantes.Temperatura = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID)
                        {
                            objConstantes.Constantes.Respiracion = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID)
                        {
                            objConstantes.Constantes.ICC = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.EXCEPCIONES_RX_FECHA_ULTIMA_REGLA)
                        {
                            objConstantes.Constantes.FechaUltimaRegla = item.v_Value1;
                        }
                    }

                    #endregion

                    lError = "datosConstantes.LanzarDatos";
                    LanzarDatos(client.EnviarDatosConstantes(objConstantes), e, column, serviceId);
                    break;
                
                case "DatosExamenOcularVB":

                    var objExaOcular = new EstructuraDatosExamenOcularVB();

                    var examenOcular = _ServiceBL.ValoresComponenteExamenOcular(serviceId);
                    
                    datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                    ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento, datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);


                    #region DatosPaciente

                    objExaOcular.DatosPaciente = new XmlDatosPaciente();

                    lError = "datosPaciente.Nombre";
                    objExaOcular.DatosPaciente.Nombre = datosPaciente.Nombre;
                    lError = "datosPaciente.Dni";
                    objExaOcular.DatosPaciente.DNI = datosPaciente.Dni;
                    lError = "datosPaciente.DepartamentoNacimiento ";
                    objExaOcular.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                    lError = "datosPaciente.Direccion";
                    objExaOcular.DatosPaciente.Direccion = datosPaciente.Direccion;
                    lError = "datosPaciente.DistritoNacimiento";
                    objExaOcular.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                    lError = "datosPaciente.Email";
                    objExaOcular.DatosPaciente.Email = datosPaciente.Email;
                    lError = "datosPaciente.EstadoCivil";
                    objExaOcular.DatosPaciente.EstadoCivil = "S";
                    lError = "datosPaciente.FechaNacimiento";
                    objExaOcular.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                    lError = "datosPaciente.Hc";
                    objExaOcular.DatosPaciente.HC = datosPaciente.Hc;
                    lError = "datosPaciente.PrimerApellido";
                    objExaOcular.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                    lError = "datosPaciente.SegundoApellido";
                    objExaOcular.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                    lError = "datosPaciente.ProvinciaNacimiento";
                    objExaOcular.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                    lError = "datosPaciente.ResidenciaActual";
                    objExaOcular.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                    lError = "datosPaciente.Sexo";
                    objExaOcular.DatosPaciente.Sexo = "M";
                    lError = "datosPaciente.TipoDocumento";
                    objExaOcular.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                    #endregion

                    #region DatosExamen

                    objExaOcular.DatosExamen = new XmlDatosExamen();

                    lError = "datosPaciente.IDActuacion";
                    objExaOcular.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                    lError = "datosPaciente.IDCentro";
                    objExaOcular.DatosExamen.IDCentro = datosPaciente.IDCentro;
                    lError = "datosPaciente.FechaRegistro";
                    objExaOcular.DatosExamen.FechaRegistro = datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                    lError = "datosPaciente.IDEstado";
                    objExaOcular.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                    lError = "datosPaciente.IDEstructura";
                    objExaOcular.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                    lError = "datosPaciente.IDExamen";
                    objExaOcular.DatosExamen.IDExamen = datosPaciente.IDExamen;
                    lError = "datosPaciente.TipoExamen";
                    objExaOcular.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();
                    #endregion


                    #region ExamenOcular

                    foreach (var item in examenOcular)
                    {
                        objExaOcular.ExamenOcularVB = new XmlExamenOcularVB();

                        if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOD)
                        {
                            objExaOcular.ExamenOcularVB.VisionCercaODSCVB = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOI)
                        {
                            objExaOcular.ExamenOcularVB.VisionCercaOISCVB = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOD)
                        {
                            objExaOcular.ExamenOcularVB.VisionLejosODSCVB = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOI)
                        {
                            objExaOcular.ExamenOcularVB.VisionLejosOISCVB = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOD)
                        {
                            objExaOcular.ExamenOcularVB.VisionCercaODCVB = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOI)
                        {
                            objExaOcular.ExamenOcularVB.VisionCercaOICVB = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOD)
                        {
                            objExaOcular.ExamenOcularVB.VisionLejosODCVB = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOI)
                        {
                            objExaOcular.ExamenOcularVB.VisionLejosOICVB = item.v_Value1;
                        }
                        else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_ISHIHARA)
                        {
                            objExaOcular.ExamenOcularVB.TestColores = item.v_Value1;
                        }
                       
                        //objExaOcular.ExamenOcularVB.RestriccionActual
                        //objExaOcular.ExamenOcularVB.TipoRestriccion
                    }

                    #endregion

                    lError = "datosExamenOcular.LanzarDatos";
                    LanzarDatos(client.EnviarDatosExamenOcularVB(objExaOcular), e, column, serviceId);

                    break;
                case "DatosHabitosToxicos":

                    var objHabToxicos = new EstructuraDatosHabitosToxicos();
                    
                    var habitos = _HistoryBL.GetNoxiousHabitsPagedAndFilteredByPersonId(ref objOperationResult, 0, null, "i_TypeHabitsId DESC", null, personId);

                    
                    datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                    ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento, datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);
                    
                    #region DatosPaciente

                    objHabToxicos.DatosPaciente = new XmlDatosPaciente();

                    lError = "datosPaciente.Nombre";
                    objHabToxicos.DatosPaciente.Nombre = datosPaciente.Nombre;
                    lError = "datosPaciente.Dni";
                    objHabToxicos.DatosPaciente.DNI = datosPaciente.Dni;
                    lError = "datosPaciente.DepartamentoNacimiento ";
                    objHabToxicos.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                    lError = "datosPaciente.Direccion";
                    objHabToxicos.DatosPaciente.Direccion = datosPaciente.Direccion;
                    lError = "datosPaciente.DistritoNacimiento";
                    objHabToxicos.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                    lError = "datosPaciente.Email";
                    objHabToxicos.DatosPaciente.Email = datosPaciente.Email;
                    lError = "datosPaciente.EstadoCivil";
                    objHabToxicos.DatosPaciente.EstadoCivil = "S";
                    lError = "datosPaciente.FechaNacimiento";
                    objHabToxicos.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                    lError = "datosPaciente.Hc";
                    objHabToxicos.DatosPaciente.HC = datosPaciente.Hc;
                    lError = "datosPaciente.PrimerApellido";
                    objHabToxicos.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                    lError = "datosPaciente.SegundoApellido";
                    objHabToxicos.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                    lError = "datosPaciente.ProvinciaNacimiento";
                    objHabToxicos.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                    lError = "datosPaciente.ResidenciaActual";
                    objHabToxicos.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                    lError = "datosPaciente.Sexo";
                    objHabToxicos.DatosPaciente.Sexo = "M";
                    lError = "datosPaciente.TipoDocumento";
                    objHabToxicos.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                    #endregion

                    #region DatosExamen

                    objHabToxicos.DatosExamen = new XmlDatosExamen();

                    lError = "datosPaciente.IDActuacion";
                    objHabToxicos.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                    lError = "datosPaciente.IDCentro";
                    objHabToxicos.DatosExamen.IDCentro = datosPaciente.IDCentro;
                    lError = "datosPaciente.FechaRegistro";
                    objHabToxicos.DatosExamen.FechaRegistro = datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                    lError = "datosPaciente.IDEstado";
                    objHabToxicos.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                    lError = "datosPaciente.IDEstructura";
                    objHabToxicos.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                    lError = "datosPaciente.IDExamen";
                    objHabToxicos.DatosExamen.IDExamen = datosPaciente.IDExamen;
                    lError = "datosPaciente.TipoExamen";
                    objHabToxicos.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                    #endregion

                    #region Habitos Toxicos

                    foreach (var item in habitos)
                    {
                        objHabToxicos.HabitosToxicos = new XmlHabitosToxicos();
                        objHabToxicos.HabitosToxicos.Descripcion = item.v_DescriptionHabit;
                        //objEHabToxicos.HabitosToxicos.Vigencia = item.v_DescriptionQuantity;
                        objHabToxicos.HabitosToxicos.TipoHabito = item.v_TypeHabitsName;
                        //objEHabToxicos.HabitosToxicos.Fechainicio = item.;
                        //objEHabToxicos.HabitosToxicos.FechaFin = item.;
                        lError = "datosConstantes.LanzarDatos";
                        LanzarDatos(client.EnviarDatosHabitosToxicos(objHabToxicos), e, column, serviceId);
                    }

                    #endregion                  

                    break;
                case "DatosHistoriaFamiliar":
                    var objHistFamiliar = new EstructuraDatosHistoriaFamiliar();

                    var antecedentesFamiliares = _HistoryBL.GetFamilyMedicalAntecedentsPagedAndFilteredByPersonId(ref objOperationResult, 0, null, "v_DiseasesId DESC", null, personId);

                    
                    datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                    ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento, datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);
                    
                    #region DatosPaciente
                    objHistFamiliar.DatosPaciente = new XmlDatosPaciente();

                    lError = "datosPaciente.Nombre";
                    objHistFamiliar.DatosPaciente.Nombre = datosPaciente.Nombre;
                    lError = "datosPaciente.Dni";
                    objHistFamiliar.DatosPaciente.DNI = datosPaciente.Dni;
                    lError = "datosPaciente.DepartamentoNacimiento ";
                    objHistFamiliar.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                    lError = "datosPaciente.Direccion";
                    objHistFamiliar.DatosPaciente.Direccion = datosPaciente.Direccion;
                    lError = "datosPaciente.DistritoNacimiento";
                    objHistFamiliar.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                    lError = "datosPaciente.Email";
                    objHistFamiliar.DatosPaciente.Email = datosPaciente.Email;
                    lError = "datosPaciente.EstadoCivil";
                    objHistFamiliar.DatosPaciente.EstadoCivil = "S";
                    lError = "datosPaciente.FechaNacimiento";
                    objHistFamiliar.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                    lError = "datosPaciente.Hc";
                    objHistFamiliar.DatosPaciente.HC = datosPaciente.Hc;
                    lError = "datosPaciente.PrimerApellido";
                    objHistFamiliar.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                    lError = "datosPaciente.SegundoApellido";
                    objHistFamiliar.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                    lError = "datosPaciente.ProvinciaNacimiento";
                    objHistFamiliar.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                    lError = "datosPaciente.ResidenciaActual";
                    objHistFamiliar.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                    lError = "datosPaciente.Sexo";
                    objHistFamiliar.DatosPaciente.Sexo = "M";
                    lError = "datosPaciente.TipoDocumento";
                    objHistFamiliar.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                    #endregion

                    #region DatosExamen

                    objHistFamiliar.DatosExamen = new XmlDatosExamen();

                    lError = "datosPaciente.IDActuacion";
                    objHistFamiliar.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                    lError = "datosPaciente.IDCentro";
                    objHistFamiliar.DatosExamen.IDCentro = datosPaciente.IDCentro;
                    lError = "datosPaciente.FechaRegistro";
                    objHistFamiliar.DatosExamen.FechaRegistro = datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                    lError = "datosPaciente.IDEstado";
                    objHistFamiliar.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                    lError = "datosPaciente.IDEstructura";
                    objHistFamiliar.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                    lError = "datosPaciente.IDExamen";
                    objHistFamiliar.DatosExamen.IDExamen = datosPaciente.IDExamen;
                    lError = "datosPaciente.TipoExamen";
                    objHistFamiliar.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                    #endregion


                    #region DatosHistoriaFamiliar

                    foreach (var item in antecedentesFamiliares)
                    {
                        objHistFamiliar.HistoriaFamiliar = new XmlHistoriaFamiliar();

                        objHistFamiliar.HistoriaFamiliar.CodigoCIE = item.v_CIE10Id;
                        objHistFamiliar.HistoriaFamiliar.Descripcion = item.DxAndComment;
                        //objHistFamiliar.HistoriaFamiliar.Fechainicio = item.;
                        //objHistFamiliar.HistoriaFamiliar.FechaFin = item.;

                        lError = "datosHistoriaFamiliar.LanzarDatos";
                        LanzarDatos(client.EnviarDatosHistoriaFamiliar(objHistFamiliar), e, column, serviceId);
                    }

                    #endregion
                    
                    
                    break;
                
                case "DatosHistoriaLaboral":

                    var objHistLaboral = new EstructuraDatosHistoriaLaboral();

                    var historiaLaboral = _HistoryBL.GetHistoryPagedAndFiltered(ref objOperationResult, 0, null, "d_StartDate DESC", null, personId);



                    
                    datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                    ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento, datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);
                    
                    #region DatosPaciente
                    objHistLaboral.DatosPaciente = new XmlDatosPaciente();

                    lError = "datosPaciente.Nombre";
                    objHistLaboral.DatosPaciente.Nombre = datosPaciente.Nombre;
                    lError = "datosPaciente.Dni";
                    objHistLaboral.DatosPaciente.DNI = datosPaciente.Dni;
                    lError = "datosPaciente.DepartamentoNacimiento ";
                    objHistLaboral.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                    lError = "datosPaciente.Direccion";
                    objHistLaboral.DatosPaciente.Direccion = datosPaciente.Direccion;
                    lError = "datosPaciente.DistritoNacimiento";
                    objHistLaboral.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                    lError = "datosPaciente.Email";
                    objHistLaboral.DatosPaciente.Email = datosPaciente.Email;
                    lError = "datosPaciente.EstadoCivil";
                    objHistLaboral.DatosPaciente.EstadoCivil = "S";
                    lError = "datosPaciente.FechaNacimiento";
                    objHistLaboral.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                    lError = "datosPaciente.Hc";
                    objHistLaboral.DatosPaciente.HC = datosPaciente.Hc;
                    lError = "datosPaciente.PrimerApellido";
                    objHistLaboral.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                    lError = "datosPaciente.SegundoApellido";
                    objHistLaboral.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                    lError = "datosPaciente.ProvinciaNacimiento";
                    objHistLaboral.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                    lError = "datosPaciente.ResidenciaActual";
                    objHistLaboral.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                    lError = "datosPaciente.Sexo";
                    objHistLaboral.DatosPaciente.Sexo = "M";
                    lError = "datosPaciente.TipoDocumento";
                    objHistLaboral.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                    #endregion

                    #region DatosExamen

                    objHistLaboral.DatosExamen = new XmlDatosExamen();

                    lError = "datosPaciente.IDActuacion";
                    objHistLaboral.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                    lError = "datosPaciente.IDCentro";
                    objHistLaboral.DatosExamen.IDCentro = datosPaciente.IDCentro;
                    lError = "datosPaciente.FechaRegistro";
                    objHistLaboral.DatosExamen.FechaRegistro = datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                    lError = "datosPaciente.IDEstado";
                    objHistLaboral.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                    lError = "datosPaciente.IDEstructura";
                    objHistLaboral.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                    lError = "datosPaciente.IDExamen";
                    objHistLaboral.DatosExamen.IDExamen = datosPaciente.IDExamen;
                    lError = "datosPaciente.TipoExamen";
                    objHistLaboral.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                    #endregion


                    #region DatosExamen
                    foreach (var item in historiaLaboral)
                    {

                        objHistLaboral.HistoriaLaboral = new XmlHistoriaLaboral();

                        objHistLaboral.HistoriaLaboral.EmpresaLaboral = item.v_Organization;
                        objHistLaboral.HistoriaLaboral.ZonaLaboral = item.v_workstation;
                        //objHistLaboral.HistoriaLaboral.RUCEmpresa = item.;
                        //objHistLaboral.HistoriaLaboral.PuestosHistorial = item;
                        //objHistLaboral.HistoriaLaboral.DuracionPuestoA = item.;
                        //objHistLaboral.HistoriaLaboral.DuracionPuestoM = item.;
                        //objHistLaboral.HistoriaLaboral.FechaInicioPuesto = item.;
                        //objHistLaboral.HistoriaLaboral.FechaFinPuesto = item.;
                        //objHistLaboral.HistoriaLaboral.ActividadCNAE = item.;
                        //objHistLaboral.HistoriaLaboral.PuestoActual = item.;
                        //objHistLaboral.HistoriaLaboral.DescripcionTareas = item.;
                        //objHistLaboral.HistoriaLaboral.ValoracionRiesgo = item.;
                        //objHistLaboral.HistoriaLaboral.AreaTrabajo = item.;                        
                        //objHistLaboral.HistoriaLaboral.AlturaLaboral = item.;
                        //objHistLaboral.HistoriaLaboral.DuracionTrabajosAlturaA = item.;
                        //objHistLaboral.HistoriaLaboral.DuracionPuestoM = item.;
                        //objHistLaboral.HistoriaLaboral.PostulaPuesto = item.;

                        LanzarDatos(client.EnviarDatosHistoriaLaboral(objHistLaboral), e, column, serviceId);
                    }
                    #endregion

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
                case "DatosApMedicos":
                    //do something

                    break;
                case "DatosEpisodios":
                    //do something
                    break;
                case "DatosExamenFisico":
                    //do something
                    break;
                case "DatosHistoriaFisiologico":
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
                e.Row.Cells[p].ButtonAppearance.Image = Resources.accept;
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

        private XmlAudiometria objAudiometriaValues( List<ServiceComponentFieldValuesList> data)
        {

            var objAud = new XmlAudiometria();
            foreach (var item in data)
            {

                #region ComponentsFieldAudiometri

                switch (item.v_ComponentFieldId)
                {
                    #region Tonal AÉREA no enmascarada OD

                    case Constants.txt_VA_OD_125:

                        objAud.ATAND125 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_250:

                        objAud.ATAND250 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_500:

                        objAud.ATAND500 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_1000:

                        objAud.ATAND1000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_2000:

                        objAud.ATAND2000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_3000:

                        objAud.ATAND3000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_4000:

                        objAud.ATAND4000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_6000:

                        objAud.ATAND6000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_8000:

                        objAud.ATAND8000 = item.v_Value1;

                        break;
                    
                    #endregion

                    #region Tonal AÉREA no enmascarada OI

                    case Constants.txt_VA_OI_125:

                        objAud.ATANI125 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_250:

                        objAud.ATANI250 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_500:

                        objAud.ATANI500 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_1000:

                        objAud.ATANI1000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_2000:

                        objAud.ATANI2000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_3000:

                        objAud.ATANI3000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_4000:

                        objAud.ATANI4000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_6000:

                        objAud.ATANI6000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_8000:

                        objAud.ATANI8000 = item.v_Value1;

                        break;

                    #endregion

                    #region Tonal AÉREA enmascarada OD

                    case Constants.txt_EM_OD_125:

                        objAud.ATAED125 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_250:

                        objAud.ATAED250 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_500:

                        objAud.ATAED500 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_1000:

                        objAud.ATAED1000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_2000:

                        objAud.ATAED2000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_3000:

                        objAud.ATAED3000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_4000:

                        objAud.ATAED4000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_6000:

                        objAud.ATAED6000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_8000:

                        objAud.ATAED8000 = item.v_Value1;

                        break;

                    #endregion

                    #region Tonal AÉREA enmascarada OI

                    case Constants.txt_EM_OI_125:

                        objAud.ATAEI125 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_250:

                        objAud.ATAEI250 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_500:

                        objAud.ATAEI500 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_1000:

                        objAud.ATAEI1000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_2000:

                        objAud.ATAEI2000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_3000:

                        objAud.ATAEI3000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_4000:

                        objAud.ATAEI4000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_6000:

                        objAud.ATAEI6000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_8000:

                        objAud.ATAEI8000 = item.v_Value1;

                        break;

                    #endregion

                    #region Tonal OSEA no enmascarada OD

                    case Constants.txt_VO_OD_125:

                        objAud.ATOND125 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_250:

                        objAud.ATOND250 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_500:

                        objAud.ATOND500 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_1000:

                        objAud.ATOND1000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_2000:

                        objAud.ATOND2000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_3000:

                        objAud.ATOND3000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_4000:

                        objAud.ATOND4000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_6000:

                        objAud.ATOND6000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_8000:

                        objAud.ATOND8000 = item.v_Value1;

                        break;

                    #endregion

                    #region Tonal OSEA no enmascarada OI

                    case Constants.txt_VO_OI_125:

                        objAud.ATONI125 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_250:

                        objAud.ATONI250 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_500:

                        objAud.ATONI500 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_1000:

                        objAud.ATONI1000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_2000:

                        objAud.ATONI2000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_3000:

                        objAud.ATONI3000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_4000:

                        objAud.ATONI4000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_6000:

                        objAud.ATONI6000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_8000:

                        objAud.ATONI8000 = item.v_Value1;

                        break;

                    #endregion

                    #region Descripcion - Observaciones OD-OI

                    case Constants.txt_AUD_DX_OCUPACIONAL_AUTO_OD:

                        objAud.DESCATOTO = item.v_Value1;

                        break;
                    case Constants.txt_AUD_DX_OCUPACIONAL_AUTO_OI:

                        objAud.DESCATOTOI = item.v_Value1;

                        break;
                    case Constants.txt_AUD_DX_CLINICO_AUTO_OD:

                        objAud.OBSATOTOD = item.v_Value1;

                        break;
                    case Constants.txt_AUD_DX_CLINICO_AUTO_OI:

                        objAud.OBSATOTOI = item.v_Value1;

                        break;

                    #endregion
                    
                }

                #endregion

            }
            return objAud;
        }
        
    }
}
