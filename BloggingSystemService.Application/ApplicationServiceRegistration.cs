using BloggingSystemService.Application.Services.Helper;
using BloggingSystemService.Application.Services.ServiceContract;
using BloggingSystemService.Application.Services.ServiceImplementation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BloggingSystemService.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<JwtTokenGenerator>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
