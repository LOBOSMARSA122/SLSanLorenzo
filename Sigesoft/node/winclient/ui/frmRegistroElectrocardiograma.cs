using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmRegistroElectrocardiograma : Form
    {
        private ServiceBL _serviceBL = new ServiceBL();
        private ServiceComponentFieldsList oServiceComponentFieldsList = new ServiceComponentFieldsList();
        private List<ServiceComponentFieldsList> _serviceComponentFieldsList = new List<ServiceComponentFieldsList>();
        
        private ServiceComponentFieldValuesList oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
        private List<ServiceComponentFieldValuesList> ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();

        private string _personId;
        private string _serviceComponentId;

        public frmRegistroElectrocardiograma(string pstrPersonId, string pstrServiceComponentId)
        {
            _personId = pstrPersonId;
            _serviceComponentId = pstrServiceComponentId;

            InitializeComponent();
        }

        private void frmRegistroElectrocardiograma_Load(object sender, EventArgs e)
        {
            txtRitmo.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.EVA_CARDIOLOGICA_RITMO);
            txtEje.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.EVA_CARDIOLOGICA_EJE);
            txtFC.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.EVA_CARDIOLOGICA_FC);
            txtPR.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.EVA_CARDIOLOGICA_PR);
            txtQrs.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.EVA_CARDIOLOGICA_QRS);
            txtQT.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.EVA_CARDIOLOGICA_QT);
            txtOndaQ.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.EVA_CARDIOLOGICA_ONDA_Q);
            txtOndaP.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.EVA_CARDIOLOGICA_ONDA_P);
            txtOndaR.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.EVA_CARDIOLOGICA_ONDA_R);
            txtOndaS.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.EVA_CARDIOLOGICA_ONDA_S);
            txtOndaT.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.EVA_CARDIOLOGICA_ONDA_T);
            txtOndaU.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.EVA_CARDIOLOGICA_ONDA_U);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.EVA_CARDIOLOGICA_RITMO;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            oServiceComponentFieldValuesList.v_Value1 = txtRitmo.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList; 
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);


            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.EVA_CARDIOLOGICA_EJE;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            oServiceComponentFieldValuesList.v_Value1 = txtEje.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList;
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);

            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.EVA_CARDIOLOGICA_FC;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            oServiceComponentFieldValuesList.v_Value1 = txtFC.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList;
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);

            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.EVA_CARDIOLOGICA_PR;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            oServiceComponentFieldValuesList.v_Value1 = txtPR.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList;
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);

            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.EVA_CARDIOLOGICA_QRS;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            oServiceComponentFieldValuesList.v_Value1 = txtQrs.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList;
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);

            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.EVA_CARDIOLOGICA_QT;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            oServiceComponentFieldValuesList.v_Value1 = txtQT.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList;
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);

            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.EVA_CARDIOLOGICA_ONDA_Q;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            oServiceComponentFieldValuesList.v_Value1 = txtOndaQ.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList;
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);

            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.EVA_CARDIOLOGICA_ONDA_P;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            oServiceComponentFieldValuesList.v_Value1 = txtOndaP.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList;
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);

            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.EVA_CARDIOLOGICA_ONDA_R;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            oServiceComponentFieldValuesList.v_Value1 = txtOndaR.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList;
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);

            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.EVA_CARDIOLOGICA_ONDA_S;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            oServiceComponentFieldValuesList.v_Value1 = txtOndaS.Text;
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);

            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.EVA_CARDIOLOGICA_ONDA_T;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            oServiceComponentFieldValuesList.v_Value1 = txtOndaT.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList;
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);

            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.EVA_CARDIOLOGICA_ONDA_U;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            oServiceComponentFieldValuesList.v_Value1 = txtOndaU.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList;
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);

          
            var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                          _serviceComponentFieldsList,
                                                          Globals.ClientSession.GetAsList(),
                                                          _personId,
                                                          _serviceComponentId);

            if (result)
            {
              MessageBox.Show("Se grabó correctamente", "SISTEMAS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
        }
    }
}
