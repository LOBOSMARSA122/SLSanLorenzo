using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using System.IO;


namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucOtoscopia : UserControl
    {
        bool _isChangueValueControl = false;
        List<ServiceComponentFieldValuesList> _listOfAtencionAdulto1 = new List<ServiceComponentFieldValuesList>();
        ServiceComponentFieldValuesList _UserControlValores = null;

        #region "------------- Public Events -------------"
        /// <summary>
        /// Se desencadena cada vez que se cambia un valor del examen de Audiometria.
        /// </summary>
        public event EventHandler<AudiometriaAfterValueChangeEventArgs> AfterValueChange;
        protected void OnAfterValueChange(AudiometriaAfterValueChangeEventArgs e)
        {
            if (AfterValueChange != null)
                AfterValueChange(this, e);
        }
        #endregion

        #region "--------------- Properties --------------------"
        public string PersonId { get; set; }
        public string ServiceId { get; set; }

        public List<ServiceComponentFieldValuesList> DataSource
        {
            get
            {
                if (rbExposicionLaboralNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_RUIDO, "1");
                }

                if (rbExposicionQuimicosNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_QUIMICO, "1");
                }

                if (rbDeportesNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_DEPORTE, "1");
                }

                if (rbExposicionRuidoNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_RUIDO_EXCE, "1");
                }

                if (rbDispositivosMusicaNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_MUSICA, "1");
                }

                if (rbConsumoOtotoxicosNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OTOXICOS, "1");
                }

                if (rbManipulacionArmaNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_MANIPULACION, "1");
                }

                if (rbAntecedentesOtologicosNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OTOLOGICOS, "1");
                }

                if (rbZumbidosNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_ZUMBIDOS, "1");
                }

                if (rbSecrecionOidoNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_SECRECION, "1");
                }

                if (rbMareosNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_MAREOS, "1");
                }

                if (rbOtalgiaNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OTALGIA, "1");
                }

                if (rbDisminucionAudicionNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_DISMINUCION, "1");
                }

                if (rbEnfTractoRespNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_TRACTO, "1");
                }

                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OD_1, txtMembranaOidoDerecho.Text);
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OD_2, txtPabellonDerechoArriba.Text);
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OD_3, txtPabellonDerechoDerecha.Text);
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OD_4, txtPabellonDerechoAbajo.Text);

                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OI_1, txtMembranaOidoIzquierdo.Text);
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OI_2, txtPabellonIzquierdoArriba.Text);
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OI_3, txtPabellonIzquierdoIzquierda.Text);
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OI_4, txtPabellonIzquierdoAbajo.Text);


                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OD_DESC, txtOidoDerechoDescripcion.Text);
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OI_DESC, txtOidoIzquierdoDescripcion.Text);

                return _listOfAtencionAdulto1;
            }
            set
            {
                if (value != _listOfAtencionAdulto1)
                {
                    ClearValueControl();
                    _listOfAtencionAdulto1 = value;
                    SearchControlAndFill(value);
                }
            }
        }

        public void ClearValueControl()
        {
            _isChangueValueControl = false;
        }

        public bool IsChangeValueControl { get { return _isChangueValueControl; } }
        #endregion
        public ucOtoscopia()
        {
            InitializeComponent();
        }

        private void ucOtoscopia_Load(object sender, EventArgs e)
        {

            rbExposicionLaboralSI.Name = "N009-OTO00000001";
            rbExposicionLaboralNO.Name = "N009-OTO00000001";

            rbExposicionQuimicosSI.Name = "N009-OTO00000002";
            rbExposicionQuimicosNO.Name = "N009-OTO00000002";

            rbDeportesSI.Name = "N009-OTO00000003";
            rbDeportesNO.Name = "N009-OTO00000003";

            rbExposicionRuidoSI.Name = "N009-OTO00000004";
            rbExposicionRuidoNO.Name = "N009-OTO00000004";


            rbDispositivosMusicaSI.Name = "N009-OTO00000005";
            rbDispositivosMusicaNO.Name = "N009-OTO00000005";


            rbConsumoOtotoxicosSI.Name = "N009-OTO00000006";
            rbConsumoOtotoxicosNO.Name = "N009-OTO00000006";

            rbManipulacionArmaSI.Name = "N009-OTO00000007";
            rbManipulacionArmaNO.Name = "N009-OTO00000007";

            rbAntecedentesOtologicosSI.Name = "N009-OTO00000008";
            rbAntecedentesOtologicosNO.Name = "N009-OTO00000008";

            rbZumbidosSI.Name = "N009-OTO00000009";
            rbZumbidosNO.Name = "N009-OTO00000009";


            rbSecrecionOidoSI.Name = "N009-OTO00000010";
            rbSecrecionOidoNO.Name = "N009-OTO00000010";


            rbMareosSI.Name = "N009-OTO00000011";
            rbMareosNO.Name = "N009-OTO00000011";

            rbOtalgiaSI.Name = "N009-OTO00000012";
            rbOtalgiaNO.Name = "N009-OTO00000012";

            rbDisminucionAudicionSI.Name = "N009-OTO00000013";
            rbDisminucionAudicionNO.Name = "N009-OTO00000013";

            rbEnfTractoRespSI.Name = "N009-OTO00000014";
            rbEnfTractoRespNO.Name = "N009-OTO00000014";

            txtSntomasOtros.Name = "N009-OTO00000015";

            txtMembranaOidoDerecho.Name = "N009-OTO00000016";
            txtPabellonDerechoArriba.Name = "N009-OTO00000017";
            txtPabellonDerechoDerecha.Name = "N009-OTO00000018";
            txtPabellonDerechoAbajo.Name = "N009-OTO00000019";


            txtMembranaOidoIzquierdo.Name = "N009-OTO00000020";
            txtPabellonIzquierdoArriba.Name = "N009-OTO00000021";
            txtPabellonIzquierdoIzquierda.Name = "N009-OTO00000022";
            txtPabellonIzquierdoAbajo.Name = "N009-OTO00000023";

            txtOidoDerechoDescripcion.Name = "N009-OTO00000024";
            txtOidoIzquierdoDescripcion.Name = "N009-OTO00000025";

            SearchControlAndSetEvents(this);

        }

        private void SearchControlAndFill(List<ServiceComponentFieldValuesList> DataSource)
        {
            if (DataSource == null || DataSource.Count == 0) return;
            // Ordenar Lista Datasource
            var DataSourceOrdenado = DataSource.OrderBy(p => p.v_ComponentFieldId).ToList();

            // recorrer la lista que viene de la BD
            foreach (var item in DataSourceOrdenado)
            {
                var matchedFields = this.Controls.Find(item.v_ComponentFieldId, true);

                if (matchedFields.Length > 0)
                {
                    var field = matchedFields[0];

                    if (field is TextBox)
                    {
                        if (field.Name == item.v_ComponentFieldId)
                        {
                            ((TextBox)field).Text = item.v_Value1;
                        }
                    }

                    else if (field is RadioButton)
                    {
                        if (field.Name == item.v_ComponentFieldId)
                        {
                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_RUIDO)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbExposicionLaboralSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbExposicionLaboralNO.Checked = true;
                                }
                              
                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_QUIMICO)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbExposicionQuimicosSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbExposicionQuimicosNO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_DEPORTE)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbDeportesSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbDeportesNO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_RUIDO_EXCE)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbExposicionRuidoSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbExposicionRuidoNO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_MUSICA)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbDispositivosMusicaSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbDispositivosMusicaNO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_OTOXICOS)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbConsumoOtotoxicosSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbConsumoOtotoxicosNO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_MANIPULACION)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbManipulacionArmaSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbManipulacionArmaNO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_OTOLOGICOS)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbAntecedentesOtologicosSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbAntecedentesOtologicosNO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_ZUMBIDOS)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbZumbidosSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbZumbidosNO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_SECRECION)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSecrecionOidoSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSecrecionOidoNO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_MAREOS)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbMareosSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbMareosNO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_OTALGIA)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbOtalgiaSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbOtalgiaNO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_DISMINUCION)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbDisminucionAudicionSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbDisminucionAudicionNO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OTOSCOPIA_TRACTO)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbEnfTractoRespSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbEnfTractoRespNO.Checked = true;
                                }

                            }

                         
                        }
                    }
                }
            }
        }

        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    if (ctrl.Name.Contains("N009-OTO"))
                    {
                        ctrl.Leave += new EventHandler(lbl_Leave);
                    }
                }
                if (ctrl.HasChildren)
                    SearchControlAndSetEvents(ctrl);
            }
        }

        private void lbl_Leave(object sender, System.EventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;

            SaveValueControlForInterfacingESO(senderCtrl.Name, senderCtrl.Text.ToString());

            _isChangueValueControl = true;
        }

        private void SaveValueControlForInterfacingESO(string name, string value)
        {
            #region Capturar Valor del campo

            _listOfAtencionAdulto1.RemoveAll(p => p.v_ComponentFieldId == name);

            _UserControlValores = new ServiceComponentFieldValuesList();

            _UserControlValores.v_ComponentFieldId = name;
            _UserControlValores.v_Value1 = value;
            _UserControlValores.v_ComponentId = Constants.OTOSCOPIA_ID;

            _listOfAtencionAdulto1.Add(_UserControlValores);

            DataSource = _listOfAtencionAdulto1;

            #endregion
        }


        #region Eventos de Radio Button
        private void rbExposicionLaboralSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExposicionLaboralSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_RUIDO, "0");
            }
        }
        private void rbExposicionLaboralNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExposicionLaboralNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_RUIDO, "1");
            }
        }

        private void rbExposicionQuimicosSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExposicionQuimicosSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_QUIMICO, "0");
            }
        }
        private void rbExposicionQuimicosNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExposicionQuimicosNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_QUIMICO, "1");
            }
        }

        private void rbDeportesSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDeportesSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_DEPORTE, "0");
            }
        }
        private void rbDeportesNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDeportesNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_DEPORTE, "1");
            }
        }

        private void rbExposicionRuidoSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExposicionRuidoSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_RUIDO_EXCE, "0");
            }
        }
        private void rbExposicionRuidoNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExposicionRuidoNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_RUIDO_EXCE, "1");
            }
        }

        private void rbDispositivosMusicaSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDispositivosMusicaSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_MUSICA, "0");
            }
        }
        private void rbDispositivosMusicaNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDispositivosMusicaNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_MUSICA, "1");
            }
        }

        private void rbConsumoOtotoxicosSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbConsumoOtotoxicosSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OTOXICOS, "0");
            }
        }
        private void rbConsumoOtotoxicosNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbConsumoOtotoxicosNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OTOXICOS, "1");
            }
        }

        private void rbManipulacionArmaSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbManipulacionArmaSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_MANIPULACION, "0");
            }
        }
        private void rbManipulacionArmaNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbManipulacionArmaNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_MANIPULACION, "1");
            }
        }

        private void rbAntecedentesOtologicosSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAntecedentesOtologicosSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OTOLOGICOS, "0");
            }
        }
        private void rbAntecedentesOtologicosNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAntecedentesOtologicosNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OTOLOGICOS, "1");
            }
        }

        private void rbZumbidosSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbZumbidosSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_ZUMBIDOS, "0");
            }
        }
        private void rbZumbidosNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbZumbidosNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_ZUMBIDOS, "1");
            }
        }

        private void rbSecrecionOidoSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSecrecionOidoSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_SECRECION, "0");
            }
        }
        private void rbSecrecionOidoNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSecrecionOidoNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_SECRECION, "1");
            }
        }

        private void rbMareosSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMareosSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_MAREOS, "0");
            }
        }
        private void rbMareosNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMareosNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_MAREOS, "1");
            }
        }

        private void rbOtalgiaSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOtalgiaSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OTALGIA, "0");
            }
        }
        private void rbOtalgiaNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOtalgiaNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_OTALGIA, "1");
            }
        }

        private void rbDisminucionAudicionSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDisminucionAudicionSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_DISMINUCION, "0");
            }
        }
        private void rbDisminucionAudicionNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDisminucionAudicionNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_DISMINUCION, "1");
            }
        }

        private void rbEnfTractoRespSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnfTractoRespSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_TRACTO, "0");
            }
        }
        private void rbEnfTractoRespNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnfTractoRespNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OTOSCOPIA_TRACTO, "1");
            }
        }


        #endregion

        private void txtPabellonIzquierdoAbajo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
