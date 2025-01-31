using System.Globalization;
using System.Threading.Tasks;
using Feramor.Abp.AuditLogging.ElasticSearch.Jobs;
using Feramor.Abp.AuditLogging.ElasticSearch.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NCrontab;
using Volo.Abp;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Feramor.Abp.AuditLogging.ElasticSearch;

public class ElasticSearchHandlerModule : AbpModule
{
    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<ElasticSearchAuditLogSettings>>();
        var distributedCache = context.ServiceProvider.GetRequiredService<IDistributedCache>();

        if (options.Value.IsPeriodicDeleterEnabled)
        {
            var clock = context.ServiceProvider.GetRequiredService<IClock>();
            
            var schedule = CrontabSchedule.Parse(options.Value.PeriodicDeleterCron, new CrontabSchedule.ParseOptions()
            {
                IncludingSeconds = false
            });
            var nextDate = schedule.GetNextOccurrence(clock.Now);
            
            await distributedCache.SetStringAsync($"Next:{nameof(PeriodicAuditLogDeleterWorker)}", nextDate.ToString("O", CultureInfo.InvariantCulture));
            await context.AddBackgroundWorkerAsync<PeriodicAuditLogDeleterWorker>();
        }
        else
        {
            await distributedCache.RemoveAsync($"Next:{nameof(PeriodicAuditLogDeleterWorker)}");
        }
        await base.OnPostApplicationInitializationAsync(context);
    }
}