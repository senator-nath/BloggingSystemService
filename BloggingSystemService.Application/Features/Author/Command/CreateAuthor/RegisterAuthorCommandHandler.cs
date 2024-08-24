using BloggingSystemService.API.Validator;
using BloggingSystemService.Application.Contracts.RepositoryContracts;
using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Dto.Response;
using BloggingSystemService.Application.Services.Helper;
using BloggingSystemService.Application.Services.ServiceImplementation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Features.Author.Command.CreateAuthor
{
    public class RegisterAuthorCommandHandler : IRequestHandler<RegisterAuthorCommand, AuthorResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthorService> _logger;
        private readonly JwtTokenGenerator _jwtGenerator;
        private readonly AuthorRequestValidator _validator;

        public RegisterAuthorCommandHandler(IUnitOfWork unitOfWork, ILogger<AuthorService> logger, JwtTokenGenerator jwtGenerator, AuthorRequestValidator validator)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _jwtGenerator = jwtGenerator;
            _validator = validator;
        }
        public async Task<AuthorResponseDto> Handle(RegisterAuthorCommand request, CancellationToken cancellationToken)
        {
            // Validate the request
            var validationResult = _validator.Validate(request.RequestDto);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Registration failed due to validation errors: {Errors}", validationResult.Errors);
                return new AuthorResponseDto
                {
                    Message = "Validation failed"
                };
            }

            _logger.LogInformation("Starting registration process for author with email: {Email}", request.RequestDto.Email);

            // Create the Author entity
            var newAuthor = new Domain.Entity.Author()
            {
                Name = request.RequestDto.Name,
                Email = request.RequestDto.Email,
            };

            // Add the Author entity to the repository
            _unitOfWork.authorRepository.Add(newAuthor);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Registration successful for author with email: {Email}", request.RequestDto.Email);

            return new AuthorResponseDto
            {
                Message = "Registration Successful"
            };
        }
    }
}
