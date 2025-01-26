import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ElasticSearchSettingsComponent } from './components/elastic-search-settings/elastic-search-settings.component';
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { NgSelectModule } from '@ng-select/ng-select';

const components = [
  ElasticSearchSettingsComponent
];

@NgModule({
  imports: [CoreModule, ThemeSharedModule, NgxValidateCoreModule, NgSelectModule],
  exports: [...components],
  declarations: [...components],
})
export class ElasticSearchSettingsModule { }
