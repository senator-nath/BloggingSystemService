using BloggingSystemService.Application.Dto.Request;
using FluentValidation;

namespace BloggingSystemService.API.Validator
{
    public class AuthorRequestValidator : AbstractValidator<AuthorRequestDto>
    {
        public AuthorRequestValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required.")
           .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");
        }
    }
}
