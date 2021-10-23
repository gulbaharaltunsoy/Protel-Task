# Protel Task 

## Pre Requirements
* [.Net SDK 3.1](https://dotnet.microsoft.com/download/dotnet/3.1)
* [Visual Studio or Visual Studio Code](https://visualstudio.microsoft.com/tr/downloads/)
* [MSSQL (If use Database)](https://www.microsoft.com/tr-tr/sql-server/sql-server-downloads)

## Framework and Packages
*   **.NET Core 3.1**
    *   Hangfire for background job
    *   Microsoft.EntityFrameworkCore for Database
    *   xUnit for Test
    *   Moq for Fake Objects

********************
## Project
#### Edit Config
If you aren't using **Local Database** , you will change `"ConnectionStrings:ProtelDbConnection"{Your Connection String}` and `"ConnectionStrings:HangfireDbConnection"{Your Connection String}` in `.\Portel\appsettings.json`.

#### Hangfire Requirements
```sh
You will create a database with database name 'HangfireDB'.
```

#### Pre Command
```sh
dotnet restore
```
#### Project Run Command
```sh
dotnet run --project Protel
```
**[UI](https://localhost:5001/)**
**[Hangfire](https://localhost:5001/job)**

#### Test Run Projects Command
```sh
dotnet test
```
********************