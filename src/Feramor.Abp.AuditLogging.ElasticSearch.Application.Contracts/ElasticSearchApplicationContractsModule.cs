using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Feramor.Abp.AuditLogging.ElasticSearch;

[DependsOn(
    typeof(ElasticSearchDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class ElasticSearchApplicationContractsModule : AbpModule
{

}
