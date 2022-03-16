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

        public event EventHandler<DoorChangedEventArgs> DoorChangedEvent;

        public void SetDoor(bool newDoorStatus)
        {
            if (newDoorStatus != oldDoorStatus)
            {
                OnDoorChanged(new DoorChangedEventArgs { Door = newDoorStatus });
                oldDoorStatus = newDoorStatus;
            }
        }

        protected virtual void OnDoorChanged(DoorChangedEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }

        public void LockDoor()
        {
            
        }

        public void UnlockDoor()
        {
            
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
