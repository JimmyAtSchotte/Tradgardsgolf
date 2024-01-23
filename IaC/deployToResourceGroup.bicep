param location string = resourceGroup().location
param imagetag string
param dockerNamespace string

resource appServicePlan 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: 'tradgardsgolf-api-plan'
  location: location
  sku: {
    name: 'F1'
  }
  kind: 'linux'
}

resource webApp 'Microsoft.Web/sites@2023-01-01' = {
  name: 'tradgardsgolf'
  location: location
  kind: 'app,linux,container'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'DOCKER|${dockerNamespace}/tradgardsgolf-wasm:${imagetag}'
    }
  }
}

resource webApi 'Microsoft.Web/sites@2023-01-01' = {
  name: 'tradgardsgolf-api'
  location: location
  kind: 'app,linux,container'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'DOCKER|${dockerNamespace}/tradgardsgolf-api:${imagetag}'
    }
  }
}
