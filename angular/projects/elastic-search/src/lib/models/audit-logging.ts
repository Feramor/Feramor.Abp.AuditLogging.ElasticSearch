import { PagedResultDto } from '@abp/ng.core';
import { AuditLogDto } from '@feramor/ng.abp-audit-logging-elastic-search/proxy';

export namespace ElasticSearch {
  export interface State {
    result: PagedResultDto<AuditLogDto>;
    averageExecutionStatistics: Record<string, number>;
    errorRateStatistics: Record<string, number>;
  }
}
