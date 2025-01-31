using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using Feramor.Abp.AuditLogging.ElasticSearch.Eto;
using Feramor.Abp.AuditLogging.ElasticSearch.Settings;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp.Timing;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Managers;

public class ElasticSearchManager : DomainService, IElasticSearchManager
{
    private protected ElasticSearchAuditLogSettings ElasticSearchAuditLogSettings { get; init; }
    private protected ElasticsearchClient ElasticsearchClient { get; init; }
    private readonly IClock _clock;
    
    public ElasticSearchManager(IOptions<ElasticSearchAuditLogSettings> elasticSearchAuditLogSettings, IClock clock)
    {
        _clock = clock;
        ElasticSearchAuditLogSettings = elasticSearchAuditLogSettings.Value;
        ElasticsearchClient = new ElasticsearchClient(ElasticSearchAuditLogSettings.GetElasticsearchClientSettings());
    }

    public async Task<bool> TestConnectionAsync()
    {
        var ping = await ElasticsearchClient.PingAsync();
        if (!ping.IsValidResponse)
        {
            throw new BusinessException(ElasticSearchErrorCodes.TestFailed, ping.ApiCallDetails.OriginalException?.Message);
        }
        await CreateIndexIfNotExistsAsync($"{ElasticSearchAuditLogSettings.Index}-{_clock.Now:yyyy-MM-dd}");
        return true;
    }
    
    private async Task CreateIndexIfNotExistsAsync(string index)
    {
        var isExist = await ElasticsearchClient.Indices.GetAsync<AuditLog>(index);
        if (isExist.IsValidResponse && isExist.Indices.Any())
        {
            return;
        }
        var response = await ElasticsearchClient.Indices.CreateAsync<AuditLog>(index, c =>
        {
            c.Mappings(map => map.Dynamic(DynamicMapping.False).Properties(ps => ps.
                    Text(x => x.Id).
                    Text(x => x.ApplicationName).
                    Text(x => x.UserId).
                    Text(x => x.UserName).
                    Text(x => x.TenantId).
                    Text(x => x.TenantName).
                    Text(x => x.ImpersonatorUserId).
                    Text(x => x.ImpersonatorUserName).
                    Text(x => x.ImpersonatorTenantId).
                    Text(x => x.ImpersonatorTenantName).
                    Date(x => x.ExecutionTime).
                    IntegerNumber(x => x.ExecutionDuration).
                    Ip(x => x.ClientIpAddress).
                    Text(x => x.ClientName).
                    Text(x => x.ClientId).
                    Text(x => x.CorrelationId).
                    Text(x => x.BrowserInfo).
                    Text(x => x.HttpMethod).
                    Text(x => x.Url).
                    Text(x => x.Exceptions).
                    Text(x => x.Comments).
                    IntegerNumber(x => x.HttpStatusCode).
                    Nested(nameof(AuditLog.EntityChanges).ToCamelCase(),
                        new NestedProperty
                        {
                            Properties = new Properties(new Dictionary<PropertyName, IProperty>
                            {
                                {
                                    nameof(EntityChange.Id).ToCamelCase(), new TextProperty()
                                },
                                {
                                    nameof(EntityChange.TenantId).ToCamelCase(), new TextProperty()
                                },
                                {
                                    nameof(EntityChange.ChangeTime).ToCamelCase(), new DateProperty()
                                },
                                {
                                    nameof(EntityChange.ChangeType).ToCamelCase(), new TextProperty()
                                },
                                {
                                    nameof(EntityChange.EntityTenantId).ToCamelCase(), new TextProperty()
                                },
                                {
                                    nameof(EntityChange.EntityId).ToCamelCase(), new TextProperty()
                                },
                                {
                                    nameof(EntityChange.EntityTypeFullName).ToCamelCase(), new TextProperty()
                                },
                                {
                                    nameof(EntityChange.PropertyChanges).ToCamelCase(), new NestedProperty()
                                    {
                                        Properties = new Properties(new Dictionary<PropertyName, IProperty>
                                        {
                                            {
                                                nameof(EntityPropertyChange.Id).ToCamelCase(), new TextProperty()
                                            },
                                            {
                                                nameof(EntityPropertyChange.TenantId).ToCamelCase(), new TextProperty()
                                            },
                                            {
                                                nameof(EntityPropertyChange.NewValue).ToCamelCase(), new TextProperty()
                                            },
                                            {
                                                nameof(EntityPropertyChange.OriginalValue).ToCamelCase(), new TextProperty()
                                            },
                                            {
                                                nameof(EntityPropertyChange.PropertyName).ToCamelCase(), new TextProperty()
                                            },
                                            {
                                                nameof(EntityPropertyChange.PropertyTypeFullName).ToCamelCase(), new TextProperty()
                                            },
                                        })
                                    }
                                },
                                {
                                    nameof(AuditLogAction.ExtraProperties).ToCamelCase(), new ObjectProperty()
                                }
                            })
                        }).
                    Nested(nameof(AuditLog.Actions).ToCamelCase(),
                        new NestedProperty
                        {
                            Properties = new Properties(new Dictionary<PropertyName, IProperty>
                            {
                                {
                                    nameof(AuditLogAction.Id).ToCamelCase(), new TextProperty()
                                },
                                {
                                    nameof(AuditLogAction.TenantId).ToCamelCase(), new TextProperty()
                                },
                                {
                                    nameof(AuditLogAction.ServiceName).ToCamelCase(), new TextProperty()
                                },
                                {
                                    nameof(AuditLogAction.MethodName).ToCamelCase(), new TextProperty()
                                },
                                {
                                    nameof(AuditLogAction.Parameters).ToCamelCase(), new TextProperty()
                                },
                                {
                                    nameof(AuditLogAction.ExecutionTime).ToCamelCase(), new DateProperty()
                                },
                                {
                                    nameof(AuditLogAction.ExecutionDuration).ToCamelCase(), new IntegerNumberProperty()
                                },
                                {
                                    nameof(AuditLogAction.ExtraProperties).ToCamelCase(), new ObjectProperty()
                                }
                            })
                        }).
                    Object(nested => nested.ExtraProperties)
                )
            );
        });
        if (!response.IsValidResponse)
        {
            throw new BusinessException(ElasticSearchErrorCodes.CreateIndexFailed, response.ApiCallDetails.OriginalException?.Message);
        }
    }
    
