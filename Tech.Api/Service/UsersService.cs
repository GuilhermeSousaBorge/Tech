using Tech.Api.Infrastructure;
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
            Validate(request);

            var entity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            var dbContext = new TechDbContext();

            dbContext.Users.Add(entity);
            dbContext.SaveChanges();

            return new UserCreatedResponseDto
            {
                Name = entity.Name,
            };
        }

        private void Validate(UserRequestDto request)
        {
            var validator = new CreateUserValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
