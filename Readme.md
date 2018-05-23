# Config.Http


[![Travis](https://travis-ci.org/dave-hillier/Config.Http.svg?branch=master)](https://travis-ci.org/dave-hillier/Config.Http)


Fetch configuration from a web service providing JSON.

Can be configured to use Azure Active Directory/OAuth2 Client credentials to obtain an Access Token for service authorization.

Example registration:

```
    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
            {
              var builtConfig = config.Build();

              var section = builtConfig.GetSection("MyServiceConfig");
              if (section.GetChildren().Any())
              {
                config.AddHttpService(section["Url"],
                  section["Auth:Authority"],
                  section["Auth:ClientId"],
                  section["Auth:ClientSecret"],
                  section["Auth:Resource"]);
              }
            })
            .UseStartup<Startup>()
            .Build();
  }
```
