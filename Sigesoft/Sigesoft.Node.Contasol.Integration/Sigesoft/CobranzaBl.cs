using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SAMBHS.Almacen.BL;
using SAMBHS.Common.BE;
using SAMBHS.Common.DataModel;
using Sigesoft.Common;
using Sigesoft.Node.Contasol.Integration.Contasol.Models;
using ConnectionState = System.Data.ConnectionState;

namespace Sigesoft.Node.Contasol.Integration.Sigesoft
{
    public class CobranzaBl
    {
        public decimal MontoCobrado(string nroFactura)
        {
            try
            {

                return 0;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}
