using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicAuthenticationMessageHandlers.Models
{
    public class UserMaster
    {
        //CF Approach for existing DB
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserRoles { get; set; }
        public string UserEmailID { get; set; }
    }
}