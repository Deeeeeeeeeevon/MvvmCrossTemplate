using Android.App;
using Android.OS;
using MvvmCrossTemplate.Android.Resources;
using MvvmCrossTemplate.Android.Views.Base;
using MvvmCrossTemplate.Core.ViewModels;

namespace MvvmCrossTemplate.Android.Views
{
    [Activity]
    public class WelcomeView : BaseView<WelcomeViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.WelcomeView);
        }
    }
}

