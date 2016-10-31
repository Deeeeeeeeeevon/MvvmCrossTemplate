using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Interfaces.Services
{
    public interface IDatabaseService
    {
        Result CreateTable<T>();

        Task<Result<List<T>>> LoadEntitiesBySqlQueryAsync<T>(string sqlQuery) where T : BaseEntity;
        Task<Result<List<T>>> LoadEntitiesBySqlQueryAsync<T>(string sqlQuery, params object[] parameters) where T : BaseEntity;
        Task<Result> DeleteAllAsync<T>();
        Task<Result<T>> InsertAsync<T>(T entity);
        Task<Result<T>> UpdateAsync<T>(T entity);
        Task<Result> DeleteAsync<T>(long id);
        Task<Result> ExecuteSqlAsync(string sql);
        Task<Result<T>> ExecuteScalarAsync<T>(string sql);
    }
}