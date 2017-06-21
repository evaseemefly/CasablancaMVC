using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasablancaMVC.Models
{
    /// <summary>
    /// 验证通过的用户列表
    /// 本列表用于匹配查询用户名和密码组合
    /// </summary>
    public class AuthenticatedUsers
    {
        private static List<User> _users = new List<User>
        {
            new User("james","123",null,new List<string> {"::1" })
        };

        public static List<User> Users { get { return _users; } }
    }
}
