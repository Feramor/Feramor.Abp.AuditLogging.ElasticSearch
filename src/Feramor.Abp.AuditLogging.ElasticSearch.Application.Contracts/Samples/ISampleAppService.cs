using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
