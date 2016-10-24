using Android.App;
using MvvmCross.Droid.Views;
using MvvmCrossTemplate.Android.Resources;

namespace MvvmCrossTemplate.Android
{
    //Required by MvvmCross to initialise the system

    [Activity(Label = "MvvmCrossTemplate", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true)]
    public class Splash : MvxSplashScreenActivity
    {
        public Splash(): base(Resource.Layout.Splash)
        {
        }
    }

}