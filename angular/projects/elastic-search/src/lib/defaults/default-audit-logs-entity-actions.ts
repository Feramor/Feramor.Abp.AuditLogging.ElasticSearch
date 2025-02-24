import { EntityAction } from '@abp/ng.components/extensible';
import { AuditLogsComponent } from '../components/audit-logs/audit-logs.component';
import { AuditLogDto } from '@feramor/ng.abp-audit-logging-elastic-search/proxy';

export const DEFAULT_ES_AUDIT_LOGS_ENTITY_ACTIONS = EntityAction.createMany<AuditLogDto>([
  {
    text: 'ElasticSearch::Button:Details',
    action: data => {
      const component = data.getInjected(AuditLogsComponent);
      component.openModal(data.record.id);
    },
    icon: 'fa fa-eye',
  },
]);
