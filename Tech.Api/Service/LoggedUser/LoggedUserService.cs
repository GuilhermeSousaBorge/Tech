using System.IdentityModel.Tokens.Jwt;
using Tech.Api.Infrastructure.DataAccess;
using Tech.Api.Model;

namespace Tech.Api.Service.LoggedUser
{
    public class LoggedUserService
    {

        private readonly HttpContext _httpContext;

        public LoggedUserService(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public User User(TechDbContext dbContext)
        {
            var authentication = _httpContext.Request.Headers.Authorization.ToString();

            var token = authentication["Bearer ".Length..].Trim();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

            var userId = Guid.Parse(identifier);

            return dbContext.Users.First(user => user.Id == userId);
        }
    }
}
