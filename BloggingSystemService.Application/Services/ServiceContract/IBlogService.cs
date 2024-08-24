using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Services.ServiceContract
{
    public interface IBlogService
    {

        Task<IEnumerable<BlogResponseDetails>> GetAllBlogByAuthorIdAsync(int id);
        Task<BlogResponseDetails> AddBlogAsync(BlogRequestDto request);
        Task<BlogResponseDetails> UpdateBlogAsync(BlogRequestDto request, int id);
        Task<BlogResponseDetails> DeleteBlogAsync(int id);
    }
}
