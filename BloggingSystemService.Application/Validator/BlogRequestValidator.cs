using BloggingSystemService.Application.Dto.Request;
using FluentValidation;
using System;

namespace BloggingSystemService.API.Validator
{
    public class BlogRequestValidator : AbstractValidator<BlogRequestDto>
    {
        public BlogRequestValidator()
        {
            RuleFor(x => x.Name)
       .NotEmpty().WithMessage("Name is required.")
       .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url is required.")
                .Must(BeAValidUrl).WithMessage("Url must be a valid URL.");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("AuthorId must be a positive integer.");
        }
        private bool BeAValidUrl(string url)
        {
            // Basic URL validation, can be expanded as needed
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
