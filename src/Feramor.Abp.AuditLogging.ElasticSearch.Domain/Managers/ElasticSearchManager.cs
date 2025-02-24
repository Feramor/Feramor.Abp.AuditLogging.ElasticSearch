using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Feramor.Abp.AuditLogging.ElasticSearch.Data;
using Feramor.Abp.AuditLogging.ElasticSearch.Eto;
using Feramor.Abp.AuditLogging.ElasticSearch.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
    private ElasticSearchAuditLogSettings ElasticSearchAuditLogSettings { get; init; }
    private ElasticsearchClient ElasticsearchClient { get; init; }
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
        var response = await ElasticsearchClient.CreateAsync<ElasticSearchAuditLog>(eto, index: $"{ElasticSearchAuditLogSettings.Index}-{eto.ExecutionTime:yyyy-MM-dd}");
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
    public async Task<List<ElasticSearchAuditLog?>?> GetListAsync(string? sorting = null, int maxResultCount = 50, int skipCount = 0, DateTime? startTime = null,
        DateTime? endTime = null, string? httpMethod = null, string? url = null, string? clientId = null, Guid? userId = null,
        string? userName = null, string? applicationName = null, string? clientIpAddress = null, string? correlationId = null,
        int? maxExecutionDuration = null, int? minExecutionDuration = null, bool? hasException = null,
        HttpStatusCode? httpStatusCode = null, bool includeDetails = false, CancellationToken cancellationToken = default)
    { 
        var index = $"{ElasticSearchAuditLogSettings.Index}-*";
        var response = await ElasticsearchClient.SearchAsync<ElasticSearchAuditLog>(s => s
            .Index(index)
            .Query(GenerateQuery(startTime, endTime, httpMethod, url, clientId, userId, userName, applicationName, clientIpAddress, correlationId, maxExecutionDuration, minExecutionDuration, hasException, httpStatusCode))
            .Sort(GenerateSort<ElasticSearchAuditLog>(sorting))
            .From(skipCount)
            .Size(maxResultCount)
        , cancellationToken);
        if (!response.IsValidResponse)
        {
            throw new BusinessException(ElasticSearchErrorCodes.GetLogsFailed, response.ApiCallDetails.OriginalException?.Message);
        }
        
        return response.Hits.Select(hit => hit.Source)?.Select(x =>
        {
            if (includeDetails)
            {
                return x;
            }
            x?.Actions.Clear();
            x?.EntityChanges.Clear();
            return x;
        }).ToList();
    }

    public async Task<long> GetCountAsync(DateTime? startTime = null, DateTime? endTime = null, string? httpMethod = null, string? url = null,
        string? clientId = null, Guid? userId = null, string? userName = null, string? applicationName = null,
        string? clientIpAddress = null, string? correlationId = null, int? maxExecutionDuration = null,
        int? minExecutionDuration = null, bool? hasException = null, HttpStatusCode? httpStatusCode = null,
        CancellationToken cancellationToken = default)
    {
        var index = $"{ElasticSearchAuditLogSettings.Index}-*";
        var response = await ElasticsearchClient.CountAsync<ElasticSearchAuditLog>(s => s
                .Indices(index)
                .Query(GenerateQuery(startTime, endTime, httpMethod, url, clientId, userId, userName, 
                    applicationName, clientIpAddress, correlationId, maxExecutionDuration, 
                    minExecutionDuration, hasException, httpStatusCode))
            , cancellationToken);
        if (!response.IsValidResponse)
        {
            throw new BusinessException(ElasticSearchErrorCodes.GetLogsFailed, response.ApiCallDetails.OriginalException?.Message);
        }
        return response.Count;
    }

    public async Task<ElasticSearchAuditLog?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var index = $"{ElasticSearchAuditLogSettings.Index}-*";
        var response = await ElasticsearchClient.SearchAsync<ElasticSearchAuditLog>(s => s
                .Index(index)
                .Query(GenerateQuery(id))
            , cancellationToken);
        if (!response.IsValidResponse)
        {
            throw new BusinessException(ElasticSearchErrorCodes.GetLogsFailed, response.ApiCallDetails.OriginalException?.Message);
        }
        return response.Hits.Select(hit => hit.Source)?.FirstOrDefault();
    }

    private static BoolQuery GenerateQuery(DateTime? startTime = null, DateTime? endTime = null, string? httpMethod = null, 
        string? url = null, string? clientId = null, Guid? userId = null, string? userName = null, 
        string? applicationName = null, string? clientIpAddress = null, string? correlationId = null, 
        int? maxExecutionDuration = null, int? minExecutionDuration = null, bool? hasException = null, 
        HttpStatusCode? httpStatusCode = null)
    {
        var query = new BoolQuery
        {
            Must = new List<Query>()
        };
        
        if (startTime.HasValue)
        {
            query.Must.Add(new DateRangeQuery("executionTime")
            {
                Gte = new DateMathExpression(startTime.Value),
            });
        }
        
        if (endTime.HasValue)
        {
            query.Must.Add(new DateRangeQuery("executionTime")
            {
                Lte = new DateMathExpression(endTime.Value),
            });
        }
        
        if (!httpMethod.IsNullOrWhiteSpace())
        {
            query.Must.Add(new TermQuery(nameof(httpMethod))
            {
                CaseInsensitive = true,
                Value = httpMethod
            });
        }  
        
        if (!url.IsNullOrWhiteSpace())
        {
            query.Must.Add(new WildcardQuery(nameof(url))
            {
                CaseInsensitive = true,
                Value = $"*{url}*"
            });
        }
        
        if (!clientId.IsNullOrWhiteSpace())
        {
            query.Must.Add(new TermQuery(nameof(clientId))
            {
                CaseInsensitive = true,
                Value = clientId
            });
        }  
        
        if (userId.HasValue)
        {
            query.Must.Add(new TermQuery(nameof(userId))
            {
                CaseInsensitive = true,
                Value = userId.ToString()
            });
        }  
        
        if (!userName.IsNullOrWhiteSpace())
        {
            query.Must.Add(new TermQuery(nameof(userName))
            {
                CaseInsensitive = true,
                Value = userName
            });
        }  
        
        if (!applicationName.IsNullOrWhiteSpace())
        {
            query.Must.Add(new TermQuery(nameof(applicationName))
            {
                CaseInsensitive = true,
                Value = applicationName
            });
        }  
        
        if (!clientIpAddress.IsNullOrWhiteSpace())
        {
            query.Must.Add(new TermQuery(nameof(clientIpAddress))
            {
                Value = clientIpAddress
            });
        }  
        
        if (!correlationId.IsNullOrWhiteSpace())
        {
            query.Must.Add(new TermQuery(nameof(correlationId))
            {
                CaseInsensitive = true,
                Value = correlationId
            });
        }  
        
        if (maxExecutionDuration is > 0)
        {
            query.Must.Add(new NumberRangeQuery("executionDuration")
            {
                Lte = maxExecutionDuration,
            });
        }
        
        if (minExecutionDuration is > 0)
        {
            query.Must.Add(new NumberRangeQuery("executionDuration")
            {
                Gte = minExecutionDuration,
            });
        }
        
        if (hasException.HasValue)
        {
            if (hasException.Value)
            {
                query.Must.Add(new ExistsQuery
                {
                    Field = "exceptions"
                });
            }
            else
            {
                query.Must.Add(new BoolQuery
                {
                    Should = new List<Query>
                    {
                        new TermQuery("exceptions") { Value = "" },
                        new BoolQuery { MustNot = [new ExistsQuery() { Field = "exceptions" }] }
                    },
                    MinimumShouldMatch = 1
                });
            }
        }
        
        if (httpStatusCode is > 0)
        {
            query.Must.Add(new TermQuery(nameof(httpStatusCode))
            {
                Value = (int)httpStatusCode
            });
        }
        
        return query;
    }
    
        private static BoolQuery GenerateQuery(string id)
    {
        var query = new BoolQuery
        {
            Must = new List<Query>()
        };
        
        if (!id.IsNullOrWhiteSpace())
        {
            query.Must.Add(new IdsQuery()
            {
                Values = new Ids(id.ToString())
            });
        }  
        
        return query;
    }

    private static SortOptionsDescriptor<T> GenerateSort<T>(string? sorting = null)
    {
        string sortField = "executionTime";
        string sortOrder = "DESC";

        if (!sorting.IsNullOrWhiteSpace())
        {
            sortField = sorting.Split(' ').First();
            sortOrder = sorting.ToUpper().Contains("DESC") ? "DESC" : "ASC";
        }

        return new SortOptionsDescriptor<T>().Field(sortField, new FieldSort
        {
            Order = sortOrder == "DESC" ? SortOrder.Desc : SortOrder.Asc,
        });
    }
}