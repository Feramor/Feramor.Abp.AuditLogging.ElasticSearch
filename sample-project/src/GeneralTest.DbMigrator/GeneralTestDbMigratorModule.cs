using GeneralTest.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace GeneralTest.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(GeneralTestEntityFrameworkCoreModule),
    typeof(GeneralTestApplicationContractsModule)
)]
public class GeneralTestDbMigratorModule : AbpModule
{
}
