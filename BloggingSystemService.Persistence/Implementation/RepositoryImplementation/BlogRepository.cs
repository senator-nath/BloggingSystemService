using BloggingSystemService.Application.Contracts.RepositoryContracts;
using BloggingSystemService.Domain.Entity;
using BloggingSystemService.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Persistence.Implementation.RepositoryImplementation
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly BlogDbContext _dbContext;
        public BlogRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        //public async Task<(IEnumerable<Blog> Blogs, int TotalCount)> GetBlogsByAuthorIdAsync(int authorId, int pageNumber, int pageSize)
        //{
        //    var query = _dbContext.Blogs.Where(b => b.AuthorId == authorId);
        //    var totalCount = await query.CountAsync();

        //    var blogs = await query
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    return (blogs, totalCount);
        //}
    }
}
