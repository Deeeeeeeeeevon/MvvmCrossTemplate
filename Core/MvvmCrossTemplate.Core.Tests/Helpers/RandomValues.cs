using System;
using MvvmCrossTemplate.Core.Tests.Builders.Utils;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.ValueObjects;
using Ploeh.AutoFixture;

namespace MvvmCrossTemplate.Core.Tests.Helpers
{
    public static class RandomValues
    {
        static RandomValues()
        {
            MyFixture = new Fixture();
        }
        internal static Fixture MyFixture;

        internal static string String => MyFixture.Create<string>();
        internal static int Int => MyFixture.Create<int>();
        internal static bool Bool => MyFixture.Create<bool>();
        internal static long Long => MyFixture.Create<long>();
        internal static float Float => MyFixture.Create<float>();
        internal static DateTime DateTime => MyFixture.Create<DateTime>();
        internal static decimal Decimal => MyFixture.Create<decimal>();
        internal static string Email => String + "@" + String + "." + String;
        public static string Guid => System.Guid.NewGuid().ToString();
        public static EntityId EntityId => new EntityIdBuilder().Create();
    }
}