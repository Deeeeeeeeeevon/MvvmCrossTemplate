using System;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Models.User;

namespace MvvmCrossTemplate.Core.Entities
{
    public class UserEntity : BaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void UpdateFromUserModel(IUserModel userModel)
        {
            FirstName = userModel.PersonalDetails.FirstName;
            LastName = userModel.PersonalDetails.LastName;
        }
        public void InitialiseFromUserModel(IUserModel userModel)
        {
            UpdateFromUserModel(userModel);
            UniqueId = Guid.NewGuid().ToString();
        }

    }
}