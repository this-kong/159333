using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using XUETISHUJUKU.Data;

namespace XUETISHUJUKU
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // ȷ�����ݿ��ʼ��
            System.Data.Entity.Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<XUETISHUJUKUContext, Migrations.Configuration>()
            );

        }
    }
}