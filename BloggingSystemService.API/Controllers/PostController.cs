﻿using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Services.ServiceContract;
using BloggingSystemService.Application.Services.ServiceImplementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BloggingSystemService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddPostToBlog([FromBody] PostRequestDto request)
        {


            var result = await _postService.AddPostToBlogAsync(request);
            return Ok(result);



        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeletePost([FromBody] PostRequestDto request)
        {
            var result = await _postService.DeletePostAsync(request);
            return Ok(result);


        }


        [HttpGet]
        [Route("by-blog-id/{blogId}")]
        public async Task<IActionResult> GetBlogsByAuthorId(int blogId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _postService.GetPostByBlogId(blogId, pageNumber, pageSize);
            return Ok(result);

        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdatePost([FromBody] PostRequestDto request)
        {


            var result = await _postService.UpdatePostAsync(request);
            return Ok(result);



        }
    }
}
