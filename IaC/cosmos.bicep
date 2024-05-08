param location string
param keyvaultName string
param prefix string

var accountName = '${prefix}-db-account'
var databaseName = '${prefix}-db'
var throughput = 1000

resource databaseAccount 'Microsoft.DocumentDB/databaseAccounts@2024-02-15-preview' = {
  name: accountName
  kind: 'GlobalDocumentDB'
  location: location
  properties: {
    consistencyPolicy: {
      defaultConsistencyLevel: 'Session'
    }
    locations: [
      {      
        locationName: location
      }
    ]
    databaseAccountOfferType: 'Standard'
    isVirtualNetworkFilterEnabled: false
    enableMultipleWriteLocations: false
    enableFreeTier: true    
    enableAutomaticFailover: false
    capacity: {
      totalThroughputLimit: throughput
    }
  }
}

resource database 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases@2023-11-15' = {
  parent: databaseAccount
  name: databaseName
  properties: {
    resource: {
      id: databaseName
    }
    options: {
      throughput: throughput
    }
  }
} 

resource keyvault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyvaultName
  resource cosmosConnectionString 'secrets' = {
    name: 'cosmos-connection-string'
    properties: {
      value: databaseAccount.listConnectionStrings().connectionStrings[0].connectionString
    }
  }
}

output cosmosAccountName string = databaseAccount.name
