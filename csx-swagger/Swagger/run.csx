#r "Microsoft.Azure.WebJobs.Extensions.Http" 
#load "..\Shared\SwaggerGen.csx"
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
 

[FunctionName("swagger.json")]
public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "GET")]   HttpRequest req, ILogger log )
{
var context = new OpenApiHttpTriggerContext();
return new ContentResult()
{
    Content = await SwaggerModel.SwaggerGen(req),
    ContentType = context.GetOpenApiFormat("json").GetContentType(),
    StatusCode = (int)HttpStatusCode.OK
}; 
}