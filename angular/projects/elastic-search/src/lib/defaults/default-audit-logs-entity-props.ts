import { EntityProp, ePropType } from '@abp/ng.components/extensible';
import { of } from 'rxjs';
import { AuditLogsComponent } from '../components/audit-logs/audit-logs.component';
import { AuditLogDto } from '@feramor/ng.abp-audit-logging-elastic-search/proxy';

export const DEFAULT_ES_AUDIT_LOGS_ENTITY_PROPS = EntityProp.createMany<AuditLogDto>([
  {
    type: ePropType.String,
    name: 'url',
    displayName: 'ElasticSearch::DisplayName:HttpRequest',
    sortable: true,
    columnWidth: 600,
    valueResolver: data => {
      const component = data.getInjected(AuditLogsComponent);
      const { httpMethod, httpStatusCode, url } = data.record;
      const methodClass = component.httpMethodClass(httpMethod);
      const statusClass = component.httpCodeClass(httpStatusCode);

      return of(
        `<span class="badge ${statusClass} me-1">${httpStatusCode}</span>` +
          `<span class="badge ${methodClass} me-1">${httpMethod}</span>` +
          (url || ''),
      );
    },
  },
  {
    type: ePropType.String,
    name: 'userName',
    displayName: 'ElasticSearch::DisplayName:User',
    sortable: true,
    columnWidth: 150,
  },
  {
    type: ePropType.String,
    name: 'clientIpAddress',
    displayName: 'ElasticSearch::DisplayName:IpAddress',
    sortable: true,
    columnWidth: 150,
  },
  {
    type: ePropType.DateTime,
    name: 'executionTime',
    displayName: 'AbpIdentity::DisplayName:Date',
    sortable: true,
    columnWidth: 150,
  },
  {
    type: ePropType.Number,
    name: 'executionDuration',
    displayName: 'ElasticSearch::DisplayName:Duration',
    sortable: true,
    columnWidth: 150,
  },
  {
    type: ePropType.String,
    name: 'applicationName',
    displayName: 'ElasticSearch::DisplayName:ApplicationName',
    sortable: true,
    columnWidth: 200,
  },
]);
