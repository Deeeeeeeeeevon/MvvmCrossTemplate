using MvvmCrossTemplate.Core.Tests.Builders.ViewModels.User;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.User.ListUsersViewModel
{
    [TestFixture]
    public class StartTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_load_data()
        {
            //Arrange
            var sut = (ListUsersViewModelBuilder.Instance) new ListUsersViewModelBuilder().Create();
            sut.LoadDataCommandCalls = 0;

            //Act
            sut.Start();

            //Assert
            Assert.That(sut.LoadDataCommandCalls, Is.EqualTo(1));
        }
    }
}