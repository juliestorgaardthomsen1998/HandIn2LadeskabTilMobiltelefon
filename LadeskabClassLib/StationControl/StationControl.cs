﻿using System;
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
        private int _oldId;
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
                switch (_charger.Connected) // hvorfor virker denne ikke?
                {
                    case true:
                        rfidID = rfidReader.ID;
                        _charger.StartCharge();
                        _door.LockDoor(); // skal denne kunne tage et ID?
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


        private void HandleDoorEvent(object sender, DoorChangedEventArgs doorstatus)
        {
            switch (doorstatus.LockingStatus)
            {
                case true:
                    _display.UpdateText(DisplayMeassage.TilslutTelefon);
                    break;
                default:
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


// Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
//private void RfidDetected(int id)
//{
//    switch (_state)
//        {
//            case LadeskabState.Available:
//                // Check for ladeforbindelse
//                if (_charger.Connected)
//                {
//                    _door.LockDoor();
//                    _charger.StartCharge();
//                    _oldId = id;
//                    using (var writer = File.AppendText(logFile))
//                    {
//                        writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
//                    }

//                    Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
//                    _state = LadeskabState.Locked;
//                }
//                else
//                {
//                    Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
//                }

//                break;

//            case LadeskabState.DoorOpen:
//                // Ignore
//                break;

//            case LadeskabState.Locked:
//                // Check for correct ID
//                if (id == _oldId)
//                {
//                    _charger.StopCharge();
//                    _door.UnlockDoor();
//                    using (var writer = File.AppendText(logFile))
//                    {
//                        writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
//                    }

//                    Console.WriteLine("Tag din telefon ud af skabet og luk døren");
//                    _state = LadeskabState.Available;
//                }
//                else
//                {
//                    Console.WriteLine("Forkert RFID tag");
//                }

//}

//// Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
//private enum LadeskabState
//{
//    Available,
//    Locked,
//    DoorOpen
//};




