using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MasterRecommendationRestricctionList
    {
        public string v_MasterRecommendationRestricctionId { get; set; }
        public string v_Name { get; set; }
        public int i_TypifyingId { get; set; }
        public int i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
