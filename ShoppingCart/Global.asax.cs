using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using ShoppingCart.DAL;
using System.Data.Entity;

namespace ShoppingCart
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var dbContext = new ShoppingCartContext();
            //设置初始化对象
            Database.SetInitializer(new DataInitialization());

            dbContext.Database.Initialize(true);
        }

        protected void Session_Start(object sender,EventArgs e)
        {
            HttpContext.Current.Session.Add("_MyAppSession", string.Empty);
        }
    }
}
