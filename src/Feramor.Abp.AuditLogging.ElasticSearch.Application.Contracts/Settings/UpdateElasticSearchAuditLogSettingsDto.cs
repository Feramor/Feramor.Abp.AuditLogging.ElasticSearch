using Feramor.Abp.AuditLogging.ElasticSearch.Enums;
using Volo.Abp.Auditing;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Settings;

public class UpdateElasticSearchAuditLogSettingsDto
{
    public required bool IsActive { get; set; }
    public required string Uri { get; set; }
    public required bool UseSsl { get; set; }
    [DisableAuditing]
    public string? SslFingerprint { get; set; }
    public ElasticSearchAuthenticationType? AuthenticationType { get; set; }
    [DisableAuditing]
    public string? Username { get; set; }
    [DisableAuditing]
    public string? Password { get; set; }
    [DisableAuditing]
    public string? ApiKey { get; set; }
    [DisableAuditing]
    public string? ApiKeyId { get; set; }
    public required string Index { get; set; }
}