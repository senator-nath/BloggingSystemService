using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Dto.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Features.Author.Command.AuthenticateAuthor
{
    public class AuthenticateAuthorCommand : IRequest<AuthorResponseDetails>
    {
        public AuthorRequestDto requestDto;

        public AuthenticateAuthorCommand(AuthorRequestDto requestDto)
        {
            this.requestDto = requestDto;
        }
    }
}
