using MvvmCrossTemplate.Core.Resources;
using MvvmCrossTemplate.Core.ViewModels.Base;

namespace MvvmCrossTemplate.Core.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        public string WelcomeString => UiStrings.WelcomeString;
    }
}