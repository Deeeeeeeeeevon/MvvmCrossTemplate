using MvvmCrossTemplate.Core.Interfaces.Models.Generic;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Interfaces.Models.User
{
    public interface IUserModel : IEntityModel
    {
        IPersonalDetails PersonalDetails { get; }
        Result UpdateUserName(string firstName, string lastName);
    }

}