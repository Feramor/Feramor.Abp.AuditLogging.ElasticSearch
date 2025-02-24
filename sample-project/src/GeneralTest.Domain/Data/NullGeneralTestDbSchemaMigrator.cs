using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace GeneralTest.Data;

/* This is used if database provider does't define
 * IGeneralTestDbSchemaMigrator implementation.
 */
public class NullGeneralTestDbSchemaMigrator : IGeneralTestDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
