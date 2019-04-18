using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Navigation;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmProcesosEso : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        public List<DataEso> _datos;
        public DataEso DataSource
        {
            set
            {
                FillObjDatos(value);
            }
        }
      
        public frmProcesosEso()
        {
            InitializeComponent();
        
        }

        private void frmProcesosEso_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void FillObjDatos(DataEso DataSource)
        {
            if (grdData.Rows.Count == 0)
                _datos = new List<DataEso>();
            else
            { 
                //Evaluar Petición entrante
                if (!PeticionAceptada(DataSource.categoria))
                {
                    MessageBox.Show(DataSource.categoria + " se está grabando. Para volver a grabar tiene que esperar que el proceso termine.", "VALIDACIÒN", MessageBoxButtons.OK);
                    return;
                }
            }

            //Insertar datos en el primer orden
            _datos.Insert(0, DataSource);

            //Cargar y Actualizar Grilla
            grdData.DataSource = _datos;
            grdData.Rows.Refresh(RefreshRow.ReloadData);

            //Poner en cola
            PonerEnCola();
        }

        private bool PeticionAceptada(string peticion)
        {
            var estadoCategoria = grdData.Rows.Where(c => c.Cells["Categoria"].Value.ToString() == peticion).Select(p => p.Cells["Estado"].Value.ToString()).ToArray();

            if (estadoCategoria.Length > 0)
                if (estadoCategoria[0] == "Grabando" || estadoCategoria[0] == "En cola") return false;

            return true;

        }

        private void PonerEnCola()
        {
            grdData.Rows[0].Cells["Estado"].Appearance.BackColor = Color.Goldenrod;
            grdData.Rows[0].Cells["Estado"].Value = "En cola";

        }

        private void frmProcesosEso_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = grdData.Rows.Where(c => c.Cells["Estado"].Value.ToString() != "Ok").ToArray().Length;
            if (result <= 0) return;
            MessageBox.Show("No puede cerrar el formulario hasta que termine de grabar todas las categorías", "VALIDACIÒN", MessageBoxButtons.OK);

            e.Cancel = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var item in grdData.Rows)
            {
                var estado = item.Cells["Estado"].Value.ToString();
                if (estado == "En cola")
                {
                    GrabadoAsync(item);
                }
            }
        }
       
        private void GrabadoAsync(UltraGridRow rw)
       {
            Task.Factory.StartNew(() => ProcesarDatos(rw), TaskCreationOptions.LongRunning).ContinueWith(t =>
            {
             

            });
        }

        private void ProcesarDatos(UltraGridRow rw)
        {

           Task<string>.Factory.StartNew(() =>
               {
                   return GrabarDatos(rw,1);
               }, TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning)
                                .ContinueWith(t =>
                                {
                                    rw.Cells["Estado"].Value = "Ok";
                                    rw.Cells["Detalle"].Value = t.Result;
                                    rw.Cells["Estado"].Appearance.BackColor = Color.Green;
                                  
                                    return "Ok";
                                });
               
        }

        private string GrabarDatos(UltraGridRow rw, int countIntentos)
        {
            rw.Cells["Estado"].Value = "Grabando";
            rw.Cells["Estado"].Appearance.BackColor = Color.Blue;
            rw.Cells["NroIntentos"].Value = countIntentos++ + "° intento.";
            
            var scId = rw.Cells["v_ServiceComponentId"].Value.ToString();
            var perId = rw.Cells["v_PersonId"].Value.ToString();
            var datos = _datos.Find(p => p.v_ServiceComponentId == scId).datos;

            try
            {
                OperationResult objOperationResult = new OperationResult();

                 _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                          datos,
                                                          Globals.ClientSession.GetAsList(),
                                                          perId,
                                                          scId);
                return "Se grabo correctamente";
            }
            catch (Exception e)
            {
                //rw.Cells["Estado"].Appearance.BackColor = Color.Red;
                rw.Cells["Detalle"].Value = Common.Utils.ExceptionFormatter(e);
                GrabarDatos(rw, countIntentos);
                return "Error: " + Common.Utils.ExceptionFormatter(e);
            }
        }

        private void grdData_MouseEnterElement(object sender, Infragistics.Win.UIElementEventArgs e)
        {
            if (!(e.Element is CellUIElement))
            {
                return;
            }

            UltraGridCell cell = e.Element.GetContext(typeof(UltraGridCell)) as UltraGridCell;
            if (cell.Row.Cells["Detalle"].Value == null)return;
            
            string detalle = cell.Row.Cells["Detalle"].Value.ToString();
            txtDetalle.Text = detalle;
        }

        private void grdData_MouseLeaveElement(object sender, Infragistics.Win.UIElementEventArgs e)
        {
         
        }

        private void grdData_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (grdData.Selected.Rows.Count != 0)
            {
                if(grdData.Selected.Rows[0].Cells["Detalle"].Value ==null)
                {
                    txtDetalle.Text = "";
                }
                else
                {
                    string detalle = grdData.Selected.Rows[0].Cells["Detalle"].Value.ToString();
                    txtDetalle.Text = detalle;    
                }
                
            }
            else
            {
                txtDetalle.Text = "";
            }
        }
    }
}
