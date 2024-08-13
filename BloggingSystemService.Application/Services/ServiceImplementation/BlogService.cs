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
    public class BlogService : IBlogService
    {
        public Task<BlogResponseDetails> AddBlogAsync(BlogRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<BlogResponseDetails> DeleteBlogAsync(BlogRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogResponseDetails>> GetAllBlogAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogResponseDetails>> GetAllBlogByAuthorAsync(string Author)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogResponseDetails>> GetAllBlogByAuthorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogResponseDetails> UpdateBlogAsync(BlogRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
