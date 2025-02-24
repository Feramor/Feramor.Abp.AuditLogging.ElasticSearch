using Microsoft.Extensions.Localization;
using GeneralTest.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace GeneralTest;

[Dependency(ReplaceServices = true)]
public class GeneralTestBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<GeneralTestResource> _localizer;

    public GeneralTestBrandingProvider(IStringLocalizer<GeneralTestResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
