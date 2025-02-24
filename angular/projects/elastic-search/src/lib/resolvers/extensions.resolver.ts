import {
  ExtensionsService,
  getObjectExtensionEntitiesFromStore,
  mapEntitiesToContributors,
  mergeWithDefaultActions,
  mergeWithDefaultProps,
} from '@abp/ng.components/extensible';
import { Injector, inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { map, tap } from 'rxjs';
import {
  EsAuditLoggingEntityActionContributors,
  EsAuditLoggingToolbarActionContributors,
  EsAuditLoggingEntityPropContributors,
} from '../models';
import {
  ES_AUDIT_LOGGING_ENTITY_ACTION_CONTRIBUTORS,
  ES_AUDIT_LOGGING_TOOLBAR_ACTION_CONTRIBUTORS,
  ES_AUDIT_LOGGING_ENTITY_PROP_CONTRIBUTORS,
  DEFAULT_ES_AUDIT_LOGGING_ENTITY_ACTIONS,
  DEFAULT_ES_AUDIT_LOGGING_TOOLBAR_ACTIONS,
  DEFAULT_ES_AUDIT_LOGGING_ENTITY_PROPS,
} from '../tokens';
import { eElasticSearchComponents } from '@feramor/ng.abp-audit-logging-elastic-search/common';

export const esAuditLoggingExtensionsResolver: ResolveFn<any> = () => {
  const injector = inject(Injector);

  const extensions: ExtensionsService = injector.get(ExtensionsService);
  const actionContributors: EsAuditLoggingEntityActionContributors =
    injector.get(ES_AUDIT_LOGGING_ENTITY_ACTION_CONTRIBUTORS, null) || {};
  const toolbarContributors: EsAuditLoggingToolbarActionContributors =
    injector.get(ES_AUDIT_LOGGING_TOOLBAR_ACTION_CONTRIBUTORS, null) || {};
  const propContributors: EsAuditLoggingEntityPropContributors =
    injector.get(ES_AUDIT_LOGGING_ENTITY_PROP_CONTRIBUTORS, null) || {};

  return getObjectExtensionEntitiesFromStore(injector, 'ElasticSearch').pipe(
    map(entities => ({
      [eElasticSearchComponents.AuditLogs]: entities.AuditLog,
    })),
    mapEntitiesToContributors(injector, 'ElasticSearch'),
    tap(objectExtensionContributors => {
      mergeWithDefaultActions(
        extensions.entityActions,
        DEFAULT_ES_AUDIT_LOGGING_ENTITY_ACTIONS,
        actionContributors,
      );
      mergeWithDefaultActions(
        extensions.toolbarActions,
        DEFAULT_ES_AUDIT_LOGGING_TOOLBAR_ACTIONS,
        toolbarContributors,
      );
      mergeWithDefaultProps(
        extensions.entityProps,
        DEFAULT_ES_AUDIT_LOGGING_ENTITY_PROPS,
        objectExtensionContributors.prop,
        propContributors,
      );
    }),
  );
};
