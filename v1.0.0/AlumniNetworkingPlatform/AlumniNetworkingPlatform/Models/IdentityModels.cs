using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AlumniNetworkingPlatform.Models
{
    public class ApplicationUser : IdentityUser
    {
        // 添加自定义属性
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
        public string ProfilePictureUrl { get; set; }
        public int? UniversitycampusID { get; set; }
        public virtual Universitycampus Universitycampus { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}