import { NgModule } from '@angular/core';
import {
  authGuard,
  permissionGuard,
  ReplaceableComponents,
  ReplaceableRouteContainerComponent,
  RouterOutletComponent
} from '@abp/ng.core';
import { Routes, RouterModule } from '@angular/router';
import { AuditLogsComponent } from './components/audit-logs/audit-logs.component';
import { esAuditLoggingExtensionsResolver } from './resolvers';
import {
  eElasticSearchComponents,
  eElasticSearchRouteNames
} from '@feramor/ng.abp-audit-logging-elastic-search/common';
import { eElasticSearchPolicyNames } from '@feramor/ng.abp-audit-logging-elastic-search/common';

const routes: Routes = [
  {
    path: '',
    component: RouterOutletComponent,
    canActivate: [authGuard, permissionGuard],
    resolve: [esAuditLoggingExtensionsResolver],
    children: [
      {
        path: '',
        component: ReplaceableRouteContainerComponent,
        data: {
          requiredPolicy: eElasticSearchPolicyNames.AuditLogs,
          replaceableComponent: {
            key: eElasticSearchComponents.AuditLogs,
            defaultComponent: AuditLogsComponent,
          } as ReplaceableComponents.RouteData<AuditLogsComponent>,
        },
        title: eElasticSearchRouteNames.ElasticSearchAuditLogging,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ElasticSearchRoutingModule {}
