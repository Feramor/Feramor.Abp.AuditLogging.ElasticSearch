using System;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Feramor.Abp.AuditLogging.ElasticSearch.Enums;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Settings;

public class ElasticSearchAuditLogSettings
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

    public ElasticsearchClientSettings GetElasticsearchClientSettings()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Uri, nameof(Uri));
        ArgumentException.ThrowIfNullOrWhiteSpace(Index, nameof(Index));
        
        if (!Uri.StartsWith("https://") && !Uri.StartsWith("http://"))
        {
            throw new ArgumentException($"Uri must start with http(s)://", nameof(Uri));
        }
        
        var settings = new ElasticsearchClientSettings(new Uri(Uri));
        
        if (UseSsl)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(SslFingerprint, nameof(SslFingerprint));
            if (!Uri.StartsWith("https://"))
            {
                throw new ArgumentException($"Uri must start with https://", nameof(Uri));
            }
            settings = settings.CertificateFingerprint(SslFingerprint);
        }

        switch (AuthenticationType)
        {
            case ElasticSearchAuthenticationType.BasicAuthentication:
                ArgumentException.ThrowIfNullOrWhiteSpace(Username, nameof(Username));
                ArgumentException.ThrowIfNullOrWhiteSpace(Password, nameof(Password));
                settings = settings.Authentication(new BasicAuthentication(Username, Password));
                break;
            case ElasticSearchAuthenticationType.ApiKeyAuthentication:
                ArgumentException.ThrowIfNullOrWhiteSpace(ApiKey, nameof(ApiKey));
                settings = settings.Authentication(new ApiKey(ApiKey));
                break;
            case ElasticSearchAuthenticationType.Base64ApiKey:
                ArgumentException.ThrowIfNullOrWhiteSpace(ApiKey, nameof(ApiKey));
                ArgumentException.ThrowIfNullOrWhiteSpace(ApiKeyId, nameof(ApiKeyId));
                settings = settings.Authentication(new Base64ApiKey(ApiKeyId, ApiKey));
                break;
            case null:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return settings;
    }
}