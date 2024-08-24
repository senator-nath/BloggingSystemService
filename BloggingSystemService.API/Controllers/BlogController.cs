using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Services.ServiceContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BloggingSystemService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddBlog([FromBody] BlogRequestDto request)
        {

            var result = await _blogService.AddBlogAsync(request);

            return Ok(result);



        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var result = await _blogService.DeleteBlogAsync(id);
            return Ok(result);
        }




        [HttpGet]
        [Route("by-author-id/{id}")]
        public async Task<IActionResult> GetAllBlogsByAuthorId(int id)
        {
            var result = await _blogService.GetAllBlogByAuthorIdAsync(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] BlogRequestDto request)
        {

            var result = await _blogService.UpdateBlogAsync(request, id);
            return Ok(result);



        }
    }
}