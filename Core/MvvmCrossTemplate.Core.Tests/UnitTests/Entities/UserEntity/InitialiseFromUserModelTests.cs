using System;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Tests.Builders.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.Utils;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Entities.UserEntity
{
    [TestFixture]
    public class InitialiseFromUserModelTests : BaseUnitTest
    {

        [TestFixture]
        public class ConstructFromUserModel : BaseUnitTest
        {
            [Test]
            public void SHOULD_set_FirstName()
            {
                //Arrange
                var personalDetails = new PersonalDetailsBuilder().With_FirstName("bob").CreateMock();
                var userModel = new UserModelBuilder().With_PersonalDetails(personalDetails).Create();
                var sut = new Core.Entities.UserEntity();

                //Act
                sut.InitialiseFromUserModel(userModel);

                //Assert
                Assert.That(sut.FirstName, Is.EqualTo("bob"));
            }

            [Test]
            public void SHOULD_set_LastName()
            {
                //Arrange
                var personalDetails = new PersonalDetailsBuilder().With_LastName("squirrel").CreateMock();
                var userModel = new UserModelBuilder().With_PersonalDetails(personalDetails).Create();
                var sut = new Core.Entities.UserEntity();

                //Act
                sut.InitialiseFromUserModel(userModel);

                //Assert
                Assert.That(sut.LastName, Is.EqualTo("squirrel"));
            }

            [Test]
            public void SHOULD_create_new_UniqueId()
            {
                //Arrange
                var userModel = new UserModelBuilder().With_EntityId(new EntityIdBuilder().Create()).Create();
                var sut = new Core.Entities.UserEntity();

                //Act
                sut.InitialiseFromUserModel(userModel);

                //Assert
                Assert.That(sut.UniqueId.Length, Is.EqualTo(Guid.NewGuid().ToString().Length));
                Assert.That(sut.UniqueId.Length, Is.Not.EqualTo(userModel.EntityId.UniqueId));
            }
        }
    }
}