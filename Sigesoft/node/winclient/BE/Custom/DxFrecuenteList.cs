using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class DxFrecuenteList
    {
      public  string v_DxFrecuenteId { get; set; }
      public string v_DiseasesId { get; set; }
      public string v_DiseasesName { get; set; }
      public string v_CIE10Id { get; set; }
      public string v_DxFrecuenteDetalleId { get; set; }
      public string v_MasterRecommendationRestricctionId { get; set; }
      public string v_MasterRecommendationRestricctionName { get; set; }
      public int i_Tipo { get; set; }
      public string v_RestriccionName { get; set; }
      public List<DxFrecuenteDetalleList> DxFrecuenteDetalle { get; set; }
      public string v_CIE10Idv_Name { get; set; }

    }
}
