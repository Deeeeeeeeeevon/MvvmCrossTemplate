using System;
using MvvmCross.iOS.Platform;
using UIKit;

namespace MvvmCrossTemplate.iOS
{
    public class Application
    {
        static void Main(string[] args)
        {
#if DEBUG
            try
            {
                var x = typeof(AppDelegate).Name;
                UIApplication.Main(args, null, typeof(AppDelegate).Name);
            }
            catch (Exception e)
            {
                //log output or put debug breakpoint here
            }
#else
            UIApplication.Main(args, null, typeof(AppDelegate).Name);
#endif
        }
    }
}