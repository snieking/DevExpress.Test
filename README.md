# How to

## Requirements
* .NET Core 3.1
* .NET Core EF (CLI) tools https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet
* SQL Server Database

## Connection String 
Modify Connection String in **appsettings.json**.

## Seed Database

```shell
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Run 

```
dotnet run
``` 
