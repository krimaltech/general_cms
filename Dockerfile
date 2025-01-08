# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["backend.csproj", "./"]
RUN dotnet restore

# Copy the entire project
COPY . .

# Build and publish
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

# Use the runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Run the app
ENTRYPOINT ["dotnet", "backend.dll"]
