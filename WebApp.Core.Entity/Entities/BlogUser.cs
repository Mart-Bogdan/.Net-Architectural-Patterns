using Microsoft.AspNetCore.Identity;

namespace WebApp.Core.Entity.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class BlogUser : IdentityUser, IBaseEntity<string>
    {
        public string FullName { get; set; }
        
        public override string ToString()
        {
            return string.Format("BlogUser(Id: {3},Name: {0}, Nick: {1}, UserPassword: {2})", FullName, UserName, PasswordHash, Id);
        }

    }
}