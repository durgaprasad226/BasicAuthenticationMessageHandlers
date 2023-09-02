using BasicAuthenticationMessageHandlers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BasicAuthenticationMessageHandlers
{
    public class BasicAuthenticationMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var authenticationToken = request.Headers.GetValues("Authorization").FirstOrDefault();
                if (authenticationToken != null)
                {
                    // Convert the Base64-encoded string to a byte array
                    byte[] data = Convert.FromBase64String(authenticationToken);
                    // Convert the byte array back to a string using UTF-8 encoding
                    string decodedAuthenticationToken = Encoding.UTF8.GetString(data);
                    // Split the string into an array using ':'
                    string[] UsernamePasswordArray = decodedAuthenticationToken.Split(':');
                    // UsernamePasswordArray[0] will be "username"
                    // UsernamePasswordArray[1] will be "password"
                    string username = UsernamePasswordArray[0];
                    string password = UsernamePasswordArray[1];
                    // Now, 'obj' will contain information about the user if the credentials are valid,
                    // or it might be null or contain an error message if the credentials are invalid.
                    UserMaster obj = new ValidateUser().CheckUserCredentials(username, password);
                    if (obj != null)
                    {
                        var identity = new GenericIdentity(obj.UserName);
                        identity.AddClaim(new Claim("Email", obj.UserEmailID));
                        IPrincipal principal = new GenericPrincipal(identity, obj.UserRoles.Split(','));
                        Thread.CurrentPrincipal = principal;
                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.User = principal;
                        }
                        return base.SendAsync(request, cancellationToken);
                    }
                    else
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        var tsc = new TaskCompletionSource<HttpResponseMessage>();
                        tsc.SetResult(response);
                        return tsc.Task;
                    }
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }
            }
            catch
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
        }
    }
}