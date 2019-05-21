using NetPdf;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAdditionalExamMant : Form
    {
        private string _serviceId = "";
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        private List<AdditionalExamUpdate> _DataSource = new List<AdditionalExamUpdate>();
        public frmAdditionalExamMant(string serviceId, List<AdditionalExamUpdate> data)
        {
            if (data != null)
            {
                _DataSource = data;
            }
            _serviceId = serviceId;
            InitializeComponent();
        }

        private void frmAdditionalExamMant_Load(object sender, EventArgs e)
        {
            grdDataAdditionalExam.DataSource = _DataSource;
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (_DataSource.Count > 0)
            {
                
                var data = _DataSource.FindAll(x => x.v_ComponentName.Contains(txtName.Text.ToUpper())).ToList();
                grdDataAdditionalExam.DataSource = data;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var Dialog = MessageBox.Show("¿ Seguro de eliminar las filas seleccionadas ?", "CONFIRMACIÓN",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Dialog == DialogResult.Yes)
                {
                    using (new LoadingClass.PleaseWait(this.Location, "Eliminando..."))
                    {
                        var newList = new List<AdditionalExamUpdate>();
                        foreach (var row in grdDataAdditionalExam.Selected.Rows)
                        {
                            var additionalExamId = row.Cells["v_AdditionalExamId"].Value.ToString();
                            new AdditionalExamBL().DeleteAdditionalExam(additionalExamId, Globals.ClientSession.i_SystemUserId);
                        }
                        BindingGrid();
                    }                   
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
            
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (grdDataAdditionalExam.Selected.Rows.Count > 0)
            {
                var componentId = grdDataAdditionalExam.Selected.Rows[0].Cells["v_ComponentId"].Value.ToString();
                var additionalExamId = grdDataAdditionalExam.Selected.Rows[0].Cells["v_AdditionalExamId"].Value.ToString();
                var frm = new frmUpdateAdditionalExam(componentId, additionalExamId);
                frm.ShowDialog();
                BindingGrid();

            }
            
        }

        private void BindingGrid()
        {
            var Data = new AdditionalExamBL().GetAdditionalExamForUpdateByServiceId(_serviceId, Globals.ClientSession.i_SystemUserId);
            grdDataAdditionalExam.DataSource = Data;
            grdDataAdditionalExam.DataBind();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (grdDataAdditionalExam.Rows.Count == 0)
            {
                MessageBox.Show("No hay exámenes para imprimir", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            var ruta = Common.Utils.GetApplicationConfigValue("rutaExamenesAdicionales").ToString();
            var rutaBasura = Common.Utils.GetApplicationConfigValue("rutaReportesBasura").ToString();
            string pathFile = "";
            string CMP = "";
            var openFile = false;
            using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
            {
                OperationResult objOperationResult = new OperationResult();

                var datosGrabo = new ServiceBL().DevolverDatosUsuarioFirma(Globals.ClientSession.i_SystemUserId);
                CMP = datosGrabo.CMP;
                pathFile = string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + "ORDEN-EX-MED-ADICI-" + datosGrabo.CMP));


                List<Categoria> AdditionalExam = new List<Categoria>();
                List<Categoria> DataSource = new List<Categoria>();
                List<string> ComponentList = new List<string>();
                var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_all(_serviceId, Globals.ClientSession.i_SystemUserId);

                foreach (var componenyId in ListadditExam)
                {
                    ComponentList.Add(componenyId.ComponentId);
                }

                foreach (var componentId in ComponentList)
                {
                    var ListServiceComponent = new ServiceBL().GetAllComponents(ref objOperationResult, (int)TipoBusqueda.ComponentId, componentId);

                    Categoria categoria = DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                    if (categoria != null)
                    {
                        List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                        componentDetail = ListServiceComponent[0].Componentes;
                        DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes.AddRange(componentDetail);
                    }
                    else
                    {
                        DataSource.AddRange(ListServiceComponent);
                    }
                }


                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                var DatosPaciente = new PacientBL().DevolverDatosPaciente(_serviceId);

                new PrintAdditionalExam().GenerateAdditionalexam(pathFile, MedicalCenter, DatosPaciente, datosGrabo, txtComentario.Text, DataSource, ListadditExam);
            }

            List<string> pdfList = new List<string>();
            pdfList.Add(pathFile);
            _mergeExPDF.FilesName = pdfList;
            _mergeExPDF.DestinationFile = string.Format("{0}.pdf", Path.Combine(rutaBasura, _serviceId + "-" + "ORDEN-EX-MED-ADICI-" + CMP));
            _mergeExPDF.Execute();
            _mergeExPDF.RunFile();
        }
    }
}
