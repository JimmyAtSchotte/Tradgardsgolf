param location string
param prefix string
param appServicePlanId string

@secure()
param sqlUsername string

@secure()
param sqlPassword string

param sqlServer string
param database string

param container {
  namespace: string
  tag: string
}


resource webApi 'Microsoft.Web/sites@2023-01-01' = {
  name: '${prefix}-api'
  location: location
  kind: 'app,linux,container'
  identity: {
    type: 'SystemAssigned'
  }
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
        connectionString: 'Server=tcp:${sqlServer},1433;Initial Catalog=${database};Persist Security Info=False;User ID=${sqlUsername};Password=${sqlPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
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

