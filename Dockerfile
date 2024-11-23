# Basis image voor runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# SDK image voor build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Kopieer de bestanden
COPY citybuilder-backend.sln ./         # .sln in huidige map
COPY citybuilder-backend/ ./citybuilder-backend/  # Kopieer de projectmap
RUN ls -la
RUN ls -la ./citybuilder-backend  # Controleer of de projectmap ook aanwezig is
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
