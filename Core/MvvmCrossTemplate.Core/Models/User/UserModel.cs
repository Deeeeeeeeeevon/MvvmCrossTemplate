using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.ValueObjects;

namespace MvvmCrossTemplate.Core.Models.User
{
    public class UserModel : IUserModel
    {
        public UserModel(EntityId entityId, IPersonalDetails personalDetails)
        {
            EntityId = entityId;
            PersonalDetails = personalDetails;
        }

        public EntityId EntityId { get; }
        public IPersonalDetails PersonalDetails { get; }

        public Result UpdateUserName(string firstName, string lastName)
        {
            return Result.Ok();

        }
    }
}