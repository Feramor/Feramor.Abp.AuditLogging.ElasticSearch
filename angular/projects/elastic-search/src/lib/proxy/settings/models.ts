import type { ElasticSearchAuthenticationType } from '../enums/elastic-search-authentication-type.enum';

export interface ElasticSearchAuditLogSettingsDto {
  isActive: any;
  uri?: string;
  useSsl: any;
  sslFingerprint?: string;
  authenticationType?: ElasticSearchAuthenticationType;
  username?: string;
  index?: string;
}

export interface UpdateElasticSearchAuditLogSettingsDto {
  isActive: any;
  uri?: string;
  useSsl: any;
  sslFingerprint?: string;
  authenticationType?: ElasticSearchAuthenticationType;
  username?: string;
  password?: string;
  apiKey?: string;
  apiKeyId?: string;
  index?: string;
}
