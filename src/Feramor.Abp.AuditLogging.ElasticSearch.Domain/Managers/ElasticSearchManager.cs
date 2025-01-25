using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Feramor.Abp.AuditLogging.ElasticSearch.Settings;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Services;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Managers;

public class ElasticSearchManager : DomainService, IElasticSearchManager
{
    private protected ElasticSearchAuditLogSettings ElasticSearchAuditLogSettings { get; init; }
    private protected ElasticsearchClient ElasticsearchClient { get; init; }

    public ElasticSearchManager(IOptions<ElasticSearchAuditLogSettings> elasticSearchAuditLogSettings)
    {
        ElasticSearchAuditLogSettings = elasticSearchAuditLogSettings.Value;
        ElasticsearchClient = new ElasticsearchClient(ElasticSearchAuditLogSettings.GetElasticsearchClientSettings());
    }

    public async Task<bool> TestConnectionAsync()
    {
        var ping = await ElasticsearchClient.PingAsync();
        if (!ping.IsValidResponse)
        {
            throw new BusinessException(ElasticSearchErrorCodes.TestFailed, ping.ApiCallDetails.OriginalException?.Message);
        }
        return true;
    }
}