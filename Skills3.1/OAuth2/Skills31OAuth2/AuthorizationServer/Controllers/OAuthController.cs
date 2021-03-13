using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace AuthorizeWebApplication.Controllers
{
    public class OAuthController : Controller
    {
        // GET: OAuth
        public ActionResult Authorize()
        {
            if (Response.StatusCode != 200)
            {
                return View("AuthorizeError");
            }

            var authentication = HttpContext.GetOwinContext().Authentication;
            var ticket = authentication.AuthenticateAsync("ApplicationCookie").Result;
            var identity = ticket?.Identity;
            if (identity == null)
            {
                authentication.Challenge("ApplicationCookie");
                return new HttpUnauthorizedResult();
            }

            var scopes = (Request.QueryString.Get("scope") ?? "").Split((' '));
            if (Request.HttpMethod == "POST")
            {
                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Grant")))
                {
                    identity = new ClaimsIdentity(identity.Claims, "Bearer", identity.NameClaimType,
                        identity.RoleClaimType);
                    foreach (var scope in scopes)
                    {
                        identity.AddClaim(new Claim("urn:oauth:scope", scope));
                    }

                    authentication.SignIn(identity);
                }

                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Login")))
                {
                    authentication.SignOut("ApplicationCookie");
                    authentication.Challenge("ApplicationCookie");
                    return new HttpUnauthorizedResult();
                }
            }

            return View();
        }
    }
}