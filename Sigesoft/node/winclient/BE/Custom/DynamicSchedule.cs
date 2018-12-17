using System;
using System.Collections.Generic;

namespace Sigesoft.Node.WinClient.BE
{
    public class DynamicSchedule
    {
        public string NroDocument { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime Birthdate { get; set; }
        public int SexTypeId { get; set; }
        public string GesoId { get; set; }
        public string Geso { get; set; }
        public string CurrentOccupation { get; set; }
    }

    public class ScheduleForProcess
    {
        public string NroDocument { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime Birthdate { get; set; }
        public int SexTypeId { get; set; }
        public string CurrentOccupation { get; set; }
        public string GesoId { get; set; }
        public string Geso { get; set; }
        public string PersonId { get; set; }
        public string ProtocolId { get; set; }
        public List<ComponentsByRecord> ComponentsByRecord { get; set; }
    }

    public class ComponentsByRecord
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ComponetId { get; set; }
        public string ComponetName { get; set; }
        public bool Check { get; set; }
    }
}
