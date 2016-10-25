using MvvmCrossTemplate.Core.Tests.Builders.ViewModels;
using MvvmCrossTemplate.Core.Tests.Builders.ViewModels.User;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;
using MvvmCrossTemplate.Core.ViewModels.Error;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.BaseViewModel
{
    [TestFixture]
    public class HandleErrorTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_LogError()
        {
            //Arrange
            var originalErrorSender = new ListUsersViewModelBuilder().Create();
            var error = Error.Create(originalErrorSender, ErrorType.Crash);
            var sut = (BaseViewModelBuilder.Instance) new BaseViewModelBuilder().Create();

            //Act
            sut.HandleError(originalErrorSender, error);

            //Assert
            Assert.That(sut.LoggedError.ErrorType, Is.EqualTo(ErrorType.Crash));
        }

        [Test]
        public void SHOULD_Update_error_before_logging_it()
        {
            //Arrange
            var originalErrorSender = new ListUsersViewModelBuilder().Create();
            var newSender = new ErrorViewModel();
            var error = Error.Create(originalErrorSender, ErrorType.Crash);
            var sut = (BaseViewModelBuilder.Instance)new BaseViewModelBuilder().Create();

            //Act
            sut.HandleError(newSender, error);

            //Assert
            Assert.That(sut.LoggedError.ClassName, Is.EqualTo("ErrorViewModel"));
        }

        [Test]
        public void SHOULD_ShowError()
        {
            //Arrange
            var originalErrorSender = new ListUsersViewModelBuilder().Create();
            var error = Error.Create(originalErrorSender, ErrorType.Crash);
            var sut = (BaseViewModelBuilder.Instance)new BaseViewModelBuilder().Create();

            //Act
            sut.HandleError(originalErrorSender, error);

            //Assert
            Assert.That(sut.ShownError.ErrorType, Is.EqualTo(ErrorType.Crash));
        }

        [Test]
        public void SHOULD_Update_error_before_showing_it()
        {
            //Arrange
            var originalErrorSender = new ListUsersViewModelBuilder().Create();
            var newSender = new ErrorViewModel();
            var error = Error.Create(originalErrorSender, ErrorType.Crash);
            var sut = (BaseViewModelBuilder.Instance)new BaseViewModelBuilder().Create();

            //Act
            sut.HandleError(newSender, error);

            //Assert
            Assert.That(sut.ShownError.ClassName, Is.EqualTo("ErrorViewModel"));
        }
    }
}