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
    public partial class ucEvaluacionErgonomica : UserControl
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
                if (rb1HombrosNunca.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_HOMBORS, "1");
                }

                if (rb1CuelloNunca.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLO, "1");
                }

                if (rb1EspaldaNunca.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ESPALDA, "1");
                }

                if (rb1RodillasNunca.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS, "1");
                }

                if (rb1Rodillas2Nunca.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS_2, "1");
                }




                if (rb2BrazosNunca.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_BRAZO_MUNE, "1");
                }
                if (rb2BrazosMuñecas2Nunca.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_BRAZO_MUNE_2, "1");
                }
                if (rb2ManosNunca.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_MANOS, "1");
                }
                if (rb2RodillasNunca.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS_3, "1");
                }



                if (rb3CuellosHombrosNunca.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLOS_HOMB, "1");
                }

                if (rb3CuelloTeclaNunca.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLOS_HOMB_2, "1");
                }




                if (rb4ZonaLumbar1NO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ZONA_LUMBAR, "0");
                }

                if (rb4ZonaLumbar2NO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ZONA_LUMBAR_2, "0");
                }

                if (rb5ManosNunca.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_MANOS_BRAZOS, "1");
                }

                if (rb5ManosSI.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_MANOS_BRAZOS_2, "0");
                }
                
                
               


                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CONCLUSIONES, txtConclusionesErgonomico.Text);
                
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

        public ucEvaluacionErgonomica()
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
                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_HOMBORS)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb1HombrosNunca.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb1HombrosOcasional.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rb1HombrosFrecuente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rb1HombrosMuyFrecuente.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_CUELLO)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb1CuelloNunca.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb1CuelloOcasional.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rb1CuelloFrecuente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rb1CuelloMuyFrecuente.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_ESPALDA)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb1EspaldaNunca.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb1EspaldaOcasional.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rb1EspaldaFrecuente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rb1EspaldaMuyFrecuente.Checked = true;
                                }
                            }


                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_RODILLAS)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb1RodillasNunca.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb1RodillasOcasional.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rb1RodillasFrecuente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rb1RodillasMuyFrecuente.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_RODILLAS_2)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb1Rodillas2Nunca.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb1Rodillas2Ocasional.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rb1Rodillas2Frecuente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rb1Rodillas2MuyFrecuente.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_BRAZO_MUNE)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb2BrazosNunca.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb2BrazosOcasional.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rb2BrazosFrecuente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rb2BrazosMuyFrecuente.Checked = true;
                                }
                            }


                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_BRAZO_MUNE_2)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb2BrazosMuñecas2Nunca.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb2BrazosMuñecas2Ocasional.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rb2BrazosMuñecas2Frecuente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rb2BrazosMuñecas2MuyFrecuente.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_MANOS)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb2ManosNunca.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb2ManosOcasional.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rb2ManosFrecuente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rb2ManosMuyFreuente.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_RODILLAS_3)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb2RodillasNunca.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb2RodillasOCasional.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rb2RodillasFrecuente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rb2RodillasMuyFrecuente.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_CUELLOS_HOMB)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb3CuellosHombrosNunca.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb3CuellosHombrosOcasional.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rb3CuellosHombrosFrecuente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rb3CuellosHombrosMuyFrecuente.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_CUELLOS_HOMB_2)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb3CuelloTeclaNunca.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb3CuelloTeclaOcasional.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rb3CuelloTeclaFrecuente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rb3CuelloTeclaMuyFrecuente.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_ZONA_LUMBAR)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb4ZonaLumbar1SI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb4ZonaLumbar1NO.Checked = true;
                                }
                              
                            }

                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_ZONA_LUMBAR_2)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb4ZonaLumbar2SI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb4ZonaLumbar2NO.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_MANOS_BRAZOS)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb5ManosNunca.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb5ManosOcasional.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rb5ManosFrecuente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rb5ManosMuyFreuente.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.EVA_ERGONOMICA_MANOS_BRAZOS_2)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rb5ManosSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rb5ManosNO.Checked = true;
                                }

                            }
                        }
                    }
                }
            }
        }

        private void ucEvaluacionErgonomica_Load(object sender, EventArgs e)
        {
            rb1HombrosNunca.Name = "N009-EVA00000001";
            rb1HombrosOcasional.Name = "N009-EVA00000001";
            rb1HombrosFrecuente.Name = "N009-EVA00000001";
            rb1HombrosMuyFrecuente.Name = "N009-EVA00000001";

            rb1CuelloNunca.Name = "N009-EVA00000002";
            rb1CuelloOcasional.Name = "N009-EVA00000002";
            rb1CuelloFrecuente.Name = "N009-EVA00000002";
            rb1CuelloMuyFrecuente.Name = "N009-EVA00000002";

            rb1EspaldaNunca.Name = "N009-EVA00000003";
            rb1EspaldaOcasional.Name = "N009-EVA00000003";
            rb1EspaldaFrecuente.Name = "N009-EVA00000003";
            rb1EspaldaMuyFrecuente.Name = "N009-EVA00000003";

            rb1RodillasNunca.Name = "N009-EVA00000004";
            rb1RodillasOcasional.Name = "N009-EVA00000004";
            rb1RodillasFrecuente.Name = "N009-EVA00000004";
            rb1RodillasMuyFrecuente.Name = "N009-EVA00000004";

            rb1Rodillas2Nunca.Name = "N009-EVA00000005";
            rb1Rodillas2Ocasional.Name = "N009-EVA00000005";
            rb1Rodillas2Frecuente.Name = "N009-EVA00000005";
            rb1Rodillas2MuyFrecuente.Name = "N009-EVA00000005";

            rb2BrazosNunca.Name = "N009-EVA00000006";
            rb2BrazosOcasional.Name = "N009-EVA00000006";
            rb2BrazosFrecuente.Name = "N009-EVA00000006";
            rb2BrazosMuyFrecuente.Name = "N009-EVA00000006";

            rb2BrazosMuñecas2Nunca.Name = "N009-EVA00000007";
            rb2BrazosMuñecas2Ocasional.Name = "N009-EVA00000007";
            rb2BrazosMuñecas2Frecuente.Name = "N009-EVA00000007";
            rb2BrazosMuñecas2MuyFrecuente.Name = "N009-EVA00000007";

            rb2ManosNunca.Name = "N009-EVA00000008";
            rb2ManosOcasional.Name = "N009-EVA00000008";
            rb2ManosFrecuente.Name = "N009-EVA00000008";
            rb2ManosMuyFreuente.Name = "N009-EVA00000008";

            rb2RodillasNunca.Name = "N009-EVA00000009";
            rb2RodillasOCasional.Name = "N009-EVA00000009";
            rb2RodillasFrecuente.Name = "N009-EVA00000009";
            rb2RodillasMuyFrecuente.Name = "N009-EVA00000009";

            rb3CuellosHombrosNunca.Name = "N009-EVA00000010";
            rb3CuellosHombrosOcasional.Name = "N009-EVA00000010";
            rb3CuellosHombrosFrecuente.Name = "N009-EVA00000010";
            rb3CuellosHombrosMuyFrecuente.Name = "N009-EVA00000010";

            rb3CuelloTeclaNunca.Name = "N009-EVA00000011";
            rb3CuelloTeclaOcasional.Name = "N009-EVA00000011";
            rb3CuelloTeclaFrecuente.Name = "N009-EVA00000011";
            rb3CuelloTeclaMuyFrecuente.Name = "N009-EVA00000011";

            rb4ZonaLumbar1SI.Name = "N009-EVA00000012";
            rb4ZonaLumbar1NO.Name = "N009-EVA00000012";

            rb4ZonaLumbar2SI.Name = "N009-EVA00000013";
            rb4ZonaLumbar2NO.Name = "N009-EVA00000013";

            rb5ManosNunca.Name = "N009-EVA00000014";
            rb5ManosOcasional.Name = "N009-EVA00000014";
            rb5ManosFrecuente.Name = "N009-EVA00000014";
            rb5ManosMuyFreuente.Name = "N009-EVA00000014";

            rb5ManosSI.Name = "N009-EVA00000015";
            rb5ManosNO.Name = "N009-EVA00000015";
            txtConclusionesErgonomico.Name = "N009-EVA00000016";

            SearchControlAndSetEvents(this);
        }

        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    if (ctrl.Name.Contains("N009-EVA"))
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
            _UserControlValores.v_ComponentId = Constants.EVA_ERGONOMICA_ID;

            _listOfAtencionAdulto1.Add(_UserControlValores);

            DataSource = _listOfAtencionAdulto1;

            #endregion
        }

        #region Eventos de Radio Button

        private void rb1HombrosNunca_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1HombrosNunca.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_HOMBORS, "0");          
            }
        }
        private void rb1HombrosOcasional_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1HombrosOcasional.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_HOMBORS, "1");
            }
        }
        private void rb1HombrosFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1HombrosFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_HOMBORS, "2");
            }
        }
        private void rb1HombrosMuyFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1HombrosMuyFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_HOMBORS, "3");
            }
        }

        private void rb1CuelloNunca_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1CuelloNunca.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLO, "0");
            }
        }
        private void rb1CuelloOcasional_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1CuelloOcasional.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLO, "1");
            }
        }
        private void rb1CuelloFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1CuelloFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLO, "2");
            }
        }
        private void rb1CuelloMuyFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1CuelloMuyFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLO, "3");
            }
        }

        private void rb1EspaldaNunca_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1EspaldaNunca.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ESPALDA, "0");
            }
        }
        private void rb1EspaldaOcasional_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1EspaldaOcasional.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ESPALDA, "1");
            }
        }
        private void rb1EspaldaFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1EspaldaFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ESPALDA, "2");
            }
        }
        private void rb1EspaldaMuyFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1EspaldaMuyFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ESPALDA, "3");
            }
        }

        private void rb1RodillasNunca_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1RodillasNunca.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS, "0");
            }
        }
        private void rb1RodillasOcasional_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1RodillasOcasional.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS, "1");
            }
        }
        private void rb1RodillasFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1RodillasFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS, "2");
            }
        }
        private void rb1RodillasMuyFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1RodillasMuyFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS, "3");
            }
        }

        private void rb1Rodillas2Nunca_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1Rodillas2Nunca.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS_2, "0");
            }
        }
        private void rb1Rodillas2Ocasional_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1Rodillas2Ocasional.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS_2, "1");
            }
        }
        private void rb1Rodillas2Frecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1Rodillas2Frecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS_2, "2");
            }
        }
        private void rb1Rodillas2MuyFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1Rodillas2MuyFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS_2, "3");
            }
        }

        private void rb2BrazosNunca_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2BrazosNunca.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_BRAZO_MUNE, "0");
            }
        }
        private void rb2BrazosOcasional_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2BrazosOcasional.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_BRAZO_MUNE, "1");
            }
        }
        private void rb2BrazosFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2BrazosFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_BRAZO_MUNE, "2");
            }
        }
        private void rb2BrazosMuyFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2BrazosMuyFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_BRAZO_MUNE, "3");
            }
        }

        private void rb2BrazosMuñecas2Nunca_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2BrazosMuñecas2Nunca.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_BRAZO_MUNE_2, "0");
            }
        }
        private void rb2BrazosMuñecas2Ocasional_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2BrazosMuñecas2Ocasional.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_BRAZO_MUNE_2, "1");
            }
        }
        private void rb2BrazosMuñecas2Frecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2BrazosMuñecas2Frecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_BRAZO_MUNE_2, "2");
            }
        }
        private void rb2BrazosMuñecas2MuyFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2BrazosMuñecas2MuyFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_BRAZO_MUNE_2, "3");
            }
        }

        private void rb2ManosNunca_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2ManosNunca.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_MANOS, "0");
            }
        }
        private void rb2ManosOcasional_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2ManosOcasional.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_MANOS, "1");
            }
        }
        private void rb2ManosFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2ManosFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_MANOS, "2");
            }
        }
        private void rb2ManosMuyFreuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2ManosMuyFreuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_MANOS, "3");
            }
        }

        private void rb2RodillasNunca_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2RodillasNunca.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS_3, "0");
            }
        }
        private void rb2RodillasOCasional_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2RodillasOCasional.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS_3, "1");
            }
        }
        private void rb2RodillasFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2RodillasFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS_3, "2");
            }
        }
        private void rb2RodillasMuyFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2RodillasMuyFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_RODILLAS_3, "3");
            }
        }

        private void rb3CuellosHombrosNunca_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3CuellosHombrosNunca.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLOS_HOMB, "0");
            }
        }
        private void rb3CuellosHombrosOcasional_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3CuellosHombrosOcasional.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLOS_HOMB, "1");
            }
        }
        private void rb3CuellosHombrosFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3CuellosHombrosFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLOS_HOMB, "2");
            }
        }
        private void rb3CuellosHombrosMuyFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3CuellosHombrosMuyFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLOS_HOMB, "3");
            }
        }

        private void rb3CuelloTeclaNunca_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3CuelloTeclaNunca.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLOS_HOMB_2, "0");
            }
        }
        private void rb3CuelloTeclaOcasional_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3CuelloTeclaOcasional.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLOS_HOMB_2, "1");
            }
        }
        private void rb3CuelloTeclaFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3CuelloTeclaFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLOS_HOMB_2, "2");
            }
        }
        private void rb3CuelloTeclaMuyFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3CuelloTeclaMuyFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_CUELLOS_HOMB_2, "3");
            }
        }



        private void rb4ZonaLumbar1SI_CheckedChanged(object sender, EventArgs e)
        {
            if (rb4ZonaLumbar1SI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ZONA_LUMBAR, "0");
            }
        }
        private void rb4ZonaLumbar1NO_CheckedChanged(object sender, EventArgs e)
        {
            if (rb4ZonaLumbar1NO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ZONA_LUMBAR, "1");
            }
        }

        private void rb4ZonaLumbar2SI_CheckedChanged(object sender, EventArgs e)
        {
            if (rb4ZonaLumbar2SI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ZONA_LUMBAR_2, "0");
            }
        }
        private void rb4ZonaLumbar2NO_CheckedChanged(object sender, EventArgs e)
        {
            if (rb4ZonaLumbar2NO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ZONA_LUMBAR_2, "1");
            }
        }

        private void rb5ManosNunca_CheckedChanged(object sender, EventArgs e)
        {
            if (rb5ManosNunca.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ZONA_LUMBAR_2, "0");
            }
        }
        private void rb5ManosOcasional_CheckedChanged(object sender, EventArgs e)
        {
            if (rb5ManosOcasional.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ZONA_LUMBAR_2, "1");
            }
        }
        private void rb5ManosFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb5ManosFrecuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ZONA_LUMBAR_2, "2");
            }
        }
        private void rb5ManosMuyFreuente_CheckedChanged(object sender, EventArgs e)
        {
            if (rb5ManosMuyFreuente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_ZONA_LUMBAR_2, "3");
            }
        }

        private void rb5ManosSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rb5ManosSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_MANOS_BRAZOS_2, "0");
            }
        }
        private void rb5ManosNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rb5ManosNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.EVA_ERGONOMICA_MANOS_BRAZOS_2, "1");
            }
        }

   

        #endregion  
    }
}
