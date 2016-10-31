using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Tests.Builders.ViewModels;
using NUnit.Framework;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.BaseViewModel
{
    [TestFixture]
    public class IsBusyTests : BaseUnitTest
    {
        [Test]
        public void WHEN_setting_to_true_SHOULD_set_IsNotBusy_false()
        {
            //Arrange
            var sut = new BaseViewModelBuilder().Create();

            //Act
            sut.IsBusy = true;

            //Assert
            Assert.That(sut.IsNotBusy, Is.False);
        }

        [Test]
        public void WHEN_setting_to_false_SHOULD_set_IsNotBusy_true()
        {
            //Arrange
            var sut = new BaseViewModelBuilder().Create();

            //Act
            sut.IsBusy = false;

            //Assert
            Assert.That(sut.IsNotBusy, Is.True);
        }

        [Test]
        public void WHEN_setting_SHOULD_RaisePropertyChanged_on_IsBusy()
        {
            //Arrange
            var sut = (BaseViewModelBuilder.Instance)new BaseViewModelBuilder().Create();
            sut.PropertiesChanged["IsBusy"] = 0;

            //Act
            sut.IsBusy = false;

            //Assert
            Assert.That(sut.PropertiesChanged["IsBusy"], Is.EqualTo(1));
        }

        [Test]
        public void WHEN_setting_SHOULD_RaisePropertyChanged_on_IsNotBusy()
        {
            //Arrange
            var sut = (BaseViewModelBuilder.Instance)new BaseViewModelBuilder().Create();
            sut.PropertiesChanged["IsNotBusy"] = 0;

            //Act
            sut.IsBusy = false;

            //Assert
            Assert.That(sut.PropertiesChanged["IsNotBusy"], Is.EqualTo(1));
        }
    }
}