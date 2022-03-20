using LadeskabClassLib.LogFile;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.NUnit.test
{
    [TestFixture]
    public class LogFileTests
    {
        private LogFile uut;
        private IFileWriter fileWriter;
        private ITimeProvider timeProvider;

        [SetUp]
        public void Setup()
        {
            //Arrange
            fileWriter = Substitute.For<IFileWriter>();
            timeProvider = Substitute.For<ITimeProvider>();

            uut = new LogFile(timeProvider, fileWriter);
        }

        //Zero-test af LogLine
        public void ctor_LogLineIsEqualToNull()
        {
            //Assert
            Assert.That(uut.LogLine, Is.Null);
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("11.1")]
        public void DoorUnlocked_LogLineIsCorrect(string id)
        {
            uut.DoorUnlocked(id);

            var time = timeProvider.GetTime();

            string logLine = "Door was unlocked at " + time + " with Rfid id: " + id;

            Assert.That(uut.LogLine, Is.EqualTo(logLine));

            timeProvider.Received(1);
            fileWriter.Received(1);
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("11.1")]
        public void DoorLocked_LogLineIsCorrect(string id)
        {
            uut.DoorLocked(id);

            var time = timeProvider.GetTime();

            string logLine = "Door was locked at " + time + " with Rfid id: " + id;

            Assert.That(uut.LogLine, Is.EqualTo(logLine));

            timeProvider.Received(1);
            fileWriter.Received(1);
        }

    }
}
