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
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PostResponseDetails> AddPostToBlogAsync(PostRequestDto request)
        {
            Log.Information("Starting process to add a new post to blog with BlogId: {BlogId}", request.BlogId);

            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                DatePublished = request.DatePublished,
                BlogId = request.BlogId,
                AuthorId = request.AuthorId
            };

            _unitOfWork.postRepository.Add(post);
            await _unitOfWork.CompleteAsync();

            Log.Information("Post added successfully to blog with BlogId: {BlogId}", request.BlogId);

            return new PostResponseDetails
            {
                Message = "Post added successfully.",
                IsSuccess = true,
                ResponseDetails = new PostResponseDto
                {
                    Title = post.Title,
                    Content = post.Content,
                    DatePublished = post.DatePublished,
                    BlogId = post.BlogId
                }
            };
        }

        public async Task<PostResponseDetails> DeletePostAsync(PostRequestDto request)
        {
            Log.Information("Starting process to delete post with Id: {PostId}", request.Id);

            var post = await _unitOfWork.postRepository.GetByAsync(p => p.Id == request.Id);

            if (post == null)
            {
                Log.Warning("Deletion failed: Post with Id: {PostId} not found.", request.Id);
                return new PostResponseDetails
                {
                    Message = "Post not found.",
                    IsSuccess = false
                };
            }

            _unitOfWork.postRepository.Delete(post);
            await _unitOfWork.CompleteAsync();

            Log.Information("Post with Id: {PostId} deleted successfully.", request.Id);

            return new PostResponseDetails
            {
                Message = "Post deleted successfully.",
                IsSuccess = true
            };
        }

        public async Task<PaginatedList<PostResponseDto>> GetPostByBlogId(int blogId, int pageNumber, int pageSize)
        {
            Log.Information("Retrieving posts for BlogId: {BlogId} with pagination parameters - PageNumber: {PageNumber}, PageSize: {PageSize}", blogId, pageNumber, pageSize);

            var listPost = await _unitOfWork.postRepository.GetPaginatedAsync(x => x.BlogId == blogId, pageNumber, pageSize, include: query => query.Include(b => b.Blog));

            var dtoItems = listPost.Items.Select(post => new PostResponseDto
            {
                Content = post.Content,
                Title = post.Title,
                DatePublished = post.DatePublished,
                BlogName = post.Blog.Name
            }).ToList();

            Log.Information("Successfully retrieved {PostCount} posts for BlogId: {BlogId}", dtoItems.Count, blogId);

            return new PaginatedList<PostResponseDto>(dtoItems, listPost.TotalCount, pageNumber, pageSize);
        }

        public async Task<PostResponseDetails> UpdatePostAsync(PostRequestDto request)
        {
            Log.Information("Starting process to update post with Id: {PostId}", request.Id);

            var post = await _unitOfWork.postRepository.GetByAsync(p => p.Id == request.Id);

            if (post == null)
            {
                Log.Warning("Update failed: Post with Id: {PostId} not found.", request.Id);
                return new PostResponseDetails
                {
                    Message = "Post not found.",
                    IsSuccess = false
                };
            }

            post.Title = request.Title;
            post.Content = request.Content;
            post.DatePublished = request.DatePublished;
            post.BlogId = request.BlogId;
            post.AuthorId = request.AuthorId;

            _unitOfWork.postRepository.Update(post);
            await _unitOfWork.CompleteAsync();

            Log.Information("Post with Id: {PostId} updated successfully.", request.Id);

            return new PostResponseDetails
            {
                Message = "Post updated successfully.",
                IsSuccess = true,
                ResponseDetails = new PostResponseDto
                {
                    Title = post.Title,
                    Content = post.Content,
                    DatePublished = post.DatePublished,
                    BlogId = post.BlogId
                }
            };
        }
    }
}