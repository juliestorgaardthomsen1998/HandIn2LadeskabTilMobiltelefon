using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabClassLib.LogFile
{
    public class LogFile : ILogFile
    {
        private readonly IFileWriter _fileWriter;
        private readonly ITimeProvider _timeProvider;
        public string LogLine { get; set; }

        public LogFile(ITimeProvider timeProvider, IFileWriter fileWriter)
        {
            _fileWriter = fileWriter;
            _timeProvider = timeProvider;
        }

        

        public void DoorUnlocked(string rfid_Id)
        {
            string time = _timeProvider.GetTime();

            LogLine = "Door was unlocked at " + time + " with Rfid id: " + rfid_Id;

            _fileWriter.WriteLineToFile(LogLine);
        }

        public void DoorLocked(string rfid_Id)
        {
            string time = _timeProvider.GetTime();

            LogLine = "Door was locked at " + time + " with Rfid id: " + rfid_Id;

            _fileWriter.WriteLineToFile(LogLine);
        }
    }
}
