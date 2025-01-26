namespace Feramor.Abp.AuditLogging.ElasticSearch.Settings;

public static class ElasticSearchSettings
{
    public const string GroupName = "ElasticSearch";
    
    public const string IsActive = GroupName + ".IsActive";
    public const string Uri = GroupName + ".Uri";
    public const string UseSsl = GroupName + ".UseSsl";
    public const string SslFingerprint = GroupName + ".SslFingerprint";
    public const string AuthenticationType = GroupName + ".AuthenticationType";
    public const string Username = GroupName + ".Username";
    public const string Password = GroupName + ".Password";
    public const string ApiKey = GroupName + ".ApiKey";
    public const string ApiKeyId = GroupName + ".ApiKeyId";
    public const string Index = GroupName + ".Index";
}