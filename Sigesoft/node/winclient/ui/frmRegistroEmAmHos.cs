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
            #region Carga Groupbox
            if (_tabName == "Ambulatorio" || _tabName == "Emergencia")
            {
                uegbAmb.Visible = true;
                uegbAmb.Expanded = true;
                uegbHospi.Expanded = false;
                uegbHospi.Visible = false;
                uegbProcedimiento.Expanded = false;
                uegbProcedimiento.Visible = false;
                uegbParto.Expanded = false;
                uegbParto.Visible = false;
                uegbCirugia.Expanded = false;
                uegbCirugia.Visible = false;

            }
            else if (_tabName == "Hospitalización")
            {
                uegbAmb.Visible = true;
                uegbAmb.Expanded = true;
                uegbHospi.Expanded = true;
                uegbHospi.Visible = true;
                uegbProcedimiento.Expanded = false;
                uegbProcedimiento.Visible = false;
                uegbParto.Expanded = false;
                uegbParto.Visible = false;
                uegbCirugia.Expanded = false;
                uegbCirugia.Visible = false;
            }
            else if (_tabName == "Procedimientos")
            {
                uegbAmb.Visible = false;
                uegbAmb.Expanded = false;
                uegbHospi.Expanded = false;
                uegbHospi.Visible = false;
                uegbProcedimiento.Expanded = true;
                uegbProcedimiento.Visible = true;
                uegbParto.Expanded = false;
                uegbParto.Visible = false;
                uegbCirugia.Expanded = false;
                uegbCirugia.Visible = false;
                uegbProcedimiento.Location = new Point(7, 4);
            }
            else if (_tabName == "Partos")
            {
                uegbAmb.Visible = false;
                uegbAmb.Expanded = false;
                uegbHospi.Expanded = false;
                uegbHospi.Visible = false;
                uegbProcedimiento.Expanded = false;
                uegbProcedimiento.Visible = false;
                uegbParto.Expanded = true;
                uegbParto.Visible = true;
                uegbCirugia.Expanded = false;
                uegbCirugia.Visible = false;
                uegbParto.Location = new Point(7, 4);
            }
            else if (_tabName == "Cirugías")
            {
                uegbAmb.Visible = false;
                uegbAmb.Expanded = false;
                uegbHospi.Expanded = false;
                uegbHospi.Visible = false;
                uegbProcedimiento.Expanded = false;
                uegbProcedimiento.Visible = false;
                uegbParto.Expanded = false;
                uegbParto.Visible = false;
                uegbCirugia.Expanded = true;
                uegbCirugia.Visible = true;
                uegbCirugia.Location = new Point(7, 4);
            }
            #endregion

            #region Llena Combos
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbGenero, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbRangoEdad, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 347, null), DropDownListAction.Select);
            #endregion

            #region Cargar listas DB
            List<Diagnosticos> Dxs = new List<Diagnosticos>();
            #endregion

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            TramasBL _tramasBL = new TramasBL();
            tramasDto objtramasDto = new tramasDto();
            if (_tabName == "Ambulatorio" || _tabName == "Emergencia")
            {
                objtramasDto.d_FechaIngreso = dtpFechaIngreso.Value.Date;
                objtramasDto.i_Genero = int.Parse(cbGenero.SelectedValue.ToString());
                objtramasDto.i_GrupoEtario = int.Parse(cbRangoEdad.SelectedValue.ToString());
                objtramasDto.v_DiseasesName = cbDx.Text;
                objtramasDto.v_CIE10Id = txtCie10.Text;
                _tramasBL.AddTramas(ref objOperationResult, objtramasDto, Globals.ClientSession.GetAsList());
            }
        }
    }
}
