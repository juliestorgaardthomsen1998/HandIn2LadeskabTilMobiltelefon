using LadeskabClassLib.LogFile;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.NUnit.test
{
    [TestFixture]
    public class TimeProviderUnitTest
    {
        private TimeProvider uut;

        [SetUp]
        public void Setup()
        {
            //Arrange
            uut = new TimeProvider();
        }

        [Test]
        public void GetTime_RealTimeIsEqualToTimeFromMethod()
        {
            //Act
            string s = uut.GetTime();

            //Assert
            Assert.That(s,Is.EqualTo(System.Convert.ToString(System.DateTime.Now)));
        }
    }
}
