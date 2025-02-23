using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tech.Api.Model;

namespace Tech.Api.Infrastructure.Security.Token.Acess
{
    public class JwtTokenGenerator
    {

        public string Generate(User user)
        {

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(60),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
        private static SymmetricSecurityKey SecurityKey()
        {
            var key = Environment.GetEnvironmentVariable("TOKEN_KEY") ?? throw new System.Exception("Chave nao ocnfigurada!");

            var symmetricKey = Encoding.UTF8.GetBytes(key);
            return new SymmetricSecurityKey(symmetricKey);
        }
    }

    
}
