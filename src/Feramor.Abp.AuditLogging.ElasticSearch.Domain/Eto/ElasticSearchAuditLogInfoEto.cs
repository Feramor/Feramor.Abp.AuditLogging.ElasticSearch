using System;
using System.Linq;
using Feramor.Abp.AuditLogging.ElasticSearch.Data;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.EventBus;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Eto;

[EventName("Feramor.Abp.AuditLogging.ElasticSearch.AuditLogInfo")]
[Serializable]
public class ElasticSearchAuditLogInfoEto : ElasticSearchAuditLog
{
    public ElasticSearchAuditLogInfoEto(AuditLog auditLog) : base(auditLog.Id)
    {
        ApplicationName = auditLog.ApplicationName;
        UserId = auditLog.UserId;
        UserName = auditLog.UserName;
        TenantId = auditLog.TenantId;
        TenantName = auditLog.TenantName;
        ImpersonatorUserId = auditLog.ImpersonatorUserId;
        ImpersonatorUserName = auditLog.ImpersonatorUserName;
        ImpersonatorTenantId = auditLog.ImpersonatorTenantId;
        ImpersonatorTenantName = auditLog.ImpersonatorTenantName;
        ExecutionTime = auditLog.ExecutionTime;
        ExecutionDuration = auditLog.ExecutionDuration;
        ClientIpAddress = auditLog.ClientIpAddress;
        ClientName = auditLog.ClientName;
        ClientId = auditLog.ClientId;
        CorrelationId = auditLog.CorrelationId;
        BrowserInfo = auditLog.BrowserInfo;
        HttpMethod = auditLog.HttpMethod;
        Url = auditLog.Url;
        Exceptions = auditLog.Exceptions;
        Comments = auditLog.Comments;
        HttpStatusCode = auditLog.HttpStatusCode;
        EntityChanges = auditLog.EntityChanges.Select(entityChange => new ElasticSearchEntityChange(entityChange.Id)
        {
            AuditLogId = entityChange.AuditLogId,
            TenantId = entityChange.TenantId,
            ChangeTime = entityChange.ChangeTime,
            ChangeType = entityChange.ChangeType,
            EntityTenantId = entityChange.EntityTenantId,
            EntityId = entityChange.EntityId,
            EntityTypeFullName = entityChange.EntityTypeFullName,
            PropertyChanges = entityChange.PropertyChanges.Select(entityPropertyChange => new ElasticSearchEntityPropertyChange(entityPropertyChange.Id)
            {
                TenantId = entityPropertyChange.TenantId,
                EntityChangeId = entityPropertyChange.EntityChangeId,
                NewValue = entityPropertyChange.NewValue,
                OriginalValue = entityPropertyChange.OriginalValue,
                PropertyName = entityPropertyChange.PropertyName,
                PropertyTypeFullName = entityPropertyChange.PropertyTypeFullName,
            }).ToList(),
            ExtraProperties = entityChange.ExtraProperties
        }).ToList();
        Actions = auditLog.Actions.Select(auditLogAction => new ElasticSearchAuditLogAction(auditLogAction.Id)
        {
            TenantId = auditLogAction.TenantId,
            AuditLogId = auditLogAction.AuditLogId,
            ServiceName = auditLogAction.ServiceName,
            MethodName = auditLogAction.MethodName,
            Parameters = auditLogAction.Parameters,
            ExecutionTime = auditLogAction.ExecutionTime,
            ExecutionDuration = auditLogAction.ExecutionDuration,
            ExtraProperties  = auditLogAction.ExtraProperties
        }).ToList();
    }
}