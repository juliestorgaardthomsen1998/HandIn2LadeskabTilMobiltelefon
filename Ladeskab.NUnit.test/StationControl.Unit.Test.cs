using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeskabClassLib.Display;
using LadeskabClassLib.Door;
using LadeskabClassLib.LogFile;
using LadeskabClassLib.RfidReader;
using LadeskabClassLib.StationControl;
using LadeskabClassLib.USBCharger;
using NUnit.Framework;
using NSubstitute;
using NUnit.Framework.Constraints;

namespace Ladeskab.NUnit.test
{
    public class StationControlTests
    {
        private StationControl uut;
        private IChargeControl chargeControl;
        private IDisplay display;
        private IDoor door;
        private IRfidReader rfidReader;
        private ILogFile logFile;

        [SetUp]
        public void Setup()
        {
            //Arrange
            chargeControl = Substitute.For<IChargeControl>();
            display = Substitute.For<IDisplay>();
            door = Substitute.For<IDoor>();
            rfidReader = Substitute.For<IRfidReader>();
            logFile = Substitute.For<ILogFile>();

            uut = new StationControl(chargeControl,display,door,rfidReader,logFile);
        }

        //Tester at display ikke har en DisplayMessage, når der ikke er kaldt nogen metoder til UUT. (Zero-test)
        [Test]
        public void setUpUUT_NoMessageOnDisplay()
        {
            //Act //Assert
            display.DidNotReceive().UpdateText(Arg.Any<DisplayMeassage>());
        }
        
        //Tester om DisplayMessage stemmer overens med status på døren. 
        [TestCase(true,DisplayMeassage.TilslutTelefon)]
        [TestCase(false,DisplayMeassage.IndlæsRFID)]
        public void handleDoorEvent_DisplayMeassageIsCorrect(bool doorStatus, DisplayMeassage displayMeassage)
        {
            //Act
            door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs {DoorStatus = doorStatus});

            //Assert
            display.Received(1).UpdateText(displayMeassage);
        }

        //Tester om DisplayMessage viser tilslutningsfejl, hvis telefonen ikke er tilsluttet korrekt efter Rfid er indlæst.
        //[Test]
        //public void handleRfidEvent_ChargerIsNotConnected(bool doorStatus, DisplayMeassage displayMeassage)
        //{
        //    Act

        //    Assert
        //}
    }
}
