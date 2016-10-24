using System.Collections.Generic;
using Moq;
using MvvmCrossTemplate.Core.Entities;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Interfaces.Repos.EntityRepos;
using MvvmCrossTemplate.Core.Interfaces.Services;
using MvvmCrossTemplate.Core.Utils;
using static MvvmCrossTemplate.Core.Tests.Helpers.RandomValues;

namespace MvvmCrossTemplate.Core.Tests.Builders.Base
{
    public abstract class BaseBuilder<T> 
    {
        protected BaseBuilder()
        {
            SetupMockDatabaseService();
            SetupMockUserEntityRepo();
        }

        public abstract T Create();

        public List<T> CreateList(int numberToCreate = 3)
        {
            var list = new List<T>();
            for (int i = 0; i < numberToCreate; i++)
            {
                list.Add(Create());
            }
            return list;
        }


        #region DatabaseService

        public Mock<IDatabaseService> MockDatabaseService { get; protected set; }

        private void SetupMockDatabaseService()
        {
            MockDatabaseService = new Mock<IDatabaseService>();
            MockDatabaseService.Setup(x => x.InsertAsync(It.IsAny<BaseEntity>())).ReturnsAsync(Result.Ok(new BaseEntity()));
            MockDatabaseService.Setup(x => x.UpdateAsync(It.IsAny<BaseEntity>())).ReturnsAsync(Result.Ok(new BaseEntity()));
            MockDatabaseService.Setup(x => x.LoadEntitiesBySqlQueryAsync<BaseEntity>(It.IsAny<string>())).ReturnsAsync(Result.Ok(new List<BaseEntity>()));
            MockDatabaseService.Setup(x => x.ExecuteScalarAsync<int>(It.IsAny<string>())).ReturnsAsync(Result.Ok(Int));
            MockDatabaseService.Setup(x => x.ExecuteScalarAsync<long>(It.IsAny<string>())).ReturnsAsync(Result.Ok(Long));
            MockDatabaseService.Setup(x => x.ExecuteSqlAsync(It.IsAny<string>())).ReturnsAsync(Result.Ok());
            MockDatabaseService.Setup(x => x.DeleteAsync<T>(It.IsAny<long>())).ReturnsAsync(Result.Ok());
            MockDatabaseService.Setup(x => x.DeleteAllAsync<T>()).ReturnsAsync(Result.Ok());
        }

        public BaseBuilder<T> Where_DatabaseService_InsertAsync_returns<TEntity>(Result<TEntity> result)
        {
            MockDatabaseService.Setup(x => x.InsertAsync(It.IsAny<TEntity>())).ReturnsAsync(result);
            return this;
        }

        public BaseBuilder<T> Where_DatabaseService_UpdateAsync_returns<TEntity>(Result<TEntity> result)
        {
            MockDatabaseService.Setup(x => x.UpdateAsync(It.IsAny<TEntity>())).ReturnsAsync(result);
            return this;
        }

        public BaseBuilder<T> Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns<TEntity>(Result<List<TEntity>> result) where TEntity : BaseEntity
        {
            MockDatabaseService.Setup(x => x.LoadEntitiesBySqlQueryAsync<TEntity>(It.IsAny<string>())).ReturnsAsync(result);
            return this;
        }

        public BaseBuilder<T> Where_DatabaseService_ExecuteScalarAsync_returns<TScalar>(Result<TScalar> result) 
        {
            MockDatabaseService.Setup(x => x.ExecuteScalarAsync<TScalar>(It.IsAny<string>())).ReturnsAsync(result);
            return this;
        }

        public BaseBuilder<T> Where_DatabaseService_ExecuteSqlAsync_returns(Result result)
        {
            MockDatabaseService.Setup(x => x.ExecuteSqlAsync(It.IsAny<string>())).ReturnsAsync(result);
            return this;
        }

        public BaseBuilder<T> Where_databaseservice_DeleteAsync_returns<TEntity>(Result result)
        {
            MockDatabaseService.Setup(x => x.DeleteAsync<TEntity>(It.IsAny<long>())).ReturnsAsync(result);
            return this;
        }

        public BaseBuilder<T> Where_databaseservice_DeleteAllAsync_returns<TEntity>(Result result)
        {
            MockDatabaseService.Setup(x => x.DeleteAllAsync<TEntity>()).ReturnsAsync(result);
            return this;
        }
        #endregion

        #region UserEntityRepo

        public Mock<IUserEntityRepo> MockUserEntityRepo { get; private set; }

        private void SetupMockUserEntityRepo()
        {
            MockUserEntityRepo = new Mock<IUserEntityRepo>();
            MockUserEntityRepo.Setup(x => x.LoadEntityAsync(It.IsAny<EntityId>())).ReturnsAsync(Result.Ok(new UserEntity()));
            MockUserEntityRepo.Setup(x => x.SaveEntityAsync(It.IsAny<UserEntity>())).ReturnsAsync(Result.Ok(new UserEntity()));
        }

        public BaseBuilder<T> Where_UserEntityRepo_LoadEntityAsync_returns(Result<UserEntity> result)
        {
            MockUserEntityRepo.Setup(x => x.LoadEntityAsync(It.IsAny<EntityId>())).ReturnsAsync(result);
            return this;
        }
        
        public BaseBuilder<T> Where_UserEntityRepo_SaveEntityAsync_returns(Result<UserEntity> result)
        {
            MockUserEntityRepo.Setup(x => x.SaveEntityAsync(It.IsAny<UserEntity>())).ReturnsAsync(result);
            return this;
        }
        #endregion
    }
}