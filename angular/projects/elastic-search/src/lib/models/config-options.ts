import {
  EntityActionContributorCallback,
  EntityPropContributorCallback,
  ToolbarActionContributorCallback,
} from '@abp/ng.components/extensible';
import { AuditLogDto, EntityChangeDto } from '@feramor/ng.abp-audit-logging-elastic-search/proxy';
import { eElasticSearchComponents } from '@feramor/ng.abp-audit-logging-elastic-search/common';

export type EsAuditLoggingEntityActionContributors = Partial<{
  [eElasticSearchComponents.AuditLogs]: EntityActionContributorCallback<AuditLogDto>[];
  [eElasticSearchComponents.EntityChanges]: EntityActionContributorCallback<EntityChangeDto>[];
}>;

export type EsAuditLoggingToolbarActionContributors = Partial<{
  [eElasticSearchComponents.AuditLogs]: ToolbarActionContributorCallback<AuditLogDto[]>[];
}>;

export type EsAuditLoggingEntityPropContributors = Partial<{
  [eElasticSearchComponents.AuditLogs]: EntityPropContributorCallback<AuditLogDto>[];
}>;

export interface EsAuditLoggingConfigOptions {
  entityActionContributors?: EsAuditLoggingEntityActionContributors;
  toolbarActionContributors?: EsAuditLoggingToolbarActionContributors;
  entityPropContributors?: EsAuditLoggingEntityPropContributors;
}
