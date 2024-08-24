using BloggingSystemService.Application.Contracts.RepositoryContracts;
using BloggingSystemService.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Persistence.Implementation.RepositoryImplementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BlogDbContext _dbContext;
        public IBlogRepository blogRepository { get; }

        public IAuthorRepository authorRepository { get; }

        public IPostRepository postRepository { get; }
        public UnitOfWork(BlogDbContext dbContext)
        {
            _dbContext = dbContext;

            blogRepository = new BlogRepository(dbContext);
            authorRepository = new AuthorRepository(dbContext);
            postRepository = new PostRepository(dbContext);
        }
        public async Task<int> CompleteAsync()
        {
            var save = await _dbContext.SaveChangesAsync();
            return save;
        }

        public void Dispose()
        {
            _dbContext.DisposeAsync();
        }
    }
}
