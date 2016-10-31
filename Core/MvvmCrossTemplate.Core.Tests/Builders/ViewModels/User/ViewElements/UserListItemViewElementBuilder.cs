using Moq;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.ViewModels.User.ViewElements;

namespace MvvmCrossTemplate.Core.Tests.Builders.ViewModels.User.ViewElements
{
    public class UserListItemViewElementBuilder : BaseBuilder<UserListItemViewElement>
    {
        private readonly Mock<IUserModel> _mockUserModel = new Mock<IUserModel>();
        private readonly Mock<IPersonalDetails> _personalDetails = new Mock<IPersonalDetails>();
        private string _firstName;
        private string _lastName;

        public override UserListItemViewElement Create()
        {
            _personalDetails.SetupGet(x => x.FirstName).Returns(_firstName);
            _personalDetails.SetupGet(x => x.LastName).Returns(_lastName);
            _mockUserModel.SetupGet(x => x.PersonalDetails).Returns(_personalDetails.Object);
            return new UserListItemViewElement(_mockUserModel.Object);
        }

        public UserListItemViewElementBuilder With_FirstName(string first)
        {
            _firstName = first;
            return this;
        }
        public UserListItemViewElementBuilder With_LastName(string last)
        {
            _lastName = last;
            return this;
        }
    }
}