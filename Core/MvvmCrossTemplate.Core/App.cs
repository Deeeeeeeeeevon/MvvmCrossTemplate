using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using MvvmCrossTemplate.Core.ViewModels.User;

namespace MvvmCrossTemplate.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes().EndingWith("Service").AsInterfaces().RegisterAsLazySingleton();
            CreatableTypes().EndingWith("Repo").AsInterfaces().RegisterAsLazySingleton();
            RegisterAppStart<ListUsersViewModel>();
        }

    }
}
