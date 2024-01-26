targetScope = 'subscription'

param resourceGroupName string = 'tradgardsgolf-dev'
param location string = deployment().location
param namespace string
param tag string


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

module deployToResourceGroup 'deployToResourceGroup.bicep' = {
  name: 'deployToResourceGroup'
  scope: resourceGroup
  params: {
    namespace: namespace
    tag: tag
    location: location
    prefix: resourceGroup.name    
    SqlAdminGroupId: deploymentkeyvalues.getSecret('SqlAdminGroupId')
    SqlAdminGroupName: deploymentkeyvalues.getSecret('SqlAdminGroupName')
    DefaultSqlPassword: deploymentkeyvalues.getSecret('DefaultSqlPassword')
    DefaultSqlUsername: deploymentkeyvalues.getSecret('DefaultSqlUsername')    
  }
}

