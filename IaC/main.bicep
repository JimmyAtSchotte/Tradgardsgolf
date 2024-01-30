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

resource resourceGroup 'Microsoft.Resources/resourceGroups@2023-07-01' = {
  name: resourceGroupName
  location: location 
}

resource deploymentResourceGroup 'Microsoft.Resources/resourceGroups@2023-07-01' existing = {
  name: 'deployment'
}

resource deploymentkeyvalues 'Microsoft.KeyVault/vaults@2023-02-01' existing = {
  name: 'deploymentkeyvalues'
  scope: deploymentResourceGroup
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
    configuration: {
      connectionstring: sqlServer.outputs.connectionstring
    }
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
    }
  }
  dependsOn: [ webApi ]
}


