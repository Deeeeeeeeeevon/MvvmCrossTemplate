using System;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Entities;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Repos.Models;
using MvvmCrossTemplate.Core.Tests.Builders.Utils;
using MvvmCrossTemplate.Core.Tests.Helpers;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Repos.ModelRepos.UserModelRepo
{
    [TestFixture]
    public class LoadUserModelAsyncTests : BaseUnitTest
    {

        [Test]
        public async Task IF_loading_UserEntity_fails_SHOULD_fail()
        {
            //Arrange
            var sut = new UserModelRepoBuilder()
                .Where_UserEntityRepo_LoadEntityAsync_returns(FailResult<UserEntity>("oops"))
                .Create();

            //Act
            var result = await sut.LoadUserModelAsync(RandomValues.EntityId);

            //Assert
            Assert.That(result.IsFailure);
            Assert.That(result.Error.SourceError.ClassName, Is.EqualTo("oops"));
        }

        [Test]
        public async Task WHEN_UserEntity_does_not_already_exist_SHOULD_return_new_one_with_blan_id()
        {
            //Arrange
            var sut = new UserModelRepoBuilder()
                .Where_UserEntityRepo_LoadEntityAsync_returns(FailResult<UserEntity>(ErrorType.NotFound))
                .Create();

            //Act
            var result = await sut.LoadUserModelAsync(RandomValues.EntityId);

            //Assert
            Assert.That(result.Value.EntityId.UniqueId, Is.EqualTo(""));
            Assert.That(result.Value.EntityId.ObjectId, Is.EqualTo(0));
            Assert.That(result.Value.EntityId.LocalId, Is.EqualTo(0));
        }

        [Test]
        public async Task SHOULD_populate_EntityId()
        {
            //Arrange
            var userEntity = new UserEntityBuilder().Create();
            var sut = new UserModelRepoBuilder()
                .Where_UserEntityRepo_LoadEntityAsync_returns(Result.Ok(userEntity))
                .Create();

            //Act
            var result = await sut.LoadUserModelAsync(RandomValues.EntityId);

            //Assert
            Assert.That(result.Value.EntityId, Is.EqualTo(userEntity.EntityId));
        }

        [Test]
        public async Task SHOULD_populate_FirstName()
        {
            //Arrange
            var userEntity = new UserEntityBuilder().With_FirstName("bob").Create();
            var sut = new UserModelRepoBuilder()
                .Where_UserEntityRepo_LoadEntityAsync_returns(Result.Ok(userEntity))
                .Create();

            //Act
            var result = await sut.LoadUserModelAsync(RandomValues.EntityId);

            //Assert
            Assert.That(result.Value.PersonalDetails.FirstName, Is.EqualTo("bob"));
        }

        [Test]
        public async Task SHOULD_populate_LastName()
        {
            //Arrange
            var userEntity = new UserEntityBuilder().With_LastName("freever").Create();
            var sut = new UserModelRepoBuilder()
                .Where_UserEntityRepo_LoadEntityAsync_returns(Result.Ok(userEntity))
                .Create();

            //Act
            var result = await sut.LoadUserModelAsync(RandomValues.EntityId);

            //Assert
            Assert.That(result.Value.PersonalDetails.LastName, Is.EqualTo("freever"));
        }
    }
}