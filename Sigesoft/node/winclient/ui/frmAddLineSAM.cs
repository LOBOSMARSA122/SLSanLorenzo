using Sigesoft.Common;
using Sigesoft.Node.Contasol.Integration;
using Sigesoft.Node.Contasol.Integration.Contasol.Models;
using System;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAddLineSAM : Form
    {
        private string _IdLinea;
        private string _strFilterExpression;
        private string _txtLineaCodigo;
        private string _txtLineaNombre;
        public frmAddLineSAM()
        {
            InitializeComponent();
        }

        private void btnLineaBuscar_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            var filters = new Queue<string>();
            if (!string.IsNullOrEmpty(txtLineaCodigo.Text)) filters.Enqueue("v_CodLinea==" + "\"" + txtLineaCodigo.Text + "\"");
            if (!string.IsNullOrEmpty(txtLineaNombre.Text)) filters.Enqueue("v_Nombre.Contains(\"" + txtLineaNombre.Text.Trim().ToUpper() + "\")");

            // Create the Filter Expression
            _strFilterExpression = filters.Count > 0 ? String.Join(" && ", filters) : null;
            BindGridLinea();
        }

        private void BindGridLinea()
        {
            var objData = GetDataLinea(_strFilterExpression);
            grdDataLinea.DataSource = objData;
            lblContadorFilasLinea.Text = string.Format("Se encontraron {0} registros.", this.grdDataLinea.Rows.Count());
        }

        private object GetDataLinea(string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = ListarLinea(ref objOperationResult, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        public List<lineaDto_2> ListarLinea(ref OperationResult objOperationResult, string pstrFilterExpression)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                    const string query = "select LL.v_CodLinea as 'v_CodLinea', LL.v_Nombre as 'v_Nombre', SU.v_UserName as 'v_UsuarioCreacion', LL.t_InsertaFecha as 't_InsertaFecha' , LL.v_IdLinea as 'v_IdLinea' from linea LL inner join systemuser SU on LL.i_InsertaIdUsuario = SU.i_SystemUserId where i_Eliminado = 0 group by v_CodLinea, v_Nombre, SU.v_UserName, t_InsertaFecha, v_IdLinea ";
                    objOperationResult.Success = 1;
                    return cnx.Query<lineaDto_2>(query).ToList();
                    //cnx.Query(query);

                }
            }
            catch (Exception)
            {
                objOperationResult.Success = 0;
                throw;
            }
            
        }

        private void btnLineaAgregar_Click(object sender, EventArgs e)
        {
            if (txtLineaCodigo.Text.Trim() == "" || txtLineaNombre.Text.Trim() == "")
            {
                if (txtLineaCodigo.Text.Trim() == "" && txtLineaNombre.Text.Trim() == "")
                {
                    MessageBox.Show("Ingrese nro de código y nombre:", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtLineaNombre.Text.Trim() == "")
                {
                    MessageBox.Show("Ingrese nombre:", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtLineaCodigo.Text.Trim() == "")
                {
                    MessageBox.Show("Ingrese nro de código", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
            }
            else
            {
                OperationResult objOperationResult = new OperationResult();
                string NewId = "(No generado)";
                try
                {
                    using (var cnx = ConnectionHelperSige.GetConnection)
                    {
                        List<string> _ClientSession = Globals.ClientSession.GetAsList();
                        int intNodeId = int.Parse(_ClientSession[0]);
                        NewId = Common.Utils.GetNewId(intNodeId, Sigesoft.Node.WinClient.BLL.Utils.GetNextSecuentialId(intNodeId, 316), "LN");
                        string LineId = NewId;
                        string codigo = txtLineaCodigo.Text;
                        string nombre = txtLineaNombre.Text;
                        string mes = "";
                        string dia = "";
                        if (DateTime.Now.Month.ToString().Length == 1){mes = "0" + DateTime.Now.Month.ToString();}
                        else{mes = DateTime.Now.Month.ToString();}

                        if (DateTime.Now.Day.ToString().Length == 1){dia = "0" + DateTime.Now.Day.ToString();}
                        else{dia = DateTime.Now.Day.ToString();}
                        string fecha = DateTime.Now.Year.ToString() + "-" + dia + "-" + mes + " 00:00:00.000";

                        int userId = int.Parse(_ClientSession[2]);
                        #region Usuarios
                        switch (userId)
                        {
                            case 11: userId = 1; break;

                        }
                        #endregion
                        
                        if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                        string query ="INSERT INTO [dbo].[linea](v_IdLinea,v_Periodo,v_CodLinea,v_Nombre,v_NroCuentaVenta,v_NroCuentaCompra,v_NroCuentaDConsumo,v_NroCuentaHConsumo,i_Eliminado,i_InsertaIdUsuario,t_InsertaFecha,i_ActualizaIdUsuario,t_ActualizaFecha,b_Foto,i_Header) " +
                            "VALUES ('"+LineId+"',2019,'"+codigo+"','"+nombre+"',1011101,1011101,'','',0,"+userId+",'"+fecha+"',NULL,NULL,NULL,NULL)";
                        objOperationResult.Success = 1;
                        cnx.Query(query);
                        MessageBox.Show("Se agregó correctamente:", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnLineaBuscar_Click(sender, e);

                    }
                }
                catch (Exception)
                {
                    objOperationResult.Success = 0;
                    throw;
                }
            }

            txtLineaCodigo.Text = "";
            txtLineaNombre.Text = "";
        }

        private void grdDataLinea_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            btnLineaEliminar.Enabled = true;
            btnLineaEditar.Enabled = true;
        }

        private void btnLineaEditar_Click(object sender, EventArgs e)
        {
            if (grdDataLinea.Selected.Rows.Count > 0)
            {
                _IdLinea = grdDataLinea.Selected.Rows[0].Cells["v_IdLinea"].Value.ToString();
                string CodLinea = grdDataLinea.Selected.Rows[0].Cells["v_CodLinea"].Value.ToString();
                string NombreLinea = grdDataLinea.Selected.Rows[0].Cells["v_Nombre"].Value.ToString();
                txtLineaCodigo.Text = CodLinea;
                txtLineaNombre.Text = NombreLinea;
                _txtLineaCodigo = CodLinea;
                _txtLineaNombre = NombreLinea;
                btnEditar_2.Visible = true;
                btnLineaAgregar.Visible = false;
            }
        }

        public void frmAddLineSAM_Load(object sender, EventArgs e)
        {
            
        }

        private void btnLineaEliminar_Click(object sender, EventArgs e)
        {
            if (grdDataLinea.Selected.Rows.Count > 0)
            {
                string IdLinea = grdDataLinea.Selected.Rows[0].Cells["v_IdLinea"].Value.ToString();
                OperationResult objOperationResult = new OperationResult();
                try
                {
                    
                    using (var cnx = ConnectionHelperSige.GetConnection)
                    {
                        if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                        string query = "update [dbo].[linea] set i_Eliminado = 1 where v_IdLinea='" + IdLinea + "'";
                        objOperationResult.Success = 1;
                        cnx.Query(query);
                        MessageBox.Show("Se eliminó correctamente:", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    objOperationResult.Success = 0;
                    throw;
                }

            }
            btnLineaBuscar_Click(sender, e);
        }

        private void btnEditar_2_Click(object sender, EventArgs e)
        {
            if (txtLineaCodigo.Text.Trim() == "" || txtLineaNombre.Text.Trim() == "")
            {
                if (txtLineaCodigo.Text.Trim() == "" && txtLineaNombre.Text.Trim() == "")
                {
                    MessageBox.Show("Ingrese nro de código y nombre:", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtLineaNombre.Text.Trim() == "")
                {
                    MessageBox.Show("Ingrese nombre:", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtLineaCodigo.Text.Trim() == "")
                {
                    MessageBox.Show("Ingrese nro de código", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {
                OperationResult objOperationResult = new OperationResult();
                try
                {
                    using (var cnx = ConnectionHelperSige.GetConnection)
                    {
                      
                        if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                        string queryGetCommentary = "select v_ComentaryUpdate from linea where v_IdLinea='" + _IdLinea + "'";

                        var GetCommentary = cnx.Query(queryGetCommentary).ToList();
                        string comentarioUpdate = GetCommentary[0].v_ComentaryUpdate;
                        comentarioUpdate += "<FechaActualiza:" + DateTime.Now.ToString() + "|UsuarioActualiza:" + Globals.ClientSession.v_UserName + "|";
                        bool IsUpdate = false;
                        if (_txtLineaCodigo != txtLineaCodigo.Text)
                        {
                            comentarioUpdate += "CodigoLinea:" + _txtLineaCodigo + "|";
                            IsUpdate = true;
                        }

                        if (_txtLineaNombre != txtLineaNombre.Text)
                        {
                            comentarioUpdate += "NombreLinea:" + _txtLineaNombre + "|";
                            IsUpdate = true;
                        }

                        if (!IsUpdate)
                        {
                            comentarioUpdate = "";
                        }

                        string query = "update [dbo].[linea] set v_CodLinea = '" + txtLineaCodigo.Text + "',v_Nombre='" +
                                       txtLineaNombre.Text + "',v_ComentaryUpdate='" + comentarioUpdate + "' where v_IdLinea='" + _IdLinea + "'";
                        objOperationResult.Success = 1;
                        cnx.Query(query);
                        MessageBox.Show("Se editó correctamente:", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnEditar_2.Visible = false;
                        btnLineaAgregar.Visible = true;
                        txtLineaCodigo.Text = "";
                        txtLineaNombre.Text = "";
                    }
                }
                catch (Exception)
                {
                    objOperationResult.Success = 0;
                    throw;
                }
            }
            btnLineaBuscar_Click(sender, e);
        }
    }
}
