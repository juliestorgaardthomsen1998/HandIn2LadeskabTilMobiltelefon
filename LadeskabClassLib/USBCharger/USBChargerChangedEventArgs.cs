using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.USBCharger
{
    public class USBChargerChangedEventArgs : EventArgs
    {
        public bool USBCharger { get; set; }
    }
}
