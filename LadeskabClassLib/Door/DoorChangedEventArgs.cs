using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.Door
{
    public class DoorChangedEventArgs : EventArgs
    {
        public bool Door { get; set; }
    }
}
