import { makeEnvironmentProviders } from '@angular/core';
import { ELASTIC_SEARCH_SETTING_TAB_PROVIDERS } from './setting-tab.provider';
import { ELASTIC_SEARCH_ROUTE_PROVIDERS } from './route.provider';

export function provideElasticSearchConfig() {
  return makeEnvironmentProviders([
    ELASTIC_SEARCH_SETTING_TAB_PROVIDERS,
    ELASTIC_SEARCH_ROUTE_PROVIDERS
  ]);
}
