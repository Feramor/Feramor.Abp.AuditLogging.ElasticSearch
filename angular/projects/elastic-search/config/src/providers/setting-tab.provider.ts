import { SettingTabsService } from '@abp/ng.setting-management/config';
import { APP_INITIALIZER, inject } from '@angular/core';
import { ConfigStateService } from '@abp/ng.core';
import { ABP } from '@abp/ng.core';
import { ElasticSearchSettingsComponent } from '../components';
import {
  eElasticSearchPolicyNames,
  eElasticSearchSettingTabNames
} from '@feramor/ng.abp-audit-logging-elastic-search/common';

export const ELASTIC_SEARCH_SETTING_TAB_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureSettingTabs,
    deps: [SettingTabsService],
    multi: true,
  },
];

export function configureSettingTabs(settingTabs: SettingTabsService) {
  const tabsArray: ABP.Tab[] = [
    {
      name: eElasticSearchSettingTabNames.ElasticSearch,
      requiredPolicy: eElasticSearchPolicyNames.SettingManagement,
      component: ElasticSearchSettingsComponent,
    },
  ];
  return async () => {
    settingTabs.add(tabsArray);
  };
}
