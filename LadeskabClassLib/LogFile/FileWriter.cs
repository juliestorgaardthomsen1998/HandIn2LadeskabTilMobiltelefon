using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.LogFile
{
    public class FileWriter: IFileWriter
    {
        private string filename = "Logfile.txt";

        //Denne metode gemmer line i en ny fil hvis den ikke er oprettet, ellers skriver den blot til i bunden af filen

        public void WriteLineToFile(string line)
        {
            if (!File.Exists(filename))
            {
                using StreamWriter sw = new StreamWriter(File.Create(filename));
                {
                    sw.WriteLine(line);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filename))
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}
