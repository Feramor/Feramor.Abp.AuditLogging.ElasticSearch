using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feramor.Abp.AuditLogging.ElasticSearch.Data;
using Feramor.Abp.AuditLogging.ElasticSearch.Managers;
using Feramor.Abp.AuditLogging.ElasticSearch.Permissions;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Data;

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
            input.Sorting,
            input.MaxResultCount,
            input.SkipCount,
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

        return new PagedResultDto<AuditLogDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<ElasticSearchAuditLog>, List<AuditLogDto>>(auditLogs)
        };
    }

    public async Task<AuditLogDto> GetAsync(Guid id)
    {
        var log = await _elasticSearchManager.GetAsync(id.ToString());
        var logDto = ObjectMapper.Map<ElasticSearchAuditLog, AuditLogDto>(log);

        if (logDto.Exceptions.IsNullOrEmpty())
        {
            logDto.Exceptions = "[]";
        }
        
        var parsedJson = JsonConvert.DeserializeObject(logDto.Exceptions);
        logDto.Exceptions = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        
        foreach (var action in logDto.Actions)
        {
            if (action.Parameters.IsNullOrEmpty())
            {
                action.Parameters = "{}";
            }

            parsedJson = JsonConvert.DeserializeObject(action.Parameters);
            action.Parameters = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        return logDto;
    }
}