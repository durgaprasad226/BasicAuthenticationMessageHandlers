using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace BasicAuthenticationMessageHandlers.Controllers
{
    public class UserController : ApiController
    {
        [Authorize(Roles="Admin, User")]
        public HttpResponseMessage Get()
        {
            //We can implement our own logic
            //Get the Identity Name
            string username = Thread.CurrentPrincipal.Identity.Name;
            return Request.CreateResponse(HttpStatusCode.OK, "UserName="+username);
        }
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage Post()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            return Request.CreateResponse(HttpStatusCode.OK, "UserName=" + username);
        }
    }
}
