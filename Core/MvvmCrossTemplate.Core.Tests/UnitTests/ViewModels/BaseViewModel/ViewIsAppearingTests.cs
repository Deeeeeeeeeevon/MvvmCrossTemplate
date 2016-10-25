using MvvmCrossTemplate.Core.Tests.Builders.ViewModels;
using MvvmCrossTemplate.Core.Tests.UnitTests.Base;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModels.BaseViewModel
{
    [TestFixture]
    public class ViewIsAppearingTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_set_ViewStillActiveToken()
        {
            //Arrange
            var sut = new BaseViewModelBuilder().Create();
                
            //Act
            sut.ViewIsAppearing();

            //Assert
            Assert.That(sut.ViewStillActiveTokenSource, Is.Not.Null);
        }
    }
}
