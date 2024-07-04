
using System.ComponentModel.DataAnnotations;

namespace Auth.Domain
{
    public class User
    {
        public required string Id;
        public required string Name;
        public required string Email;
        public required string Password;
    }

    public class RegisterPayload
    {
        [Required, MinLength(3)]
        public required string Name { get; set; }
        [Required, EmailAddress]
        public required string Email { get; set; }
        [Required, MinLength(8)]
        public required string Password { get; set; }
    }

    public class LoginPayload
    {
        [Required, EmailAddress]
        public required string Email { get; set; }
        [Required, MinLength(8)]
        public required string Password { get; set; }
    }

    public interface IUserRepository
    {
        void Save(User user);
        User? Search(string email);
    }
}