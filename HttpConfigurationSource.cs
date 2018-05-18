using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;


namespace Config.Http
{
  class HttpConfigurationSource : IConfigurationSource
  {
    public HttpClient Client { get; set; }
    public string ConfigUrl { get; set; }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
      return new HttpConfigurationProvider(this);
    }
  }
}
