using System;
using System.Threading.Tasks;
using Feramor.Abp.AuditLogging.ElasticSearch.Eto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Feramor.Abp.AuditLogging.ElasticSearch.AuditingStore;

public class ElasticSearchAuditingStore : IAuditingStore, ITransientDependency
{
    public ILogger<ElasticSearchAuditingStore> Logger { get; set; }
    private readonly AbpAuditingOptions _options;
    private readonly IDistributedEventBus _distributedEventBus;

    public ElasticSearchAuditingStore(IOptions<AbpAuditingOptions> options, IDistributedEventBus distributedEventBus)
    {
        _distributedEventBus = distributedEventBus;
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
        await _distributedEventBus.PublishAsync(new ElasticSearchAuditLogInfoEto
        {
            ApplicationName = auditInfo.ApplicationName,
            UserId = auditInfo.UserId,
            UserName = auditInfo.UserName,
            TenantId = auditInfo.TenantId,
            TenantName = auditInfo.TenantName,
            ImpersonatorUserId = auditInfo.ImpersonatorUserId,
            ImpersonatorTenantId = auditInfo.ImpersonatorTenantId,
            ImpersonatorUserName = auditInfo.ImpersonatorUserName,
            ImpersonatorTenantName = auditInfo.ImpersonatorTenantName,
            ExecutionTime = auditInfo.ExecutionTime,
            ExecutionDuration = auditInfo.ExecutionDuration,
            ClientId = auditInfo.ClientId,
            CorrelationId = auditInfo.CorrelationId,
            ClientIpAddress = auditInfo.ClientIpAddress,
            ClientName = auditInfo.ClientName,
            BrowserInfo = auditInfo.BrowserInfo,
            HttpMethod = auditInfo.HttpMethod,
            HttpStatusCode = auditInfo.HttpStatusCode,
            Url = auditInfo.Url,
            Actions = auditInfo.Actions,
            Comments = auditInfo.Comments
        });
    }
}