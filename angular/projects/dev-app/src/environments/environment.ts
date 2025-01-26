import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'GeneralTest',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44363/',
    redirectUri: baseUrl,
    clientId: 'GeneralTest_App',
    responseType: 'code',
    scope: 'offline_access GeneralTest',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44363',
      rootNamespace: 'Feramor.Abp.AuditLogging.ElasticSearch',
    },
    ElasticSearch: {
      url: 'https://localhost:44363',
      rootNamespace: 'Feramor.Abp.AuditLogging.ElasticSearch',
    },
  },
} as Environment;
