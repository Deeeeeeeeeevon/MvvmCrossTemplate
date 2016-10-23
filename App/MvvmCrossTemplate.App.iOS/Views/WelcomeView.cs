using Cirrious.FluentLayouts.Touch;
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
            base.ViewDidLoad();

            var scrollView = new UIScrollView(View.Frame)
            {
                BackgroundColor = UIColor.White,
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight
            };
            var welcomeLabel = new UITextField
            {
                TextColor = UIColor.Blue
            };

            var set = this.CreateBindingSet<WelcomeView, WelcomeViewModel>();
            set.Bind(welcomeLabel).To(vm => vm.WelcomeString);
            set.Apply();

            scrollView.Add(welcomeLabel);
            Add(scrollView);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                scrollView.AtLeftOf(View),
                scrollView.AtTopOf(View),
                scrollView.WithSameWidth(View),
                scrollView.WithSameHeight(View));

            scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            var constraints = scrollView.VerticalStackPanelConstraints(new Margins(20, 10, 20, 10, 5, 5),
                scrollView.Subviews);
            scrollView.AddConstraints(constraints);

        }
    }
}