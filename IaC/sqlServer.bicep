param location string
param prefix string
param keyvaultName string

@secure()
param sqlAdminGroupId string
@secure()
param sqlAdminGroupName string
@secure()
param defaultSqlPassword string

var dbConfig = {
  defaultSqlPassword: defaultSqlPassword
  username: '${prefix}-db'
  sqlAdminGroupId: sqlAdminGroupId
  sqlAdminGroupName: sqlAdminGroupName
}

resource sqlServer 'Microsoft.Sql/servers@2023-05-01-preview' = {
  name: '${prefix}-db-srv'
  location: location
  identity: {
    type: 'SystemAssigned'
  } 
  properties: {
    administratorLogin: dbConfig.username
    administratorLoginPassword: dbConfig.defaultSqlPassword
  }
}

resource sqlAdministrator 'Microsoft.Sql/servers/administrators@2023-05-01-preview' = {
  name: 'ActiveDirectory'
  parent: sqlServer
  properties: {
    administratorType: 'ActiveDirectory'
    login: dbConfig.sqlAdminGroupName
    sid: dbConfig.sqlAdminGroupId
    tenantId: tenant().tenantId
  }
}


resource SQLAllowAllWindowsAzureIps 'Microsoft.Sql/servers/firewallRules@2023-05-01-preview' = {
  name: 'AllowAllWindowsAzureIps'
  parent: sqlServer
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
}

resource database 'Microsoft.Sql/servers/databases@2023-05-01-preview' = {
  name: '${prefix}-db'
  parent: sqlServer
  location: location
  sku: {
    name: 'GP_S_Gen5'
    tier: 'GeneralPurpose'
    family: 'Gen5'
    capacity: 2
  }
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    //maxSizeBytes: 1073741824
    catalogCollation: 'SQL_Latin1_General_CP1_CI_AS'
    zoneRedundant: false
    readScale: 'Disabled'
    autoPauseDelay: 60
    requestedBackupStorageRedundancy: 'Local'
    isLedgerOn: false
    useFreeLimit: true
    freeLimitExhaustionBehavior: 'AutoPause'
    availabilityZone: 'NoPreference'
  } 
}

resource keyvault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyvaultName
  resource dbConnectionString 'secrets' = {
    name: 'db-connection-string'
    properties: {
      value: 'Server=tcp:${sqlServer.properties.fullyQualifiedDomainName},1433;Initial Catalog=${database.name};Persist Security Info=False;User ID=${dbConfig.username};Password=${dbConfig.defaultSqlPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
    }
  }
}


