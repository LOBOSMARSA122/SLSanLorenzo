using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Common
{
    public class ConexionSambhs
    {
        string cadena;
        public SqlConnection conectarSambhs = new SqlConnection();
        public ConexionSambhs()
        {
            cadena = Common.Utils.GetApplicationConfigValue("ConexionSambhs");
            conectarSambhs.ConnectionString = cadena;
        }
        public void openSambhs()
        {
            try
            {
                conectarSambhs.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show(@"Error al abrir la BD Sambhs" + ex.Message, @"Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void closeSambhs()
        {
            conectarSambhs.Close();
        }
    }
}