    public async Task<bool> SaveLogAsync(object logInfo) 
    {
        if (logInfo is not ElasticSearchAuditLogInfoEto eto)
        {
            throw new BusinessException(ElasticSearchErrorCodes.InvalidLogType);
        }
        await CreateIndexIfNotExistsAsync($"{ElasticSearchAuditLogSettings.Index}-{eto.ExecutionTime:yyyy-MM-dd}");
        var response = await ElasticsearchClient.CreateAsync<AuditLog>(eto, index: $"{ElasticSearchAuditLogSettings.Index}-{eto.ExecutionTime:yyyy-MM-dd}");
        if (!response.IsValidResponse)
        {
            throw new BusinessException(ElasticSearchErrorCodes.CreateDocumentFailed, response.ApiCallDetails.OriginalException?.Message);
        }
        return true;
    }

    public async Task<List<string>> GetIndicesAsync()
    {
        var index = $"{ElasticSearchAuditLogSettings.Index}-*";
        var response = await ElasticsearchClient.Indices.GetAsync<AuditLog>(index);
        if (!response.IsValidResponse)
        {
            throw new BusinessException(ElasticSearchErrorCodes.GetIndicesFailed, response.ApiCallDetails.OriginalException?.Message);
        }
        return response.Indices.Select(x => x.Key.ToString()).ToList();
    }

    public async Task DeleteIndicesAsync(List<string> indices)
    {
        var response = await ElasticsearchClient.Indices.DeleteAsync<AuditLog>(indices.JoinAsString(","));
        if (!response.IsValidResponse)
        {
            throw new BusinessException(ElasticSearchErrorCodes.DeleteIndicesFailed, response.ApiCallDetails.OriginalException?.Message);
        }
    }
}