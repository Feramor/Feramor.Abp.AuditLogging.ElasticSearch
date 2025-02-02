using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Feramor.Abp.AuditLogging.ElasticSearch.AuditLogs;

[Area(ElasticSearchRemoteServiceConsts.ModuleName)]
[RemoteService(Name = ElasticSearchRemoteServiceConsts.RemoteServiceName)]
[Route("api/audit-logs/")]
public class AuditLogsController : ElasticSearchController, IAuditLogsAppService
{
    private readonly IAuditLogsAppService _auditLogsAppService;
    
    public AuditLogsController(IAuditLogsAppService auditLogsAppService)
    {
        _auditLogsAppService = auditLogsAppService;
    }

    [HttpGet]
    public Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogsDto input)
    {
        return _auditLogsAppService.GetListAsync(input);
    }
}