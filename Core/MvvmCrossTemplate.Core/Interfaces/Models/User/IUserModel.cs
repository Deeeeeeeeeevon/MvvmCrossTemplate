using MvvmCrossTemplate.Core.Interfaces.Models.Generic;

namespace MvvmCrossTemplate.Core.Interfaces.Models.User
{
    public interface IUserModel : IEntityModel
    {
        IPersonalDetails PersonalDetails { get; }
    }
}