param location string
param prefix string

resource keyvault 'Microsoft.KeyVault/vaults@2023-07-01' = {
  location: location
  name: '${prefix}-kv'
  properties: {
    sku: {
      name: 'standard'
      family: 'A'
    }
    tenantId: subscription().tenantId
    enableRbacAuthorization: true
  }
}

output keyvaultName string = keyvault.name
