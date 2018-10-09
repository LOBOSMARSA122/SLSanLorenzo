using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class CalendarDetail
    {
        public string v_ServiceId { get; set; }
        public string Pacient{ get; set; }
        public string EmpresaCliente { get; set; }
        public string EmpresaEmpleadora { get; set; }
        public string EmpresaTrabajo { get; set; }
        public string FechaService { get; set; }
        public string Protocol { get; set; }

        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }      
        public List<Components> Components { get; set; }
    }

    public class Components
    {
        public string ComponentId { get; set; }
        public string Component { get; set; }
        public int StatusComponentId { get; set; }
        public string StatusComponent { get; set; }
        public string User { get; set; }
    }
}
