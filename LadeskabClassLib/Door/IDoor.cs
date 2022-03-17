using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.Door
{
    public interface IDoor
    {
        bool OldDoorStatus { get; set; }

        bool OldLockingStatus { get; set; }

        event EventHandler<DoorChangedEventArgs> DoorChangedEvent;

        void LockDoor();

        void UnlockDoor();

        void OnDoorOpen();

        void OnDoorClose();
    }
}
