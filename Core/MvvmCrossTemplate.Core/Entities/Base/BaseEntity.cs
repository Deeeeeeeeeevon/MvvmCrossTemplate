using MvvmCrossTemplate.Core.Utils;
using SQLite.Net.Attributes;

namespace MvvmCrossTemplate.Core.Entities.Base
{
    public class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public long LocalId { get; set; }
        public long ObjectId { get; set; }
        public string UniqueId { get; set; } = "";

        public EntityId EntityId => new EntityId(ObjectId, UniqueId, LocalId);

        public void UpdateEntityId(EntityId entityId)
        {
            LocalId = entityId.LocalId;
            ObjectId = entityId.ObjectId;
            UniqueId = entityId.UniqueId;
        }
    }
}