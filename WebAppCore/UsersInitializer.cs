using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Core.DAL.EfMsSql;
using WebApp.Core.Entity.Entities;

namespace WebAppCore
{
    public class UsersInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var scopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
            using (var serviceScope = scopeFactory.CreateScope())
            {
                var scopedProvider = serviceScope.ServiceProvider;
                var context = scopedProvider.GetService<BlogDbContext>();
                
                

                var roles = new List<string>() { "Blogger", "Administrator", "Subscriber" };

                foreach (string role in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(context);

                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        roleStore.CreateAsync(new IdentityRole(role){NormalizedName = role.ToUpper()}).Wait();
                    }
                }

                context.SaveChanges();
                
                {
                    var user = new BlogUser
                    {
                        FullName = "Admin",
                        Email = "admin@example.com",
                        NormalizedEmail = "admin@example.com",
                        UserName = "admin@example.com",
                        NormalizedUserName = "ADMIN",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    };

                    CreateUserIfNotPresent(scopedProvider, context, user, "qwerty", roles.ToArray());
                }

                {
                    var user = new BlogUser
                    {
                        FullName = "Blogger",
                        Email = "blogger@example.com",
                        NormalizedEmail = "blogger@example.com",
                        UserName = "blogger@example.com",
                        NormalizedUserName = "BLOGGER",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    };

                    roles.Remove("Administrator");
                    CreateUserIfNotPresent(scopedProvider, context, user, "qwerty", roles.ToArray());
                }
                context.SaveChanges();
            }

        }

        private static void CreateUserIfNotPresent(IServiceProvider serviceProvider, BlogDbContext context, BlogUser user,
            string passwordStr, string[] roles)
        {
            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var passwordHasher = new PasswordHasher<BlogUser>();
                var hashed = passwordHasher.HashPassword(user, passwordStr);
                user.PasswordHash = hashed;

                var userStore = new UserStore<BlogUser>(context);
                userStore.CreateAsync(user).Wait();
            }

            AssignRoles(serviceProvider, user.Email, roles).Wait();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<BlogUser> _userManager = services.GetService<UserManager<BlogUser>>();
            BlogUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }

    }
}