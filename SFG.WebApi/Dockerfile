FROM mcr.microsoft.com/dotnet/sdk:7.0 AS dev-build
WORKDIR /SFG.WebApi-Build
COPY . ./
RUN dotnet restore
RUN dotnet publish "SFG.WebApi" -c Release -o Build
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /SFG.WebApi-Release
COPY --from=dev-build /SFG.WebApi-Build/Build /SFG.WebApi-Release
ENV ASPNETCORE_URLS=http://+:7001
ENTRYPOINT ["dotnet", "SFG.WebApi.dll"]