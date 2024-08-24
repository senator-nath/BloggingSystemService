using BloggingSystemService.Application.Contracts.RepositoryContracts;
using BloggingSystemService.Persistence.Data;
using BloggingSystemService.Persistence.Implementation.RepositoryImplementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            services.AddDbContext<BlogDbContext>(Options => Options.UseSqlServer(config.GetConnectionString("defaultConnection")));
            return services;
        }
    }
}
