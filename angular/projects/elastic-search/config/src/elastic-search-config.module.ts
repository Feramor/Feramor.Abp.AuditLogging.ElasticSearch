import { ModuleWithProviders, NgModule } from '@angular/core';
import { provideElasticSearchConfig } from './providers';

@NgModule(

)
export class ElasticSearchConfigModule {
  static forRoot(): ModuleWithProviders<ElasticSearchConfigModule> {
    return {
      ngModule: ElasticSearchConfigModule,
      providers: [provideElasticSearchConfig()],
    };
  }
}
