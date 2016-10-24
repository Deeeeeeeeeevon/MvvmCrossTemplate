using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Tests.Builders.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Models.User;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Entities.UserEntity
{
    [TestFixture]
    public class UpdateFromUserModelTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_set_FirstName()
        {
            //Arrange
            var personalDetails = new PersonalDetailsBuilder().With_FirstName("bob").CreateMock();
            var userModel = new UserModelBuilder().With_PersonalDetails(personalDetails).Create();
            var sut = new UserEntityBuilder().With_FirstName("notBob").Create();

            //Act
            sut.UpdateFromUserModel(userModel);

            //Assert
            Assert.That(sut.FirstName, Is.EqualTo("bob"));
        }

        [Test]
        public void SHOULD_set_LastName()
        {
            //Arrange
            var personalDetails = new PersonalDetailsBuilder().With_LastName("squirrel").CreateMock();
            var userModel = new UserModelBuilder().With_PersonalDetails(personalDetails).Create();
            var sut = new UserEntityBuilder().With_LastName("notSquirrel").Create();

            //Act
            sut.UpdateFromUserModel(userModel);

            //Assert
            Assert.That(sut.LastName, Is.EqualTo("squirrel"));
        }
    }
}   