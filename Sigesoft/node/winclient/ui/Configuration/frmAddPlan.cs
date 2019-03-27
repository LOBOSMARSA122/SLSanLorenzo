using Sigesoft.Common;
using Sigesoft.Node.Contasol.Integration.Contasol;
using Sigesoft.Server.WebClientAdmin.BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Configuration
{
    public partial class frmAddPlan : Form
    {
        private string _protocolId;
        OperationResult objOperationResult1 = new OperationResult();
        private string _aseguradoraId;
        private string v_IdLinea;
        private string v_Nombre;
        BindingList<planDto> _gridDataSouce;
        List<planDto> _listToDelete;
        PlanBl _objPlanBl;
        public frmAddPlan()
        {
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
           
        }
    }
}
