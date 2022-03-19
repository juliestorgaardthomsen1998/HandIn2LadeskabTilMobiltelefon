using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeskabClassLib.Display;

namespace LadeskabClassLib.USBCharger
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _usbCharger;
        private IDisplay _display;
        public double? CurrentCurrent { get; set; }
        private const int ZeroCurrent = 0;
        private const int TelefonOpladet = 5;
        private const int Ladestrøm = 500;
        private const int Kortslutning = 501;

        public bool Connected { get; set; }

        public event EventHandler<CurrentEventArgs> USBChangedEvent;

        public ChargeControl(IDisplay display, IUsbCharger usb)
        {
            _display = display;
            _usbCharger = usb;
            _usbCharger.CurrentValueEvent += HandleUSBChargerEvent;
        }

        private void HandleUSBChargerEvent(object sender, CurrentEventArgs e)
        {
            CurrentCurrent = e.Current;
            HandleCurrentDataEvent();
        }

        private void HandleCurrentDataEvent()
        {

            switch (CurrentCurrent)
            {
                case <= ZeroCurrent:
                    break;
                case <= TelefonOpladet:
                    if (_display.DisplayMes != DisplayMeassage.TelefonFuldtOpladet)
                    {
                        _display.UpdateText(DisplayMeassage.TelefonFuldtOpladet);
                    }
                    break;
                case <= Ladestrøm:
                    if (_display.DisplayMes != DisplayMeassage.LadningIgang)
                    {
                        _display.UpdateText(DisplayMeassage.LadningIgang);
                    }
                    break;
                case >= Kortslutning:
                    if (_display.DisplayMes != DisplayMeassage.Kortslutning)
                    {
                        _display.UpdateText(DisplayMeassage.Kortslutning);
                    }
                    break;
                default:
                    break;
            }
        }

        public bool IsConnected()
        {
            return _usbCharger.Connected;
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }
    }
}
