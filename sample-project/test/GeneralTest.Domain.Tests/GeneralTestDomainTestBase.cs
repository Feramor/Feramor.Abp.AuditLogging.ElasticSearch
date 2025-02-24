using Volo.Abp.Modularity;

namespace GeneralTest;

/* Inherit from this class for your domain layer tests. */
public abstract class GeneralTestDomainTestBase<TStartupModule> : GeneralTestTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
