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
    public partial class ucOjoSeco : UserControl
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
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_TOTAL, txtTotal.Text);

                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ENROJECIMIENTO, txtEnrojecimientoPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_BORDE, txtBordeParpadosPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ESCAMAS, txtEscamasCostraPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_OJOS, txtOjosPegadosPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SECRE, txtSecrecionesPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SEQUEDAD, txtSequedadOjoPtje.Text);

                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ARENILLA, txtSensacionArenillaPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_EXTRANO, txtSensacionCuerpoPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ARDOR, txtArdorQuemaPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_PICOR, txtPicorPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_MALESTAR, txtMalestarOjoPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_DOLOR, txtDolorAgudoPtje.Text);

                SaveValueControlForInterfacingESO(Constants.OJO_SECO_LAGRIMEO, txtLagrimeoPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_LLOROSOS, txtOjosLlorososPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SENSIBILIDAD, txtSensabilidadLuzPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_VISION, txtVisionBorrosaPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_CANSANCION, txtCansancioOjosPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_PESADEZ, txtSensacionParpadezPtje.Text);
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_TOTAL, txtTotal.Text);
                
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

        public ucOjoSeco()
        {
            InitializeComponent();
        }

        private void ucOjoSeco_Load(object sender, EventArgs e)
        {
            rbEnrojecimiento0.Name = "N009-OJS00000001";
            rbEnrojecimiento1.Name = "N009-OJS00000001";
            rbEnrojecimiento2.Name = "N009-OJS00000001";
            rbEnrojecimiento3.Name = "N009-OJS00000001";
            rbEnrojecimiento4.Name = "N009-OJS00000001";
            txtEnrojecimientoPtje.Name = "N009-OJS00000006";

            rbBordeParpados0.Name = "N009-OJS00000007";
            rbBordeParpados1.Name = "N009-OJS00000007";
            rbBordeParpados2.Name = "N009-OJS00000007";
            rbBordeParpados3.Name = "N009-OJS00000007";
            rbBordeParpados4.Name = "N009-OJS00000007";
            txtBordeParpadosPtje.Name = "N009-OJS00000012";

            rbEscamasCostra0.Name = "N009-OJS00000013";
            rbEscamasCostra1.Name = "N009-OJS00000013";
            rbEscamasCostra2.Name = "N009-OJS00000013";
            rbEscamasCostra3.Name = "N009-OJS00000013";
            rbEscamasCostra4.Name = "N009-OJS00000013";
            txtEscamasCostraPtje.Name = "N009-OJS00000018";

            rbOjosPegados0.Name = "N009-OJS00000019";
            rbOjosPegados1.Name = "N009-OJS00000019";
            rbOjosPegados2.Name = "N009-OJS00000019";
            rbOjosPegados3.Name = "N009-OJS00000019";
            rbOjosPegados4.Name = "N009-OJS00000019";
            txtOjosPegadosPtje.Name = "N009-OJS00000024";

            rbSecreciones0.Name = "N009-OJS00000025";
            rbSecreciones1.Name = "N009-OJS00000025";
            rbSecreciones2.Name = "N009-OJS00000025";
            rbSecreciones3.Name = "N009-OJS00000025";
            rbSecreciones4.Name = "N009-OJS00000025";
            txtSecrecionesPtje.Name = "N009-OJS00000030";

            rbSequedadOjo0.Name = "N009-OJS00000031";
            rbSequedadOjo1.Name = "N009-OJS00000031";
            rbSequedadOjo2.Name = "N009-OJS00000031";
            rbSequedadOjo3.Name = "N009-OJS00000031";
            rbSequedadOjo4.Name = "N009-OJS00000031";
            txtSequedadOjoPtje.Name = "N009-OJS00000036";

            rbSensacionArenilla0.Name = "N009-OJS00000037";
            rbSensacionArenilla1.Name = "N009-OJS00000037";
            rbSensacionArenilla2.Name = "N009-OJS00000037";
            rbSensacionArenilla3.Name = "N009-OJS00000037";
            rbSensacionArenilla4.Name = "N009-OJS00000037";
            txtSensacionArenillaPtje.Name = "N009-OJS00000042";

            rbSensacionCuerpo0.Name = "N009-OJS00000043";
            rbSensacionCuerpo1.Name = "N009-OJS00000043";
            rbSensacionCuerpo2.Name = "N009-OJS00000043";
            rbSensacionCuerpo3.Name = "N009-OJS00000043";
            rbSensacionCuerpo4.Name = "N009-OJS00000043";
            txtSensacionCuerpoPtje.Name = "N009-OJS00000048";

            rbArdorQuema0.Name = "N009-OJS00000049";
            rbArdorQuema1.Name = "N009-OJS00000049";
            rbArdorQuema2.Name = "N009-OJS00000049";
            rbArdorQuema3.Name = "N009-OJS00000049";
            rbArdorQuema4.Name = "N009-OJS00000049";
            txtArdorQuemaPtje.Name = "N009-OJS00000054";

            rbPicor0.Name = "N009-OJS00000055";
            rbPicor1.Name = "N009-OJS00000055";
            rbPicor2.Name = "N009-OJS00000055";
            rbPicor3.Name = "N009-OJS00000055";
            rbPicor4.Name = "N009-OJS00000055";
            txtPicorPtje.Name = "N009-OJS00000060";

            rbMalestarOjo0.Name = "N009-OJS00000061";
            rbMalestarOjo1.Name = "N009-OJS00000061";
            rbMalestarOjo2.Name = "N009-OJS00000061";
            rbMalestarOjo3.Name = "N009-OJS00000061";
            rbMalestarOjo4.Name = "N009-OJS00000061";
            txtMalestarOjoPtje.Name = "N009-OJS00000066";

            rbDolorAgudo0.Name = "N009-OJS00000067";
            rbDolorAgudo1.Name = "N009-OJS00000067";
            rbDolorAgudo2.Name = "N009-OJS00000067";
            rbDolorAgudo3.Name = "N009-OJS00000067";
            rbDolorAgudo4.Name = "N009-OJS00000067";
            txtDolorAgudoPtje.Name = "N009-OJS00000072";

            rbLagrimeo0.Name = "N009-OJS00000073";
            rbLagrimeo1.Name = "N009-OJS00000073";
            rbLagrimeo2.Name = "N009-OJS00000073";
            rbLagrimeo3.Name = "N009-OJS00000073";
            rbLagrimeo4.Name = "N009-OJS00000073";
            txtLagrimeoPtje.Name = "N009-OJS00000078";

            rbOjosLlorosos0.Name = "N009-OJS00000079";
            rbOjosLlorosos1.Name = "N009-OJS00000079";
            rbOjosLlorosos2.Name = "N009-OJS00000079";
            rbOjosLlorosos3.Name = "N009-OJS00000079";
            rbOjosLlorosos4.Name = "N009-OJS00000079";
            txtOjosLlorososPtje.Name = "N009-OJS00000084";

            rbSensabilidadLuz0.Name = "N009-OJS00000085";
            rbSensabilidadLuz1.Name = "N009-OJS00000085";
            rbSensabilidadLuz2.Name = "N009-OJS00000085";
            rbSensabilidadLuz3.Name = "N009-OJS00000085";
            rbSensabilidadLuz4.Name = "N009-OJS00000085";
            txtSensabilidadLuzPtje.Name = "N009-OJS00000090";

            rbVisionBorrosa0.Name = "N009-OJS00000091";
            rbVisionBorrosa1.Name = "N009-OJS00000091";
            rbVisionBorrosa2.Name = "N009-OJS00000091";
            rbVisionBorrosa3.Name = "N009-OJS00000091";
            rbVisionBorrosa4.Name = "N009-OJS00000091";
            txtVisionBorrosaPtje.Name = "N009-OJS00000096";

            rbCansancioOjos0.Name = "N009-OJS00000097";
            rbCansancioOjos1.Name = "N009-OJS00000097";
            rbCansancioOjos2.Name = "N009-OJS00000097";
            rbCansancioOjos3.Name = "N009-OJS00000097";
            rbCansancioOjos4.Name = "N009-OJS00000097";
            txtCansancioOjosPtje.Name = "N009-OJS00000102";

            rbSensacionParpadez0.Name = "N009-OJS00000103";
            rbSensacionParpadez1.Name = "N009-OJS00000103";
            rbSensacionParpadez2.Name = "N009-OJS00000103";
            rbSensacionParpadez3.Name = "N009-OJS00000103";
            rbSensacionParpadez4.Name = "N009-OJS00000103";
            txtSensacionParpadezPtje.Name = "N009-OJS00000108";

            txtTotal.Name = "N009-OJS00000109";

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
                            if (item.v_ComponentFieldId == Constants.OJO_SECO_ENROJECIMIENTO)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbEnrojecimiento0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbEnrojecimiento1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbEnrojecimiento2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbEnrojecimiento3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbEnrojecimiento4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_BORDE)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbBordeParpados0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbBordeParpados1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbBordeParpados2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbBordeParpados3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbBordeParpados4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_ESCAMAS)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbEscamasCostra0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbEscamasCostra1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbEscamasCostra2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbEscamasCostra3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbEscamasCostra4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_OJOS)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbOjosPegados0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbOjosPegados1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbOjosPegados2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbOjosPegados3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbOjosPegados4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_SECRE)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSecreciones0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSecreciones1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbSecreciones2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbSecreciones3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbSecreciones4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_SEQUEDAD)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSequedadOjo0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSequedadOjo1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbSequedadOjo2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbSequedadOjo3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbSequedadOjo4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_ARENILLA)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSensacionArenilla0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSensacionArenilla1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbSensacionArenilla2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbSensacionArenilla3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbSensacionArenilla4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_EXTRANO)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSensacionCuerpo0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSensacionCuerpo1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbSensacionCuerpo2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbSensacionCuerpo3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbSensacionCuerpo4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_ARDOR)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbArdorQuema0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbArdorQuema1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbArdorQuema2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbArdorQuema3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbArdorQuema4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_PICOR)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbPicor0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbPicor1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbPicor2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbPicor3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbPicor4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_MALESTAR)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbMalestarOjo0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbMalestarOjo1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbMalestarOjo2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbMalestarOjo3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbMalestarOjo4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_DOLOR)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbDolorAgudo0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbDolorAgudo1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbDolorAgudo2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbDolorAgudo3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbDolorAgudo4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_LAGRIMEO)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbLagrimeo0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbLagrimeo1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbLagrimeo2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbLagrimeo3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbLagrimeo4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_LLOROSOS)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbOjosLlorosos0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbOjosLlorosos1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbOjosLlorosos2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbOjosLlorosos3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbOjosLlorosos4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_SENSIBILIDAD)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSensabilidadLuz0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSensabilidadLuz1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbSensabilidadLuz2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbSensabilidadLuz3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbSensabilidadLuz4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_VISION)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbVisionBorrosa0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbVisionBorrosa1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbVisionBorrosa2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbVisionBorrosa3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbVisionBorrosa4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_CANSANCION)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbCansancioOjos0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbCansancioOjos1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbCansancioOjos2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbCansancioOjos3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbCansancioOjos4.Checked = true;
                                }

                            }

                            if (item.v_ComponentFieldId == Constants.OJO_SECO_PESADEZ)
                            {
                                if (item.v_Value1.ToString() == "0")
                                {
                                    rbSensacionParpadez0.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "1")
                                {
                                    rbSensacionParpadez1.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbSensacionParpadez2.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbSensacionParpadez3.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbSensacionParpadez4.Checked = true;
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
                    if (ctrl.Name.Contains("N009-OJS"))
                    {
                        ctrl.Leave += new EventHandler(lbl_Leave);
                    }
                }

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
            _UserControlValores.v_ComponentId = Constants.OJO_SECO_ID;

            _listOfAtencionAdulto1.Add(_UserControlValores);

            DataSource = _listOfAtencionAdulto1;

            #endregion
        }

        private void calcularTotal()
        {
            int p1 = txtEnrojecimientoPtje.Text == "" ? 0 : int.Parse(txtEnrojecimientoPtje.Text.ToString());
            int p2 = txtBordeParpadosPtje.Text == "" ? 0 : int.Parse(txtBordeParpadosPtje.Text.ToString());
            int p3 = txtEscamasCostraPtje.Text == "" ? 0 : int.Parse(txtEscamasCostraPtje.Text.ToString());
            int p4 = txtOjosPegadosPtje.Text == "" ? 0 : int.Parse(txtOjosPegadosPtje.Text.ToString());
            int p5 = txtSecrecionesPtje.Text == "" ? 0 : int.Parse(txtSecrecionesPtje.Text.ToString());
            int p6 = txtSequedadOjoPtje.Text == "" ? 0 : int.Parse(txtSequedadOjoPtje.Text.ToString());
            int p7 = txtSensacionArenillaPtje.Text == "" ? 0 : int.Parse(txtSensacionArenillaPtje.Text.ToString());
            int p8 = txtSensacionCuerpoPtje.Text == "" ? 0 : int.Parse(txtSensacionCuerpoPtje.Text.ToString());
            int p9 = txtArdorQuemaPtje.Text == "" ? 0 : int.Parse(txtArdorQuemaPtje.Text.ToString());
            int p10 = txtPicorPtje.Text == "" ? 0 : int.Parse(txtPicorPtje.Text.ToString());
            int p11 = txtMalestarOjoPtje.Text == "" ? 0 : int.Parse(txtMalestarOjoPtje.Text.ToString());
            int p12 = txtDolorAgudoPtje.Text == "" ? 0 : int.Parse(txtDolorAgudoPtje.Text.ToString());
            int p13 = txtLagrimeoPtje.Text == "" ? 0 : int.Parse(txtLagrimeoPtje.Text.ToString());
            int p14 = txtOjosLlorososPtje.Text == "" ? 0 : int.Parse(txtOjosLlorososPtje.Text.ToString());
            int p15 = txtSensabilidadLuzPtje.Text == "" ? 0 : int.Parse(txtSensabilidadLuzPtje.Text.ToString());
            int p16 = txtVisionBorrosaPtje.Text == "" ? 0 : int.Parse(txtVisionBorrosaPtje.Text.ToString());
            int p17 = txtCansancioOjosPtje.Text == "" ? 0 : int.Parse(txtCansancioOjosPtje.Text.ToString());
            int p18 = txtSensacionParpadezPtje.Text == "" ? 0 : int.Parse(txtSensacionParpadezPtje.Text.ToString());
            txtTotal.Text = (p1 + p2 + p3 + p4 + p5 + p6 + p7 + p8 + p9 + p10 + p11 + p12 + p13 + p14 + p15 + p16 + p17 + p18).ToString();

        }

        #region Eventos de Radio Button


        private void rbEnrojecimiento0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnrojecimiento0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ENROJECIMIENTO, "0");
                txtEnrojecimientoPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbEnrojecimiento1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnrojecimiento1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ENROJECIMIENTO, "1");
                txtEnrojecimientoPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbEnrojecimiento2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnrojecimiento2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ENROJECIMIENTO, "2");
                txtEnrojecimientoPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbEnrojecimiento3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnrojecimiento3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ENROJECIMIENTO, "3");
                txtEnrojecimientoPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbEnrojecimiento4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnrojecimiento4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ENROJECIMIENTO, "4");
                txtEnrojecimientoPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbBordeParpados0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBordeParpados0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_BORDE, "0");
                txtBordeParpadosPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbBordeParpados1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBordeParpados1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_BORDE, "1");
                txtBordeParpadosPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbBordeParpados2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBordeParpados2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_BORDE, "2");
                txtBordeParpadosPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbBordeParpados3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBordeParpados3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_BORDE, "3");
                txtBordeParpadosPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbBordeParpados4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBordeParpados4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_BORDE, "4");
                txtBordeParpadosPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbEscamasCostra0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEscamasCostra0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ESCAMAS, "0");
                txtEscamasCostraPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbEscamasCostra1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEscamasCostra1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ESCAMAS, "1");
                txtEscamasCostraPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbEscamasCostra2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEscamasCostra2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ESCAMAS, "2");
                txtEscamasCostraPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbEscamasCostra3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEscamasCostra3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ESCAMAS, "3");
                txtEscamasCostraPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbEscamasCostra4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEscamasCostra4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ESCAMAS, "4");
                txtEscamasCostraPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbOjosPegados0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOjosPegados0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_OJOS, "0");
                txtOjosPegadosPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbOjosPegados1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOjosPegados1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_OJOS, "1");
                txtOjosPegadosPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbOjosPegados2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOjosPegados2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_OJOS, "2");
                txtOjosPegadosPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbOjosPegados3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOjosPegados3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_OJOS, "3");
                txtOjosPegadosPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbOjosPegados4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOjosPegados4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_OJOS, "4");
                txtOjosPegadosPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbSecreciones0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSecreciones0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SECRE, "0");
                txtSecrecionesPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbSecreciones1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSecreciones1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SECRE, "1");
                txtSecrecionesPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbSecreciones2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSecreciones2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SECRE, "2");
                txtSecrecionesPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbSecreciones3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSecreciones3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SECRE, "3");
                txtSecrecionesPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbSecreciones4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSecreciones4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SECRE, "4");
                txtSecrecionesPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbSequedadOjo0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSequedadOjo0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SEQUEDAD, "0");
                txtSequedadOjoPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbSequedadOjo1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSequedadOjo1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SEQUEDAD, "1");
                txtSequedadOjoPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbSequedadOjo2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSequedadOjo2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SEQUEDAD, "2");
                txtSequedadOjoPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbSequedadOjo3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSequedadOjo3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SEQUEDAD, "3");
                txtSequedadOjoPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbSequedadOjo4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSequedadOjo4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SEQUEDAD, "4");
                txtSequedadOjoPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbSensacionArenilla0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionArenilla0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ARENILLA, "0");
                txtSensacionArenillaPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbSensacionArenilla1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionArenilla1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ARENILLA, "1");
                txtSensacionArenillaPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbSensacionArenilla2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionArenilla2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ARENILLA, "2");
                txtSensacionArenillaPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbSensacionArenilla3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionArenilla3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ARENILLA, "3");
                txtSensacionArenillaPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbSensacionArenilla4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionArenilla4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ARENILLA, "4");
                txtSensacionArenillaPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbSensacionCuerpo0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionCuerpo0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_EXTRANO, "0");
                txtSensacionCuerpoPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbSensacionCuerpo1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionCuerpo1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_EXTRANO, "1");
                txtSensacionCuerpoPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbSensacionCuerpo2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionCuerpo2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_EXTRANO, "2");
                txtSensacionCuerpoPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbSensacionCuerpo3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionCuerpo3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_EXTRANO, "3");
                txtSensacionCuerpoPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbSensacionCuerpo4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionCuerpo4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_EXTRANO, "4");
                txtSensacionCuerpoPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbArdorQuema0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbArdorQuema0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ARDOR, "0");
                txtArdorQuemaPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbArdorQuema1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbArdorQuema1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ARDOR, "1");
                txtArdorQuemaPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbArdorQuema2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbArdorQuema2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ARDOR, "2");
                txtArdorQuemaPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbArdorQuema3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbArdorQuema3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ARDOR, "3");
                txtArdorQuemaPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbArdorQuema4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbArdorQuema4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_ARDOR, "4");
                txtArdorQuemaPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbPicor0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPicor0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_PICOR, "0");
                txtPicorPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbPicor1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPicor1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_PICOR, "1");
                txtPicorPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbPicor2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPicor2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_PICOR, "2");
                txtPicorPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbPicor3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPicor3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_PICOR, "3");
                txtPicorPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbPicor4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPicor4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_PICOR, "4");
                txtPicorPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbMalestarOjo0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMalestarOjo0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_MALESTAR, "0");
                txtMalestarOjoPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbMalestarOjo1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMalestarOjo1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_MALESTAR, "1");
                txtMalestarOjoPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbMalestarOjo2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMalestarOjo2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_MALESTAR, "2");
                txtMalestarOjoPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbMalestarOjo3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMalestarOjo3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_MALESTAR, "3");
                txtMalestarOjoPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbMalestarOjo4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMalestarOjo4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_MALESTAR, "4");
                txtMalestarOjoPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbDolorAgudo0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDolorAgudo0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_DOLOR, "0");
                txtDolorAgudoPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbDolorAgudo1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDolorAgudo0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_DOLOR, "1");
                txtDolorAgudoPtje.Text = "1";
                calcularTotal();
            }

        }
        private void rbDolorAgudo2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDolorAgudo2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_DOLOR, "2");
                txtDolorAgudoPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbDolorAgudo3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDolorAgudo3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_DOLOR, "3");
                txtDolorAgudoPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbDolorAgudo4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDolorAgudo4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_DOLOR, "3");
                txtDolorAgudoPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbLagrimeo0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLagrimeo0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_LAGRIMEO, "0");
                txtLagrimeoPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbLagrimeo1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLagrimeo1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_LAGRIMEO, "1");
                txtLagrimeoPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbLagrimeo2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLagrimeo2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_LAGRIMEO, "2");
                txtLagrimeoPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbLagrimeo3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLagrimeo3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_LAGRIMEO, "3");
                txtLagrimeoPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbLagrimeo4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLagrimeo4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_LAGRIMEO, "4");
                txtLagrimeoPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbOjosLlorosos0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOjosLlorosos0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_LLOROSOS, "0");
                txtOjosLlorososPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbOjosLlorosos1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOjosLlorosos1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_LLOROSOS, "1");
                txtOjosLlorososPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbOjosLlorosos2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOjosLlorosos2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_LLOROSOS, "2");
                txtOjosLlorososPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbOjosLlorosos3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOjosLlorosos3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_LLOROSOS, "3");
                txtOjosLlorososPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbOjosLlorosos4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOjosLlorosos4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_LLOROSOS, "4");
                txtOjosLlorososPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbSensabilidadLuz0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensabilidadLuz0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SENSIBILIDAD, "0");
                txtSensabilidadLuzPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbSensabilidadLuz1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensabilidadLuz1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SENSIBILIDAD, "1");
                txtSensabilidadLuzPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbSensabilidadLuz2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensabilidadLuz2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SENSIBILIDAD, "2");
                txtSensabilidadLuzPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbSensabilidadLuz3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensabilidadLuz3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SENSIBILIDAD, "3");
                txtSensabilidadLuzPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbSensabilidadLuz4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensabilidadLuz4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_SENSIBILIDAD, "4");
                txtSensabilidadLuzPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbVisionBorrosa0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVisionBorrosa0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_VISION, "0");
                txtVisionBorrosaPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbVisionBorrosa1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVisionBorrosa1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_VISION, "1");
                txtVisionBorrosaPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbVisionBorrosa2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVisionBorrosa2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_VISION, "2");
                txtVisionBorrosaPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbVisionBorrosa3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVisionBorrosa3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_VISION, "3");
                txtVisionBorrosaPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbVisionBorrosa4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVisionBorrosa4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_VISION, "4");
                txtVisionBorrosaPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbCansancioOjos0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCansancioOjos0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_CANSANCION, "0");
                txtCansancioOjosPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbCansancioOjos1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCansancioOjos1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_CANSANCION, "1");
                txtCansancioOjosPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbCansancioOjos2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCansancioOjos2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_CANSANCION, "2");
                txtCansancioOjosPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbCansancioOjos3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCansancioOjos3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_CANSANCION, "3");
                txtCansancioOjosPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbCansancioOjos4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCansancioOjos4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_CANSANCION, "4");
                txtCansancioOjosPtje.Text = "4";
                calcularTotal();
            }
        }

        private void rbSensacionParpadez0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionParpadez0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_PESADEZ, "0");
                txtSensacionParpadezPtje.Text = "0";
                calcularTotal();
            }
        }
        private void rbSensacionParpadez1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionParpadez1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_PESADEZ, "1");
                txtSensacionParpadezPtje.Text = "1";
                calcularTotal();
            }
        }
        private void rbSensacionParpadez2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionParpadez2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_PESADEZ, "2");
                txtSensacionParpadezPtje.Text = "2";
                calcularTotal();
            }
        }
        private void rbSensacionParpadez3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionParpadez3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_PESADEZ, "3");
                txtSensacionParpadezPtje.Text = "3";
                calcularTotal();
            }
        }
        private void rbSensacionParpadez4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSensacionParpadez4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.OJO_SECO_PESADEZ, "4");
                txtSensacionParpadezPtje.Text = "4";
                calcularTotal();
            }
        }

        #endregion

        private void lineShape11_Click(object sender, EventArgs e)
        {

        }

        private void lineShape10_Click(object sender, EventArgs e)
        {

        }

        private void lineShape8_Click(object sender, EventArgs e)
        {

        }

        private void lineShape7_Click(object sender, EventArgs e)
        {

        }

        private void lineShape6_Click(object sender, EventArgs e)
        {

        }

        private void lineShape5_Click(object sender, EventArgs e)
        {

        }

        private void lineShape4_Click(object sender, EventArgs e)
        {

        }

        private void lineShape3_Click(object sender, EventArgs e)
        {

        }

        private void lineShape9_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }
    }
}
