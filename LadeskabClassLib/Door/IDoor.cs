﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.Door
{
    public interface IDoor
    {
        event EventHandler<DoorChangedEventArgs> DoorChangedEvent;

        public void LockDoor();
        public void UnlockDoor();
    }
}