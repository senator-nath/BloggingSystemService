using BloggingSystemService.Application.Contracts.RepositoryContracts;
using BloggingSystemService.Application.Dto.Request;
using BloggingSystemService.Application.Dto.Response;
using BloggingSystemService.Application.Helper;
using BloggingSystemService.Application.Services.ServiceContract;
using BloggingSystemService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
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
            Log.Information("Starting blog creation process for blog with name: {Name}", request.Name);

            var blogExist = await _unitOfWork.blogRepository.GetByAsync(b => b.Name == request.Name && b.Url == request.Url);
            if (blogExist != null)
            {
                Log.Warning("Blog creation failed: Blog with name {Name} or URL {Url} already exists", request.Name, request.Url);
                return new BlogResponseDetails
                {
                    Message = "Blog or URL already exists",
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

            Log.Information("Blog created successfully with name: {Name}", request.Name);
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
            Log.Information("Starting blog deletion process for blog with ID: {Id}", id);

            var blog = await _unitOfWork.blogRepository.GetByAsync(b => b.Id == id);
            if (blog == null)
            {
                Log.Warning("Blog deletion failed: Blog with ID {Id} not found", id);
                return new BlogResponseDetails
                {
                    Message = "Blog not found.",
                    IsSuccess = false
                };
            }

            _unitOfWork.blogRepository.Delete(blog);
            await _unitOfWork.CompleteAsync();

            Log.Information("Blog deleted successfully with ID: {Id}", id);
            return new BlogResponseDetails
            {
                Message = "Blog deleted successfully.",
                IsSuccess = true
            };
        }

        public async Task<PaginatedList<BlogResponseDto>> GetBlogByAuthorId(int authorId, int pageNumber, int pageSize)
        {
            Log.Information("Fetching blogs for author with ID: {AuthorId}", authorId);

            var listBlog = await _unitOfWork.blogRepository.GetPaginatedAsync(
                x => x.AuthorId == authorId,
                pageNumber,
                pageSize,
                include: query => query.Include(b => b.Author)
            );

            var dtoItems = listBlog.Items.Select(blog => new BlogResponseDto
            {
                Name = blog.Name,
                AuthorName = blog.Author.Name,
            }).ToList();

            Log.Information("Retrieved {Count} blogs for author with ID: {AuthorId}", listBlog.TotalCount, authorId);
            return new PaginatedList<BlogResponseDto>(dtoItems, listBlog.TotalCount, pageNumber, pageSize);
        }

        public async Task<BlogResponseDetails> UpdateBlogAsync(BlogRequestDto request, int id)
        {
            Log.Information("Starting blog update process for blog with ID: {Id}", id);

            var blog = await _unitOfWork.blogRepository.GetByAsync(b => b.Id == id);
            if (blog == null)
            {
                Log.Warning("Blog update failed: Blog with ID {Id} not found", id);
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

            Log.Information("Blog updated successfully with ID: {Id}", id);
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
