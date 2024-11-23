# Gebruik ASP.NET runtime image als basis
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Gebruik .NET SDK image voor het bouwen van de applicatie
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Kopieer alle bestanden van het project naar de container
COPY . .

# Herstel de NuGet-pakketten
RUN dotnet restore "citybuilder-backend/citybuilder-backend/Api.csproj"

# Bouw het project
RUN dotnet build "citybuilder-backend/citybuilder-backend/Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publiceer het project
FROM build AS publish
RUN dotnet publish "citybuilder-backend/citybuilder-backend/Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Gebruik het base image voor de productieomgeving
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
