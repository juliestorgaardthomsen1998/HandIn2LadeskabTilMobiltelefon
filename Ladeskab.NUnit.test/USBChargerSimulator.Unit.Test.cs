using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LadeskabClassLib.USBCharger;
using NUnit.Framework;

namespace Ladeskab.NUnit.test
{
    [TestFixture]
    public class USBChargerSimulatorTests
    {
        private UsbChargerSimulator uut;

        [SetUp]
        public void Setup()
        {
            uut = new UsbChargerSimulator();
        }

        #region Test of the constructor
        //Current value is zero
        [Test]
        public void Ctor_CurrentValueIsZero()
        {
            //Act
            //Assert
            Assert.That(uut.CurrentValue,Is.EqualTo(0));
        }

        //Connected is true
        [Test]
        public void Ctor_ConnectedIsTrue()
        {
            //Act
            //Assert
            Assert.That(uut.Connected, Is.EqualTo(true));
        }
        #endregion

        #region Test of simulates
        //Test of Simulate Connected
        [TestCase(true, true)]
        [TestCase(false, false)]
        public void SimulateConnected_IsResult(bool parameter, bool result)
        {
            //Act
            uut.SimulateConnected(parameter);

            //Assert
            Assert.That(uut.Connected, Is.EqualTo(result));
        }

        //Test of Simulate Overload
        [TestCase(true, true)]
        [TestCase(false, false)]
        public void SimulateOverload_ParameterIsResult(bool parameter, bool result)
        {
            //Act
            uut.SimulateOverload(parameter);

            //Assert
            Assert.That(uut.Overload, Is.EqualTo(result));
        }


        #endregion

        #region Test of StartCharge
        //Korrekt Current afhængig af status på Overload og Connected
        [TestCase(true, false, 500)]
        [TestCase(true, true, 750)]
        [TestCase(false, false, 0)]
        [TestCase(false, true, 0)]
        public void StartCharge_ConnectededAndOverloads_ResultIsCorrectCurrent(bool connect, bool overload, double currentValue)
        {
            //Arrange
            uut.SimulateConnected(connect);
            uut.SimulateOverload(overload);

            //Act
            uut.StartCharge();

            //Assert
            Assert.That(uut.CurrentValue, Is.EqualTo(currentValue));
        }

        
        [Test]
        public void StartCharge_Wait1Sec_RecivedMoreValues()
        {
            //Arrange
            int numValues = 0;
            uut.CurrentValueEvent += (o, args) => numValues++;

            //Act
            uut.StartCharge();
            Thread.Sleep(1000);

            //Assert
            Assert.That(numValues, Is.GreaterThan(3));
        }

        [Test]
        public void StartCharge_StartedEventReceiver_Wait1Sec_ReceivedChangedValue()
        {
            //Arrange
            double lastValue = 1000;
            uut.CurrentValueEvent += (o, args) => lastValue = (double)args.Current;

            //Act
            uut.StartCharge();
            Thread.Sleep(1000);

            //Assert
            Assert.That(lastValue, Is.LessThan(500.0));
        }

        [Test]
        public void StartCharge_Wait1Sec_CurrentValueChangedValue()
        {
            //Act
            uut.StartCharge();
            Thread.Sleep(1000);

            //Assert
            Assert.That(uut.CurrentValue, Is.LessThan(500.0));
        }

        [Test]
        public void StartCharge_DisConnectedWait1Sec_CurrentValueIsZero()
        {
            //Arrange
            uut.SimulateConnected(false);

            //Act
            uut.StartCharge();
            Thread.Sleep(1000);

            //Assert
            Assert.That(uut.CurrentValue, Is.EqualTo(0));
        }

        [Test]
        public void StopCharge_CurrentValueIsZero()
        {
            //Arrange
            //Act
            uut.StartCharge();
            Thread.Sleep(1000);
            uut.StopCharge();

            Assert.That(uut.CurrentValue, Is.EqualTo(0.0));
        }
        #endregion


    }
}
