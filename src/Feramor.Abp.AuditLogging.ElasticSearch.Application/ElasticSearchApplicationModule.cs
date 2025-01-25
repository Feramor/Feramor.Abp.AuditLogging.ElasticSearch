using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Feramor.Abp.AuditLogging.ElasticSearch;

[DependsOn(
    typeof(ElasticSearchDomainModule),
    typeof(ElasticSearchApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class ElasticSearchApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<ElasticSearchApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ElasticSearchApplicationModule>(validate: true);
        });
    }
}
