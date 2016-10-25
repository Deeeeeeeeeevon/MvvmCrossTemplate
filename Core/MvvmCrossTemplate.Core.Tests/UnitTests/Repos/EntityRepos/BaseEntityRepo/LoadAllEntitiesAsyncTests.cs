using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Entities;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Tests.Builders.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Repos.Entities;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Repos.EntityRepos.BaseEntityRepo
{
    [TestFixture]
    public class LoadAllEntitiesAsyncTests : BaseUnitTest
    {
        [Test]
        public async Task SHOULD_load_all_by_sql()
        {
            //Arrange
            var builder = new BaseEntityRepoBuilder();
            var sut = builder.Create();

            //Act
            await sut.LoadAllEntitiesAsync();

            //Assert
            var sql = "SELECT * FROM BaseEntity";
            builder.MockDatabaseService.Verify(x => x.LoadEntitiesBySqlQueryAsync<BaseEntity>(sql));
        }

        [Test]
        public async Task SHOULD_return_entities_loaded_from_database()
        {
            //Arrange
            var entities = new BaseEntityBuilder().CreateList(4);
            var builder = new BaseEntityRepoBuilder().Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns(Result.Ok(entities));
            var sut = builder.Create();

            //Act
            var result = await sut.LoadAllEntitiesAsync();

            //Assert
            Assert.That(result.Value, Is.EqualTo(entities));
        }

        [Test]
        public async Task IF_database_load_fails_SHOULD_retrn_fail_result()
        {
            //Arrange
            var sut = new BaseEntityRepoBuilder()
                .Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns(FailResult<List<BaseEntity>>("oops"))
                .Create();

            //Act
            var result = await sut.LoadAllEntitiesAsync();

            //Assert
            Assert.That(result.Error.SourceError.ClassName, Is.EqualTo("oops"));
        }
    }
}
