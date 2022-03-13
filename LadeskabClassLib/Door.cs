using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib
{
    public class DoorChangedEventArgs : EventArgs
    {
        public bool Door { get; set; }
    }

    public interface IDoor
    {
        event EventHandler<DoorChangedEventArgs> DoorChangedEvent;

        public void LockDoor();
    }

    public class Door : IDoor
    {
        private bool oldDoorStatus;

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
    }

   
}
