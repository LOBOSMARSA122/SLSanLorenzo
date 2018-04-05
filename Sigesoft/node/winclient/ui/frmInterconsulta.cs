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
using System.IO;
using System.Drawing.Imaging;
using Infragistics.Win;


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmInterconsulta : Form
    {
        string _IdServicio;
        DxCie10 o;
        List<DxCie10> Lista;
        DiagnosticRepositoryList check = new DiagnosticRepositoryList();
        public frmInterconsulta(string pstrIdServicio)
        {
            _IdServicio = pstrIdServicio;
            InitializeComponent();
        }

        private void frmInterconsulta_Load(object sender, EventArgs e)
        {
            ServiceBL o = new ServiceBL();
             OperationResult objOperationResult = new OperationResult();
           ((ListBox)chklistDx).DataSource=  o.GetDisgnosticsByServiceId(ref objOperationResult, _IdServicio);
           ((ListBox)chklistDx).DisplayMember = "v_Dx_CIE10";
           ((ListBox)chklistDx).ValueMember = "v_DiseasesId";
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Lista = new List<DxCie10>();
            foreach (DiagnosticRepositoryList item in chklistDx.CheckedItems)
            {
                o = new DxCie10();
                o.Dx = item.v_DiseasesName;
                o.Cie10 = item.v_Cie10;
                Lista.Add(o);
            }

            var frm = new Reports.frmInterconsulta(_IdServicio, txtAltitudLabor.Text, txtEspConsultar.Text, txtRiegoLabor.Text, txtSolicita.Text, Lista, txtObservaciones.Text);
            frm.ShowDialog();
        }
    }
}
