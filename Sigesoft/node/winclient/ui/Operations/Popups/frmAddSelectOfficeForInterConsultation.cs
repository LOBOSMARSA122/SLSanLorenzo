using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using Infragistics.Win.UltraWinEditors;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmAddSelectOfficeForInterConsultation : Form
    {
        public string _officeId;
        public string _officeName;

        public frmAddSelectOfficeForInterConsultation()
        {
            InitializeComponent();
        }

        private void frmAddSelectOfficeForInterConsultation_Load(object sender, EventArgs e)
        {
            BindComboConsultorio();
        }

        private void BindComboConsultorio()
        {
            OperationResult objOperationResult = new OperationResult();

            var componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);

            var xxx = componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

            groupComponentList.AddRange(componentListTemp.ToList().FindAll(p => p.Value4 == -1));

            // add the sequence number on the fly
            groupComponentList = groupComponentList
                    .Select((x, index) => new KeyValueDTO
                    {
                        Id = (index + 1).ToString(),
                        Value1 = x.Value1,
                        Value2 = x.Value2,
                        Value3 = x.Value3,
                        Value4 = x.Value4
                    }).ToList();      
       
            rblConsultorios.DataSource = groupComponentList;
            rblConsultorios.ValueMember = "Id";
            rblConsultorios.DisplayMember = "Value1";          
           

        }

        private void SelectOptionAndWindowClose()
        {
            var checkedItem = (KeyValueDTO)rblConsultorios.CheckedItem.ListObject;
            _officeId = checkedItem.Value2;
            _officeName = checkedItem.Value1;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectOptionAndWindowClose();
        }

        private void rblConsultorios_DoubleClick(object sender, EventArgs e)
        {

            SelectOptionAndWindowClose();

        }
    }
}
