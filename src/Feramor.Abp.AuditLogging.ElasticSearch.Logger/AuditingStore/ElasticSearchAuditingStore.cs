using System;
using System.Threading.Tasks;
using Feramor.Abp.AuditLogging.ElasticSearch.Eto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;

namespace Feramor.Abp.AuditLogging.ElasticSearch.AuditingStore;

public class ElasticSearchAuditingStore : IAuditingStore, ITransientDependency
{
    public ILogger<ElasticSearchAuditingStore> Logger { get; set; }
    private readonly AbpAuditingOptions _options;
    private readonly IDistributedEventBus _distributedEventBus;
    private readonly IAuditLogInfoToAuditLogConverter _auditLogInfoToAuditLogConverter;
    private readonly IGuidGenerator _guidGenerator;
    public ElasticSearchAuditingStore(IOptions<AbpAuditingOptions> options, IDistributedEventBus distributedEventBus, IAuditLogInfoToAuditLogConverter auditLogInfoToAuditLogConverter, IGuidGenerator guidGenerator)
    {
        _distributedEventBus = distributedEventBus;
        _auditLogInfoToAuditLogConverter = auditLogInfoToAuditLogConverter;
        _guidGenerator = guidGenerator;
        _options = options.Value;
    }
    
    public async Task SaveAsync(AuditLogInfo auditInfo)
    {
        if (!_options.HideErrors)
        {
            await SaveLogAsync(auditInfo);
            return;
        }
        
        try
        {
            await SaveLogAsync(auditInfo);
        }
        catch (Exception ex)
        {
            Logger.LogWarning("Could not save the audit log object: " + Environment.NewLine + auditInfo.ToString());
            Logger.LogException(ex, LogLevel.Error);
        }
    }
    
    protected virtual async Task SaveLogAsync(AuditLogInfo auditInfo)
    {
        await _distributedEventBus.PublishAsync(new ElasticSearchAuditLogInfoEto(await _auditLogInfoToAuditLogConverter.ConvertAsync(auditInfo)),false);
    }
}