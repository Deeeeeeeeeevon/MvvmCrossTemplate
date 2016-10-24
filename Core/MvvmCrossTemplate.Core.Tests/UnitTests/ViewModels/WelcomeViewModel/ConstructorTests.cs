using MvvmCrossTemplate.Core.Resources;
using MvvmCrossTemplate.Core.Tests.Builders.ViewModels;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.WelcomeViewModel
{
    [TestFixture]
    public class ConstructorTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_set_WelcomeString()
        {
            //Arrange
            var sut = new WelcomeViewModelBuilder().Create();

            //Act
            var result = sut.WelcomeString;

            //Assert
            Assert.That(result, Is.EqualTo(UiStrings.WelcomeString));
        }
    }
}