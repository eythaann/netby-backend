using Auth.Domain;

namespace Auth
{
    internal class HardCodedUserRepository : UserRepository
    {
        private List<User> users;

        public HardCodedUserRepository()
        {
            this.users = new List<User>();
        }

        public void save(User user)
        {
            this.users.Add(user);
        }

        public User? search(string email)
        {
            return this.users.Find(element => element.Email.Equals(email));
        }
    }
}