using System.Collections.Generic;
using Moq;
using MvvmCrossTemplate.Core.Entities;
using MvvmCrossTemplate.Core.Tests.Builders.Base;

namespace MvvmCrossTemplate.Core.Tests.Builders.Entities
{
    public class UserEntityBuilder : BaseBuilder<UserEntity>
    {
        private UserEntity _userEntity;

        public UserEntityBuilder()
        {
            _userEntity = new UserEntity();
        }
        
        public override UserEntity Create()
        {
            return _userEntity;
        }

        public UserEntityBuilder With_LastName(string lastName)
        {
            _userEntity.LastName = lastName;
            return this;
        }

        public UserEntityBuilder With_FirstName(string firstName)
        {
            _userEntity.FirstName = firstName;
            return this;
        }
    }
}