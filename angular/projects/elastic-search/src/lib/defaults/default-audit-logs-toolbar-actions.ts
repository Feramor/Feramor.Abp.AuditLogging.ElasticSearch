import { ToolbarAction } from '@abp/ng.components/extensible';
import { AuditLogDto } from '@feramor/ng.abp-audit-logging-elastic-search/proxy';

export const DEFAULT_ES_AUDIT_LOGS_TOOLBAR_ACTIONS = ToolbarAction.createMany<AuditLogDto[]>([]);
