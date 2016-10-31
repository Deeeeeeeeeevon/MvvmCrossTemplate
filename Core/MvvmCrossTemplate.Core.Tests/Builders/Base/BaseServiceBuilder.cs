using System.Collections.Generic;
using System.Threading;
using Moq;
using MvvmCrossTemplate.Core.Entities;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Interfaces.Repos.EntityRepos;
using MvvmCrossTemplate.Core.Interfaces.Repos.ModelRepos;
using MvvmCrossTemplate.Core.Interfaces.Services;
using MvvmCrossTemplate.Core.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Models.User;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.ValueObjects;
using static MvvmCrossTemplate.Core.Tests.Helpers.RandomValues;

namespace MvvmCrossTemplate.Core.Tests.Builders.Base
{
    public abstract class BaseServiceBuilder<T> : BaseBuilder<T>
    {
        protected BaseServiceBuilder()
        {
            SetupMockDatabaseService();
            SetupMockUserEntityRepo();
            SetupMockUserModelRepo();
        }
        


        #region DatabaseService

        public Mock<IDatabaseService> MockDatabaseService { get; protected set; }

        private void SetupMockDatabaseService()
        {
            MockDatabaseService = new Mock<IDatabaseService>();
            MockDatabaseService.Setup(x => x.InsertAsync(It.IsAny<BaseEntity>())).ReturnsAsync(Result.Ok(new BaseEntityBuilder().Create()));
            MockDatabaseService.Setup(x => x.UpdateAsync(It.IsAny<BaseEntity>())).ReturnsAsync(Result.Ok(new BaseEntityBuilder().Create()));
            MockDatabaseService.Setup(x => x.LoadEntitiesBySqlQueryAsync<BaseEntity>(It.IsAny<string>())).ReturnsAsync(Result.Ok(new BaseEntityBuilder().CreateList()));
            MockDatabaseService.Setup(x => x.ExecuteScalarAsync<int>(It.IsAny<string>())).ReturnsAsync(Result.Ok(Int));
            MockDatabaseService.Setup(x => x.ExecuteScalarAsync<long>(It.IsAny<string>())).ReturnsAsync(Result.Ok(Long));
            MockDatabaseService.Setup(x => x.ExecuteSqlAsync(It.IsAny<string>())).ReturnsAsync(Result.Ok());
            MockDatabaseService.Setup(x => x.DeleteAsync<T>(It.IsAny<long>())).ReturnsAsync(Result.Ok());
            MockDatabaseService.Setup(x => x.DeleteAllAsync<T>()).ReturnsAsync(Result.Ok());
        }
        
        public BaseServiceBuilder<T> Where_DatabaseService_InsertAsync_returns<TEntity>(Result<TEntity> result)
        {
            MockDatabaseService.Setup(x => x.InsertAsync(It.IsAny<TEntity>())).ReturnsAsync(result);
            return this;
        }

        public BaseServiceBuilder<T> Where_DatabaseService_UpdateAsync_returns<TEntity>(Result<TEntity> result)
        {
            MockDatabaseService.Setup(x => x.UpdateAsync(It.IsAny<TEntity>())).ReturnsAsync(result);
            return this;
        }

        public BaseServiceBuilder<T> Where_DatabaseService_LoadEntitiesBySqlQueryAsync_returns<TEntity>(Result<List<TEntity>> result) where TEntity : BaseEntity
        {
            MockDatabaseService.Setup(x => x.LoadEntitiesBySqlQueryAsync<TEntity>(It.IsAny<string>())).ReturnsAsync(result);
            return this;
        }

        public BaseServiceBuilder<T> Where_DatabaseService_ExecuteScalarAsync_returns<TScalar>(Result<TScalar> result) 
        {
            MockDatabaseService.Setup(x => x.ExecuteScalarAsync<TScalar>(It.IsAny<string>())).ReturnsAsync(result);
            return this;
        }

        public BaseServiceBuilder<T> Where_DatabaseService_ExecuteSqlAsync_returns(Result result)
        {
            MockDatabaseService.Setup(x => x.ExecuteSqlAsync(It.IsAny<string>())).ReturnsAsync(result);
            return this;
        }

        public BaseServiceBuilder<T> Where_databaseservice_DeleteAsync_returns<TEntity>(Result result)
        {
            MockDatabaseService.Setup(x => x.DeleteAsync<TEntity>(It.IsAny<long>())).ReturnsAsync(result);
            return this;
        }

        public BaseServiceBuilder<T> Where_databaseservice_DeleteAllAsync_returns<TEntity>(Result result)
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
            MockUserEntityRepo.Setup(x => x.LoadEntityAsync(It.IsAny<EntityId>())).ReturnsAsync(Result.Ok(new UserEntityBuilder().Create()));
            MockUserEntityRepo.Setup(x => x.SaveEntityAsync(It.IsAny<UserEntity>())).ReturnsAsync(Result.Ok(new UserEntityBuilder().Create()));
            MockUserEntityRepo.Setup(x => x.LoadAllEntitiesAsync()).ReturnsAsync(Result.Ok(new UserEntityBuilder().CreateList()));
        }

        public BaseServiceBuilder<T> Where_UserEntityRepo_LoadAllEntitiesAsync_returns(Result<List<UserEntity>> result)
        {
            MockUserEntityRepo.Setup(x => x.LoadAllEntitiesAsync()).ReturnsAsync(result);
            return this;
        }

        public BaseServiceBuilder<T> Where_UserEntityRepo_LoadEntityAsync_returns(Result<UserEntity> result)
        {
            MockUserEntityRepo.Setup(x => x.LoadEntityAsync(It.IsAny<EntityId>())).ReturnsAsync(result);
            return this;
        }
        
        public BaseServiceBuilder<T> Where_UserEntityRepo_SaveEntityAsync_returns(Result<UserEntity> result)
        {
            MockUserEntityRepo.Setup(x => x.SaveEntityAsync(It.IsAny<UserEntity>())).ReturnsAsync(result);
            return this;
        }
        #endregion

        #region UserModelRepo

        public Mock<IUserModelRepo> MockUserModelRepo { get; protected set; }

        private void SetupMockUserModelRepo()
        {
            MockUserModelRepo = new Mock<IUserModelRepo>();

            MockUserModelRepo.Setup(x => x.LoadAllUserModelsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok(new List<IUserModel> { new UserModelBuilder().CreateMock()}));
            MockUserModelRepo.Setup(x => x.CreateNewUserModel())
                .Returns(new UserModelBuilder().CreateMock());
            MockUserModelRepo.Setup(x => x.SaveUserModelAsync(It.IsAny<CancellationToken>(), It.IsAny<UserModel>()))
                .ReturnsAsync(Result.Ok(new UserModelBuilder().CreateMock()));
        }

        public BaseServiceBuilder<T> Where_UserModelRepo_LoadAllUserModelsAsync_returns(Result<List<IUserModel>> result)
        {
            MockUserModelRepo.Setup(x => x.LoadAllUserModelsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(result);
            return this;
        }

        public BaseServiceBuilder<T> Where_UserModelRepo_CreateNewUser_returns(IUserModel userModel)
        {
            MockUserModelRepo.Setup(x => x.CreateNewUserModel()).Returns(userModel);
            return this;
        }

        public BaseServiceBuilder<T> Where_UserModelRepo_SaveUserModelAsync_returns(Result<IUserModel> result)
        {
            MockUserModelRepo.Setup(x => x.SaveUserModelAsync(It.IsAny<CancellationToken>(), It.IsAny<IUserModel>())).ReturnsAsync(result);
            return this;
        }

        #endregion
    }
}