using System.Collections.Generic;
using Moq;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.Tests.Builders.Utils;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Tests.Builders.Models.User
{
    public class UserModelBuilder : BaseBuilder<UserModel>
    {
        private IPersonalDetails _personalDetails;
        private EntityId _entityId;
        public Mock<IUserModel> MockUserModel;
        private Result _updateResult;

        public UserModelBuilder()
        {
            MockUserModel = new Mock<IUserModel>();
            _personalDetails = new PersonalDetailsBuilder().CreateMock();
            _entityId = new EntityIdBuilder().Create();
            _updateResult = Result.Ok();
        }

        public override UserModel Create()
        {
            return new UserModel(_entityId, _personalDetails);
        }

        public UserModelBuilder With_PersonalDetails(IPersonalDetails personalDetails)
        {
            _personalDetails = personalDetails;
            return this;
        }

        public UserModelBuilder With_EntityId(EntityId entityId)
        {
            _entityId = entityId;
            return this;
        }

        public IUserModel CreateMock()
        {
            MockUserModel.SetupGet(x => x.EntityId).Returns(_entityId);
            MockUserModel.SetupGet(x => x.PersonalDetails).Returns(_personalDetails);
            MockUserModel.Setup(x => x.UpdateUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(_updateResult);
            return MockUserModel.Object;
        }

        public UserModelBuilder Where_UpdateUserName_returns(Result result)
        {
            _updateResult = result;
            return this;
        }
    }
}