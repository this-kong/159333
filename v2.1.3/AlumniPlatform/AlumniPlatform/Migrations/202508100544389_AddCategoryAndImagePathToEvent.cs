namespace AlumniPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryAndImagePathToEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Category", c => c.String());
            AddColumn("dbo.Events", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "ImagePath");
            DropColumn("dbo.Events", "Category");
        }
    }
}
