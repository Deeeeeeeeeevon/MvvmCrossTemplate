using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Tests.Builders.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Repos.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Utils;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.ValueObjects;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Repos.EntityRepos.BaseEntityRepo
{
    [TestFixture]
    public class LoadEntitiesBySqlQueryAsyncTests : BaseUnitTest
    {
        [Test]
        public async Task WHEN_DatabaseService_fails_SHOULD_return_Fail_Result()
        {
            //Arrange
            var error = new ErrorBuilder().With_Class("OriginatingErrorClass").Create();
            var databaseServiceResult = new ResultOfTypeBuilder<List<BaseEntity>>()
                .With_IsSuccess(false).With_Error(error)
                .Create();
            var sut = new BaseEntityRepoBuilder()
                .Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns(databaseServiceResult)
                .Create();

            //Act
            var result = await sut.LoadEntitiesBySqlQueryAsync(MyFixture.Create<string>());

            //Assert
            Assert.That(result.IsFailure);
            Assert.That(result.Error.ErrorStack.Count, Is.EqualTo(2));
            Assert.That(result.Error.ErrorStack[1].ClassName, Is.EqualTo("OriginatingErrorClass"));
        }

        [Test]
        public async Task WHEN_DatabaseService_succeeds_SHOULD_return_Success_Result()
        {
            //Arrange
            var databaseServiceResult = new ResultOfTypeBuilder<List<BaseEntity>>()
                .With_IsSuccess(true)
                .With_Value(new BaseEntityBuilder().With_EntityId(new EntityId(123)).CreateList(1))
                .Create();
            var sut = new BaseEntityRepoBuilder()
                .Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns(databaseServiceResult)
                .Create();

            //Act
            var result = await sut.LoadEntitiesBySqlQueryAsync(MyFixture.Create<string>());

            //Assert
            Assert.That(result.IsSuccess);
            Assert.That(result.Value[0].ObjectId, Is.EqualTo(123));
        }
    }
}