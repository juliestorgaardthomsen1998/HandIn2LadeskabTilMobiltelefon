using System;
using LadeskabClassLib.Display;
using LadeskabClassLib.Door;
using LadeskabClassLib.LogFile;
using LadeskabClassLib.RfidReader;
using LadeskabClassLib.StationControl;
using LadeskabClassLib.USBCharger;

namespace LadeskabTilMobiltelefon
{
    class Program
    {
        static void Main(string[] args)
        {
            Door door = new Door();
            UsbChargerSimulator usbSimulator = new UsbChargerSimulator();
            IDisplay display = new Display();
            IChargeControl chargeControl = new ChargeControl(display, usbSimulator);
            ILogFile logFile = new LogFile(new TimeProvider(), new FileWriter());
            IRfidReader rfidReader = new RfidReader();

            StationControl stationControl = new StationControl(chargeControl, display, door, rfidReader, logFile);


            Console.WriteLine("Ladeskab til mobiltelefon");

            // Assemble your system here from all the classes

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E(exit), O(open door), C(close door), R(læs Rfid), P(plug), U(unplog), L(overload), N(not overload): ");
                input = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OnDoorOpen();
                        break;

                    case 'C':
                        door.OnDoorClose();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();
                        
                        rfidReader.RfidChanged(idString);
                        break;
                    case 'P':
                        usbSimulator.SimulateConnected(true);
                        break;
                    case 'U':
                        usbSimulator.SimulateConnected(false);
                        break;
                    case 'L':
                        usbSimulator.SimulateOverload(true);
                        break;
                    case 'N':
                        usbSimulator.SimulateConnected(false);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
