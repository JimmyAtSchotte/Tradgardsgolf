
param prefix string
param keyvaultName string

resource maps 'Microsoft.Maps/accounts@2024-01-01-preview' = {
  location: 'northeurope'
  name: '${prefix}-maps'
  sku: {
    name:'G2'
  }
  kind: 'Gen2'
  properties: {
    cors: {corsRules: [
      {
        allowedOrigins: [
        'https://tradgardsgolf-app.azurewebsites.net'
        'https://localhost:7157'
      ]}
    ]}
  }
}

resource keyvault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyvaultName
  resource mapsSubscriptionKey 'secrets' = {
    name: 'maps-subscription-key'
    properties: {
      value: listKeys(resourceId('Microsoft.Maps/accounts', maps.name), '2024-01-01-preview').primaryKey
    }
  }
}

