using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Interfaces.Repos.EntityRepos.Base
{
    public interface IBaseEntityRepo<TEntity> where TEntity : BaseEntity
    {
        string TableName { get; }

        Task<Result<TEntity>> InsertEntityAsync(TEntity entity);
        Task<Result<TEntity>> UpdateEntityAsync(TEntity entity);
        Task<Result> DeleteEntityAsync(TEntity entitytoDelete);
        Task<Result> DeleteAllEntitiesAsync();
        Task<Result<List<TEntity>>> LoadEntitiesBySqlQueryAsync(string sql);
        Task<Result<T>> LoadValueBySqlQueryAsync<T>(string sql);
        Task<Result> ExecuteSqlAsync(string sql);
    }
}