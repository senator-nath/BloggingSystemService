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
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PostResponseDetails> AddPostToBlogAsync(PostRequestDto request)
        {
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

            return new PostResponseDetails
            {
                Message = "Post added successfully.",
                IsSuccess = true,
                ResponseDetails = new PostResponseDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    DatePublished = post.DatePublished,
                    BlogId = post.BlogId,
                    AuthorId = post.AuthorId
                }
            };
        }

        public async Task<PostResponseDetails> DeletePostAsync(PostRequestDto request)
        {
            var post = await _unitOfWork.postRepository.GetByAsync(p => p.Id == request.Id);

            if (post == null)
            {
                return new PostResponseDetails
                {
                    Message = "Post not found.",
                    IsSuccess = false
                };
            }

            _unitOfWork.postRepository.Delete(post);
            await _unitOfWork.CompleteAsync();

            return new PostResponseDetails
            {
                Message = "Post deleted successfully.",
                IsSuccess = true
            };
        }



        public async Task<IEnumerable<PostResponseDetails>> GetAllPostByBlogIdAsync(int id)
        {
            var posts = await _unitOfWork.postRepository.GetWhere(p => p.BlogId == id);

            return posts.Select(post => new PostResponseDetails
            {
                IsSuccess = true,
                ResponseDetails = new PostResponseDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    DatePublished = post.DatePublished,
                    BlogId = post.BlogId,
                    AuthorId = post.AuthorId
                }
            }).ToList();
        }

        public async Task<PostResponseDetails> UpdatePostAsync(PostRequestDto request)
        {
            var post = await _unitOfWork.postRepository.GetByAsync(p => p.Id == request.Id);

            if (post == null)
            {
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

            return new PostResponseDetails
            {
                Message = "Post updated successfully.",
                IsSuccess = true,
                ResponseDetails = new PostResponseDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    DatePublished = post.DatePublished,
                    BlogId = post.BlogId,
                    AuthorId = post.AuthorId
                }
            };
        }
    }
}