using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MyOpenApiFunctionApp;

[assembly: FunctionsStartup(typeof(Startup))]

namespace MyOpenApiFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            {
                builder.Services
                       .AddSingleton<IOpenApiConfigurationOptions>(_ =>
                       {
                           var options = new OpenApiConfigurationOptions
                                         {
                                             Info = new OpenApiInfo
                                                    {
                                                        Version = DefaultOpenApiConfigurationOptions.GetOpenApiDocVersion(),
                                                        Title = $"{DefaultOpenApiConfigurationOptions.GetOpenApiDocTitle()} (Injected)",
                                                        Description = DefaultOpenApiConfigurationOptions.GetOpenApiDocDescription(),
                                                        TermsOfService =
                                                            new Uri("https://github.com/Azure/azure-functions-openapi-extension"),
                                                        Contact = new OpenApiContact
                                                                  {
                                                                      Name = "Enquiry",
                                                                      Email = "azfunc-openapi@microsoft.com",
                                                                      Url = new Uri(
                                                                          "https://github.com/Azure/azure-functions-openapi-extension/issues")
                                                                  },
                                                        License = new OpenApiLicense
                                                                  {
                                                                      Name = "MIT",
                                                                      Url = new Uri("http://opensource.org/licenses/MIT")
                                                                  }
                                                    },
                                             Servers = DefaultOpenApiConfigurationOptions.GetHostNames(),
                                             OpenApiVersion = DefaultOpenApiConfigurationOptions.GetOpenApiVersion(),
                                             //IncludeRequestingHostName = true,
                                             ForceHttps = DefaultOpenApiConfigurationOptions.IsHttpsForced(),
                                             ForceHttp = DefaultOpenApiConfigurationOptions.IsHttpForced()
                                         };

                           return options;
                       });
            }
        }
    }
}