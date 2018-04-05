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
using System.IO;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmRolCuota : Form
    {
        List<RolCuotaDetalleList> _TempRolCuotaDetalleList;

        string _Mode;
        string _RolCuotaId;
        public frmRolCuota(string pstrMode, string pstrRolCuotaId)
        {
            InitializeComponent();
            _Mode = pstrMode;
            _RolCuotaId = pstrRolCuotaId;

        }

        private void frmRolCuota_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            RolCuotaBL oRolCuotaBL = new RolCuotaBL();
            RolCuotaDetalleBL oRolCuotaDetalleBL = new RolCuotaDetalleBL();

            rolcuotaDto orolcuotaDto = new rolcuotaDto();
            
            Utils.LoadDropDownList(cbRolVenta, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 121, null), DropDownListAction.Select);
           
            if (_Mode=="New")
            {
                
            }
            else if(_Mode =="Edit")
            {
              orolcuotaDto=  oRolCuotaBL.GetRolCuota(ref objOperationResult, _RolCuotaId);

              cbRolVenta.SelectedValue = orolcuotaDto.i_RolId.ToString();


              _TempRolCuotaDetalleList = oRolCuotaDetalleBL.GetRolCuotaDetallePagedAndFiltered(ref objOperationResult, 0, null, "", "v_RolCuotaId==" + "\"" + _RolCuotaId + "\"");
             grdData.DataSource = _TempRolCuotaDetalleList;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", _TempRolCuotaDetalleList.Count());

            }
         
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {


        }

        private void btnRemover_Click(object sender, EventArgs e)
        {

        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            bool Imported = true;
            int ErrorCounter = 0;
            StringBuilder sbMensaje = new StringBuilder();

            if (_TempRolCuotaDetalleList != null)
            {
                if (MessageBox.Show("Ya existe una lista ; ¿Desea reemplazarla?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }

            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Image Files (*.xls;*.xlsx)|*.xls;*.xlsx";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _TempRolCuotaDetalleList = new List<RolCuotaDetalleList>();
                var Ext = Path.GetExtension(openFileDialog1.FileName).ToUpper();

                if (Ext == ".XLSX" || Ext == ".XLS")
                {
                    Infragistics.Documents.Excel.Workbook workbook1 = Infragistics.Documents.Excel.Workbook.Load(openFileDialog1.FileName);

                    Infragistics.Documents.Excel.Worksheet worksheet1 = workbook1.Worksheets["PLANTILLA"];


                    RolCuotaDetalleList TemRolCuotaDetalleList;

                    int i = 4;
                    int ii = 4;

                    while (worksheet1.Rows[ii].Cells[0].Value != null)
                    {
                        if (worksheet1.Rows[ii].Cells[0].Value == null || worksheet1.Rows[ii].Cells[1].Value == null || worksheet1.Rows[ii].Cells[2].Value == null || worksheet1.Rows[ii].Cells[3].Value == null)
                        {
                            for (int y = 0; y <= 2; y++)
                            {
                                if (worksheet1.Rows[ii].Cells[y].Value == null)
                                {
                                    Imported = false;
                                    sbMensaje.Append("Registro número : ");
                                    sbMensaje.Append(worksheet1.Rows[ii].Cells[0].Value);
                                    sbMensaje.Append(". El campo " + worksheet1.Rows[3].Cells[y].Value.ToString() + " no puede estar vacio");
                                    sbMensaje.Append("\n");
                                }
                            }
                        }
                        ii++;
                    }

                    if (Imported == false)
                    {
                        MessageBox.Show(sbMensaje.ToString(), "Corregir registros en blanco", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    while (worksheet1.Rows[i].Cells[0].Value != null)
                    {
                        TemRolCuotaDetalleList = new RolCuotaDetalleList();
                        if (worksheet1.Rows[i].Cells[0].Value != null)
                        {
                            TemRolCuotaDetalleList.i_Correlative = int.Parse(worksheet1.Rows[i].Cells[0].Value.ToString());
                            Imported = true;
                        }

                        //ID PRODUCTO
                        if (worksheet1.Rows[i].Cells[1].Value != null)
                        {
                            TemRolCuotaDetalleList.v_IdProducto = worksheet1.Rows[i].Cells[1].Value.ToString();
                            Imported = true;
                        }
                        else
                        {
                            ErrorCounter++;
                            Imported = false;
                            sbMensaje.Append("Registro número : ");
                            sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                            sbMensaje.Append(". El campo IDPRODUCTO es inválido");
                            sbMensaje.Append("\n");
                            i++;
                            continue;
                        }

                        //NOMBRE PRODUCTO
                        if (worksheet1.Rows[i].Cells[2].Value != null)
                        {
                            TemRolCuotaDetalleList.v_ProductoNombre = worksheet1.Rows[i].Cells[2].Value.ToString();
                            Imported = true;
                        }
                        else
                        {
                            ErrorCounter++;
                            Imported = false;
                            sbMensaje.Append("Registro número : ");
                            sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                            sbMensaje.Append(". El campo NOMBRE PRODUCTO es inválido");
                            sbMensaje.Append("\n");
                            i++;
                            continue;
                        }
                        //CUOTA MENSUAL
                        if (worksheet1.Rows[i].Cells[3].Value != null)
                        {
                            TemRolCuotaDetalleList.i_Cuota = int.Parse(worksheet1.Rows[i].Cells[3].Value.ToString());
                            Imported = true;
                        }
                        else
                        {
                            ErrorCounter++;
                            Imported = false;
                            sbMensaje.Append("Registro número : ");
                            sbMensaje.Append(worksheet1.Rows[i].Cells[0].Value);
                            sbMensaje.Append(". El campo CUOTA MENSUAL es inválido");
                            sbMensaje.Append("\n");
                            i++;
                            continue;
                        }


                        _TempRolCuotaDetalleList.Add(TemRolCuotaDetalleList);

                        var Result = _TempRolCuotaDetalleList.FindAll(p => p.v_IdProducto == TemRolCuotaDetalleList.v_IdProducto );
                        if (Result.Count > 1)
                        {
                            MessageBox.Show("El correlativo " + Result[0].i_Correlative + " tiene el mismo IDPRODUCTO que el correlativo " + Result[1].i_Correlative + " .Revise el Excel y corriga la duplicidad", "Error al cargar Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        i++;

                    }


                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", _TempRolCuotaDetalleList.Count());

                    if (ErrorCounter > 0)
                    {
                        _TempRolCuotaDetalleList = new List<RolCuotaDetalleList>();
                        grdData.DataSource = new List<RolCuotaDetalleList>();
                        MessageBox.Show(sbMensaje.ToString(), "Registros no importados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        sbMensaje = null;
                    }
                    else if (ErrorCounter == 0)
                    {
                        grdData.DataSource = _TempRolCuotaDetalleList;
                        MessageBox.Show("Se importaron " + _TempRolCuotaDetalleList.Count() + " registros.", "Importación correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _TempRolCuotaDetalleList = new List<RolCuotaDetalleList>();
                        grdData.DataSource = new List<RolCuotaDetalleList>();
                        MessageBox.Show(sbMensaje.ToString(), "Registros no importados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        sbMensaje = null;
                    }  
                }

            }
            else
            {
                grdData.DataSource = new List<PacientList>();
                MessageBox.Show("Seleccione un formato correcto (.xlsx)", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            RolCuotaBL oRolCuotaBL = new RolCuotaBL();
            RolCuotaDetalleBL oRolCuotaDetalleBL = new RolCuotaDetalleBL();

            rolcuotaDto orolcuotaDto = new rolcuotaDto();

            if (uvCuota.Validate(true, false).IsValid)
            {

                if (_TempRolCuotaDetalleList == null)
                {
                    MessageBox.Show("No se permite mientras la lista esté vacía", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_TempRolCuotaDetalleList.Count == 0)
                {
                     MessageBox.Show("No se permite mientras la lista esté vacía", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                //Cabecera
                orolcuotaDto.i_RolId = int.Parse(cbRolVenta.SelectedValue.ToString());
               string RolCuotaId=  oRolCuotaBL.AddRolCuota(ref objOperationResult, orolcuotaDto, Globals.ClientSession.GetAsList());

                //Detalle

                foreach (var item in _TempRolCuotaDetalleList)
                {
                    rolcuotadetalleDto orolcuotadetalleDto = new rolcuotadetalleDto();

                    orolcuotadetalleDto.v_IdProducto = item.v_IdProducto;
                    orolcuotadetalleDto.v_ProductoNombre = item.v_ProductoNombre;
                    orolcuotadetalleDto.v_RolCuotaId = RolCuotaId;
                    orolcuotadetalleDto.i_Cuota = item.i_Cuota;
                    oRolCuotaDetalleBL.AddRolCuotaDetalle(ref objOperationResult, orolcuotadetalleDto, Globals.ClientSession.GetAsList());
                }

                if (objOperationResult.Success == 1)
                {
                    MessageBox.Show("Se grabó correctamente", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else// Operación con error
                {
                    MessageBox.Show(objOperationResult.ErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

           
        }

        private void grdData_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {

        }
    }
}
