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
using Sigesoft.Node.WinClient.UI.CIE10;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmRegistroEmAmHos : Form
    {
        DateTime _fechaload;
        string _genero;
        string _edad;
        string _tabName;
        string _tramaId;
        string _mode;
        object lista;
        object listaUps;
        object listaproc;
        
        private tramasDto _tramaDto = null;
        private TramasBL _tramasBL = new TramasBL();
        public frmRegistroEmAmHos(string tabName, string idTrama, string mode, DateTime fecha, string genero, string edad, object  _lista, object _listaUps, object _listaproc)
        {
            InitializeComponent();
            _fechaload = fecha;
            _tabName = tabName;
            _tramaId = idTrama;
            _mode = mode;
            if (genero != String.Empty)
            {
                _genero = genero;
            }
            if (edad != String.Empty)
            {
                int parse_edad = int.Parse(edad);
                if (parse_edad < 1) { _edad = "Menores de 1 año"; }
                else if (parse_edad >= 1 && parse_edad <= 4) { _edad = "De 1 a 4 años"; }
                else if (parse_edad >= 5 && parse_edad <= 9) { _edad = "De 5 a 9 años"; }
                else if (parse_edad >= 10 && parse_edad <= 14) { _edad = "De 10 a 14 años"; }
                else if (parse_edad >= 15 && parse_edad <= 19) { _edad = "De 15 a 19 años"; }
                else if (parse_edad >= 20 && parse_edad <= 24) { _edad = "De 20 a 24 años"; }
                else if (parse_edad >= 25 && parse_edad <= 29) { _edad = "De 25 a 29 años"; }
                else if (parse_edad >= 30 && parse_edad <= 34) { _edad = "De 30 a 34 años"; }
                else if (parse_edad >= 35 && parse_edad <= 39) { _edad = "De 35 a 39 años"; }
                else if (parse_edad >= 40 && parse_edad <= 44) { _edad = "De 40 a 44 años"; }
                else if (parse_edad >= 45 && parse_edad <= 49) { _edad = "De 45 a 49 años"; }
                else if (parse_edad >= 50 && parse_edad <= 54) { _edad = "De 50 a 54 años"; }
                else if (parse_edad >= 55 && parse_edad <= 59) { _edad = "De 55 a 59 años"; }
                else if (parse_edad >= 60 && parse_edad <= 64) { _edad = "De 60 a 64 años"; }
                else if (parse_edad >= 65) { _edad = "De 65 años a más"; }
            }
            
           
           
            lista = _lista;
            listaUps = _listaUps;
            listaproc = _listaproc;
            
        }

        private void frmRegistroEmAmHos_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (_mode == "New")
            {
                #region Carga Groupbox
                if (_tabName == "Ambulatorio" || _tabName == "Emergencia")
                {
                    uegbAmb.Visible = true;
                    uegbHospi.Visible = false;
                    uegbProcedimiento.Visible = false;
                    uegbParto.Visible = false;
                    uegbCirugia.Visible = false;
                    #region Lista de Diagnosticos
                    
                    cbDx.Select();
                    
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
                    dtpFechaIngreso.Value = _fechaload;
                    cbGenero.Text = _genero;
                    cbRangoEdad.Text = _edad;

                }
                else if (_tabName == "Hospitalización")
                {
                    uegbAmb.Visible = true;
                    uegbHospi.Visible = true;
                    uegbProcedimiento.Visible = false;
                    uegbParto.Visible = false;
                    uegbCirugia.Visible = false;
                    #region Lista de Diagnosticos
                    
                    cbDx.Select();                    
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
                    
                    cbUPS.DataSource = listaUps;
                    cbUPS.DisplayMember = "v_Value1";
                    cbUPS.ValueMember = "i_ParameterId";
                    cbUPS.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                    cbUPS.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                    this.cbUPS.DropDownWidth = 590;
                    cbUPS.DisplayLayout.Bands[0].Columns[0].Width = 550;
                    cbUPS.DisplayLayout.Bands[0].Columns[1].Width = 40;

                    #endregion
                    dtpFechaIngreso.Value = _fechaload;
                    cbGenero.Text = _genero;
                    cbRangoEdad.Text = _edad;
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
                   
                    cbProcedimiento.Select();
                    
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
                    dtpFechaIngreso.Value = _fechaload;
                    cbGenero.Text = _genero;
                    cbRangoEdad.Text = _edad;

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
                    dtpFechaIngreso.Value = _fechaload;
                    cbGenero.Text = _genero;
                    cbRangoEdad.Text = _edad;

                }

                #endregion
            }
            else if (_mode == "Edit")
            {
                _tramaDto = _tramasBL.GetTrama(ref objOperationResult, _tramaId);

                #region Carga Groupbox
                if (_tabName == "Ambulatorio" || _tabName == "Emergencia")
                {
                    uegbAmb.Visible = true;
                    uegbHospi.Visible = false;
                    uegbProcedimiento.Visible = false;
                    uegbParto.Visible = false;
                    uegbCirugia.Visible = false;

                    Utils.LoadDropDownList(cbGenero, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
                    Utils.LoadDropDownList(cbRangoEdad, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 347, null), DropDownListAction.Select);
                    
                    dtpFechaIngreso.Value = _tramaDto.d_FechaIngreso.Value;
                    cbGenero.SelectedValue = _tramaDto.i_Genero.ToString();
                    cbRangoEdad.SelectedValue = _tramaDto.i_GrupoEtario.ToString();

                    #region Lista de Diagnosticos
                    cbDx.DataSource = lista;
                    cbDx.DisplayMember = "v_Name";
                    cbDx.ValueMember = "v_CIE10Id";
                    cbDx.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                    cbDx.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                    this.cbDx.DropDownWidth = 550;
                    cbDx.DisplayLayout.Bands[0].Columns[0].Width = 400;
                    cbDx.DisplayLayout.Bands[0].Columns[1].Width = 40;
                    if (!string.IsNullOrEmpty(_tramaDto.v_DiseasesName))
                    {
                        cbDx.Text = _tramaDto.v_DiseasesName;
                    }
                    #endregion

                    txtCie10.Text = _tramaDto.v_CIE10Id;
                }
                else if (_tabName == "Hospitalización")
                {
                    uegbAmb.Visible = true;
                    uegbHospi.Visible = true;
                    uegbProcedimiento.Visible = false;
                    uegbParto.Visible = false;
                    uegbCirugia.Visible = false;

                    Utils.LoadDropDownList(cbGenero, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
                    Utils.LoadDropDownList(cbRangoEdad, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 347, null), DropDownListAction.Select);
                    Utils.LoadDropDownList(cbFallecido, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);

                    dtpFechaIngreso.Value = _tramaDto.d_FechaIngreso.Value;
                    cbGenero.SelectedValue = _tramaDto.i_Genero.ToString();
                    cbRangoEdad.SelectedValue = _tramaDto.i_GrupoEtario.ToString();
                    cbFallecido.SelectedValue = _tramaDto.i_Procedimiento.ToString();
                    txtUpsId_1.Text = _tramaDto.i_UPS.ToString();
                    dtpFechaAlta.Value = _tramaDto.d_FechaAlta.Value;

                    #region Lista de Diagnosticos
                    
                    cbDx.DataSource = lista;
                    cbDx.DisplayMember = "v_Name";
                    cbDx.ValueMember = "v_CIE10Id";
                    cbDx.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                    cbDx.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                    this.cbDx.DropDownWidth = 550;
                    cbDx.DisplayLayout.Bands[0].Columns[0].Width = 400;
                    cbDx.DisplayLayout.Bands[0].Columns[1].Width = 40;
                    if (!string.IsNullOrEmpty(_tramaDto.v_DiseasesName))
                    {
                        cbDx.Text = _tramaDto.v_DiseasesName;
                    }
                    #endregion
                    txtCie10.Text = _tramaDto.v_CIE10Id;
                    
                    #region Lista de UPS - Especialidades
                    //cbUPS.Select();
                   
                    cbUPS.DataSource = listaUps;
                    cbUPS.DisplayMember = "v_Value1";
                    cbUPS.ValueMember = "i_ParameterId";
                    cbUPS.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                    cbUPS.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                    this.cbUPS.DropDownWidth = 590;
                    cbUPS.DisplayLayout.Bands[0].Columns[0].Width = 550;
                    cbUPS.DisplayLayout.Bands[0].Columns[1].Width = 40;
                    if (!string.IsNullOrEmpty(_tramaDto.i_UPS.ToString()))
                    {
                        cbUPS.Text = _tramaDto.i_UPS.ToString();
                    }
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

                    Utils.LoadDropDownList(cbProgramacion, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 353, null), DropDownListAction.Select);
                    Utils.LoadDropDownList(cbTipoCirugia, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 354, null), DropDownListAction.Select);

                    dtpFechaProced.Value = _tramaDto.d_FechaIngreso.Value;
                    txtUpsId_2.Text = _tramaDto.i_UPS.ToString();
                    txtProcedId.Text = _tramaDto.i_Procedimiento.ToString();
                    cbProgramacion.SelectedValue = _tramaDto.i_Programacion.ToString();
                    cbTipoCirugia.SelectedValue = _tramaDto.i_TipoCirugia.ToString();
                    txtHrsProg.Text = _tramaDto.i_HorasProg.ToString();
                    txtHrsEfect.Text = _tramaDto.i_HorasEfect.ToString();
                    txtHrsAct.Text = _tramaDto.i_HorasActo.ToString();

                    #region Lista de Procedimientos
                   
                    cbProcedimiento.DataSource = listaproc;
                    cbProcedimiento.DisplayMember = "v_Value1";
                    cbProcedimiento.ValueMember = "i_ParameterId";
                    cbProcedimiento.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                    cbProcedimiento.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                    this.cbProcedimiento.DropDownWidth = 590;
                    cbProcedimiento.DisplayLayout.Bands[0].Columns[0].Width = 550;
                    cbProcedimiento.DisplayLayout.Bands[0].Columns[1].Width = 40;
                    if (!string.IsNullOrEmpty(_tramaDto.i_Procedimiento.ToString()))
                    {
                        cbProcedimiento.Text = _tramaDto.i_Procedimiento.ToString();
                    }
                    #endregion
                    //combo especialidades
                    //cbEspecialidades.Select();
                    
                    cbEspecialidades.DataSource = listaUps;
                    cbEspecialidades.DisplayMember = "v_Value1";
                    cbEspecialidades.ValueMember = "i_ParameterId";
                    cbEspecialidades.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                    cbEspecialidades.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                    this.cbEspecialidades.DropDownWidth = 590;
                    cbEspecialidades.DisplayLayout.Bands[0].Columns[0].Width = 550;
                    cbEspecialidades.DisplayLayout.Bands[0].Columns[1].Width = 40;
                    if (!string.IsNullOrEmpty(_tramaDto.i_UPS.ToString()))
                    {
                        cbEspecialidades.Text = _tramaDto.i_UPS.ToString();
                    }
                    
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

                    dtpFechaParto.Value = _tramaDto.d_FechaIngreso.Value;
                    cbTipoParto.SelectedValue = _tramaDto.i_TipoParto.ToString();
                    cbTipoNacimiento.SelectedValue = _tramaDto.i_TipoNacimiento.ToString();
                    cbTipoComplicacion.SelectedValue = _tramaDto.i_TipoComplicacion.ToString();
                }
                #endregion
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Cie10MF _cie10MF = new Cie10MF();
            //TramasBL _tramasBL = new TramasBL();
            //var cie10 = txtCie10.Text;
            //if (int.Parse(cbGenero.SelectedValue.ToString()) == (int)Gender.MASCULINO)
            //{
            //    var cie10Masculino = _cie10MF.Femeninos.ToList().Find(x => x == cie10);
            //    if (cie10Masculino != null)
            //    {
            //        if (cie10Masculino.Length > 0)
            //        {
            //            MessageBox.Show("El diagnóstico elegido pertenece solo al género FEMENINO.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            return;
            //        }
            //    }
                
            //}
            //else if (int.Parse(cbGenero.SelectedValue.ToString()) == (int)Gender.FEMENINO)
            //{
            //    var cie10Femenino = _cie10MF.Masculinos.ToList().Find(x => x == cie10);
            //    if (cie10Femenino != null)
            //    {
            //        if (cie10Femenino.Length > 0)
            //        {
            //            MessageBox.Show("El diagnóstico elegido pertenece solo al género MASCULINO.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            return;
            //        }
            //    }
                
            //}

            _tramaDto = new tramasDto();
            if (_mode == "New")
            {
                bool result = true;
                if (_tabName == "Ambulatorio" || _tabName == "Emergencia" || _tabName == "Hospitalización")
                {
                    //
                    var cie10 = txtCie10.Text;
                    if (int.Parse(cbGenero.SelectedValue.ToString()) == (int)Gender.MASCULINO)
                    {
                        var cie10Masculino = _cie10MF.Femeninos.ToList().Find(x => x == cie10);
                        if (cie10Masculino != null)
                        {
                            if (cie10Masculino.Length > 0)
                            {
                                MessageBox.Show("El diagnóstico elegido pertenece solo al género FEMENINO.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                    }
                    else if (int.Parse(cbGenero.SelectedValue.ToString()) == (int)Gender.FEMENINO)
                    {
                        var cie10Femenino = _cie10MF.Masculinos.ToList().Find(x => x == cie10);
                        if (cie10Femenino != null)
                        {
                            if (cie10Femenino.Length > 0)
                            {
                                MessageBox.Show("El diagnóstico elegido pertenece solo al género MASCULINO.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                    }
                    //
                    _tramaDto.v_TipoRegistro = _tabName;
                    _tramaDto.d_FechaIngreso = dtpFechaIngreso.Value;
                    if (cbGenero.SelectedValue.ToString() == "-1" || cbRangoEdad.SelectedValue.ToString() == "-1" || cbDx.Text == "")
                    {
                        MessageBox.Show("Completar los campos vacíos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _tramaDto.i_Genero = int.Parse(cbGenero.SelectedValue.ToString());
                    _tramaDto.i_GrupoEtario = int.Parse(cbRangoEdad.SelectedValue.ToString());
                    _tramaDto.v_DiseasesName = cbDx.Text;
                    _tramaDto.v_CIE10Id = txtCie10.Text;
                    if (_tabName == "Hospitalización")
                    {
                        if (cbUPS.Text == "" || cbFallecido.SelectedValue.ToString() == "-1")
                        {
                            MessageBox.Show("No dejar campos vacíos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            result = false;
                            return;
                        }
                        if (result == true)
                        {
                            _tramaDto.d_FechaAlta = dtpFechaAlta.Value;
                            _tramaDto.i_UPS = int.Parse(txtUpsId_1.Text);
                            _tramaDto.i_Procedimiento = int.Parse(cbFallecido.SelectedValue.ToString());//Cambiar procedimiento por fallecido 
                        }
                       
                    }
                    _tramasBL.AddTramas(ref objOperationResult, _tramaDto, Globals.ClientSession.GetAsList());
                }
                else if (_tabName == "Procedimientos / Cirugía")
                {
                    _tramaDto.v_TipoRegistro = _tabName;
                    _tramaDto.d_FechaIngreso = dtpFechaProced.Value;
                    _tramaDto.i_UPS = int.Parse(txtUpsId_2.Text);
                    if (cbProcedimiento.Text == "" || cbEspecialidades.Text == "")
                    {
                        MessageBox.Show("No dejar campos vacíos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _tramaDto.i_Programacion = int.Parse(cbProgramacion.SelectedValue.ToString());
                    _tramaDto.i_TipoCirugia = int.Parse(cbTipoCirugia.SelectedValue.ToString());
                    if (txtHrsProg.Text != "")
                    {
                        _tramaDto.i_HorasProg = int.Parse(txtHrsProg.Text);
                        _tramaDto.i_HorasEfect = int.Parse(txtHrsEfect.Text);
                        _tramaDto.i_HorasActo = int.Parse(txtHrsAct.Text);
                    }

                    _tramaDto.i_Procedimiento = int.Parse(txtProcedId.Text);
                    _tramasBL.AddTramas(ref objOperationResult, _tramaDto, Globals.ClientSession.GetAsList());
                }
                else if (_tabName == "Partos")
                {
                    _tramaDto.v_TipoRegistro = _tabName;
                    _tramaDto.d_FechaIngreso = dtpFechaParto.Value;
                    if (cbTipoParto.SelectedValue.ToString() == "-1" || cbTipoNacimiento.SelectedValue.ToString() == "-1" || cbTipoComplicacion.SelectedValue.ToString() == "-1")
                    {
                        MessageBox.Show("No dejar campos vacíos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _tramaDto.i_TipoParto = int.Parse(cbTipoParto.SelectedValue.ToString());
                    _tramaDto.i_TipoNacimiento = int.Parse(cbTipoNacimiento.SelectedValue.ToString());
                    _tramaDto.i_TipoComplicacion = int.Parse(cbTipoComplicacion.SelectedValue.ToString());
                    _tramasBL.AddTramas(ref objOperationResult, _tramaDto, Globals.ClientSession.GetAsList());
                }
                MessageBox.Show("Registro Exitoso", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if (_mode == "Edit")
            {
                var _getTrama = _tramasBL.GetTrama(ref objOperationResult, _tramaId);
                if (_tabName == "Ambulatorio" || _tabName == "Emergencia" || _tabName == "Hospitalización")
                {

                    //
                    var cie10 = txtCie10.Text;
                    if (int.Parse(cbGenero.SelectedValue.ToString()) == (int)Gender.MASCULINO)
                    {
                        var cie10Masculino = _cie10MF.Femeninos.ToList().Find(x => x == cie10);
                        if (cie10Masculino != null)
                        {
                            if (cie10Masculino.Length > 0)
                            {
                                MessageBox.Show("El diagnóstico elegido pertenece solo al género FEMENINO.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                    }
                    else if (int.Parse(cbGenero.SelectedValue.ToString()) == (int)Gender.FEMENINO)
                    {
                        var cie10Femenino = _cie10MF.Masculinos.ToList().Find(x => x == cie10);
                        if (cie10Femenino != null)
                        {
                            if (cie10Femenino.Length > 0)
                            {
                                MessageBox.Show("El diagnóstico elegido pertenece solo al género MASCULINO.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                    }
                    //

                    bool result = true;
                    _tramaDto.v_TramaId = _tramaId;
                    _tramaDto.v_TipoRegistro = _tabName;
                    _tramaDto.d_FechaIngreso = dtpFechaIngreso.Value;
                    if (cbGenero.SelectedValue.ToString() == "-1" || cbRangoEdad.SelectedValue.ToString() == "-1" || cbDx.Text == "")
                    {
                        MessageBox.Show("Completar los campos vacíos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _tramaDto.i_Genero = int.Parse(cbGenero.SelectedValue.ToString());
                    _tramaDto.i_GrupoEtario = int.Parse(cbRangoEdad.SelectedValue.ToString());
                    _tramaDto.v_DiseasesName = cbDx.Text;
                    _tramaDto.v_CIE10Id = txtCie10.Text;
                    if (_tabName == "Hospitalización")
                    {
                        if (cbUPS.Text == "" || cbFallecido.SelectedValue.ToString() == "-1")
                        {
                            MessageBox.Show("No dejar campos vacíos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            result = false;
                            return;
                        }
                        if (result == true)
                        {
                            _tramaDto.d_FechaAlta = dtpFechaAlta.Value;
                            _tramaDto.i_UPS = int.Parse(txtUpsId_1.Text);
                            _tramaDto.i_Procedimiento = int.Parse(cbFallecido.SelectedValue.ToString());//Cambiar procedimiento por fallecido 
                        }
                    }
                    _tramaDto.d_InsertDate = _getTrama.d_InsertDate.Value;
                    _tramaDto.i_InsertUserId = _getTrama.i_InsertUserId;
                    _tramasBL.UpdateTrama(ref objOperationResult, _tramaDto, Globals.ClientSession.GetAsList());
                }
                else if (_tabName == "Procedimientos / Cirugía")
                {
                    _tramaDto.v_TramaId = _tramaId;
                    _tramaDto.v_TipoRegistro = _tabName;
                    _tramaDto.d_FechaIngreso = dtpFechaProced.Value;
                    _tramaDto.i_UPS = int.Parse(txtUpsId_2.Text);
                    if (cbProcedimiento.Text == "" || cbEspecialidades.Text == "")
                    {
                        MessageBox.Show("No dejar campos vacíos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _tramaDto.i_Programacion = int.Parse(cbProgramacion.SelectedValue.ToString());
                    _tramaDto.i_TipoCirugia = int.Parse(cbTipoCirugia.SelectedValue.ToString());
                    if (txtHrsProg.Text != "")
                    {
                        _tramaDto.i_HorasProg = int.Parse(txtHrsProg.Text);
                        _tramaDto.i_HorasEfect = int.Parse(txtHrsEfect.Text);
                        _tramaDto.i_HorasActo = int.Parse(txtHrsAct.Text);
                    }

                    _tramaDto.i_Procedimiento = int.Parse(txtProcedId.Text);

                    _tramaDto.d_InsertDate = _getTrama.d_InsertDate.Value;
                    _tramaDto.i_InsertUserId = _getTrama.i_InsertUserId;
                    _tramasBL.UpdateTrama(ref objOperationResult, _tramaDto, Globals.ClientSession.GetAsList());
                }
                else if (_tabName == "Partos")
                {
                    _tramaDto.v_TramaId = _tramaId;
                    _tramaDto.v_TipoRegistro = _tabName;
                    _tramaDto.d_FechaIngreso = dtpFechaParto.Value;
                    _tramaDto.i_TipoParto = int.Parse(cbTipoParto.SelectedValue.ToString());
                    _tramaDto.i_TipoNacimiento = int.Parse(cbTipoNacimiento.SelectedValue.ToString());
                    _tramaDto.i_TipoComplicacion = int.Parse(cbTipoComplicacion.SelectedValue.ToString());
                    
                    _tramaDto.d_InsertDate = _getTrama.d_InsertDate.Value;
                    _tramaDto.i_InsertUserId = _getTrama.i_InsertUserId;
                    _tramasBL.UpdateTrama(ref objOperationResult, _tramaDto, Globals.ClientSession.GetAsList());
                }
                MessageBox.Show("Actualización Exitosa", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            //frmTramasSusalud frmTramas = new frmTramasSusalud();
            //frmTramas.btnFilter_Click(sender, e);

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

        private void cbDx_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbDx.Text != "")
            {
                cbDx.SelectionStart = 0;
                cbDx.SelectionLength = cbDx.Text.Length;
            }
        }

        private void cbUPS_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbUPS.Text != "")
            {
                cbUPS.SelectionStart = 0;
                cbUPS.SelectionLength = cbUPS.Text.Length;
            }
        }

        private void cbEspecialidades_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbEspecialidades.Text != "")
            {
                cbEspecialidades.SelectionStart = 0;
                cbEspecialidades.SelectionLength = cbEspecialidades.Text.Length;
            }
        }

        private void cbProcedimiento_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbProcedimiento.Text != "")
            {
                cbProcedimiento.SelectionStart = 0;
                cbProcedimiento.SelectionLength = cbProcedimiento.Text.Length;
            }
        }
    }
}
