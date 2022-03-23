using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeskabClassLib.Door;
using NUnit.Framework;

namespace Ladeskab.NUnit.test
{
    public class DoorTest
    {
        private DoorChangedEventArgs eventArgs;
        private Door uut;

        [SetUp]
        public void Setup()
        {
            eventArgs = null;
            uut = new Door();
            uut.DoorChangedEvent += (o, args) =>
            {
                eventArgs = args;
            };
        }
        
        #region Tester LockDoor

        [Test]
        public void LockDoor_zero_Unlocked_and_closed_expect_false()
        {
            Assert.That(uut.OldLockingStatus, Is.False);
        }

        [Test]
        public void LockDoor_one_unlocked_and_closed_expect_true()
        {
            //Act
            uut.LockDoor();

            //Assert
            Assert.That(uut.OldLockingStatus, Is.True);
            Assert.That(eventArgs, Is.Not.Null);
        }
        #endregion

        #region Tester UnlockDoor

        [Test]
        public void UnlockDoor_zero_locked_and_closed_expect_true()
        {
            uut.LockDoor();

            Assert.That(uut.OldLockingStatus, Is.True);
        }

        [Test]
        public void UnlockDoor_one_locked_and_closed_expect_false()
        {
            //Arrange
            uut.LockDoor();

            //Act
            uut.UnlockDoor();

            //Assert
            Assert.That(uut.OldLockingStatus, Is.False);
        }

        #endregion

        #region Tester onDoorOpen

        [Test]
        public void onDoorOpen_zero_expect_door_is_closed_and_event_not_fired()
        {
            Assert.Multiple(() =>
            {
                Assert.That(uut.OldDoorStatus, Is.False);
                Assert.That(eventArgs, Is.Null);
            });
        }

        [Test]
        public void onDoorOpen_one_expect_door_is_open_and_event_fired()
        {
            //Act
            uut.OnDoorOpen();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(uut.OldDoorStatus, Is.True);
                Assert.That(eventArgs, Is.Not.Null);
                Assert.That(eventArgs.DoorStatus, Is.True);
            });
        }

        [Test]
        public void onDoorOpen_two_expect_door_is_open_and_event_fired()
        {
            //Act
            uut.OnDoorOpen();
            uut.OnDoorOpen();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(uut.OldDoorStatus, Is.True);
                Assert.That(eventArgs, Is.Not.Null);
                Assert.That(eventArgs.DoorStatus, Is.True);
            });
        }

        #endregion

        #region Tester onDoorClose

        [Test]
        public void OnDoorClose_zero_expect_door_is_closed_and_event_not_fired()
        {
            Assert.Multiple(() =>
            {
                Assert.That(eventArgs, Is.Null);
                Assert.That(uut.OldDoorStatus, Is.False);
            });
        }

        [Test]
        public void OnDoorClose_zero_expect_door_is_open_and_event_not_fired()
        {
            //Arrange
            uut.OnDoorOpen();

            //Act
            //ingen act da det er zero test

            //Arrange
            Assert.Multiple(() =>
            {
                Assert.That(uut.OldDoorStatus, Is.True);
                Assert.That(eventArgs, Is.Not.Null);
                Assert.That(eventArgs.DoorStatus, Is.True);
            });
        }

        [Test]
        public void OnDoorClose_one_expect_door_is_closed_and_event_fired()
        {
            //Arrange
            uut.OnDoorOpen();

            //Act
            uut.OnDoorClose();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(uut.OldDoorStatus, Is.False);
                Assert.That(eventArgs, Is.Not.Null);
                Assert.That(eventArgs.DoorStatus, Is.False);
            });
        }

        #endregion
        
    }
}
