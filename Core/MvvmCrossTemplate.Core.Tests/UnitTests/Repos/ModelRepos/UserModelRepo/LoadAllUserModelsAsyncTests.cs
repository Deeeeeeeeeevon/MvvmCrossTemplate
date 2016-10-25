using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MvvmCrossTemplate.Core.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.Repos.Models;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Repos.ModelRepos.UserModelRepo
{
    [TestFixture]
    public class LoadAllUserModelsAsyncTests : BaseUnitTest
    {
        [Test]
        public async Task SHOULD_return_UserModels_for_each_loaded_UserEntity()
        {
            //Arrange
            var userEntities = new UserEntityBuilder().CreateList(2);
            var sut = new UserModelRepoBuilder().Where_UserEntityRepo_LoadAllEntitiesAsync_returns(Result.Ok(userEntities))
                .Create();

            //Act
            var result = await sut.LoadAllUserModelsAsync(CancelToken);

            //Assert
            Assert.That(result.Value.Count, Is.EqualTo(2));
            Assert.That(result.Value[0].EntityId, Is.EqualTo(userEntities[0].EntityId));
            Assert.That(result.Value[1].EntityId, Is.EqualTo(userEntities[1].EntityId));
        }

        [Test]
        public async Task IF_loading_all_UserEntities_fails_SHOULD_fail()
        {
            //Arrange
            var sut = new UserModelRepoBuilder().Where_UserEntityRepo_LoadAllEntitiesAsync_returns(FailResult<List<UserEntity>>("oops"))
                .Create();

            //Act
            var result = await sut.LoadAllUserModelsAsync(CancelToken);

            //Assert
            Assert.That(result.Error.SourceError.ClassName, Is.EqualTo("oops"));
        }

        [Test]
        public async Task IF_token_is_cancelled_SHOULD_fail()
        {
            //Arrange
            var sut = new UserModelRepoBuilder().Create();
            TestCancellationTokenSource.Cancel();

            //Act
            var result = await sut.LoadAllUserModelsAsync(CancelToken);

            //Assert
            Assert.That(result.Error.SourceError.ErrorType, Is.EqualTo(ErrorType.Cancelled));
        }

        [Test]
        public async Task IF_token_is_cancelled_SHOULD_not_load_entities()
        {
            //Arrange
            var builder = new UserModelRepoBuilder();
            var sut = builder.Create();
            TestCancellationTokenSource.Cancel();

            //Act
            var result = await sut.LoadAllUserModelsAsync(CancelToken);

            //Assert
            builder.MockUserEntityRepo.Verify(x => x.LoadAllEntitiesAsync(), Times.Never);
        }
    }
}