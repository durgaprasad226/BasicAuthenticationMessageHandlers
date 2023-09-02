using BasicAuthenticationMessageHandlers.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuthenticationMessageHandlers.Models
{
    public class ValidateUser
    {
        public UserMaster CheckUserCredentials(string username, string password)
        {
            using(var context = new OnlineMartContext())
            {
                var user=context.userMasters.FirstOrDefault(u=>u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                && u.UserPassword==password);
                return user;
            }
        }
    }
}