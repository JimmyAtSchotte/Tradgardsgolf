param location string = resourceGroup().location
param prefix string = resourceGroup().name
param namespace string
param tag string


@secure()
param SqlAdminGroupId string
@secure()
param SqlAdminGroupName string
@secure()
param DefaultSqlPassword string
@secure()
param DefaultSqlUsername string

var container = {
  namespace: namespace
  tag: tag
}

var dbConfig = {
  DefaultSqlPassword: DefaultSqlPassword
  DefaultSqlUsername: DefaultSqlUsername
  SqlAdminGroupId: SqlAdminGroupId
  SqlAdminGroupName: SqlAdminGroupName
}

resource appServicePlan 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: '${prefix}-service-plan'
  location: location
  sku: {
    name: 'F1'
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

var databaseServerExists = false

resource databaseServer 'Microsoft.Sql/servers@2023-05-01-preview' =  {
  name: '${prefix}-db-srv'
  location: location
  properties: {
    administratorLogin: databaseServerExists ? null : dbConfig.DefaultSqlUsername
    administratorLoginPassword: databaseServerExists ? null : dbConfig.DefaultSqlPassword
    administrators: {
      administratorType: 'ActiveDirectory'
      principalType: 'Group'
      login: dbConfig.SqlAdminGroupName
      sid: dbConfig.SqlAdminGroupId
      tenantId: tenant().tenantId
      azureADOnlyAuthentication: true
    }
  }  
}

resource database 'Microsoft.Sql/servers/databases@2023-05-01-preview' = {
  name: '${prefix}-db'
  parent: databaseServer
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

resource webApi 'Microsoft.Web/sites@2023-01-01' = {
  name: '${prefix}-api'
  location: location
  kind: 'app,linux,container'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'DOCKER|${container.namespace}/tradgardsgolf-api:${container.tag}'
    }
  }
}

resource webApiConfig 'Microsoft.Web/sites/config@2023-01-01' = {
  name: 'web'
  parent: webApi
  properties: {
    connectionStrings: [
      {
        name: 'Database'
        connectionString: 'Server=tcp:${databaseServer.properties.fullyQualifiedDomainName},1433;Initial Catalog=${database.name};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication="Active Directory Default";'
        type: 'SQLAzure'
      }
    ]
  } 
}

resource webApiInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: '${webApi.name}-insights'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}


resource webApp 'Microsoft.Web/sites@2023-01-01' = {
  name: '${prefix}-app'
  location: location
  kind: 'app,linux,container'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'DOCKER|${container.namespace}/tradgardsgolf-wasm:${container.tag}'
    }
  }
}

resource appConfiguration 'Microsoft.AppConfiguration/configurationStores@2023-03-01' = { 
  location: location
   name: '${prefix}-config'
   sku: {
    name: 'Free'
   }
}

resource configurationStoreValue 'Microsoft.AppConfiguration/configurationStores/keyValues@2023-03-01' = {
  name: 'ApplicationInsights:InstrumentationKey'
  parent: appConfiguration
  properties: {
    value: webApiInsights.properties.InstrumentationKey
  }
}


