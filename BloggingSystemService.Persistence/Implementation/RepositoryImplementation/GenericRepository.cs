using BloggingSystemService.Application.Contracts.RepositoryContracts;
using BloggingSystemService.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Persistence.Implementation.RepositoryImplementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BlogDbContext _dbContext;
        public GenericRepository(BlogDbContext dbContext)
        {

        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }


        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
