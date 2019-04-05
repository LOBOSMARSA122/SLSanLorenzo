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

namespace Sigesoft.Node.WinClient.UI.OperationsNatclar
{
    public partial class FormNatclar : Form
    {
        OperationsNatclarBl oNataclarBl = new OperationsNatclarBl(); 
        public FormNatclar()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            grdDataService.DataSource = oNataclarBl.Filter(dtpDateTimeStar.Value, dptDateTimeEnd.Value);
        }

        private void grdDataService_KeyDown(object sender, KeyEventArgs e)
        {
         

        }

        private void grdDataService_ClickCell(object sender, ClickCellEventArgs e)
        {
            var grid = (UltraGrid)sender;
            if (grid.ActiveCell == null) return;

            var column = grid.ActiveCell.Column.Key;

            if (column == "DatosPersonales")
            {
                MessageBox.Show("ssss");
            }
        }

        private void grdDataService_ClickCellButton(object sender, CellEventArgs e)
        {
            var serviceId = e.Cell.Row.Cells["v_ServiceId"].Value.ToString();
            var nameColumn = e.Cell.Column.Key;
            ProcessInformation(nameColumn, serviceId,e);
        }


        private void ProcessInformation(string column, string serviceId, CellEventArgs e)
        {
            WSRIProveedorExternoClient client = new WSRIProveedorExternoClient();

            #region Casos

            switch (column)
            {
                case "DatosPersonales":
                    //do something
                    Natclar oNatclarBL = new Natclar();
                    var datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                    var ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento, datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                    var obj = new EstructuraDatosAPMedicos();
                    obj.DatosPaciente = new XmlDatosPaciente();
                    obj.DatosPaciente.Nombre = datosPaciente.Nombre;
                    obj.DatosPaciente.DNI = datosPaciente.Dni;
                    obj.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                    obj.DatosPaciente.Direccion = datosPaciente.Direccion;
                    obj.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                    obj.DatosPaciente.Email = datosPaciente.Email;
                    obj.DatosPaciente.EstadoCivil = "S";
                    obj.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                    obj.DatosPaciente.HC = datosPaciente.Hc;
                    obj.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                    obj.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                    obj.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                    obj.DatosPaciente.Sexo = "M";
                    obj.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                    var result = client.EnviarDatosAPMedicos(obj);
                    e.Cell.ButtonAppearance.Image = result.EstadoXML != "OK" ? Resources.system_close : Resources.cog_stop;

                    break;
                case "DatosApMedicos":
                    //do something
                    break;
                case "DatosApQuirurgicos":
                    //do something
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
            
            //if (e.Cell.Column.Key == "DatosPersonales")
            //{
            //    WSRIProveedorExternoClient client = new WSRIProveedorExternoClient();

            //    var obj = new EstructuraDatosAPMedicos();
            //    obj.DatosPaciente = new XmlDatosPaciente();
            //    obj.DatosPaciente.Nombre = "Alberto";

            //    var result = client.EnviarDatosAPMedicos(obj);

            //    if (result.EstadoXML == "OK")
            //    {
            //        e.Cell.ButtonAppearance.Image = Resources.cog_stop;

            //        MessageBox.Show(serviceId);
            //    }
            //}

        }

        private void FormNatclar_Load(object sender, EventArgs e)
        {
            dtpDateTimeStar.Focus();
        }
    }
}
