import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'ElasticSearch',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44350/',
    redirectUri: baseUrl,
    clientId: 'ElasticSearch_App',
    responseType: 'code',
    scope: 'offline_access ElasticSearch',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44350',
      rootNamespace: 'Feramor.Abp.AuditLogging.ElasticSearch',
    },
    ElasticSearch: {
      url: 'https://localhost:44345',
      rootNamespace: 'Feramor.Abp.AuditLogging.ElasticSearch',
    },
  },
} as Environment;
