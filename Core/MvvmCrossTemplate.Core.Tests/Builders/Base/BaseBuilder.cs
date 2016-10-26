using System.Collections.Generic;

namespace MvvmCrossTemplate.Core.Tests.Builders.Base
{
    public abstract class BaseBuilder<T> 
    {
        public abstract T Create();

        public List<T> CreateList(int numberToCreate = 3)
        {
            var list = new List<T>();
            for (int i = 0; i < numberToCreate; i++)
            {
                list.Add(Create());
            }
            return list;
        }
        
    }
}