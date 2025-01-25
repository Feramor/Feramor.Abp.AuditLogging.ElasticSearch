using System;
using Feramor.Abp.AuditLogging.ElasticSearch.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Settings;

public class ElasticSearchSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(new SettingDefinition(ElasticSearchSettings.IsActive, "false", L("DisplayName:IsActive"), L("Description:IsActive"), isVisibleToClients: false));
        context.Add(new SettingDefinition(ElasticSearchSettings.Uri, "https://localhost:9200", L("DisplayName:Uri"), L("Description:Uri"), isVisibleToClients: false));
        context.Add(new SettingDefinition(ElasticSearchSettings.UseSsl, "false", L("DisplayName:UseSsl"), L("Description:UseSsl"), isVisibleToClients: false));
        context.Add(new SettingDefinition(ElasticSearchSettings.SslFingerprint, null, L("DisplayName:SslFingerprint"), L("Description:SslFingerprint"), isVisibleToClients: false));
        context.Add(new SettingDefinition(ElasticSearchSettings.AuthenticationType, "0", L("DisplayName:AuthenticationType"), L("Description:AuthenticationType"), isVisibleToClients: false));
        context.Add(new SettingDefinition(ElasticSearchSettings.Username, null, L("DisplayName:Username"), L("Description:Username"), isVisibleToClients: false));
        context.Add(new SettingDefinition(ElasticSearchSettings.Password, null, L("DisplayName:Password"), L("Description:Password"), isVisibleToClients: false, isEncrypted: true));
        context.Add(new SettingDefinition(ElasticSearchSettings.ApiKey, null, L("DisplayName:ApiKey"), L("Description:ApiKey"), isVisibleToClients: false, isEncrypted: true));
        context.Add(new SettingDefinition(ElasticSearchSettings.ApiKeyId, null, L("DisplayName:ApiKeyId"), L("Description:ApiKeyId"), isVisibleToClients: false, isEncrypted: true));
        context.Add(new SettingDefinition(ElasticSearchSettings.Index, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToKebabCase() , L("DisplayName:Index"), L("Description:Index"), isVisibleToClients: false, isEncrypted: true));
    }
    
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ElasticSearchResource>(name);
    }
}