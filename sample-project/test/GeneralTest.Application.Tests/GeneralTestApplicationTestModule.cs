using Volo.Abp.Modularity;

namespace GeneralTest;

[DependsOn(
    typeof(GeneralTestApplicationModule),
    typeof(GeneralTestDomainTestModule)
)]
public class GeneralTestApplicationTestModule : AbpModule
{

}
