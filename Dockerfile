# Basis image voor de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# SDK image voor de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Kopieer de oplossing en de broncode naar de container
COPY citybuilder-backend.sln .  # Kopieer de oplossing naar de werkdirectory
COPY citybuilder-backend/ ./citybuilder-backend/  # Kopieer de broncode naar de juiste map

# Debug stap: Controleer of de bestanden correct gekopieerd zijn
RUN ls -la
RUN ls -la ./citybuilder-backend  # Controleer de inhoud van de citybuilder-backend map

# Herstel de dependencies
RUN dotnet restore citybuilder-backend.sln

# Bouw de oplossing
RUN dotnet build citybuilder-backend.sln -c $BUILD_CONFIGURATION -o /app/build

# Publiceer de applicatie
FROM build AS publish
RUN dotnet publish citybuilder-backend/Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image voor productie
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
