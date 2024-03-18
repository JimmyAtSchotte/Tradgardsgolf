targetScope = 'subscription'

param resourceGroupName string = 'tradgardsgolf'
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

module keyvault 'keyvault.bicep' = {
  name: 'keyvault'
  scope: resourceGroup
  params: {
    location: location
    prefix: resourceGroupName
  }
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
    keyvaultName: keyvault.outputs.keyvaultName
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
    keyvaultName: keyvault.outputs.keyvaultName
    container: container
    sqlServer: sqlServer.outputs.server
    database: sqlServer.outputs.database
    sqlPassword: deploymentkeyvalues.getSecret('DefaultSqlPassword')
    storage: {
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
    keyvaultName: keyvault.outputs.keyvaultName
  }
  dependsOn: [ storage ]
}


module storageAccountRBAC 'storageAccountRBAC.bicep' = {
  name: 'storageAccountRBAC'
  scope: resourceGroup
  params: {
    storageAccountName: storage.outputs.storageAccountName
    principalId: webApi.outputs.principalId
    roleDefinition: 'Storage Blob Data Contributor'
    principalType: 'ServicePrincipal'
  }
}


module keyvaultReader 'keyvaultRBAC.bicep' = {
  name: 'keyvaultReader'
  scope: resourceGroup
  params: {
    keyvaultName: keyvault.outputs.keyvaultName
    principalId: webApi.outputs.principalId
    principalType: 'ServicePrincipal'
    roleDefinition: 'Key Vault Reader'
  }
}

module keyvaultSecretUser 'keyvaultRBAC.bicep' = {
  name: 'keyvaultSecretUser'
  scope: resourceGroup
  params: {
    keyvaultName: keyvault.outputs.keyvaultName
    principalId: webApi.outputs.principalId
    principalType: 'ServicePrincipal'
    roleDefinition: 'Key Vault Secrets User'
  }
}
