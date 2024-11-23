FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
# Gebruik de .NET SDK om te bouwen
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY .

# Controleer de bestanden (debugging)
RUN ls -la

# Restore de dependencies
RUN dotnet restore citybuilder-backend.sln

# Build de applicatie
RUN dotnet build citybuilder-backend.sln -c Release -o /app/build

# Publiceer de applicatie
RUN dotnet publish citybuilder-backend.sln -c Release -o /app/publish

# Gebruik de runtime-image voor productie
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "citybuilder-backend.dll"]
