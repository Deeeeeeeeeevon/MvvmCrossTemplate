using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Tests.Builders.ViewModels;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.BaseViewModel
{
    [TestFixture]
    public class ViewIsDisappearingTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_cancel_ViewStillActiveToken()
        {
            //Arrange
            var sut = new BaseViewModelBuilder().Create();
            sut.ViewIsAppearing();

            //Pretest
            Assert.That(sut.ViewStillActiveTokenSource.IsCancellationRequested, Is.False);

            //Act
            sut.ViewIsDisappearing();

            //Assert
            Assert.That(sut.ViewStillActiveTokenSource.IsCancellationRequested, Is.True);
        }
    }
}