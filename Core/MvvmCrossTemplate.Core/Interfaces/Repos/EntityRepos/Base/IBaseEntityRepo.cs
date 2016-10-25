using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Interfaces.Repos.EntityRepos.Base
{
    public interface IBaseEntityRepo<TEntity> where TEntity : BaseEntity
    {
        string TableName { get; }

        Task<Result<TEntity>> LoadEntityAsync(EntityId entityId);
        Task<Result<TEntity>> SaveEntityAsync(TEntity entityToSave);

        Task<Result<List<TEntity>>> LoadEntitiesBySqlQueryAsync(string sql);
        Task<Result<List<TEntity>>> LoadAllEntitiesAsync();
        Task<Result<T>> LoadValueBySqlQueryAsync<T>(string sql);
        Task<Result> ExecuteSqlAsync(string sql);
    }
}