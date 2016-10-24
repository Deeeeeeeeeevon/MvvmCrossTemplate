using System;
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
    public class SaveEntityAsyncTests : BaseUnitTest
    {
        [Test]
        public async Task WHEN_entity_has_no_ObjectId_OR_UniqueId_OR_LocalId_SHOULD_generate_a_UniqueId_before_saving()
        {
            //Arrange
            var entityToInsert = new BaseEntityBuilder().With_EntityId(new EntityId(0, "")).Create();
            var databaseResult = new ResultOfTypeBuilder<List<BaseEntity>>()
                .With_IsSuccess(false)
                .With_Error(new ErrorBuilder().With_ErrorType(ErrorType.NotFound).Create())
                .Create();
            var builder = new BaseEntityRepoBuilder().Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns(databaseResult);
            var sut = builder.Create();

            //Act
            await sut.SaveEntityAsync(entityToInsert);

            //Assert
            builder.MockDatabaseService.Verify(x => x.InsertAsync(It.Is<BaseEntity>(y => y.UniqueId.Length == Guid.NewGuid().ToString().Length)));
        }

        [Test]
        public async Task WHEN_updating_existingEntity_FAILS_SHOULD_return_update_ErrorMessage()
        {
            //Arrange
            var entityToInsert = new BaseEntityBuilder().Create();
            var existingEntity = new BaseEntityBuilder().CreateList(1);
            var loadResult = new ResultOfTypeBuilder<List<BaseEntity>>().With_IsSuccess(true).With_Value(existingEntity).Create();
            var updateResult = new ResultOfTypeBuilder<BaseEntity>().With_IsSuccess(false)
                .With_Error(new ErrorBuilder().With_ErrorType(ErrorType.SaveEntityToDatabase).With_Method("methodToUpdate").Create())
                .Create();

            var sut = new BaseEntityRepoBuilder()
                .Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns(loadResult)
                .Where_DatabaseService_UpdateAsync_returns(updateResult)
                .Create();

            //Act
            var result = await sut.SaveEntityAsync(entityToInsert);

            //Assert
            Assert.That(result.IsFailure);
            Assert.That(result.Error.SourceError.MethodName, Is.EqualTo("methodToUpdate"));
        }

        [Test]
        public async Task WHEN_updating_existingEntity_SUCCEEDS_SHOULD_return_updatedEntity()
        {
            //Arrange
            var entityToInsert = new BaseEntityBuilder().Create();
            var existingEntity = new BaseEntityBuilder().CreateList(1);
            var loadResult = new ResultOfTypeBuilder<List<BaseEntity>>().With_IsSuccess(true).With_Value(existingEntity).Create();
            var updateResult = new ResultOfTypeBuilder<BaseEntity>().With_IsSuccess(true).With_Value(entityToInsert).Create();

            var sut = new BaseEntityRepoBuilder()
                .Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns(loadResult)
                .Where_DatabaseService_UpdateAsync_returns(updateResult)
                .Create();

            //Act
            var result = await sut.SaveEntityAsync(entityToInsert);

            //Assert
            Assert.That(result.IsSuccess);
            Assert.That(result.Value.EntityId.ObjectId, Is.EqualTo(entityToInsert.ObjectId));
        }

        [Test]
        public async Task WHEN_entity_exists_SHOULD_update_using_correct_Id()
        {
            //Arrange
            var entityToInsert = new BaseEntityBuilder().Create();
            var existingEntity = new BaseEntityBuilder().CreateList(1);
            var loadResult = new ResultOfTypeBuilder<List<BaseEntity>>().With_IsSuccess(true).With_Value(existingEntity).Create();
            var updateResult = new ResultOfTypeBuilder<BaseEntity>().With_IsSuccess(true).With_Value(entityToInsert).Create();

            var builder = new BaseEntityRepoBuilder()
                .Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns(loadResult)
                .Where_DatabaseService_UpdateAsync_returns(updateResult);
            var sut = builder.Create();

            //Act
            await sut.SaveEntityAsync(entityToInsert);

            //Assert
            builder.MockDatabaseService.Verify(x => x.UpdateAsync(It.Is<BaseEntity>(y => y.LocalId == existingEntity[0].LocalId)));
        }

    }
}