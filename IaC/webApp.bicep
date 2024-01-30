param location string
param prefix string
param appServicePlanId string
param container {
  namespace: string
  tag: string
}

param configuration {
  apiUrl: string
}


resource webApp 'Microsoft.Web/sites@2023-01-01' = {
  name: '${prefix}-app'
  location: location
  kind: 'app,linux,container'
  properties: {
    serverFarmId: appServicePlanId
    siteConfig: {
      linuxFxVersion: 'DOCKER|${container.namespace}/tradgardsgolf-wasm:${container.tag}'
    }
  }
}

resource webAppConfig 'Microsoft.Web/sites/config@2023-01-01' = {
  name: 'appsettings'
  parent: webApp
  properties: {
    BLAZOR__Backend__Url: configuration.apiUrl
  } 
}
