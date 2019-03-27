using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Extensions.Configuration;
using System.Data.Entity.Infrastructure.Interception;
using LMS.Infrastructure.Seedwork;
using Autofac.Core;

namespace MVC.Client
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //IOC
            // Add the configuration to the ConfigurationBuilder.
            var config = new ConfigurationBuilder();
            // config.AddJsonFile comes from Microsoft.Extensions.Configuration.Json
            // config.AddXmlFile comes from Microsoft.Extensions.Configuration.Xml
            config.AddXmlFile("Autofac/Seedwork.xml");
            config.AddXmlFile("Autofac/SystemMgr.xml");

            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);


            //builder.RegisterType<DbInterceptor>();

            // Register your MVC Controllers.
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            // Register your Api Controllers.
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterType<DbInterceptor>();
            //builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            //builder.RegisterType<DbInterceptor>();

            //3.Build the container and store it for later use.
            var container = builder.Build();

            //4.实现DI(DependencyResolver方式) 
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //AOP拦截，为了切换数据库
            DbInterception.Add(new DbInterceptor());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
