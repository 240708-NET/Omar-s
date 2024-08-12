# Use the official .NET SDK 8.0 image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the project files to the container
COPY . ./

# Build the project
RUN dotnet publish ./Project0.csproj -c Release -o out

# Use the official .NET runtime 8.0 image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the build output from the build-env
COPY --from=build-env /app/out .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "Project0.dll"]
