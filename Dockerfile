FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY citybuilder-backend.sln .                    
COPY citybuilder-backend/ ./citybuilder-backend/
RUN dotnet restore ./citybuilder-backend/citybuilder-backend.sln

RUN dotnet build ./citybuilder-backend/citybuilder-backend.sln -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "citybuilder-backend/citybuilder-backend/Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
