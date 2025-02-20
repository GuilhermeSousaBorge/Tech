using Tech.Api.Infrastructure.DataAccess;
using Tech.Api.Infrastructure.Security.Cryptography;
using Tech.Api.Infrastructure.Security.Token.Acess;
using Tech.Exception;
using TechDto.Request;
using TechDto.Response;

namespace Tech.Api.Service
{
    public class SignInService
    {

        public UserCreatedResponseDto SignIn(SignInRequestDto request)
        {

            var dbContext = new TechDbContext();

            var user = dbContext.Users.FirstOrDefault(user => user.Email.Equals(request.Email));

            if (user is null)
            {
                throw new InvalidLoginException();
            }

            var cryptography = new BcryptAlgorithm();
            var passwordIsCorrect = cryptography.verify(request.Password, user);

            if (!passwordIsCorrect) 
            {
                throw new InvalidLoginException();
            }

            var token = new JwtTokenGenerator();

            return new UserCreatedResponseDto
            {
                Name = user.Name,
                AcessToken = token.Generate(user)

            };
        }
    }
}
