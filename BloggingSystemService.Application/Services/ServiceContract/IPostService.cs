using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Services.ServiceContract
{
    public interface IPostService
    {

        Task<IEnumerable<PostResponseDetails>> GetAllPostByBlogIdAsync(int id);
        Task<IEnumerable<PostResponseDetails>> GetAllPostByBlogAsync(string blogName);
        Task<PostResponseDetails> AddPostToBlogAsync(PostRequestDto request);
        Task<PostResponseDetails> UpdatePostAsync(PostRequestDto request);
        Task<PostResponseDetails> DeletePostAsync(PostRequestDto request);
    }
}
