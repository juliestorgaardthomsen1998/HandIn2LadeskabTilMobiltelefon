using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib
{
    public class RfidChangedEventArgs : EventArgs
    {
        public bool RfidReader { get; set; }
    }
    
    public interface IRfidReader
    {
        event EventHandler<RfidChangedEventArgs> RfidChangedEvent;
    }

    public class RfidReader : IRfidReader
    {
        public event EventHandler<RfidChangedEventArgs> RfidChangedEvent;

        public void SetRfid(bool newRfidStatus)
        {

        }
    }
}
