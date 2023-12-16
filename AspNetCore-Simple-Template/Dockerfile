# .NET Core SDK
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build

# Sets the working directory
WORKDIR /app

# Copy Projects
#COPY *.sln .
COPY Src/AST.Core/AST.Core.csproj ./AST.Core/
COPY Src/AST.Application/AST.Application.csproj ./AST.Application/
COPY Src/AST.Infrastructure/AST.Infrastructure.csproj ./AST.Infrastructure/
COPY Src/AST.Web/AST.Web.csproj ./AST.Web/

# .NET Core Restore
RUN dotnet restore ./AST.Web/AST.Web.csproj

# Copy All Files
COPY Src ./

# .NET Core Build and Publish
RUN dotnet publish ./AST.Web/AST.Web.csproj -c Release -o /publish

# ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /publish ./
#EXPOSE 80
#EXPOSE 443
ENTRYPOINT ["dotnet", "AST.Web.dll"]
