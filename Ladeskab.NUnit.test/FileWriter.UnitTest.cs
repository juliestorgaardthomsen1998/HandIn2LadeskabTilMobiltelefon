using LadeskabClassLib.LogFile;
using NSubstitute;
using System.IO;
using NUnit.Framework;

namespace Ladeskab.NUnit.test
{
    [TestFixture]
    public class FileWriterUnitTest
    {
        private FileWriter uut;

        [SetUp]
        public void Setup()
        {
            //Arrange
            uut = new FileWriter();
        }

        [TestCase(" ")]
        [TestCase("test")]
        public void WriteLineToFile_LinePropertyIsEqualToLineInMethod(string line)
        {
            //Act
            uut.WriteLineToFile(line);

            //Assert
            Assert.That(uut.Line,Is.EqualTo(line));
        }

    }
}
