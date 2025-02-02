using Feramor.Abp.AuditLogging.ElasticSearch.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Permissions;

public class ElasticSearchPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var permissionGroup = context.AddGroup(ElasticSearchPermissions.GroupName, L("Permission:ElasticSearch"));
        var settingManagementPermission = permissionGroup.AddPermission(ElasticSearchPermissions.SettingsManagement.Default, L("Permission:SettingManagement"));
        var auditLogPermission = permissionGroup.AddPermission(ElasticSearchPermissions.AuditLogs.SubGroup, L("Permission:AuditLogs"));
        auditLogPermission.AddChild(ElasticSearchPermissions.AuditLogs.EntityChange, L("Permission:AuditLogs:EntityChange"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ElasticSearchResource>(name);
    }
}
