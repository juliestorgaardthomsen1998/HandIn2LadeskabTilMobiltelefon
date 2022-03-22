using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using LadeskabClassLib.RfidReader;


namespace Ladeskab.NUnit.test
{
    public class RfidReaderTest
    {
        private RfidChangedEventArgs eventArgs;
        private IRfidReader uut;
        


        [SetUp]
        public void Setup()
        {
            eventArgs = null;
            uut = new RfidReader();
            uut.RfidChanged("");

            uut.RfidChangedEvent += (o, args) =>
            {
                eventArgs = args;
            };
        }

        #region Zero

        [Test]
        public void RfidDetected_NoMethodCall()
        {
            Assert.That(eventArgs, Is.Null);
        }

        #endregion

        #region One

        [TestCase("Test")]
        public void RfidDetected_one_callmethod(string Rfid)
        {
            //Act
            uut.RfidChanged(Rfid);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(eventArgs.ID, Is.EqualTo(Rfid));
                Assert.That(eventArgs, Is.Not.Null);
            });
        }

        #endregion

        #region Many

        //Tester med store/små bogstaver, tal og rom teststreng
        [TestCase("Test")]
        [TestCase("")]
        [TestCase("123")]
        [TestCase("ID")]
        public void Rfid_many_callmethod(string Rfid)
        {
            //Act
            uut.RfidChanged(Rfid);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(eventArgs.ID, Is.EqualTo(Rfid));
                Assert.That(eventArgs, Is.Not.Null);
            });
        }

        //Kalder metoden flere gange med forskellige parameter. Tester at ID gemmes ved det sidste kald
        [Test]
        public void Rfid_callMethodManyTimes()
        {
            //Act
            uut.RfidChanged("test");
            uut.RfidChanged("ID");
            uut.RfidChanged("Rfid");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(eventArgs.ID, Is.EqualTo("Rfid"));
                Assert.That(eventArgs, Is.Not.Null);
            });
        }
        #endregion
    }
}
