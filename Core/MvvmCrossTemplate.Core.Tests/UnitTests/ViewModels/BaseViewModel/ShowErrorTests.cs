using System;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Tests.Builders.Utils;
using MvvmCrossTemplate.Core.Tests.Builders.ViewModels;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using MvvmCrossTemplate.Core.Utils.Enums;
using MvvmCrossTemplate.Core.ViewModels.Error;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.BaseViewModel
{
    [TestFixture]
    public class ShowErrorTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_ShowErrorViewModel()
        {
            //Arrange
            var builder = new BaseViewModelBuilder();
            var sut = builder.Create();
            var error = new ErrorBuilder()
                .With_Class(sut.GetType().Name)
                .With_ErrorType(ErrorType.Crash)
                .With_Exception(new NullReferenceException())
                .Create();

            //Act
            sut.ShowError(error);

            //Assert
            Assert.That(MockViewDispatcher.Requests.Count, Is.EqualTo(1));
            Assert.That(MockViewDispatcher.Requests[0].ViewModelType, Is.EqualTo(typeof(ErrorViewModel)));
        }

        [Test]
        public void SHOULD_pass_error_parameters_to_ErrorViewModel()
        {
            //Arrange
            var builder = new BaseViewModelBuilder();
            var sut = builder.Create();
            var error = new ErrorBuilder()
                .With_Class(sut.GetType().Name)
                .With_ErrorType(ErrorType.Crash)
                .With_Exception(new NullReferenceException())
                .Create();

            //Act
            sut.ShowError(error);

            //Assert
            Assert.That(MockViewDispatcher.Requests[0].ParameterValues["errorType"], Is.EqualTo("Crash"));
            Assert.That(MockViewDispatcher.Requests[0].ParameterValues["exceptionMessage"], Is.EqualTo("Object reference not set to an instance of an object."));
        }
    }
}