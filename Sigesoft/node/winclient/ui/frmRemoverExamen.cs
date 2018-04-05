using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid.DocumentExport;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmRemoverExamen : Form
    {
        int _categoriaId;
        string _servicioId;
        ServiceBL oServiceBL = new ServiceBL();
        OperationResult objOperationResult = new OperationResult();
        public string v_ServiceComponentId;
        public frmRemoverExamen(int pintCategoriaId, string pstrServiceId)
        {
            _categoriaId = pintCategoriaId;
            _servicioId = pstrServiceId;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            v_ServiceComponentId = cboExamen.SelectedValue.ToString();

            this.DialogResult = DialogResult.OK;
        }

        private void frmRemoverExamen_Load(object sender, EventArgs e)
        {
           var oServiceComponentList = oServiceBL.GetServiceComponentByCategoryId(ref objOperationResult, _categoriaId, _servicioId);
           List<KeyValueDTO> lista = new List<KeyValueDTO>();
           KeyValueDTO oKeyValueDTO;
           foreach (var item in oServiceComponentList)
           {
               oKeyValueDTO = new KeyValueDTO();
               oKeyValueDTO.Id = item.v_ServiceComponentId;
               oKeyValueDTO.Value1 = item.v_ComponentName;
               lista.Add(oKeyValueDTO);
           }
           Utils.LoadDropDownList(cboExamen, "Value1", "Id", lista, DropDownListAction.Select);
           
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
