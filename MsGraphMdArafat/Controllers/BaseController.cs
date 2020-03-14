using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Graph;
using MsGraphMdArafat.Models;
using Microsoft.Owin.Security.Cookies;
using MsGraphMdArafat.TokenStorage;
using Alert = MsGraphMdArafat.Models.Alert;

namespace MsGraphMdArafat.Controllers
{
    public class BaseController : Controller
    {

      protected override void OnActionExecuting(ActionExecutingContext filterContext)
      {
        if (Request.IsAuthenticated)
        {
          var tokenStore = new SessionTokenStorage.SessionTokenStore(null,
            System.Web.HttpContext.Current, ClaimsPrincipal.Current);

          if (tokenStore.HasData())
          {
            ViewBag.User = tokenStore.GetUserDetails();
          }
          else
          {
            // The session has lost data. This happens often // when debugging. Log out so the user can log back in
            Request.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            filterContext.Result = RedirectToAction("Index", "Home");
          }
        }
        base.OnActionExecuting(filterContext);
      }
      protected void Flash(string message, string debug = null)
      {
        var alerts = TempData.ContainsKey(Alert.AlertKey) ? (List<Alert>)TempData[Alert.AlertKey] : new List<Alert>();
        alerts.Add(new Alert
        {
          Message = message,
          Debug = debug
        });

        TempData[Alert.AlertKey] = alerts;
      }

  }
}