using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{

    public class ComponentForLiquiCustom
    {
        public int? MasterServiceId { get; set; }
        public string MasterServiceName { get; set; }
        public double? Discount { get; set; }
        public decimal? ImporteDeducible { get; set; }
        public List<KindOfServices> ListKindOfServices { get; set; }
    }

    public class KindOfServices
    {
        public int? KindOfServiceId { get; set; }
        public string KindOfServiceName { get; set; }
        public List<CategoryForKOS> ListCategoryForKOS { get; set; }        
    }

    public class CategoryForKOS
    {
        public string CategoryName { get; set; }
        public int? KindOfServiceId { get; set; }
        public string KindOfServiceName { get; set; }
        public List<ServicesComponentForCategory> ListServicesComponentForKOS { get; set; }
    }
    public class ServicesComponentForCategory
    {
        public string ComponentName { get; set; }
        public string CategoryName { get; set; }
        public string ComponentId { get; set; }
        public string KindOfServiceName { get; set; }
        public int? KindOfServiceId { get; set; }
        public decimal? SaldoPaciente { get; set; }
        public decimal? SaldoAseguradora { get; set; }
        public float? Price { get; set; }
        public string CodigoSegus { get; set; }
    }
}
