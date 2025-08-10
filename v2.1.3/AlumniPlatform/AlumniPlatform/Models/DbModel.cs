using AlumniPlatform.Models.user;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AlumniPlatform.Models
{
    public class AlumniNetworkingPlatformContext : IdentityDbContext<ApplicationUser>
    {
        // 原有DbSets
        public DbSet<Student> Students { get; set; }
        public DbSet<Alumni> Alumni { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Universitycampus> Universitycampuses { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventAttendance> EventAttendances { get; set; }
        public DbSet<Mentorship> Mentorships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置TPH继承映射
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Alumni>().ToTable("Alumni");
            modelBuilder.Entity<Admin>().ToTable("Admins");

            // 配置关系
            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(u => u.Universitycampus)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.UniversitycampusID);

            // 配置EventAttendance的复合主键
            modelBuilder.Entity<EventAttendance>()
                .HasKey(ea => new { ea.EventId, ea.UserId });

            // 配置EventAttendance与Event的关系
            modelBuilder.Entity<EventAttendance>()
                .HasRequired(ea => ea.Event)
                .WithMany(e => e.Attendances)
                .HasForeignKey(ea => ea.EventId);

            // 配置EventAttendance与ApplicationUser的关系
            modelBuilder.Entity<EventAttendance>()
                .HasRequired(ea => ea.User)
                .WithMany()
                .HasForeignKey(ea => ea.UserId);
        }
    }
}