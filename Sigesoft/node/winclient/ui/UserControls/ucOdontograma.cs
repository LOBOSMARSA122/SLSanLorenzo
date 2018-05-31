using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucOdontograma : UserControl
    {

        #region Declarations
        List<string> listaCaries = new List<string>();
        List<string> listaAusentes = new List<string>();
        ServiceComponentFieldValuesList oOdontograma;
        List<ServiceComponentFieldValuesList> _ListaOdontograma = new List<ServiceComponentFieldValuesList>();
        string _valueOld;
        bool _isChangueValueControl;
        /// <summary>
        /// Lista para el transporte de diagnosticos desde el odontograma hacia el ESO
        /// </summary>
        private List<DiagnosticRepositoryList> _tmpExamDiagnosticComponentList = null;

        #endregion

        #region Properties
        public bool IsChangeValueControl { get { return _isChangueValueControl; } }

        public List<ServiceComponentFieldValuesList> DataSource
        {
            get
            {
                if (_isChangueValueControl)
                {
                    return _ListaOdontograma;
                }
                else
                {
                    _ListaOdontograma = new List<ServiceComponentFieldValuesList>();
                    return _ListaOdontograma ;
                }
                
            }
            set
            {
                if (value != _ListaOdontograma)
                {
                    //AddRemoveEventControl(this, "Remove");
                    ClearValueControl();
                    //AddRemoveEventControl(this, "Add");                 

                    var newListaOdontograma = ListOdoToNewEntity(value);

                    _ListaOdontograma = newListaOdontograma;
                    SearchControlAndFill(this, value);
                }
            }
        }

        public string _v_ServiceComponentFieldsId { get; set; }

        public string _v_ServiceComponentId { get; set; }

        public string _v_ServiceComponentFieldValuesId { get; set; }

        #endregion

        #region Custom Events
        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is Label)
                {
                    if (ctrl.Name.Contains("N002-ODO"))
                    {
                        ctrl.Click += new EventHandler(lbl_Click);
                        ctrl.Enter += new EventHandler(lbl_Enter);
                        ctrl.Leave += new EventHandler(lbl_Leave);
                    }
                }

                if (ctrl is ComboBox)
                {
                    if (ctrl.Name.Contains("N002-ODO") && ctrl.Name != "N002-ODO00000198")
                    {
                        var obj = (ComboBox)ctrl;
                        obj.SelectedValueChanged += new EventHandler(ucd_TextChanged);
                        ctrl.Click += new EventHandler(ucd_Click);
                        ctrl.Leave += new EventHandler(ucd_Leave);
                    }
                }

                if (ctrl.HasChildren)
                    SearchControlAndSetEvents(ctrl);

            }

        }

        private void AddRemoveEventControl(Control ctrlContainer, string action)
        {

            if (action == "Add")
            {
                foreach (Control ctrl in ctrlContainer.Controls)
                {
                    if (ctrl is Label)
                    {
                        if (ctrl.Name.Contains("N002-ODO"))
                        {
                            ((Label)ctrl).Click += new EventHandler(lbl_Click);
                            ((Label)ctrl).Enter += new EventHandler(lbl_Enter);
                            ((Label)ctrl).Leave += new EventHandler(lbl_Leave);
                        }
                    }
                    if (ctrl is ComboBox)
                    {

                        ((ComboBox)ctrl).SelectedValueChanged += new EventHandler(ucd_TextChanged);
                        ((ComboBox)ctrl).Click += new EventHandler(ucd_Click);
                        ((ComboBox)ctrl).Leave += new EventHandler(ucd_Leave);
                    }
                    if (ctrl.HasChildren)
                        AddRemoveEventControl(ctrl, "Add");
                }
            }
            else if (action == "Remove")
            {
                foreach (Control ctrl in ctrlContainer.Controls)
                {
                    if (ctrl is Label)
                    {
                        if (ctrl.Name.Contains("N002-ODO"))
                        {
                            ((Label)ctrl).Click -= lbl_Click;
                            ((Label)ctrl).Enter -= lbl_Enter;
                            ((Label)ctrl).Leave -= lbl_Leave;
                        }
                    }
                    if (ctrl is ComboBox)
                    {
                        ((ComboBox)ctrl).SelectedValueChanged -= ucd_TextChanged;
                        ((ComboBox)ctrl).Click -= ucd_Click;
                        ((ComboBox)ctrl).Leave -= ucd_Leave;
                    }
                    if (ctrl.HasChildren)
                        AddRemoveEventControl(ctrl, "Remove");
                }
            }


        }

        private void lbl_Leave(object sender, System.EventArgs e)
        {
            Label senderCtrl = (Label)sender;
            int ColorDevuelto = paintedLabel(senderCtrl.BackColor, senderCtrl);

            if (_valueOld != ColorDevuelto.ToString())
            {
                _isChangueValueControl = true;
            }
            else
            {
                _isChangueValueControl = false;
            }
        }

        private void ucd_Leave(object sender, System.EventArgs e)
        {
            ComboBox senderCtrl = (ComboBox)sender;

            if (_valueOld != senderCtrl.SelectedValue.ToString())
            {
                _isChangueValueControl = true;
            }
            else
            {
                _isChangueValueControl = false;
            }
        }

        private void lbl_Enter(object sender, System.EventArgs e)
        {
            Label senderCtrl = (Label)sender;
            int ColorDevuelto = paintedLabel(senderCtrl.BackColor, senderCtrl);

            _valueOld = ColorDevuelto.ToString();
        }

        private void ucd_Click(object sender, System.EventArgs e)
        {
            ComboBox senderCtrl = (ComboBox)sender;
            _valueOld = senderCtrl.SelectedValue.ToString();
        }

        private void lbl_Click(object sender, System.EventArgs e)
        {
            // Capturar el control invocador
            Label senderCtrl = (Label)sender;
            int ColorDevuelto = paintedLabel(senderCtrl.BackColor, senderCtrl);
            if (ColorDevuelto == (int)(Common.ColorDiente.Red))
            {
                listaCaries.Add(senderCtrl.Tag.ToString());
                txtPzaCar.Text = CadenaCaries(listaCaries);
            }
            else
            {
                if (senderCtrl.BackColor == System.Drawing.Color.Blue || senderCtrl.BackColor == System.Drawing.Color.White)
                {
                    //senderCtrl.Tag.ToString());
                    //listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString());
                    listaCaries.Remove(senderCtrl.Tag.ToString());
                    txtPzaCar.Text = CadenaCaries(listaCaries);
                }
            }
            oOdontograma = new ServiceComponentFieldValuesList();
            //Llenar la lista para grabar el user control////////////////////////

            int Result = _ListaOdontograma.RemoveAll(p => p.v_ComponentFieldId == senderCtrl.Name);
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = senderCtrl.Name;

            oOdontograma.v_Value1 = ColorDevuelto.ToString();
            oOdontograma.v_Value2 = senderCtrl.Tag.ToString();
            oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;

            _ListaOdontograma.Add(oOdontograma);
            DataSource = _ListaOdontograma;

            if (_valueOld != ColorDevuelto.ToString())
            {
                _isChangueValueControl = true;
            }
            else
            {
                _isChangueValueControl = false;
            }
            ///////////////////// FIN LLENADO DE LA LISTA////////////////////////
        }

        private void ucd_TextChanged(object sender, System.EventArgs e)
        {
            // Capturar el control invocador
            ComboBox senderCtrl = (ComboBox)sender;
            int Id;
            //bool NotExistAusent = true;
            if (senderCtrl.SelectedValue == null) return;

            var y = _ListaOdontograma.OrderBy(p => p.v_ComponentFieldId).ToList();
            int Result = _ListaOdontograma.RemoveAll(p => p.v_ComponentFieldId == senderCtrl.Name);
            Id = int.Parse(senderCtrl.SelectedValue.ToString());

            if (Id == (int)Common.LeyendaOdontograma.Ausente)
            {
                // Buscamos el control PictureImage para poder saber que imagen vamos a cambiar
                PictureBox ControlPictureBox;
                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.dienteausente;

                //Agregar dientes ausentes en el textbox
                listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
                //NotExistAusent = false;

                DataSource = _ListaOdontograma;
            }
            else if (Id == (int)Common.LeyendaOdontograma.Exodoncia)
            {
                // Buscamos el control PictureImage para poder saber que imagen vamos a cambiar
                PictureBox ControlPictureBox;
                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.Exodoncia;

                ////Agregar dientes ausentes en el textbox
                //listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                //txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
            }
            else if (Id == (int)Common.LeyendaOdontograma.Implante)
            {
                // Buscamos el control PictureImage para poder saber que imagen vamos a cambiar
                PictureBox ControlPictureBox;
                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.Implante;

                ////Agregar dientes ausentes en el textbox
                //listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                //txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
            }
            else if (Id == (int)Common.LeyendaOdontograma.PPR)
            {
                // Buscamos el control PictureImage para poder saber que imagen vamos a cambiar
                PictureBox ControlPictureBox;
                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.PPR;

                ////Agregar dientes ausentes en el textbox
                //listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                //txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
                //NotExistAusent = false;

                DataSource = _ListaOdontograma;
            }
            else if (Id == (int)Common.LeyendaOdontograma.Corona)
            {
                oOdontograma = new ServiceComponentFieldValuesList();
                PictureBox ControlPictureBox;

                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.diente3;

                ////Agregar dientes ausentes en el textbox
                //listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                //txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
                //NotExistAusent = false;

                DataSource = _ListaOdontograma;
            }
            else if (Id == (int)Common.LeyendaOdontograma.AparatoOrtodontico)
            {
                oOdontograma = new ServiceComponentFieldValuesList();
                PictureBox ControlPictureBox;

                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.diente3;

                ////Agregar dientes ausentes en el textbox
                //listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                //txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
                //NotExistAusent = false;

                DataSource = _ListaOdontograma;
            }
            else if (Id == (int)Common.LeyendaOdontograma.ProtesisTotal)
            {
                // Buscamos el control PictureImage para poder saber que imagen vamos a cambiar
                PictureBox ControlPictureBox;
                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.ProtesisTotal;

                ////Agregar dientes ausentes en el textbox
                //listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                //txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
            }
            else if (Id == (int)Common.LeyendaOdontograma.RemanenteRedicular)
            {
                // Buscamos el control PictureImage para poder saber que imagen vamos a cambiar
                PictureBox ControlPictureBox;
                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.remanenteredicular;

                ////Agregar dientes ausentes en el textbox
                //listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                //txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
            }
            else if (Id == (int)Common.LeyendaOdontograma.CoronaTemporal)
            {
                // Buscamos el control PictureImage para poder saber que imagen vamos a cambiar
                PictureBox ControlPictureBox;
                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.coronatemporal;

                ////Agregar dientes ausentes en el textbox
                //listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                //txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
            }
            else if (Id == (int)Common.LeyendaOdontograma.CoronaDefinitiva)
            {
                // Buscamos el control PictureImage para poder saber que imagen vamos a cambiar
                PictureBox ControlPictureBox;
                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.coronadefinitiva;

                ////Agregar dientes ausentes en el textbox
                //listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                //txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
            }
            else if (Id == (int)Common.LeyendaOdontograma.ProtesisFijaBueno)
            {
                // Buscamos el control PictureImage para poder saber que imagen vamos a cambiar
                PictureBox ControlPictureBox;
                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.protesisfijabueno;

                ////Agregar dientes ausentes en el textbox
                //listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                //txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
            }
            else if (Id == (int)Common.LeyendaOdontograma.ProtesisFijaMalo)
            {
                // Buscamos el control PictureImage para poder saber que imagen vamos a cambiar
                PictureBox ControlPictureBox;
                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.protesisfijamalo;

                ////Agregar dientes ausentes en el textbox
                //listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                //txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
            }
            else if (Id == (int)Common.LeyendaOdontograma.ProtesisRemovible)
            {
                // Buscamos el control PictureImage para poder saber que imagen vamos a cambiar
                PictureBox ControlPictureBox;
                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.protesisremovible;

                ////Agregar dientes ausentes en el textbox
                //listaAusentes.Add(senderCtrl.Tag.ToString().Substring(3, 2));
                //txtPzaAus.Text = CadenaAusente(listaAusentes);

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                oOdontograma = new ServiceComponentFieldValuesList();
                oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
                oOdontograma.v_Value1 = Id.ToString();
                oOdontograma.v_ComponentFieldId = senderCtrl.Name;

                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "BLOQUEAR");
            }
            else if (Id == -1)
            {
                oOdontograma = new ServiceComponentFieldValuesList();
                PictureBox ControlPictureBox;

                var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.diente3;



                oOdontograma.v_ComponentFieldId = senderCtrl.Name;
                oOdontograma.v_Value1 = Id.ToString();
                _ListaOdontograma.Add(oOdontograma);

                BloquearDesbloquearLabels(this, senderCtrl, "DESBLOQUEAR");

                DataSource = _ListaOdontograma;

                //Como es un diente ausente tengo que remover el diente de la lista con caries
                listaCaries.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaCar.Text = CadenaCaries(listaCaries);

                listaAusentes.RemoveAll(p => p == senderCtrl.Tag.ToString().Substring(3, 2));
                txtPzaAus.Text = CadenaAusente(listaAusentes);

            }

            /////////////////////// FIN LLENADO DE LA LISTA////////////////////////

            //if (NotExistAusent && senderCtrl.Tag != null)
            //{
            //    oOdontograma = new ServiceComponentFieldValuesList();
            //    PictureBox ControlPictureBox;

            //    var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
            //    ControlPictureBox = ListControlPictureBox[0] as PictureBox;
            //    ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.diente3;

            //    //Quitar dientes ausentes en el textbox   
            //    listaAusentes.Remove(senderCtrl.Tag.ToString().Substring(3, 2));
            //    txtPzaAus.Text = CadenaAusente(listaAusentes);

            //    oOdontograma.v_ComponentFieldId = senderCtrl.Name;
            //    oOdontograma.v_Value1 = Id.ToString();
            //    _ListaOdontograma.Add(oOdontograma);               

            //    BloquearDesbloquearLabels(this, senderCtrl, "DESBLOQUEAR");
            //    DataSource = _ListaOdontograma;
            //}

            //var newEntity = ListOdoToNewEntity(_ListaOdontograma);

            //oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            //oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            //oOdontograma.v_ComponentFieldId = senderCtrl.Name;

            ////newEntity.Add(oOdontograma);

            //_ListaOdontograma.Add(oOdontograma);

            //////_ListaOdontograma = newEntity;

            //DataSource = _ListaOdontograma;
        }

        #endregion

        #region Otros
        public void ClearValueControl()
        {
            SearchControlAndSetValueDefault(this);
            _isChangueValueControl = false;
            //Otros Controles
            txtPzaCar.Text = string.Empty;
            txtPzaAus.Text = string.Empty;
            txtOtros.Text = string.Empty;

            chkPresenciaPlaca.Checked = false;
            chkPresenciaRema.Checked = false;
            ddlStatusAptitudId.SelectedValue = "-1";
        }

        public ucOdontograma()
        {
            InitializeComponent();
            //Setear en nuevo NAme de cada control
            #region Cuadrante 1

            //Diente 18
            chkd181.Name = Constants.D18_1;
            chkd182.Name = Constants.D18_2;
            chkd183.Name = Constants.D18_3;
            chkd184.Name = Constants.D18_4;
            chkd185.Name = Constants.D18_5;
            ucd186.Name = Constants.D18_6;

            //Diente 17
            chkd171.Name = Constants.D17_1;
            chkd172.Name = Constants.D17_2;
            chkd173.Name = Constants.D17_3;
            chkd174.Name = Constants.D17_4;
            chkd175.Name = Constants.D17_5;
            ucd176.Name = Constants.D17_6;

            //Diente 16
            chkd161.Name = Constants.D16_1;
            chkd162.Name = Constants.D16_2;
            chkd163.Name = Constants.D16_3;
            chkd164.Name = Constants.D16_4;
            chkd165.Name = Constants.D16_5;
            ucd166.Name = Constants.D16_6;

            //Diente 15
            chkd151.Name = Constants.D15_1;
            chkd152.Name = Constants.D15_2;
            chkd153.Name = Constants.D15_3;
            chkd154.Name = Constants.D15_4;
            chkd155.Name = Constants.D15_5;
            ucd156.Name = Constants.D15_6;

            //Diente 14
            chkd141.Name = Constants.D14_1;
            chkd142.Name = Constants.D14_2;
            chkd143.Name = Constants.D14_3;
            chkd144.Name = Constants.D14_4;
            chkd145.Name = Constants.D14_5;
            ucd146.Name = Constants.D14_6;

            //Diente 13
            chkd131.Name = Constants.D13_1;
            chkd132.Name = Constants.D13_2;
            chkd133.Name = Constants.D13_3;
            chkd134.Name = Constants.D13_4;
            chkd135.Name = Constants.D13_5;
            ucd136.Name = Constants.D13_6;

            //Diente 12
            chkd121.Name = Constants.D12_1;
            chkd122.Name = Constants.D12_2;
            chkd123.Name = Constants.D12_3;
            chkd124.Name = Constants.D12_4;
            chkd125.Name = Constants.D12_5;
            ucd126.Name = Constants.D12_6;

            //Diente 11
            chkd111.Name = Constants.D11_1;
            chkd112.Name = Constants.D11_2;
            chkd113.Name = Constants.D11_3;
            chkd114.Name = Constants.D11_4;
            chkd115.Name = Constants.D11_5;
            ucd116.Name = Constants.D11_6;

            #endregion

            #region Cuadrante 2

            //Diente 21
            chkd211.Name = Constants.D21_1;
            chkd212.Name = Constants.D21_2;
            chkd213.Name = Constants.D21_3;
            chkd214.Name = Constants.D21_4;
            chkd215.Name = Constants.D21_5;
            ucd216.Name = Constants.D21_6;

            //Diente 22
            chkd221.Name = Constants.D22_1;
            chkd222.Name = Constants.D22_2;
            chkd223.Name = Constants.D22_3;
            chkd224.Name = Constants.D22_4;
            chkd225.Name = Constants.D22_5;
            ucd226.Name = Constants.D22_6;

            //Diente 23
            chkd231.Name = Constants.D23_1;
            chkd232.Name = Constants.D23_2;
            chkd233.Name = Constants.D23_3;
            chkd234.Name = Constants.D23_4;
            chkd235.Name = Constants.D23_5;
            ucd236.Name = Constants.D23_6;

            //Diente 24
            chkd241.Name = Constants.D24_1;
            chkd242.Name = Constants.D24_2;
            chkd243.Name = Constants.D24_3;
            chkd244.Name = Constants.D24_4;
            chkd245.Name = Constants.D24_5;
            ucd246.Name = Constants.D24_6;

            //Diente 25
            chkd251.Name = Constants.D25_1;
            chkd252.Name = Constants.D25_2;
            chkd253.Name = Constants.D25_3;
            chkd254.Name = Constants.D25_4;
            chkd255.Name = Constants.D25_5;
            ucd256.Name = Constants.D25_6;

            //Diente 26
            chkd261.Name = Constants.D26_1;
            chkd262.Name = Constants.D26_2;
            chkd263.Name = Constants.D26_3;
            chkd264.Name = Constants.D26_4;
            chkd265.Name = Constants.D26_5;
            ucd266.Name = Constants.D26_6;

            //Diente 27
            chkd271.Name = Constants.D27_1;
            chkd272.Name = Constants.D27_2;
            chkd273.Name = Constants.D27_3;
            chkd274.Name = Constants.D27_4;
            chkd275.Name = Constants.D27_5;
            ucd276.Name = Constants.D27_6;

            //Diente 28
            chkd281.Name = Constants.D28_1;
            chkd282.Name = Constants.D28_2;
            chkd283.Name = Constants.D28_3;
            chkd284.Name = Constants.D28_4;
            chkd285.Name = Constants.D28_5;
            ucd286.Name = Constants.D28_6;

            #endregion

            #region Cuadrante 3

            //Diente 31
            chkd311.Name = Constants.D31_1;
            chkd312.Name = Constants.D31_2;
            chkd313.Name = Constants.D31_3;
            chkd314.Name = Constants.D31_4;
            chkd315.Name = Constants.D31_5;
            ucd316.Name = Constants.D31_6;

            //Diente 32
            chkd321.Name = Constants.D32_1;
            chkd322.Name = Constants.D32_2;
            chkd323.Name = Constants.D32_3;
            chkd324.Name = Constants.D32_4;
            chkd325.Name = Constants.D32_5;
            ucd326.Name = Constants.D32_6;

            //Diente 33
            chkd331.Name = Constants.D33_1;
            chkd332.Name = Constants.D33_2;
            chkd333.Name = Constants.D33_3;
            chkd334.Name = Constants.D33_4;
            chkd335.Name = Constants.D33_5;
            ucd336.Name = Constants.D33_6;

            //Diente 34
            chkd341.Name = Constants.D34_1;
            chkd342.Name = Constants.D34_2;
            chkd343.Name = Constants.D34_3;
            chkd344.Name = Constants.D34_4;
            chkd345.Name = Constants.D34_5;
            ucd346.Name = Constants.D34_6;

            //Diente 35
            chkd351.Name = Constants.D35_1;
            chkd352.Name = Constants.D35_2;
            chkd353.Name = Constants.D35_3;
            chkd354.Name = Constants.D35_4;
            chkd355.Name = Constants.D35_5;
            ucd356.Name = Constants.D35_6;

            //Diente 36
            chkd361.Name = Constants.D36_1;
            chkd362.Name = Constants.D36_2;
            chkd363.Name = Constants.D36_3;
            chkd364.Name = Constants.D36_4;
            chkd365.Name = Constants.D36_5;
            ucd366.Name = Constants.D36_6;

            //Diente 37
            chkd371.Name = Constants.D37_1;
            chkd372.Name = Constants.D37_2;
            chkd373.Name = Constants.D37_3;
            chkd374.Name = Constants.D37_4;
            chkd375.Name = Constants.D37_5;
            ucd376.Name = Constants.D37_6;

            //Diente 38
            chkd381.Name = Constants.D38_1;
            chkd382.Name = Constants.D38_2;
            chkd383.Name = Constants.D38_3;
            chkd384.Name = Constants.D38_4;
            chkd385.Name = Constants.D38_5;
            ucd386.Name = Constants.D38_6;

            #endregion

            #region Cuadrante 4

            //Diente 41
            chkd411.Name = Constants.D41_1;
            chkd412.Name = Constants.D41_2;
            chkd413.Name = Constants.D41_3;
            chkd414.Name = Constants.D41_4;
            chkd415.Name = Constants.D41_5;
            ucd416.Name = Constants.D41_6;

            //Diente 42
            chkd421.Name = Constants.D42_1;
            chkd422.Name = Constants.D42_2;
            chkd423.Name = Constants.D42_3;
            chkd424.Name = Constants.D42_4;
            chkd425.Name = Constants.D42_5;
            ucd426.Name = Constants.D42_6;

            //Diente 43
            chkd431.Name = Constants.D43_1;
            chkd432.Name = Constants.D43_2;
            chkd433.Name = Constants.D43_3;
            chkd434.Name = Constants.D43_4;
            chkd435.Name = Constants.D43_5;
            ucd436.Name = Constants.D43_6;

            //Diente 44
            chkd441.Name = Constants.D44_1;
            chkd442.Name = Constants.D44_2;
            chkd443.Name = Constants.D44_3;
            chkd444.Name = Constants.D44_4;
            chkd445.Name = Constants.D44_5;
            ucd446.Name = Constants.D44_6;

            //Diente 45
            chkd451.Name = Constants.D45_1;
            chkd452.Name = Constants.D45_2;
            chkd453.Name = Constants.D45_3;
            chkd454.Name = Constants.D45_4;
            chkd455.Name = Constants.D45_5;
            ucd456.Name = Constants.D45_6;

            //Diente 46
            chkd461.Name = Constants.D46_1;
            chkd462.Name = Constants.D46_2;
            chkd463.Name = Constants.D46_3;
            chkd464.Name = Constants.D46_4;
            chkd465.Name = Constants.D46_5;
            ucd466.Name = Constants.D46_6;

            //Diente 47
            chkd471.Name = Constants.D47_1;
            chkd472.Name = Constants.D47_2;
            chkd473.Name = Constants.D47_3;
            chkd474.Name = Constants.D47_4;
            chkd475.Name = Constants.D47_5;
            ucd476.Name = Constants.D47_6;

            //Diente 48
            chkd481.Name = Constants.D48_1;
            chkd482.Name = Constants.D48_2;
            chkd483.Name = Constants.D48_3;
            chkd484.Name = Constants.D48_4;
            chkd485.Name = Constants.D48_5;
            ucd486.Name = Constants.D48_6;

            #endregion

            //Otros Controles
            txtPzaCar.Name = Constants.PiezaCaries;
            txtPzaAus.Name = Constants.PiezaAusentes;
            txtOtros.Name = Constants.Otros;

            chkPresenciaPlaca.Name = Constants.chkBacteriana;
            chkPresenciaRema.Name = Constants.chkRadiculares;
            ddlStatusAptitudId.Name = Constants.Aptitud;

            //SearchControlAndSetValueDefault(this);
            oOdontograma = new ServiceComponentFieldValuesList();

            #region Llenado de combos
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ucd186, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd176, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd166, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd156, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd146, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd136, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd126, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd116, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd216, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd226, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd236, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd246, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd256, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd266, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd276, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd286, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd316, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd326, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd336, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd346, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd356, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd366, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd376, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd386, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd416, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd426, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd436, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd446, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd456, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd466, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd476, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ucd486, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 162, null), DropDownListAction.Select);

            #endregion

            #region Init Controls

            //Llenar combo Aptitud
            Utils.LoadDropDownList(ddlStatusAptitudId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 163, null), DropDownListAction.Select);

            oOdontograma = new ServiceComponentFieldValuesList();
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = txtPzaCar.Name;
            oOdontograma.v_Value1 = "";
            _ListaOdontograma.Add(oOdontograma);

            oOdontograma = new ServiceComponentFieldValuesList();
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = txtPzaAus.Name;
            oOdontograma.v_Value1 = "";
            _ListaOdontograma.Add(oOdontograma);

            oOdontograma = new ServiceComponentFieldValuesList();
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = txtOtros.Name;
            oOdontograma.v_Value1 = "";
            _ListaOdontograma.Add(oOdontograma);

            oOdontograma = new ServiceComponentFieldValuesList();
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = chkPresenciaPlaca.Name;
            oOdontograma.v_Value1 = "0";
            _ListaOdontograma.Add(oOdontograma);

            oOdontograma = new ServiceComponentFieldValuesList();
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = chkPresenciaRema.Name;
            oOdontograma.v_Value1 = "0";
            _ListaOdontograma.Add(oOdontograma);

            oOdontograma = new ServiceComponentFieldValuesList();
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = ddlStatusAptitudId.Name;
            oOdontograma.v_Value1 = "-1";
            _ListaOdontograma.Add(oOdontograma);
            #endregion

            SearchControlAndSetEvents(this);
        }

        private void ucOdontograma_Load(object sender, EventArgs e)
        {

        }

        private Control[] FindControlLabel(string key)
        {
            //tcExamList.Tabs.TabControl.Controls.Find(key, true);
            var findControl = this.Controls.Find("N002-ODO", true);

            return findControl;
        }

        private void SearchControlAndSetValueDefault(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is Label)
                {
                    if (ctrl.Name.Contains("N002-ODO"))
                    {
                        ctrl.BackColor = System.Drawing.Color.White;
                    }
                }
                if (ctrl is ComboBox)
                {
                    ((ComboBox)ctrl).SelectedValue = "-1";
                }
                if (ctrl.HasChildren)
                    SearchControlAndSetEvents(ctrl);
            }
        }

        int paintedLabel(System.Drawing.Color Color, Label control)
        {
            int Value = 0;
            if (Color == System.Drawing.Color.White)
            {
                control.BackColor = System.Drawing.Color.Red;
                Value = (int)(Common.ColorDiente.Red);
            }
            else if (Color == System.Drawing.Color.Red)
            {
                control.BackColor = System.Drawing.Color.Blue;
                Value = (int)(Common.ColorDiente.Blue);
            }
            else if (Color == System.Drawing.Color.Blue)
            {
                control.BackColor = System.Drawing.Color.White;
                Value = (int)(Common.ColorDiente.White);
            }
            return Value;
        }

        string CadenaCaries(List<string> Lista)
        {
            string _Cadena = string.Empty;

            var y = Lista.OrderBy(p1 => p1).ToList();
            var x = y.GroupBy(p => p).ToList();


            foreach (var item in x)
            {
                _Cadena += item.Key.ToString() + ";";
            }
            if (_Cadena != string.Empty)
            {
                _Cadena = _Cadena.Substring(0, _Cadena.Length - 1);
            }

            return _Cadena;
        }

        string CadenaAusente(List<string> Lista)
        {
            string _Cadena = string.Empty;
            var y = Lista.OrderBy(p1 => p1).ToList();
            var x = y.GroupBy(p => p).ToList();
            foreach (var item in x)
            {
                _Cadena += item.Key.ToString() + ";";
            }
            if (_Cadena != string.Empty)
            {
                _Cadena = _Cadena.Substring(0, _Cadena.Length - 1);
            }

            return _Cadena;
        }

        private List<ServiceComponentFieldValuesList> ListOdoToNewEntity(List<ServiceComponentFieldValuesList> odo)
        {
            List<ServiceComponentFieldValuesList> temp = new List<ServiceComponentFieldValuesList>();

            foreach (var item in odo)
            {
                var child = new ServiceComponentFieldValuesList();
                child.v_ComponentFieldId = item.v_ComponentFieldId;
                child.v_Value1 = item.v_Value1;
                temp.Add(child);
            }
            return temp;
        }

        private void BloquearDesbloquearLabels(Control ctrlContainer, ComboBox senderCtrl, string Mode)
        {
            if (Mode == "BLOQUEAR")
            {
                string TagControlLabel = senderCtrl.Tag.ToString().Substring(3, 2);
                List<string> List = ListControlId(TagControlLabel);

                foreach (var item in List)
                {
                    var x = ctrlContainer.Controls.Find(item.ToString(), true);

                    ((Label)x[0]).Enabled = false;
                    ((Label)x[0]).BackColor = System.Drawing.Color.White;
                    var Result = _ListaOdontograma.Find(p => p.v_ComponentFieldId == ((Label)x[0]).Name);
                    if (Result != null)
                    {
                        _ListaOdontograma.Remove(Result);
                        oOdontograma = new ServiceComponentFieldValuesList();

                        oOdontograma.v_ComponentFieldId = ((Label)x[0]).Name;
                        oOdontograma.v_Value1 = ((int)ColorDiente.White).ToString();
                        _ListaOdontograma.Add(oOdontograma);
                    }
                }
            }
            else if (Mode == "DESBLOQUEAR")
            {
                string TagControlLabel = senderCtrl.Tag.ToString().Substring(3, 2);
                List<string> List = ListControlId(TagControlLabel);
                foreach (var item in List)
                {
                    var x = ctrlContainer.Controls.Find(item.ToString(), true);

                    ((Label)x[0]).Enabled = true;
                    ((Label)x[0]).BackColor = System.Drawing.Color.White;
                    var Result = _ListaOdontograma.Find(p => p.v_ComponentFieldId == ((Label)x[0]).Name);
                    if (Result != null)
                    {
                        _ListaOdontograma.Remove(Result);
                        oOdontograma = new ServiceComponentFieldValuesList();

                        oOdontograma.v_ComponentFieldId = ((Label)x[0]).Name;
                        oOdontograma.v_Value1 = ((int)ColorDiente.White).ToString();
                        _ListaOdontograma.Add(oOdontograma);
                    }
                }
            }


            //foreach (Control ctrl in ctrlContainer.Controls)
            //{
            //    if (Mode == "BLOQUEAR")
            //    {
            //        if (ctrl is Label)
            //        {
            //            string prefijo = senderCtrl.Tag.ToString().Substring(3, 2);
            //            if (ctrl.Tag != null)
            //            {
            //                if (ctrl.Tag.ToString() == prefijo)
            //                {
            //                    ctrl.Enabled = false;
            //                    ctrl.BackColor = System.Drawing.Color.White;

            //                    var Result = _ListaOdontograma.Find(p => p.v_ComponentFieldId == ctrl.Name);

            //                    if (Result != null && ctrl is Label)
            //                    {
            //                        //_ListaOdontograma.Remove(Result);
            //                        //oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            //                        //oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            //                        //oOdontograma.v_ComponentFieldId = ctrl.Name;

            //                        //_ListaOdontograma.Add(oOdontograma);
            //                        //DataSource = _ListaOdontograma;
            //                    }
            //                }
            //            }
            //        }
            //        if (ctrl.HasChildren)
            //            BloquearDesbloquearLabels(ctrl, senderCtrl, "BLOQUEAR");
            //    }
            //    else if (Mode == "DESBLOQUEAR")
            //    {
            //        if (ctrl is Label)
            //        {
            //            if (senderCtrl.Tag != null)
            //            {
            //                string prefijo = senderCtrl.Tag.ToString().Substring(3, 2);
            //                if (ctrl.Tag != null)
            //                {
            //                    if (ctrl.Tag.ToString() == prefijo)
            //                    {
            //                        ctrl.Enabled = true;
            //                        ctrl.BackColor = System.Drawing.Color.White;

            //                        var Result = _ListaOdontograma.Find(p => p.v_ComponentFieldId == ctrl.Name);

            //                        if (Result != null && ctrl is Label)
            //                        {
            //                            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            //                            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            //                            oOdontograma.v_ComponentFieldId = ctrl.Name;

            //                            _ListaOdontograma.Add(oOdontograma);
            //                            DataSource = _ListaOdontograma;
            //                        }
            //                    }
            //                }
            //            }

            //        }
            //        if (ctrl.HasChildren)
            //            BloquearDesbloquearLabels(ctrl, senderCtrl, "DESBLOQUEAR");
            //    }
            //}
        }

        List<string> ListControlId(string pstrTag)
        {
            List<string> ListControlId = new List<string>();
            switch (pstrTag)
            {
                #region Primera Region
                case "18":
                    ListControlId.Add(Constants.D18_5);
                    ListControlId.Add(Constants.D18_4);
                    ListControlId.Add(Constants.D18_3);
                    ListControlId.Add(Constants.D18_2);
                    ListControlId.Add(Constants.D18_1);
                    break;
                case "17":
                    ListControlId.Add(Constants.D17_5);
                    ListControlId.Add(Constants.D17_4);
                    ListControlId.Add(Constants.D17_3);
                    ListControlId.Add(Constants.D17_2);
                    ListControlId.Add(Constants.D17_1);
                    break;
                case "16":
                    ListControlId.Add(Constants.D16_5);
                    ListControlId.Add(Constants.D16_4);
                    ListControlId.Add(Constants.D16_3);
                    ListControlId.Add(Constants.D16_2);
                    ListControlId.Add(Constants.D16_1);
                    break;
                case "15":
                    ListControlId.Add(Constants.D15_5);
                    ListControlId.Add(Constants.D15_4);
                    ListControlId.Add(Constants.D15_3);
                    ListControlId.Add(Constants.D15_2);
                    ListControlId.Add(Constants.D15_1);
                    break;
                case "14":
                    ListControlId.Add(Constants.D14_5);
                    ListControlId.Add(Constants.D14_4);
                    ListControlId.Add(Constants.D14_3);
                    ListControlId.Add(Constants.D14_2);
                    ListControlId.Add(Constants.D14_1);
                    break;
                case "13":
                    ListControlId.Add(Constants.D13_5);
                    ListControlId.Add(Constants.D13_4);
                    ListControlId.Add(Constants.D13_3);
                    ListControlId.Add(Constants.D13_2);
                    ListControlId.Add(Constants.D13_1);
                    break;
                case "12":
                    ListControlId.Add(Constants.D12_5);
                    ListControlId.Add(Constants.D12_4);
                    ListControlId.Add(Constants.D12_3);
                    ListControlId.Add(Constants.D12_2);
                    ListControlId.Add(Constants.D12_1);
                    break;
                case "11":
                    ListControlId.Add(Constants.D11_5);
                    ListControlId.Add(Constants.D11_4);
                    ListControlId.Add(Constants.D11_3);
                    ListControlId.Add(Constants.D11_2);
                    ListControlId.Add(Constants.D11_1);
                    break;
                #endregion
                #region Segunda Region
                case "21":
                    ListControlId.Add(Constants.D21_5);
                    ListControlId.Add(Constants.D21_4);
                    ListControlId.Add(Constants.D21_3);
                    ListControlId.Add(Constants.D21_2);
                    ListControlId.Add(Constants.D21_1);
                    break;
                case "22":
                    ListControlId.Add(Constants.D22_5);
                    ListControlId.Add(Constants.D22_4);
                    ListControlId.Add(Constants.D22_3);
                    ListControlId.Add(Constants.D22_2);
                    ListControlId.Add(Constants.D22_1);
                    break;
                case "23":
                    ListControlId.Add(Constants.D23_5);
                    ListControlId.Add(Constants.D23_4);
                    ListControlId.Add(Constants.D23_3);
                    ListControlId.Add(Constants.D23_2);
                    ListControlId.Add(Constants.D23_1);
                    break;
                case "24":
                    ListControlId.Add(Constants.D24_5);
                    ListControlId.Add(Constants.D24_4);
                    ListControlId.Add(Constants.D24_3);
                    ListControlId.Add(Constants.D24_2);
                    ListControlId.Add(Constants.D24_1);
                    break;
                case "25":
                    ListControlId.Add(Constants.D25_5);
                    ListControlId.Add(Constants.D25_4);
                    ListControlId.Add(Constants.D25_3);
                    ListControlId.Add(Constants.D25_2);
                    ListControlId.Add(Constants.D25_1);
                    break;
                case "26":
                    ListControlId.Add(Constants.D26_5);
                    ListControlId.Add(Constants.D26_4);
                    ListControlId.Add(Constants.D26_3);
                    ListControlId.Add(Constants.D26_2);
                    ListControlId.Add(Constants.D26_1);
                    break;
                case "27":
                    ListControlId.Add(Constants.D27_5);
                    ListControlId.Add(Constants.D27_4);
                    ListControlId.Add(Constants.D27_3);
                    ListControlId.Add(Constants.D27_2);
                    ListControlId.Add(Constants.D27_1);
                    break;
                case "28":
                    ListControlId.Add(Constants.D28_5);
                    ListControlId.Add(Constants.D28_4);
                    ListControlId.Add(Constants.D28_3);
                    ListControlId.Add(Constants.D28_2);
                    ListControlId.Add(Constants.D28_1);
                    break;
                #endregion
                #region Tercera Region
                case "31":
                    ListControlId.Add(Constants.D31_5);
                    ListControlId.Add(Constants.D31_4);
                    ListControlId.Add(Constants.D31_3);
                    ListControlId.Add(Constants.D31_2);
                    ListControlId.Add(Constants.D31_1);
                    break;
                case "32":
                    ListControlId.Add(Constants.D32_5);
                    ListControlId.Add(Constants.D32_4);
                    ListControlId.Add(Constants.D32_3);
                    ListControlId.Add(Constants.D32_2);
                    ListControlId.Add(Constants.D32_1);
                    break;
                case "33":
                    ListControlId.Add(Constants.D33_5);
                    ListControlId.Add(Constants.D33_4);
                    ListControlId.Add(Constants.D33_3);
                    ListControlId.Add(Constants.D33_2);
                    ListControlId.Add(Constants.D33_1);
                    break;
                case "34":
                    ListControlId.Add(Constants.D34_5);
                    ListControlId.Add(Constants.D34_4);
                    ListControlId.Add(Constants.D34_3);
                    ListControlId.Add(Constants.D34_2);
                    ListControlId.Add(Constants.D34_1);
                    break;
                case "35":
                    ListControlId.Add(Constants.D35_5);
                    ListControlId.Add(Constants.D35_4);
                    ListControlId.Add(Constants.D35_3);
                    ListControlId.Add(Constants.D35_2);
                    ListControlId.Add(Constants.D35_1);
                    break;
                case "36":
                    ListControlId.Add(Constants.D36_5);
                    ListControlId.Add(Constants.D36_4);
                    ListControlId.Add(Constants.D36_3);
                    ListControlId.Add(Constants.D36_2);
                    ListControlId.Add(Constants.D36_1);
                    break;
                case "37":
                    ListControlId.Add(Constants.D37_5);
                    ListControlId.Add(Constants.D37_4);
                    ListControlId.Add(Constants.D37_3);
                    ListControlId.Add(Constants.D37_2);
                    ListControlId.Add(Constants.D37_1);
                    break;
                case "38":
                    ListControlId.Add(Constants.D38_5);
                    ListControlId.Add(Constants.D38_4);
                    ListControlId.Add(Constants.D38_3);
                    ListControlId.Add(Constants.D38_2);
                    ListControlId.Add(Constants.D38_1);
                    break;
                #endregion
                #region Cuarta Region
                case "41":
                    ListControlId.Add(Constants.D41_5);
                    ListControlId.Add(Constants.D41_4);
                    ListControlId.Add(Constants.D41_3);
                    ListControlId.Add(Constants.D41_2);
                    ListControlId.Add(Constants.D41_1);
                    break;
                case "42":
                    ListControlId.Add(Constants.D42_5);
                    ListControlId.Add(Constants.D42_4);
                    ListControlId.Add(Constants.D42_3);
                    ListControlId.Add(Constants.D42_2);
                    ListControlId.Add(Constants.D42_1);
                    break;
                case "43":
                    ListControlId.Add(Constants.D43_5);
                    ListControlId.Add(Constants.D43_4);
                    ListControlId.Add(Constants.D43_3);
                    ListControlId.Add(Constants.D43_2);
                    ListControlId.Add(Constants.D43_1);
                    break;
                case "44":
                    ListControlId.Add(Constants.D44_5);
                    ListControlId.Add(Constants.D44_4);
                    ListControlId.Add(Constants.D44_3);
                    ListControlId.Add(Constants.D44_2);
                    ListControlId.Add(Constants.D44_1);
                    break;
                case "45":
                    ListControlId.Add(Constants.D45_5);
                    ListControlId.Add(Constants.D45_4);
                    ListControlId.Add(Constants.D45_3);
                    ListControlId.Add(Constants.D45_2);
                    ListControlId.Add(Constants.D45_1);
                    break;
                case "46":
                    ListControlId.Add(Constants.D46_5);
                    ListControlId.Add(Constants.D46_4);
                    ListControlId.Add(Constants.D46_3);
                    ListControlId.Add(Constants.D46_2);
                    ListControlId.Add(Constants.D46_1);
                    break;
                case "47":
                    ListControlId.Add(Constants.D47_5);
                    ListControlId.Add(Constants.D47_4);
                    ListControlId.Add(Constants.D47_3);
                    ListControlId.Add(Constants.D47_2);
                    ListControlId.Add(Constants.D47_1);
                    break;
                case "48":
                    ListControlId.Add(Constants.D48_5);
                    ListControlId.Add(Constants.D48_4);
                    ListControlId.Add(Constants.D48_3);
                    ListControlId.Add(Constants.D48_2);
                    ListControlId.Add(Constants.D48_1);
                    break;
                #endregion
                default:
                    break;
            }

            return ListControlId;
        }

        private void DesbloquearSeleccione(Control ctrlContainer, ComboBox senderCtrl)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is Label)
                {
                    if (senderCtrl.Tag != null)
                    {
                        //// Buscamos el control PictureImage para poder saber que imagen vamos a cambiar
                        //PictureBox ControlPictureBox;
                        //var ListControlPictureBox = this.Controls.Find(senderCtrl.Tag.ToString(), true);
                        //ControlPictureBox = ListControlPictureBox[0] as PictureBox;
                        //ControlPictureBox.Image = global::Sigesoft.Node.WinClient.UI.Resources.diente3;

                        string prefijo = senderCtrl.Tag.ToString().Substring(3, 2);
                        if (ctrl.Tag != null)
                        {
                            if (ctrl.Tag.ToString() == prefijo)
                            {
                                ctrl.Enabled = true;
                            }
                        }
                    }

                }
                if (ctrl.HasChildren)
                    DesbloquearSeleccione(ctrl, senderCtrl);
            }
        }

        private void chkPresenciaPlaca_CheckedChanged(object sender, EventArgs e)
        {
            //Llenar la lista para grabar el user control////////////////////////
            oOdontograma = new ServiceComponentFieldValuesList();


            //Llenar la lista para grabar el user control////////////////////////
            int Result = _ListaOdontograma.RemoveAll(p => p.v_ComponentFieldId == chkPresenciaPlaca.Name);
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = chkPresenciaPlaca.Name;

            oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;

            if (chkPresenciaPlaca.Checked)
            {
                oOdontograma.v_Value1 = "1";
            }
            else
            {
                oOdontograma.v_Value1 = "0";
            }

            _ListaOdontograma.Add(oOdontograma);
            DataSource = _ListaOdontograma;
            ///////////////////// FIN LLENADO DE LA LISTA////////////////////////
        }

        private void chkPresenciaRema_CheckedChanged(object sender, EventArgs e)
        {
            //Llenar la lista para grabar el user control////////////////////////
            oOdontograma = new ServiceComponentFieldValuesList();

            //Llenar la lista para grabar el user control////////////////////////
            int Result = _ListaOdontograma.RemoveAll(p => p.v_ComponentFieldId == chkPresenciaRema.Name);
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = chkPresenciaRema.Name;

            oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;

            if (chkPresenciaRema.Checked)
            {
                oOdontograma.v_Value1 = "1";
            }
            else
            {
                oOdontograma.v_Value1 = "0";
            }

            //_ListaValues.Add(oValues);
            //oOdontograma.Values = _ListaValues;
            _ListaOdontograma.Add(oOdontograma);
            DataSource = _ListaOdontograma;
            ///////////////////// FIN LLENADO DE LA LISTA////////////////////////
        }

        private void ddlStatusAptitudId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Llenar la lista para grabar el user control////////////////////////
            oOdontograma = new ServiceComponentFieldValuesList();

            //if (ddlStatusAptitudId.SelectedValue != "-1")
            //{
            //Llenar la lista para grabar el user control////////////////////////
            int Result = _ListaOdontograma.RemoveAll(p => p.v_ComponentFieldId == ddlStatusAptitudId.Name);
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = ddlStatusAptitudId.Name;

            oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;

            oOdontograma.v_Value1 = ddlStatusAptitudId.SelectedValue.ToString();
            _ListaOdontograma.Add(oOdontograma);
            DataSource = _ListaOdontograma;
            //}
        }

        private void txtOtros_TextChanged(object sender, EventArgs e)
        {
            //Llenar la lista para grabar el user control////////////////////////
            oOdontograma = new ServiceComponentFieldValuesList();
            //Llenar la lista para grabar el user control////////////////////////
            int Result = _ListaOdontograma.RemoveAll(p => p.v_ComponentFieldId == txtOtros.Name);
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = txtOtros.Name;
            _isChangueValueControl = true;

            oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
            oOdontograma.v_Value1 = txtOtros.Text.ToString();

            _ListaOdontograma.Add(oOdontograma);
            DataSource = _ListaOdontograma;
            ///////////////////// FIN LLENADO DE LA LISTA////////////////////////
        }

        private void txtPzaCar_TextChanged(object sender, EventArgs e)
        {
            //_ListaOdontograma = _ListaOdontograma.OrderBy(p => p.v_ComponentFieldId).ToList();
            //Llenar la lista para grabar el user control///////////////////////////
            oOdontograma = new ServiceComponentFieldValuesList();

            //Llenar la lista para grabar el user control///////////////////////////

            int Result = _ListaOdontograma.RemoveAll(p => p.v_ComponentFieldId == txtPzaCar.Name);
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = txtPzaCar.Name;
            oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
            oOdontograma.v_Value1 = txtPzaCar.Text.ToString();

            _ListaOdontograma.Add(oOdontograma);
            DataSource = _ListaOdontograma;

            //_ListaOdontograma = _ListaOdontograma.OrderBy(p => p.v_ComponentFieldId).ToList();
            ///////////////////// FIN LLENADO DE LA LISTA////////////////////////
        }

        private void txtPzaAus_TextChanged(object sender, EventArgs e)
        {
            //Llenar la lista para grabar el user control///////////////////////////
            oOdontograma = new ServiceComponentFieldValuesList();

            //Llenar la lista para grabar el user control///////////////////////////

            _ListaOdontograma.RemoveAll(p => p.v_ComponentFieldId == txtPzaAus.Name);
            oOdontograma.v_ServiceComponentFieldsId = _v_ServiceComponentFieldsId;
            oOdontograma.v_ServiceComponentId = _v_ServiceComponentId;
            oOdontograma.v_ComponentFieldId = txtPzaAus.Name;
            oOdontograma.v_ServiceComponentFieldValuesId = _v_ServiceComponentFieldValuesId;
            oOdontograma.v_Value1 = txtPzaAus.Text.ToString();

            _ListaOdontograma.Add(oOdontograma);
            DataSource = _ListaOdontograma;

            //var x = _ListaOdontograma.OrderBy(p => p.v_ComponentFieldId).ToList();
            ///////////////////// FIN LLENADO DE LA LISTA////////////////////////
        }

        private void SearchControlAndFill(Control ctrlContainer, List<ServiceComponentFieldValuesList> DataSource)
        {
            if (DataSource == null) return;

            //Limpiar Lista Caries y Ausentes
            listaCaries = new List<string>();
            listaAusentes = new List<string>();
            // Ordenar Lista Datasource
            var DataSourceOrdenado = DataSource.OrderBy(p => p.v_ComponentFieldId).ToList();
            // recorrer la lista que viene de la BD
            foreach (var item in DataSourceOrdenado)
            {
                var ctrl1 = this.Controls.Find(item.v_ComponentFieldId, true);
                if (ctrl1[0].GetType() == typeof(Label))
                {
                    // si el Id del control es igual al Id de la lista que viene de la BD 
                    if (ctrl1[0].Name == item.v_ComponentFieldId)
                    {
                        if (item.v_Value1 == ((int)(Common.ColorDiente.White)).ToString())
                        {
                            ctrl1[0].BackColor = System.Drawing.Color.White;
                        }
                        else if (item.v_Value1 == ((int)(Common.ColorDiente.Red)).ToString())
                        {
                            ctrl1[0].BackColor = System.Drawing.Color.Red;
                            //listaCaries.Add(item.v_Value2);
                            listaCaries.Add(ctrl1[0].Tag.ToString());
                        }
                        else if (item.v_Value1 == ((int)(Common.ColorDiente.Blue)).ToString())
                        {
                            ctrl1[0].BackColor = System.Drawing.Color.Blue;
                        }


                    }
                }
                // si los controles son combobox
                else if (ctrl1[0].GetType() == typeof(ComboBox))
                {
                    if (ctrl1[0].Name == "N002-ODO00000042")
                    {

                    }
                    if (ctrl1[0].Name == item.v_ComponentFieldId)
                    {
                        ((ComboBox)ctrl1[0]).SelectedValue = item.v_Value1;
                    }
                }

                else if (ctrl1[0].GetType() == typeof(CheckBox))
                {
                    if (ctrl1[0].Name == item.v_ComponentFieldId)
                    {
                        ((CheckBox)ctrl1[0]).Checked = item.v_Value1 == "1" ? true : false;
                    }
                }

                else if (ctrl1[0].GetType() == typeof(TextBox))
                {
                    if (ctrl1[0].Name == item.v_ComponentFieldId)
                    {
                        if (ctrl1[0].Name == "N002-ODO00000193")
                        {
                            if (item.v_Value1 != string.Empty)
                            {
                                string[] Dientes = item.v_Value1.Split(';');
                                foreach (string NroDiente in Dientes)
                                {
                                    //listaCaries.Add(NroDiente);
                                }
                            }

                            ((TextBox)ctrl1[0]).Text = item.v_Value1;
                        }
                        else if (ctrl1[0].Name == "N002-ODO00000194")
                        {
                            if (item.v_Value1 != string.Empty)
                            {
                                string[] Dientes = item.v_Value1.Split(';');
                                foreach (string NroDiente in Dientes)
                                {
                                    listaAusentes.Add(NroDiente);
                                }
                            }
                            ((TextBox)ctrl1[0]).Text = item.v_Value1;
                        }
                        else if (ctrl1[0].Name == "N002-ODO00000195")
                        {
                            ((TextBox)ctrl1[0]).Text = item.v_Value1;
                        }
                    }
                }
            }
        }

        #endregion

    }
}
