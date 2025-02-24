using Volo.Abp.Modularity;

namespace GeneralTest;

[DependsOn(
    typeof(GeneralTestDomainModule),
    typeof(GeneralTestTestBaseModule)
)]
public class GeneralTestDomainTestModule : AbpModule
{

}
