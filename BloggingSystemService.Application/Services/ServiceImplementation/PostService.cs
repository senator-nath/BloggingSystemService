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
    public class PostService : IPostService
    {
        public Task<PostResponseDetails> AddPostToBlogAsync(PostRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<PostResponseDetails> DeletePostAsync(PostRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostResponseDetails>> GetAllPostByBlogAsync(string blogName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostResponseDetails>> GetAllPostByBlogIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PostResponseDetails> UpdatePostAsync(PostRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
