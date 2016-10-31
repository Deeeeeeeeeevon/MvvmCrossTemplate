using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.ValueObjects;

namespace MvvmCrossTemplate.Core.Interfaces.Repos.ModelRepos
{
    public interface IUserModelRepo
    {
        IUserModel CreateNewUserModel();

        Task<Result<IUserModel>> LoadUserModelAsync(CancellationToken cancelToken, EntityId entityId);
        Task<Result<List<IUserModel>>> LoadAllUserModelsAsync(CancellationToken cancelToken);

        Task<Result<IUserModel>> SaveUserModelAsync(CancellationToken cancelToken, IUserModel userModel);
    }
}