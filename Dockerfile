# Use a base image with the .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use a base image with the .NET SDK for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CatalogService.API/CatalogService.API.csproj", "CatalogService.API/"]
RUN dotnet restore "CatalogService.API/CatalogService.API.csproj"
COPY . .
WORKDIR "/src/CatalogService.API"
RUN dotnet build "CatalogService.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "CatalogService.API.csproj" -c Release -o /app/publish

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogService.API.dll"]
