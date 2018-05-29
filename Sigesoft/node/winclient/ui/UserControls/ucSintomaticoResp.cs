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
    public partial class ucSintomaticoResp : UserControl
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
                if (rbTucerculosisNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SINTOMATICO_1, "0");
                }
                if (rbTos15diasNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SINTOMATICO_2, "0");
                }
                if (rbBajaPesoNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SINTOMATICO_3, "0");
                }
                if (rbSudoracionNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SINTOMATICO_4, "0");
                }
                if (rbExpectoracionNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SINTOMATICO_5, "0");
                }
                if (rbFamiliaresNO.Checked)
                {

                    SaveValueControlForInterfacingESO(Constants.SINTOMATICO_6, "0");
                }
                if (rbSospechaNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SINTOMATICO_7, "0");
                }
                if (rbConclusionNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.SINTOMATICO_SI_NO, "0");
                }
               
                
               
                
              
             
              

                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_BK_1, txtResultadosBK01.Text);

                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_BK_2, txtResultadosBK02.Text);

                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_RX, txtResultadosRX.Text);

                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_OBS, txtSntomasOtros.Text);
               
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

        public ucSintomaticoResp()
        {
            InitializeComponent();
        }

        private void ucSintomaticoResp_Load(object sender, EventArgs e)
        {
            rbTucerculosisSI.Name = "N009-RES00000001";
            rbTucerculosisNO.Name = "N009-RES00000001";

            rbTos15diasSI.Name = "N009-RES00000002";
            rbTos15diasNO.Name = "N009-RES00000002";

            rbBajaPesoSI.Name = "N009-RES00000003";
            rbBajaPesoNO.Name = "N009-RES00000003";

            rbSudoracionSI.Name = "N009-RES00000004";
            rbSudoracionNO.Name = "N009-RES00000004";

            rbExpectoracionSI.Name = "N009-RES00000005";
            rbExpectoracionNO.Name = "N009-RES00000005";


            rbFamiliaresSI.Name = "N009-RES00000006";
            rbFamiliaresNO.Name = "N009-RES00000006";

            rbSospechaSI.Name = "N009-RES00000007";
            rbSospechaNO.Name = "N009-RES00000007";


            txtSntomasOtros.Name = "N009-RES00000008";

            rbConclusionSI.Name = "N009-RES00000009";
            rbConclusionNO.Name = "N009-RES00000009";


            txtResultadosBK01.Name = "N009-RES00000010";
            txtResultadosBK02.Name = "N009-RES00000011";
            txtResultadosRX.Name = "N009-RES00000012";

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
                            if (item.v_ComponentFieldId == Constants.SINTOMATICO_1)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbTucerculosisNO.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbTucerculosisSI.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.SINTOMATICO_2)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbTos15diasNO.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbTos15diasSI.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.SINTOMATICO_3)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbBajaPesoNO.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbBajaPesoSI.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.SINTOMATICO_4)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSudoracionNO.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSudoracionSI.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.SINTOMATICO_5)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbExpectoracionNO.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbExpectoracionSI.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.SINTOMATICO_6)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbFamiliaresNO.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbFamiliaresSI.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.SINTOMATICO_7)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSospechaNO.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSospechaSI.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.SINTOMATICO_SI_NO)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbConclusionNO.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbConclusionSI.Checked = true;
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
                    if (ctrl.Name.Contains("N009-RES"))
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
            _UserControlValores.v_ComponentId = Constants.SINTOMATICO_ID;

            _listOfAtencionAdulto1.Add(_UserControlValores);

            DataSource = _listOfAtencionAdulto1;

            #endregion
        }


        #region Eventos de Radio Button
        private void rbTucerculosisSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTucerculosisSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_1, "1");
            }
        }
        private void rbTucerculosisNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTucerculosisNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_1, "0");
            }
        }

        private void rbTos15diasSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTos15diasSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_2, "1");
            }
        }
        private void rbTos15diasNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTos15diasNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_2, "0");
            }
        }

        private void rbBajaPesoSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBajaPesoSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_3, "1");
            }
        }
        private void rbBajaPesoNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBajaPesoNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_3, "0");
            }
        }

        private void rbSudoracionSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSudoracionSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_4, "1");
            }
        }
        private void rbSudoracionNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSudoracionNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_4, "0");
            }
        }

        private void rbExpectoracionSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExpectoracionSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_5, "1");
            }
        }
        private void rbExpectoracionNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExpectoracionNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_5, "0");
            }
        }

        private void rbFamiliaresSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFamiliaresSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_6, "1");
            }
        }
        private void rbFamiliaresNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFamiliaresNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_6, "0");
            }
        }

        private void rbSospechaSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSospechaSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_7, "1");
            }
        }
        private void rbSospechaNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSospechaNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_7, "0");
            }
        }

        #endregion

        private void rbConclusionSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbConclusionSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_SI_NO, "1");
            }
        }

        private void rbConclusionNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbConclusionNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.SINTOMATICO_SI_NO, "0");
            }
        }

       


    }
}
