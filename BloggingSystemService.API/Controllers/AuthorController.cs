using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Services.ServiceContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MediatR;
using BloggingSystemService.Application.Features.Author.Command.CreateAuthor;
using BloggingSystemService.Application.Features.Author.Command.AuthenticateAuthor;

namespace BloggingSystemService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMediator _mediator;

        public AuthorController(IAuthorService authorService, IMediator mediator)
        {
            _authorService = authorService;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAuthor([FromBody] AuthorRequestDto requestDto)
        {

            {
                var result = await _mediator.Send(new RegisterAuthorCommand(requestDto));
                return Ok(result);
            }

        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAuthor([FromBody] AuthorRequestDto requestDto)
        {

            {
                var result = await _mediator.Send(new AuthenticateAuthorCommand(requestDto));
                return Ok(result);
            }

        }
    }
}
