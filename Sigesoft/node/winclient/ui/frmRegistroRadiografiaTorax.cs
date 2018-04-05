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
    public partial class frmRegistroRadiografiaTorax : Form
    {
        private ServiceBL _serviceBL = new ServiceBL();
        private ServiceComponentFieldsList oServiceComponentFieldsList = new ServiceComponentFieldsList();
        private List<ServiceComponentFieldsList> _serviceComponentFieldsList = new List<ServiceComponentFieldsList>();

        private ServiceComponentFieldValuesList oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
        private List<ServiceComponentFieldValuesList> ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
        private string _personId;
        private string _serviceComponentId;
        public frmRegistroRadiografiaTorax(string pstrPersonId, string pstrServiceComponentId)
        {
            _personId = pstrPersonId;
            _serviceComponentId = pstrServiceComponentId;
            InitializeComponent();
        }

        private void frmRegistroRadiografiaTorax_Load(object sender, EventArgs e)
        {
            txtVertices.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.RX_VERTICES_ID);
            txtHilios.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.RX_HILOS_ID);
            txtSenosCardio.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.RX_SENOS_CARDIOFRENICOS_DESCRIPCION_ID);
            txtSiluetaCardia.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.RX_SILUETA_CARDIACA_DESCRIPCION_ID);
            txtPartesBlandas.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.RX_PARTES_BLANDAS_OSEAS_ID);
            txtCamposPulmonares.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.RX_CAMPOS_PULMONARES_ID);
            txtSenosDiag.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.RX_COSTO_ODIAFRAGMATICO_ID);
            txtMediastinos.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.RX_MEDIASTINOS_DESCRIPCION_ID);
            txtIndice.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.RX_INDICE_CARDIACO_DESCRIPCION_ID);
            txtConclusiones.Text = _serviceBL.DevolverValorDeCampo(_serviceComponentId, Constants.RX_CONCLUSIONES_RADIOGRAFICAS_DESCRIPCION_ID);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.RX_VERTICES_ID;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            //oServiceComponentFieldsList.ServiceComponentFieldValues[0].v_Value1 = txtVertices.Text;
            oServiceComponentFieldValuesList.v_Value1 = txtVertices.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList; 
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);


            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.RX_HILOS_ID;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            //oServiceComponentFieldsList.ServiceComponentFieldValues[0].v_Value1 = txtHilios.Text;
            oServiceComponentFieldValuesList.v_Value1 = txtHilios.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList; 
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);



            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.RX_SENOS_CARDIOFRENICOS_DESCRIPCION_ID;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            //oServiceComponentFieldsList.ServiceComponentFieldValues[0].v_Value1 = txtSenosCardio.Text;
            oServiceComponentFieldValuesList.v_Value1 = txtSenosCardio.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList; 
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);


            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.RX_SILUETA_CARDIACA_DESCRIPCION_ID;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            //oServiceComponentFieldsList.ServiceComponentFieldValues[0].v_Value1 = txtSiluetaCardia.Text;
            oServiceComponentFieldValuesList.v_Value1 = txtSiluetaCardia.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList; 
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);


            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.RX_PARTES_BLANDAS_OSEAS_ID;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            //oServiceComponentFieldsList.ServiceComponentFieldValues[0].v_Value1 = txtPartesBlandas.Text;
            oServiceComponentFieldValuesList.v_Value1 = txtPartesBlandas.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList; 
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);


            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.RX_CAMPOS_PULMONARES_ID;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            //oServiceComponentFieldsList.ServiceComponentFieldValues[0].v_Value1 = txtCamposPulmonares.Text;
            oServiceComponentFieldValuesList.v_Value1 = txtCamposPulmonares.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList; 
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);


            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.RX_COSTO_ODIAFRAGMATICO_ID;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            //oServiceComponentFieldsList.ServiceComponentFieldValues[0].v_Value1 = txtSenosDiag.Text;
            oServiceComponentFieldValuesList.v_Value1 = txtSenosDiag.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList; 
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);


            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.RX_MEDIASTINOS_DESCRIPCION_ID;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            //oServiceComponentFieldsList.ServiceComponentFieldValues[0].v_Value1 = txtMediastinos.Text;
            oServiceComponentFieldValuesList.v_Value1 = txtMediastinos.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList; 
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);


            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.RX_INDICE_CARDIACO_DESCRIPCION_ID;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            //oServiceComponentFieldsList.ServiceComponentFieldValues[0].v_Value1 = txtIndice.Text;
            oServiceComponentFieldValuesList.v_Value1 = txtIndice.Text;
            ListServiceComponentFieldValuesList.Add(oServiceComponentFieldValuesList);
            oServiceComponentFieldsList.ServiceComponentFieldValues = ListServiceComponentFieldValuesList; 
            _serviceComponentFieldsList.Add(oServiceComponentFieldsList);


            oServiceComponentFieldsList = new ServiceComponentFieldsList();
            ListServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            oServiceComponentFieldValuesList = new ServiceComponentFieldValuesList();
            oServiceComponentFieldsList.v_ComponentFieldsId = Constants.RX_CONCLUSIONES_RADIOGRAFICAS_DESCRIPCION_ID;
            oServiceComponentFieldsList.v_ServiceComponentId = _serviceComponentId;
            //oServiceComponentFieldsList.ServiceComponentFieldValues[0].v_Value1 = txtConclusiones.Text;
            oServiceComponentFieldValuesList.v_Value1 = txtConclusiones.Text;
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
