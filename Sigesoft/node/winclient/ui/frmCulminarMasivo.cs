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
using NetPdf;
using Infragistics.Win.UltraWinGrid;
using System.Diagnostics;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmCulminarMasivo : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        OperationResult _objOperationResult = new OperationResult();
        List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();

        public int? CategoriaId = null;
        public int? TecnicoId = null;
        public int? MedicoId = null;

        public frmCulminarMasivo()
        {
            InitializeComponent();
        }

        private void frmCulminarMasivo_Load(object sender, EventArgs e)
        {

            // Obtener permisos de cada examen de un rol especifico
            var componentProfile = _serviceBL.GetRoleNodeComponentProfileByRoleNodeId(Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value);

            _componentListTemp = BLL.Utils.GetAllComponents(ref _objOperationResult);

            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

            groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));
            // Remover los componentes que no estan asignados al rol del usuario
            var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));

            Utils.LoadDropDownList(ddlConsultorio, "Value1", "Value4", results, DropDownListAction.Select);
            Utils.LoadDropDownList(cboTecnico, "Value1", "Id", BLL.Utils.GetProfessional(ref _objOperationResult, ""), DropDownListAction.Select);
            Utils.LoadDropDownList(cboMedico, "Value1", "Id", BLL.Utils.GetProfessional(ref _objOperationResult, ""), DropDownListAction.Select);
            //Utils.LoadDropDownList(cbComponente, "Value1", "Id", null, DropDownListAction.Select);

        }

        private void ddlConsultorio_SelectedIndexChanged(object sender, EventArgs e)
        {
           
          
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            CategoriaId = int.Parse(ddlConsultorio.SelectedValue.ToString());
            TecnicoId = int.Parse(cboTecnico.SelectedValue.ToString());
            MedicoId = int.Parse(cboMedico.SelectedValue.ToString());

            this.DialogResult = DialogResult.OK;
        
        }
    }
}
