#!/bin/bash

public_ip=$(curl -s ifconfig.me)
resource_group="Application"
server_name="tradgardsgolf-db-srv"
firewall_rule_name="db-migration"

echo Login into Azure
az login

echo Migration name
read migration

echo Connection string
read conn

export SQLAZURECONNSTR_Database=$conn

az sql server update -g $resource_group -n $server_name --set publicNetworkAccess="Enabled"
az sql server firewall-rule create -g $resource_group -s $server_name -n $firewall_rule_name --start-ip-address $public_ip --end-ip-address $public_ip

dotnet ef migrations add $migration --project Tradgardsgolf.Application.Infrastructure --startup-project Tradgardsgolf.Api -v

az sql server firewall-rule delete -g $resource_group -s $server_name -n $firewall_rule_name
az sql server update -g $resource_group -n $server_name --set publicNetworkAccess="Disabled"

export SQLAZURECONNSTR_Database=

echo Completed!
read
