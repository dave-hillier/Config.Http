using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Config.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();

                    var section = builtConfig.GetSection("ConnectionStringService");
                    if (section.GetChildren().Any())
                    {
                        config.AddHttpService(section["Url"],
                            section["Auth:Authority"],
                            section["Auth:ClientId"],
                            section["Auth:ClientSecret"],
                            section["Auth:Resource"]);
                    }
                })
                .UseStartup<Startup>();
    }
}
