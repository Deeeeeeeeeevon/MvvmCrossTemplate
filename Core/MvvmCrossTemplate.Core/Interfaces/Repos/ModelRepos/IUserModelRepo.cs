using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Interfaces.Repos.ModelRepos
{
    public interface IUserModelRepo
    {
        Task<Result<IUserModel>> LoadUserModelAsync(EntityId entityId);
        Task<Result<IUserModel>> SaveUserModelAsync(IUserModel userModel);
    }
}