using MvvmCrossTemplate.Core.Interfaces.Models.User;

namespace MvvmCrossTemplate.Core.Models.User
{
    public class PersonalDetails : IPersonalDetails
    {
        public string FirstName { get; }
        public string LastName { get; }

        public PersonalDetails(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}