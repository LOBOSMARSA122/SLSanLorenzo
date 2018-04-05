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
    public partial class ucRXLumboSacra : UserControl
    {
        bool _isChangueValueControl = false;
        List<ServiceComponentFieldValuesList> _listOfAtencionAdulto1 = new List<ServiceComponentFieldValuesList>();
        ServiceComponentFieldValuesList _UserControlValores = null;

        private ServiceBL oServiceBL = new ServiceBL();


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
                SaveValueControlForInterfacingESO(Constants.LUMBOSACRA_1, txtRXLumbosacra01.Text);
                SaveValueControlForInterfacingESO(Constants.LUMBOSACRA_2, txtRXLumbosacra02.Text);
                SaveValueControlForInterfacingESO(Constants.LUMBOSACRA_3, txtRXLumbosacra03.Text);
                SaveValueControlForInterfacingESO(Constants.LUMBOSACRA_4, txtRXLumbosacra04.Text);
                SaveValueControlForInterfacingESO(Constants.LUMBOSACRA_5, txtRXLumbosacra05.Text);
                SaveValueControlForInterfacingESO(Constants.LUMBOSACRA_6, txtRXLumbosacra06.Text);
                SaveValueControlForInterfacingESO(Constants.LUMBOSACRA_7, txtRXLumbosacraConclusiones.Text);
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

        public ucRXLumboSacra()
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

                        }
                    }
                }

        private void ucRXLumboSacra_Load(object sender, EventArgs e)
        {
            txtRXLumbosacra01.Name = "N009-RXL00000001";
            txtRXLumbosacra02.Name = "N009-RXL00000002";
            txtRXLumbosacra03.Name = "N009-RXL00000003";
            txtRXLumbosacra04.Name = "N009-RXL00000004";
            txtRXLumbosacra05.Name = "N009-RXL00000005";
            txtRXLumbosacra06.Name = "N009-RXL00000006";
            txtRXLumbosacraConclusiones.Name = "N009-RXL00000007";

       var result = oServiceBL.historialComponente(PersonId,Constants.ANTROPOMETRIA_ID);
        }

        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    if (ctrl.Name.Contains("N009-RXL"))
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
            _UserControlValores.v_ComponentId = Constants.LUMBOSACRA_ID;

            _listOfAtencionAdulto1.Add(_UserControlValores);

            DataSource = _listOfAtencionAdulto1;

            #endregion
        }

        
    }
}
