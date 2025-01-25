using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Feramor.Abp.AuditLogging.ElasticSearch.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Feramor.Abp.AuditLogging.ElasticSearch;

[DependsOn(
    typeof(AbpValidationModule),
    typeof(AbpAuditLoggingDomainSharedModule),
    typeof(AbpDddDomainSharedModule)
)]
public class ElasticSearchDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ElasticSearchDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<ElasticSearchResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/ElasticSearch");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("ElasticSearch", typeof(ElasticSearchResource));
        });
    }
}
