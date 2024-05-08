param location string
param prefix string
param appServicePlanId string
param keyvaultName string

param container {
  namespace: string
  tag: string
}

param storage {
  container: string
}

resource keyvault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyvaultName
  resource storageConnectionString 'secrets' existing = {
    name: 'storage-connection-string'
  }
  resource cosmosConnectionString 'secrets' existing = {
    name: 'cosmos-connection-string'
  }
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

resource webApiHostName 'Microsoft.Web/sites/hostNameBindings@2023-01-01' = {
  parent: webApi
  name: '${webApi.name}.azurewebsites.net'
  properties: {
    siteName: webApi.name
    hostNameType: 'Verified'
  }
}

resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: '${webApi.name}-analytics-workspace'
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
  }
}

resource webApiInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: '${webApi.name}-insights'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalyticsWorkspace.id    
  }
}


resource webApiConfig 'Microsoft.Web/sites/config@2023-01-01' = {
  name: 'web'
  parent: webApi
  properties: {
    appSettings: [
      {
        name: 'AllowPlayDistance__Value'
        value: '400'
      }
      {
        name: 'AzureStorage__ConnectionString'
        value: '@Microsoft.KeyVault(SecretUri=${keyvault::storageConnectionString.properties.secretUri})'
      }
      {
        name: 'AzureStorage__Container'
        value: storage.container
      }
      {
        name: 'ApplicationInsights__InstrumentationKey'
        value: webApiInsights.properties.InstrumentationKey
      }      
    ]
    
    connectionStrings: [
       {
        name: 'Database'
        connectionString: '@Microsoft.KeyVault(SecretUri=${keyvault::cosmosConnectionString.properties.secretUri})'
        type: 'Custom'
      }

    ]    
  } 
}

output apiUrl string = 'https://${webApiHostName.name}/'
output principalId string = webApi.identity.principalId

