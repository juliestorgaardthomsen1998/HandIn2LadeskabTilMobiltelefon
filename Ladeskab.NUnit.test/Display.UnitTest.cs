using LadeskabClassLib.Display;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.NUnit.test
{
    [TestFixture]
    public class DisplayUnitTest
    {
        private IDisplay uut;

        [SetUp]
        public void Setup()
        {
            uut = new Display();
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
            uut.UpdateText(m);

            //Assert
            Assert.That(uut.DisplayMes, Is.EqualTo(m));
        }

        [Test]
        public void UpdateText_TestIfDefaultMessageIsCorrect_()
        {
            //Act
            uut.UpdateText(uut.DisplayMes = 0);

            //Assert
            Assert.That(uut.DisplayMes,Is.EqualTo(default(DisplayMeassage)));
        }
    }
}
