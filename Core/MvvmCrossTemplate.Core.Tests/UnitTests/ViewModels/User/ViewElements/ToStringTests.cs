using MvvmCrossTemplate.Core.Tests.Builders.ViewModels.User.ViewElements;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.User.ViewElements
{
    [TestFixture]
    public class ToStringTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_return_full_name()
        {
            //Arrange
            var sut = new UserListItemViewElementBuilder()
                .With_FirstName("bob")
                .With_LastName("freever")
                .Create();

            //Act
            var result = sut.ToString();

            //Assert
            Assert.That(result, Is.EqualTo("bob freever"));
        }
    }
}