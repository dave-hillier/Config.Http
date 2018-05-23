using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Config.Http
{
  /// <summary>
  /// Extension methods for registering HttpConfigurationProvider
  /// </summary>
  public static class HttpConfigurationProviderExtensions
  {
    /// <summary>
    /// Extension methods for registering HttpConfigurationProvider
    /// </summary>
    public static IConfigurationBuilder AddHttpService(
      this IConfigurationBuilder configurationBuilder,
      string configUrl,
      string authority,
      string clientId,
      string secret,
      string resource)
    {
      var handler = new AuthorizationHandler()
      {
        Authority = authority,
        ClientId = clientId,
        ClientSecret = secret,
        Resource = resource
      };
      return AddHttpService(configurationBuilder, configUrl, HttpClientFactory.Create(handler));
    }
    public static IConfigurationBuilder AddHttpService(
      this IConfigurationBuilder configurationBuilder,
      string configUrl)
    {
      return AddHttpService(configurationBuilder, configUrl, HttpClientFactory.Create());
    }

    public static IConfigurationBuilder AddHttpService(
        this IConfigurationBuilder configurationBuilder,
        string configUrl,
        HttpClient client)
    {
      configurationBuilder.Add(new HttpConfigurationSource()
      {
        ConfigUrl = configUrl,
        Client = client
      });
      return configurationBuilder;
    }
  }
}
