#r "Microsoft.Azure.WebJobs.Extensions.Http" 
#load "..\Shared\IOpenApiHttpTriggerContext.csx" 
#load "..\Shared\SwaggerLoad.csx" 
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

public static class SwaggerModel
{
    private static string V2 = "v2";
    private static string V3 = "v3";
    private static string JSON = "json";
    private static string YAML = "yaml";
    private static Document _document = null;
    private static string content = "";



    public static async Task<string> SwaggerGen(HttpRequest req)
    {
        string extension = "json";
        var filter = new RouteConstraintFilter();
        var acceptor = new OpenApiSchemaAcceptor();
        var helper = new DocumentHelper(filter, acceptor);

        if (_document == null)
        {

            var context = new OpenApiHttpTriggerContext();
            _document = new Document(helper);
            content = await _document.InitialiseDocument()
            .AddMetadata(context.OpenApiInfo)
            .AddServer(req, context.HttpSettings.RoutePrefix)
            .AddNamingStrategy(context.NamingStrategy)
            .AddVisitors(context.GetVisitorCollection())
            .Build(Assembly.GetExecutingAssembly())
            .RenderAsync(context.GetOpenApiSpecVersion(V3), context.GetOpenApiFormat(extension))
            .ConfigureAwait(false);
        }
        return content;
    }
}