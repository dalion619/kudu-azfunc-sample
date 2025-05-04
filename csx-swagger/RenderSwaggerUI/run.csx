#r "Microsoft.Azure.WebJobs.Extensions.Http" 
#load "..\Shared\IOpenApiHttpTriggerContext.csx"
using Aliencube.AzureFunctions.Extensions.OpenApi;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Extensions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Configurations;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Visitors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models; 


        private readonly static  OpenApiHttpTriggerContext context = new OpenApiHttpTriggerContext();

    /// <summary>
        /// Invokes the HTTP trigger endpoint to render Swagger UI in HTML.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Swagger UI in HTML.</returns>
        [FunctionName(nameof(Run))]
        [OpenApiOperation("list", "sample")] 
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"SwaggerUI page was requested.");

            var result = await context.SwaggerUI
                                      .AddMetadata(context.OpenApiInfo)
                                      .AddServer(req, context.HttpSettings.RoutePrefix)
                                      .BuildAsync()
                                      .RenderAsync("swagger.json", context.GetSwaggerAuthKey())
                                      .ConfigureAwait(false);

            var content = new ContentResult()
            {
                Content = result,
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK
            };

            return content;
        }