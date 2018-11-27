using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BLL
{
   public class AplicationHierarchyBL
    {
       public List<applicationhierarchyDto> ObtenerFormularios()
       {
           try
           {
               using (SigesoftEntitiesModel ctx  =new SigesoftEntitiesModel())
               {
                  //var query =  from a in ctx.applicationhierarchy
                  //             where a.
               }

               return null;
           }
           catch (Exception ex)
           {               
               throw;
           }
       }
    }
}
