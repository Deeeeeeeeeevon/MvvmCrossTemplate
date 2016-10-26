using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Tests.Builders.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Repos.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Utils;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Repos.EntityRepos.BaseEntityRepo
{

    [TestFixture]
    public class LoadEntityAsyncTests : BaseUnitTest
    {
        [Test]
        public async Task WHEN_EntityId_includes_all_3_Ids_SHOULD_query_on_all_3()
        {
            //Arrange
            var builder = new BaseEntityRepoBuilder();
            var sut = builder.Create();

            //Act
            await sut.LoadEntityAsync(new EntityId(33, "abc", 9));

            //Assert
            const string expectedSql = "SELECT * FROM BaseEntity WHERE Id = 9 OR ObjectId = 33 OR UniqueId = \'abc\'";
            builder.MockDatabaseService.Verify(x => x.LoadEntitiesBySqlQueryAsync<BaseEntity>(expectedSql));
        }

        [Test]
        public async Task When_EntityId_IsEmpty_SHOULD_NOT_attempt_to_load_From_Database()
        {
            //Arrange
            var builder = new BaseEntityRepoBuilder();
            var sut = builder.Create();

            //Act
            await sut.LoadEntityAsync(new EntityIdBuilder().With_Ids().Create());

            //Assert
            builder.MockDatabaseService.Verify(x => x.LoadEntitiesBySqlQueryAsync<BaseEntity>(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task When_EntityId_IsEmpty_SHOULD_return_Fail_with_NotFound_error()
        {
            //Arrange
            var builder = new BaseEntityRepoBuilder();
            var sut = builder.Create();

            //Act
            var result = await sut.LoadEntityAsync(new EntityIdBuilder().With_Ids().Create());

            //Assert
            Assert.That(result.IsFailure);
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.NotFound));
        }

        [Test]
        public async Task WHEN_EntityId_includes_local_Id_and_ObjectId_SHOULD_query_on_only_those_2()
        {
            //Arrange
            var builder = new BaseEntityRepoBuilder();
            var sut = builder.Create();

            //Act
            await sut.LoadEntityAsync(new EntityId(33, "", 9));

            //Assert
            const string expectedSql = "SELECT * FROM BaseEntity WHERE Id = 9 OR ObjectId = 33";
            builder.MockDatabaseService.Verify(x => x.LoadEntitiesBySqlQueryAsync<BaseEntity>(expectedSql));
        }

        [Test]
        public async Task WHEN_EntityId_includes_local_Id_and_UniqueId_SHOULD_query_on_only_those_2()
        {
            //Arrange
            var builder = new BaseEntityRepoBuilder();
            var sut = builder.Create();

            //Act
            await sut.LoadEntityAsync(new EntityId(0, "abc", 9 ));

            //Assert
            const string expectedSql = "SELECT * FROM BaseEntity WHERE Id = 9 OR UniqueId = \'abc\'";
            builder.MockDatabaseService.Verify(x => x.LoadEntitiesBySqlQueryAsync<BaseEntity>(expectedSql));
        }

        [Test]
        public async Task WHEN_EntityId_includes_ObjectId_and_UniqueId_SHOULD_query_on_only_those_2()
        {
            //Arrange
            var builder = new BaseEntityRepoBuilder();
            var sut = builder.Create();

            //Act
            await sut.LoadEntityAsync(new EntityId(12, "abc"));

            //Assert
            const string expectedSql = "SELECT * FROM BaseEntity WHERE ObjectId = 12 OR UniqueId = \'abc\'";
            builder.MockDatabaseService.Verify(x => x.LoadEntitiesBySqlQueryAsync<BaseEntity>(expectedSql));
        }

        [Test]
        public async Task WHEN_EntityId_includes_only_ObjectId_SHOULD_query_only_it()
        {
            //Arrange
            var builder = new BaseEntityRepoBuilder();
            var sut = builder.Create();

            //Act
            await sut.LoadEntityAsync(new EntityId(12, ""));

            //Assert
            const string expectedSql = "SELECT * FROM BaseEntity WHERE ObjectId = 12";
            builder.MockDatabaseService.Verify(x => x.LoadEntitiesBySqlQueryAsync<BaseEntity>(expectedSql));
        }

        [Test]
        public async Task WHEN_EntityId_includes_only_UniqueId_SHOULD_query_only_it()
        {
            //Arrange
            var builder = new BaseEntityRepoBuilder();
            var sut = builder.Create();

            //Act
            await sut.LoadEntityAsync(new EntityId(0, "abc"));

            //Assert
            const string expectedSql = "SELECT * FROM BaseEntity WHERE UniqueId = \'abc\'";
            builder.MockDatabaseService.Verify(x => x.LoadEntitiesBySqlQueryAsync<BaseEntity>(expectedSql));
        }

        [Test]
        public async Task WHEN_EntityId_includes_only_LocalId_SHOULD_query_only_it()
        {
            //Arrange
            var builder = new BaseEntityRepoBuilder();
            var sut = builder.Create();

            //Act
            await sut.LoadEntityAsync(new EntityId(0, "", 12));

            //Assert
            const string expectedSql = "SELECT * FROM BaseEntity WHERE Id = 12";
            builder.MockDatabaseService.Verify(x => x.LoadEntitiesBySqlQueryAsync<BaseEntity>(expectedSql));
        }

        [Test]
        public async Task WHEN_DatabaseService_load_fails_SHOULD_return_Fail_with_error_message()
        {
            //Arrange
            Result<List<BaseEntity>> databaseServiceResult = new ResultOfTypeBuilder<List<BaseEntity>>()
                .With_Error(new ErrorBuilder().With_Class("errorClass").Create())
                .With_IsSuccess(false).Create();
            var sut = new BaseEntityRepoBuilder()
                .Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns(databaseServiceResult)
                .Create();

            //Act
            var result = await sut.LoadEntityAsync(new EntityId(0, "", 12 ));

            //Assert
            Assert.That(result.IsFailure);
            Assert.That(result.Error.PreviousError.ClassName, Is.EqualTo("errorClass"));
        }

        [Test]
        public async Task WHEN_DatabaseService_load_succeeds_with_multiple_entities_SHOULD_return_Fail_with_multipleResult_error()
        {
            //Arrange
            var entityId = new EntityId(0, "", 12);
            Result<List<BaseEntity>> databaseServiceResult = new ResultOfTypeBuilder<List<BaseEntity>>().With_IsSuccess(true)
                .With_Value(new BaseEntityBuilder().CreateList(4)).Create();
            var builder = new BaseEntityRepoBuilder()
                .Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns(databaseServiceResult);
            var sut = builder.Create();

            //Act
            var result = await sut.LoadEntityAsync(entityId);

            //Assert
            Assert.That(result.IsFailure);
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.DuplicateResults));
            Assert.That(result.Error.AdditionalData["entityId"], Is.EqualTo(entityId.FullId));
        }

        [Test]
        public async Task WHEN_DatabaseService_load_succeeds_with_0_entities_SHOULD_return_Fail_with_NotFound_error()
        {
            //Arrange
            var entityId = new EntityId(0, "", 12);
            Result<List<BaseEntity>> databaseServiceResult = new ResultOfTypeBuilder<List<BaseEntity>>().With_IsSuccess(true)
                .With_Value(new BaseEntityBuilder().CreateList(0)).Create();
            var builder = new BaseEntityRepoBuilder()
                .Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns(databaseServiceResult);
            var sut = builder.Create();

            //Act
            Result<BaseEntity> result = await sut.LoadEntityAsync(entityId);

            //Assert
            Assert.That(result.IsFailure);
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.NotFound));
            Assert.That(result.Error.AdditionalData["entityId"], Is.EqualTo(entityId.FullId));
        }


        [Test]
        public async Task WHEN_DatabaseService_load_succeeds_with_single_entity_SHOULD_return_Succeed_with_loaded_entity()
        {
            //Arrange
            List<BaseEntity> entities = new BaseEntityBuilder().CreateList(1);
            Result<List<BaseEntity>> databaseServiceResult = new ResultOfTypeBuilder<List<BaseEntity>>().With_IsSuccess(true)
                .With_Value(entities).Create();
            var builder = new BaseEntityRepoBuilder()
                .Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns(databaseServiceResult);
            var sut = builder.Create();

            //Act
            Result<BaseEntity> result = await sut.LoadEntityAsync(new EntityId(0, "", 12 ));

            //Assert
            Assert.That(result.IsSuccess);
            Assert.That(result.Value, Is.InstanceOf<BaseEntity>());
            Assert.That(result.Value.EntityId.ObjectId, Is.EqualTo(entities[0].ObjectId));
        }
    }
}