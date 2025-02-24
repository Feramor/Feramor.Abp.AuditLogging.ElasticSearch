import type { AuditLogDto, GetAuditLogsDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuditLogsService {
  apiName = 'ElasticSearch';
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AuditLogDto>({
      method: 'GET',
      url: `/api/es-audit-logs/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetAuditLogsDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<AuditLogDto>>({
      method: 'GET',
      url: '/api/es-audit-logs',
      params: { startTime: input.startTime, endTime: input.endTime, url: input.url, clientId: input.clientId, userName: input.userName, applicationName: input.applicationName, clientIpAddress: input.clientIpAddress, correlationId: input.correlationId, httpMethod: input.httpMethod, httpStatusCode: input.httpStatusCode, maxExecutionDuration: input.maxExecutionDuration, minExecutionDuration: input.minExecutionDuration, hasException: input.hasException, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
