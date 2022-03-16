﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.USBCharger
{
    public interface IChargeControl
    {
        event EventHandler<USBChargerChangedEventArgs> USBChangedEvent;

        bool Connected { get; set; }

        void StartCharge();
        void StopCharge();
    }
}