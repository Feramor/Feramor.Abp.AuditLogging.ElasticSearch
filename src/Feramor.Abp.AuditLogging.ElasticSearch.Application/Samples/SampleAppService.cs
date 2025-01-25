using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Samples;

public class SampleAppService : ElasticSearchAppService, ISampleAppService
{
    public Task<SampleDto> GetAsync()
    {
        return Task.FromResult(
            new SampleDto
            {
                Value = 42
            }
        );
    }

    [Authorize]
    public Task<SampleDto> GetAuthorizedAsync()
    {
        return Task.FromResult(
            new SampleDto
            {
                Value = 42
            }
        );
    }
}
