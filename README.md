# Azure Functions Code Sample

### web.config
Simple reverse proxy using a URL Rewrite rule

### csx-basic
Basic HTTP func using csx

### compiled-swagger
Compiled C# HTTP func with Swagger/OpenAPI

`dotnet publish -c Debug -r win-x64 -p:PublishReadyToRun=true`

### csx-swagger
HTTP func with Swagger/OpenAPI using csx. Only the bin folder is required from the build for the ref assemblies.

`dotnet publish -c Debug -r win-x64 -p:PublishReadyToRun=true`
