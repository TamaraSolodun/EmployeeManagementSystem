# Use the runtime image as the base for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the full context (all necessary projects) into the container
COPY . .

# Restore dependencies for all projects in the solution
RUN dotnet restore "./API/API.csproj"

# Move into the project directory and build the application
WORKDIR /src/API
RUN dotnet build "./API.csproj" -c Release -o /app/build

# Publish the application for optimized deployment
FROM build AS publish
RUN dotnet publish "./API.csproj" -c Release -o /app/publish --no-restore /p:UseAppHost=false

# The final image based on ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "API.dll"]
