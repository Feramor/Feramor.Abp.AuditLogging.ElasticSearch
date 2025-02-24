import {
  EntityActionContributorCallback,
  EntityPropContributorCallback,
  ToolbarActionContributorCallback,
} from '@abp/ng.components/extensible';
import { InjectionToken } from '@angular/core';
import { DEFAULT_ES_AUDIT_LOGS_ENTITY_ACTIONS } from '../defaults/default-audit-logs-entity-actions';
import { DEFAULT_ES_AUDIT_LOGS_ENTITY_PROPS } from '../defaults/default-audit-logs-entity-props';
import { DEFAULT_ES_AUDIT_LOGS_TOOLBAR_ACTIONS } from '../defaults/default-audit-logs-toolbar-actions';
import { DEFAULT_ES_ENTITY_CHANGES_ENTITY_ACTIONS } from '../defaults/default-entity-changes-entity-actions';
import { AuditLogDto, EntityChangeDto } from '@feramor/ng.abp-audit-logging-elastic-search/proxy';
import { eElasticSearchComponents } from '@feramor/ng.abp-audit-logging-elastic-search/common';

export const DEFAULT_ES_AUDIT_LOGGING_ENTITY_ACTIONS = {
  [eElasticSearchComponents.AuditLogs]: DEFAULT_ES_AUDIT_LOGS_ENTITY_ACTIONS,
  [eElasticSearchComponents.EntityChanges]: DEFAULT_ES_ENTITY_CHANGES_ENTITY_ACTIONS,
};

export const DEFAULT_ES_AUDIT_LOGGING_TOOLBAR_ACTIONS = {
  [eElasticSearchComponents.AuditLogs]: DEFAULT_ES_AUDIT_LOGS_TOOLBAR_ACTIONS,
};
export const DEFAULT_ES_AUDIT_LOGGING_ENTITY_PROPS = {
  [eElasticSearchComponents.AuditLogs]: DEFAULT_ES_AUDIT_LOGS_ENTITY_PROPS,
};

export const ES_AUDIT_LOGGING_ENTITY_ACTION_CONTRIBUTORS =
  new InjectionToken<EntityActionContributors>('ES_AUDIT_LOGGING_ENTITY_ACTION_CONTRIBUTORS');

export const ES_AUDIT_LOGGING_TOOLBAR_ACTION_CONTRIBUTORS =
  new InjectionToken<ToolbarActionContributors>('ES_AUDIT_LOGGING_TOOLBAR_ACTION_CONTRIBUTORS');

export const ES_AUDIT_LOGGING_ENTITY_PROP_CONTRIBUTORS = new InjectionToken<EntityPropContributors>(
  'ES_AUDIT_LOGGING_ENTITY_PROP_CONTRIBUTORS',
);

// https://github.com/microsoft/TypeScript/issues/9944#issuecomment-254693497
type EntityActionContributors = Partial<{
  [eElasticSearchComponents.AuditLogs]: EntityActionContributorCallback<AuditLogDto>[];
  [eElasticSearchComponents.EntityChanges]: EntityActionContributorCallback<EntityChangeDto>[];
}>;
type ToolbarActionContributors = Partial<{
  [eElasticSearchComponents.AuditLogs]: ToolbarActionContributorCallback<AuditLogDto[]>[];
}>;
type EntityPropContributors = Partial<{
  [eElasticSearchComponents.AuditLogs]: EntityPropContributorCallback<AuditLogDto>[];
}>;
