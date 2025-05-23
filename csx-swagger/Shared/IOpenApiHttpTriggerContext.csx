﻿#r "..\bin\Aliencube.AzureFunctions.Extensions.OpenApi.dll"
#r "..\bin\Aliencube.AzureFunctions.Extensions.OpenApi.Core.dll"
#r "..\bin\Microsoft.OpenApi.dll"
#r "Microsoft.Azure.WebJobs.Extensions.Http" 
#r "Newtonsoft.Json"
#r "Microsoft.Extensions.Configuration.Abstractions"
using System.Reflection;
using System;

using Aliencube.AzureFunctions.Extensions.OpenApi.Core;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Configurations;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Enums;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Extensions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Resolvers;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Visitors;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

public class OpenApiHttpTriggerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenApiHttpTrigger"/> class.
    /// </summary>
    public OpenApiHttpTriggerContext()
    {
        var host = HostJsonResolver.Resolve();
        this.HttpSettings = host.GetHttpSettings();
        Console.WriteLine(host);
        this.OpenApiInfo = OpenApiInfoResolver.Resolve(host);
        this.HttpSettings = host.GetHttpSettings();

        var filter = new RouteConstraintFilter();
        var acceptor = new OpenApiSchemaAcceptor();
        var helper = new DocumentHelper(filter, acceptor);

        this.Document = new Document(helper);
        this.SwaggerUI = new SwaggerUI();
    }

    /// <inheritdoc />
    public virtual OpenApiInfo OpenApiInfo { get; }

    /// <inheritdoc />
    public virtual HttpSettings HttpSettings { get; }

    /// <inheritdoc />
    public virtual IDocument Document { get; }

    /// <inheritdoc />
    public virtual ISwaggerUI SwaggerUI { get; }

    /// <inheritdoc />
    public virtual NamingStrategy NamingStrategy { get; } = new CamelCaseNamingStrategy();

    /// <inheritdoc />
    public virtual Assembly GetExecutingAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }

    /// <inheritdoc />
    public virtual VisitorCollection GetVisitorCollection()
    {
        var collection = VisitorCollection.CreateInstance();

        return collection;
    }

    /// <inheritdoc />
    public virtual OpenApiSpecVersion GetOpenApiSpecVersion(string version = "v2")
    {
        var parsed = Enum.TryParse(version, true, out OpenApiVersionType output)
                         ? output
                         : throw new InvalidOperationException("Invalid Open API version");

        return this.GetOpenApiSpecVersion(parsed);
    }

    /// <inheritdoc />
    public virtual OpenApiSpecVersion GetOpenApiSpecVersion(OpenApiVersionType version = OpenApiVersionType.V2)
    {
        return version.ToOpenApiSpecVersion();
    }

    /// <inheritdoc />
    public virtual OpenApiFormat GetOpenApiFormat(string format = "json")
    {
        if (format.Equals("yml", StringComparison.InvariantCultureIgnoreCase))
        {
            format = "yaml";
        }

        var parsed = Enum.TryParse(format, true, out OpenApiFormatType output)
                         ? output
                         : throw new InvalidOperationException("Invalid Open API format");

        return this.GetOpenApiFormat(parsed);
    }

    /// <inheritdoc />
    public virtual OpenApiFormat GetOpenApiFormat(OpenApiFormatType format = OpenApiFormatType.Json)
    {
        return format.ToOpenApiFormat();
    }

    /// <inheritdoc />
    public virtual string GetSwaggerAuthKey(string key = "OpenApi__ApiKey")
    {
        var value = Environment.GetEnvironmentVariable(key);

        return value;
    }
}