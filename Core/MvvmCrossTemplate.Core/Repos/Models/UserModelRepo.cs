using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Interfaces.Repos.EntityRepos;
using MvvmCrossTemplate.Core.Interfaces.Repos.ModelRepos;
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

        public async Task<Result<IUserModel>> LoadUserModelAsync(CancellationToken cancelToken, EntityId userEntityId)
        {
            if (cancelToken.IsCancellationRequested)
            {
                return Result.Fail<IUserModel>(this, ErrorType.Cancelled);
            }
            var userEntityResult = await _userEntityRepo.LoadEntityAsync(userEntityId);
            if (userEntityResult.IsFailure)
            {
                return userEntityResult.Error.ErrorType == ErrorType.NotFound 
                    ? Result.Ok(CreateNewUserModel()) 
                    : Result.Fail<IUserModel>(this, userEntityResult);
            }
            var userEntity = userEntityResult.Value;

            var entityId = new EntityId(userEntity);
            var personalDetails = new PersonalDetails(userEntity.FirstName, userEntity.LastName);

            var userModel = new UserModel(entityId, personalDetails);

            return Result.Ok<IUserModel>(userModel);
        }

        public async Task<Result<List<IUserModel>>> LoadAllUserModelsAsync(CancellationToken cancelToken)
        {
            if (cancelToken.IsCancellationRequested)
            {
                return Result.Fail<List<IUserModel>>(this, ErrorType.Cancelled);
            }

            var loadUserEntitiesResult = await _userEntityRepo.LoadAllEntitiesAsync();
            if (loadUserEntitiesResult.IsFailure)
            {
                return Result.Fail<List<IUserModel>>(this, loadUserEntitiesResult);
            }

            var allUsers = new List<IUserModel>();
            foreach (var userEntity in loadUserEntitiesResult.Value)
            {
                var entityId = new EntityId(userEntity);
                var personalDetails = new PersonalDetails(userEntity.FirstName, userEntity.LastName);
                allUsers.Add(new UserModel(entityId, personalDetails));
            }
            return Result.Ok(allUsers);
        }

        private static IUserModel CreateNewUserModel()
        {
            var entityId = new EntityId(0, "", 0);
            var personalDetails = new PersonalDetails("", "");
            return new UserModel(entityId, personalDetails);
        }

        public async Task<Result<IUserModel>> SaveUserModelAsync(CancellationToken cancelToken, IUserModel userModel)
        {
            if (cancelToken.IsCancellationRequested)
            {
                return Result.Fail<IUserModel>(this, ErrorType.Cancelled);
            }

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