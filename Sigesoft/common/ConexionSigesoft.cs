using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Common
{
    public class ConexionSigesoft
    {
        string cadena = @"Data Source = 192.168.0.101\OMEGANET_2019; Initial Catalog=SanLorenzo1102; User Id=sa; Password=123456";
        public SqlConnection conectarsigesoft = new SqlConnection();
        public ConexionSigesoft()
        {
            conectarsigesoft.ConnectionString = cadena;
        }
        public void opensigesoft()
        {
            try
            {
                conectarsigesoft.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show(@"Error al abrir la BD Sigesoft" + ex.Message, @"Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void closesigesoft()
        {
            conectarsigesoft.Close();
        }
    }
}
