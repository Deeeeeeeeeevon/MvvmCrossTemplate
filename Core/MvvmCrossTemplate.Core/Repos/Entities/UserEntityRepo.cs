using MvvmCrossTemplate.Core.Entities;
using MvvmCrossTemplate.Core.Interfaces.Services;
using MvvmCrossTemplate.Core.Repos.Entities.Base;

namespace MvvmCrossTemplate.Core.Repos.Entities
{
    public class UserEntityRepo : BaseEntityRepo<UserEntity>
    {
        public UserEntityRepo(IDatabaseService databaseService) : base(databaseService)
        {
        }
    }
}