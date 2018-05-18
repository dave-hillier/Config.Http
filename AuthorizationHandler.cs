using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Config.Http
{
  public class AuthorizationHandler : DelegatingHandler
  {
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Authority { get; set; }

    public string Resource { get; set; }

    private string AccessToken = null;
    private DateTimeOffset ExpiresOn;

    private async Task<string> GetAuthToken()
    {
      if (AccessToken != null && (ExpiresOn - TimeSpan.FromSeconds(1)) > DateTime.UtcNow) // TODO: configure timespan before expiry
        return AccessToken;

      var cc = new ClientCredential(ClientId, ClientSecret);
      var ctx = new AuthenticationContext(Authority);
      var tk = await ctx.AcquireTokenAsync(ClientId, cc);

      AccessToken = tk.AccessToken;
      ExpiresOn = tk.ExpiresOn;
      return tk.AccessToken;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var token = await GetAuthToken();
      request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
      return await base.SendAsync(request, cancellationToken);
    }
  }

}
