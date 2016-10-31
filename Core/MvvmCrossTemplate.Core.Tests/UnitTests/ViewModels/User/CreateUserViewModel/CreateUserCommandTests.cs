using System.Threading;
using System.Threading.Tasks;
using Moq;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.ViewModels.User;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.User.CreateUserViewModel
{
    [TestFixture]
    public class CreateUserCommandTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_create_new_UserModel_in_repo()
        {
            //Arrange
            var builder = new CreateUserViewModelBuilder();
            var sut = builder.Create();

            //Act
            sut.CreateUserCommand.Execute(null);

            //Assert
            builder.MockUserModelRepo.Verify(x => x.CreateNewUserModel(), Times.Once);
        }

        [Test]
        public void SHOULD_update_User_name_from_viewmodel_properties()
        {
            //Arrange
            var userModelBuilder = new UserModelBuilder();
            var mockUserModel = userModelBuilder.MockUserModel;
            var builder = new CreateUserViewModelBuilder().Where_UserModelRepo_CreateNewUser_returns(mockUserModel.Object);
            var sut = builder.Create();
            sut.FirstName = "bob";
            sut.LastName = "freever";

            //Act
            sut.CreateUserCommand.Execute(null);

            //Assert
            mockUserModel.Verify(x => x.UpdateUserName("bob", "freever"), Times.Once);
        }

        [Test]
        public void IF_updating_User_name_fails_SHOULD_HandleError()
        {
            //Arrange
            var mock = new Mock<IUserModel>();
            mock.Setup(x => x.UpdateUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(Result.Fail(this, ErrorType.ConnectToDatabase));
            var ss = mock.Object.UpdateUserName("a", "a");
            var mockUserModel = new UserModelBuilder().Where_UpdateUserName_returns(Result.Fail(this, ErrorType.ConnectToDatabase)).CreateMock();
            var builder = new CreateUserViewModelBuilder().Where_UserModelRepo_CreateNewUser_returns(mockUserModel);
            var sut = (CreateUserViewModelBuilder.Instance) builder.Create();
            sut.HandleErrorCalls = 0;

            //Act
            sut.CreateUserCommand.Execute(null);

            //Assert
            Assert.That(sut.HandleErrorCalls, Is.EqualTo(1));
            Assert.That(sut.LoggedError.ErrorType, Is.EqualTo(ErrorType.ConnectToDatabase));
        }

        [Test]
        public void SHOULD_save_user_model()
        {
            //Arrange
            var userModel = new UserModelBuilder().Create();
            var builder = new CreateUserViewModelBuilder().Where_UserModelRepo_CreateNewUser_returns(userModel);
            var sut = (CreateUserViewModelBuilder.Instance)builder.Create();

            //Act
            sut.CreateUserCommand.Execute(null);

            //Assert
            builder.MockUserModelRepo.Verify(x=> x.SaveUserModelAsync(It.IsAny<CancellationToken>(), userModel));
        }

        [Test]
        public void IF_saving_user_model_fails_SHOULD_HandleError()
        {
            //Arrange
            var builder = new CreateUserViewModelBuilder().Where_UserModelRepo_SaveUserModelAsync_returns(FailResult<IUserModel>("oops"));
            var sut = (CreateUserViewModelBuilder.Instance)builder.Create();
            sut.HandleErrorCalls = 0;

            //Act
            sut.CreateUserCommand.Execute(null);

            //Assert
            Assert.That(sut.HandleErrorCalls, Is.EqualTo(1));
            Assert.That(sut.LoggedError.SourceError.ClassName, Is.EqualTo("oops"));
        }

        [Test]
        public void WHEN_saving_succeeds_SHOULD_go_to_UserList()
        {
            //Arrange
            var savedUser = new UserModelBuilder().CreateMock();
            var builder = new CreateUserViewModelBuilder().Where_UserModelRepo_SaveUserModelAsync_returns(Result.Ok(savedUser));
            var sut = (CreateUserViewModelBuilder.Instance)builder.Create();

            //Act
            sut.CreateUserCommand.Execute(null);

            //Assert
            Assert.That(MockViewDispatcher.Requests[0].ViewModelType, Is.EqualTo(typeof(Core.ViewModels.User.ListUsersViewModel)));
        }
    }
}