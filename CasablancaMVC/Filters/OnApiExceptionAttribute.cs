using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace CasablancaMVC.Filters
{
    public class OnApiExceptionAttribute:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //获取错误类型的名称
            var exceptionType = actionExecutedContext.Exception.GetType().Name; 
            
            base.OnException(actionExecutedContext);
        }
    }
}
