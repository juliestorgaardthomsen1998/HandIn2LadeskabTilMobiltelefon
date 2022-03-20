using LadeskabClassLib.Display;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.NUnit.test
{
    [TestFixture]
    public class DisplayUnitTest
    {
        private IDisplay _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }

        [TestCase(DisplayMeassage.TilslutTelefon)]
        [TestCase(DisplayMeassage.Tilslutningsfejl)]
        [TestCase(DisplayMeassage.TelefonFuldtOpladet)]
        [TestCase(DisplayMeassage.RFIDFejl)]
        [TestCase(DisplayMeassage.LadeskabOptaget)]
        [TestCase(DisplayMeassage.IndlæsRFID)]
        [TestCase(DisplayMeassage.FjernTelefon)]
        [TestCase(DisplayMeassage.Kortslutning)]
        [TestCase(DisplayMeassage.LadningIgang)]
        public void UpdateText_DisplayMesIsCorrect(DisplayMeassage m)
        {
            //Act
            _uut.UpdateText(m);

            //Assert
            Assert.That(_uut.DisplayMes, Is.EqualTo(m));
        }

        [TestCase()]
        public void UpdateText_(DisplayMeassage m)
        {
            //Act
            _uut.UpdateText(m);

            //Assert
            Assert.That(_uut.DisplayMes, Is.EqualTo(m));
        }
    }
}
