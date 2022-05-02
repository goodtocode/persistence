# GoodToCode Persistence Library
[![Build Status](https://dev.azure.com/GoodToCode/GoodToCode.com/_apis/build/status/g2c-rg-shared?branchName=main)](https://dev.azure.com/GoodToCode/GoodToCode.com/_build/latest?definitionId=62&branchName=main)

GoodToCode Persistence is a cross-cutting (AOP) centric collection of projects that support common plumbing done in Azure Functions, Web API, Blazor and Razor Pages. GoodToCode Persistence is based on serverless, DDD, onion-architecture, vertical slice and CQRS in .NET Core and EF Core code-first.

## Prerequisites
You will need the following tools:
* [Visual Studio Code or 2022](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 6.0 or above](https://www.microsoft.com/net/download/dotnet-core/6.0)

## Setup
Follow these steps to get your development environment set up:

### 1. Setup your local ASPNETCORE_ENVIRONMENT setting
    ```
- Application layer uses ASPNETCORE_ENVIRONMENT environment variable, and will default to Production if not found.
- Add the ASPNETCORE_ENVIRONMENT entry in your Enviornment Variables

	To accopmlish this on Windows
	1. Open Control Panel > System > >Advanced Settings > Environment Variables > System Variables
	2. Next add new User Variable
		UserVariables > New
			Variable name:  ASPNETCORE_ENVIRONMENT
			Variable value: Development
	3. Then Ok (to save)
	 
	You will need to restart Visual Studio, VS Code and Terminals to see the changes
	```

### 2. Setup your Azure Storage Account and Azure Service Bus connection string in appsettings.*.json
```
- Application layer requires 1 Azure Storage Account and 1 Azure Service Bus cloud resource to operate.
- Both JSON configuration (appsettings.*.json) and Azure App Configuration service are supported
	
To accopmlish this in appsettings.*.json
1. Open all instances of appsettings.Development.json and appsettings.Production.json
2. Copy your Azure Service Bus Connection String from the Azure Portal
3. Paste your connection string over the following setting:
	"ConnectionStrings": {
		"StorageTablesConnection": "AZURESTORAGE_CONNECTION_STRING_HERE",
		"ServiceBusConnection": "AZURESERVICEBUS_CONNECTION_STRING_HERE"
	}
4. Repeat for both Development and Production
5. Save all instances of appsettings.Development.json and appsettings.Production.json
```

## Namespaces
### GoodToCode.Persistence.Abstractions
Includes cross-platform interfaces and abstractions for use in all assemblies.

### GoodToCode.Persistence.Blob
Includes all abstractions, classes and service collection extensions for Azure Blob Storage, Excel file reading/writing, and CSV reading/writing.

### GoodToCode.Persistence.Azure
Includes all abstractions, classes and service collection extensions for Azure Storage Tables, CosmosDb, Entity Framework (EF) Core, etc.


## Contact
* [GitHub Repo](https://www.github.com/goodtocode/persistence)
* [@goodtocode](https://www.twitter.com/goodtocode)
* [github.com/goodtocode](https://www.github.com/goodtocode)

## Technologies
* .NET 6.0

## Additional Technologies References
* Azure Storage Tables
* Azure CosmosDb
* Entity Framework Core