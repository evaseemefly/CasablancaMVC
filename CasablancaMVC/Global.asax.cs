using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.Data.Entity;
using CasablancaMVC.Models;
using CasablancaMVC.DAL;
using CasablancaMVC.App_Start;

namespace CasablancaMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var bookContext = new BookContext(); 
            //为DbContext首次访问数据库设定初始值
            Database.SetInitializer(new BookInitializer());
            bookContext.Database.Initialize(true);
        }
    }
}
