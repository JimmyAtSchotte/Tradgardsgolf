param location string
param prefix string
param appServicePlanId string
param container {
  namespace: string
  tag: string
}

param configuration {
  connectionstring: string
}

resource webApi 'Microsoft.Web/sites@2023-01-01' = {
  name: '${prefix}-api'
  location: location
  kind: 'app,linux,container'
  properties: {
    serverFarmId: appServicePlanId
    siteConfig: {
      linuxFxVersion: 'DOCKER|${container.namespace}/tradgardsgolf-api:${container.tag}'
    }   
  }
}

resource webApiConfig 'Microsoft.Web/sites/config@2023-01-01' = {
  name: 'web'
  parent: webApi
  properties: {  
    connectionStrings: [
      {
        name: 'Database'
        connectionString: configuration.connectionstring
        type: 'SQLAzure'
      }
    ]    
  } 
}

resource webApiHostName 'Microsoft.Web/sites/hostNameBindings@2023-01-01' = {
  parent: webApi
  name: '${webApi.name}.azurewebsites.net'
  properties: {
    siteName: webApi.name
    hostNameType: 'Verified'
  }
}

resource webApiInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: '${webApi.name}-insights'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}

output apiUrl string = 'https://${webApiHostName.name}/'
output instrumentationKey string = webApiInsights.properties.InstrumentationKey
