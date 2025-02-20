using Tech.Api.Model;

namespace Tech.Api.Infrastructure.Security.Cryptography
{
    public class BcryptAlgorithm
    {
        public string HashPass(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public bool verify(string password, User user) => BCrypt.Net.BCrypt.Verify(password, user.Password);
    }
}
