FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app

# copy csproj and restore as distinct layers
# COPY *.sln .
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build app
COPY ./ ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "aspnetcore-docker.dll"]