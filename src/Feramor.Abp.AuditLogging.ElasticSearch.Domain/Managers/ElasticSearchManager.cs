using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Feramor.Abp.AuditLogging.ElasticSearch.Eto;
using Feramor.Abp.AuditLogging.ElasticSearch.Settings;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Services;
using Volo.Abp.Timing;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Managers;

public class ElasticSearchManager : DomainService, IElasticSearchManager
{
    private protected ElasticSearchAuditLogSettings ElasticSearchAuditLogSettings { get; init; }
    private protected ElasticsearchClient ElasticsearchClient { get; init; }
    private readonly IClock _clock;

    public ElasticSearchManager(IOptions<ElasticSearchAuditLogSettings> elasticSearchAuditLogSettings, IClock clock)
    {
        _clock = clock;
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

    private async Task CreateIndexIfNotExistsAsync()
    {
        var index = $"{ElasticSearchAuditLogSettings.Index}-{_clock.Now:yyyy-MM-dd}";
        var response = await ElasticsearchClient.Indices.CreateAsync<ElasticSearchAuditLogInfoEto>(index, c =>
        {
        });
        if (!response.IsValidResponse)
        {
            throw new BusinessException(ElasticSearchErrorCodes.CreateIndexFailed, response.ApiCallDetails.OriginalException?.Message);
        }
    }
}