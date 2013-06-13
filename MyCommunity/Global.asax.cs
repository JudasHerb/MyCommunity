using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using Configuration = MyCommunity.Migrations.Configuration;

namespace MyCommunity
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static IWindsorContainer _container;

        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UnitOfWork, Configuration>());


            using (var unitOfWork = new UnitOfWork())
            {
                Community results = unitOfWork.Communities.FirstOrDefault();
            }

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            Windsor();
        }

        protected void Application_End()
        {
            _container.Dispose();
        }

        private void Windsor()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());
        }
    }
}