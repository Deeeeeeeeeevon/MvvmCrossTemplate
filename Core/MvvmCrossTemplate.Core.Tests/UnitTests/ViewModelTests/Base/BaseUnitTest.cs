using System;
using Ploeh.AutoFixture;

namespace MvvmCrossTemplate.Core.Tests.UnitTests.ViewModelTests.Base
{
    public class BaseUnitTest
    {
        internal Fixture MyFixture;

        internal string AnyString => MyFixture.Create<string>();
        internal int AnyInt => MyFixture.Create<int>();
        internal bool AnyBool => MyFixture.Create<bool>();
        internal long AnyLong => MyFixture.Create<long>();
        internal float AnyFloat => MyFixture.Create<float>();
        internal DateTime AnyDateTime => MyFixture.Create<DateTime>();
        internal decimal AnyDecimal => MyFixture.Create<decimal>();
        internal string AnyEmail => AnyString + "@" + AnyString + "." + AnyString;
    }
}