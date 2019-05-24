using System.Configuration;
using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;



namespace Sigesoft.Node.WinClient.UI
{
    public class ConnectionHelperSige
    {

        /// <summary>
        /// Obtiene la cadena de conexion del App.Config
        /// </summary>
        private static string ConnectionString
        {
            get
            {
                var csConf = ConfigurationManager.ConnectionStrings["SigConnectionString"];
                return csConf != null ? csConf.ConnectionString : string.Empty;
            }
        }

        /// <summary>
        /// Obtiene una conexión nativa para consultas rápidas a la bd de Contasol.
        /// </summary>
        public static IDbConnection GetConnection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }

    }
}
