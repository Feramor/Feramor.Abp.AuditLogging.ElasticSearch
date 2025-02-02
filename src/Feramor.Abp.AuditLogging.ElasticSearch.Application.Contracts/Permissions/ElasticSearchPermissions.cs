using Volo.Abp.Reflection;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Permissions;

public class ElasticSearchPermissions
{
    public const string GroupName = "ElasticSearch";

    public class SettingsManagement
    {
        public const string Default = GroupName + ".SettingManagement";
    }
    
    public class AuditLogs
    {
        public const string SubGroup = GroupName + ".AuditLogs";
        public const string EntityChange = SubGroup + ".EntityChange";
    }
    
    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ElasticSearchPermissions));
    }
}
