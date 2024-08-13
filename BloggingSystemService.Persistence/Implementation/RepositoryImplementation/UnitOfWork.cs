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
        private readonly BlogDbContext _dbContest;
        public IBlogRepository blogRepository { get; }

        public IAuthorRepository authorRepository { get; }

        public IPostRepository postRepository { get; }
        public UnitOfWork(BlogDbContext dbContest)
        {
            _dbContest = dbContest;

            blogRepository = new BlogRepository(dbContest);
            authorRepository = new AuthorRepository(dbContest);
            postRepository = new PostRepository(dbContest);
        }
        public async Task<int> CompleteAsync()
        {
            var save = await _dbContest.SaveChangesAsync();
            return save;
        }

        public void Dispose()
        {
            _dbContest.DisposeAsync();
        }
    }
}
