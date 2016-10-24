using SQLite.Net.Attributes;

namespace MvvmCrossTemplate.Core.Entities.Base
{
    public class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
    }
}