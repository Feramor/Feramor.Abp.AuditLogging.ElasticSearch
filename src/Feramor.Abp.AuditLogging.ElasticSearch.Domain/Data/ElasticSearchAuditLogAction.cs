using System;
using Volo.Abp.AuditLogging;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Data;

public class ElasticSearchAuditLogAction : Entity<Guid>, IMultiTenant, IHasExtraProperties
{
    public Guid? TenantId { get; set; }
    public Guid AuditLogId { get; set; }
    public string ServiceName { get; set; }
    public string MethodName { get; set; }
    public string Parameters { get; set; }
    public DateTime ExecutionTime { get; set; }
    public int ExecutionDuration { get; set; }
    public ExtraPropertyDictionary ExtraProperties { get; set; }
    public ElasticSearchAuditLogAction(Guid id) : base(id)
    {
        ExtraProperties = new ExtraPropertyDictionary();
    }
}