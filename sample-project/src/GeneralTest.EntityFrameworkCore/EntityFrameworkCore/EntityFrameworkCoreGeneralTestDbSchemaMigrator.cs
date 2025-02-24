using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GeneralTest.Data;
using Volo.Abp.DependencyInjection;

namespace GeneralTest.EntityFrameworkCore;

public class EntityFrameworkCoreGeneralTestDbSchemaMigrator
    : IGeneralTestDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreGeneralTestDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the GeneralTestDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<GeneralTestDbContext>()
            .Database
            .MigrateAsync();
    }
}
