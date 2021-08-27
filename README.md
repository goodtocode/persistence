# GoodToCode Shared Library

GoodToCode Shared is a microservice/serverless centric collection of NuGet packages that support common plumbing done in Azure Functions, Web API, Blazor and Razor Pages. GoodToCode Shared is based on serverless, DDD, onion-architecture, vertical slice and CQRS in .NET Core and EF Core code-first.

## .NET Versions
This library remains current with .NET, updating dependencies 3-6 months after a major release. Current versions are:
* .NET 5.0
* .NET Standard 2.1
* Azure Functions v3 running .NET 5.0

## How-to Setup Environment to run *.Tests.csproj projects
### Two (2) Environment Variables are required
The various *.Tests.csproj projects depend on Azure App Configuration to operate. If you wish to execute the test methods:
1. Create "AzureSettingConnection" (required)
* Go to portal.azure.com
* Create or find your Azure App Configuration service connection string

    Powershell: $env:AzureSettingConnection="Endpoint=https://{Your-Endpoint}.azconfig.io;Id={Your-Key}"
    
2. Create "ASPNETCORE_ENVIRONMENT" (optional, defaults to "Production")
* Local
* Development
* Production

    Powershell: $env:ASPNETCORE_ENVIRONMENT="Development"

### Each test project includes AppSettingsKey.cs files
If a test project depends on a configuration setting, you will find an AppSettingsKeys.cs file.
If a key is required, please add the key by:
1. Go to portal.azure.com
2. Create or find your Azure App Configuration service 
3. Add the required keys, with value from your Azure resources. I.e.
* Gtc:Shared:Sentinel - true - text/plain

## Namespaces
### GoodToCode.Shared.Analytics
Includes all abstractions, classes and service collection extensions for Azure Cognitive Services text analytics. More APIs are added each iteration.

### GoodToCode.Shared.Blob
Includes all abstractions, classes and service collection extensions for Azure Blob Storage, Excel file reading/writing, and CSV reading/writing.

### GoodToCode.Shared.dotNet
Includes all extensions for .NET. I.e. convert.ToDictionary<>().

### GoodToCode.Shared.Patterns
Includes all abstractions, classes and service collection extensions for common patterns, architectures and principles. Such as CQRS, Repository, DDD, Service Collection Extension, etc. Serverless patterns are a focus as well.

### GoodToCode.Shared.Persistence
Includes all abstractions, classes and service collection extensions for Azure Storage Tables, CosmosDb, Entity Framework (EF) Core, etc.

### GoodToCode.Shared.Spatial
Includes all abstractions, classes and service collection extensions for Geography, Geometry and Spatial operations.


Disclaimer: This work is under development mostly for internal projects, and is still highly volatile. Merges to main branch are tested and pushed to NuGet.org.

