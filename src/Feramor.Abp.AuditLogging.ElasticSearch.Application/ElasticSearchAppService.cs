using Feramor.Abp.AuditLogging.ElasticSearch.Localization;
using Volo.Abp.Application.Services;

namespace Feramor.Abp.AuditLogging.ElasticSearch;

public abstract class ElasticSearchAppService : ApplicationService
{
    protected ElasticSearchAppService()
    {
        LocalizationResource = typeof(ElasticSearchResource);
        ObjectMapperContext = typeof(ElasticSearchApplicationModule);
    }
}
