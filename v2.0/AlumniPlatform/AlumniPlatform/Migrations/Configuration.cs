namespace AlumniPlatform.Migrations
{
    using AlumniPlatform.Models;
    using AlumniPlatform.Models.user;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AlumniPlatform.Models.AlumniNetworkingPlatformContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AlumniPlatform.Models.AlumniNetworkingPlatformContext";
        }

        protected override void Seed(AlumniPlatform.Models.AlumniNetworkingPlatformContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var campuses = new List<Universitycampus>
        {
            new Universitycampus { Name = "主校区", IDNumber = "CAMP-001" },
            new Universitycampus { Name = "东校区", IDNumber = "CAMP-002" },
            new Universitycampus { Name = "西校区", IDNumber = "CAMP-003" },
            new Universitycampus { Name = "国际校区", IDNumber = "CAMP-004" }
        };

            context.Universitycampuses.AddRange(campuses);
            context.SaveChanges();
            // 获取校园
            //var mainCampus = context.Universitycampuses.First(c => c.Name == "主校区");
            //var eastCampus = context.Universitycampuses.First(c => c.Name == "东校区");

            // 创建管理员
            if (!context.Admins.Any())
            {
                var admins = new List<Admin>
        {
            new Admin {
                UserName = "admin1@school.edu",
                Email = "admin1@school.edu",
                Name = "张管理员",
                Department = "就业指导中心",
                CanManageUsers = true,
                CanManageContent = true,
                CanManageEvents = true,
                Universitycampus = campuses[1]
            },
            new Admin {
                UserName = "admin2@school.edu",
                Email = "admin2@school.edu",
                Name = "李管理员",
                Department = "校友联络处",
                CanManageContent = true,
                CanManageEvents = true,
                Universitycampus = campuses[3]
            }
        };

                context.Admins.AddRange(admins);
                context.SaveChanges();
            }

            // 创建学生
            if (!context.Students.Any())
            {
                var students = new List<Student>
        {
            new Student {
                UserName = "student1@school.edu",
                Email = "student1@school.edu",
                Name = "王小明",
                StudentID = "20230001",
                Major = "计算机科学",
                GraduationYear = 2024,
                AcademicInterests = "人工智能, 机器学习",
                CareerGoals = "成为AI工程师",
                Universitycampus = campuses[2]
            },
            new Student {
                UserName = "student2@school.edu",
                Email = "student2@school.edu",
                Name = "李小红",
                StudentID = "20230002",
                Major = "工商管理",
                GraduationYear = 2025,
                AcademicInterests = "市场营销, 品牌管理",
                CareerGoals = "成为市场总监",
                Universitycampus = campuses[0]
            },
            new Student {
                UserName = "student3@school.edu",
                Email = "student3@school.edu",
                Name = "赵小刚",
                StudentID = "20230003",
                Major = "电子工程",
                GraduationYear = 2023,
                AcademicInterests = "嵌入式系统, 物联网",
                CareerGoals = "成为硬件工程师",
                Universitycampus = campuses[1]
            }
        };

                context.Students.AddRange(students);
                context.SaveChanges();
            }

            // 创建校友（使用你之前的校友数据）
            if (!context.Alumni.Any())
            {
                var alumniList = new List<Alumni>
        {
            new Alumni {
                UserName = "alumni1@school.edu",
                Email = "alumni1@school.edu",
                Name = "张三",
                GraduationYear = 2020,
                Degree = "计算机科学学士",
                CurrentCompany = "微软",
                CurrentPosition = "软件工程师",
                Industry = "科技",
                Skills = "C#, ASP.NET, SQL",
                IsMentor = true,
                CareerHistory = "2020-至今：微软软件工程师",
                Universitycampus = campuses[0]
            },
            new Alumni {
                UserName = "alumni2@school.edu",
                Email = "alumni2@school.edu",
                Name = "李四",
                GraduationYear = 2019,
                Degree = "电子工程硕士",
                CurrentCompany = "谷歌",
                CurrentPosition = "高级工程师",
                Industry = "科技",
                Skills = "Java, Python, 机器学习",
                IsMentor = false,
                CareerHistory = "2019-至今：谷歌高级工程师",
                Universitycampus = campuses[1]
            },
            new Alumni {
                UserName = "alumni3@school.edu",
                Email = "alumni3@school.edu",
                Name = "王五",
                GraduationYear = 2018,
                Degree = "工商管理硕士",
                CurrentCompany = "阿里巴巴",
                CurrentPosition = "产品经理",
                Industry = "电商",
                Skills = "项目管理, 市场分析",
                IsMentor = true,
                CareerHistory = "2018-至今：阿里巴巴产品经理",
                Universitycampus = campuses[2]
            }
        };

                context.Alumni.AddRange(alumniList);
                context.SaveChanges();
            }

            if (!context.Events.Any())
            {
                var alumni = context.Alumni.First();
                var admin = context.Admins.First();

                var events = new List<Event>
        {
            new Event {
                Title = "校友职业分享会",
                EventDate = DateTime.Now.AddDays(14),
                Location = "学校大礼堂",
                Description = "邀请成功校友分享职业发展经验",
            },
            new Event {
                Title = "技术研讨会：人工智能前沿",
                EventDate = DateTime.Now.AddDays(21),
                Location = "计算机学院报告厅",
                Description = "探讨人工智能最新发展与应用",
            },
            new Event {
                Title = "校友联谊晚宴",
                EventDate = DateTime.Now.AddMonths(1),
                Location = "学校宴会厅",
                Description = "年度校友聚会，增进校友情谊",
            }
        };

                context.Events.AddRange(events);
                context.SaveChanges();

                // 添加活动参与记录
                var students = context.Students.ToList();
                foreach (var ev in events)
                {
                    foreach (var student in students.Take(2)) // 每个活动前2个学生参加
                    {
                        context.EventAttendances.Add(new EventAttendance
                        {
                            Event = ev,
                            User = student,
                            RegistrationTime = DateTime.Now
                        });
                    }

                    // 活动创建者也参加
                    context.EventAttendances.Add(new EventAttendance
                    {
                        Event = ev,
                        User = alumni,
                        RegistrationTime = DateTime.Now
                    });
                }
                context.SaveChanges();
            }
            if (!context.Mentorships.Any())
            {
                var mentorAlumni = context.Alumni.Where(a => a.IsMentor).ToList();
                var students = context.Students.ToList();

                var mentorships = new List<Mentorship>
        {
            new Mentorship {
                Alumni = mentorAlumni[0], // 张三
                Student = students[0],     // 王小明
                StartDate = DateTime.Now.AddMonths(-1),
                Status = "Active"
            },
            new Mentorship {
                Alumni = mentorAlumni[1], // 王五
                Student = students[1],     // 李小红
                StartDate = DateTime.Now.AddMonths(-2),
                EndDate = DateTime.Now.AddMonths(1),
                Status = "Active"
            },
            new Mentorship {
                Alumni = mentorAlumni[0], // 张三
                Student = students[2],     // 赵小刚
                StartDate = DateTime.Now.AddDays(-10),
                Status = "Pending"
            }
        };

                context.Mentorships.AddRange(mentorships);
                context.SaveChanges();
            }

        }

        public static void Initialize(AlumniNetworkingPlatformContext context)
        {

            // 1. 先创建校园
            SeedCampuses(context);

            // 2. 创建用户（管理员、学生、校友）
            SeedUsers(context);

            // 3. 创建活动
            SeedEvents(context);

            // 4. 创建导师关系
            SeedMentorships(context);
        }

        private static void SeedCampuses(AlumniNetworkingPlatformContext context)
        {
            if (!context.Universitycampuses.Any())
            {
                var campuses = new List<Universitycampus>
        {
            new Universitycampus { Name = "主校区", IDNumber = "CAMP-001" },
            new Universitycampus { Name = "东校区", IDNumber = "CAMP-002" },
            new Universitycampus { Name = "西校区", IDNumber = "CAMP-003" },
            new Universitycampus { Name = "国际校区", IDNumber = "CAMP-004" }
        };

                context.Universitycampuses.AddRange(campuses);
                context.SaveChanges();
            }
        }

        private static void SeedUsers(AlumniNetworkingPlatformContext context)
        {
            // 获取校园
            var mainCampus = context.Universitycampuses.First(c => c.Name == "主校区");
            var eastCampus = context.Universitycampuses.First(c => c.Name == "东校区");

            // 创建管理员
            if (!context.Admins.Any())
            {
                var admins = new List<Admin>
        {
            new Admin {
                UserName = "admin1@school.edu",
                Email = "admin1@school.edu",
                Name = "张管理员",
                Department = "就业指导中心",
                CanManageUsers = true,
                CanManageContent = true,
                CanManageEvents = true,
                Universitycampus = mainCampus
            },
            new Admin {
                UserName = "admin2@school.edu",
                Email = "admin2@school.edu",
                Name = "李管理员",
                Department = "校友联络处",
                CanManageContent = true,
                CanManageEvents = true,
                Universitycampus = eastCampus
            }
        };

                context.Admins.AddRange(admins);
                context.SaveChanges();
            }

            // 创建学生
            if (!context.Students.Any())
            {
                var students = new List<Student>
        {
            new Student {
                UserName = "student1@school.edu",
                Email = "student1@school.edu",
                Name = "王小明",
                StudentID = "20230001",
                Major = "计算机科学",
                GraduationYear = 2024,
                AcademicInterests = "人工智能, 机器学习",
                CareerGoals = "成为AI工程师",
                Universitycampus = mainCampus
            },
            new Student {
                UserName = "student2@school.edu",
                Email = "student2@school.edu",
                Name = "李小红",
                StudentID = "20230002",
                Major = "工商管理",
                GraduationYear = 2025,
                AcademicInterests = "市场营销, 品牌管理",
                CareerGoals = "成为市场总监",
                Universitycampus = eastCampus
            },
            new Student {
                UserName = "student3@school.edu",
                Email = "student3@school.edu",
                Name = "赵小刚",
                StudentID = "20230003",
                Major = "电子工程",
                GraduationYear = 2023,
                AcademicInterests = "嵌入式系统, 物联网",
                CareerGoals = "成为硬件工程师",
                Universitycampus = mainCampus
            }
        };

                context.Students.AddRange(students);
                context.SaveChanges();
            }

            // 创建校友（使用你之前的校友数据）
            if (!context.Alumni.Any())
            {
                var alumniList = new List<Alumni>
        {
            new Alumni {
                UserName = "alumni1@school.edu",
                Email = "alumni1@school.edu",
                Name = "张三",
                GraduationYear = 2020,
                Degree = "计算机科学学士",
                CurrentCompany = "微软",
                CurrentPosition = "软件工程师",
                Industry = "科技",
                Skills = "C#, ASP.NET, SQL",
                IsMentor = true,
                CareerHistory = "2020-至今：微软软件工程师",
                Universitycampus = mainCampus
            },
            new Alumni {
                UserName = "alumni2@school.edu",
                Email = "alumni2@school.edu",
                Name = "李四",
                GraduationYear = 2019,
                Degree = "电子工程硕士",
                CurrentCompany = "谷歌",
                CurrentPosition = "高级工程师",
                Industry = "科技",
                Skills = "Java, Python, 机器学习",
                IsMentor = false,
                CareerHistory = "2019-至今：谷歌高级工程师",
                Universitycampus = eastCampus
            },
            new Alumni {
                UserName = "alumni3@school.edu",
                Email = "alumni3@school.edu",
                Name = "王五",
                GraduationYear = 2018,
                Degree = "工商管理硕士",
                CurrentCompany = "阿里巴巴",
                CurrentPosition = "产品经理",
                Industry = "电商",
                Skills = "项目管理, 市场分析",
                IsMentor = true,
                CareerHistory = "2018-至今：阿里巴巴产品经理",
                Universitycampus = mainCampus
            }
        };

                context.Alumni.AddRange(alumniList);
                context.SaveChanges();
            }
        }

        private static void SeedEvents(AlumniNetworkingPlatformContext context)
        {
            if (!context.Events.Any())
            {
                var alumni = context.Alumni.First();
                var admin = context.Admins.First();

                var events = new List<Event>
        {
            new Event {
                Title = "校友职业分享会",
                EventDate = DateTime.Now.AddDays(14),
                Location = "学校大礼堂",
                Description = "邀请成功校友分享职业发展经验",
            },
            new Event {
                Title = "技术研讨会：人工智能前沿",
                EventDate = DateTime.Now.AddDays(21),
                Location = "计算机学院报告厅",
                Description = "探讨人工智能最新发展与应用",
            },
            new Event {
                Title = "校友联谊晚宴",
                EventDate = DateTime.Now.AddMonths(1),
                Location = "学校宴会厅",
                Description = "年度校友聚会，增进校友情谊",
            }
        };

                context.Events.AddRange(events);
                context.SaveChanges();

                // 添加活动参与记录
                var students = context.Students.ToList();
                foreach (var ev in events)
                {
                    foreach (var student in students.Take(2)) // 每个活动前2个学生参加
                    {
                        context.EventAttendances.Add(new EventAttendance
                        {
                            Event = ev,
                            User = student,
                            RegistrationTime = DateTime.Now
                        });
                    }

                    // 活动创建者也参加
                    context.EventAttendances.Add(new EventAttendance
                    {
                        Event = ev,
                        RegistrationTime = DateTime.Now
                    });
                }
                context.SaveChanges();
            }
        }

        private static void SeedMentorships(AlumniNetworkingPlatformContext context)
        {
            if (!context.Mentorships.Any())
            {
                var mentorAlumni = context.Alumni.Where(a => a.IsMentor).ToList();
                var students = context.Students.ToList();

                var mentorships = new List<Mentorship>
        {
            new Mentorship {
                Alumni = mentorAlumni[0], // 张三
                Student = students[0],     // 王小明
                StartDate = DateTime.Now.AddMonths(-1),
                Status = "Active"
            },
            new Mentorship {
                Alumni = mentorAlumni[2], // 王五
                Student = students[1],     // 李小红
                StartDate = DateTime.Now.AddMonths(-2),
                EndDate = DateTime.Now.AddMonths(1),
                Status = "Active"
            },
            new Mentorship {
                Alumni = mentorAlumni[0], // 张三
                Student = students[2],     // 赵小刚
                StartDate = DateTime.Now.AddDays(-10),
                Status = "Pending"
            }
        };

                context.Mentorships.AddRange(mentorships);
                context.SaveChanges();
            }

        }

    
    }
}
