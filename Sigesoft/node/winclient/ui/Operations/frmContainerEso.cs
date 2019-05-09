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
            }
            
            createTabePage();
        }

    }
}
