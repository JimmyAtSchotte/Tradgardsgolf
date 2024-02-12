param location string
param prefix string
param storageAccountConnectionString string


resource eventGridTopic 'Microsoft.EventGrid/topics@2020-10-15-preview' = {
  name: '${prefix}-topics'
  location: location
  sku: {
    name: 'Basic'
  }
}

resource appServicePlan 'Microsoft.Web/serverFarms@2018-02-01' = {
  name: '${prefix}-functions-plan'
  location: location
  sku: {
    name: 'Y1'
    tier: 'Dynamic'
  }
}


resource functions 'Microsoft.Web/sites@2023-01-01' = {
  name: '${prefix}-functions'
  location: location
  kind: 'functionapp,linux'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      appSettings: [
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~4'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet-isolated'
        }
        {
          name: 'WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED'
          value: '1'
        }
        {
          name: 'AzureWebJobsStorage'
          value: storageAccountConnectionString
        }
        {
          name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
          value: storageAccountConnectionString
        }
        {
          name: 'EventGridTopicEndpoint'
          value: eventGridTopic.properties.endpoint
        }
      ]
    }
  }
}

resource functionsConfig 'Microsoft.Web/sites/config@2023-01-01' = {
  name: 'web'
  parent: functions
  properties: {
    netFrameworkVersion: 'v8.0' 
  } 
}
