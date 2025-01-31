using Feramor.Abp.AuditLogging.ElasticSearch.Enums;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Settings;

public class ElasticSearchAuditLogSettingsDto
{
    public required bool IsActive { get; set; }
    public required string Uri { get; set; }
    public required bool UseSsl { get; set; }
    public string? SslFingerprint { get; set; }
    public ElasticSearchAuthenticationType? AuthenticationType { get; set; }
    public string? Username { get; set; }
    public required string Index { get; set; }
    public required bool IsPeriodicDeleterEnabled { get; set; }
    public required string PeriodicDeleterCron { get; set; }
    public required int PeriodicDeleterPeriod { get; set; }
}