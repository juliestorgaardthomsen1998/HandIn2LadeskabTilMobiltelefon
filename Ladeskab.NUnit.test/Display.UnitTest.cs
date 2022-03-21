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

        //[Test]
        //public void ctor_DisplayMesIsNull()
        //{
        //    DisplayMeassage d = 0;

        //    Assert.That(uut.DisplayMes, Is.EqualTo(uut.DisplayMes));
        //}

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

        //[Test]
        //public void UpdateText_TestIfDefaultMessageIsCorrect_()
        //{

        //}
    }
}
