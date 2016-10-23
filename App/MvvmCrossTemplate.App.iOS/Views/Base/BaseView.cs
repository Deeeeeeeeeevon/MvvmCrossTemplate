using MvvmCross.iOS.Views;
using MvvmCrossTemplate.Core.ViewModels.Base;

namespace MvvmCrossTemplate.iOS.Views.Base
{
    public class BaseView<TViewModel> : MvxViewController<TViewModel> where TViewModel: BaseViewModel
    {
       
    }
}