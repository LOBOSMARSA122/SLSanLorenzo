using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    /// <summary>
    /// Proporciona datos del evento DataReceived
    /// </summary>
    public class AudiometriaAfterValueChangeEventArgs : EventArgs
    {
        public AudiometriaAfterValueChangeEventArgs() { }
        public object PackageSynchronization { get; set; }
        public string componentFieldsId { get; set; }
        public List<string> ListcomponentFieldsId { get; set; }
    }
}
