using Sigesoft.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System.Data.SqlClient;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmRegistroEmAmHos : Form
    {

        string _tabName;
        public frmRegistroEmAmHos(string tabName)
        {
            InitializeComponent();
            _tabName = tabName;

        }

        private void frmRegistroEmAmHos_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            #region Carga Groupbox
            if (_tabName == "Ambulatorio" || _tabName == "Emergencia")
            {
                uegbAmb.Visible = true;
                uegbHospi.Visible = false;
                uegbProcedimiento.Visible = false;
                uegbParto.Visible = false;
                uegbCirugia.Visible = false;
                #region Lista de Diagnosticos
                PacientBL _PacientBL = new PacientBL();
                cbDx.Select();
                var lista = _PacientBL.LlenarDxsTramas(ref objOperationResult);
                cbDx.DataSource = lista;
                cbDx.DisplayMember = "v_Name";
                cbDx.ValueMember = "v_CIE10Id";
                cbDx.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                cbDx.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.cbDx.DropDownWidth = 550;
                cbDx.DisplayLayout.Bands[0].Columns[0].Width = 400;
                cbDx.DisplayLayout.Bands[0].Columns[1].Width = 40;
                #endregion
                Utils.LoadDropDownList(cbGenero, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
                Utils.LoadDropDownList(cbRangoEdad, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 347, null), DropDownListAction.Select);


            }
            else if (_tabName == "Hospitalización")
            {
                uegbAmb.Visible = true;
                uegbHospi.Visible = true;
                uegbProcedimiento.Visible = false;
                uegbParto.Visible = false;
                uegbCirugia.Visible = false;
                #region Lista de Diagnosticos
                PacientBL _PacientBL = new PacientBL();
                cbDx.Select();
                var lista = _PacientBL.LlenarDxsTramas(ref objOperationResult);
                cbDx.DataSource = lista;
                cbDx.DisplayMember = "v_Name";
                cbDx.ValueMember = "v_CIE10Id";
                cbDx.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                cbDx.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.cbDx.DropDownWidth = 550;
                cbDx.DisplayLayout.Bands[0].Columns[0].Width = 400;
                cbDx.DisplayLayout.Bands[0].Columns[1].Width = 40;
                #endregion
                Utils.LoadDropDownList(cbGenero, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
                Utils.LoadDropDownList(cbRangoEdad, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 347, null), DropDownListAction.Select);
                Utils.LoadDropDownList(cbFallecido, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
                #region Lista de UPS - Especialidades
                cbUPS.Select();
                var listaUps = _PacientBL.LlenarListaUps(ref objOperationResult);
                cbUPS.DataSource = listaUps;
                cbUPS.DisplayMember = "v_Value1";
                cbUPS.ValueMember = "i_ParameterId";
                cbUPS.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                cbUPS.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.cbUPS.DropDownWidth = 590;
                cbUPS.DisplayLayout.Bands[0].Columns[0].Width = 550;
                cbUPS.DisplayLayout.Bands[0].Columns[1].Width = 40;

                #endregion
            }
            else if (_tabName == "Procedimientos / Cirugía")
            {
                uegbAmb.Visible = false;
                uegbHospi.Visible = false;
                uegbProcedimiento.Visible = true;
                uegbParto.Visible = false;
                uegbCirugia.Visible = true;
                uegbProcedimiento.Location = new Point(7, 4);
                uegbCirugia.Location = new Point(9, 135);
                #region Lista de Procedimientos
                PacientBL _PacientBL = new PacientBL();
                cbProcedimiento.Select();
                var listaproc = _PacientBL.LlenarListaProc(ref objOperationResult);
                cbProcedimiento.DataSource = listaproc;
                cbProcedimiento.DisplayMember = "v_Value1";
                cbProcedimiento.ValueMember = "i_ParameterId";
                cbProcedimiento.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                cbProcedimiento.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.cbProcedimiento.DropDownWidth = 590;
                cbProcedimiento.DisplayLayout.Bands[0].Columns[0].Width = 550;
                cbProcedimiento.DisplayLayout.Bands[0].Columns[1].Width = 40;
                #endregion
                //combo especialidades
                cbEspecialidades.Select();
                var listaUps = _PacientBL.LlenarListaUps(ref objOperationResult);
                cbEspecialidades.DataSource = listaUps;
                cbEspecialidades.DisplayMember = "v_Value1";
                cbEspecialidades.ValueMember = "i_ParameterId";
                cbEspecialidades.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                cbEspecialidades.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.cbEspecialidades.DropDownWidth = 590;
                cbEspecialidades.DisplayLayout.Bands[0].Columns[0].Width = 550;
                cbEspecialidades.DisplayLayout.Bands[0].Columns[1].Width = 40;
                Utils.LoadDropDownList(cbProgramacion, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 353, null), DropDownListAction.Select);
                Utils.LoadDropDownList(cbTipoCirugia, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 354, null), DropDownListAction.Select);

            }
            else if (_tabName == "Partos")
            {
                uegbAmb.Visible = false;
                uegbHospi.Visible = false;
                uegbProcedimiento.Visible = false;
                uegbParto.Visible = true;
                uegbCirugia.Visible = false;
                uegbParto.Location = new Point(7, 4);
                Utils.LoadDropDownList(cbTipoParto, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 350, null), DropDownListAction.Select);
                Utils.LoadDropDownList(cbTipoNacimiento, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 351, null), DropDownListAction.Select);
                Utils.LoadDropDownList(cbTipoComplicacion, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 352, null), DropDownListAction.Select);

            }

            #endregion



        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            TramasBL _tramasBL = new TramasBL();
            tramasDto objtramasDto = new tramasDto();
            if (_tabName == "Ambulatorio" || _tabName == "Emergencia" || _tabName == "Hospitalización")
            {
                objtramasDto.v_TipoRegistro = _tabName;
                objtramasDto.d_FechaIngreso = dtpFechaIngreso.Value;
                objtramasDto.i_Genero = int.Parse(cbGenero.SelectedValue.ToString());
                objtramasDto.i_GrupoEtario = int.Parse(cbRangoEdad.SelectedValue.ToString());
                objtramasDto.v_DiseasesName = cbDx.Text;
                objtramasDto.v_CIE10Id = txtCie10.Text;
                if (_tabName == "Hospitalización")
                {
                    objtramasDto.d_FechaAlta = dtpFechaAlta.Value;
                    objtramasDto.i_UPS = int.Parse(txtUpsId_1.Text);
                    objtramasDto.i_Procedimiento = int.Parse(cbFallecido.SelectedValue.ToString());//Cambiar procedimiento por fallecido 
                }
                _tramasBL.AddTramas(ref objOperationResult, objtramasDto, Globals.ClientSession.GetAsList());
            }
            else if (_tabName == "Procedimientos / Cirugía")
            {
                objtramasDto.v_TipoRegistro = _tabName;
                objtramasDto.d_FechaIngreso = dtpFechaProced.Value;
                objtramasDto.i_UPS = int.Parse(txtUpsId_2.Text);
                objtramasDto.i_Programacion = int.Parse(cbProgramacion.SelectedValue.ToString());
                objtramasDto.i_TipoCirugia = int.Parse(cbTipoCirugia.SelectedValue.ToString());
                if (txtHrsProg.Text != "")
                {
                    objtramasDto.i_HorasProg = int.Parse(txtHrsProg.Text);
                    objtramasDto.i_HorasEfect = int.Parse(txtHrsEfect.Text);
                    objtramasDto.i_HorasActo = int.Parse(txtHrsAct.Text);
                }

                objtramasDto.i_Procedimiento = int.Parse(txtProcedId.Text);
                _tramasBL.AddTramas(ref objOperationResult, objtramasDto, Globals.ClientSession.GetAsList());
            }
            else if (_tabName == "Partos")
            {
                objtramasDto.v_TipoRegistro = _tabName;
                objtramasDto.d_FechaIngreso = dtpFechaParto.Value;
                objtramasDto.i_TipoParto = int.Parse(cbTipoParto.SelectedValue.ToString());
                objtramasDto.i_TipoNacimiento = int.Parse(cbTipoNacimiento.SelectedValue.ToString());
                objtramasDto.i_TipoComplicacion = int.Parse(cbTipoComplicacion.SelectedValue.ToString());
                _tramasBL.AddTramas(ref objOperationResult, objtramasDto, Globals.ClientSession.GetAsList());
            }
            MessageBox.Show("Registro Exitoso", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void cbDx_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e)
        {
            //MessageBox.Show("hola MUNDO", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            #region Conexion SAM
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion

            string dx = cbDx.Text;
            var cadena1 = "select v_CIE10Description2 from cie10 where v_CIE10Description1='" + dx + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                txtCie10.Text = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbUPS_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e)
        {
            #region Conexion SAM
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion

            string ups = cbUPS.Text;
            var cadena1 = "select i_ParameterId from systemparameter where v_Value1='" + ups + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                txtUpsId_1.Text = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
        }

        private void cbEspecialidades_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e)
        {
            #region Conexion SAM
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion

            string ups = cbEspecialidades.Text;
            var cadena1 = "select i_ParameterId from systemparameter where v_Value1='" + ups + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                txtUpsId_2.Text = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
        }

        private void cbProcedimiento_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e)
        {
            #region Conexion SAM
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion

            string proced = cbProcedimiento.Text;
            var cadena1 = "select i_ParameterId from systemparameter where v_Value1='" + proced + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                txtProcedId.Text = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
        }
    }
}
