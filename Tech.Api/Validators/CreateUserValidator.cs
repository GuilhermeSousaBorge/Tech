using FluentValidation;
using TechDto.Request;

namespace Tech.Api.Validators
{
    public class CreateUserValidator : AbstractValidator<UserRequestDto>
    {

        public CreateUserValidator()
        {

            RuleFor(request => request.Name).NotEmpty().WithMessage("O campo nome é obrigatorio");
            RuleFor(request => request.Email).EmailAddress().WithMessage("O campo email não é valido");
            RuleFor(request => request.Password).NotEmpty().WithMessage("O campo senha é obrigatorio");
            When(request => string.IsNullOrEmpty(request.Password) == false, () =>
            {
                RuleFor(request => request.Password.Length).GreaterThanOrEqualTo(6).WithMessage("A senha deve conter 6 ou mais caracteres");
            });
        }
    }
}
