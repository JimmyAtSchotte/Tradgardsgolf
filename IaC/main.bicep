targetScope = 'subscription'

param resourceGroupName string = 'tradgardsgolf-dev'
param location string = deployment().location
param namespace string
param tag string

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

module storage 'storage.bicep' = {
  name: 'storage'
  scope: resourceGroup
  params: {
    location: location
  }
}

module sqlServer 'sqlServer.bicep' = {  
  name: 'sqlServer'
  scope: resourceGroup
  params: {
    location: location
    prefix: resourceGroupName
    defaultSqlPassword: deploymentkeyvalues.getSecret('DefaultSqlPassword')
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
    sqlPassword: deploymentkeyvalues.getSecret('DefaultSqlPassword')
    storage: {
      connectionString: storage.outputs.connectionString
      container: storage.outputs.container
    }
  }
  dependsOn: [ sqlServer, appServicePlan, storage ]
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

module functions 'functions.bicep' = {
  name: 'functions'
  scope: resourceGroup
  params: {
    location: location
    prefix: resourceGroupName
    storageAccountConnectionString: storage.outputs.connectionString
  }
  dependsOn: [ storage ]
}


module storageAccountRBAC 'storageAccountRBAC.bicep' = {
  name: 'storageAccountRBAC'
  scope: resourceGroup
  params: {
    storageAccountName: storage.outputs.storageAccountName
    principalId: webApi.outputs.principalId
    roleDefinitionId: 'ba92f5b4-2d11-453d-a403-e96b0029c9fe'
    principalType: 'ServicePrincipal'
  }
}
