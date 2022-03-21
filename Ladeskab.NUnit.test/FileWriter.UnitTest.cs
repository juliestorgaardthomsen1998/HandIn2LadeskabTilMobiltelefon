using LadeskabClassLib.LogFile;
using NSubstitute;
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
        public void WriteLineToFile_(string l)
        {
            //Act
            uut.WriteLineToFile(l);

            //Assert
            //Assert.That();
        }

    }
}
