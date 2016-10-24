using System.Collections.Generic;
using System.Text;
using MvvmCrossTemplate.Core.Entities.Base;

namespace MvvmCrossTemplate.Core.Utils
{
    public class EntityId
    {

        public EntityId(string uniqueId)
        {
            UniqueId = uniqueId;
            LocalId = 0;
            ObjectId = 0;
        }

        public EntityId(long objectId)
        {
            UniqueId = "";
            ObjectId = objectId;
        }

        public EntityId(long objectId, string uniqueId)
        {
            UniqueId = uniqueId;
            ObjectId = objectId;
        }

        public EntityId(BaseEntity entity)
        {
            LocalId = entity.LocalId;
            UniqueId = entity.UniqueId;
            ObjectId = entity.ObjectId;
        }

        public EntityId(long objectId, string uniqueId, long localId)
        {
            UniqueId = uniqueId;
            ObjectId = objectId;
            LocalId = localId;
        }

        public EntityId(string unknownString, bool isHybridId)
        {
            if (isHybridId) InitialiseByHybridIdString(unknownString);
            else InitialiseByUnknownIdString(unknownString);
        }

        public string HybridId => new StringBuilder().Append(ObjectId).Append("|").Append(UniqueId).ToString();
        public string FullId => new StringBuilder().Append(LocalId).Append("|").Append(ObjectId).Append("|").Append(UniqueId).ToString();
        public string UniqueId { get; private set; } = "";
        public long ObjectId { get; private set; }
        public long LocalId { get; private set; }
        public bool HasBeenUploaded => ObjectId > 0;
        public string WhereClause => GetWhereClause();
        public string WhereParentClause => GetWhereParentClause();
        public string CurrentIdString => HasBeenUploaded ? ObjectId.ToString() : UniqueId;


        public bool IsEmpty()
        {
            return ObjectId == 0 && string.IsNullOrEmpty(UniqueId) && LocalId == 0;
        }

        public void Clear()
        {
            ObjectId = 0;
            UniqueId = string.Empty;
            LocalId = 0;
        }

        public bool IsInList(List<EntityId> entityList)
        {
            if (entityList == null) return false;
            foreach (var entityIdModel in entityList)
            {
                if (HasBeenUploaded && entityIdModel.ObjectId == ObjectId) return true;
                if (!HasBeenUploaded && entityIdModel.UniqueId == UniqueId) return true;
            }
            return false;
        }

        public bool IsEqualTo(EntityId idToCompare)
        {
            if ((object)idToCompare == null) return false;
            if (IsEmpty() && idToCompare.IsEmpty()) return true;
            if (ObjectId > 0 && idToCompare.ObjectId == ObjectId) return true;
            if (ObjectId == 0 && idToCompare.ObjectId > 0) return false;
            if (ObjectId == 0 && idToCompare.UniqueId == UniqueId) return true;
            return false;
        }

        public override string ToString()
        {
            return FullId;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(EntityId)) return false;
            var idToCompare = (EntityId)obj;

            if (IsEmpty() && idToCompare.IsEmpty()) return true;
            if (ObjectId > 0 && idToCompare.ObjectId == ObjectId) return true;
            if (ObjectId == 0 && idToCompare.ObjectId > 0) return false;
            if (ObjectId == 0 && idToCompare.UniqueId == UniqueId) return true;
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((UniqueId?.GetHashCode() ?? 0) * 397) ^ ObjectId.GetHashCode();
            }
        }

        public static bool operator ==(EntityId x, EntityId y)
        {
            if ((object)x == null) return false;
            return x.Equals(y);
        }

        public static bool operator !=(EntityId x, EntityId y)
        {
            if ((object)x == null) return false;
            return !(x == y);
        }

        public void UpdateFromEntity(BaseEntity baseEntity)
        {
            UniqueId = baseEntity.UniqueId;
            ObjectId = baseEntity.ObjectId;
        }

        //PRIVATES

        private void InitialiseByHybridIdString(string hybridId)
        {
            var separatedHybrid = hybridId.Split('|');
            ObjectId = long.Parse(separatedHybrid[0]);
            UniqueId = separatedHybrid[1];
        }

        private void InitialiseByUnknownIdString(string unknownString)
        {
            if (unknownString.Length < 20)
            {
                ObjectId = long.Parse(unknownString);
                UniqueId = "";
            }
            else
            {
                UniqueId = unknownString;
                ObjectId = 0;
            }
        }

        private string GetWhereClause()
        {
            if (HasBeenUploaded)
                return " WHERE ObjectId = " + ObjectId + " ";
            return " WHERE UniqueId = \'" + UniqueId + "\' ";
        }

        private string GetWhereParentClause()
        {
            if (HasBeenUploaded)
                return " WHERE ParentObjectId = " + ObjectId + " ";
            return " WHERE ParentUniqueId = \'" + UniqueId + "\' ";
        }
    }
}