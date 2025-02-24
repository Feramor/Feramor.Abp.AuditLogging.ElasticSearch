using Volo.Abp.Settings;

namespace GeneralTest.Settings;

public class GeneralTestSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(GeneralTestSettings.MySetting1));
    }
}
