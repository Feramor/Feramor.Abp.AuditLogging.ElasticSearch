using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Samples;

[Area(ElasticSearchRemoteServiceConsts.ModuleName)]
[RemoteService(Name = ElasticSearchRemoteServiceConsts.RemoteServiceName)]
[Route("api/ElasticSearch/sample")]
public class SampleController : ElasticSearchController, ISampleAppService
{
    private readonly ISampleAppService _sampleAppService;

    public SampleController(ISampleAppService sampleAppService)
    {
        _sampleAppService = sampleAppService;
    }

    [HttpGet]
    public async Task<SampleDto> GetAsync()
    {
        return await _sampleAppService.GetAsync();
    }

    [HttpGet]
    [Route("authorized")]
    [Authorize]
    public async Task<SampleDto> GetAuthorizedAsync()
    {
        return await _sampleAppService.GetAsync();
    }
}
