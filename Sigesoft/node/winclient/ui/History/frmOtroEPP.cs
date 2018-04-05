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

namespace Sigesoft.Node.WinClient.UI.History
{
    public partial class frmOtroEPP : Form
    {
        public frmOtroEPP()
        {
            InitializeComponent();
        }

        public int ParameterId { get; set; }
        public string EppName { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SystemParameterBL _objProxy = new SystemParameterBL();
            OperationResult objOperationResult = new OperationResult();

            systemparameterDto objEntity = new systemparameterDto();

            // Populate the entity
            objEntity.i_GroupId = 146;

            objEntity.i_ParameterId = _objProxy.GetSystemParameterMaxParameterId(146);
            ParameterId = objEntity.i_ParameterId;
            objEntity.v_Value1 = txtPeligro.Text;
            EppName = txtPeligro.Text;
            objEntity.i_Sort = null;
            objEntity.v_Value2 = "";
            objEntity.v_Field = "";
            objEntity.i_ParentParameterId = 16;


            // Save the data                  
            _objProxy.AddSystemParameter(ref objOperationResult, objEntity, Globals.ClientSession.GetAsList());

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
