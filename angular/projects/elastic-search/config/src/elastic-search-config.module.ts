import { ModuleWithProviders, NgModule } from '@angular/core';
import { provideElasticSearchConfig } from './providers/elastic-search-config.provider';

@NgModule()
export class ElasticSearchConfigModule {
  static forRoot(): ModuleWithProviders<ElasticSearchConfigModule> {
    return {
      ngModule: ElasticSearchConfigModule,
      providers: [provideElasticSearchConfig()],
    };
  }
}
