using System.Data.Entity.Migrations;
using AlumniNetworkingPlatform.Models;

namespace AlumniNetworkingPlatform.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AlumniNetworkingPlatformContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AlumniNetworkingPlatformContext context)
        {
            // 可以在这里添加初始数据
        }
    }
}