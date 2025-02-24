 import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44363/',
  redirectUri: baseUrl,
  clientId: 'GeneralTest_App',
  responseType: 'code',
  scope: 'offline_access GeneralTest',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'GeneralTest',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44363',
      rootNamespace: 'GeneralTest',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
