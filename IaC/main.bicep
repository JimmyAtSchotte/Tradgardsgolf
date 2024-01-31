targetScope = 'subscription'

param resourceGroupName string = 'tradgardsgolf-dev'
param location string = deployment().location
param namespace string
param tag string
param sqlServerExists bool

var container = {
  namespace: namespace
  tag: tag
}

resource deploymentResourceGroup 'Microsoft.Resources/resourceGroups@2023-07-01' existing = {
  name: 'deployment'
}

resource deploymentkeyvalues 'Microsoft.KeyVault/vaults@2023-02-01' existing = {
  name: 'deploymentkeyvalues'
  scope: deploymentResourceGroup
}

resource resourceGroup 'Microsoft.Resources/resourceGroups@2023-07-01' = {
  name: resourceGroupName
  location: location 
}

module appServicePlan 'appServicePlan.bicep' = {
  name: 'appServicePlan'
  scope: resourceGroup
  params: {
    location: location
    prefix: resourceGroupName
  }
}

module sqlServer 'sqlServer.bicep' = {  
  name: 'sqlServer'
  scope: resourceGroup
  params: {
    location: location
    prefix: resourceGroupName
    sqlServerExists: sqlServerExists   
    defaultSqlPassword: deploymentkeyvalues.getSecret('DefaultSqlPassword')
    defaultSqlUsername: deploymentkeyvalues.getSecret('DefaultSqlUsername')
    sqlAdminGroupId: deploymentkeyvalues.getSecret('SqlAdminGroupId')
    sqlAdminGroupName: deploymentkeyvalues.getSecret('SqlAdminGroupName')
  }  
}

module webApi 'webApi.bicep' = {
  name: 'webApi'  
  scope: resourceGroup
  params: {
    location: location
    prefix: resourceGroupName
    appServicePlanId: appServicePlan.outputs.id
    container: container
    sqlServer: sqlServer.outputs.server
    database: sqlServer.outputs.database
    sqlUsername: deploymentkeyvalues.getSecret('DefaultSqlUsername')
    sqlPassword: deploymentkeyvalues.getSecret('DefaultSqlPassword')    
  }
  dependsOn: [ sqlServer, appServicePlan ]
}


module webApp 'webApp.bicep' = {
  name: 'webApp'  
  scope: resourceGroup
  params: {
    location: location
    prefix: resourceGroupName
    appServicePlanId: appServicePlan.outputs.id
    container: container
    configuration: {
      apiUrl: webApi.outputs.apiUrl
    }
  }
  dependsOn: [ webApi, appServicePlan ]
}

module storage 'storage.bicep' = {
  name: 'storage'
  scope: resourceGroup
  params: {
    location: location
  }
}

module configuration 'configuration.bicep' = {
  name: 'configuration'
  scope: resourceGroup
  params: {
    location: location
    prefix: resourceGroupName
    configuration: {
      webApi: {
        InstrumentationKey: webApi.outputs.instrumentationKey
      }
      storage: {
        connectionString: storage.outputs.connectionString
        container: storage.outputs.container
      }
    }
  }
  dependsOn: [ webApi, storage ]
}


