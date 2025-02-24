using System;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Data;

public class ElasticSearchEntityPropertyChange : Entity<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }
    public Guid EntityChangeId { get; set; }
    public string NewValue { get; set; }
    public string OriginalValue { get; set; }
    public string PropertyName { get; set; }
    public string PropertyTypeFullName { get; set; }
    public ElasticSearchEntityPropertyChange(Guid id) : base(id)
    {
    }
}