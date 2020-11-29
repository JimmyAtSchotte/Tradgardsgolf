# Dockerfile

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS build-env
WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet publish Tradgardsgolf.Blazor -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Tradgardsgolf.Blazor.dll