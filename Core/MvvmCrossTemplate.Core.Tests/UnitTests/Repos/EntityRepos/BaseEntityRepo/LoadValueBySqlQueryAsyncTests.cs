using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Repos.Entities.Base;
using MvvmCrossTemplate.Core.Tests.Builders.Repos.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Utils;
using MvvmCrossTemplate.Core.Tests.Helpers;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Repos.EntityRepos.BaseEntityRepo
{
    [TestFixture]
    public class LoadValueBySqlQueryAsyncTests : BaseUnitTest
    {
        [Test]
        public async Task WHEN_DatabaseService_fails_SHOULD_return_Fail_Result()
        {
            //Arrange
            var error = new ErrorBuilder().With_Class("OriginatingErrorClass").Create();
            var databaseServiceResult = new ResultOfTypeBuilder<int>()
                .With_IsSuccess(false).With_Error(error).Create();
            var sut = new BaseEntityRepoBuilder()
                .Where_DatabaseService_ExecuteScalarAsync_returns(databaseServiceResult)
                .Create();

            //Act
            Result<int> result = await sut.LoadValueBySqlQueryAsync<int>(RandomValues.String);

            //Assert
            Assert.That(result.IsFailure);
            Assert.That(result.Error.ErrorStack.Count, Is.EqualTo(2));
            Assert.That(result.Error.ErrorStack[1].ClassName, Is.EqualTo("OriginatingErrorClass"));
        }

        [Test]
        public async Task WHEN_DatabaseService_succeeds_SHOULD_return_Success_Result()
        {
            //Arrange
            Result<int> databaseServiceResult = new ResultOfTypeBuilder<int>()
                .With_IsSuccess(true)
                .With_Value(1)
                .Create();
            var sut = new BaseEntityRepoBuilder()
                .Where_DatabaseService_ExecuteScalarAsync_returns(databaseServiceResult)
                .Create();

            //Act
            Result<int> result = await sut.LoadValueBySqlQueryAsync<int>(RandomValues.String);

            //Assert
            Assert.That(result.IsSuccess);
            Assert.That(result.Value, Is.EqualTo(1));
        }
    }
}