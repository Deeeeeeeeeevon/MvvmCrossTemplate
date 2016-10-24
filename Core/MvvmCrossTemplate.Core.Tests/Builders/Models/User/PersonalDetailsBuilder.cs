using Moq;
using MvvmCrossTemplate.Core.Interfaces.Models.User;
using MvvmCrossTemplate.Core.Models.User;
using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.Tests.Helpers;

namespace MvvmCrossTemplate.Core.Tests.Builders.Models.User
{
    public class PersonalDetailsBuilder : BaseBuilder<PersonalDetails>
    {
        private string _lastName;
        private string _firstName;
        private Mock<IPersonalDetails> _mock;

        public PersonalDetailsBuilder()
        {
            _mock = new Mock<IPersonalDetails>();

            _lastName = RandomValues.String;
            _firstName = RandomValues.String;


        }

        public override PersonalDetails Create()
        {
            return new PersonalDetails(_firstName, _lastName);
        }

        public IPersonalDetails CreateMock()
        {
            _mock.SetupGet(x => x.FirstName).Returns(_firstName);
            _mock.SetupGet(x => x.LastName).Returns(_lastName);
            return _mock.Object;
        }

        public PersonalDetailsBuilder With_FirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public PersonalDetailsBuilder With_LastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

    }
}