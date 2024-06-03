FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR ./Api

# Copy everything
COPY ./PostsService ./PostsService
COPY ./LocalPackages ./LocalPackages
COPY ./MessageContracts ./MessageContracts

# Restore as distinct layers
WORKDIR ./PostsService
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o Build --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR ./Api
COPY --from=build ./Api/PostsService/Build .
ENTRYPOINT ["dotnet", "PostsService.dll"]