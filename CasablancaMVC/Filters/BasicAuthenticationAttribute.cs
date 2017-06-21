using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Filters;
using System.Web.Mvc;
using System.Security.Principal;
using CasablancaMVC.Models;

namespace CasablancaMVC.Filters
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        /// <summary>
        /// 对请求进行身份验证。
        /// IAuthenticationFilter需要实现的方法
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //1.1获取当前Http的请求对象
            var request = filterContext.HttpContext.Request;
            //1.2检查身份验证上下文对象中包含的Http上下文对象中的请求对象中是否包含名为Authorization的请求头
            var authorization = request.Headers["Authorization"];
            //1.3未找到授权或不包含Basic，函数停止
            if (string.IsNullOrEmpty(authorization) || !authorization.Contains("Basic"))
                return;

            //2.1 从请求头中取出user pwd,并删除Basic这个词
            byte[] encodeDataAsBytes = Convert.FromBase64String(authorization.Replace("Basic", ""));
            //2.2 将字节数组转成字符串
            string value = Encoding.ASCII.GetString(encodeDataAsBytes);
            //2.3 获取用户名及密码
            string username = value.Substring(0, value.IndexOf(':'));
            string pwd = value.Substring(value.IndexOf(':') + 1);

            //3.1 判断用户名或密码是否为空
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pwd))
            {
                filterContext.Result = new HttpUnauthorizedResult("缺少用户名或密码");
                return;
            }
            var user = AuthenticatedUsers.Users.FirstOrDefault(u => u.Name == username && u.Password == pwd);
            //3.2 若未找到用户，也返回未经授权的结果
            if (user == null)
            {
                filterContext.Result= new HttpUnauthorizedResult("用户名或密码错误");
                return;
            }
            //3.3若找到用户，则使用匹配到的用户实例化一个新的GenericPrincipal对象
            filterContext.Principal = new GenericPrincipal(user, user.Roles);

        }

        /// <summary>
        /// 向当前 System.Web.Mvc.ActionResult 添加身份验证质询。
        /// IAuthenticationFilter需要实现的方法
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            filterContext.Result = new BasicChallengeActionResult
            {
                CurrentResult = filterContext.Result
            };
        }
    }
}
