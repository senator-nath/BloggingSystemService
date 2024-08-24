using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Dto.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Features.Author.Command.CreateAuthor
{
    public class RegisterAuthorCommand : IRequest<AuthorResponseDto>
    {


        public AuthorRequestDto RequestDto { get; }

        //parameter
        public RegisterAuthorCommand(AuthorRequestDto _requestDto)
        {
            RequestDto = _requestDto;
        }

    }
}

