using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Feramor.Abp.AuditLogging.ElasticSearch.AuditLogs;

public interface IAuditLogsAppService : IApplicationService
{
    Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogsDto input);
}