#!/bin/sh

# Execute update_blazor_settings.sh
/docker-entrypoint.d/update_blazor_settings.sh

# Start Nginx
sed -i -e 's/$PORT/'"$PORT"'/g' /etc/nginx/nginx.conf
nginx -g 'daemon off;'