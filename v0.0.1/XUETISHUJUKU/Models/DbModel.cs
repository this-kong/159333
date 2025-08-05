using System.Data.Entity;
using XUETISHUJUKU.Models;
using XUETISHUJUKU.Models.User;

namespace XUETISHUJUKU.Data
{
    public class XUETISHUJUKUContext : DbContext
    {
        public XUETISHUJUKUContext() : base("name=DefaultConnection")
        {
            // 确保数据库在模型更改时自动迁移
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<XUETISHUJUKUContext, Migrations.Configuration>());
        }

        // 用户相关
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Alumni> Alumni { get; set; }
        public DbSet<Admin> Admins { get; set; }

        // 校区
        public DbSet<Universitycampus> Universitycampuses { get; set; }

        // 其他模型
        public DbSet<Event> Events { get; set; }
        public DbSet<EventAttendance> EventAttendances { get; set; }
        public DbSet<Mentorship> Mentorships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 配置TPH继承映射
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Alumni>().ToTable("Alumni");
            modelBuilder.Entity<Admin>().ToTable("Admins");

            // 配置关系
            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(u => u.Universitycampus)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.UniversitycampusID);

            base.OnModelCreating(modelBuilder);
        }
    }
}