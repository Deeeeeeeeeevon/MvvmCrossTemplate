using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Extensions;
using MvvmCrossTemplate.Core.Interfaces.Repos.EntityRepos.Base;
using MvvmCrossTemplate.Core.Interfaces.Services;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;

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

        public async Task<Result<TEntity>> LoadEntityAsync(EntityId entityId)
        {
            if (entityId.IsEmpty())
            {
                return Result.Fail<TEntity>(this, ErrorType.NotFound);
            }
            var sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT * FROM ").Append(TableName).Append(" WHERE");
            if (entityId.LocalId > 0)
                sqlQuery.Append(" Id = ").Append(entityId.LocalId).Append(" OR");
            if (entityId.ObjectId > 0)
                sqlQuery.Append(" ObjectId = ").Append(entityId.ObjectId).Append(" OR");
            if (entityId.UniqueId != "")
                sqlQuery.Append(" UniqueId = \'").Append(entityId.UniqueId).Append("\'").Append(" OR");
            sqlQuery.Length -= 3;

            Result<List<TEntity>> result = await DatabaseService.LoadEntitiesBySqlQueryAsync<TEntity>(sqlQuery.ToString());
            if (result.IsFailure)
            {
                return Result.Fail<TEntity>(this, result);
            }
            if (result.Value.Count > 1)
            {
                return Result.Fail<TEntity>(this, ErrorType.DuplicateResults).AddData("entityId", entityId.FullId);
            }
            if (result.Value.Count == 0)
            {
                return Result.Fail<TEntity>(this, ErrorType.NotFound).AddData("entityId", entityId.FullId);
            }
            return Result.Ok(result.Value.First());
        }

        public async Task<Result<TEntity>> SaveEntityAsync(TEntity entityToSave)
        {
            Result<TEntity> loadResult = await LoadEntityAsync(entityToSave.EntityId);

            if (loadResult.IsFailure)
            {
                if (entityToSave.EntityId.IsEmpty())
                {
                    entityToSave.UniqueId = Guid.NewGuid().ToString();
                }
                Result<TEntity> insertResult = await DatabaseService.InsertAsync(entityToSave);
                return Result.Return(this, insertResult);
            }

            entityToSave.LocalId = loadResult.Value.LocalId;
            Result<TEntity> updateResult = await DatabaseService.UpdateAsync(entityToSave);
            return Result.Return(this, updateResult);
        }

        public async Task<Result<List<TEntity>>> LoadEntitiesBySqlQueryAsync(string sql)
        {
            Result<List<TEntity>> result = await DatabaseService.LoadEntitiesBySqlQueryAsync<TEntity>(sql);
            return result.IsSuccess
                ? Result.Ok(result.Value)
                : Result.Fail<List<TEntity>>(this, result);
        }

        public async Task<Result<List<TEntity>>> LoadAllEntitiesAsync()
        {
            var sql = new StringBuilder();
            sql.Append("SELECT * FROM ").Append(TableName);
            var loadEntitiesResult = await DatabaseService.LoadEntitiesBySqlQueryAsync<TEntity>(sql.ToString());
            return loadEntitiesResult.IsFailure 
                ? Result.Fail<List<TEntity>>(this, loadEntitiesResult) 
                : Result.Ok(loadEntitiesResult.Value);
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
        
    }
}