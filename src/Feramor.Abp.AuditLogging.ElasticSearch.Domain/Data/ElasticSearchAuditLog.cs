using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Data;

public class ElasticSearchAuditLog : AggregateRoot<Guid>, IMultiTenant
{
    public string ApplicationName { get; set; }
    public Guid? UserId { get; set; }
    public string UserName { get; set; }
    public Guid? TenantId { get; set; }
    public string TenantName { get; set; }
    public Guid? ImpersonatorUserId { get; set; }
    public string ImpersonatorUserName { get; set; }
    public Guid? ImpersonatorTenantId { get; set; }
    public string ImpersonatorTenantName { get; set; }
    public DateTime ExecutionTime { get; set; }
    public int ExecutionDuration { get; set; }
    public string ClientIpAddress { get; set; }
    public string ClientName { get; set; }
    public string ClientId { get; set; }
    public string CorrelationId { get; set; }
    public string BrowserInfo { get; set; }
    public string HttpMethod { get; set; }
    public string Url { get; set; }
    public string Exceptions { get; set; }
    public string Comments { get; set; }
    public int? HttpStatusCode { get; set; }
    public List<ElasticSearchEntityChange> EntityChanges { get; set; }
    public List<ElasticSearchAuditLogAction> Actions { get; set; }
    public ElasticSearchAuditLog(Guid id) : base(id)
    {
        EntityChanges = new List<ElasticSearchEntityChange>();
        Actions = new List<ElasticSearchAuditLogAction>();
    }
    public AuditLog ToAuditLog(IGuidGenerator guidGenerator)
    {
        return new AuditLog(
            Id,
            ApplicationName,
            TenantId,
            TenantName,
            UserId,
            UserName,
            ExecutionTime,
            ExecutionDuration,
            ClientIpAddress,
            ClientName,
            ClientId,
            CorrelationId,
            BrowserInfo,
            HttpMethod,
            Url,
            HttpStatusCode,
            ImpersonatorUserId,
            ImpersonatorUserName,
            ImpersonatorTenantId,
            ImpersonatorTenantName,
            ExtraProperties,
            EntityChanges.Select(entityChange => new EntityChange(guidGenerator,Id,new EntityChangeInfo
            {
                ChangeTime = entityChange.ChangeTime,
                ChangeType = entityChange.ChangeType,
                EntityTenantId = entityChange.EntityTenantId,
                EntityId = entityChange.EntityId,
                EntityTypeFullName = entityChange.EntityTypeFullName,
                PropertyChanges = entityChange.PropertyChanges.Select(propertyChange => new EntityPropertyChangeInfo
                {
                    NewValue = propertyChange.NewValue,
                    OriginalValue = propertyChange.OriginalValue,
                    PropertyName = propertyChange.PropertyName,
                    PropertyTypeFullName = propertyChange.PropertyTypeFullName
                }).ToList()
            },entityChange.TenantId)).ToList(),
            Actions.Select(action => new AuditLogAction(action.Id,Id,new AuditLogActionInfo
            {
                ServiceName = action.ServiceName,
                MethodName = action.MethodName,
                Parameters = action.Parameters,
                ExecutionTime = action.ExecutionTime,
                ExecutionDuration = action.ExecutionDuration
            },action.TenantId)).ToList(),
            Exceptions,
            Comments);
    }
}