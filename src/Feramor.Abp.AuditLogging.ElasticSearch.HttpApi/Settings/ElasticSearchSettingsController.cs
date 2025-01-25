using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Settings;

[Area(ElasticSearchRemoteServiceConsts.ModuleName)]
[RemoteService(Name = ElasticSearchRemoteServiceConsts.RemoteServiceName)]
[Route("api/setting-management/")]
public class ElasticSearchSettingsController : ElasticSearchController, IElasticSearchAuditLogSettingsAppService
{
    private readonly IElasticSearchAuditLogSettingsAppService _elasticSearchAuditLogSettingsAppService;

    public ElasticSearchSettingsController(IElasticSearchAuditLogSettingsAppService elasticSearchAuditLogSettingsAppService)
    {
        _elasticSearchAuditLogSettingsAppService = elasticSearchAuditLogSettingsAppService;
    }

    [HttpGet]
    [Route("elastic-search")]
    public Task<ElasticSearchAuditLogSettingsDto> GetAsync()
    {
        return _elasticSearchAuditLogSettingsAppService.GetAsync();
    }

    [HttpPost]
    [Route("elastic-search")]
    public Task UpdateAsync(UpdateElasticSearchAuditLogSettingsDto input)
    {
        return _elasticSearchAuditLogSettingsAppService.UpdateAsync(input);
    }

    [HttpPost]
    [Route("elastic-search/test-connection")]
    public Task<bool> TestConnectionAsync()
    {
        return _elasticSearchAuditLogSettingsAppService.TestConnectionAsync();
    }
}