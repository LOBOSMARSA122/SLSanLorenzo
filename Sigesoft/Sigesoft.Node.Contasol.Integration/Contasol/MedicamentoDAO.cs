using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Sigesoft.Node.Contasol.Integration.Contasol.Models;
using System.Windows.Forms;

namespace Sigesoft.Node.Contasol.Integration.Contasol
{
    public class MedicamentoDao
    {
        public static List<MedicamentoDto> ObtenerContasolMedicamentos()
        {
            try
            {
                using (var cnx = ConnectionHelper.GetConnection)
                {
                    if (cnx.State != ConnectionState.Open) cnx.Open();

                    const string query = "select \"v_IdProductoDetalle\" as 'IdProductoDetalle', \"v_CodInterno\" as 'CodInterno', " +
                                         "\"v_Descripcion\" as 'Nombre', " +
                                         "\"v_Presentacion\" as 'Presentacion', \"v_Concentracion\" as 'Concentracion', " +
                                         "\"v_Ubicacion\" as 'Ubicacion'" +
                                         "from producto p " +
                                         "join productodetalle pd on p.\"v_IdProducto\" = pd.\"v_IdProducto\" " +
                                         "where pd.\"i_Eliminado\" = 0";

                    return cnx.Query<MedicamentoDto>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void ObtenerLineasParaCombo(UltraComboEditor cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetConnection)
                {
                    if (cnx.State != ConnectionState.Open) cnx.Open();
                    cbo.DropDownStyle = DropDownStyle.DropDownList;
                    const string query = "select v_IdLinea as 'IdLinea', v_nombre as 'Nombre' from linea where i_Eliminado = 0";
                    cbo.DataSource = cnx.Query<LineaDto>(query).ToList();
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "IdLinea";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<LineaDto> ObtenerLineas()
        {
            try
            {
                using (var cnx = ConnectionHelper.GetConnection)
                {
                    if (cnx.State != ConnectionState.Open) cnx.Open();
                    const string query = "select v_IdLinea as 'IdLinea', v_nombre as 'Nombre' from linea where i_Eliminado = 0";
                    return cnx.Query<LineaDto>(query).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ObtenerLineasParaCombo(ComboBox cbo)
        {
            try
            {
                cbo.DropDownStyle = ComboBoxStyle.DropDownList;
                cbo.DataSource = ObtenerLineas();
                cbo.DisplayMember = "Nombre";
                cbo.ValueMember = "IdLinea";
                cbo.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void ObtenerUmParaCombo(UltraComboEditor cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetConnection)
                {
                    if (cnx.State != ConnectionState.Open) cnx.Open();
                    cbo.DropDownStyle = DropDownStyle.DropDownList;
                    const string query = "select i_ItemId as 'IdUnidadMedida', v_Value1 as 'Nombre' from datahierarchy where i_GroupId = 17 and i_IsDeleted = 0";
                    cbo.DataSource = cnx.Query<UnidadMedidaDto>(query).ToList();
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "IdUnidadMedida";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal static object ObtenerLineasWhere(string p)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetConnection)
                {
                    if (cnx.State != ConnectionState.Open) cnx.Open();
                    string query = @"select v_IdLinea as 'IdLinea', v_nombre as 'Nombre' from linea where i_Eliminado = 0 and v_nombre like'%"+p+"%'";
                    return cnx.Query<LineaDto>(query).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
