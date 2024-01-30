FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore

FROM build AS publish-wasm
RUN dotnet publish Tradgardsgolf.Blazor.Wasm -c Release -o out

FROM nginx:alpine AS final-wasm
WORKDIR /usr/share/nginx/html
RUN apk add jq

COPY ./Tradgardsgolf.Blazor.Wasm/update_blazor_settings.sh /docker-entrypoint.d/update_blazor_settings.sh
RUN chmod +x /docker-entrypoint.d/update_blazor_settings.sh

COPY --from=publish-wasm /app/out/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf

CMD sed -i -e 's/$PORT/'"$PORT"'/g' /etc/nginx/nginx.conf && nginx -g 'daemon off;'

# Move the final stage definition to the end
FROM final-wasm AS final
