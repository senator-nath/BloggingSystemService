using BloggingSystemService.API.Validator;
using BloggingSystemService.Application.Contracts.RepositoryContracts;
using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Dto.Response;
using BloggingSystemService.Application.Services.Helper;
using BloggingSystemService.Application.Services.ServiceContract;
using BloggingSystemService.Domain.Entity;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Services.ServiceImplementation
{




    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly JwtTokenGenerator _jwtGenerator;
        private readonly AuthorRequestValidator _validator;

        public AuthorService(IUnitOfWork unitOfWork, JwtTokenGenerator jwtGenerator, AuthorRequestValidator validator)
        {
            _unitOfWork = unitOfWork;
            _jwtGenerator = jwtGenerator;
            _validator = validator;
        }

        public async Task<AuthorResponseDetails> AuthenticateAuthorAsync(AuthorRequestDto request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                Log.Warning("Authentication failed due to validation errors: {Errors}", validationResult.Errors);
                return new AuthorResponseDetails
                {
                    Message = "Validation failed",

                };
            }
            Log.Information("Starting authentication process for author with email: {Email}", request.Email);

            var authorExist = await _unitOfWork.authorRepository.GetByAsync(a => a.Email == request.Email);
            if (authorExist == null)
            {
                Log.Warning("Authentication failed: Author with email {Email} does not exist.", request.Email);
                return new AuthorResponseDetails
                {
                    Message = "User does not exist"
                };
            }
            string token = _jwtGenerator.GenerateToken(authorExist.Name);
            Log.Information("Authentication successful for author with email: {Email}", request.Email);
            return new AuthorResponseDetails
            {
                Message = "Authentication Successful",
                IsSuccess = true,
                Token = token
            };
        }

        public async Task<AuthorResponseDetails> RegisterAuthorAsync(AuthorRequestDto request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                Log.Warning("Registration failed due to validation errors: {Errors}", validationResult.Errors);
                return new AuthorResponseDetails
                {
                    Message = "Validation failed",
                    IsSuccess = false
                };
            }

            Log.Information("Starting registration process for author with email: {Email}", request.Email);

            // Check if the email already exists
            bool emailExists = await _unitOfWork.authorRepository.ExistsAsync(a => a.Email == request.Email);
            if (emailExists)
            {
                Log.Warning("Registration failed for email: {Email} - Email already exists", request.Email);
                return new AuthorResponseDetails
                {
                    Message = "Email already exists",
                    IsSuccess = false
                };
            }

            // Create a new author
            var author = new Author
            {
                Name = request.Name,
                Email = request.Email,
            };

            _unitOfWork.authorRepository.Add(author);
            await _unitOfWork.CompleteAsync();

            Log.Information("Registration successful for author with email: {Email}", request.Email);
            return new AuthorResponseDetails
            {
                Message = "Registration Successful",
                IsSuccess = true
            };
        }
    }
}