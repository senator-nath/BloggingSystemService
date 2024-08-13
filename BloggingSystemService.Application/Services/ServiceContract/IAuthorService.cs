using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Services.ServiceContract
{
    public interface IAuthorService
    {

        Task<AuthorResponseDetails> RegisterAuthorAsync(AuthorRequestDto request);
        Task<AuthorResponseDetails> AuthenticateAuthorAsync(AuthorRequestDto request);


    }
}
