using Android.App;
using Android.OS;
using MvvmCrossTemplate.Android.Views.Base;
using MvvmCrossTemplate.Core.ViewModels;

namespace MvvmCrossTemplate.Android.Views
{
    [Activity(Label = "MvvmCrossTemplate", MainLauncher = true, Icon = "@drawable/icon")]
    public class WelcomeView : BaseView<WelcomeViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }
    }
}

