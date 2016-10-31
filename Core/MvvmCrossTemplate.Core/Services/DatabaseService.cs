using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Platform.Core;
using MvvmCross.Plugins.Sqlite;
using MvvmCrossTemplate.Core.Config;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Interfaces.Services;
using MvvmCrossTemplate.Core.Utils;
using SQLite.Net.Async;
using SQLite.Net.Interop;
using static MvvmCrossTemplate.Core.Utils.Enums.ErrorType;
using Result = MvvmCrossTemplate.Core.Utils.Result;

namespace MvvmCrossTemplate.Core.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IMvxSqliteConnectionFactory _sqliteConnectionFactory;
        private readonly string _databaseName = AppConfig.DatabaseName;
        private SQLiteAsyncConnection _connection;

        public DatabaseService(IMvxSqliteConnectionFactory sqliteConnectionFactory)
        {
            _sqliteConnectionFactory = sqliteConnectionFactory;
        }

        private Result<SQLiteAsyncConnection> GetConnection()
        {
            if (_connection != null)
            {
                return Result.Ok(_connection);
            }
            try
            {
                _sqliteConnectionFactory.CurrentPlattform.SQLiteApi.Config(ConfigOption.Serialized);
                _connection = _sqliteConnectionFactory.GetAsyncConnection(_databaseName);
            }
            catch (Exception e)
            {
                return Result.Fail<SQLiteAsyncConnection>(this, ConnectToDatabase, e);
            }
            return Result.Ok(_connection);
        }

        public Result CreateTable<T>()
        {
            try
            {
                _sqliteConnectionFactory.GetConnection(_databaseName).CreateTable<T>();
            }
            catch (Exception e)
            {
                return Result.Fail(this, CreateDatabaseTable, e);
            }
            return Result.Ok();
        }

        public async Task<Result<T>> InsertAsync<T>(T entity)
        {
            try
            {
                Result<SQLiteAsyncConnection> connectionResult = GetConnection();
                if (connectionResult.IsSuccess)
                {
                    await connectionResult.Value.InsertAsync(entity);
                }
                else
                {
                    return Result.Fail<T>(this, connectionResult);
                }
            }
            catch (Exception e)
            {
                return Result.Fail<T>(this, SaveEntityToDatabase, e);
            }
            return Result.Ok(entity);
        }

        public async Task<Result<T>> UpdateAsync<T>(T entity)
        {
            try
            {
                Result<SQLiteAsyncConnection> connectionResult = GetConnection();
                if (connectionResult.IsSuccess)
                {
                    await connectionResult.Value.UpdateAsync(entity);
                }
                else
                {
                    return Result.Fail<T>(this, connectionResult);
                }
            }
            catch (Exception e)
            {
                return Result.Fail<T>(this, SaveEntityToDatabase, e);
            }
            return Result.Ok(entity);
        }

        public async Task<Result> DeleteAllAsync<T>()
        {
            try
            {
                Result<SQLiteAsyncConnection> connectionResult = GetConnection();
                if (connectionResult.IsSuccess)
                {
                    await connectionResult.Value.DeleteAllAsync<T>();
                }
                else
                {
                    return Result.Fail<T>(this, connectionResult);
                }
            }
            catch (Exception e)
            {
                return Result.Fail(this, DeleteFromDatabase, e);
            }
            return Result.Ok();
        }

        public async Task<Result> DeleteAsync<T>(long id)
        {
            try
            {
                Result<SQLiteAsyncConnection> connectionResult = GetConnection();
                if (connectionResult.IsSuccess)
                {
                    await connectionResult.Value.DeleteAsync<T>(id);
                }
                else
                {
                    return Result.Fail(this, connectionResult);
                }
            }
            catch (Exception e)
            {
                return Result.Fail(this, DeleteFromDatabase, e);
            }
            return Result.Ok();
        }

        public async Task<Result<List<T>>> LoadEntitiesBySqlQueryAsync<T>(string sqlQuery) where T : BaseEntity
        {
            List<T> resultList;
            try
            {
                Result<SQLiteAsyncConnection> connectionResult = GetConnection();
                if (connectionResult.IsSuccess)
                {
                    resultList = await connectionResult.Value.QueryAsync<T>(sqlQuery);
                }
                else
                {
                    return Result.Fail<List<T>>(this, connectionResult);
                }
            }
            catch (Exception e)
            {
                return Result.Fail<List<T>>(this, QueryDatabase, e);
            }
            return Result.Ok(resultList);
        }

        public async Task<Result<List<T>>> LoadEntitiesBySqlQueryAsync<T>(string sqlQuery, params object[] parameters) where T : BaseEntity
        {
            List<T> resultList;
            try
            {
                Result<SQLiteAsyncConnection> connectionResult = GetConnection();
                if (connectionResult.IsSuccess)
                {
                    resultList = await connectionResult.Value.QueryAsync<T>(sqlQuery, parameters);
                }
                else
                {
                    return Result.Fail<List<T>>(this, connectionResult);
                }
            }
            catch (Exception e)
            {
                return Result.Fail<List<T>>(this, QueryDatabase, e);
            }
            return Result.Ok(resultList);
        }

        public async Task<Result> ExecuteSqlAsync(string sql)
        {
            try
            {
                Result<SQLiteAsyncConnection> connectionResult = GetConnection();
                if (connectionResult.IsSuccess)
                {
                    await connectionResult.Value.ExecuteAsync(sql);
                }
                else
                {
                    return Result.Fail(this, connectionResult);
                }
            }
            catch (Exception e)
            {
                return Result.Fail(this, UpdateDatabase, e);
            }
            return Result.Ok();
        }

        public async Task<Result<T>> ExecuteScalarAsync<T>(string sql)
        {
            T value;
            try
            {
                Result<SQLiteAsyncConnection> connectionResult = GetConnection();
                if (connectionResult.IsSuccess)
                {
                    value = await connectionResult.Value.ExecuteScalarAsync<T>(sql);
                }
                else
                {
                    return Result.Fail<T>(this, connectionResult);
                }
            }
            catch (Exception e)
            {
                return Result.Fail<T>(this, ExecuteSql, e);
            }
            return Result.Ok(value);
        }
    }
}