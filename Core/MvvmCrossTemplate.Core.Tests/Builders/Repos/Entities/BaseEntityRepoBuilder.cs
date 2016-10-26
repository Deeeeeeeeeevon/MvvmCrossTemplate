using System.Collections.Generic;
using MvvmCrossTemplate.Core.Entities.Base;
using MvvmCrossTemplate.Core.Repos.Entities.Base;
using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Tests.Builders.Repos.Entities
{
    public class BaseEntityRepoBuilder : BaseServiceBuilder<BaseEntityRepo<BaseEntity>>
    {
        

        public override BaseEntityRepo<BaseEntity> Create()
        {
            return new BaseEntityRepo<BaseEntity>(MockDatabaseService.Object);    
        }


    }
}