using Auth.Domain;

namespace Auth
{
    internal class HardCodedUserRepository : IUserRepository
    {
        private readonly List<User> users;

        public HardCodedUserRepository()
        {
            this.users = new List<User>();
        }

        public void Save(User user)
        {
            this.users.Add(user);
        }

        public User? Search(string email)
        {
            return this.users.Find(element => element.Email.Equals(email));
        }
    }
}