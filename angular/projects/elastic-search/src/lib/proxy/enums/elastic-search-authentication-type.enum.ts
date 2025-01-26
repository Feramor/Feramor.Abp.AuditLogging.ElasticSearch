import { mapEnumToOptions } from '@abp/ng.core';

export enum ElasticSearchAuthenticationType {
  BasicAuthentication = 0,
  ApiKeyAuthentication = 1,
  Base64ApiKey = 2,
}

export const elasticSearchAuthenticationTypeOptions = mapEnumToOptions(ElasticSearchAuthenticationType);
