using System.Threading;
using MvvmCross.Core.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;
using MvvmCross.Test.Core;
using MvvmCrossTemplate.Core.Tests.Builders.Utils;
using MvvmCrossTemplate.Core.Tests.Helpers;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;
using Ploeh.AutoFixture;
using NUnit.Framework;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Base
{
    public class BaseUnitTest : MvxIoCSupportingTest
    {

        public BaseUnitTest()
        {
            MyFixture = new Fixture();
        }

        [SetUp]
        public virtual void Start()
        {
            ClearAll();
            TestCancellationTokenSource = new CancellationTokenSource();
            MockViewDispatcher = new MockMvxViewDispatcher();
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockViewDispatcher);
            Ioc.RegisterSingleton<IMvxViewDispatcher>(MockViewDispatcher);
            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());
        }

        internal CancellationTokenSource TestCancellationTokenSource;
        internal CancellationToken CancelToken => TestCancellationTokenSource.Token;

        internal MockMvxViewDispatcher MockViewDispatcher;
        internal Fixture MyFixture;

        internal Result<T> FailResult<T>(string className) => new ResultOfTypeBuilder<T>().With_Error_ClassName(className).Create();
        internal Result<T> FailResult<T>(ErrorType type) => new ResultOfTypeBuilder<T>().With_Error_ClassName("oops").With_Error_Type(type).Create();

    }
}