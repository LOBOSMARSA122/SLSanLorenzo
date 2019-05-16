using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Operations
{
    public partial class frmContainerEso : Form
    {
        private string _serviceId;
        private string _personId;
        private string _componentIdByDefault;
        private string _action;
        private int _tipo;
        private string _serviceDate;
        private List<KeyValueDTO> _KeyValueDTO = null;
        ServiceBL _serviceBL = new ServiceBL();

        public frmContainerEso(string serviceId, string componentIdByDefault, string action, int tipo, string personId)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _componentIdByDefault = componentIdByDefault;
            _action = action;
            _tipo = tipo;
            _personId = personId;

        }

        private void frmContainerEso_Load(object sender, EventArgs e)
        {
            tcContEso.ShowToolTips = true;
            OperationResult objOperationResult = new OperationResult();
            _KeyValueDTO = BLL.Utils.GetServiceByPersonForCombo(ref objOperationResult, _personId);

            Utils.LoadDropDownList(ddlExamenesAnterioes, "Value1", "Id", _KeyValueDTO, DropDownListAction.Select);
            ddlExamenesAnterioes.SelectedItem = _KeyValueDTO.Find(x => x.Id == _serviceId);
        }

        private void createTabePage()
        {
            var ExistTab = tcContEso.TabPages[_serviceId];
            if (ExistTab == null)
            {
                Form frmChild = new Operations.FrmEsoV2(_serviceId, _componentIdByDefault, _action, Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, _tipo);
                AddNewTab(frmChild);
            }
            else
            {
                tcContEso.SelectedTab = ExistTab;
            }
        }

        private void AddNewTab(Form frm)
        {
            _serviceDate = _KeyValueDTO.Find(x => x.Id == _serviceId).Value2;
            TabPage tab = new TabPage(_serviceDate);
            
            tab.ToolTipText = _serviceId;
            tab.Name = _serviceId;
            var tag = tab.Name;
            frm.TopLevel = false;
            frm.Parent = tab;

            frm.Visible = true;
          
            tcContEso.TabPages.Add(tab);
            frm.FormBorderStyle = FormBorderStyle.None;
            tcContEso.SelectedTab = tab;

        }

        private void ddlExamenesAnterioes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExamenesAnterioes.SelectedValue.ToString() != "-1")
            {
                _serviceId = ddlExamenesAnterioes.SelectedValue.ToString();
                _tipo = _KeyValueDTO.Find(x => x.Id == _serviceId).IdI;
            }
            
            createTabePage();
        }

        private void frmContainerEso_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.ClientSession.i_SystemUserId = Globals.ClientSession.i_SystemUserCopyId;
            OperationResult objOperationResult = new OperationResult();
            ServiceBL objServiceBL = new ServiceBL();
            foreach (var item in _KeyValueDTO)
            {
                if (item.Id != "-1")
                {
                    var alert = objServiceBL.GetServiceComponentsCulminados(ref objOperationResult, item.Id);
                    serviceDto objserviceDto = new serviceDto();
                    objserviceDto = objServiceBL.GetService(ref objOperationResult, item.Id);
                    if (alert.Count == 0 || alert == null)
                    {
                        Sigesoft.Common.ConexionSigesoft conexionSigesoft = new ConexionSigesoft();
                        conexionSigesoft.opensigesoft();
                        var cadena =
                            "select i_MasterServiceId, i_AptitudeStatusId, v_FirstLastName+' '+v_SecondLastName+', '+v_FirstName, i_ServiceStatusId " +
                            "from service SR " +
                            "inner join person PP on SR.v_PersonId=PP.v_PersonId " +
                            "where v_ServiceId='" + item.Id + "'";
                        SqlCommand comando = new SqlCommand(cadena, connection: conexionSigesoft.conectarsigesoft);
                        SqlDataReader lector = comando.ExecuteReader();
                        string masterService = "0";
                        string aptirudService = "0";
                        string name = "";
                        string serviceStatus = "";
                        while (lector.Read())
                        {
                            masterService = lector.GetValue(0).ToString();
                            aptirudService = lector.GetValue(1).ToString();
                            name = lector.GetValue(2).ToString();
                            serviceStatus = lector.GetValue(3).ToString();
                        }

                        if (int.Parse(masterService) == (int)MasterService.AtxMedicaParticular || int.Parse(masterService) == (int)MasterService.AtxMedicaSeguros)
                        {
                            if (int.Parse(serviceStatus) != (int)ServiceStatus.Culminado)
                            {
                                MessageBox.Show("El servicio: " + item.Id + " del paciente: " + name + ", se ha concluido correctamente.", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                objserviceDto = objServiceBL.GetService(ref objOperationResult, item.Id);
                                objserviceDto.i_ServiceStatusId = (int)ServiceStatus.Culminado;
                                objserviceDto.v_Motive = "Culminado";
                                objserviceDto.i_AptitudeStatusId = (int)AptitudeStatus.Asistencial;
                                objServiceBL.UpdateService(ref objOperationResult, objserviceDto, Globals.ClientSession.GetAsList());
                            }
                            
                        }
                        else if (int.Parse(masterService) == (int)MasterService.Eso)
                        {
                            if (int.Parse(aptirudService) == (int)AptitudeStatus.SinAptitud )
                            {
                                MessageBox.Show("Todos los Examenes del servicio: "+item.Id+" del paciente: "+name+", se encuentran concluidos.\nEl estado de la Atención es: En espera de Aptitud.", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                objserviceDto = objServiceBL.GetService(ref objOperationResult, item.Id);
                                objserviceDto.i_ServiceStatusId = (int)ServiceStatus.EsperandoAptitud;
                                objserviceDto.v_Motive = "Esperando Aptitud";
                                objServiceBL.UpdateService(ref objOperationResult, objserviceDto, Globals.ClientSession.GetAsList());
                            }
                            else
                            {
                                if (int.Parse(serviceStatus) != (int)ServiceStatus.Culminado)
                                {
                                    MessageBox.Show("El servicio: " + item.Id + " del paciente: " + name + ", se ha concluido correctamente.", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    objserviceDto = objServiceBL.GetService(ref objOperationResult, item.Id);
                                    objserviceDto.i_ServiceStatusId = (int)ServiceStatus.Culminado;
                                    objserviceDto.v_Motive = "Culminado Automático";
                                    objServiceBL.UpdateService(ref objOperationResult, objserviceDto, Globals.ClientSession.GetAsList());
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
