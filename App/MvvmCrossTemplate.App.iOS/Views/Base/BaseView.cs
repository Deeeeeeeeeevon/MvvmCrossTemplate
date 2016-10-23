using MvvmCross.iOS.Views;
using MvvmCrossTemplate.Core.ViewModels.Base;

namespace MvvmCrossTemplate.iOS.Views.Base
{
    public class BaseView<TViewModel> : MvxViewController<TViewModel> where TViewModel: BaseViewModel
    {
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ViewModel.ViewIsAppearing();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            ViewModel.ViewIsDisappearing();
        }
    }
}