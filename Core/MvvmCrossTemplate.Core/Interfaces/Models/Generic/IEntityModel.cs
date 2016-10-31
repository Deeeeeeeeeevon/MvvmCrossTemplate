using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.ValueObjects;

namespace MvvmCrossTemplate.Core.Interfaces.Models.Generic
{
    public interface IEntityModel
    {
        EntityId EntityId { get; }
    }
}