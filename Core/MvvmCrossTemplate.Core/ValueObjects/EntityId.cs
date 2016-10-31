using Maxxor.Mvx.Core.MxValueObjects;

namespace MvvmCrossTemplate.Core.ValueObjects
{
    public class EntityId : MxEntityId
    {
        public EntityId(long objectId = 0, string uniqueId = "", long localId = 0) : base(objectId, uniqueId, localId)
        {}
    }
}