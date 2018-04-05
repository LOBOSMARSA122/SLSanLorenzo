using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class BlackListList
    {
        public string v_BlackListPerson { get; set; }
        public string v_PersonId { get; set; }
        public string v_NroDocumento { get; set; }
        public string Paciente { get; set; }
        public string v_Comment { get; set; }
        public int i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
        public bool seleccion { get; set; }

        public int i_Status { get; set; }
        public DateTime? d_DateRegister { get; set; }
        public DateTime? d_DateDetection { get; set; }
        public DateTime? d_DateSolution { get; set; }
        public object Img { get; set; }
    }
}
