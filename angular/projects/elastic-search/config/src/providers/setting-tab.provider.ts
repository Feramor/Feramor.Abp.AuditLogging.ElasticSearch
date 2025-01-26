import { SettingTabsService } from '@abp/ng.setting-management/config';
import { APP_INITIALIZER, inject } from '@angular/core';
import { eAccountSettingTabNames } from '../enums/setting-tab-names';
import { ConfigStateService } from '@abp/ng.core';
import { ABP } from '@abp/ng.core';
import { filter, firstValueFrom } from 'rxjs';
import {
  ElasticSearchSettingsComponent
} from '../../../src/lib/components/elastic-search-settings/elastic-search-settings.component';

export const ELASTIC_SEARCH_SETTING_TAB_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureSettingTabs,
    deps: [SettingTabsService],
    multi: true,
  },
];

export function configureSettingTabs(settingtabs: SettingTabsService) {
  const configState = inject(ConfigStateService);
  const tabsArray: ABP.Tab[] = [
    {
      name: eAccountSettingTabNames.ElasticSearch,
      order: 100,
      requiredPolicy: 'ElasticSearch.SettingManagement',
      component: ElasticSearchSettingsComponent,
    },
  ];
  return async () => {
    settingtabs.add(tabsArray);
  };
}
