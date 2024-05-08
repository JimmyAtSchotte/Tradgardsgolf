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

module cosmos 'cosmos.bicep' = {
  name: 'cosmos'
  scope: resourceGroup
  params: {
    location: location
    keyvaultName: keyvault.outputs.keyvaultName
    prefix: resourceGroupName
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
    storage: {
      container: storage.outputs.container
    }
  }
  dependsOn: [ cosmos, appServicePlan, storage ]
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


module apiKeyvaultSecretUser 'keyvaultRBAC.bicep' = {
  name: 'apiKeyvaultSecretUser'
  scope: resourceGroup
  params: {
    keyvaultName: keyvault.outputs.keyvaultName
    principalId: webApi.outputs.principalId
    principalType: 'ServicePrincipal'
    roleDefinition: 'Key Vault Secrets User'
  }
}


module functionsKeyvaultSecretUser 'keyvaultRBAC.bicep' = {
  name: 'functionsKeyvaultSecretUser'
  scope: resourceGroup
  params: {
    keyvaultName: keyvault.outputs.keyvaultName
    principalId: functions.outputs.principalId
    principalType: 'ServicePrincipal'
    roleDefinition: 'Key Vault Secrets User'
  }
}

module cosmosRbac 'cosmosRBAC.bicep' = {
  name: 'cosmosRbac'
  scope: resourceGroup
  params: {
    cosmosAccountName: cosmos.outputs.cosmosAccountName
    principalId: webApi.outputs.principalId
  }
}
