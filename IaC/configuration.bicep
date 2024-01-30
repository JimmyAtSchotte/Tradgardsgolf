param location string
param prefix string
param configuration {
  webApi: {
    InstrumentationKey: string
  }
}

resource appConfiguration 'Microsoft.AppConfiguration/configurationStores@2023-03-01' = { 
  location: location
   name: '${prefix}-config'
   sku: {
    name: 'Free'
   }
}

resource configurationStoreValue 'Microsoft.AppConfiguration/configurationStores/keyValues@2023-03-01' = {
  name: 'ApplicationInsights:InstrumentationKey'
  parent: appConfiguration
  properties: {
    value: configuration.webApi.InstrumentationKey
  }
}
