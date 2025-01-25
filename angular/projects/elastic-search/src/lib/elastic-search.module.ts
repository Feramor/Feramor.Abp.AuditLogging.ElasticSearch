import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { ElasticSearchComponent } from './components/elastic-search.component';
import { ElasticSearchRoutingModule } from './elastic-search-routing.module';

@NgModule({
  declarations: [ElasticSearchComponent],
  imports: [CoreModule, ThemeSharedModule, ElasticSearchRoutingModule],
  exports: [ElasticSearchComponent],
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
