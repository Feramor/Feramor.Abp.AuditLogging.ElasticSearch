import type { EntityDto, ExtensibleEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { EntityChangeType } from '../volo/abp/auditing/entity-change-type.enum';

export interface AuditLogActionDto extends ExtensibleEntityDto<string> {
  tenantId?: string;
  auditLogId?: string;
  serviceName?: string;
  methodName?: string;
  parameters?: string;
  executionTime?: string;
  executionDuration: number;
}

export interface AuditLogDto extends ExtensibleEntityDto<string> {
  userId?: string;
  userName?: string;
  tenantId?: string;
  tenantName?: string;
  impersonatorUserId?: string;
  impersonatorUserName?: string;
  impersonatorTenantId?: string;
  impersonatorTenantName?: string;
  executionTime?: string;
  executionDuration: number;
  clientIpAddress?: string;
  clientId?: string;
  clientName?: string;
  browserInfo?: string;
  httpMethod?: string;
  url?: string;
  exceptions?: string;
  comments?: string;
  httpStatusCode?: number;
  applicationName?: string;
  correlationId?: string;
  entityChanges: EntityChangeDto[];
  actions: AuditLogActionDto[];
}

export interface EntityChangeDto extends ExtensibleEntityDto<string> {
  auditLogId?: string;
  tenantId?: string;
  changeTime?: string;
  changeType: EntityChangeType;
  entityId?: string;
  entityTypeFullName?: string;
  propertyChanges: EntityPropertyChangeDto[];
}

export interface EntityPropertyChangeDto extends EntityDto<string> {
  tenantId?: string;
  entityChangeId?: string;
  newValue?: string;
  originalValue?: string;
  propertyName?: string;
  propertyTypeFullName?: string;
}

export interface GetAuditLogsDto extends PagedAndSortedResultRequestDto {
  startTime?: string;
  endTime?: string;
  url?: string;
  clientId?: string;
  userName?: string;
  applicationName?: string;
  clientIpAddress?: string;
  correlationId?: string;
  httpMethod?: string;
  httpStatusCode?: number;
  maxExecutionDuration?: number;
  minExecutionDuration?: number;
  hasException?: any;
}
