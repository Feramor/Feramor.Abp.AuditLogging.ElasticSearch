import { makeEnvironmentProviders } from '@angular/core';
import { ELASTIC_SEARCH_SETTING_TAB_PROVIDERS } from '@feramor.Abp.Audit-logging/elastic-search/config';

export function provideElasticSearchConfig() {
  return makeEnvironmentProviders([ELASTIC_SEARCH_SETTING_TAB_PROVIDERS]);
}
