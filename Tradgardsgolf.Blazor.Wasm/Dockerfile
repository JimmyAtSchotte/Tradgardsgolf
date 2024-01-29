# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY .. .
RUN dotnet restore

# Publish Blazor WebAssembly app
FROM build AS publish-wasm
RUN dotnet publish Tradgardsgolf.Blazor.Wasm -c Release -o out

# NGINX stage
FROM nginx:alpine AS final-wasm
WORKDIR /usr/share/nginx/html

# Copy static files from the published Blazor WebAssembly app
COPY --from=publish-wasm /app/out/wwwroot .

# Copy NGINX configuration
COPY nginx.conf /etc/nginx/nginx.conf

# Replace $PORT in NGINX configuration with the environment variable
CMD sed -i -e 's/$PORT/'"$PORT"'/g' /etc/nginx/nginx.conf && \
    sed -i 's|"Backend": ".*"|"Backend": "'"$BACKEND_URL"'"|' appsettings.json && \
    nginx -g 'daemon off;'

COPY Tradgardsgolf.Blazor.Wasm/sshd_config /etc/ssh/
COPY Tradgardsgolf.Blazor.Wasm/entrypoint.sh ./

# Start and enable SSH
RUN apk add openssh \
    && echo "root:Docker!" | chpasswd \
    && chmod +x ./entrypoint.sh \
    && cd /etc/ssh/ \
    && ssh-keygen -A


EXPOSE 8000 2222


ENTRYPOINT [ "./entrypoint.sh" ] 

# Final stage
