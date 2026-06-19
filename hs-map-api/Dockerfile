# 1. Update runtime image to .NET 10
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 2. Update SDK image to .NET 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["hs-map-api.csproj", "."]
RUN dotnet restore "hs-map-api.csproj" --source https://api.nuget.org/v3/index.json
COPY . .
WORKDIR "/src/."
RUN dotnet build "hs-map-api.csproj" -c Release -o /app/build

# 3. Publish the compiled binaries
FROM build AS publish
RUN dotnet publish "hs-map-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4. Copy final binaries over to the .NET 10 runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hs-map-api.dll"]