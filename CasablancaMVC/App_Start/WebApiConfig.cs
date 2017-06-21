using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using CasablancaMVC.Filters;

namespace CasablancaMVC.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //向过滤器列表中添加新的过滤器
            config.Filters.Add(new ValidationActionFilterAttrribute());
            config.Filters.Add(new OnApiExceptionAttribute());
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name:"DefaultApi",
                routeTemplate:"api/{controller}/{id}",
                defaults:new { id=RouteParameter.Optional}
                );
        }
    }
}
