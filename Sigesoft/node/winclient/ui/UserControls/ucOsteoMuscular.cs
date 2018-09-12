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
    public partial class ucOsteoMuscular : UserControl
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
                //Valores por Defecto

                if (rbAbdomenExcelente.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.UC_OSTEO_FLEXIBILIDAD,"1");
                }

                if (rbCaderaExcelente.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.UC_OSTEO_CADERA, "1");
                }
                if (rbMusloExcelente.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.UC_OSTEO_MUSLO, "1");
                }

                if (rbAbdomenLateralExcelente.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABDOMEN, "1");
                }

                if (rbAbduccion180Optimo.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_180, "1");
                }

                if (rbAbduccion60Optimo.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_60, "1");
                }
                if (rbRotacion090Optimo.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_90, "1");
                }
                if (rbRotacionExtIntOptimo.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ROTACION, "1");
                }

                if (rbAbduccion180DolorNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_180_SINO, "2");
                }
                if (rbAbduccion60DolorNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_60_SINO, "2");
                }
                if (rbRotacion090DolorNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_90_SINO, "2");
                }
                if (rbRotacionExtIntDolorNO.Checked)
                {
                    SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ROTACION_SINO, "2");
                }


              

                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_FLEXIBILIDAD_PTJ, txtAbdomenPuntos.Text);
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_FLEXIBILIDAD_OBS, txtAbdomenObservaciones.Text);

                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_CADERA_PTJ, txtCaderaPuntos.Text);
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_CADERA_OBS, txtCaderaOnservaciones.Text);

                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_MUSLO_PTJ, txtMusloPuntos.Text);
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_MUSLO_OBS, txtMusloObservaciones.Text);

                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABDOMEN_PTJ, txtAbdomenLateralPuntos.Text);
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABDOMEN_OBS, txtAbdomenLateralObservaciones.Text);
                
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_180_PTJ, txtAbduccion180Puntos.Text);
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_60_PTJ, txtAbduccion60Puntos.Text);

                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_90_PTJ, txtRotacion090Puntos.Text);

                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ROTACION_PTJ, txtRotacionExtIntPuntos.Text);

                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_TOTAL1, txtTotalAptitudEspalda.Text);
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_TOTAL2, txtTotalRangos.Text);
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_OBS, txtObservaciones.Text);
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

        public ucOsteoMuscular()
        {
            InitializeComponent();
        }

        private void ucOsteoMuscular_Load(object sender, EventArgs e)
        {
            rbAbdomenExcelente.Name="N009-OTS00000001";
            rbAbdomenPromedio.Name="N009-OTS00000001";
            rbAbdomenRegular.Name="N009-OTS00000001";
            rbAbdomenPobre.Name="N009-OTS00000001";
            txtAbdomenPuntos.Name="N009-OTS00000002";
            txtAbdomenObservaciones.Name="N009-OTS00000003";

            rbCaderaExcelente.Name="N009-OTS00000004";
            rbCaderaPromedio.Name="N009-OTS00000004";
            rbCaderaRegular.Name="N009-OTS00000004";
            rbCaderaPobre.Name="N009-OTS00000004";
            txtCaderaPuntos.Name="N009-OTS00000005";
            txtCaderaOnservaciones.Name="N009-OTS00000006";

            rbMusloExcelente.Name="N009-OTS00000007";
            rbMusloPromedio.Name="N009-OTS00000007";
            rbMusloRegular.Name="N009-OTS00000007";
            rbMusloPobre.Name="N009-OTS00000007";
            txtMusloPuntos.Name="N009-OTS00000008";
            txtMusloObservaciones.Name="N009-OTS00000009";

            rbAbdomenLateralExcelente.Name="N009-OTS00000010";
            rbAbdomenLateralPromedio.Name="N009-OTS00000010";
            rbAbdomenLateralRegular.Name="N009-OTS00000010";
            rbAbdomenLateralPobre.Name="N009-OTS00000010";
            txtAbdomenLateralPuntos.Name="N009-OTS00000011";
            txtAbdomenLateralObservaciones.Name="N009-OTS00000012";

            rbAbduccion180Optimo.Name="N009-OTS00000013";
            rbAbduccion180Limitado.Name = "N009-OTS00000013";
            rbAbduccion180MuyLimitado.Name = "N009-OTS00000013";
            txtAbduccion180Puntos.Name = "N009-OTS00000014";
            rbAbduccion180DolorSI.Name = "N009-OTS00000015";
            rbAbduccion180DolorNO.Name = "N009-OTS00000015";

            rbAbduccion60Optimo.Name = "N009-OTS00000016";
            rbAbduccion60Limitado.Name = "N009-OTS00000016";
            rbAbduccion60MuyLimitado.Name = "N009-OTS00000016";
            txtAbduccion60Puntos.Name = "N009-OTS00000017";
            rbAbduccion60DolorSI.Name = "N009-OTS00000018";
            rbAbduccion60DolorNO.Name = "N009-OTS00000018";

            rbRotacion090Optimo.Name = "N009-OTS00000019";
            rbRotacion090Limitado.Name = "N009-OTS00000019";
            rbRotacion090MuyLimitado.Name = "N009-OTS00000019";
            txtRotacion090Puntos.Name = "N009-OTS00000020";
            rbRotacion090DolorSI.Name = "N009-OTS00000021";
            rbRotacion090DolorNO.Name = "N009-OTS00000021";

            rbRotacionExtIntOptimo.Name = "N009-OTS00000022";
            rbRotacionExtIntLimitado.Name = "N009-OTS00000022";
            rbRotacionExtIntMuyLimitado.Name = "N009-OTS00000022";
            txtRotacionExtIntPuntos.Name = "N009-OTS00000023";
            rbRotacionExtIntDolorSI.Name = "N009-OTS00000024";
            rbRotacionExtIntDolorNO.Name = "N009-OTS00000024";


            txtObservaciones.Name = "N009-OTS00000025";

            txtTotalAptitudEspalda.Name = "N009-OTS00000026";

            txtTotalRangos.Name = "N009-OTS00000027";


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
                            if (item.v_ComponentFieldId == Constants.UC_OSTEO_FLEXIBILIDAD)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbAbdomenExcelente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbAbdomenPromedio.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbAbdomenRegular.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbAbdomenPobre.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.UC_OSTEO_CADERA)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbCaderaExcelente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbCaderaPromedio.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbCaderaRegular.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbCaderaPobre.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.UC_OSTEO_MUSLO)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbMusloExcelente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbMusloPromedio.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbMusloRegular.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbMusloPobre.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.UC_OSTEO_ABDOMEN)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbAbdomenLateralExcelente.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbAbdomenLateralPromedio.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbAbdomenLateralRegular.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "4")
                                {
                                    rbAbdomenLateralPobre.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.UC_OSTEO_ABD_180)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbAbduccion180Optimo.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbAbduccion180Limitado.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbAbduccion180MuyLimitado.Checked = true;
                                }                               
                            }

                            if (item.v_ComponentFieldId == Constants.UC_OSTEO_ABD_60)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbAbduccion60Optimo.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbAbduccion60Limitado.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbAbduccion60MuyLimitado.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.UC_OSTEO_ABD_90)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbRotacion090Optimo.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbRotacion090Limitado.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbRotacion090MuyLimitado.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.UC_OSTEO_ROTACION)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbRotacionExtIntOptimo.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbRotacionExtIntLimitado.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "3")
                                {
                                    rbRotacionExtIntMuyLimitado.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.UC_OSTEO_ABD_180_SINO)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbAbduccion180DolorSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbAbduccion180DolorNO.Checked = true;
                                }                                
                            }

                            if (item.v_ComponentFieldId == Constants.UC_OSTEO_ABD_60_SINO)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbAbduccion60DolorSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbAbduccion60DolorNO.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.UC_OSTEO_ABD_90_SINO)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbRotacion090DolorSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbRotacion090DolorNO.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.UC_OSTEO_ROTACION_SINO)
                            {
                                if (item.v_Value1.ToString() == "1")
                                {
                                    rbRotacionExtIntDolorSI.Checked = true;
                                }
                                else if (item.v_Value1.ToString() == "2")
                                {
                                    rbRotacionExtIntDolorNO.Checked = true;
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
                    if (ctrl.Name.Contains("N009-OTS"))
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
            _UserControlValores.v_ComponentId = Constants.EXAMEN_FISICO_ID;

            _listOfAtencionAdulto1.Add(_UserControlValores);

            DataSource = _listOfAtencionAdulto1;

            #endregion
        }

        private void calcularTotal1()
        {
            int p1 = txtAbdomenPuntos.Text == "" ? 0 : int.Parse(txtAbdomenPuntos.Text.ToString());
            int p2 = txtCaderaPuntos.Text == "" ? 0 : int.Parse(txtCaderaPuntos.Text.ToString());
            int p3 = txtMusloPuntos.Text == "" ? 0 : int.Parse(txtMusloPuntos.Text.ToString());
            int p4 = txtAbdomenLateralPuntos.Text == "" ? 0 : int.Parse(txtAbdomenLateralPuntos.Text.ToString());
            txtTotalAptitudEspalda.Text = (p1 + p2 + p3 + p4 ).ToString();

        }

        private void calcularTotal2()
        {
            int p1 = txtAbduccion180Puntos.Text == "" ? 0 : int.Parse(txtAbduccion180Puntos.Text.ToString());
            int p2 = txtAbduccion60Puntos.Text == "" ? 0 : int.Parse(txtAbduccion60Puntos.Text.ToString());
            int p3 = txtRotacion090Puntos.Text == "" ? 0 : int.Parse(txtRotacion090Puntos.Text.ToString());
            int p4 = txtRotacionExtIntPuntos.Text == "" ? 0 : int.Parse(txtRotacionExtIntPuntos.Text.ToString());
            txtTotalRangos.Text = (p1 + p2 + p3 + p4).ToString();

        }

        private void rbAbdomenExcelente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbdomenExcelente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_FLEXIBILIDAD, "1");
                txtAbdomenPuntos.Text = "1";
                calcularTotal1();
            }
        }
        private void rbAbdomenPromedio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbdomenPromedio.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_FLEXIBILIDAD, "2");
                txtAbdomenPuntos.Text = "2";
                calcularTotal1();
            }
        }
        private void rbAbdomenRegular_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbdomenRegular.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_FLEXIBILIDAD, "3");
                txtAbdomenPuntos.Text = "3";
                calcularTotal1();
            }
        }
        private void rbAbdomenPobre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbdomenPobre.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_FLEXIBILIDAD, "4");
                txtAbdomenPuntos.Text = "4";
                calcularTotal1();
            }
        }

        private void rbCaderaExcelente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCaderaExcelente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_CADERA, "1");
                txtCaderaPuntos.Text = "1";
                calcularTotal1();
            }
        }
        private void rbCaderaPromedio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCaderaPromedio.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_CADERA, "2");
                txtCaderaPuntos.Text = "2";
                calcularTotal1();
            }
        }
        private void rbCaderaRegular_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCaderaRegular.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_CADERA, "3");
                txtCaderaPuntos.Text = "3";
                calcularTotal1();
            }
        }
        private void rbCaderaPobre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCaderaPobre.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_CADERA, "4");
                txtCaderaPuntos.Text = "4";
                calcularTotal1();
            }
        }

        private void rbMusloExcelente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMusloExcelente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_MUSLO, "1");
                txtMusloPuntos.Text = "1";
                calcularTotal1();
            }
        }
        private void rbMusloPromedio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMusloPromedio.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_MUSLO, "2");
                txtMusloPuntos.Text = "2";
                calcularTotal1();
            }
        }
        private void rbMusloRegular_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMusloRegular.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_MUSLO, "3");
                txtMusloPuntos.Text = "3";
                calcularTotal1();
            }
        }
        private void rbMusloPobre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMusloPobre.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_MUSLO, "4");
                txtMusloPuntos.Text = "4";
                calcularTotal1();
            }
        }

        private void rbAbdomenLateralExcelente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbdomenLateralExcelente.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABDOMEN, "1");
                txtAbdomenLateralPuntos.Text = "1";
                calcularTotal1();
            }
        }
        private void rbAbdomenLateralPromedio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbdomenLateralPromedio.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABDOMEN, "2");
                txtAbdomenLateralPuntos.Text = "2";
                calcularTotal1();
            }
        }
        private void rbAbdomenLateralRegular_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbdomenLateralRegular.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABDOMEN, "3");
                txtAbdomenLateralPuntos.Text = "3";
                calcularTotal1();
            }
        }
        private void rbAbdomenLateralPobre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbdomenLateralPobre.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABDOMEN, "4");
                txtAbdomenLateralPuntos.Text = "4";
                calcularTotal1();
            }
        }

        private void rbAbduccion180Optimo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbduccion180Optimo.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_180, "1");
                txtAbduccion180Puntos.Text = "1";
                calcularTotal2();
            }
        }
        private void rbAbduccion180Limitado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbduccion180Limitado.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_180, "2");
                txtAbduccion180Puntos.Text = "2";
                calcularTotal2();
            }
        }
        private void rbAbduccion180MuyLimitado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbduccion180MuyLimitado.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_180, "3");
                txtAbduccion180Puntos.Text = "3";
                calcularTotal2();
            }
        }

        private void rbAbduccion60Optimo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbduccion60Optimo.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_60, "1");
                txtAbduccion60Puntos.Text = "1";
                calcularTotal2();
            }
        }
        private void rbAbduccion60Limitado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbduccion60Limitado.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_60, "2");
                txtAbduccion60Puntos.Text = "2";
                calcularTotal2();
            }
        }
        private void rbAbduccion60MuyLimitado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbduccion60MuyLimitado.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_60, "3");
                txtAbduccion60Puntos.Text = "3";
                calcularTotal2();
            }
        }

        private void rbRotacion090Optimo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotacion090Optimo.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_90, "1");
                txtRotacion090Puntos.Text = "1";
                calcularTotal2();
            }
        }
        private void rbRotacion090Limitado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotacion090Limitado.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_90, "2");
                txtRotacion090Puntos.Text = "2";
                calcularTotal2();
            }
        }
        private void rbRotacion090MuyLimitado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotacion090MuyLimitado.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_90, "3");
                txtRotacion090Puntos.Text = "3";
                calcularTotal2();
            }
        }

        private void rbRotacionExtIntOptimo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotacionExtIntOptimo.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ROTACION, "1");
                txtRotacionExtIntPuntos.Text = "1";
                calcularTotal2();
            }
        }
        private void rbRotacionExtIntLimitado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotacionExtIntLimitado.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ROTACION, "2");
                txtRotacionExtIntPuntos.Text = "2";
                calcularTotal2();
            }
        }
        private void rbRotacionExtIntMuyLimitado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotacionExtIntMuyLimitado.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ROTACION, "3");
                txtRotacionExtIntPuntos.Text = "3";
                calcularTotal2();
            }
        }

        private void rbAbduccion180DolorSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbduccion180DolorSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_180_SINO, "1");            
            }
        }
        private void rbAbduccion180DolorNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbduccion180DolorNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_180_SINO, "2");

            }
        }

        private void rbAbduccion60DolorSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbduccion60DolorSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_60_SINO, "1");
            }
        }
        private void rbAbduccion60DolorNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbduccion60DolorNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_60_SINO, "2");
            }
        }

        private void rbRotacion090DolorSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotacion090DolorSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_90_SINO, "1");
            }
        }
        private void rbRotacion090DolorNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotacion090DolorNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ABD_90_SINO, "2");
            }
        }

        private void rbRotacionExtIntDolorSI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotacionExtIntDolorSI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ROTACION_SINO, "1");
            }
        }
        private void rbRotacionExtIntDolorNO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotacionExtIntDolorNO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.UC_OSTEO_ROTACION_SINO, "2");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lineShape8_Click(object sender, EventArgs e)
        {

        }

        private void lineShape6_Click(object sender, EventArgs e)
        {

        }

        private void lineShape5_Click(object sender, EventArgs e)
        {

        }
    }
}
