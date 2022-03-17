using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.Door
{
    public class Door : IDoor
    {
        public bool OldDoorStatus { get; set; } = false;
        public bool OldLockingStatus { get; set; } = false;

        public event EventHandler<DoorChangedEventArgs> DoorChangedEvent;

        public void SetDoor(bool newDoorStatus)
        {
            if (newDoorStatus != OldDoorStatus)
            {
                OnDoorChanged(new DoorChangedEventArgs { Door = newDoorStatus });
                OldDoorStatus = newDoorStatus;
            }
        }

        public void SetLockingDoor(bool newLockingStatus)
        {
            if (newLockingStatus != OldLockingStatus)
            {
                OnDoorChanged(new DoorChangedEventArgs { LockingStatus = newLockingStatus });


                OldLockingStatus = newLockingStatus;

                OldLockingStatus = newLockingStatus;

                OldLockingStatus = newLockingStatus;

            }
        }

        protected virtual void OnDoorChanged(DoorChangedEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }

        public void LockDoor()
        {
            SetLockingDoor(true);
        }

        public void UnlockDoor()
        {
            SetLockingDoor(false);
        }

        public void OnDoorOpen()
        {
            SetDoor(true);
        }

        public void OnDoorClose()
        {
            SetDoor(false);
        }
    }
}
