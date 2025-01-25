using System.Threading.Tasks;
using Feramor.Abp.AuditLogging.ElasticSearch.Eto;
using Feramor.Abp.AuditLogging.ElasticSearch.Managers;
using Feramor.Abp.AuditLogging.ElasticSearch.Settings;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Feramor.Abp.AuditLogging.ElasticSearch.EventHandlers;

public class ElasticSearchAuditLogInfoEventHandler : IDistributedEventHandler<ElasticSearchAuditLogInfoEto>, ITransientDependency
{
    private readonly IElasticSearchManager _elasticSearchManager;
    private protected ElasticSearchAuditLogSettings ElasticSearchAuditLogSettings { get; init; }

    public ElasticSearchAuditLogInfoEventHandler(IOptions<ElasticSearchAuditLogSettings> elasticSearchAuditLogSettings, IElasticSearchManager elasticSearchManager)
    {
        ElasticSearchAuditLogSettings = elasticSearchAuditLogSettings.Value;
        _elasticSearchManager = elasticSearchManager;
    }
    
    public Task HandleEventAsync(ElasticSearchAuditLogInfoEto eventData)
    {
        throw new System.NotImplementedException();
    }
}