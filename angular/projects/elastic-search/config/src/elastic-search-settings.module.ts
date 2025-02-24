import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { ElasticSearchSettingsComponent } from './components';

const components = [
  ElasticSearchSettingsComponent
];

@NgModule({
  imports: [CoreModule, ThemeSharedModule, NgxValidateCoreModule, NgSelectModule, NgbNavModule],
  exports: [...components],
  declarations: [...components],
})
export class ElasticSearchSettingsModule { }
