# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the solution file and restore dependencies
COPY ["Chrono-Master.sln", "."]
COPY ["src/api/WebApi/WebApi.csproj", "src/api/WebApi/"]
COPY ["src/api/Application/Application.csproj", "src/api/Application/"]
COPY ["src/api/Domain/Domain.csproj", "src/api/Domain/"]
COPY ["src/api/Infrastructure/Infrastructure.csproj", "src/api/Infrastructure/"]

RUN dotnet restore "src/api/WebApi/WebApi.csproj"
RUN dotnet restore "src/api/Application/Application.csproj"
RUN dotnet restore "src/api/Infrastructure/Infrastructure.csproj"
RUN dotnet restore "src/api/Domain/Domain.csproj"

# Copy the entire solution and build the application
COPY . .
WORKDIR "/src/src/api/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

# Build the application for publishing
FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

# Use the official .NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
EXPOSE 80

# Copy the published output from the 'publish' stage
COPY --from=publish /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "WebApi.dll"]
