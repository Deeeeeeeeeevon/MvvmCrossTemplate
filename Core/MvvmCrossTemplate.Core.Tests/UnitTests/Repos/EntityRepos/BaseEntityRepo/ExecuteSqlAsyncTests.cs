using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Tests.Builders.Repos.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Utils;
using MvvmCrossTemplate.Core.Tests.Helpers;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Repos.EntityRepos.BaseEntityRepo
{
    [TestFixture]
    public class ExecuteSqlAsyncTests : BaseUnitTest
    {
        [Test]
        public async Task WHEN_DatabaseService_fails_SHOULD_return_Fail_Result()
        {
            //Arrange
            Error error = new ErrorBuilder().Create();
            Result databaseServiceResult = new ResultBuilder()
                .With_Sender(new BaseEntityRepoBuilder())
                .With_IsSuccess(false).With_Error(error)
                .Create();
           var sut = new BaseEntityRepoBuilder()
                .Where_DatabaseService_ExecuteSqlAsync_returns(databaseServiceResult)
                .Create();

            //Act
            Result result = await sut.ExecuteSqlAsync(RandomValues.String);

            //Assert
            Assert.That(result.IsFailure);
            Assert.That(result.Error.ErrorStack.Count, Is.EqualTo(2));
            Assert.That(result.Error.ErrorStack[1].ClassName, Is.EqualTo(typeof(BaseEntityRepoBuilder).Name));
        }

        [Test]
        public async Task WHEN_DatabaseService_succeeds_SHOULD_return_Success_Result()
        {
            //Arrange
            Result<int> databaseServiceResult = new ResultOfTypeBuilder<int>()
                .With_IsSuccess(true).Create();
            var sut = new BaseEntityRepoBuilder()
                .Where_DatabaseService_ExecuteSqlAsync_returns(databaseServiceResult)
                .Create();

            //Act
            Result result = await sut.ExecuteSqlAsync(RandomValues.String);

            //Assert
            Assert.That(result.IsSuccess);
        }
    }
}