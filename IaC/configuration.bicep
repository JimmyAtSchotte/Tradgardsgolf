param location string
param prefix string
param configuration {
  webApi: {
    InstrumentationKey: string
  }
  storage: {
    connectionString: string
    container: string
  }
}

resource appConfiguration 'Microsoft.AppConfiguration/configurationStores@2023-03-01' = { 
  location: location
   name: '${prefix}-config'
   sku: {
    name: 'Free'
   }
}

resource applicationInsightsInstrumentationKey 'Microsoft.AppConfiguration/configurationStores/keyValues@2023-03-01' = {
  name: 'ApplicationInsights:InstrumentationKey'
  parent: appConfiguration
  properties: {
    value: configuration.webApi.InstrumentationKey
  }
}

resource azureStorageConnectionString 'Microsoft.AppConfiguration/configurationStores/keyValues@2023-03-01' = {
  name: 'AzureStorage:ConnectionString'
  parent: appConfiguration
  properties: {
    value: configuration.storage.connectionString
  }
}


resource azureStorageContainer 'Microsoft.AppConfiguration/configurationStores/keyValues@2023-03-01' = {
  name: 'AzureStorage:Container'
  parent: appConfiguration
  properties: {
    value: configuration.storage.container
  }
}
