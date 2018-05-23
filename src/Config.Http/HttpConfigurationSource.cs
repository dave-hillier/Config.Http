using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;


namespace Config.Http
{
  /// <summary>
  /// JSON over HTTP as a Configuration Source
  /// </summary>
  public class HttpConfigurationSource : IConfigurationSource
  {
    public HttpClient Client { get; set; }
    public string ConfigUrl { get; set; }

    /// <summary>
    /// Builds a HttpConfigurationProvider
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
      return new HttpConfigurationProvider(this);
    }
  }
}
