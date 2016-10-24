using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.Tests.Helpers;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Tests.Builders.Entities
{
    public class BaseEntityBuilder : BaseBuilder<BaseEntity>
    {
        private readonly BaseEntity _baseEntity;

        public BaseEntityBuilder()
        {
            _baseEntity = new BaseEntity
            {
                Id = RandomValues.Long
            };
        }

        public override BaseEntity Create()
        {
            return _baseEntity;
        }

        public BaseEntityBuilder With_Id(long id)
        {
            _baseEntity.Id = id;
            return this;
        }

    }
}