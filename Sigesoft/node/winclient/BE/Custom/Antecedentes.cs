using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class Antecedentes
    {
        public string PersonId { get; set; }
        public string ServicioId { get; set; }
        public List<AntecedentesList> ListaAntecendentes { get; set; }
        public List<NoxiousHabitsList> ListaHabitos { get; set; }
        public List<PersonMedicalHistoryList> ListaPersonalMedical { get; set; }
        public List<FamilyMedicalAntecedentsList> ListaAntecedentesFamiliares { get; set; }
    }
}
