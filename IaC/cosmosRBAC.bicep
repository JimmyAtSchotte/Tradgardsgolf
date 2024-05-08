param cosmosAccountName string
param principalId string

@allowed([
  'ForeignGroup'
  'Group'
  'ServicePrincipal'
  'User'
])
param principalType string

@allowed([
  'Data Reader'
  'Data Contributor'
])
param roleDefinition string

var roles = {
  // See https://docs.microsoft.com/en-us/azure/role-based-access-control/built-in-roles for these mappings and more.
  'Data Reader': '/providers/Microsoft.Authorization/roleDefinitions/00000000-0000-0000-0000-000000000001'
  'Data Contributor': '/providers/Microsoft.Authorization/roleDefinitions/00000000-0000-0000-0000-000000000002'
}

var roleDefinitionId = roles[roleDefinition]

resource databaseAccount 'Microsoft.DocumentDB/databaseAccounts@2024-02-15-preview' existing = {
  name: cosmosAccountName
}

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  scope: databaseAccount
  name: guid('cosmos-rbac', resourceGroup().id, principalId, roleDefinitionId)
  properties: {
    principalId: principalId
    roleDefinitionId: roleDefinitionId
    principalType: principalType
  }
}
