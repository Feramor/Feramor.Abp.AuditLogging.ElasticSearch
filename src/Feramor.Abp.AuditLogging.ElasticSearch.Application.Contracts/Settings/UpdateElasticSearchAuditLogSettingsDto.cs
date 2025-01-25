using Feramor.Abp.AuditLogging.ElasticSearch.Enums;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Settings;

public class UpdateElasticSearchAuditLogSettingsDto
{
    public required bool IsActive { get; set; }
    public required string Uri { get; set; }
    public required bool UseSsl { get; set; }
    public string? SslFingerprint { get; set; }
    public ElasticSearchAuthenticationType? AuthenticationType { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? ApiKey { get; set; }
    public string? ApiKeyId { get; set; }
    public required string Index { get; set; }
}