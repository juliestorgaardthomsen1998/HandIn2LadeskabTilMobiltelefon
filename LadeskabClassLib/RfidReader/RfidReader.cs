using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.RfidReader
{
    public class RfidReader : IRfidReader
    {
        public event EventHandler<RfidChangedEventArgs> RfidChangedEvent;

        public void SetRfid(bool newRfidStatus)
        {

        }
    }
}
