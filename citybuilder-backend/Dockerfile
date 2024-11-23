# Basis image voor de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# SDK image voor de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Kopieer de broncode (zorg ervoor dat je de juiste locatie van de map opgeeft)
COPY ..\..\citybuilder-backend\ ./citybuilder-backend/

# Restore de dependencies
RUN dotnet restore ./citybuilder-backend/citybuilder-backend.sln

# Bouw de oplossing
RUN dotnet build ./citybuilder-backend/citybuilder-backend.sln -c $BUILD_CONFIGURATION -o /app/build

# Publiceer de applicatie
FROM build AS publish
RUN dotnet publish ./citybuilder-backend/citybuilder-backend.api/SOC-backend.api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image voor productie
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOC-backend.api.dll"]
