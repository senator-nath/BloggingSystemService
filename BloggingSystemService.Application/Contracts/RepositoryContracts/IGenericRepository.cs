using BloggingSystemService.Application.Helper;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Contracts.RepositoryContracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByAsync(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<T> GetByIdAsync(T id);

        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        Task<PaginatedList<T>> GetPaginatedAsync(
        Expression<Func<T, bool>> predicate,
        int pageNumber,
        int pageSize,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
