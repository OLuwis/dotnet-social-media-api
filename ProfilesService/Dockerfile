FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR ./Api

# Copy everything
COPY ./ProfilesService ./ProfilesService
COPY ./LocalPackages ./LocalPackages
COPY ./MessageContracts ./MessageContracts

# Restore as distinct layers
WORKDIR ./ProfilesService
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o Build --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR ./Api
COPY --from=build ./Api/ProfilesService/Build .
ENTRYPOINT ["dotnet", "ProfilesService.dll"]