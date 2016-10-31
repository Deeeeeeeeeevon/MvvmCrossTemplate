using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Tests.Builders.ViewModels;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.BaseViewModel
{
    [TestFixture]
    public class UpdateAllViewElementsTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_raise_property_changed_on_ViewElements()
        {
            //Arrange
            var sut = (BaseViewModelBuilder.Instance) new BaseViewModelBuilder().Create();
            sut.PropertiesChanged["TestViewElement"] = 0;

            //Act
            sut.FinishLoading();

            //Assert
            Assert.That(sut.PropertiesChanged["TestViewElement"], Is.EqualTo(1));
        }

        [Test]
        public void SHOULD_not_raise_property_changed_on_non_ViewElements()
        {
            //Arrange
            var sut = (BaseViewModelBuilder.Instance)new BaseViewModelBuilder().Create();
            sut.PropertiesChanged["TestNonViewElement"] = 0;

            //Act
            sut.FinishLoading();

            //Assert
            Assert.That(sut.PropertiesChanged["TestNonViewElement"], Is.EqualTo(0));
        }
    }
}