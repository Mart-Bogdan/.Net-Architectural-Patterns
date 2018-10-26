using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Dependency;
using WebApp.Core.DAL.Abstract;
using WebApp.Core.DAL.EfMsSql;
using WebApp.Core.Entity.Entities;
using WebAppCore.Services;

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
           
            
            
            var connStr = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
         
            services.AddDbContext<BlogDbContext>(options =>
                //options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
                options.UseSqlServer(connStr)//, b=> b.MigrationsAssembly(typeof(DbContext).Assembly.FullName))
            );

            services.AddBlogRepositories();
            
            services.AddTransient<IEmailSender, EmailSender>();
            
            services.AddIdentity<BlogUser, IdentityRole>(config =>
                   {
                //        config.SignIn.RequireConfirmedEmail = true;
                       config.ClaimsIdentity.UserIdClaimType =
                           "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

                   }
                )
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(c=>
                {
                    c.Cookie.Name = "ActsMvcBlog";
                    
                });

//            services.AddScoped<ITransactionManager, SqlTransactionManager>();
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

            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            UsersInitializer.Initialize(app.ApplicationServices);
        }
    }
}
