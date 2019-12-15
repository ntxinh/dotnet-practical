```sh
mkdir ef-cosmos
cd ef-cosmos/
dotnet --list-sdks
dotnet new globaljson --sdk-version 3.1.100
dotnet new sln --name EfCosmos
mkdir src
cd src/
mkdir EfCosmos.Services.Api
cd EfCosmos.Services.Api/
dotnet new webapi --no-https -f netcoreapp3.1
cd ../..
dotnet sln add src/EfCosmos.Services.Api/EfCosmos.Services.Api.csproj
dotnet sln list
dotnet new gitignore
dotnet add src/EfCosmos.Services.Api/EfCosmos.Services.Api.csproj package Microsoft.EntityFrameworkCore.Cosmos
```

```sh
dotnet run --project src/EfCosmos.Services.Api/EfCosmos.Services.Api.csproj --launch-profile CLI_DEV
```