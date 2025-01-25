using System;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Feramor.Abp.AuditLogging.ElasticSearch.Enums;
using Feramor.Abp.AuditLogging.ElasticSearch.Managers;
using Feramor.Abp.AuditLogging.ElasticSearch.Permissions;
using Feramor.Abp.AuditLogging.ElasticSearch.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Settings;

[Authorize(ElasticSearchPermissions.SettingsManagement.Default)]
public class ElasticSearchAuditLogSettingsAppService : ElasticSearchAppService, IElasticSearchAuditLogSettingsAppService
{
    private protected ElasticSearchAuditLogSettings ElasticSearchAuditLogSettings { get; init; }
    private readonly ISettingManager _settingManager;
    private readonly IElasticSearchManager _elasticSearchManager;

    
    public ElasticSearchAuditLogSettingsAppService(IOptions<ElasticSearchAuditLogSettings> elasticSearchAuditLogSettings, ISettingManager settingManager, IElasticSearchManager elasticSearchManager)
    {
        _settingManager = settingManager;
        _elasticSearchManager = elasticSearchManager;
        ElasticSearchAuditLogSettings = elasticSearchAuditLogSettings.Value;
    }
    
    public Task<ElasticSearchAuditLogSettingsDto> GetAsync()
    {
        return Task.FromResult(new ElasticSearchAuditLogSettingsDto
        {
            IsActive = ElasticSearchAuditLogSettings.IsActive,
            Uri = ElasticSearchAuditLogSettings.Uri,
            UseSsl = ElasticSearchAuditLogSettings.UseSsl,
            SslFingerprint = ElasticSearchAuditLogSettings.SslFingerprint,
            AuthenticationType = ElasticSearchAuditLogSettings.AuthenticationType,
            Username = ElasticSearchAuditLogSettings.Username,
            Index = ElasticSearchAuditLogSettings.Index
        });
    }

    public async Task UpdateAsync(UpdateElasticSearchAuditLogSettingsDto input)
    {
        Check.NotNullOrWhiteSpace(input.Uri, nameof(input.Uri));
        
        if (!input.Uri.StartsWith("https://") && !input.Uri.StartsWith("http://"))
        {
            throw new BusinessException(ElasticSearchErrorCodes.UriMustStartWithHttpOrHttps);
        }
        
        if (input.UseSsl)
        {
            Check.NotNullOrWhiteSpace(input.SslFingerprint, nameof(input.SslFingerprint));
            if (!input.Uri.StartsWith("https://"))
            {
                throw new BusinessException(ElasticSearchErrorCodes.UriMustStartWithHttps);
            }
        }
        
        switch (input.AuthenticationType)
        {
            case ElasticSearchAuthenticationType.BasicAuthentication:
                Check.NotNullOrWhiteSpace(input.Username, nameof(input.Username));
                if (ElasticSearchAuditLogSettings.Password.IsNullOrWhiteSpace())
                {
                    Check.NotNullOrWhiteSpace(input.Password, nameof(input.Password));
                }
                break;
            case ElasticSearchAuthenticationType.ApiKeyAuthentication:
                if (ElasticSearchAuditLogSettings.ApiKey.IsNullOrWhiteSpace())
                {
                    Check.NotNullOrWhiteSpace(input.ApiKey, nameof(input.ApiKey));
                }
                break;
            case ElasticSearchAuthenticationType.Base64ApiKey:
                if (ElasticSearchAuditLogSettings.ApiKeyId.IsNullOrWhiteSpace())
                {
                    Check.NotNullOrWhiteSpace(input.ApiKeyId, nameof(input.ApiKeyId));
                }
                if (ElasticSearchAuditLogSettings.ApiKey.IsNullOrWhiteSpace())
                {
                    Check.NotNullOrWhiteSpace(input.ApiKey, nameof(input.ApiKey));
                }
                break;
            case null:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        await _settingManager.SetGlobalAsync(ElasticSearchSettings.IsActive, input.IsActive.ToString());
        await _settingManager.SetGlobalAsync(ElasticSearchSettings.Uri, input.Uri);
        await _settingManager.SetGlobalAsync(ElasticSearchSettings.UseSsl, input.UseSsl.ToString());
        await _settingManager.SetGlobalAsync(ElasticSearchSettings.SslFingerprint, input.SslFingerprint);
        await _settingManager.SetGlobalAsync(ElasticSearchSettings.AuthenticationType, ((int?)input.AuthenticationType)?.ToString());
        await _settingManager.SetGlobalAsync(ElasticSearchSettings.Username, input.Username);
        await _settingManager.SetGlobalAsync(ElasticSearchSettings.Password, input.Password);
        await _settingManager.SetGlobalAsync(ElasticSearchSettings.ApiKey, input.ApiKey);
        await _settingManager.SetGlobalAsync(ElasticSearchSettings.ApiKeyId, input.ApiKeyId);
        await _settingManager.SetGlobalAsync(ElasticSearchSettings.Index, input.Index);
        
        ElasticSearchAuditLogSettings.IsActive = input.IsActive;
        ElasticSearchAuditLogSettings.Uri = input.Uri;
        ElasticSearchAuditLogSettings.UseSsl = input.UseSsl;
        ElasticSearchAuditLogSettings.SslFingerprint = input.SslFingerprint;
        ElasticSearchAuditLogSettings.AuthenticationType = input.AuthenticationType;
        ElasticSearchAuditLogSettings.Username = input.Username;
        ElasticSearchAuditLogSettings.Password = input.Password;
        ElasticSearchAuditLogSettings.ApiKey = input.ApiKey;
        ElasticSearchAuditLogSettings.ApiKeyId = input.ApiKeyId;
        ElasticSearchAuditLogSettings.Index = input.Index;
    }

    public Task<bool> TestConnectionAsync()
    {
        return _elasticSearchManager.TestConnectionAsync();
    }
}