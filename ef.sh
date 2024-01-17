#!/bin/bash
# Ask the user for their name

echo Migration name
read migration

echo Connection string
read conn

export SQLAZURECONNSTR_Database=$conn
dotnet ef migrations add $migration --project Tradgardsgolf.Application.Infrastructure --startup-project Tradgardsgolf.Api -v
export SQLAZURECONNSTR_Database=

echo Completed!
read
