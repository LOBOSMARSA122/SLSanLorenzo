using Sigesoft.Common;
using Sigesoft.Node.Contasol.Integration.Contasol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.DAL;
using System.Data.Entity;
using Sigesoft.Node.WinClient.BE;
using planDto = Sigesoft.Server.WebClientAdmin.BE.planDto;

namespace Sigesoft.Node.WinClient.UI.Configuration
{
    public partial class frmAddPlan : Form
    {
        private string _protocolId;
        private string _pstrNombreProtocolo;
        OperationResult objOperationResult1 = new OperationResult();
        private string _aseguradoraId;
        private string v_IdLinea;
        private string v_Nombre;
        BindingList<planDto> _gridDataSouce;
        List<planDto> _listToDelete;
        PlanBl _objPlanBl;
        public frmAddPlan(string protocolId, string aseguradoraId, string pstrNombreProtocolo)
        {
            _protocolId = protocolId;
            _aseguradoraId = aseguradoraId;
            _pstrNombreProtocolo = pstrNombreProtocolo;
            InitializeComponent();
            _objPlanBl = new PlanBl();
        }

        private void frmAddPlan_Load(object sender, EventArgs e)
        {
            cbLine.Select();
            object listaLine = LlenarLines();
            cbLine.DataSource = listaLine;
            cbLine.DisplayMember = "v_Nombre";
            cbLine.ValueMember = "v_IdLinea";
            cbLine.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            cbLine.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cbLine.DropDownWidth = 590;
            cbLine.DisplayLayout.Bands[0].Columns[0].Width = 20;
            cbLine.DisplayLayout.Bands[0].Columns[1].Width = 550;

        }

        private object LlenarLines()
        {
            #region Conexion SAMBHS
            ConexionSambhs conectaConexionSambhs = new ConexionSambhs();conectaConexionSambhs.openSambhs();
            var cadenasam = "select v_Nombre, v_IdLinea  from linea where i_Eliminado=0";
            var comando = new SqlCommand(cadenasam, connection: conectaConexionSambhs.conectarSambhs);
            var lector = comando.ExecuteReader();
            string preciounitario = "";
            List <ListaLineas> objListaLineas = new List<ListaLineas>();
            
            while (lector.Read())
            {
                ListaLineas Lista = new ListaLineas();
                Lista.v_Nombre = lector.GetValue(0).ToString();
                Lista.v_IdLinea = lector.GetValue(1).ToString();
                objListaLineas.Add(Lista);
            }
            lector.Close();
            conectaConexionSambhs.closeSambhs();
            #endregion

