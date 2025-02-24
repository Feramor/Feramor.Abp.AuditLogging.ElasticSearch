using System;
using System.Linq;
using System.Threading.Tasks;
using Feramor.Abp.AuditLogging.ElasticSearch.Eto;
using Feramor.Abp.AuditLogging.ElasticSearch.Managers;
using Feramor.Abp.AuditLogging.ElasticSearch.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;

namespace Feramor.Abp.AuditLogging.ElasticSearch.EventHandlers;

public class ElasticSearchAuditLogInfoEventHandler : IDistributedEventHandler<ElasticSearchAuditLogInfoEto>, ITransientDependency
{
    private readonly IElasticSearchManager _elasticSearchManager;
    private protected ElasticSearchAuditLogSettings ElasticSearchAuditLogSettings { get; init; }
    private protected AbpAuditingOptions AbpAuditingOptions { get; init; }
    public ILogger<ElasticSearchAuditLogInfoEventHandler> Logger { get; init; }
    
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly IGuidGenerator _guidGenerator;
    public ElasticSearchAuditLogInfoEventHandler(IOptions<ElasticSearchAuditLogSettings> elasticSearchAuditLogSettings, IOptions<AbpAuditingOptions> abpAuditingOptions,IElasticSearchManager elasticSearchManager, IAuditLogRepository auditLogRepository, IGuidGenerator guidGenerator)
    {
        _elasticSearchManager = elasticSearchManager;
        _auditLogRepository = auditLogRepository;
        _guidGenerator = guidGenerator;

        ElasticSearchAuditLogSettings = elasticSearchAuditLogSettings.Value;
        AbpAuditingOptions = abpAuditingOptions.Value;
        Logger = NullLogger<ElasticSearchAuditLogInfoEventHandler>.Instance;
    }
    
    public async Task HandleEventAsync(ElasticSearchAuditLogInfoEto eventData)
    {
        if (!ElasticSearchAuditLogSettings.IsActive)
        {
            if (!AbpAuditingOptions.HideErrors)
            {
                await _auditLogRepository.InsertAsync(eventData.ToAuditLog(_guidGenerator));
                return;
            }
            try
            {
                await _auditLogRepository.InsertAsync(eventData.ToAuditLog(_guidGenerator));
            }
            catch (Exception ex)
            {
                Logger.LogWarning("Could not save the audit log object: " + Environment.NewLine + eventData.ToString());
                Logger.LogException(ex, LogLevel.Error);
            }
            return;
        }
        if (!AbpAuditingOptions.HideErrors)
        {
            await _elasticSearchManager.SaveLogAsync(eventData);
            return;
        }
        
        try
        {
            await _elasticSearchManager.SaveLogAsync(eventData);
        }
        catch (Exception ex)
        {
            Logger.LogWarning("Could not save the audit log object: " + Environment.NewLine + eventData.ToString());
            Logger.LogException(ex, LogLevel.Error);
        }
    }
}