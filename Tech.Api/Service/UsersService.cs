using FluentValidation.Results;
using Tech.Api.Infrastructure.DataAccess;
using Tech.Api.Infrastructure.Security.Cryptography;
using Tech.Api.Infrastructure.Security.Token.Acess;
using Tech.Api.Model;
using Tech.Api.Validators;
using Tech.Exception;
using TechDto.Request;
using TechDto.Response;

namespace Tech.Api.Service
{
    public class UsersService
    {

        //construtor para injetar o repository


        public UserCreatedResponseDto CreateUser(UserRequestDto request)
        {
            var dbContext = new TechDbContext();

            Validate(request, dbContext);
            var cryptography = new BcryptAlgorithm();

            var entity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = cryptography.HashPass(request.Password)
            };

            

            dbContext.Users.Add(entity);
            dbContext.SaveChanges();
            var token = new JwtTokenGenerator();

            return new UserCreatedResponseDto
            {
                Name = entity.Name,
                AcessToken = token.Generate(entity)
            };
        }

        private void Validate(UserRequestDto request, TechDbContext dbContext)
        {
            var validator = new CreateUserValidator();

            var result = validator.Validate(request);

            var emailAlreadyExist = dbContext.Users.Any(user => user.Email.Equals(request.Email));

            if (emailAlreadyExist)
            {
                result.Errors.Add(new ValidationFailure("Email", "Email já cadastrado!"));
            }

            if (!result.IsValid)
            {
                Console.WriteLine(result);
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
