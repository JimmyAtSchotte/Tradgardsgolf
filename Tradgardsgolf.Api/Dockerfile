# Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY .. .
RUN dotnet restore Tradgardsgolf.sln

FROM build AS publish-api
RUN dotnet publish Tradgardsgolf.Api -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final-api
WORKDIR /app
COPY --from=publish-api /app/out .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Tradgardsgolf.Api.dll

FROM final-api AS final