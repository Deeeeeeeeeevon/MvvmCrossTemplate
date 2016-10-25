using System.Collections.Generic;
using MvvmCrossTemplate.Core.Entities;
using MvvmCrossTemplate.Core.Interfaces.Repos.ModelRepos;
using MvvmCrossTemplate.Core.Repos.Models;
using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Tests.Builders.Repos.Models
{
    public class UserModelRepoBuilder : BaseBuilder<IUserModelRepo>
    {
        public override IUserModelRepo Create()
        {
            return new UserModelRepo(MockUserEntityRepo.Object);
        }

    }
}