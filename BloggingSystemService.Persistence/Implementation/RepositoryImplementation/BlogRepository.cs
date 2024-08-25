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

    }
}
