using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.Door
{
    public class Door : IDoor
    {
        private bool oldDoorStatus = false;
        private bool oldLockingStatus = false;

        public event EventHandler<DoorChangedEventArgs> DoorChangedEvent;

        public void SetDoor(bool newDoorStatus)
        {
            if (newDoorStatus != oldDoorStatus)
            {
                OnDoorChanged(new DoorChangedEventArgs { Door = newDoorStatus });
                oldDoorStatus = newDoorStatus;
            }
        }

        public void SetLockingDoor(bool newLockingStatus)
        {
            if (newLockingStatus != oldLockingStatus)
            {
                OnDoorChanged(new DoorChangedEventArgs { LockingStatus = newLockingStatus });
                oldDoorStatus = newLockingStatus;
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
