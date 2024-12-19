# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the entire solution directory into the container
COPY . .

# Debugging step: Check the files in /src
RUN ls -la /src

# Restore dependencies
RUN dotnet restore "citybuilder-backend.sln"

# Build the application
RUN dotnet build "citybuilder-backend.sln" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
RUN dotnet publish "citybuilder-backend.sln" -c $BUILD_CONFIGURATION -o /app/publish

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "citybuilder-backend.dll"]
