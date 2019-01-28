using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Common
{
    class ConexionSigesoft
    {
        string cadena = @"Data Source = 192.168.1.179; Initial Catalog=SigesoftDesarrollo_2; Integrated Security=True";
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

                MessageBox.Show("Error al abrir la BD Sigesoft" + ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void closesigesoft()
        {
            conectarsigesoft.Close();
        }
    }
}
