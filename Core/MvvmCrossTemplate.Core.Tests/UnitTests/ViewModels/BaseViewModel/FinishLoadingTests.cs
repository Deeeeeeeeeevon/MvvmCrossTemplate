using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Tests.Builders.ViewModels;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.BaseViewModel
{
    [TestFixture]
    public class FinishLoadingTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_set_IsBusy_false()
        {
            //Arrange
            var sut = new BaseViewModelBuilder().Create();
            sut.IsBusy = true;

            //Act
            sut.FinishLoading();

            //Assert
            Assert.That(sut.IsBusy, Is.False);
        }

        [Test]
        public void SHOULD_UpdateAllViewElements()
        {
            //Arrange
            var sut = (BaseViewModelBuilder.Instance)new BaseViewModelBuilder().Create();
            sut.UpdateAllViewElementsCalls = 0;

            //Act
            sut.FinishLoading();

            //Assert
            Assert.That(sut.UpdateAllViewElementsCalls, Is.EqualTo(1));
        }
    }
}