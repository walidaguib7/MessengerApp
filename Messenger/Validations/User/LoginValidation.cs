using FluentValidation;
using Messenger.Dtos.User;

namespace Messenger.Validations.User;

public class LoginValidation : AbstractValidator<LoginDto>
{
    public LoginValidation()
    {
        RuleFor(u => u.email)
            .NotNull().EmailAddress().NotEmpty();
            
        RuleFor(u => u.password)
            .NotNull().NotEmpty().MinimumLength(12)
            .WithMessage("password must have 12 letters");
    }
}