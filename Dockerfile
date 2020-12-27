# Dockerfile

ARG ENV=api

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore

FROM build AS publish-api
RUN dotnet publish Tradgardsgolf.Api -c Release -o out

FROM build AS publish-wasm
RUN dotnet publish Tradgardsgolf.Blazor.Wasm -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final-api
WORKDIR /app
COPY --from=publish-api /app/out .
#LOCAL
#ENTRYPOINT [ "dotnet", "Tradgardsgolf.Api.dll" ]
#HEROKU
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Tradgardsgolf.Api.dll

FROM nginx:alpine AS final-wasm
WORKDIR /usr/share/nginx/html
COPY --from=publish-wasm /app/out/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf

FROM final-${ENV} AS final