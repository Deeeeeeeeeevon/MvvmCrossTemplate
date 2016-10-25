using System.Collections.Generic;
using System.Windows.Input;
using MvvmCrossTemplate.Core.Interfaces.Repos.ModelRepos;
using MvvmCrossTemplate.Core.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Attributes;
using MvvmCrossTemplate.Core.ViewModels.Base;
using MvvmCrossTemplate.Core.ViewModels.User;

namespace MvvmCrossTemplate.Core.Tests.Builders.ViewModels.User
{
    public class ListUsersViewModelBuilder : BaseBuilder<ListUsersViewModel>
    {
        public override ListUsersViewModel Create()
        {
            return new Instance(MockUserModelRepo.Object);
        }


        #region Instance

        public class Instance : ListUsersViewModel
        {
            public Instance(IUserModelRepo userModelRepo) : base(userModelRepo)
            {
                ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
                PropertyChanged += Instance_PropertyChanged;
            }

            public Dictionary<string, int> PropertiesChanged { get; set; } = new Dictionary<string, int>();
            private void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (PropertiesChanged.ContainsKey(e.PropertyName))
                    PropertiesChanged[e.PropertyName]++;
                else
                    PropertiesChanged[e.PropertyName] = 1;
            }

            public int StartLoadingCalls { get; set; }
            public override void StartLoading()
            {
                StartLoadingCalls++;
                base.StartLoading();
            }

            public int FinishLoadingCalls { get; set; }
            public override void FinishLoading()
            {
                FinishLoadingCalls++;
                base.FinishLoading();
            }

            public int HandleErrorCalls { get; set; }
            public override void HandleError(BaseViewModel sender, Error error, string methodName = "")
            {
                HandleErrorCalls++;
                base.HandleError(sender, error, methodName);
            }

            public Error LoggedError { get; set; }
            public override void LogError(Error error)
            {
                LoggedError = error;
                base.LogError(error);
            }

            public int UpdateAllViewElementsCalls { get; set; }
            public override void UpdateAllViewElements()
            {
                UpdateAllViewElementsCalls++;
                base.UpdateAllViewElements();
            }
            
            public int LoadDataCommandCalls { get; set; }

            public override ICommand LoadDataCommand
            {
                get
                {
                    LoadDataCommandCalls++;
                    return base.LoadDataCommand;
                }
            }
        }

        #endregion

    }
}