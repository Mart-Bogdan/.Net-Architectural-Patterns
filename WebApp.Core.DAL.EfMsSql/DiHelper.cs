using Microsoft.Extensions.DependencyInjection;
using WebApp.Core.DAL.Abstract;
using WebApp.Core.DAL.EfMsSql.Repository;

namespace WebApp.Core.DAL.EfMsSql
{
    public static class DiHelper
    {
        public static IServiceCollection AddBlogRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient<IBlogPostRepository, BlogPostRepository>()
                .AddTransient<IBlogUserRepository, BlogUserRepository>();
        }
    }
}