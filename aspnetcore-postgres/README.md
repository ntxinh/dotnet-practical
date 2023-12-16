```bash
mkdir aspnetcore-postgres
cd aspnetcore-postgres
dotnet new globaljson --sdk-version 8.0.100
dotnet new gitignore
dotnet new sln -n AspNetCore-Postgres
mkdir Src
cd Src
dotnet new webapi --no-https --framework net8.0 -o AspNetCore-Postgres
cd ..
dotnet sln add Src/AspNetCore-Postgres/AspNetCore-Postgres.csproj
cd Src/AspNetCore-Postgres
dotnet add package Npgsql --version 8.0.1
cd ../..
dotnet build
```

- http://localhost:5000/swagger/index.html
- http://localhost:5000/weatherforecast