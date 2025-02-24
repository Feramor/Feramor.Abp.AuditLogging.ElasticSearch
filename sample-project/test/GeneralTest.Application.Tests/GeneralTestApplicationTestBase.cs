using Volo.Abp.Modularity;

namespace GeneralTest;

public abstract class GeneralTestApplicationTestBase<TStartupModule> : GeneralTestTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
