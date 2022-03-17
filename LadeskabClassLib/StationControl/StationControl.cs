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
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private IDisplay _display;
        private int _oldId;
        private IDoor _door;
        private IRfidReader _rfidReader;
        private ILogFile _logFile;
        private int rfidID;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public StationControl(IChargeControl chargeControl, IDisplay display, IDoor door, IRfidReader rfidReader, ILogFile logFile)
        {
            _charger = chargeControl;
            _display = display;
            _door = door;
            _rfidReader = rfidReader;
            _logFile = logFile;

        }

        private void HandleRfidEvent(int rfidID, RfidChangedEventArgs rfidReader)
        {
            if (_door.IsLocked == false && _door.IsOpen == false) // hvis døren ikke er låst. Skal lige have lavet det ordenligt
            {
                switch (_charger.Connected()) // hvorfor virker denne ikke?
                {
                    case true:
                        rfidID = rfidReader.ID;
                        _charger.StartCharge();
                        _door.LockDoor(); // skal denne kunne tage et ID?
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", rfidID);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                        break;

                    case false:
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                        break;
                }
            }
            else if (_door.IsLocked)
            {
                switch (CheckId(rfidReader.ID))
                {
                    case true:
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", rfidID );
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        break;

                    case false:
                        Console.WriteLine("Forkert RFID tag");
                        break;
                }
            }
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
                {
                    case LadeskabState.Available:
                        // Check for ladeforbindelse
                        if (_charger.Connected)
                        {
                            _door.LockDoor();
                            _charger.StartCharge();
                            _oldId = id;
                            using (var writer = File.AppendText(logFile))
                            {
                                writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                            }

                            Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                            _state = LadeskabState.Locked;
                        }
                        else
                        {
                            Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                        }

                        break;

                    case LadeskabState.DoorOpen:
                        // Ignore
                        break;

                    case LadeskabState.Locked:
                        // Check for correct ID
                        if (id == _oldId)
                        {
                            _charger.StopCharge();
                            _door.UnlockDoor();
                            using (var writer = File.AppendText(logFile))
                            {
                                writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                            }

                            Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                            _state = LadeskabState.Available;
                        }
                        else
                        {
                            Console.WriteLine("Forkert RFID tag");
                        }

                        break;
                }
            
        }

        // Her mangler de andre trigger handlere
        private bool CheckId(int id)
        {
            if (rfidID == id)
            {
                return true;
            }

            return false;
        }

    }
}
