using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using MsGraphMdArafat.TokenStorage;

namespace MsGraphMdArafat.Helpers
{
  public class GraphHelper
  {
    private static string appId = ConfigurationManager.AppSettings["ida:AppId"];
    private static string appSecret = ConfigurationManager.AppSettings["ida:AppSecret"];
    private static string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];
    private static string graphScopes = ConfigurationManager.AppSettings["ida:AppScopes"];

    public static async Task<User> GetUserDetailsAsync(string accessToken)
    {
      var graphClient = new GraphServiceClient(
        new DelegateAuthenticationProvider(
          async (requestMessage) =>
          {
            requestMessage.Headers.Authorization =
              new AuthenticationHeaderValue("Bearer", accessToken);
          }));

      return await graphClient.Me.Request().GetAsync();
    }


    internal static GraphServiceClient GetAuthenticatedClient()
    {
      return new GraphServiceClient(
        new DelegateAuthenticationProvider(
          async (requestMessage) =>
          {
            var idClient = ConfidentialClientApplicationBuilder.Create(appId)
              .WithRedirectUri(redirectUri)
              .WithClientSecret(appSecret)
              .Build();

            var tokenStore = new SessionTokenStorage.SessionTokenStore(idClient.UserTokenCache,
              HttpContext.Current, ClaimsPrincipal.Current);

            var accounts = await idClient.GetAccountsAsync();

            var scopes = graphScopes.Split(' ');
            var result = await idClient.AcquireTokenSilent(scopes, accounts.FirstOrDefault())
              .ExecuteAsync();

            requestMessage.Headers.Authorization =
              new AuthenticationHeaderValue("Bearer", result.AccessToken);
          }));
    }
  }
}
