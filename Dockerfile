# Gebruik de .NET SDK om te bouwen
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopieer de solution en projectbestanden
COPY citybuilder-backend.sln .
COPY ./citybuilder-backend/ ./citybuilder-backend/

# Controleer bestanden (debugging)
RUN ls -la
RUN ls -la ./citybuilder-backend

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
