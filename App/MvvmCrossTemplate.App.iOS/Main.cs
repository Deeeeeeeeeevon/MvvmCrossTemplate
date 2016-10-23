using System;
using MvvmCross.iOS.Platform;
using UIKit;

namespace MvvmCrossTemplate.iOS
{
    public class Application
    {
        static void Main(string[] args)
        {
               UIApplication.Main(args, null, typeof(AppDelegate).Name);
        }
    }
}