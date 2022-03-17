using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.Display
{
    public interface IDisplay
    {
        DisplayMeassage DisplayMes { get; set; }
        void UpdateText(DisplayMeassage s);
    }
}
