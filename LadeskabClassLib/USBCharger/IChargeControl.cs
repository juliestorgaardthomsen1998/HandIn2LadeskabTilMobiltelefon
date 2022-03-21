using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.USBCharger
{
    public interface IChargeControl
    {
        event EventHandler<CurrentEventArgs> USBChangedEvent;
        bool IsConnected();

        void StartCharge();
        void StopCharge();
    }
}
