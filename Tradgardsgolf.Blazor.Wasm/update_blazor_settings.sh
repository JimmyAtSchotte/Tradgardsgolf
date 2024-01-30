#!/bin/sh
echo "Running update_blazor_settings.sh script..."

# Path to the appsettings.json file (using a relative path)
APPSETTINGS_FILE="/usr/share/nginx/html/appsettings.json"
appsettings=$(cat "$APPSETTINGS_FILE")

# Loop through environment variables starting with "BLAZOR__"
for var in $(env | grep '^BLAZOR__'); do
    name=$(echo "$var" | cut -d '=' -f 1)
    value=$(echo "$var" | cut -d '=' -f 2-)
    
    echo "Found variable $name"
    
    name_without_prefix=${name#"BLAZOR__"}
    json_path=$(echo ".$name_without_prefix" | tr -s '_' '.')
    
    # Update the appsettings JSON object
    appsettings=$(echo "$appsettings" | jq --arg argValue "$value" --arg argJsonPath "$json_path" 'setpath([($argJsonPath | split(".")[1:])[]]; $argValue)')
done

echo "$appsettings" > "$APPSETTINGS_FILE"

echo "Environment variables applied to appsettings.json."
