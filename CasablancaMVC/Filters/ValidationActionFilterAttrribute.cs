using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CasablancaMVC.Filters
{
    /// <summary>
    /// 对action进行验证（requeiered等操作）
    /// </summary>
    public class ValidationActionFilterAttrribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //获取上下文中的模型字典
            var modelState = actionContext.ModelState;
            //判断模型状态字典是否有效
            if (!modelState.IsValid)
                //若无效返回400，并将状态信息一并返回
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, modelState);
            base.OnActionExecuting(actionContext);
        }
    }
}