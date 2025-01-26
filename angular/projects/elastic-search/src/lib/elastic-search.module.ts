import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { ElasticSearchRoutingModule } from './elastic-search-routing.module';
import { ElasticSearchSettingsModule } from './elastic-search-settings.module';

@NgModule({
  declarations: [],
  imports: [CoreModule, ThemeSharedModule, ElasticSearchRoutingModule, ElasticSearchSettingsModule],
  exports: [ElasticSearchSettingsModule],
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
