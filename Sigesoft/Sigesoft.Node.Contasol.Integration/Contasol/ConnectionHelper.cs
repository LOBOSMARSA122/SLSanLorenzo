using System.Configuration;
using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using SAMBHS.Common.DataModel;

namespace Sigesoft.Node.Contasol.Integration
{
    [SuppressMessage("ReSharper", "ArrangeAccessorOwnerBody")]
    public class ConnectionHelper
    {
        /// <summary>
        /// Obtiene la cadena de conexion del App.Config
        /// </summary>
        private static string ConnectionString
        {
            get
            {
                var csConf = ConfigurationManager.ConnectionStrings["ContasolConnectionString"];
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

        /// <summary>
        /// Obtiene el datacontext del contasol para operaciones que involucren mas lógica.
        /// </summary>
        public static SAMBHSEntitiesModelWin ContasolContext
        {
            get
            {
                var newconnstring = new EntityConnectionStringBuilder
                {
                    Metadata = @"res://*/DMMSQLWIN.csdl|res://*/DMMSQLWIN.ssdl|res://*/DMMSQLWIN.msl",
                    Provider = "System.Data.SqlClient",
                    ProviderConnectionString = ConnectionString
                };

                return new SAMBHSEntitiesModelWin(newconnstring.ToString());
            }
        }
    }
}
