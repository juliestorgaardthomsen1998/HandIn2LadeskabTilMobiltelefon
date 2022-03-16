using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.Display
{
    public class Display : IDisplay
    {
        public void UpdateText(DisplayMeassage s)
        {
            switch(s)
            {
                case DisplayMeassage.TilslutTelefon:
                    Console.WriteLine("Tilslut telefon");
                    break;
                case DisplayMeassage.Tilslutningsfejl:
                    Console.WriteLine("Tilslutningsfejl");
                    break;
                case DisplayMeassage.TelefonFuldtOpladet:
                    Console.WriteLine("Telefon er fuldt opladet");
                    break;
                case DisplayMeassage.RFIDFejl:
                    Console.WriteLine("RFID Fejl");
                    break;
                case DisplayMeassage.LadeskabOptaget:
                    Console.WriteLine("Ladeskab optaget");
                    break;
                case DisplayMeassage.IndlæsRFID:
                    Console.WriteLine("Indlæs RFID");
                    break;
                case DisplayMeassage.FjernTelefon:
                    Console.WriteLine("Fjern telefon");
                    break;
                default:
                    Console.WriteLine("Unknown Error");
                    break;
            }    
                
        }
    }
}
