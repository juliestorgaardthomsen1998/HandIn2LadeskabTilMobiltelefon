using LadeskabClassLib.Display;
using LadeskabClassLib.USBCharger;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.NUnit.test
{
    public class Tests
    {
        private ChargeControl uut;
        private IUsbCharger usbCharger;
        private IDisplay display;

        [SetUp]
        public void Setup()
        {
            usbCharger = Substitute.For<IUsbCharger>();
            display = Substitute.For<IDisplay>();
            uut = new ChargeControl(display, usbCharger);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void TelefonFuldtOpladet_RaiseCurrentEvent_DisplayReceiveCorrectMessage(int ladestrøm)
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = ladestrøm });
            display.Received(1).UpdateText(DisplayMeassage.TelefonFuldtOpladet);
        }

        [TestCase(6)]
        [TestCase(-2)]
        [TestCase(10)]
        public void TelefonFuldtOpladetUdenForBounderies_RaiseCurrentEvent_DisplayReceiveCorrectMessage(int ladestrøm)
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = ladestrøm });
            display.Received(0).UpdateText(DisplayMeassage.TelefonFuldtOpladet);
        }

        [TestCase(6)]
        [TestCase(150)]
        [TestCase(500)]
        public void LadningIGang_RaiseCurrentEvent_DisplayReceiveCorrectMessage(int ladestrøm)
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = ladestrøm });
            display.Received(1).UpdateText(DisplayMeassage.LadningIgang);
        }

        [TestCase(-1)]
        [TestCase(700)]
        [TestCase(501)]
        public void LadningIGangUdenForBounderies_RaiseCurrentEvent_DisplayReceiveCorrectMessage(int ladestrøm)
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = ladestrøm });
            display.Received(0).UpdateText(DisplayMeassage.LadningIgang);
        }

        [TestCase(501)]
        [TestCase(520)]
        [TestCase(700)]
        public void Kortslutning_RaiseCurrentEvent_DisplayReceiveCorrectMessage(int ladestrøm)
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = ladestrøm });
            display.Received(1).UpdateText(DisplayMeassage.Kortslutning);
        }

        [TestCase(-5)]
        [TestCase(5)]
        [TestCase(499)]
        public void KortslutningUdenForBounderies_RaiseCurrentEvent_DisplayReceiveCorrectMessage(int ladestrøm)
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = ladestrøm });
            display.Received(0).UpdateText(DisplayMeassage.Kortslutning);
        }

        [Test]
        public void CurrentEventNull_RaiseCurrentEvent_SwitchCaseDefault()
        {
            uut.CurrentCurrent = null;

            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = null });
            display.DidNotReceive().UpdateText(Arg.Any<DisplayMeassage>());
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]
        public void ZeroCurrent_RaiseCurrentEvent_DisplayReceiveCorrectMessage(int ladestrøm)
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = ladestrøm });
            display.Received(0).DidNotReceiveWithAnyArgs();
        }

        [Test]
        public void IsConnectedUpOnStartCharge_Received_ExpectTrue()
        {
            usbCharger.Connected.Returns(true);
            uut.StartCharge();
            usbCharger.Received(1).StartCharge();

            Assert.That(uut.IsConnected, Is.EqualTo(true));
        }

        [Test]
        public void IsConnectedUpOnStartCharge_Received_ExpectFalse()
        {
            usbCharger.Connected.Returns(false);
            uut.StartCharge();
            usbCharger.Received(1).StartCharge();

            Assert.That(uut.IsConnected, Is.EqualTo(false));
        }
        [Test]
        public void StartCharge_Received_ExpectOnetime()
        {
            uut.StartCharge();
            usbCharger.Received(1).StartCharge();
            usbCharger.DidNotReceive().StopCharge();
        }

        [Test]
        public void StartCharge_ReceivedZero_ExpectOnetime()
        {
            usbCharger.Received(0).StartCharge();
            usbCharger.DidNotReceive().StopCharge();
        }

        [Test]
        public void StopCharge_Received_ExpectOnetime()
        {
            uut.StopCharge();
            usbCharger.Received(1).StopCharge();
            usbCharger.DidNotReceive().StartCharge();
        }

        [Test]
        public void StopCharge_ReceivedZeroTimes_ExpectOnetime()
        {
            usbCharger.Received(0).StopCharge();
            usbCharger.DidNotReceive().StartCharge();
        }
    }
}