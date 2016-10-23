using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using MvvmCrossTemplate.Core.ViewModels;

namespace MvvmCrossTemplate.Core
{
    public class App : MvxApplication
    {


        public override void Initialize()
        {
            CreatableTypes().EndingWith("Service").AsInterfaces().RegisterAsLazySingleton();

            RegisterAppStart<WelcomeViewModel>();
        }

    }
}
