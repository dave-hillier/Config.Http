using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Config.Http
{
  class HttpConfigurationProvider : ConfigurationProvider
  {
    HttpConfigurationSource Source { get; }
    public HttpConfigurationProvider(HttpConfigurationSource source)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }
      Source = source;
    }

    private string ConfigUrl => Source.ConfigUrl;
    private HttpClient Client => Source.Client;

    public override void Load() => LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();

    private async Task LoadAsync()
    {
      var response = await Client.GetAsync(ConfigUrl);

      var stream = await response.Content.ReadAsStreamAsync();

      var reader = new StreamReader(stream);
      string s = reader.ReadToEnd();
      var ms = new MemoryStream(Encoding.UTF8.GetBytes(s));

      Data = JsonConfigurationFileParser.Parse(stream);
    }
  }
}
