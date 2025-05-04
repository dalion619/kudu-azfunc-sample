#r "Newtonsoft.Json" //using Newtonsoft.Json;
#r "..\bin\Aliencube.AzureFunctions.Extensions.OpenApi.Core.dll" //using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Attributes;
#r "Microsoft.Azure.WebJobs.Extensions.Http" //using Microsoft.Azure.WebJobs.Extensions.Http;
#r "..\bin\Microsoft.OpenApi.dll" //using Microsoft.OpenApi;
#load "..\Shared\JsonContentResult.csx"  
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

 
[FunctionName("HelloWorld")]
[OpenApiOperation(operationId: "HelloWorld", tags: new[] { "" }, Visibility = OpenApiVisibilityType.Advanced)]  
[OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "Dummy name", Description = "Dummy name", Visibility = OpenApiVisibilityType.Important)]      
public static async Task<IActionResult> HelloWorld([HttpTrigger(AuthorizationLevel.Anonymous, "GET")]  HttpRequest req, ILogger log )
{
 
    log.LogInformation("C# HTTP trigger function processed a request.");

    string name = req.Query["name"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync(); 
    string responseMessage = string.IsNullOrEmpty(name)
        ? "!Hello World!"
        : $"Hello, {name} This HTTP triggered function executed successfully.";
    return new OkObjectResult(responseMessage);
}
           