using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.LogFile
{
    public interface ILogFile
    {
        public void DoorUnlocked(string eID);
        public void DoorLocked(string rfidID);
    }
}
