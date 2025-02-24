import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eThemeSharedRouteNames } from '@abp/ng.theme.shared';
import {
  eElasticSearchPolicyNames,
  eElasticSearchRouteNames
} from '@feramor/ng.abp-audit-logging-elastic-search/common';

export const ELASTIC_SEARCH_ROUTE_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureRoutes,
    deps: [RoutesService],
    multi: true,
  },
];

export function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        name: eElasticSearchRouteNames.ElasticSearchAuditLogging,
        path: '/es-audit-logs',
        parentName: eThemeSharedRouteNames.Administration,
        layout: eLayoutType.application,
        iconClass: 'fa fa-file-text',
        order: 6,
        requiredPolicy: eElasticSearchPolicyNames.AuditLogs,
      },
    ]);
  };
}
