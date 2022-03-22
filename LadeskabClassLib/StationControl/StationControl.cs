using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeskabClassLib.USBCharger;
using LadeskabClassLib.Door;
using LadeskabClassLib.Display;
using LadeskabClassLib.LogFile;
using LadeskabClassLib.RfidReader;

namespace LadeskabClassLib.StationControl
{
    public class StationControl
    {
        private IChargeControl _charger;
        private IDisplay _display;
        private IDoor _door;
        private IRfidReader _rfidReader;
        private ILogFile _logFile;
        private string rfidID;

        public StationControl(IChargeControl chargeControl, IDisplay display, IDoor door, IRfidReader rfidReader,
            ILogFile logFile)
        {
            _charger = chargeControl;
            _display = display;
            _door = door;
            _door.DoorChangedEvent += HandleDoorEvent;
            _rfidReader = rfidReader;
            _rfidReader.RfidChangedEvent += HandleRfidEvent;
            _logFile = logFile;

        }   

        private void HandleRfidEvent(object sender, RfidChangedEventArgs rfidReader)
        {
            if (_door.OldLockingStatus == false &&
                _door.OldDoorStatus == false) 
            {
                switch (_charger.IsConnected()) 
                {
                    case true:
                        rfidID = rfidReader.ID;
                        _charger.StartCharge();
                        _door.LockDoor();
                        _logFile.DoorLocked((rfidID));
                        _display.UpdateText(DisplayMeassage.LadeskabOptaget);
                        break;

                    case false:
                        _display.UpdateText(DisplayMeassage.Tilslutningsfejl);
                        break;
                }
            }
            else if (_door.OldLockingStatus)
            {
                switch (CheckId(rfidReader.ID))
                {
                    case true:
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        rfidID = "";
                        _logFile.DoorUnlocked(rfidReader.ID);
                        _display.UpdateText(DisplayMeassage.FjernTelefon);
                        break;

                    case false:
                        _display.UpdateText(DisplayMeassage.RFIDFejl);
                        break;
                }
            }
        }


        private void HandleDoorEvent(object sender, DoorChangedEventArgs doorStatus)
        {
            switch (doorStatus.DoorStatus)
            {
                case true:
                    _display.UpdateText(DisplayMeassage.TilslutTelefon);
                    break;
                case false:
                    _display.UpdateText(DisplayMeassage.IndlæsRFID);
                    break;
            }
        }


        private bool CheckId(string id)
        {
            if (rfidID == id)
            {
                return true;
            }
            else
                return false;
        }

    }
}
