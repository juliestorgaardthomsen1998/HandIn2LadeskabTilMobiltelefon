using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib
{
    public class USBChargerChangedEventArgs : EventArgs
    {
        public bool USBCharger { get; set; }
    }

    public interface IChargeControl
    {
        event EventHandler<USBChargerChangedEventArgs> USBChangedEvent;
    }
    public class ChargeControl
    {
        public event EventHandler<USBChargerChangedEventArgs> USBChangedEvent;
    }
}
