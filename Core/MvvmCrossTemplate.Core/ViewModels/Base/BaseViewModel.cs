using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Attributes;
using MvvmCrossTemplate.Core.ViewModels.Error;

namespace MvvmCrossTemplate.Core.ViewModels.Base
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected BaseViewModel()
        {
            ViewStillActiveTokenSource = new CancellationTokenSource();
        }

        #region LifeCycle Management

        private bool _isBusy;

        public virtual bool ViewIsActive
            => !ViewStillActiveTokenSource.IsCancellationRequested && IsNotBusy;

        public CancellationTokenSource ViewStillActiveTokenSource { get; protected set; }
        protected CancellationToken ViewStillActiveToken => ViewStillActiveTokenSource.Token;

        [ViewElement]
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsNotBusy);
                RaisePropertyChanged(() => IsBusy);
            }
        }

        [ViewElement]
        public bool IsNotBusy => !IsBusy;

        public void ViewIsAppearing()
        {
            ViewStillActiveTokenSource = new CancellationTokenSource();
        }

        public void ViewIsDisappearing()
        {
            ViewStillActiveTokenSource.Cancel();
        }

        public virtual void StartLoading()
        {
            IsBusy = true;
        }

        public virtual void FinishLoading()
        {
            UpdateAllViewElements();
            IsBusy = false;
        }

        #endregion

        #region Update ViewElements

        public virtual void UpdateAllViewElements()
        {
            var props = GetType().GetProperties().Where(prop => prop.IsDefined(typeof(ViewElement), true));
            foreach (var prop in props)
            {
                RaisePropertyChanged(prop.Name);
            }
        }

        #endregion

        #region HandleError

        public virtual void HandleError(BaseViewModel sender, Utils.Error error, string methodName = "")
        {
            var updatedError = Utils.Error.Update(sender, error, methodName);
            LogError(updatedError);
            ShowError(updatedError);
        }

        public virtual void LogError(Utils.Error error)
        {
            Mvx.Trace(error.ErrorDescriptionString, error.ErrorStack);
        }

        public virtual void ShowError(Utils.Error error)
        {
            ShowViewModel<ErrorViewModel>(error.ViewModelParameters);
        }
        #endregion

    }
}