            return objListaLineas;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            #region VALIDACION DE DUPLICADOS
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena = "select count (*) as result from [dbo].[plan] " +
                         "where v_OrganizationSeguroId='"+_aseguradoraId+"' and v_ProtocoloId='"+_protocolId+"' and v_IdUnidadProductiva='"+txtUnidadProdId.Text+"'";
            SqlCommand comando = new SqlCommand(cadena, connection: conectasam.conectarsigesoft);
            var lector = comando.ExecuteReader();
            string result = "";
            while (lector.Read())
            {
                result = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
            if (result !="0")
            {
                MessageBox.Show("Ya se agrego esta unidad al plan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            #endregion
            planDto ObjPlan = new planDto();
            ObjPlan.v_OrganizationSeguroId = _aseguradoraId;
            ObjPlan.v_ProtocoloId = _protocolId;
            ObjPlan.v_IdUnidadProductiva = txtUnidadProdId.Text;
            ObjPlan.d_Importe = txtDeducible.Text == "" ? (decimal?)null : decimal.Parse(txtDeducible.Text);
            ObjPlan.i_EsCoaseguro = chkCoaseguro.Checked == true ? 1 : 0;
            ObjPlan.i_EsDeducible = chkDeducible.Checked == true ? 1 : 0;
            ObjPlan.d_ImporteCo = txtCoaseguro.Text == "" ? (decimal?)null : decimal.Parse(txtCoaseguro.Text);
            #region Conexion SIGESOFT Insert
            
            conectasam.opensigesoft();
            string cadenanull = "";
            if (ObjPlan.d_Importe == null && ObjPlan.d_ImporteCo != null)
            {
                cadenanull = "INSERT INTO [dbo].[plan] (v_OrganizationSeguroId, v_ProtocoloId, v_IdUnidadProductiva, i_EsDeducible, i_EsCoaseguro, d_Importe, d_ImporteCo) " +
                             "VALUES ('" + ObjPlan.v_OrganizationSeguroId + "', '" + ObjPlan.v_ProtocoloId + "', '" + ObjPlan.v_IdUnidadProductiva + "', " + ObjPlan.i_EsDeducible + ", " + ObjPlan.i_EsCoaseguro + ", " + "NULL" + ", " + ObjPlan.d_ImporteCo + ")";
            }
            else if (ObjPlan.d_ImporteCo == null && ObjPlan.d_Importe != null)
            {
                cadenanull = "INSERT INTO [dbo].[plan] (v_OrganizationSeguroId, v_ProtocoloId, v_IdUnidadProductiva, i_EsDeducible, i_EsCoaseguro, d_Importe, d_ImporteCo) " +
                    "VALUES ('" + ObjPlan.v_OrganizationSeguroId + "', '" + ObjPlan.v_ProtocoloId + "', '" + ObjPlan.v_IdUnidadProductiva + "', " + ObjPlan.i_EsDeducible + ", " + ObjPlan.i_EsCoaseguro + ", " + ObjPlan.d_Importe + ", " + "NULL" + ")";
            }
            else if (ObjPlan.d_ImporteCo != null && ObjPlan.d_Importe != null)
            {
                cadenanull = "INSERT INTO [dbo].[plan] (v_OrganizationSeguroId, v_ProtocoloId, v_IdUnidadProductiva, i_EsDeducible, i_EsCoaseguro, d_Importe, d_ImporteCo) " +
                              "VALUES ('" + ObjPlan.v_OrganizationSeguroId + "', '" + ObjPlan.v_ProtocoloId + "', '" + ObjPlan.v_IdUnidadProductiva + "', " + ObjPlan.i_EsDeducible + ", " + ObjPlan.i_EsCoaseguro + ", " + ObjPlan.d_Importe + ", " + ObjPlan.d_ImporteCo + ")";
            }
            else
            {
                MessageBox.Show("Verifique la información ingresada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var cadena1 = cadenanull;
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            comando.ExecuteReader();
            conectasam.closesigesoft();
            #endregion
            //plan _objPlan = planAssembler.ToEntity(ObjPlan);
            //_objPlan.d_Importe = decimal.Parse(txtDeducible.Text);
            //SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            //dbContext.AddToplan(_objPlan);
            MessageBox.Show("Se agrego correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cbLine.Text = "";
            chkDeducible.Checked = false;
            chkCoaseguro.Checked = false;
            txtDeducible.Text = "";
            txtCoaseguro.Text = "";
        }

        private void cbLine_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e)
        {
            #region Conexion SAMBHS
            ConexionSambhs conectaConexionSambhs = new ConexionSambhs(); conectaConexionSambhs.openSambhs();
            var cadenasam = "select v_IdLinea  from linea where i_Eliminado=0 and v_Nombre ='"+cbLine.Text+"'";
            var comando = new SqlCommand(cadenasam, connection: conectaConexionSambhs.conectarSambhs);
            var lector = comando.ExecuteReader();
            string LineId = "";
            List<ListaLineas> objListaLineas = new List<ListaLineas>();

            while (lector.Read())
            {
                LineId = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectaConexionSambhs.closeSambhs();
            #endregion

            txtUnidadProdId.Text = LineId;
        }

        public void frmAddPlan_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ConexionSigesoft conectasam = new ConexionSigesoft();
            //conectasam.opensigesoft();
            //var cadena = "update protocol set " +
            //             "v_AseguradoraOrganizationId='"+_aseguradoraId+"' " +
            //             "where v_ProtocolId='"+_protocolId+"'";
            //SqlCommand comando = new SqlCommand(cadena, connection: conectasam.conectarsigesoft);
            //var lector = comando.ExecuteReader();
            //lector.Close();
            //conectasam.closesigesoft();
            //this.Close();
            btnCancelar_Click(sender, e);
        }

        public void btnCancelar_Click(object sender, EventArgs e)
        {
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena = "update protocol set " +
                         "v_AseguradoraOrganizationId='" + _aseguradoraId + "' " +
                         "where v_ProtocolId='" + _protocolId + "'";
            SqlCommand comando = new SqlCommand(cadena, connection: conectasam.conectarsigesoft);
            var lector = comando.ExecuteReader();
            lector.Close();
            conectasam.closesigesoft();
            this.Close();
        }
    }
}
