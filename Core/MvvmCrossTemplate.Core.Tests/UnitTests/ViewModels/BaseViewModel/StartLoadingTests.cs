using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Tests.Builders.ViewModels;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.BaseViewModel
{
    [TestFixture]
    public class StartLoadingTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_set_IsBusy_true()
        {
            //Arrange
            var sut = new BaseViewModelBuilder().Create();
            sut.IsBusy = false;

            //Act
            sut.StartLoading();

            //Assert
            Assert.That(sut.IsBusy, Is.True);
        }
    }
}