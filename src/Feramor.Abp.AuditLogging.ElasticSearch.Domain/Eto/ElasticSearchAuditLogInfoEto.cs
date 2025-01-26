using System;
using System.Linq;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.EventBus;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Eto;

[EventName("Feramor.Abp.AuditLogging.ElasticSearch.AuditLogInfo")]
[Serializable]
public class ElasticSearchAuditLogInfoEto : AuditLog
{
    public ElasticSearchAuditLogInfoEto(AuditLog auditLog) : base(
        auditLog.Id,
        auditLog.ApplicationName,
        auditLog.TenantId,
        auditLog.TenantName,
        auditLog.UserId,
        auditLog.UserName,
        auditLog.ExecutionTime,
        auditLog.ExecutionDuration,
        auditLog.ClientIpAddress,
        auditLog.ClientName,
        auditLog.ClientId,
        auditLog.CorrelationId,
        auditLog.BrowserInfo,
        auditLog.HttpMethod,
        auditLog.Url,
        auditLog.HttpStatusCode,
        auditLog.ImpersonatorUserId,
        auditLog.ImpersonatorUserName,
        auditLog.ImpersonatorTenantId,
        auditLog.ImpersonatorTenantName,
        auditLog.ExtraProperties,
        auditLog.EntityChanges.ToList(),
        auditLog.Actions.ToList(),
        auditLog.Exceptions,
        auditLog.Comments)
    {
    }
}