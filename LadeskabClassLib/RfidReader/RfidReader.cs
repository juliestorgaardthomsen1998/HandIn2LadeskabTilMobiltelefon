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

        public void RfidReaderIsActivated(RfidChangedEventArgs eventArgs)
        {
            RfidChangedEvent?.Invoke(this, eventArgs);
        }

        public void RfidChanged(int id)
        {
            RfidReaderIsActivated(new RfidChangedEventArgs
            {
                ID = id
            });
        }
    }
}
