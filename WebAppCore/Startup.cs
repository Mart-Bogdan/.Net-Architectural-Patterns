using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkWithDB.DAL.Standard.Abstract;
using WorkWithDB.Standartd.DAL.SqlServer;
using WorkWithDB.Standartd.DAL.SqlServer.Infrastructure;
using WorkWithDB.Standartd.DAL.SqlServer.Repository;

namespace WebAppCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //services.AddDbContext<MusicServiceContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<User, Role>(config =>
            //    {
            //        config.SignIn.RequireConfirmedEmail = true;


            //    }).AddEntityFrameworkStores<MusicServiceContext>()
            //    .AddDefaultTokenProviders();

            //services.Configure<RegistrationConfig>(Configuration.GetSection("RegistrationConfig"));
            //DependencyInstaller.RegisterDependencies(services);
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.AddTransient<IBlogPostRepository, BlogPostRepository>();
            services.AddTransient<IBlogUserRepository, BlogUserRepository>();
            services.AddTransient<IAuthRepository,AuthRepository>();
            services.AddTransient<ITransactionManager, SqlTransactionManager>();
            //services.AddTransient<SqlConnection, SqlConnectionFactory.CreateConnection();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
