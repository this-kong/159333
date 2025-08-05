using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Web.Helpers;
using XUETISHUJUKU.Data;
using XUETISHUJUKU.Models;
using XUETISHUJUKU.Models.User;

namespace XUETISHUJUKU.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<XUETISHUJUKUContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(XUETISHUJUKUContext context)
        {
            // 1. 添加初始校区
            var campus1 = new Universitycampus { Name = "主校区", IDNumber = "CAMP001" };
            var campus2 = new Universitycampus { Name = "东校区", IDNumber = "CAMP002" };
            var campus3 = new Universitycampus { Name = "西校区", IDNumber = "CAMP003" };

            context.Universitycampuses.AddOrUpdate(
                c => c.Name,
                campus1,
                campus2,
                campus3
            );
            context.SaveChanges();

            // 2. 添加初始管理员
            var admin = new Admin
            {
                Name = "系统管理员",
                Email = "admin@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = "Admin",
                Department = "信息技术中心"
            };
            context.Admins.AddOrUpdate(a => a.Email, admin);

            // 3. 添加初始校友
            var alumni = new Alumni
            {
                Name = "张校友",
                Email = "alumni@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Alumni@123"),
                Role = "Alumni",
                GraduationYear = 2015,
                Degree = "计算机科学学士",
                CurrentCompany = "微软",
                CurrentPosition = "高级工程师",
                IsMentor = true,
                UniversitycampusID = campus1.ID
            };
            context.Alumni.AddOrUpdate(a => a.Email, alumni);

            // 4. 添加初始学生
            var student = new Student
            {
                Name = "李同学",
                Email = "student@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                Role = "Student",
                StudentID = "20230001",
                Major = "软件工程",
                UniversitycampusID = campus2.ID
            };
            context.Students.AddOrUpdate(s => s.Email, student);

            context.SaveChanges();
        }
    }
}