using BloggingSystemService.Application.Contracts.RepositoryContracts;
using BloggingSystemService.Domain.Entity;
using BloggingSystemService.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Persistence.Implementation.RepositoryImplementation
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(BlogDbContext dbContext) : base(dbContext)
        {

        }
    }
}
