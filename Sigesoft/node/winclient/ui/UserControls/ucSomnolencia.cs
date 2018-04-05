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
    public partial class ucSomnolencia : UserControl
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
                if (rbSentadoLeyendoCero.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_1_SENTADO_ID, "0");
                }
                if (rbMirandoTVCero.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_2_MIRANDO_TV_ID, "0");
                }
                if (rbSentadoInactivoCero.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_3_SENTADO_INACTIVO_ID, "0");
                }
                if (rbComoPasajeroCero.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_4_PASAJERO_ID, "0");
                }
                if (rbAcostadoDescansandoCero.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_5_ACOSTADO_DESC_ID, "0");
                }
                if (rbSentadoConversandoCero.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_6_ACOSTADO_CONVER_ID, "0");
                }
                if (rbSentadoTranquiloCero.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_7_SENTADO_TRANQUILO_ID, "0");
                }
                if (rbEnUnCarroCero.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_8_CARRO_TRACON_ID, "0");
                }
             
               
              
               
               
               
                
               

                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_TOTAL_ID, txtTotal.Text);
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

        public ucSomnolencia()
        {
            InitializeComponent();
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
                            if (item.v_ComponentFieldId == Constants.SOMNOLENCIA_1_SENTADO_ID)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSentadoLeyendoCero.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSentadoLeyendoUno.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbSentadoLeyendoDos.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbSentadoLeyendoTres.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.SOMNOLENCIA_2_MIRANDO_TV_ID)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbMirandoTVCero.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbMirandoTVUno.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbMirandoTVDos.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbMirandoTVTres.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.SOMNOLENCIA_3_SENTADO_INACTIVO_ID)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSentadoInactivoCero.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSentadoInactivoUno.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbSentadoInactivoDos.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbSentadoInactivoTres.Checked = true;
                                }
                            }


                            if (item.v_ComponentFieldId == Constants.SOMNOLENCIA_4_PASAJERO_ID)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbComoPasajeroCero.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbComoPasajeroUno.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbComoPasajeroDos.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbComoPasajeroTres.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.SOMNOLENCIA_5_ACOSTADO_DESC_ID)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbAcostadoDescansandoCero.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbAcostadoDescansandoUno.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbAcostadoDescansandoDos.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbAcostadoDescansandoTres.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.SOMNOLENCIA_6_ACOSTADO_CONVER_ID)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSentadoConversandoCero.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSentadoConversandoUno.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbSentadoConversandoDos.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbSentadoConversandoTres.Checked = true;
                                }
                            }


                            if (item.v_ComponentFieldId == Constants.SOMNOLENCIA_7_SENTADO_TRANQUILO_ID)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSentadoTranquiloCero.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSentadoTranquiloUno.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbSentadoTranquiloDos.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbSentadoTranquiloTres.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.SOMNOLENCIA_8_CARRO_TRACON_ID)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbEnUnCarroCero.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbEnUnCarroUno.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbEnUnCarroDos.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbEnUnCarroTres.Checked = true;
                                }
                            }
         
                        }
                    }
                }
            }
        }

        private void ucSomnolencia_Load(object sender, EventArgs e)
        {

            rbSentadoLeyendoCero.Name = "N009-SOM00000001";
            rbSentadoLeyendoUno.Name = "N009-SOM00000001";
            rbSentadoLeyendoDos.Name = "N009-SOM00000001";
            rbSentadoLeyendoTres.Name = "N009-SOM00000001";

            rbMirandoTVCero.Name = "N009-SOM00000002";
            rbMirandoTVUno.Name = "N009-SOM00000002";
            rbMirandoTVDos.Name = "N009-SOM00000002";
            rbMirandoTVTres.Name = "N009-SOM00000002";
            
            rbSentadoInactivoCero.Name = "N009-SOM00000003";
            rbSentadoInactivoUno.Name = "N009-SOM00000003";
            rbSentadoInactivoDos.Name = "N009-SOM00000003";
            rbSentadoInactivoTres.Name = "N009-SOM00000003";

            rbComoPasajeroCero.Name = "N009-SOM00000004";
            rbComoPasajeroUno.Name = "N009-SOM00000004";
            rbComoPasajeroDos.Name = "N009-SOM00000004";
            rbComoPasajeroTres.Name = "N009-SOM00000004";

            rbAcostadoDescansandoCero.Name = "N009-SOM00000005";
            rbAcostadoDescansandoUno.Name = "N009-SOM00000005";
            rbAcostadoDescansandoDos.Name = "N009-SOM00000005";
            rbAcostadoDescansandoTres.Name = "N009-SOM00000005";

            rbSentadoConversandoCero.Name = "N009-SOM00000006";
            rbSentadoConversandoUno.Name = "N009-SOM00000006";
            rbSentadoConversandoDos.Name = "N009-SOM00000006";
            rbSentadoConversandoTres.Name = "N009-SOM00000006";

            rbSentadoTranquiloCero.Name = "N009-SOM00000007";
            rbSentadoTranquiloUno.Name = "N009-SOM00000007";
            rbSentadoTranquiloDos.Name = "N009-SOM00000007";
            rbSentadoTranquiloTres.Name = "N009-SOM00000007";

            rbEnUnCarroCero.Name = "N009-SOM00000008";
            rbEnUnCarroUno.Name = "N009-SOM00000008";
            rbEnUnCarroDos.Name = "N009-SOM00000008";
            rbEnUnCarroTres.Name = "N009-SOM00000008";

            txtSentadoLeyendoPtje.Name = "N009-SOM00000009";
            txtMirandoTVPtje.Name = "N009-SOM00000010";
            txtSentadoInactivoPtje.Name = "N009-SOM00000011";
            txtComoPasajeroPtje.Name = "N009-SOM00000012";
            txtAcostadoDescansandoPtje.Name = "N009-SOM00000013";
            txtSentadoConversandoPtje.Name = "N009-SOM00000014";
            txtSentadoTranquiloPtje.Name = "N009-SOM00000015";
            txtEnUnCarroPtje.Name = "N009-SOM00000016";
            txtTotal.Name = "N009-SOM00000017";



            SearchControlAndSetEvents(this);
        }

        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    if (ctrl.Name.Contains("N009-SOM"))
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
            _UserControlValores.v_ComponentId = Constants.SOMNOLENCIA_ID;

            _listOfAtencionAdulto1.Add(_UserControlValores);

            DataSource = _listOfAtencionAdulto1;

            #endregion
        }

        #region Eventos de Radio Button  

        private void rbSentadoLeyendoCero_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoLeyendoCero.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_1_SENTADO_ID, "0");
                txtSentadoLeyendoPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbSentadoLeyendoUno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoLeyendoUno.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_1_SENTADO_ID, "1");
                txtSentadoLeyendoPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbSentadoLeyendoDos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoLeyendoDos.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_1_SENTADO_ID, "2");
                txtSentadoLeyendoPtje.Text = "2";
                calcularTotal();
            }
        }      
        private void rbSentadoLeyendoTres_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoLeyendoTres.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_1_SENTADO_ID, "3");
                txtSentadoLeyendoPtje.Text = "3";
                calcularTotal();
            }
        }

        private void rbMirandoTVCero_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMirandoTVCero.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_2_MIRANDO_TV_ID, "0");
                txtMirandoTVPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbMirandoTVUno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMirandoTVUno.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_2_MIRANDO_TV_ID, "1");
                txtMirandoTVPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbMirandoTVDos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMirandoTVDos.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_2_MIRANDO_TV_ID, "2");
                txtMirandoTVPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbMirandoTVTres_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMirandoTVTres.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_2_MIRANDO_TV_ID, "3");
                txtMirandoTVPtje.Text = "3";
                calcularTotal();
            }
        }

        private void rbSentadoInactivoCero_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoInactivoCero.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_3_SENTADO_INACTIVO_ID, "0");
                txtSentadoInactivoPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbSentadoInactivoUno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoInactivoUno.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_3_SENTADO_INACTIVO_ID, "1");
                txtSentadoInactivoPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbSentadoInactivoDos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoInactivoDos.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_3_SENTADO_INACTIVO_ID, "2");
                txtSentadoInactivoPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbSentadoInactivoTres_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoInactivoTres.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_3_SENTADO_INACTIVO_ID, "3");
                txtSentadoInactivoPtje.Text = "3";
                calcularTotal();
            }
        }

        private void rbComoPasajeroCero_CheckedChanged(object sender, EventArgs e)
        {
            if (rbComoPasajeroCero.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_4_PASAJERO_ID, "0");
                txtComoPasajeroPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbComoPasajeroUno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbComoPasajeroUno.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_4_PASAJERO_ID, "1");
                txtComoPasajeroPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbComoPasajeroDos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbComoPasajeroDos.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_4_PASAJERO_ID, "2");
                txtComoPasajeroPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbComoPasajeroTres_CheckedChanged(object sender, EventArgs e)
        {
            if (rbComoPasajeroTres.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_4_PASAJERO_ID, "3");
                txtComoPasajeroPtje.Text = "3";
                calcularTotal();
            }
        }

        private void rbAcostadoDescansandoCero_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAcostadoDescansandoCero.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_5_ACOSTADO_DESC_ID, "0");
                txtAcostadoDescansandoPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbAcostadoDescansandoUno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAcostadoDescansandoUno.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_5_ACOSTADO_DESC_ID, "1");
                txtAcostadoDescansandoPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbAcostadoDescansandoDos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAcostadoDescansandoDos.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_5_ACOSTADO_DESC_ID, "2");
                txtAcostadoDescansandoPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbAcostadoDescansandoTres_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAcostadoDescansandoTres.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_5_ACOSTADO_DESC_ID, "3");
                txtAcostadoDescansandoPtje.Text = "3";
                calcularTotal();
            }
        }

        private void rbSentadoConversandoCero_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoConversandoCero.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_6_ACOSTADO_CONVER_ID, "0");
                txtSentadoConversandoPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbSentadoConversandoUno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoConversandoUno.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_6_ACOSTADO_CONVER_ID, "1");
                txtSentadoConversandoPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbSentadoConversandoDos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoConversandoDos.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_6_ACOSTADO_CONVER_ID, "2");
                txtSentadoConversandoPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbSentadoConversandoTres_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoConversandoTres.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_6_ACOSTADO_CONVER_ID, "3");
                txtSentadoConversandoPtje.Text = "3";
                calcularTotal();
            }
        }

        private void rbSentadoTranquiloCero_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoTranquiloCero.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_7_SENTADO_TRANQUILO_ID, "0");
                txtSentadoTranquiloPtje.Text = "0";
                calcularTotal();
            }
        }
       
        private void rbSentadoTranquiloUno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoTranquiloUno.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_7_SENTADO_TRANQUILO_ID, "1");
                txtSentadoTranquiloPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbSentadoTranquiloDos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoTranquiloDos.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_7_SENTADO_TRANQUILO_ID, "2");
                txtSentadoTranquiloPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbSentadoTranquiloTres_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSentadoTranquiloTres.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_7_SENTADO_TRANQUILO_ID, "3");
                txtSentadoTranquiloPtje.Text = "3";
                calcularTotal();
            }
        }

        private void rbEnUnCarroCero_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnUnCarroCero.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_8_CARRO_TRACON_ID, "0");
                txtEnUnCarroPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbEnUnCarroUno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnUnCarroUno.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_8_CARRO_TRACON_ID, "1");
                txtEnUnCarroPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbEnUnCarroDos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnUnCarroDos.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_8_CARRO_TRACON_ID, "2");
                txtEnUnCarroPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbEnUnCarroTres_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnUnCarroTres.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SOMNOLENCIA_8_CARRO_TRACON_ID, "3");
                txtEnUnCarroPtje.Text = "3";
                calcularTotal();
            }
        }


        private void calcularTotal()
        {
            int p1 = txtSentadoLeyendoPtje.Text==""?0: int.Parse(txtSentadoLeyendoPtje.Text.ToString());
            int p2 = txtMirandoTVPtje.Text == "" ? 0 : int.Parse(txtMirandoTVPtje.Text.ToString());
            int p3 = txtSentadoInactivoPtje.Text == "" ? 0 : int.Parse(txtSentadoInactivoPtje.Text.ToString());
            int p4 = txtComoPasajeroPtje.Text == "" ? 0 : int.Parse(txtComoPasajeroPtje.Text.ToString());
            int p5 = txtAcostadoDescansandoPtje.Text == "" ? 0 : int.Parse(txtAcostadoDescansandoPtje.Text.ToString());
            int p6 = txtSentadoConversandoPtje.Text == "" ? 0 : int.Parse(txtSentadoConversandoPtje.Text.ToString());
            int p7 = txtSentadoTranquiloPtje.Text == "" ? 0 : int.Parse(txtSentadoTranquiloPtje.Text.ToString());
            int p8 = txtEnUnCarroPtje.Text == "" ? 0 : int.Parse(txtEnUnCarroPtje.Text.ToString());
            txtTotal.Text = (p1 + p2 + p3 + p4 + p5 + p6 + p7 + p8).ToString();

        }

        #endregion
    }
}
