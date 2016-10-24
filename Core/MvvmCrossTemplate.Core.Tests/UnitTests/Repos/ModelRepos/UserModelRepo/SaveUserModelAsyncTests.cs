using System.Threading.Tasks;
using Moq;
using MvvmCrossTemplate.Core.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.Repos.Models;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Repos.ModelRepos.UserModelRepo
{
    [TestFixture]
    public class SaveUserModelAsyncTests : BaseUnitTest
    {
        [Test]
        public async Task SHOULD_save_existing_UserEntity_if_it_exists()
        {
            //Arrange
            var existingUserEntity = new UserEntityBuilder().Create();
            var builder = new UserModelRepoBuilder()
                .Where_UserEntityRepo_LoadEntityAsync_returns(Result.Ok(existingUserEntity));
            var sut = builder.Create();

            //Act
            await sut.SaveUserModelAsync(new UserModelBuilder().Create());

            //Assert
            builder.MockUserEntityRepo.Verify(x => x.SaveEntityAsync(It.Is<UserEntity>(
                y => y.EntityId == existingUserEntity.EntityId)));

        }

        [Test]
        public async Task SHOULD_update_FirstName()
        {
            //Arrange
            var personalDetails = new PersonalDetailsBuilder()
                .With_FirstName("newFirstName")
                .Create();
            var userModel = new UserModelBuilder()
                .With_PersonalDetails(personalDetails)
                .Create();
            var existingUserEntity = new UserEntityBuilder()
                .With_FirstName("oldFirstName")
                .Create();
            var builder = new UserModelRepoBuilder()
                .Where_UserEntityRepo_LoadEntityAsync_returns(Result.Ok(existingUserEntity));
            var sut = builder.Create();

            //Act
            await sut.SaveUserModelAsync(userModel);

            //Assert
            builder.MockUserEntityRepo.Verify(x => x.SaveEntityAsync(It.Is<UserEntity>(
                y => y.FirstName == "newFirstName")));

        }

        [Test]
        public async Task SHOULD_update_LastName()
        {
            //Arrange
            var personalDetails = new PersonalDetailsBuilder()
                .With_LastName("newLastName")
                .Create();
            var userModel = new UserModelBuilder()
                .With_PersonalDetails(personalDetails)
                .Create();
            var existingUserEntity = new UserEntityBuilder()
                .With_LastName("oldLastName")
                .Create();
            var builder = new UserModelRepoBuilder()
                .Where_UserEntityRepo_LoadEntityAsync_returns(Result.Ok(existingUserEntity));
            var sut = builder.Create();

            //Act
            await sut.SaveUserModelAsync(userModel);

            //Assert
            builder.MockUserEntityRepo.Verify(x => x.SaveEntityAsync(It.Is<UserEntity>(
                y => y.LastName == "newLastName")));

        }

        [Test]
        public async Task IF_loading_existing_UserEntity_fails_SHOULD_fail()
        {
            //Arrange
            var userModel = new UserModelBuilder()
                .Create();
            var builder = new UserModelRepoBuilder()
                .Where_UserEntityRepo_LoadEntityAsync_returns(FailResult<UserEntity>("oops"));
            var sut = builder.Create();

            //Act
            var result = await sut.SaveUserModelAsync(userModel);

            //Assert
            Assert.That(result.Error.SourceError.ClassName, Is.EqualTo("oops"));


        }

        [Test]
        public async Task IF_saving_UserEntity_fails_SHOULD_fail()
        {
            //Arrange
            var userModel = new UserModelBuilder()
                .Create();
            var builder = new UserModelRepoBuilder()
                .Where_UserEntityRepo_SaveEntityAsync_returns(FailResult<UserEntity>("oops"));
            var sut = builder.Create();

            //Act
            var result = await sut.SaveUserModelAsync(userModel);

            //Assert
            Assert.That(result.Error.SourceError.ClassName, Is.EqualTo("oops"));

        }
    }
}