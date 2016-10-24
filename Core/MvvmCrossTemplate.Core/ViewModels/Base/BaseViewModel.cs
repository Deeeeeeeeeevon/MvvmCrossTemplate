using System;
using System.Threading;
using MvvmCross.Core.ViewModels;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Attributes;

namespace MvvmCrossTemplate.Core.ViewModels.Base
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected BaseViewModel()
        {
            ViewStillActiveToken = new CancellationTokenSource();
        }

        #region IsBusy

        private bool _isBusy;

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

        #endregion

        public virtual bool ViewIsActive
            => !ViewStillActiveToken.IsCancellationRequested && IsNotBusy;

        public CancellationTokenSource ViewStillActiveToken { get; protected set; }

        public void ViewIsAppearing()
        {
            ViewStillActiveToken = new CancellationTokenSource();
        }

        public virtual void UpdateAllViewElements()
        {
            
        }

        public void ViewIsDisappearing()
        {
            ViewStillActiveToken.Cancel();
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

        public virtual void HandleError(BaseViewModel sender, Error error, string methodName = "")
        { }
        
    }
}