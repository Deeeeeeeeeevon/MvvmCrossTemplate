using Windows.UI.Xaml.Navigation;
using MvvmCross.WindowsUWP.Views;
using MvvmCrossTemplate.Core.ViewModels.Base;

namespace MvvmCrossTemplate.Windows.Views.Base
{
    public class BaseView : MvxWindowsPage
    {

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var viewModel = ViewModel as BaseViewModel;
            viewModel?.ViewIsAppearing();
            base.OnNavigatedTo(e);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var viewModel = ViewModel as BaseViewModel;
            viewModel?.ViewIsDisappearing();
            base.OnNavigatedFrom(e);
        }
    }
}