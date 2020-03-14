using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using MsGraphMdArafat.TokenStorage;

namespace MsGraphMdArafat.Controllers
{
    public class AccountController : Controller
    {
      public void SignIn()
      {
        if (!Request.IsAuthenticated)
        {
          Request.GetOwinContext().Authentication.Challenge(
            new AuthenticationProperties { RedirectUri = "/" },
            OpenIdConnectAuthenticationDefaults.AuthenticationType);
        }
      }

      public ActionResult SignOut()
      {
        if (Request.IsAuthenticated)
        {
          var tokenStore = new SessionTokenStorage.SessionTokenStore(null,
            System.Web.HttpContext.Current, ClaimsPrincipal.Current);

          tokenStore.Clear();

          Request.GetOwinContext().Authentication.SignOut(
            CookieAuthenticationDefaults.AuthenticationType);
        }

        return RedirectToAction("Index", "Home");
      }

  }
}
