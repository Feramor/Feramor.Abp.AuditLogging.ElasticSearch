import { ModuleWithProviders, NgModule } from '@angular/core';
import { provideElasticSearchConfig } from './public-api';

@NgModule()
export class ElasticSearchConfigModule {
  static forRoot(): ModuleWithProviders<ElasticSearchConfigModule> {
    return {
      ngModule: ElasticSearchConfigModule,
      providers: [provideElasticSearchConfig()],
    };
  }
}
