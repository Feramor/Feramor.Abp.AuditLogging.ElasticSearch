import { ModuleWithProviders, NgModule, NgModuleFactory } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgbDatepickerModule, NgbNavModule, NgbTimepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { AuditLogsComponent } from './components/audit-logs/audit-logs.component';
import { EsAuditLoggingConfigOptions } from './models';
import {
  ES_AUDIT_LOGGING_ENTITY_ACTION_CONTRIBUTORS,
  ES_AUDIT_LOGGING_ENTITY_PROP_CONTRIBUTORS,
  ES_AUDIT_LOGGING_TOOLBAR_ACTION_CONTRIBUTORS
} from './tokens';
import { PageModule } from '@abp/ng.components/page';
import { ExtensibleModule } from '@abp/ng.components/extensible';
const components = [
  AuditLogsComponent
];

@NgModule({
  imports: [CoreModule, ThemeSharedModule, NgxValidateCoreModule, NgbDatepickerModule, NgSelectModule, NgbNavModule, PageModule, ExtensibleModule],
  exports: [...components],
  declarations: [...components],
  providers: []
})
export class ElasticSearchAuditLogsModule {
  static forChild(
    options: EsAuditLoggingConfigOptions = {},
  ): ModuleWithProviders<ElasticSearchAuditLogsModule> {
    return {
      ngModule: ElasticSearchAuditLogsModule,
      providers: [
        {
          provide: ES_AUDIT_LOGGING_ENTITY_ACTION_CONTRIBUTORS,
          useValue: options.entityActionContributors,
        },
        {
          provide: ES_AUDIT_LOGGING_TOOLBAR_ACTION_CONTRIBUTORS,
          useValue: options.toolbarActionContributors,
        },
        {
          provide: ES_AUDIT_LOGGING_ENTITY_PROP_CONTRIBUTORS,
          useValue: options.entityPropContributors,
        },
      ],
    };
  }

  static forLazy(options: EsAuditLoggingConfigOptions = {}): NgModuleFactory<ElasticSearchAuditLogsModule> {
    return new LazyModuleFactory(ElasticSearchAuditLogsModule.forChild(options));
  }
}
