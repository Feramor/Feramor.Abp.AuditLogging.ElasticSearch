using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Data;

public class ElasticSearchEntityChange : Entity<Guid>, IMultiTenant, IHasExtraProperties
{
    public Guid AuditLogId { get; set; }
    public Guid? TenantId { get; set; }
    public DateTime ChangeTime { get; set; }
    public EntityChangeType ChangeType { get; set; }
    public Guid? EntityTenantId { get; set; }
    public string EntityId { get; set; }
    public string EntityTypeFullName { get; set; }
    public List<ElasticSearchEntityPropertyChange> PropertyChanges { get; set; }
    public ExtraPropertyDictionary ExtraProperties { get; set; }
    public ElasticSearchEntityChange(Guid id) : base(id)
    {
        ExtraProperties = new ExtraPropertyDictionary();
        PropertyChanges = new List<ElasticSearchEntityPropertyChange>();
    }
}