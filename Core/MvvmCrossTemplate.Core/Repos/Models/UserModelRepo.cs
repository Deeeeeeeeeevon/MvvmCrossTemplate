using System;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Interfaces.Models;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Interfaces.Repos.EntityRepos;
using MvvmCrossTemplate.Core.Interfaces.Repos.ModelRepos;
using MvvmCrossTemplate.Core.Models;
using MvvmCrossTemplate.Core.Models.User;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;

namespace MvvmCrossTemplate.Core.Repos.Models
{
    public class UserModelRepo : IUserModelRepo
    {
        private readonly IUserEntityRepo _userEntityRepo;

        public UserModelRepo(IUserEntityRepo userEntityRepo)
        {
            _userEntityRepo = userEntityRepo;
        }

        public async Task<Result<IUserModel>> LoadUserModelAsync(EntityId userEntityId)
        {
            var userEntityResult = await _userEntityRepo.LoadEntityAsync(userEntityId);
            if (userEntityResult.IsFailure)
            {
                if (userEntityResult.Error.ErrorType == ErrorType.NotFound)
                {
                    return Result.Ok(CreateNewUserModel());
                }
                else
                {
                    return Result.Fail<IUserModel>(this, userEntityResult);
                }
            }
            var userEntity = userEntityResult.Value;

            var entityId = new EntityId(userEntity);
            var personalDetails = new PersonalDetails(userEntity.FirstName, userEntity.LastName);

            var userModel = new UserModel(entityId, personalDetails);

            return Result.Ok<IUserModel>(userModel);
        }

        private static IUserModel CreateNewUserModel()
        {
            var entityId = new EntityId(0, "", 0);
            var personalDetails = new PersonalDetails("", "");
            return new UserModel(entityId, personalDetails);
        }

        public async Task<Result<IUserModel>> SaveUserModelAsync(IUserModel userModel)
        {
            var loadUserEntityResult = await _userEntityRepo.LoadEntityAsync(userModel.EntityId);
            if (loadUserEntityResult.IsFailure)
            {
                return Result.Fail<IUserModel>(this, loadUserEntityResult);
            }
            var existingUserEntity = loadUserEntityResult.Value;

            existingUserEntity.UpdateFromUserModel(userModel);

            var saveUserEntityResult = await _userEntityRepo.SaveEntityAsync(existingUserEntity);
            if (saveUserEntityResult.IsFailure)
            {
                return Result.Fail<IUserModel>(this, saveUserEntityResult);
            }
            return Result.Ok(userModel);
        }
    }
}