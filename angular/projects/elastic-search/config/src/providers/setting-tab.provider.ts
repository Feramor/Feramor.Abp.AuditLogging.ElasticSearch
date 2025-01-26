import { SettingTabsService } from '@abp/ng.setting-management/config';
import { APP_INITIALIZER, inject } from '@angular/core';
import { ConfigStateService } from '@abp/ng.core';
import { ABP } from '@abp/ng.core';
import { eElasticSearchSettingTabNames } from '../enums';
import { ElasticSearchSettingsComponent } from '@feramor/ng.abp-audit-logging-elastic-search';

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
      name: eElasticSearchSettingTabNames.ElasticSearch,
      order: 100,
      requiredPolicy: 'ElasticSearch.SettingManagement',
      component: ElasticSearchSettingsComponent,
    },
  ];
  return async () => {
    settingtabs.add(tabsArray);
  };
}
