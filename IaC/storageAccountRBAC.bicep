param storageAccountName string
param principalId string

@allowed([
  'ForeignGroup'
  'Group'
  'ServicePrincipal'
  'User'
])
param principalType string

@allowed([
  'Storage Blob Data Contributor'
  'Storage Blob Data Reader'
])
param roleDefinition string

var roles = {
  // See https://docs.microsoft.com/en-us/azure/role-based-access-control/built-in-roles for these mappings and more.
  'Storage Blob Data Contributor': '/providers/Microsoft.Authorization/roleDefinitions/ba92f5b4-2d11-453d-a403-e96b0029c9fe'
  'Storage Blob Data Reader': '/providers/Microsoft.Authorization/roleDefinitions/2a2b9908-6ea1-4ae2-8e65-a410df84e7d1'
}

var roleDefinitionId = roles[roleDefinition]

resource storageAccount 'Microsoft.Storage/storageAccounts@2023-01-01' existing = {
  name: storageAccountName
}

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  scope: storageAccount
  name: guid('storage-rbac', resourceGroup().id, principalId, roleDefinitionId)
  properties: {
    principalId: principalId
    roleDefinitionId: roleDefinitionId
    principalType: principalType
  }
}
