
using System.ComponentModel.DataAnnotations;

namespace Auth.Domain
{
    public struct User
    {
        public string Id;
        public string Name;
        public string Email;
        public string Password;
    }

    public class RegisterPayload
    {
        [Required, MinLength(3)]
        public string? Name { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required, MinLength(8)]
        public string? Password { get; set; }
    }

    public class LoginPayload
    {
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required, MinLength(8)]
        public string? Password { get; set; }
    }

    public interface UserRepository
    {
        void save(User user);
        User? search(string email);
    }
}