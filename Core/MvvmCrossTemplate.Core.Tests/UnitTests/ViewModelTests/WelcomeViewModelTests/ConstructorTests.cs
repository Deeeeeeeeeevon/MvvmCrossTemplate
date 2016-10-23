using MvvmCrossTemplate.Core.Resources;
using MvvmCrossTemplate.Core.Tests.Builders.ViewModelBuilders;
using MvvmCrossTemplate.Core.Tests.UnitTests.ViewModelTests.Base;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModelTests.WelcomeViewModelTests
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