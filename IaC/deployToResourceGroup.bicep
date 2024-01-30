param location string
param prefix string
param namespace string
param tag string
param sqlServerExists bool

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

module sqlServer 'sqlServer.bicep' = {  
  name: 'sqlServer'
  params: {
    location: location
    prefix: prefix
    sqlServerExists: sqlServerExists
    dbConfig: {
      DefaultSqlPassword: DefaultSqlPassword
      DefaultSqlUsername: DefaultSqlUsername
      SqlAdminGroupId: SqlAdminGroupId
      SqlAdminGroupName: SqlAdminGroupName
    }
  }  
}

module webApi 'webApi.bicep' = {
  name: 'webApi'
  params: {
    location: location
    prefix: prefix
    appServicePlanId: appServicePlan.id
    container: container
    configuration: {
      connectionstring: sqlServer.outputs.connectionstring
    }
  }
  dependsOn: [ sqlServer ]
}


module webApp 'webApp.bicep' = {
  name: 'webApp'
  params: {
    location: location
    prefix: prefix
    appServicePlanId: appServicePlan.id
    container: container
    configuration: {
      apiUrl: webApi.outputs.apiUrl
    }
  }
  dependsOn: [ webApi ]
}

module configuration 'configuration.bicep' = {
  name: 'configuration'
  params: {
    location: location
    prefix: prefix
    configuration: {
      webApi: {
        InstrumentationKey: webApi.outputs.instrumentationKey
      }
    }
  }
  dependsOn: [ webApi ]
}



