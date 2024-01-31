param location string
param prefix string
param sqlServerExists bool

@secure()
param sqlAdminGroupId string
@secure()
param sqlAdminGroupName string
@secure()
param defaultSqlPassword string
@secure()
param defaultSqlUsername string

var dbConfig = {
  defaultSqlPassword: defaultSqlPassword
  defaultSqlUsername: defaultSqlUsername
  sqlAdminGroupId: sqlAdminGroupId
  sqlAdminGroupName: sqlAdminGroupName
}

resource sqlServer 'Microsoft.Sql/servers@2023-05-01-preview' = {
  name: '${prefix}-db-srv'
  location: location
  identity: !sqlServerExists ? {
    type: 'SystemAssigned'
  } : null
  properties: !sqlServerExists ? {
    administratorLogin: dbConfig.defaultSqlUsername
    administratorLoginPassword: dbConfig.defaultSqlPassword
  } : null
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
    //useFreeLimit: true
    //freeLimitExhaustionBehavior: 'AutoPause'
    availabilityZone: 'NoPreference'
  } 
}

output server string = sqlServer.properties.fullyQualifiedDomainName
output database string = database.name


