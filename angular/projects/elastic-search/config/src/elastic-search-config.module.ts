import { ModuleWithProviders, NgModule } from '@angular/core';
import { ELASTİC_SEARCH_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class ElasticSearchConfigModule {
  static forRoot(): ModuleWithProviders<ElasticSearchConfigModule> {
    return {
      ngModule: ElasticSearchConfigModule,
      providers: [ELASTİC_SEARCH_ROUTE_PROVIDERS],
    };
  }
}
