using System.Collections.Generic;
using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Attributes;
using MvvmCrossTemplate.Core.ViewModels.Base;

namespace MvvmCrossTemplate.Core.Tests.Builders.ViewModels
{
    public class BaseViewModelBuilder : BaseServiceBuilder<BaseViewModel>
    {
        public override BaseViewModel Create()
        {
            return new Instance();
        }

        #region Instance

        public class Instance : BaseViewModel
        {
            public Instance()
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

            public int UpdateAllViewElementsCalls { get; set; }
            public override void UpdateAllViewElements()
            {
                UpdateAllViewElementsCalls++;
                base.UpdateAllViewElements();
            }

            public Error LoggedError { get; set; }
            public override void LogError(Error error)
            {
                LoggedError = error;
                base.LogError(error);
            }

            public Error ShownError { get; set; }
            public override void ShowError(Error error)
            {
                ShownError = error;
                base.ShowError(error);
            }

            [ViewElement]
            public string TestViewElement { get; set; }

            public string TestNonViewElement { get; set; }

        }

        #endregion
    }
}