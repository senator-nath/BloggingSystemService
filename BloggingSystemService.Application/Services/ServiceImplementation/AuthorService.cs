using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Dto.Response;
using BloggingSystemService.Application.Services.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Services.ServiceImplementation
{
    public class AuthorService : IAuthorService
    {
        public Task<AuthorResponseDetails> AuthenticateAuthorAsync(AuthorRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<AuthorResponseDetails> RegisterAuthorAsync(AuthorRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
