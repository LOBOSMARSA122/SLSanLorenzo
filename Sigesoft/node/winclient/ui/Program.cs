using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
            {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
            //Application.Run(new frmService());
            //Application.Run(new Reportes());
            //Application.Run(new frmLaboratorio());
            //Application.Run(new Reports.frmReportsAseguradoras());
            //Application.Run(new Operations.frmEso();
            //Application.Run(new Reports.frmReportsAseguradoras());
            //Application.Run(new Reports.frmDiagnosticsByAgeGroup());
            //Application.Run(new Reports.frmDiagnosticsByOfficeDetail());
        }
    }
}
