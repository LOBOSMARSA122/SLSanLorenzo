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
    public partial class ucAcumetria : UserControl
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
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_CONCLUSIONES, txtConclusionesAcuametria.Text);
               
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

        public ucAcumetria()
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
                            if (item.v_ComponentFieldId == Constants.ACUMETRIA_PRUEBA_WEBER)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbWeberDerecha.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbWeberIndiferente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbWeberIzquierda.Checked = true;
                                }                               
                            }

                            if (item.v_ComponentFieldId == Constants.ACUMETRIA_OD_RINNER)
                            {
                             
                               if (item.v_Value1.ToString() == "1")
                                {
                                    rbOseaOidoDerecho.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbAereaOidoDerecho.Checked = true;
                                }
                               
                            }

                            if (item.v_ComponentFieldId == Constants.ACUMETRIA_OI_RINNER)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbOseaOidoIzquierdo.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbAereaOidoIzquierdo.Checked = true;
                                }
                              
                            }


                            if (item.v_ComponentFieldId == Constants.ACUMETRIA_WEBER)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbWeberDerecha2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbWeberIndeferente2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbrbWeberIzquierda2.Checked = true;
                                }
                             
                            }

                            if (item.v_ComponentFieldId == Constants.ACUMETRIA_OD_RINNE)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbRinneODPositivo.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbRinneODNegativo.Checked = true;
                                }
                              
                            }

                            if (item.v_ComponentFieldId == Constants.ACUMETRIA_OI_RINNE)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbRinneOIPositivo.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbRinneOINegativo.Checked = true;
                                }
                             
                            }



                        }
                    }
                }
            }
        }

        private void ucAcumetria_Load(object sender, EventArgs e)
        {
            rbWeberDerecha.Name = "N009-ACU00000001";
            rbWeberIndiferente.Name = "N009-ACU00000001";
            rbWeberIzquierda.Name = "N009-ACU00000001";

            rbOseaOidoDerecho.Name = "N009-ACU00000002";
            rbAereaOidoDerecho.Name = "N009-ACU00000002";

            rbOseaOidoIzquierdo.Name = "N009-ACU00000003";
            rbAereaOidoIzquierdo.Name = "N009-ACU00000003";

            rbWeberDerecha2.Name = "N009-ACU00000004";
            rbWeberIndeferente2.Name = "N009-ACU00000004";
            rbrbWeberIzquierda2.Name = "N009-ACU00000004";
            
            rbRinneODPositivo.Name = "N009-ACU00000005";
            rbRinneODNegativo.Name = "N009-ACU00000005";

            rbRinneOIPositivo.Name = "N009-ACU00000006";
            rbRinneOINegativo.Name = "N009-ACU00000006";

            txtConclusionesAcuametria.Name = "N009-ACU00000007";

        }

        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    if (ctrl.Name.Contains("N009-ACU"))
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
            _UserControlValores.v_ComponentId = Constants.ACUMETRIA_ID;

            _listOfAtencionAdulto1.Add(_UserControlValores);

            DataSource = _listOfAtencionAdulto1;

            #endregion
        }

        #region Eventos de Radio Button

        private void rbWeberDerecha_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWeberDerecha.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_PRUEBA_WEBER, "1");          
            }
        }
        private void rbWeberIndiferente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWeberIndiferente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_PRUEBA_WEBER, "2");
            }
        }
        private void rbWeberIzquierda_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWeberIzquierda.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_PRUEBA_WEBER, "3");
            }
        }

        private void rbOseaOidoDerecho_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOseaOidoDerecho.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_OD_RINNER, "1");
            }
        }
        private void rbAereaOidoDerecho_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAereaOidoDerecho.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_OD_RINNER, "2");
            }
        }

        private void rbOseaOidoIzquierdo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOseaOidoIzquierdo.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_OI_RINNER, "1");
            }
        }
        private void rbAereaOidoIzquierdo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAereaOidoIzquierdo.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_OI_RINNER, "2");
            }
        }

        private void rbWeberDerecha2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWeberDerecha2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_WEBER, "1");
            }
        }
        private void rbWeberIndeferente2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWeberIndeferente2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_WEBER, "2");
            }
        }
        private void rbrbWeberIzquierda2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbrbWeberIzquierda2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_WEBER, "3");
            }
        }

        private void rbRinneODPositivo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRinneODPositivo.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_OD_RINNE, "1");
            }
        }
        private void rbRinneODNegativo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRinneODNegativo.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_OD_RINNE, "2");
            }
        }
        private void rbRinneOIPositivo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRinneOIPositivo.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_OI_RINNE, "1");
            }
        }
        private void rbRinneOINegativo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRinneOINegativo.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.ACUMETRIA_OI_RINNE, "2");
            }
        }

        #endregion  
    }
}
