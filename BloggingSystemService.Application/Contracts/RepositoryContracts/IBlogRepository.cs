using BloggingSystemService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Contracts.RepositoryContracts
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        //Task<(IEnumerable<Blog> Blogs, int TotalCount)> GetBlogsByAuthorIdAsync(int authorId, int pageNumber, int pageSize);


    }
}
