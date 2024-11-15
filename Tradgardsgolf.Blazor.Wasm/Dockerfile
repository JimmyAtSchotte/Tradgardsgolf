FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore Tradgardsgolf.sln

FROM build AS publish-wasm
RUN dotnet publish Tradgardsgolf.Blazor.Wasm -c Release -o out

FROM nginx:alpine AS final-wasm
WORKDIR /usr/share/nginx/html
RUN apk add jq

COPY ./Tradgardsgolf.Blazor.Wasm/update_blazor_settings.sh /docker-entrypoint.d/update_blazor_settings.sh
COPY --from=publish-wasm /app/out/wwwroot .
COPY ./Tradgardsgolf.Blazor.Wasm/nginx.conf /etc/nginx/nginx.conf
COPY ./Tradgardsgolf.Blazor.Wasm/app_startup.sh /app_startup.sh

RUN chmod +x /docker-entrypoint.d/update_blazor_settings.sh
RUN chmod +x /app_startup.sh

ENTRYPOINT ["/app_startup.sh"]

# Move the final stage definition to the end
FROM final-wasm AS final
