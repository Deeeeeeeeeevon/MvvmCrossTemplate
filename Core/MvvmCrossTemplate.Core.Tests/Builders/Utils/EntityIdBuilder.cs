using System;
using System.Collections.Generic;
using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.Tests.Helpers;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Tests.Builders.Utils
{

    public class EntityIdBuilder : BaseBuilder<EntityId>
    {
        private long _localId;
        private string _uniqueId;
        private long _objectId;

        public EntityIdBuilder()
        {
            _localId = RandomValues.Long;
            _uniqueId = Guid.NewGuid().ToString();
            _objectId = RandomValues.Long;
        }

        public EntityId Sut { get; set; }
        

        public override EntityId Create()
        {
            return new EntityId(_objectId, _uniqueId, _localId);
        }

        public EntityIdBuilder With_Ids(long objectId = 0, string uniqueId = "", long localId = 0)
        {
            _objectId = objectId;
            _uniqueId = uniqueId;
            _localId = localId;
            return this;
        }
        

    }
}