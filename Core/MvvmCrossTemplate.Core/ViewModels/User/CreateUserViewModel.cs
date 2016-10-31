using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Interfaces.Repos.ModelRepos;
using MvvmCrossTemplate.Core.ViewModels.Base;

namespace MvvmCrossTemplate.Core.ViewModels.User
{
    public class CreateUserViewModel : BaseViewModel
    {
        protected IUserModelRepo UserModelRepo { get; }

        public CreateUserViewModel(IUserModelRepo userModelRepo)
        {
            UserModelRepo = userModelRepo;
        }
        protected IUserModel UserModel { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public ICommand CreateUserCommand => new MvxAsyncCommand(CreateUserAsync);

        private async Task CreateUserAsync()
        {
            UserModel = UserModelRepo.CreateNewUserModel();
            var updateResult = UserModel.UpdateUserName(FirstName, LastName);
            if (updateResult.IsFailure)
            {
                HandleError(this, updateResult.Error);
            }
            else
            {
                var saveUserModelResult= await UserModelRepo.SaveUserModelAsync(ViewStillActiveToken, UserModel);
                if (saveUserModelResult.IsFailure)
                {
                    HandleError(this, saveUserModelResult.Error);
                }
                else
                {
                    ShowViewModel<ListUsersViewModel>();
                }
            }
        }
    }
}