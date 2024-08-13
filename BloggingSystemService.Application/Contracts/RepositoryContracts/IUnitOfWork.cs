using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Contracts.RepositoryContracts
{
    public interface IUnitOfWork
    {
        IBlogRepository blogRepository { get; }
        IAuthorRepository authorRepository { get; }
        IPostRepository postRepository { get; }
        Task<int> CompleteAsync();
    }
}
