targetScope = 'subscription'

param resourceGroupName string = 'Application'
param location string = deployment().location
param imagetag string
param dockerNamespace string


resource resourceGroup 'Microsoft.Resources/resourceGroups@2023-07-01' = {
  name: resourceGroupName
  location: location 
}

module deployToResourceGroup 'deployToResourceGroup.bicep' = {
  name: 'deployToResourceGroup'
  scope: resourceGroup
  params: {
    imagetag: imagetag
    dockerNamespace: dockerNamespace
  }
}

