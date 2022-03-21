using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.Display
{
    public class Display : IDisplay
    {
        public DisplayMeassage DisplayMes { get; set; }

        public void UpdateText(DisplayMeassage s)
        {
            switch(s)
            {
                case DisplayMeassage.TilslutTelefon:
                    Console.WriteLine("Tilslut telefon");
                    DisplayMes = DisplayMeassage.TilslutTelefon;
                    break;
                case DisplayMeassage.Tilslutningsfejl:
                    Console.WriteLine("Tilslutningsfejl");
                    DisplayMes = DisplayMeassage.Tilslutningsfejl;
                    break;
                case DisplayMeassage.TelefonFuldtOpladet:
                    Console.WriteLine("Telefon er fuldt opladet");
                    DisplayMes = DisplayMeassage.TelefonFuldtOpladet;
                    break;
                case DisplayMeassage.RFIDFejl:
                    Console.WriteLine("RFID Fejl");
                    DisplayMes = DisplayMeassage.RFIDFejl;
                    break;
                case DisplayMeassage.LadeskabOptaget:
                    Console.WriteLine("Ladeskab optaget");
                    DisplayMes = DisplayMeassage.LadeskabOptaget;
                    break;
                case DisplayMeassage.IndlæsRFID:
                    Console.WriteLine("Indlæs RFID");
                    DisplayMes = DisplayMeassage.IndlæsRFID;
                    break;
                case DisplayMeassage.FjernTelefon:
                    Console.WriteLine("Fjern telefon");
                    DisplayMes = DisplayMeassage.FjernTelefon;
                    break;
                case DisplayMeassage.Kortslutning:
                    Console.WriteLine("Kortslutning");
                    DisplayMes = DisplayMeassage.Kortslutning;
                    break;
                case DisplayMeassage.LadningIgang:
                    Console.WriteLine("Ladning er i gang");
                    DisplayMes = DisplayMeassage.LadningIgang;
                    break;
                default:
                    Console.WriteLine("Unknown Error");
                    DisplayMes = DisplayMeassage.UknownError;
                    break;
            }    
                
        }
    }
}
