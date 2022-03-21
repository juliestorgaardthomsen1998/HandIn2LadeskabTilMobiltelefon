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

        #region ZeroTest (display er tomt ved start)
        //Tester at display ikke har en DisplayMessage, når der ikke er kaldt nogen metoder til UUT. (Zero-test)
        [Test]
        public void setUpUUT_NoMessageOnDisplay()
        {
            //Act //Assert
            display.DidNotReceive().UpdateText(Arg.Any<DisplayMeassage>());
        }
        #endregion

        #region Test af handleDoorEvent

        //Tester om DisplayMessage stemmer overens med status på døren. 
        [TestCase(true, DisplayMeassage.TilslutTelefon)]
        [TestCase(false, DisplayMeassage.IndlæsRFID)]
        public void handleDoorEvent_DisplayMeassageIsCorrect(bool doorStatus, DisplayMeassage displayMeassage)
        {
            //Act
            door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorStatus = doorStatus });

            //Assert
            display.Received(1).UpdateText(displayMeassage);
        }


        #endregion

        #region Test af handleRfidEvent med tomt ladeskab

        //Tester om DisplayMessage er korrekt afhængig om telfonen bliver sat korrekt i opladeren.
        [TestCase(true, DisplayMeassage.LadeskabOptaget)]
        [TestCase(false, DisplayMeassage.Tilslutningsfejl)]
        public void handleRfidEvent_ChargerConnectedAndNotConnected_DisplayMeassageIsCorrect(bool chargerConnection, DisplayMeassage displayMeassage)
        {
            //Arrange
            chargeControl.IsConnected().Returns(chargerConnection);

            //Act
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });

            //Assert
            display.Received(1).UpdateText(displayMeassage);
        }

        //Test om opladning startes efter telefon er sat korrekt i opalderen og Rfid indlæses
        [Test]
        public void handleRfidEvent_ChargerConnected_ChargingIsStarted()
        {
            //Arrange

            chargeControl.IsConnected().Returns(true);

            //Act
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });

            //Assert
            chargeControl.Received(1).StartCharge();
        }

        //Test om døren låses efter telefon er sat korrekt i opalderen og Rfid indlæses
        [Test]
        public void handleRfidEvent_ChargerConnected_DoorIsLocked()
        {
            //Arrange
            chargeControl.IsConnected().Returns(true);

            //Act
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });

            //Assert
            door.Received(1).LockDoor();
        }

        //Test om logfile modtager testID efter telefon er sat korrekt i opalderen og Rfid indlæses
        [Test]
        public void handleRfidEvent_ChargerConnected_LogFileRecivesTestID()
        {
            //Arrange

            chargeControl.IsConnected().Returns(true);

            //Act
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });

            //Assert
            logFile.Received(1).DoorLocked("testID");
        }
        #endregion

        #region Test af handleRfidEvent med ladeskab med telefon. RFID er korrekt.
        //Test hvor korrekt RFID holdes foran ladeskab. DisplayMeassage bliver FjernTelefon
        [Test]
        public void handleRfidEvent_DoorIsLocked_CorrectRfid_DisplayMeassageIsFjernTelefon()
        {
            //Arrange
            chargeControl.IsConnected().Returns(true);
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });
            door.OldLockingStatus.Returns(true);

            //Act
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });

            //Assert
            display.Received(1).UpdateText(DisplayMeassage.FjernTelefon);
        }

        //Test hvor korrekt RFID holdes foran ladeskab. Door bliver låst op.
        [Test]
        public void handleRfidEvent_DoorIsLocked_CorrectRfid_DoorIsUnlocked()
        {
            //Arrange

            chargeControl.IsConnected().Returns(true);
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });
            door.OldLockingStatus.Returns(true);

            //Act
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });

            //Assert
            door.Received(1).UnlockDoor();
        }

        [Test]
        public void handleRfidEvent_DoorIsLocked_CorrectRfid_ChargedIsStopped()
        {
            //Arrange

            chargeControl.IsConnected().Returns(true);
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });
            door.OldLockingStatus.Returns(true);

            //Act
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });

            //Assert
            chargeControl.Received(1).StopCharge();
        }

        [Test]
        public void handleRfidEvent_DoorIsLocked_CorrectRfid_LogfileRegisterThatDoorIsUnLocked()
        {
            //Arrange

            chargeControl.IsConnected().Returns(true);
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });
            door.OldLockingStatus.Returns(true);

            //Act
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });

            //Assert
            logFile.Received(1).DoorUnlocked("testID");
        }

        #endregion

        #region Test af handkeRfidEvent med ladeskab med telefon. RFID IKKE korrekt.
        [Test]
        public void handleRfidEvent_DoorIsLocked_WrongRFID_DisplayMessageisRFIDfejl()
        {
            //Arrange

            chargeControl.IsConnected().Returns(true);
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID" });
            door.OldLockingStatus.Returns(true);

            //Act
            rfidReader.RfidChangedEvent += Raise.EventWith(new RfidChangedEventArgs() { ID = "testID2" });

            //Assert
            display.Received(1).UpdateText(DisplayMeassage.RFIDFejl);
        }


        #endregion

    }
}
