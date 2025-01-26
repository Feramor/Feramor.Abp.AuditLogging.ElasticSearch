using Feramor.Abp.AuditLogging.ElasticSearch.AuditingStore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Auditing;
using Volo.Abp.Modularity;

namespace Feramor.Abp.AuditLogging.ElasticSearch;

public class ElasticSearchLoggerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Replace(ServiceDescriptor.Transient<IAuditingStore, ElasticSearchAuditingStore>());
    }
}