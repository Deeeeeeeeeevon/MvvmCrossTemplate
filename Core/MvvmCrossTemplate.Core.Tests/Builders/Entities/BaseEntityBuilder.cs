using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.Tests.Helpers;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.ValueObjects;

namespace MvvmCrossTemplate.Core.Tests.Builders.Entities
{
    public class BaseEntityBuilder : BaseBuilder<BaseEntity>
    {
        private readonly BaseEntity _baseEntity;

        public BaseEntityBuilder()
        {
            _baseEntity = new BaseEntity
            {
                LocalId = RandomValues.Long,
                ObjectId = RandomValues.Long,
                UniqueId = RandomValues.Guid
            };
        }

        public override BaseEntity Create()
        {
            return _baseEntity;
        }
        
        public BaseEntityBuilder With_EntityId(EntityId entityId)
        {
            _baseEntity.LocalId = entityId.LocalId;
            _baseEntity.ObjectId = entityId.ObjectId;
            _baseEntity.UniqueId = entityId.UniqueId;
            return this;
        }
        
    }
}