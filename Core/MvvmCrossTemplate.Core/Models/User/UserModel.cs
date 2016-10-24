using MvvmCrossTemplate.Core.Interfaces.Models;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Utils;

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
    }
}