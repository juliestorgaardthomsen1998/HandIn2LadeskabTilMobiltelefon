using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.LogFile
{
    public interface ITimeProvider
    {
        string GetTime();
    }
}
