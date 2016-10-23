using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCrossTemplate.Core.ViewModels;
using MvvmCrossTemplate.iOS.Views.Base;
using UIKit;

namespace MvvmCrossTemplate.iOS.Views
{
    [Register("WelcomeView")]
    public class WelcomeView : BaseView<WelcomeViewModel>
    {
        public override void ViewDidLoad()
        {

            View = new UIScrollView {BackgroundColor = UIColor.White};
            base.ViewDidLoad();

            var welcomeLabel = new UITextView {TextColor = UIColor.Black, Text = "Hi"};
            var button = new UIButton();
            button.SetTitle("Click", UIControlState.Normal);
            View.Add(button);
            View.Add(welcomeLabel);

            var set = this.CreateBindingSet<WelcomeView, WelcomeViewModel>();
            set.Bind(welcomeLabel).To(vm => vm.WelcomeString);
            set.Apply();

        }
    }
}