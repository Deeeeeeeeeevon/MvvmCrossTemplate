using System.Collections.Generic;

namespace MvvmCrossTemplate.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToArrayString<T1, T2>(this Dictionary<T1, T2> dict)
        {
            string combined = "";
            if (dict == null) return "{ }";
            foreach (var obj in dict)
            {
                combined += $"[{obj.Key}, {obj.Value}], ";
            }
            return combined;
        }
    }
}