using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Sigesoft.Server.WebClientAdmin.UI
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoServerCaching();
            context.Response.ContentType = "application/x-javascript";
            context.Response.Write("//");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}