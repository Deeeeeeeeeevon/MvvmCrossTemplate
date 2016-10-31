using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Interfaces.Repos.ModelRepos;

namespace MvvmCrossTemplate.Core.ViewModels.User.ViewElements
{
    public class UserListItemViewElement
    {
        public UserListItemViewElement(IUserModel userModel)
        {
            FirstName = userModel.PersonalDetails.FirstName;
            LastName = userModel.PersonalDetails.LastName;
        }

        public string FirstName { get; }
        public string LastName { get; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}