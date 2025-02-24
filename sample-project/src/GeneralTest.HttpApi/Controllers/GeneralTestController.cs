using GeneralTest.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace GeneralTest.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class GeneralTestController : AbpControllerBase
{
    protected GeneralTestController()
    {
        LocalizationResource = typeof(GeneralTestResource);
    }
}
