using System.Collections.Generic;
using System.Threading.Tasks;
using Feramor.Abp.AuditLogging.ElasticSearch.Data;
using Feramor.Abp.AuditLogging.ElasticSearch.Managers;
using Feramor.Abp.AuditLogging.ElasticSearch.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;

namespace Feramor.Abp.AuditLogging.ElasticSearch.AuditLogs;

[Authorize(ElasticSearchPermissions.AuditLogs.SubGroup)]
[DisableAuditing]
public class AuditLogsAppService : ElasticSearchAppService, IAuditLogsAppService
{
    private readonly IElasticSearchManager _elasticSearchManager;
    
    public AuditLogsAppService(IElasticSearchManager elasticSearchManager)
    {
        _elasticSearchManager = elasticSearchManager;
    }

    public async Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogsDto input)
    {
        var auditLogs = await _elasticSearchManager.GetListAsync(
            sorting: input.Sorting,
            maxResultCount: input.MaxResultCount,
            skipCount: input.SkipCount,
            httpMethod: input.HttpMethod,
            httpStatusCode: input.HttpStatusCode,
            url: input.Url,
            clientId: input.ClientId,
            userName: input.UserName,
            applicationName: input.ApplicationName,
            clientIpAddress: input.ClientIpAddress,
            correlationId: input.CorrelationId,
            maxExecutionDuration: input.MaxExecutionDuration,
            minExecutionDuration: input.MinExecutionDuration,
            hasException: input.HasException,
            startTime: input.StartTime,
            endTime: input.EndTime,
            includeDetails: false
        );
        
        var totalCount = await _elasticSearchManager.GetCountAsync(
            httpMethod: input.HttpMethod,
            httpStatusCode: input.HttpStatusCode,
            url: input.Url,
            clientId: input.ClientId,
            userName: input.UserName,
            applicationName: input.ApplicationName,
            clientIpAddress: input.ClientIpAddress,
            correlationId: input.CorrelationId,
            maxExecutionDuration: input.MaxExecutionDuration,
            minExecutionDuration: input.MinExecutionDuration,
            hasException: input.HasException,
            startTime: input.StartTime,
            endTime: input.EndTime
        );
        
        return new PagedResultDto<AuditLogDto>()
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<ElasticSearchAuditLog>, List<AuditLogDto>>(auditLogs)
        };
    }
}