﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.RfidReader
{
    public interface IRfidReader
    {
        event EventHandler<RfidChangedEventArgs> RfidChangedEvent;
    }
}
