using BloggingSystemService.API.Validator;
using BloggingSystemService.Application.Contracts.RepositoryContracts;
using BloggingSystemService.Application.Dto.Response;
using BloggingSystemService.Application.Features.Author.Command.CreateAuthor;
using BloggingSystemService.Application.Services.Helper;
using BloggingSystemService.Application.Services.ServiceImplementation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Features.Author.Command.AuthenticateAuthor
{
    public class AuthenticateAuthorCommandHandler : IRequestHandler<RegisterAuthorCommand, AuthorResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthorService> _logger;
        private readonly JwtTokenGenerator _jwtGenerator;
        private readonly AuthorRequestValidator _validator;
        public AuthenticateAuthorCommandHandler(IUnitOfWork unitOfWork, ILogger<AuthorService> logger, JwtTokenGenerator jwtGenerator, AuthorRequestValidator validator)
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
                _logger.LogWarning("Authentication failed due to validation errors: {Errors}", validationResult.Errors);
                return new AuthorResponseDto
                {
                    Message = "Validation failed"
                };
            }

            _logger.LogInformation("Starting authentication process for author with email: {Email}", request.RequestDto.Email);

            // Check if the author exists
            var authorExist = await _unitOfWork.authorRepository.GetByAsync(a => a.Email == request.RequestDto.Email);
            if (authorExist == null)
            {
                _logger.LogWarning("Authentication failed: Author with email {Email} does not exist.", request.RequestDto.Email);
                return new AuthorResponseDto
                {
                    Message = "User does not exist"
                };
            }

            // Generate JWT token
            string token = _jwtGenerator.GenerateToken(authorExist.Name);

            _logger.LogInformation("Authentication successful for author with email: {Email}", request.RequestDto.Email);

            return new AuthorResponseDto
            {
                Message = "Authentication Successful",
                IsSuccess = true,
                Token = token
            };
        }
    }
}
