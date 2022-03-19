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
        public void FuldtOpladet_RaiseCurrentEvent_DisplayReceiveCorrectMessage(int ladestr�m)
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = ladestr�m });
            display.Received(1).UpdateText(DisplayMeassage.TelefonFuldtOpladet);
        }
    }
}