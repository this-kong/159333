using System.Data.Entity.Migrations;

namespace XUETISHUJUKU.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Universitycampuses",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    IDNumber = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Email = c.String(nullable: false, maxLength: 100),
                    PasswordHash = c.String(nullable: false),
                    Role = c.String(nullable: false),
                    Phone = c.String(),
                    Location = c.String(),
                    Bio = c.String(),
                    ProfilePictureUrl = c.String(),
                    UniversitycampusID = c.Int(),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Universitycampuses", t => t.UniversitycampusID)
                .Index(t => t.UniversitycampusID);

            CreateTable(
                "dbo.Admins",
                c => new
                {
                    ID = c.Int(nullable: false),
                    Department = c.String(nullable: false),
                    CanManageUsers = c.Boolean(nullable: false),
                    CanManageContent = c.Boolean(nullable: false),
                    CanManageEvents = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApplicationUsers", t => t.ID)
                .Index(t => t.ID);

            CreateTable(
                "dbo.Alumni",
                c => new
                {
                    ID = c.Int(nullable: false),
                    GraduationYear = c.Int(nullable: false),
                    Degree = c.String(),
                    CurrentCompany = c.String(),
                    CurrentPosition = c.String(),
                    Industry = c.String(),
                    Skills = c.String(),
                    IsMentor = c.Boolean(nullable: false),
                    CareerHistory = c.String(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApplicationUsers", t => t.ID)
                .Index(t => t.ID);

            CreateTable(
                "dbo.Students",
                c => new
                {
                    ID = c.Int(nullable: false),
                    StudentID = c.String(nullable: false, maxLength: 20),
                    Major = c.String(),
                    GraduationYear = c.Int(),
                    AcademicInterests = c.String(),
                    CareerGoals = c.String(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApplicationUsers", t => t.ID)
                .Index(t => t.ID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Students", "ID", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Alumni", "ID", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Admins", "ID", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUsers", "UniversitycampusID", "dbo.Universitycampuses");
            DropIndex("dbo.Students", new[] { "ID" });
            DropIndex("dbo.Alumni", new[] { "ID" });
            DropIndex("dbo.Admins", new[] { "ID" });
            DropIndex("dbo.ApplicationUsers", new[] { "UniversitycampusID" });
            DropTable("dbo.Students");
            DropTable("dbo.Alumni");
            DropTable("dbo.Admins");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.Universitycampuses");
        }
    }
}