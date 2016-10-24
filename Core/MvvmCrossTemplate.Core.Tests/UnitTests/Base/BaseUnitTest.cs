using System;
using MvvmCrossTemplate.Core.Tests.Builders.Utils;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;
using Ploeh.AutoFixture;
using static MvvmCrossTemplate.Core.Tests.Helpers.RandomValues;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.Base
{
    public class BaseUnitTest
    {

        public BaseUnitTest()
        {
            MyFixture = new Fixture();
        }

        internal Fixture MyFixture;

        internal Result<T> FailResult<T>(string className) => new ResultOfTypeBuilder<T>().With_Error_ClassName(className).Create();
        internal Result<T> FailResult<T>(ErrorType type) => new ResultOfTypeBuilder<T>().With_Error_ClassName("oops").With_Error_Type(type).Create();

    }
}