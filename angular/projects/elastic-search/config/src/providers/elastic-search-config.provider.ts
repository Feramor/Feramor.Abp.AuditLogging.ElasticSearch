import { makeEnvironmentProviders } from '@angular/core';
import { ELASTIC_SEARCH_SETTING_TAB_PROVIDERS } from './setting-tab.provider';

export function provideElasticSearchConfig() {
  return makeEnvironmentProviders([ELASTIC_SEARCH_SETTING_TAB_PROVIDERS]);
}
