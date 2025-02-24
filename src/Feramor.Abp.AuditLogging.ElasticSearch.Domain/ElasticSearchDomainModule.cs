using System;
using System.Globalization;
using System.Threading.Tasks;
using Feramor.Abp.AuditLogging.ElasticSearch.Enums;
using Feramor.Abp.AuditLogging.ElasticSearch.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NCrontab;
using Volo.Abp;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Data;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using Volo.Abp.Timing;

namespace Feramor.Abp.AuditLogging.ElasticSearch;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpAuditLoggingDomainModule),
    typeof(AbpSettingManagementDomainModule),
    typeof(ElasticSearchDomainSharedModule)
)]
public class ElasticSearchDomainModule : AbpModule
{
    public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        Configure<ElasticSearchAuditLogSettings>(options =>
        {
        });
        return base.ConfigureServicesAsync(context);
    }

    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        if (!context.ServiceProvider.IsDataMigrationEnvironment())
        { 
            var options = context.ServiceProvider.GetRequiredService<IOptions<ElasticSearchAuditLogSettings>>();
            var settingManager = context.ServiceProvider.GetRequiredService<ISettingProvider>();
            
            options.Value.IsActive = settingManager.GetOrNullAsync(ElasticSearchSettings.IsActive).GetAwaiter().GetResult()?.To<bool>() ?? false;
            options.Value.Uri = settingManager.GetOrNullAsync(ElasticSearchSettings.Uri).GetAwaiter().GetResult() ?? "https://localhost:9200";
            options.Value.UseSsl = settingManager.GetOrNullAsync(ElasticSearchSettings.UseSsl).GetAwaiter().GetResult()?.To<bool>() ?? false;
            options.Value.SslFingerprint = settingManager.GetOrNullAsync(ElasticSearchSettings.SslFingerprint).GetAwaiter().GetResult();
            options.Value.AuthenticationType = (ElasticSearchAuthenticationType?)settingManager.GetOrNullAsync(ElasticSearchSettings.AuthenticationType).GetAwaiter().GetResult()?.To<int>();
            options.Value.Username = settingManager.GetOrNullAsync(ElasticSearchSettings.Username).GetAwaiter().GetResult();
            options.Value.Password = settingManager.GetOrNullAsync(ElasticSearchSettings.Password).GetAwaiter().GetResult();
            options.Value.ApiKey = settingManager.GetOrNullAsync(ElasticSearchSettings.ApiKey).GetAwaiter().GetResult();
            options.Value.ApiKeyId = settingManager.GetOrNullAsync(ElasticSearchSettings.ApiKeyId).GetAwaiter().GetResult();
            options.Value.Index = settingManager.GetOrNullAsync(ElasticSearchSettings.Index).GetAwaiter().GetResult() ?? "feramor-abp-audit-logging";
            options.Value.IsPeriodicDeleterEnabled = settingManager.GetOrNullAsync(ElasticSearchSettings.IsPeriodicDeleterEnabled).GetAwaiter().GetResult()?.To<bool>() ?? false;
            options.Value.PeriodicDeleterCron = settingManager.GetOrNullAsync(ElasticSearchSettings.PeriodicDeleterCron).GetAwaiter().GetResult() ?? "0 0 * * *";
            options.Value.PeriodicDeleterPeriod = settingManager.GetOrNullAsync(ElasticSearchSettings.PeriodicDeleterPeriod).GetAwaiter().GetResult()?.To<int>() ?? 0;
        }
        await base.OnPostApplicationInitializationAsync(context);
    }
}
