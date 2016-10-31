using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.ViewModels.User;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.User.ListUsersViewModel
{
    [TestFixture]
    public class LoadDataCommandTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_load_all_UserModels()
        {
            //Arrange
            var builder = new ListUsersViewModelBuilder();
            var sut = builder.Create();

            //Act
            sut.LoadDataCommand.Execute(null);

            //Assert
            builder.MockUserModelRepo.Verify(x => x.LoadAllUserModelsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void SHOULD_convert_loaded_UserModel_into_UserListItemViewElement()
        {
            //Arrange
            var personalDetails = new PersonalDetailsBuilder().With_FirstName("bob").With_LastName("freever").Create();
            var userModel = new UserModelBuilder().With_PersonalDetails(personalDetails).CreateMock();
            var builder = new ListUsersViewModelBuilder().Where_UserModelRepo_LoadAllUserModelsAsync_returns(Result.Ok(new List<IUserModel> { userModel}));
            var sut = builder.Create();

            //Act
            sut.LoadDataCommand.Execute(null);

            //Assert
            Assert.That(sut.UserList.Count, Is.EqualTo(1));
            Assert.That(sut.UserList[0].FirstName, Is.EqualTo("bob"));
            Assert.That(sut.UserList[0].LastName, Is.EqualTo("freever"));
        }

        [Test]
        public void SHOULD_raise_property_changed_on_UserList()
        {
            //Arrange
            var personalDetails = new PersonalDetailsBuilder().With_FirstName("bob").With_LastName("freever").Create();
            var userModel = new UserModelBuilder().With_PersonalDetails(personalDetails).CreateMock();
            var builder =  new ListUsersViewModelBuilder().Where_UserModelRepo_LoadAllUserModelsAsync_returns(Result.Ok(new List<IUserModel> { userModel }));
            var sut = (ListUsersViewModelBuilder.Instance) builder.Create();
            sut.PropertiesChanged["UserList"] = 0;

            //Act
            sut.LoadDataCommand.Execute(null);

            //Assert
            Assert.That(sut.PropertiesChanged["UserList"], Is.EqualTo(1));
        }

        [Test]
        public void WHEN_viewmodel_is_busy_SHOULD_not_load_UserModels()
        {
            //Arrange
            var userModel = new UserModelBuilder().CreateMock();
            var builder = new ListUsersViewModelBuilder().Where_UserModelRepo_LoadAllUserModelsAsync_returns(Result.Ok(new List<IUserModel> { userModel }));
            var sut = (ListUsersViewModelBuilder.Instance)builder.Create();
            sut.IsBusy = true;

            //Act
            sut.LoadDataCommand.Execute(null);

            //Assert
            builder.MockUserModelRepo.Verify(x => x.LoadAllUserModelsAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public void SHOULD_call_StartLoading()
        {
            //Arrange
            var userModel = new UserModelBuilder().CreateMock();
            var builder = new ListUsersViewModelBuilder().Where_UserModelRepo_LoadAllUserModelsAsync_returns(Result.Ok(new List<IUserModel> { userModel }));
            var sut = (ListUsersViewModelBuilder.Instance)builder.Create();
            sut.StartLoadingCalls = 0;

            //Act
            sut.LoadDataCommand.Execute(null);

            //Assert
            Assert.That(sut.StartLoadingCalls, Is.EqualTo(1));
        }

        [Test]
        public void IF_loading_UserModels_fails_SHOULD_handle_error()
        {
            //Arrange
            var builder = new ListUsersViewModelBuilder().Where_UserModelRepo_LoadAllUserModelsAsync_returns(FailResult<List<IUserModel>>("oops"));
            var sut = (ListUsersViewModelBuilder.Instance)builder.Create();
            sut.HandleErrorCalls = 0;

            //Act
            sut.LoadDataCommand.Execute(null);

            //Assert
            Assert.That(sut.HandleErrorCalls, Is.EqualTo(1));
            Assert.That(sut.LoggedError.SourceError.ClassName, Is.EqualTo("oops"));
        }
    }
}