import type { ElasticSearchAuditLogSettingsDto, UpdateElasticSearchAuditLogSettingsDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ElasticSearchSettingsService {
  apiName = 'ElasticSearch';
  

  get = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ElasticSearchAuditLogSettingsDto>({
      method: 'GET',
      url: '/api/setting-management/elastic-search',
    },
    { apiName: this.apiName,...config });
  

  testConnection = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/setting-management/elastic-search/test-connection',
    },
    { apiName: this.apiName,...config });
  

  update = (input: UpdateElasticSearchAuditLogSettingsDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/setting-management/elastic-search',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
