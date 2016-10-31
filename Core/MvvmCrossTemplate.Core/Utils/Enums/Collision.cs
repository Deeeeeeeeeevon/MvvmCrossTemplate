using PCLStorage;

namespace MvvmCrossTemplate.Core.Utils.Enums
{
    public enum Collision
    {
        GenerateUniqueName = NameCollisionOption.GenerateUniqueName,
        ReplaceExisting = NameCollisionOption.ReplaceExisting,
        FailIfExists = NameCollisionOption.FailIfExists
    }
}