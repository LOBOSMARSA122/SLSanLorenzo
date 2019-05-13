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

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmPreOffice : Form
    {
        List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();
        private ServiceBL _serviceBL = new ServiceBL();
        
        List<string> _componentIds;

        public frmPreOffice()
        {
            InitializeComponent();
        }

        private void frmPreOffice_Load(object sender, EventArgs e)
        {
            this.Show();
            // Obtener permisos de cada examen de un rol especifico
            var componentProfile = _serviceBL.GetRoleNodeComponentProfileByRoleNodeId(Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value);
            
            //*********************************************

            ddlComponentId.SelectedValueChanged -= ddlComponentId_SelectedValueChanged;

            OperationResult objOperationResult = new OperationResult();

            _componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);

            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

            groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));

           
            // Remover los componentes que no estan asignados al rol del usuario
            var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));
            //var dd = groupComponentList.FindAll(p => componentProfile.FindAll(o => o.v_ComponentId == p.Value2));

            Utils.LoadDropDownList(ddlComponentId, "Value1", "Value4", results, DropDownListAction.Select);
            
            ddlComponentId.SelectedValueChanged += ddlComponentId_SelectedValueChanged;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (uvComponent.Validate(true, false).IsValid)
                {
                    //this.Close();

                    frmOffice frm = new frmOffice(ddlComponentId.Text,int.Parse(ddlComponentId.SelectedValue.ToString()));
                    frm._componentIds = _componentIds;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }         
           
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void ddlComponentId_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlComponentId.SelectedIndex == 0)
            {
                btnIngresar.Enabled = false;
                return;
            }

            btnIngresar.Enabled = true;

            _componentIds = new List<string>();
            var eee = (KeyValueDTO)ddlComponentId.SelectedItem;

            if (eee.Value4.ToString() == "-1")
            {
                _componentIds.Add(eee.Value2);
            }
            else
            {
                _componentIds = _componentListTemp.FindAll(p => p.Value4 == eee.Value4)
                                                
                                                .Select(s => s.Value2)
                                                .OrderBy(p => p).ToList();
            }
           
        }

     
    }
}
