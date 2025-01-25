using Feramor.Abp.AuditLogging.ElasticSearch.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Feramor.Abp.AuditLogging.ElasticSearch;

public abstract class ElasticSearchController : AbpControllerBase
{
    protected ElasticSearchController()
    {
        LocalizationResource = typeof(ElasticSearchResource);
    }
}
