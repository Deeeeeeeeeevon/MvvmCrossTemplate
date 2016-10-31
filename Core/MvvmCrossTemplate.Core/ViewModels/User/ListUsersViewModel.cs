using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCrossTemplate.Core.Interfaces.Repos.ModelRepos;
using MvvmCrossTemplate.Core.Utils.Attributes;
using MvvmCrossTemplate.Core.ViewModels.Base;
using MvvmCrossTemplate.Core.ViewModels.User.ViewElements;

namespace MvvmCrossTemplate.Core.ViewModels.User
{
    public class ListUsersViewModel : BaseViewModel
    {
        private readonly IUserModelRepo _userModelRepo;

        [ViewElement]
        public List<UserListItemViewElement> UserList { get; } = new List<UserListItemViewElement>();

        public ListUsersViewModel(IUserModelRepo userModelRepo)
        {
            _userModelRepo = userModelRepo;
        }

        public override void Start()
        {
            LoadDataCommand.Execute(null);
        }

        public virtual ICommand LoadDataCommand => new MvxAsyncCommand(LoadDataAsync);

        private async Task LoadDataAsync()
        {
            if (ViewIsActive)
            {
                StartLoading();

                var userModelResult = await _userModelRepo.LoadAllUserModelsAsync(ViewStillActiveToken);
                if (userModelResult.IsFailure)
                {
                    HandleError(this, userModelResult.Error);
                }
                else
                {
                    UserList.Clear();
                    foreach (var userModel in userModelResult.Value)
                    {
                        UserList.Add(new UserListItemViewElement(userModel));
                    }

                    FinishLoading();
                }
            }
        }
    }
}