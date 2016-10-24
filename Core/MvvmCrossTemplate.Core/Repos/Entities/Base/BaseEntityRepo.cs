using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Interfaces.Repos.EntityRepos.Base;
using MvvmCrossTemplate.Core.Interfaces.Services;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Repos.Entities.Base
{
    public class BaseEntityRepo<TEntity> : IBaseEntityRepo<TEntity> where TEntity : BaseEntity, new()
    {
        public string TableName { get; }
        protected readonly IDatabaseService DatabaseService;

        public BaseEntityRepo(IDatabaseService databaseService)
        {
            TableName = typeof(TEntity).Name;
            DatabaseService = databaseService;
            DatabaseService.CreateTable<TEntity>();
        }

        public async Task<Result<TEntity>> InsertEntityAsync(TEntity entity)
        {
            Result<TEntity> result = await DatabaseService.InsertAsync(entity);
            return result.IsSuccess
                ? Result.Ok(result.Value)
                : Result.Fail<TEntity>(this, result);
        }

        public async Task<Result<TEntity>> UpdateEntityAsync(TEntity entity)
        {
            Result<TEntity> result = await DatabaseService.UpdateAsync(entity);
            return result.IsSuccess
                ? Result.Ok(result.Value)
                : Result.Fail<TEntity>(this, result);
        }

        public async Task<Result<List<TEntity>>> LoadEntitiesBySqlQueryAsync(string sql)
        {
            Result<List<TEntity>> result = await DatabaseService.LoadEntitiesBySqlQueryAsync<TEntity>(sql);
            return result.IsSuccess
                ? Result.Ok(result.Value)
                : Result.Fail<List<TEntity>>(this, result);
        }

        public async Task<Result<T>> LoadValueBySqlQueryAsync<T>(string sql)
        {
            Result<T> result = await DatabaseService.ExecuteScalarAsync<T>(sql);
            return result.IsSuccess
                ? Result.Ok(result.Value)
                : Result.Fail<T>(this, result);
        }

        public async Task<Result> ExecuteSqlAsync(string sql)
        {
            Result result = await DatabaseService.ExecuteSqlAsync(sql);
            return result.IsSuccess
                ? Result.Ok()
                : Result.Fail(this, result);
        }

        public async Task<Result> DeleteEntityAsync(TEntity entity)
        {
            Result result = await DatabaseService.DeleteAsync<TEntity>(entity.Id);
            return result.IsSuccess
                ? Result.Ok()
                : Result.Fail(this, result);
        }

        public async Task<Result> DeleteAllEntitiesAsync()
        {
            Result result = await DatabaseService.DeleteAllAsync<TEntity>();
            return result.IsSuccess
                ? Result.Ok()
                : Result.Fail(this, result);
        }
    }
}