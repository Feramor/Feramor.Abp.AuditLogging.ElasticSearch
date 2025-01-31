using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Feramor.Abp.AuditLogging.ElasticSearch.Managers;
using Feramor.Abp.AuditLogging.ElasticSearch.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Caching;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Threading;
using Volo.Abp.Timing;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Jobs;

public class PeriodicAuditLogDeleterWorker : AsyncPeriodicBackgroundWorkerBase
{
    public PeriodicAuditLogDeleterWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
    {
        Timer.Period = 60000;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        var distributedLock = workerContext.ServiceProvider.GetRequiredService<IAbpDistributedLock>();

        await using (var handle = await distributedLock.TryAcquireAsync($"Lock:{nameof(PeriodicAuditLogDeleterWorker)}"))
        {
            if (handle != null)
            {
                var options = workerContext.ServiceProvider.GetRequiredService<IOptions<ElasticSearchAuditLogSettings>>();
                
                if (!options.Value.IsActive || !options.Value.IsPeriodicDeleterEnabled)
                {
                    return;
                }
                
                var clock = workerContext.ServiceProvider.GetRequiredService<IClock>();
                var distributedCache = workerContext.ServiceProvider.GetRequiredService<IDistributedCache>();
                var cachedNextDate = await distributedCache.GetStringAsync($"Next:{nameof(PeriodicAuditLogDeleterWorker)}");

                if (DateTime.TryParse(cachedNextDate, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var nextDate))
                {
                    if (nextDate > clock.Now)
                    {
                        return;
                    }
                }
        
                var elasticSearchManager = workerContext.ServiceProvider.GetRequiredService<IElasticSearchManager>();

                Logger.LogInformation("Periodic audit log deleter worker started");
                
                var indices = await elasticSearchManager.GetIndicesAsync();
                
                if (indices.Count > 0)
                {
                    var indicesToRemoved = indices
                        .Select(x => DateTime.ParseExact(x.RemovePreFix($"{options.Value.Index}-"), "yyyy-MM-dd", CultureInfo.InvariantCulture))
                        .Where(x => x < clock.Now.Subtract(TimeSpan.FromDays(options.Value.PeriodicDeleterPeriod)))
                        .Select(x => $"{options.Value.Index}-{x:yyyy-MM-dd}")
                        .ToList();
                    await elasticSearchManager.DeleteIndicesAsync(indicesToRemoved);
                }
                
                Logger.LogInformation("Periodic audit log deleter worker finished");
                
                var schedule = CrontabSchedule.Parse(options.Value.PeriodicDeleterCron);
                nextDate = schedule.GetNextOccurrence(clock.Now);
                await distributedCache.SetStringAsync($"Next:{nameof(PeriodicAuditLogDeleterWorker)}", nextDate.ToString("O"));
            }
        }   
    }
}