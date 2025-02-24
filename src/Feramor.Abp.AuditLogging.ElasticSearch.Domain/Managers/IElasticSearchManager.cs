using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Feramor.Abp.AuditLogging.ElasticSearch.Data;
using Volo.Abp.AuditLogging;
using Volo.Abp.DependencyInjection;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Managers;

public interface IElasticSearchManager: ITransientDependency
{
    public Task<bool> TestConnectionAsync();
    public Task<bool> SaveLogAsync(object logInfo);
    public Task<List<string>> GetIndicesAsync();
    public Task DeleteIndicesAsync(List<string> indices);
    public Task<List<ElasticSearchAuditLog?>?> GetListAsync(
        string? sorting = null,
        int maxResultCount = 50,
        int skipCount = 0,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? httpMethod = null,
        string? url = null,
        string? clientId = null,
        Guid? userId = null,
        string? userName = null,
        string? applicationName = null,
        string? clientIpAddress = null,
        string? correlationId = null,
        int? maxExecutionDuration = null,
        int? minExecutionDuration = null,
        bool? hasException = null,
        HttpStatusCode? httpStatusCode = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);
    public Task<long> GetCountAsync(
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? httpMethod = null,
        string? url = null,
        string? clientId = null,
        Guid? userId = null,
        string? userName = null,
        string? applicationName = null,
        string? clientIpAddress = null,
        string? correlationId = null,
        int? maxExecutionDuration = null,
        int? minExecutionDuration = null,
        bool? hasException = null,
        HttpStatusCode? httpStatusCode = null,
        CancellationToken cancellationToken = default);
    public Task<ElasticSearchAuditLog?> GetAsync(
        string id,
        CancellationToken cancellationToken = default);
}