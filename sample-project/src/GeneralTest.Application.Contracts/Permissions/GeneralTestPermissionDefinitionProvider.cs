using GeneralTest.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace GeneralTest.Permissions;

public class GeneralTestPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GeneralTestPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(GeneralTestPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GeneralTestResource>(name);
    }
}
