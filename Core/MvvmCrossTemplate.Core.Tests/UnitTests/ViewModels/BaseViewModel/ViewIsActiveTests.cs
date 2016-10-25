using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Tests.Builders.ViewModels;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.BaseViewModel
{
    [TestFixture]
    public class ViewIsActiveTests : BaseUnitTest
    {
        [Test]
        public void WHEN_IsBusy_false_AND_Cancelled_false_SHOULD_return_true()
        {
            //Arrange
            var sut = new BaseViewModelBuilder().Create();
            sut.IsBusy = false;

            //Act
            var result = sut.ViewIsActive;

            //Assert
            Assert.That(sut.IsBusy, Is.False);
            Assert.That(sut.ViewStillActiveTokenSource.IsCancellationRequested, Is.False);
            Assert.That(result, Is.True);
        }

        [Test]
        public void WHEN_IsBusy_false_AND_Cancelled_true_SHOULD_return_false()
        {
            //Arrange
            var sut = new BaseViewModelBuilder().Create();
            sut.IsBusy = false;
            sut.ViewIsDisappearing();

            //Act
            var result = sut.ViewIsActive;

            //Assert
            Assert.That(sut.IsBusy, Is.False);
            Assert.That(sut.ViewStillActiveTokenSource.IsCancellationRequested, Is.True);
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_IsBusy_true_AND_Cancelled_false_SHOULD_return_false()
        {
            //Arrange
            var sut = new BaseViewModelBuilder().Create();
            sut.IsBusy = true;

            //Act
            var result = sut.ViewIsActive;

            //Assert
            Assert.That(sut.IsBusy, Is.True);
            Assert.That(sut.ViewStillActiveTokenSource.IsCancellationRequested, Is.False);
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_IsBusy_true_AND_Cancelled_true_SHOULD_return_false()
        {
            //Arrange
            var sut = new BaseViewModelBuilder().Create();
            sut.ViewIsDisappearing();
            sut.IsBusy = true;

            //Act
            var result = sut.ViewIsActive;

            //Assert
            Assert.That(sut.IsBusy, Is.True);
            Assert.That(sut.ViewStillActiveTokenSource.IsCancellationRequested, Is.True);
            Assert.That(result, Is.False);
        }
    }
}