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
using System.IO;
using NetPdf;
using Infragistics.Win.UltraWinGrid;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmLiquidacionAseguradora : Form
    {
        string strFilterExpression;

        public frmLiquidacionAseguradora()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtPacient.Text)) Filters.Add("PacientDocument.Contains(\"" + txtPacient.Text.Trim() + "\")");
            if (cboEmpresa.SelectedValue.ToString() != "-1")
            {
                Filters.Add("EmpresaId==" + "\"" + cboEmpresa.SelectedValue + "\"");
            }
            strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.BindGrid();
            }
        }

        public List<LiquidacionAseguradora> _dataReport = new List<LiquidacionAseguradora>();
        private void BindGrid()
        {
            var objData = GetData(strFilterExpression);
            _dataReport = objData;
            grdData.DataSource = objData;
            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<LiquidacionAseguradora> GetData(string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            AseguradoraBL oAseguradoraBL = new AseguradoraBL();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var _objData = oAseguradoraBL.GetLiquidacionAseguradoraPagedAndFiltered(ref objOperationResult, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            txtTotalAseguradora.Text = _objData.Sum(p => p.TotalAseguradora).ToString();
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void frmLiquidacionAseguradora_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var dataListOrganization = BLL.Utils.GetOrganization(ref objOperationResult);
            Utils.LoadDropDownList(cboEmpresa, "Value1", "Id", dataListOrganization, DropDownListAction.All);          
            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Liquidación Asegueradora del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLiquidacion_Click(object sender, EventArgs e)
        {
            var serviceId = grdData.Selected.Rows[0].Cells["ServicioId"].Value.ToString();
            #region VALIDACIONES
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 =
                "select SR.v_NroCartaSolicitud, PP.v_OwnerName " +
                "from service SR  " +
                "inner join person PP on SR.v_PersonId = PP.v_PersonId  " +
                "where SR.v_ServiceId='" + serviceId + "' " +
                "group by SR.v_NroCartaSolicitud, PP.v_OwnerName";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string Nrocarta = ""; string NombreTitular = "";
            while (lector.Read())
            {
                Nrocarta = lector.GetValue(0).ToString() == "" ? "NO REGISTRADO" : lector.GetValue(0).ToString();
                NombreTitular = lector.GetValue(1).ToString() == "" ? "NO REGISTRADO" : lector.GetValue(1).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
            if (Nrocarta == "NO REGISTRADO")
            {
                MessageBox.Show("Tiene que registrar el Nro de carta o solicitud.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (NombreTitular == "NO REGISTRADO")
            {
                MessageBox.Show("Tiene que registrar un parentesco y el titular.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            string organizationId = grdData.Selected.Rows[0].Cells["Aseguradora"].Value.ToString();
            #region Conexion SAM
            
            conectasam.opensigesoft();
            #endregion
            cadena1 = "select v_OrganizationId from organization where v_Name='"+organizationId+"'";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                organizationId = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
            var serviceIdarray = new List<string>();
            serviceIdarray.Add(serviceId);
            OperationResult objOperationResult = new OperationResult();
            ServiceBL _serviceBL = new ServiceBL();
            _serviceBL.GenerarLiquidacion(ref objOperationResult, serviceIdarray.ToArray(),Globals.ClientSession.GetAsList(), organizationId );

            var data = _dataReport;
            var x = grdData.Selected.Rows[0].Cells["ServicioId"].Value;
            var result = data.FindAll(p => p.ServicioId == x).ToList();
            string ruta = Common.Utils.GetApplicationConfigValue("rutaPagoMedicos").ToString();
            var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
            string nombre = " Liquidacion - CSL";
            LiquidacionAseguradoraReport.CreateLiquidacionAseguradora(ruta + nombre + ".pdf", result, MedicalCenter);
        }

    }
}
