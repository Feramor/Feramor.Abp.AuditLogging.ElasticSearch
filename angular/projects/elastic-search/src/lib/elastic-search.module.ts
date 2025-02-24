import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { LazyModuleFactory } from '@abp/ng.core';
import { ElasticSearchAuditLogsModule } from './elastic-search-audit-logs.module';
import { ElasticSearchRoutingModule } from './elastic-search-routing.module';

@NgModule({
  declarations: [],
  imports: [ElasticSearchAuditLogsModule, ElasticSearchRoutingModule],
  exports: [ElasticSearchAuditLogsModule, ElasticSearchRoutingModule],
})
export class ElasticSearchModule {
  static forChild(): ModuleWithProviders<ElasticSearchModule> {
    return {
      ngModule: ElasticSearchModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<ElasticSearchModule> {
    return new LazyModuleFactory(ElasticSearchModule.forChild());
  }
}
