using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.USBCharger
{
    public class ChargeControl : IChargeControl
    {
        private UsbChargerSimulator usbCharger;
        public bool Connected { get; set; }

        public event EventHandler<USBChargerChangedEventArgs> USBChangedEvent;

        public bool IsConnected()
        {
            if(usbCharger.Connected == true)
            {
                return true;

                Connected = true;
            }
            else
            {
                return false;

                Connected = false;
            }
        }

        public void StartCharge()
        {

        }

        public void StopCharge()
        {

        }
    }
}
