using System;
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
        
    }
}