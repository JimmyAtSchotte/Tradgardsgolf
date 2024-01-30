param location string
param prefix string
param sqlServerExists bool
param dbConfig {
  DefaultSqlPassword: string
  DefaultSqlUsername: string
  SqlAdminGroupId: string
  SqlAdminGroupName: string
}

resource sqlServer 'Microsoft.Sql/servers@2023-05-01-preview' = {
  name: '${prefix}-db-srv'
  location: location
  identity: !sqlServerExists ? {
    type: 'SystemAssigned'
  } : null
  properties: !sqlServerExists ? {
    administratorLogin: dbConfig.DefaultSqlUsername
    administratorLoginPassword: dbConfig.DefaultSqlPassword
  } : null
}

resource sqlAdministrator 'Microsoft.Sql/servers/administrators@2023-05-01-preview' = {
  name: 'ActiveDirectory'
  parent: sqlServer
  properties: {
    administratorType: 'ActiveDirectory'
    login: dbConfig.SqlAdminGroupName
    sid: dbConfig.SqlAdminGroupId
    tenantId: tenant().tenantId
  }
}

resource aadAuth 'Microsoft.Sql/servers/azureADOnlyAuthentications@2023-05-01-preview' = {
  name: 'Default'
  parent: sqlServer
  dependsOn: [ sqlAdministrator ]
  properties: {
    azureADOnlyAuthentication: true
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

output connectionstring string = 'Server=tcp:${sqlServer.properties.fullyQualifiedDomainName},1433;Initial Catalog=${database.name};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication="Active Directory Default";'
