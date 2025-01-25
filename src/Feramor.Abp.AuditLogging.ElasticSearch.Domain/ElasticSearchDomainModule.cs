using System;
using System.Threading.Tasks;
using Feramor.Abp.AuditLogging.ElasticSearch.Enums;
using Feramor.Abp.AuditLogging.ElasticSearch.Settings;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Feramor.Abp.AuditLogging.ElasticSearch;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpAuditLoggingDomainModule),
    typeof(ElasticSearchDomainSharedModule)
)]
public class ElasticSearchDomainModule : AbpModule
{
    public override async Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        var settingManager = context.Services.GetRequiredService<ISettingManager>();
        Configure<ElasticSearchAuditLogSettings>(options =>
        {
            options.IsActive = settingManager.GetOrNullDefaultAsync(ElasticSearchSettings.IsActive).GetAwaiter().GetResult().To<bool>();
            options.Uri = settingManager.GetOrNullDefaultAsync(ElasticSearchSettings.Uri).GetAwaiter().GetResult();
            options.UseSsl = settingManager.GetOrNullDefaultAsync(ElasticSearchSettings.UseSsl).GetAwaiter().GetResult().To<bool>();
            options.SslFingerprint = settingManager.GetOrNullDefaultAsync(ElasticSearchSettings.SslFingerprint).GetAwaiter().GetResult();
            options.AuthenticationType = settingManager.GetOrNullDefaultAsync(ElasticSearchSettings.AuthenticationType).GetAwaiter().GetResult()?.To<ElasticSearchAuthenticationType>();
            options.Username = settingManager.GetOrNullDefaultAsync(ElasticSearchSettings.Username).GetAwaiter().GetResult();
            options.Password = settingManager.GetOrNullDefaultAsync(ElasticSearchSettings.Password).GetAwaiter().GetResult();
            options.ApiKey = settingManager.GetOrNullDefaultAsync(ElasticSearchSettings.ApiKey).GetAwaiter().GetResult();
            options.ApiKeyId = settingManager.GetOrNullDefaultAsync(ElasticSearchSettings.ApiKeyId).GetAwaiter().GetResult();
            options.Index = settingManager.GetOrNullDefaultAsync(ElasticSearchSettings.Index).GetAwaiter().GetResult();
        });
    }
}
