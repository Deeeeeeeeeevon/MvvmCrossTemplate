using Android.OS;
using MvvmCross.Droid.Views;
using MvvmCrossTemplate.Core.ViewModels.Base;

namespace MvvmCrossTemplate.Android.Views.Base
{
    public class BaseView<TViewModel> : MvxActivity<TViewModel> where TViewModel : BaseViewModel
    {
        protected override void OnResume()
        {
            var viewModel = ViewModel as BaseViewModel;
            viewModel?.ViewIsAppearing();
            base.OnResume();
        }

        protected override void OnDestroy()
        {
            var viewModel = ViewModel as BaseViewModel;
            viewModel?.ViewIsDisappearing();
            base.OnDestroy();

        }

    }
}