FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /ftp-server

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /ftp-server
COPY --from=build-env /ftp-server/out .


EXPOSE 5227
EXPOSE 7159
ENV ASPNETCORE_URLS=http://*:5227
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "ftp-server.dll"]
