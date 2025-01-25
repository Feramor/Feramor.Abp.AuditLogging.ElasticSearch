using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Feramor.Abp.AuditLogging.ElasticSearch;

[DependsOn(
    typeof(ElasticSearchApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class ElasticSearchHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(ElasticSearchApplicationContractsModule).Assembly,
            ElasticSearchRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ElasticSearchHttpApiClientModule>();
        });

    }
}
