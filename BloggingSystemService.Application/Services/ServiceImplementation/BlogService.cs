using BloggingSystemService.Application.Contracts.RepositoryContracts;
using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Dto.Response;
using BloggingSystemService.Application.Services.ServiceContract;
using BloggingSystemService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Services.ServiceImplementation
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BlogResponseDetails> AddBlogAsync(BlogRequestDto request)
        {
            var BlogExist = await _unitOfWork.blogRepository.GetByAsync(b => b.Name == request.Name && b.Url == request.Url);
            if (BlogExist != null)
            {
                return new BlogResponseDetails
                {
                    Message = "Blog or Url already exist",
                    IsSuccess = false
                };
            }
            var blog = new Blog
            {
                Name = request.Name,
                Url = request.Url,
                AuthorId = request.AuthorId,

            };

            _unitOfWork.blogRepository.Add(blog);
            await _unitOfWork.CompleteAsync();

            return new BlogResponseDetails
            {
                Message = "Blog added successfully.",
                IsSuccess = true,
                ResponseDetails = new BlogResponseDto
                {
                    Id = blog.Id,
                    Name = blog.Name,

                    AuthorId = blog.AuthorId
                }
            };
        }

        public async Task<BlogResponseDetails> DeleteBlogAsync(int id)
        {
            var blog = await _unitOfWork.blogRepository.GetByAsync(b => b.Id == id);

            if (blog == null)
            {
                return new BlogResponseDetails
                {
                    Message = "Blog not found.",
                    IsSuccess = false
                };
            }

            _unitOfWork.blogRepository.Delete(blog);
            await _unitOfWork.CompleteAsync();

            return new BlogResponseDetails
            {
                Message = "Blog deleted successfully.",
                IsSuccess = true
            };
        }

        public Task<IEnumerable<BlogResponseDetails>> GetAllBlogByAuthorIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<BlogResponseDetails> UpdateBlogAsync(BlogRequestDto request, int id)
        {
            var blog = await _unitOfWork.blogRepository.GetByAsync(b => b.Id == id);

            if (blog == null)
            {
                return new BlogResponseDetails
                {
                    Message = "Blog not found.",
                    IsSuccess = false
                };
            }

            blog.Name = request.Name;
            blog.Url = request.Url;


            _unitOfWork.blogRepository.Update(blog);
            await _unitOfWork.CompleteAsync();

            return new BlogResponseDetails
            {
                Message = "Blog updated successfully.",
                IsSuccess = true,
                ResponseDetails = new BlogResponseDto
                {
                    Id = blog.Id,
                    Name = blog.Name,

                    AuthorId = blog.AuthorId
                }
            };
        }
    }
}

