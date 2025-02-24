import { EntityAction } from '@abp/ng.components/extensible';
import { SHOW_ENTITY_DETAILS } from '../tokens/entity-details.token';
import { EntityChangeDto } from '@feramor/ng.abp-audit-logging-elastic-search/proxy';
import { SHOW_ENTITY_HISTORY } from '../tokens/entity-history.token';

export const DEFAULT_ES_ENTITY_CHANGES_ENTITY_ACTIONS = EntityAction.createMany<EntityChangeDto>([
  {
    text: 'ElasticSearch::ChangeDetails',
    action: data => {
      const showDetails = data.getInjected(SHOW_ENTITY_DETAILS);
      showDetails(data.record.id);
    },
    icon: 'fa fa-search',
  },
  {
    text: 'ElasticSearch::FullChangeHistory',
    action: data => {
      const showHistory = data.getInjected(SHOW_ENTITY_HISTORY);
      showHistory(data.record.entityId, data.record.entityTypeFullName);
    },
    icon: 'fa fa-history',
  },
]);
