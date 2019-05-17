using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.UI.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Constants = Sigesoft.Common.Constants;
using System.Transactions;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class AdditionalExam : Form
    {
        ServiceBL objServiceBL = new ServiceBL();
        private string _protocolId;
        private string _serviceId;
        private string _personId;
        AdditionalExamBL _additionalExamBl = new AdditionalExamBL();
        List<string> _ListaComponentes = null;
        public List<ServiceComponentList> _auxiliaryExams = null;    
        #region Properties

        private string MedicalExamId { get; set; }
        private string MedicalExamName { get; set; }
        private string CategoryName { get; set; }

        #endregion

        public AdditionalExam(List<string> ListaComponentes, string protocolId, string serviceId, string personId)
        {
            _ListaComponentes = ListaComponentes;
            _serviceId = serviceId;
            _protocolId = protocolId;
            _personId = personId;
            InitializeComponent();
        }

        private void AdditionalExam_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var ListServiceComponent = objServiceBL.GetAllComponents(ref objOperationResult, null, "");
            //grdDataServiceComponent.DataSource = ListServiceComponent;
            gdDataExams.DataSource = ListServiceComponent;

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
            conectasam.closesigesoft();

            #endregion
        }

        private void btnAgregarExamenAuxiliar_Click(object sender, EventArgs e)
        {
            AddAuxiliaryExam();   
        }


        private void AddAuxiliaryExam()
        {
            var findResult = lvExamenesSeleccionados.FindItemWithText(MedicalExamId);
            string IsProcessed = "0";
            string IsNewService = "0";
            var res = _ListaComponentes.Find(p => p == MedicalExamId);
            if (res != null)
            {
                var DialogResult = MessageBox.Show("Este examen ya se encuentra agregado, ¿Desea crear nuevo servicio?", "Error de validación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (DialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    #region Agenda Automática

                    IsNewService = "1";

                    #endregion
                }
                else
                {
                    return;
                }

            }
            // El examen ya esta agregado
            if (findResult != null)
            {
                MessageBox.Show("Por favor seleccione otro examen.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = new ListViewItem(new[] { MedicalExamId, IsProcessed, IsNewService });

            lvExamenesSeleccionados.Items.Add(row);

            gbExamenesSeleccionados.Text = string.Format("Examenes Seleccionados {0}", lvExamenesSeleccionados.Items.Count);
        }

        private void gdDataExams_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            bool row = gdDataExams.Selected.Rows.Count > 0;
            if (!row)
            {
                return;
            }
            if (gdDataExams.Selected.Rows[0].Cells["v_ComponentId"].Value == null)
            {
                btnAgregarExamenAuxiliar.Enabled = false;

                return;
            }
            else
            {
                btnAgregarExamenAuxiliar.Enabled = true;
            }

            lvExamenesSeleccionados.SelectedItems.Clear();

            if (gdDataExams.Selected.Rows.Count == 0)
                return;

            MedicalExamId = gdDataExams.Selected.Rows[0].Cells["v_ComponentId"].Value.ToString();
            MedicalExamName = gdDataExams.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            //ServiceComponentConcatId = gdDataExams.Selected.Rows[0].Cells["v_ServiceComponentId"].Value.ToString();

            if (gdDataExams.Selected.Rows[0].Cells["v_ComponentId"].Value != null)
            {
                MedicalExamName = gdDataExams.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            }
            else
            {
                MedicalExamName = string.Empty;
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            FilterComponents();
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

            gdDataExams.DataSource = ListServiceComponent;
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                FilterComponents();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (_auxiliaryExams == null)
            //    _auxiliaryExams = new List<ServiceComponentList>();
            try
            {
                using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
                {
                    List<AdditionalExamCustom> ListAdditionalExam = new List<AdditionalExamCustom>();

                    foreach (ListViewItem item in lvExamenesSeleccionados.Items)
                    {

                        AdditionalExamCustom _additionalExam = new AdditionalExamCustom();
                        var fields = item.SubItems;
                        _additionalExam.ComponentId = fields[0].Text;
                        _additionalExam.IsProcessed = int.Parse(fields[1].Text);
                        _additionalExam.IsNewService = int.Parse(fields[2].Text);
                        _additionalExam.Commentary = txtCommentary.Text;
                        _additionalExam.ServiceId = _serviceId;
                        _additionalExam.PersonId = _personId;
                        _additionalExam.ProtocolId = "";
                        if (_additionalExam.IsNewService == 1)
                        {
                            _additionalExam.ProtocolId = Constants.Prot_Hospi_Adic;
                        }
                        ListAdditionalExam.Add(_additionalExam);

                    }

                    using (var ts = new TransactionScope())
                    {
                        var success = _additionalExamBl.AddAdditionalExam(ListAdditionalExam, Globals.ClientSession.GetAsList());
                        if (!success)
                        {
                            throw new Exception("Sucedió un error, por favor vuelva a intentarlo");
                        }

                        ts.Complete();
                    }
                }
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
