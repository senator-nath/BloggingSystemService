using BloggingSystemService.Application.Dto.Request;
using FluentValidation;
using System;

namespace BloggingSystemService.API.Validator
{
    public class PostRequestValidator : AbstractValidator<PostRequestDto>
    {
        public PostRequestValidator()
        {
            RuleFor(x => x.Title)
        .NotEmpty().WithMessage("Title is required.")
        .Length(5, 200).WithMessage("Title must be between 5 and 200 characters.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MinimumLength(10).WithMessage("Content must be at least 10 characters long.");

            RuleFor(x => x.DatePublished)
                .NotEmpty().WithMessage("DatePublished is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("DatePublished cannot be in the future.");

            RuleFor(x => x.BlogId)
                .GreaterThan(0).WithMessage("BlogId must be a positive integer.");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("AuthorId must be a positive integer.");
        }
    }
}
