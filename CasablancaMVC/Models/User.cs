using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace CasablancaMVC.Models
{
    public class User : IIdentity
    {
        public User(string username,string pwd,string[] roles,List<string> validIpAddresses)
        {
            this.Name = username;
            this.Password = pwd;
            this.Roles = roles;
            this.ValidIpAddresses = validIpAddresses;
        }

        public string Name { get; private set; }
        public string Password { get; private set; }
        public string[] Roles { get; private set; }
        public List<string> ValidIpAddresses { get; private set; }

        public bool IsAuthenticated { get { return true; } }

        public string AuthenticationType { get { return "Basic"; } }       

        
     
    }
}
