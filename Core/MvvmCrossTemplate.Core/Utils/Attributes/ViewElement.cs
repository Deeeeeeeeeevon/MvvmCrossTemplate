using System;

namespace MvvmCrossTemplate.Core.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ViewElement : Attribute
    {}
    
}