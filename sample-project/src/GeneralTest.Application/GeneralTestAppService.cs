using GeneralTest.Localization;
using Volo.Abp.Application.Services;

namespace GeneralTest;

/* Inherit your application services from this class.
 */
public abstract class GeneralTestAppService : ApplicationService
{
    protected GeneralTestAppService()
    {
        LocalizationResource = typeof(GeneralTestResource);
    }
}
