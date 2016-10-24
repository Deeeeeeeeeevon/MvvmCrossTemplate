using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Tests.Builders.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Repos.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Utils;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Repos.EntityRepos.BaseEntityRepo
{
    [TestFixture]
    public class InsertEntityAsyncTests : BaseUnitTest
    {
        [Test]
        public async Task WHEN_DatabaseService_Insert_fails_SHOULD_return_Fail_Result()
        {
            //Arrange
            Error error = new ErrorBuilder().With_Class("OriginatingErrorClass").Create();
            var insertResult = new ResultOfTypeBuilder<BaseEntity>()
                .With_IsSuccess(false).With_Error(error)
                .Create();
            var sut = new BaseEntityRepoBuilder()
                .Where_DatabaseService_InsertAsync_returns(insertResult)
                .Create();

            //Act
            Result<BaseEntity> result = await sut.InsertEntityAsync(new BaseEntityBuilder().Create());

            //Assert
            Assert.That(result.IsFailure);
            Assert.That(result.Error.ErrorStack.Count, Is.EqualTo(2));
            Assert.That(result.Error.ErrorStack[1].ClassName, Is.EqualTo("OriginatingErrorClass"));
        }

        [Test]
        public async Task WHEN_DatabaseService_Insert_succeeds_SHOULD_return_Success_Result_with_inserted_entity()
        {
            //Arrange
            Result<BaseEntity> insertResult = new ResultOfTypeBuilder<BaseEntity>()
                .With_IsSuccess(true)
                .With_Value(new BaseEntityBuilder().With_Id(123).Create())
                .Create();
            var sut = new BaseEntityRepoBuilder()
                .Where_DatabaseService_InsertAsync_returns(insertResult)
                .Create();

            //Act
            var result = await sut.InsertEntityAsync(new BaseEntityBuilder().Create());

            //Assert
            Assert.That(result.IsSuccess);
            Assert.That(result.Value.Id, Is.EqualTo(123));
        }
    }
}