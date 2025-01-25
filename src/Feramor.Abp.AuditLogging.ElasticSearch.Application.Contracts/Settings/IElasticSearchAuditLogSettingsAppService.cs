using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Settings;

public interface IElasticSearchAuditLogSettingsAppService : IApplicationService
{
    Task<ElasticSearchAuditLogSettingsDto> GetAsync();
    
    Task UpdateAsync(UpdateElasticSearchAuditLogSettingsDto input);
}